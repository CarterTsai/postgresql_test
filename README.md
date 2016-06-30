TEST Postgresql
===============

### RUN PSQL

docker run -it --rm --link postgresql_db_1 postgres:9.1 psql -h 192.168.99.100 -U admin -d mrbs

### Set Execute timing On

```
# \timing
```

### Query Array UUID

```
# select * from "TEST" where "UUID" = ANY(string_to_array('1c95c52e-f597-d796-ec74-5fe72e32b196,def800a2-550e-6fb0-a440-bef65dd127da', ',')::uuid[])
```
