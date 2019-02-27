const form = $("#courseRegistrationForm");
form.validate({
    errorPlacement: function(error, element) { element.before(error); },
    rules: {
        confirm: {
            equalTo: "#password"
        }
    }
});
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
        alert("Submitted!");
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
    axios.post('/api/v1/course/register')
        .then(function (response) {
            console.log(response);
            if(response.data.success) {
                window.location = '/Dashboard';
            }
        })
        .catch(function (error) {
            console.log(error);
        });
}