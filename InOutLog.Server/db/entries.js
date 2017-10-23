module.exports = { 
    'getAllEntries' : getAllEntries,
    'findEntries' : findEntries,
    'upsertEntry' : upsertEntry,
    'removeEntry' : removeEntry
};

function getDb() {
    var db = require('./db.js');
    return db.getDb();
}

function getAllEntries() {
    var db = getDb();
    var result = db.get('entries').value();
    return result;
}

function findEntries(username, entryDate) {
    var db = getDb();
    var filter = {};
    if (username) { filter.Username = username; }
    if (entryDate) { filter.EntryDate = entryDate; }
    var result = db.get('entries').filter(filter).sortBy('EntryDate').value();
    return result;
}

function upsertEntry(entry) {
    var db = getDb();
    var existingEntry = db.get('entries').find({ Username: entry.Username, EntryDate: entry.EntryDate }).value();
    var result = '';
    if (existingEntry) {
        result = db.get('entries').find({ Username: entry.Username, EntryDate: entry.EntryDate })
                   .assign({ SessionId: entry.SessionId, Data: entry.Data, StateId: entry.StateId }).write();
    } else {
        result = db.get('entries').push(entry).write();
    }
    return result;
}

function removeEntry(username, entryDate) {
    var db = getDb();
    var existingEntry = db.get('entries').find({ Username: username, EntryDate: entryDate }).value();
    var result = '';
    if (existingEntry) {
        result = db.get('entries').remove({ Username: username, EntryDate: entryDate }).write();
    } 
    return result;
}
