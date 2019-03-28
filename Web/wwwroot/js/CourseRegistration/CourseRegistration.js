const form = $("#courseRegistrationForm");
form.validate({errorPlacement: function(error, element) { element.before(error); }});
form.children("div").steps({
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
        courseRegister();
    }
});

$("#dateOfBirth").datepicker({
    autoclose: true,
    format: "mm/dd/yyyy",
    startDate: "01-01-1920",
    endDate: '-18y',
    maxViewMode: 3
});

function courseRegister() {
    axios({
        method: "post",
        headers: {'Content-Type': 'application/json'},
        url: "/api/v1/candidate/register",
        data: JSON.stringify({
            Address: {
                Line1: $("#addressLine1").val(),
                Line2: $("#addressLine2").val(),
                City: $("#city").val(),
                PostCode: $("#postCode").val()
            },
            PhoneNumber: $("#phoneNumber").val(),
            EnglishSpeakingLevel: $("#englishSpeakingLevel").val(),
            Disability: $("#disability").val(),
            PlaceOfBirth: $("#placeOfBirth").val(),
            DateOfBirth: $("#dateOfBirth").val(),
            CompanyName: $("#companyName").val(),
            FlightExperience: $("#flightExperience").val(),
            PreferredCourseLocation: $("#preferredCourseLocation").val(),
            Drone: {
                Make: $("#droneMake").val(),
                Model: $("#droneModel").val()
            }
        })
    }).then(function (response) {
        if(response.statusText === "OK") {
            window.location = '/dashboard';
        }
    }).catch(function (error) {
        console.log(error);
    });
}