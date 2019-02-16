const uri = "/register";
let examples = null;
function getCount(data) {
    const el = $("#counter");
    let name = "to-do";
    if (data) {
        if (data > 1) {
            name = "to-dos";
        }
        el.text(data + " " + name);
    } else {
        el.text("No " + name);
    }
}

$(document).ready(function() {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function(data) {
            const tBody = $("#examples");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function(key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.name))
                    .append($("<td></td>").text(item.id));

                tr.appendTo(tBody);
            });

            examples = data;
        }
    });
}