$(document).ready(function () {

    let options = {};
    options.url = "/api/v1/users";
    options.type = "GET";
    options.dataType = "json";
    options.success = function (data) {

        data.forEach(function (element) {
            $("#result").append("<h3>" + element + "</h3>");
        });
    };
    options.error = function () {
        $("#msg").html("Error while calling the Web API!");
    };
    $.ajax(options);
});