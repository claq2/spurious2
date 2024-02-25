# Centos
rpm -ivh http://yum.postgresql.org/9.4/redhat/rhel-7-x86_64/pgdg-centos94-9.4-1.noarch.rpm
yum install -y postgresql94 postgresql94-server postgresql94-libs postgresql94-contrib postgresql94-devel
rpm -ivh http://dl.fedoraproject.org/pub/epel/epel-release-latest-7.noarch.rpm
sudo yum install -y postgis2_94
/usr/pgsql-9.4/bin/postgresql94-setup initdb
systemctl start postgresql-9.4.service
systemctl enable postgresql-9.4.service
firewall-cmd --permanent --zone=public --add-service=postgresql
systemctl restart firewalld.service
# edit /var/lib/pgsql/9.4/data/postgresql.conf listen_addresses = '*' port = 5432
# edit /var/lib/pgsql/9.4/data/pg_hba.conf host all all         0.0.0.0/0 md5

# Linux Mint 17.2/Ubuntu 14.04
echo deb http://apt.postgresql.org/pub/repos/apt/ trusty-pgdg main>/etc/apt/sources.list.d/pgdg.list
wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | \
  sudo apt-key add -
sudo apt-get update
sudo apt-get install postgresql-9.4 postgresql-9.4-postgis-2.1 postgresql-contrib
# edit /etc/postgresql/9.4/main/postgresql.conf listen_addresses = '*' port = 5432
# edit /etc/postgresql/9.4/main/pg_hba.conf host all all         0.0.0.0/0 md5

set OGR_ENABLE_PARTIAL_REPROJECTION=TRUE
ogr2ogr -t_srs EPSG:4326 -f CSV subdiv2.csv lcsd000a16g_e.gml -lco GEOMETRY=AS_WKT
