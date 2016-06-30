var pg = require("knex")({client: 'pg'});

// 建立Table
function createTable(db) {
    return db.schema.createTable('TEST', function (t) {
        t.increments('Id').index().primary().defaultTo(0);
        t.uuid('UUID').notNullable();
    }).toString();
}

console.log(createTable(pg));


function insertData(db) {
    return db('TEST').insert({
        UUID: 'SELECT uuid_in(md5(random()::text || now()::text)::cstring)'
    }).toString();
}


console.log(insertData(pg));
