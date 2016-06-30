TEST Postgresql
===============

### RUN PSQL

docker run -it --rm --link postgresql_db_1 postgres:9.1 psql -h 192.168.99.100 -U admin -d mrbs

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
