$('[data-toggle="tooltip"]').tooltip();
$(".preloader").fadeOut();
// ============================================================== 
// Login and Recover Password 
// ============================================================== 
$('#to-recover').on("click", function() {
    document.getElementById("loginError").style.display = "none";
    $("#loginForm").slideUp();
    $("#recoverForm").fadeIn();
});
$('#to-login').click(function(){
    $("#recoverForm").hide();
    $("#loginForm").fadeIn();
});

$("#loginForm").submit(function(e) {
    axios({
        method: "post",
        headers: {'Content-Type': 'application/json'},
        url: "/api/v1/account/login",
        data: JSON.stringify({
            Email: $("#emailAddress").val(),
            Password: $("#password").val()
        })
    }).then(function (response) {
        //TODO Check response in case of failure, and alert users if they're just not authenticated 
        if(response.statusText === "OK") {
            window.location = '/dashboard';
        }
        else {
            document.getElementById("loginError").style.display = "block"; 
        }
    }).catch(function (error) {
        console.log(error);
    });
    e.preventDefault();
});

//                $("#recoverForm").submit(function(e) {
//                    const loginRequest = { Email : $("#email").val(), Password : $("#password").val() }
//                    alert("Recover form was submitted");
//                    $.ajax({
//                        type: "POST",
//                        contentType:"application/json",
//                        url: "/api/v1/account/login",
//                        data: JSON.stringify(loginRequest),
//                        success: function(result)
//                        {
//                            const response = JSON.parse(JSON.stringify(result));
//                            if (response['success']) {
//                                window.location = '/home/dashboard';
//                            }
//                        },
//                        failure: function() {
//                            alert("Failed");
//                        }
//                    });
//                    e.preventDefault();
//                });