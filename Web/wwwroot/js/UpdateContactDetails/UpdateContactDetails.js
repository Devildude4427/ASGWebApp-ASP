const form = $("#updateContactDetailsForm");
form.validate({
    errorPlacement: function(error, element) { element.before(error); }
});
form.children("div").steps({
    enableCancelButton: true,
    headerTag: "h3",
    bodyTag: "section",
    transitionEffect: "slideLeft",
    onStepChanging: function(event, currentIndex, newIndex) {
        if (newIndex > currentIndex) {
            form.validate().settings.ignore = ":disabled,:hidden";
            return form.valid();
        } else {
            return true;
        }
    },
    onFinishing: function(event, currentIndex) {
        form.validate().settings.ignore = ":disabled";
        return form.valid();
    },
    onFinished: function() {
        updateContactDetails();
    }
});

$("a[href$='previous']").hide();
$("#updateContactDetailsForm > .wizard .actions > ul").css("width", "100%");
$("#updateContactDetailsForm > .wizard .actions > ul > li:eq(2)").css("float", "right");
$("#updateContactDetailsForm > .wizard .actions > ul > li:eq(3)").css("float", "left");

function updateContactDetails() {
    axios({
        method: "post",
        headers: {
            'Content-Type': 'application/json',
        },
        url: "/api/v1/candidate/updateDetails",
        data: JSON.stringify({
            Address: {
                Line1: $("#addressLine1").val(),
                Line2: $("#addressLine2").val(),
                City: $("#city").val(),
                PostCode: $("#postCode").val()
            },
            emailAddress: $("#emailAddress").val(),
            PhoneNumber: $("#phoneNumber").val()
        })
    }).then(function (response) {
        console.log(response);
        if(response.statusText === "OK") {
            window.location = '/Dashboard';
        }
    }).catch(function (error) {
            console.log(error);
        });
}