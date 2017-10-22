module.exports = { 
    'getDb' : getDb
};

const dbFile = 'inoutlog.json'; 
var db = null;

function setupDb() {
    var lowdb = require('lowdb');
    var FileSync = require('lowdb/adapters/FileSync');
    var adapter = new FileSync(dbFile);
    var db = lowdb(adapter);
    db.defaults({ entries: []}).write();
    return db;
}

function getDb() {
    if (db == null) {
        db = setupDb();
    }
    return db;
}

