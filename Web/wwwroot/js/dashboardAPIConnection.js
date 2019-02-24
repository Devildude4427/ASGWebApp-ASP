$(document).ready(function () {
    $.ajax({
        url: "/api/v1/users",
        dataType: "json",
        success: function(data) {
            $("#totalUserCount").text(data.totalCount);
        }
    });
});