DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT                       -- SELECT list can stay empty for this
      FROM   pg_catalog.pg_user
      WHERE  usename = 'spurioususer') THEN

      CREATE USER spurioususer WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION;
   END IF;
END
$do$;

CREATE DATABASE spurious
    WITH 
    OWNER = spurioususer
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;

DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT                       -- SELECT list can stay empty for this
      FROM   pg_extension
      WHERE  extname = 'adminpack') THEN

      create extension adminpack;
   END IF;
END
$do$;

DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT                       -- SELECT list can stay empty for this
      FROM   pg_extension
      WHERE  extname = 'postgis') THEN

      create extension postgis;
   END IF;
END
$do$;

create table IF NOT EXISTS boundary_incoming (
  id integer not null,
  boundary_gml text,
  name text,
  province text,
  CONSTRAINT bi_firstkey PRIMARY KEY (id)
);

ALTER TABLE boundary_incoming
  OWNER TO spurioususer;

create table IF NOT EXISTS population_incoming (
  id integer not null,
  population integer,
  name text,
  CONSTRAINT pi_firstkey PRIMARY KEY (id)
);

ALTER TABLE population_incoming
  OWNER TO spurioususer;

create table IF NOT EXISTS store_incoming (
  id integer not null,
  latitude decimal,
  longitude decimal,
  name text,
  city text,
  CONSTRAINT si_firstkey PRIMARY KEY (id)
);

ALTER TABLE store_incoming
  OWNER TO spurioususer;

create table IF NOT EXISTS product_incoming (
  id integer NOT NULL,
  name text,
  category text,
  volume integer,
  CONSTRAINT pri_firstkey PRIMARY KEY (id)
);

ALTER TABLE product_incoming
  OWNER TO spurioususer;

CREATE TABLE IF NOT EXISTS inventory_incoming
(
  product_id integer NOT NULL,
  store_id integer NOT NULL,
  quantity integer,
  CONSTRAINT inventory_incoming_pkey PRIMARY KEY (product_id, store_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE inventory_incoming
  OWNER TO spurioususer;

create table IF NOT EXISTS subdivisions (
    id integer NOT NULL,
    population integer NOT NULL DEFAULT 0,
    boundary geography,
    beer_volume bigint NOT NULL DEFAULT 0,
    wine_volume bigint NOT NULL DEFAULT 0,
    spirits_volume bigint NOT NULL DEFAULT 0,
    province text COLLATE pg_catalog."default",
    name text COLLATE pg_catalog."default",
    average_income integer NOT NULL DEFAULT 0,
    median_income integer NOT NULL DEFAULT 0,
    median_after_tax_income integer NOT NULL DEFAULT 0,
    average_after_tax_income integer NOT NULL DEFAULT 0,
    geographic_centre geography,
    alcohol_density numeric(9,2),
    beer_density numeric(9,2),
    wine_density numeric(9,2),
    spirits_density numeric(9,2),
  CONSTRAINT firstkey PRIMARY KEY (id)
);

ALTER TABLE subdivisions
  OWNER TO spurioususer;

CREATE INDEX IF NOT EXISTS idx_boundary
  ON subdivisions
  USING gist
  (boundary);

-- Table: stores

-- DROP TABLE stores;

CREATE TABLE IF NOT EXISTS stores
(
  id integer NOT NULL,
  name text,
  city text,
  beer_volume integer,
  wine_volume integer,
  spirits_volume integer,
  location geography,
  CONSTRAINT stores_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE stores
  OWNER TO spurioususer;

CREATE INDEX IF NOT EXISTS idx_location
  ON stores
  USING gist
  (location);

CREATE TABLE IF NOT EXISTS products
(
  id integer NOT NULL,
  name text,
  category text,
  volume integer,
  PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE products
  OWNER TO spurioususer;
  
CREATE TABLE IF NOT EXISTS inventories
(
  product_id integer NOT NULL,
  store_id integer NOT NULL,
  quantity integer,
  CONSTRAINT inventories_pkey PRIMARY KEY (product_id, store_id),
  CONSTRAINT inventories_product_id_fkey FOREIGN KEY (product_id)
      REFERENCES products (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE,
  CONSTRAINT inventories_store_id_fkey FOREIGN KEY (store_id)
      REFERENCES stores (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE
)
WITH (
  OIDS=FALSE
);
ALTER TABLE inventories
  OWNER TO spurioususer;

-- Index: fki_inventories_stores_id_fkey

-- DROP INDEX fki_inventories_stores_id_fkey;

CREATE INDEX IF NOT EXISTS fki_inventories_stores_id_fkey
  ON inventories
  USING btree
  (store_id);

CREATE TABLE public.inventory_pages
(
    content text COLLATE pg_catalog."default" NULL,
    product_id integer NOT NULL,
    compressed_content bytea null,
    CONSTRAINT inventory_pages_pkey PRIMARY KEY (product_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.inventory_pages
    OWNER to spurioususer;
    
-- Performs a bulk insert/update/delete based on http://dba.stackexchange.com/questions/73066/efficient-way-to-insert-update-delete-table-records-from-complex-query-in-postgr

CREATE OR REPLACE procedure update_boundaries_from_incoming()
    LANGUAGE 'sql'

AS $BODY$
DELETE FROM subdivisions s    -- with EXISTS semi-anti-join
WHERE NOT EXISTS (
   SELECT 1 FROM boundary_incoming bi
   WHERE  bi.id = s.id
   );

insert into subdivisions
(id, name, boundary, province) 
select id, bi.name,  (select ST_Transform(ST_GeomFromGML(bi.boundary_gml, 3347), 4326)), bi.province 
from boundary_incoming bi 
left join subdivisions s using (id)
where s.id is null;

update subdivisions s 
set (boundary, province) = (select ST_Transform(ST_GeomFromGML(bi.boundary_gml, 3347), 4326), bi.province) 
from boundary_incoming bi 
where s.id = bi.id;

update subdivisions
set geographic_centre = ST_centroid(boundary);
$BODY$;

ALTER procedure public.update_boundaries_from_incoming()
    OWNER TO spurioususer;

CREATE OR REPLACE procedure public.update_populations_from_incoming()
    LANGUAGE 'sql'

AS $BODY$
DELETE FROM subdivisions s    -- with EXISTS semi-anti-join
WHERE NOT EXISTS (
   SELECT 1 FROM population_incoming pi
   WHERE  pi.id = s.id
   );

insert into subdivisions
(id, name, population) 
select id, pi.name, pi.population
from population_incoming pi 
left join subdivisions s using (id)
where s.id is null;

update subdivisions s 
set (population) 
= (select pi.population)
from population_incoming pi 
where s.id = pi.id;
$BODY$;

ALTER procedure public.update_populations_from_incoming()
    OWNER TO spurioususer;

CREATE OR REPLACE procedure public.update_stores_from_incoming()
    LANGUAGE 'sql'

AS $BODY$
DELETE FROM stores s    -- with EXISTS semi-anti-join
WHERE NOT EXISTS (
   SELECT 1 FROM store_incoming si
   WHERE  si.id = s.id
   );

insert into stores
(id, name, city, location) 
select id, si.name, si.city, (select ST_SetSRID(ST_MakePoint(si.longitude, si.latitude), 4326)) 
from store_incoming si 
left join stores s using (id)
where s.id is null;

update stores s 
set (name, city, location) 
= (select si.name, si.city, (select ST_SetSRID(ST_MakePoint(si.longitude, si.latitude), 4326)))
from store_incoming si 
where s.id = si.id;
$BODY$;

ALTER procedure public.update_stores_from_incoming()
    OWNER TO spurioususer;

CREATE OR REPLACE procedure public.update_products_from_incoming()
    LANGUAGE 'sql'

AS $BODY$
DELETE FROM products p    -- with EXISTS semi-anti-join
WHERE NOT EXISTS (
   SELECT 1 FROM product_incoming pi
   WHERE  pi.id = p.id
   );

insert into products
(id, name, category, volume) 
select id, pi.name, pi.category, pi.volume 
from product_incoming pi 
left join products p using (id)
where p.id is null;

update products p 
set (name, category, volume) 
= (select pi.name, pi.category, pi.volume)
from product_incoming pi 
where p.id = pi.id;
$BODY$;

ALTER procedure public.update_products_from_incoming()
    OWNER TO spurioususer;


CREATE OR REPLACE procedure public.update_inventories_from_incoming()
    LANGUAGE 'sql'

AS $BODY$
DELETE FROM inventories i    -- with EXISTS semi-anti-join
WHERE NOT EXISTS (
   SELECT 1 FROM inventory_incoming ii
   WHERE  ii.product_id = i.product_id
   AND ii.store_id = i.store_id
   );

insert into inventories
(product_id, store_id, quantity) 
select ii.product_id, ii.store_id, ii.quantity 
from inventory_incoming ii 
left join inventories i using (product_id, store_id)
where i.product_id is null and i.store_id is null;

update inventories i
set (quantity) 
= (select ii.quantity)
from inventory_incoming ii 
where i.product_id = ii.product_id and i.store_id = ii.store_id;
$BODY$;

ALTER procedure public.update_inventories_from_incoming()
    OWNER TO spurioususer;




-- FUNCTION: public.update_store_volumes()

-- DROP FUNCTION public.update_store_volumes();

CREATE OR REPLACE procedure public.update_store_volumes()
    LANGUAGE 'sql'

AS $BODY$
update stores 
set (beer_volume, wine_volume, spirits_volume) = 
(
    (select sum(i.quantity * p.volume)
    from inventories i
    inner join products p on p.id = i.product_id
    where i.store_id = stores.id
    and p.category = 'Beer')

    ,(select sum(i.quantity * p.volume)
    from inventories i
    inner join products p on p.id = i.product_id
    where i.store_id = stores.id
    and p.category = 'Wine')

    ,(select sum(i.quantity * p.volume)
    from inventories i
    inner join products p on p.id = i.product_id
    where i.store_id = stores.id
    and p.category = 'Spirits')
)
$BODY$;

ALTER procedure public.update_store_volumes()
    OWNER TO spurioususer;

CREATE OR REPLACE PROCEDURE public.update_subdivision_volumes(
    )
LANGUAGE 'sql'

AS $BODY$
update subdivisions sd
set (beer_volume, wine_volume, spirits_volume) =
(
    (select coalesce (sum(s.beer_volume), 0)
    from stores s
    where ST_Intersects(sd.boundary, ST_Transform(s.location::geometry, 4326))),

    (select coalesce (sum(s.wine_volume), 0)
    from stores s
    where ST_Intersects(sd.boundary, ST_Transform(s.location::geometry, 4326))),

    (select coalesce (sum(s.spirits_volume), 0)
    from stores s
    where ST_Intersects(sd.boundary, ST_Transform(s.location::geometry, 4326)))
)
where sd.population > 0;

update subdivisions sd
set (alcohol_density, beer_density, wine_density, spirits_density) =
(
    (sd.beer_volume + sd.wine_volume + sd.spirits_volume) * 1.0 / sd.population,
    sd.beer_volume * 1.0 / sd.population,
    sd.wine_volume * 1.0 / sd.population,
    sd.spirits_volume * 1.0 / sd.population
)
where sd.population > 0;
$BODY$;


ALTER procedure public.update_subdivision_volumes()
    OWNER TO spurioususer;
    
CREATE OR REPLACE FUNCTION public.get_densities(
    _alcohol_volume text,
    _sort_order text,
    _limit integer)
    RETURNS TABLE(id integer, population integer, name text, centre text, volume numeric, density numeric) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$
BEGIN
   IF upper(_sort_order) IN (N'ASC', N'DESC', N'ASCENDING', N'DESCENDING') THEN
      -- proceed
   ELSE
      RAISE EXCEPTION 'Unexpected value for parameter _sort_order.
                       Allowed: ASC, DESC, ASCENDING, DESCENDING.';
   END IF;

   RETURN QUERY EXECUTE format(
      'SELECT "id", 
       "population", 
       "name",
       St_AsGeoJSON("geographic_centre") AS centre, 
       %I AS "volume", 
       %I AS "density"
       FROM "subdivisions"
       WHERE %I > 0
       ORDER BY "density" %s
       LIMIT %s'
       ,_alcohol_volume, _alcohol_volume, _alcohol_volume, _sort_order, _limit);
END
$BODY$;

ALTER FUNCTION public.get_densities(text, text, integer)
    OWNER TO spurioususer;
