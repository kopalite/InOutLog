//express app setup

var express = require("express");
var bodyParser = require("body-parser");
var app = express();
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

//getting routes
var routes = require("./routes/routes.js")(app);

//starting the server
var server = app.listen(3000, function () {
    console.log("inoutlog.server: listening on port %s...", server.address().port);
});