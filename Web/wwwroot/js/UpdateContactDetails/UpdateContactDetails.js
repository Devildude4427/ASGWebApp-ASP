const form = $("#updateContactDetailsForm");
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
        alert("Submitted!");
    }
});

function updateContactDetails() {
    axios({
        method: "post",
        headers: {
            'Content-Type': 'application/json',
        },
        url: "/api/v1/course/register",
        data: JSON.stringify({
            Address: {
                Line1: $("#addressLine1").val(),
                Line2: $("#addressLine2").val(),
                City: $("#city").val(),
                PostCode: $("#postCode").val()
            },
            CompanyName: $("#companyName").val()
        })
    }).then(function (response) {

        console.log(response);
        if(response.statusText === "OK") {
            window.location = '/Dashboard';
        }
    })
        .catch(function (error) {
            console.log(error);
        });
}