module.exports = routes;

function routes(app) {
    
    app.get("/api/entries", function(req, res) {
        var entriesDb = require('../db/entries.js');
        var result = entriesDb.getAllEntries();
        res.send(result);
    });

    app.get("/api/entries/find/:username/:entrydate", function(req, res) {
        var entriesDb = require('../db/entries.js');
        var username = req.params.username;
        var entrydate = req.params.entrydate;
        var result = entriesDb.findEntries(username, entrydate);
        res.send(result);
    });

    app.put("/api/entries", function(req, res) {
        var entriesDb = require('../db/entries.js');
        var result = entriesDb.upsertEntry(req.body);
        res.send(result);
    });

    app.delete("/api/entries/:username/:entrydate", function(req, res) {
        var entriesDb = require('../db/entries.js');
        var username = req.params.username;
        var entrydate = req.params.entrydate;
        var result = entriesDb.removeEntry(username, entrydate);
        res.send(result);
    });
}

