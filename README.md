TEST Postgresql
===============

### RUN Docker

```
$> docker-compose up -d
```

### RUN PSQL

```
$> docker run -it --rm --link postgresql_db_1 postgres:9.1 psql -h 192.168.99.100 -U admin -d test
```

### Create Table

```
create table "TEST" ("Id" serial primary key, "UUID" uuid not null);
create index "test_uuid_index" on "TEST" ("UUID")
```

### Insert 30000 data

```
CREATE OR REPLACE FUNCTION insertUuidWithLoop()
RETURNS void
AS $$
BEGIN
  FOR Loopid  IN 0..30000 LOOP
     insert into "TEST" ("UUID") values ((SELECT uuid_in(md5(random()::text || now()::text)::cstring)));
  END LOOP;
RETURN;
END;
$$ LANGUAGE plpgsql;

select insertUuidWithLoop();
```

### Set Execute timing On

```
# \timing
```

### Query Array UUID

***Please Replace '1c95c52e-f597-d796-ec74-5fe72e32b196,def800a2-550e-6fb0-a440-bef65dd127da'***

```
# select * from "TEST" where "UUID" = ANY(string_to_array('1c95c52e-f597-d796-ec74-5fe72e32b196,def800a2-550e-6fb0-a440-bef65dd127da', ',')::uuid[])
```

### Show the execution plan of a statement

Ref: http://www.designmagick.com/article/23/Using-Explain/Using-Explain/page/2

```
 EXPLAIN select * from "TEST" where "UUID" = ANY(string_to_array('1c95c52e-f597-d796-ec74-5fe72e32b196,def800a2-550e-6fb0-a440-bef65dd127da', ',')::uuid[]);
```

### log_statement

Ref: http://stackoverflow.com/questions/2430380/is-there-a-postgresql-equivalent-of-sql-server-profiler

You can use the log_statement config setting to get the list of all the queries to a server

http://www.postgresql.org/docs/current/static/runtime-config-logging.html#GUC-LOG-STATEMENT
