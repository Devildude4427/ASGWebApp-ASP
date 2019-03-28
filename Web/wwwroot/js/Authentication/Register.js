$('[data-toggle="tooltip"]').tooltip();
$(".preloader").fadeOut();

$("#registerForm").submit(function(e) {
    axios({
        method: "post",
        headers: {'Content-Type': 'application/json'},
        url: "/api/v1/account/register",
        data: JSON.stringify({
            Name: $("#fullName").val(),
            EmailAddress: $("#emailAddress").val(),
            Password: $("#password").val()
        })
    }).then(function (response) {
        console.log(response);
        if(response.statusText === "OK") {
            window.location = '/authentication/login';
        }
    }).catch(function (error) {
        console.log(error);
    });
    e.preventDefault();
});