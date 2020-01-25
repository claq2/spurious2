--set a default new query template with the command QE Boost: Set New Query Template


# Example queries
  
  select s.id, s.name, city, 
--p.name, 
p.category, sum(p.volume) as total_volume from stores as s
inner join inventories as i on i.store_id = s.id
inner join products as p on p.id = i.product_id
--where s.id = 243
group by p.category, s.id, s.name, city
order by total_volume;

create or replace function category_volume(store_id int, category text) returns int as
$$
declare
    vol int;
begin
    select sum(i.quantity * p.volume) into vol
    from stores s
    inner join inventories i on (i.store_id = s.id)
    inner join products p on (i.product_id = p.id)
    where s.id = $1 and p.category = $2;
    return vol;

end;
$$ language plpgsql;

select s.id, s.name, category_volume(s.id, 'Beer') as beer, category_volume(s.id, 'Wine') as wine, category_volume(s.id, 'Spirit') as spirit
--(select sum(i.quantity * p.volume) where p.category = 'Beer') as beer, p.category
from stores as s
--inner join inventories i on (i.store_id = s.id)
--inner join products p on (i.product_id = p.id)
--where s.id = 243
--and p.category in ('Beer', 'Wine', 'Spirit')
--group by s.id, p.category
;

select s.id, s.name, s.city,
(select sum(i.quantity * p.volume)
    from inventories i
    inner join products p on p.id = i.product_id
    where i.store_id = s.id
    and p.category = 'Beer') as beer_volume,
(select sum(i.quantity * p.volume)
    from inventories i
    inner join products p on p.id = i.product_id
    where i.store_id = s.id and p.category = 'Wine') as wine_volume,
(select sum(i.quantity * p.volume)
    from inventories i
    inner join products p on p.id = i.product_id
    where i.store_id = s.id and p.category = 'Spirit') as spirits_volume
from stores s
order by s.id

update stores set (beer_volume, wine_volume, spirits_volume) = 
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
    and p.category = 'Spirit')
);

select sb.id, s.id, s.name, s.city
from subdivisions sb
inner join stores s on ST_Intersects(sb.boundary, s.location)
order by sb.id

-- subdivs and volumes
select sb.id, sum(s.beer_volume), sum(s.wine_volume), sum(s.spirits_volume)
from subdivisions sb
inner join stores s on ST_Intersects(sb.boundary, s.location)
group by sb.id

-- centre of subdiv
select st_asgeojson(st_centroid(boundary::geometry)) as centre from subdivisions
where province = 'Ontario';
