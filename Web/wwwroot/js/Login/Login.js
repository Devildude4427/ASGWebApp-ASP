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
    const loginRequest = { Email : $("#email").val(), Password : $("#password").val() };
    $.ajax({
        type: "POST",
        contentType:"application/json",
        url: "/api/v1/account/login",
        data: JSON.stringify(loginRequest),
        success: function(result)
        {
            const response = JSON.parse(JSON.stringify(result));
            if (response['success']) {
                window.location = '/dashboard';
            } else {
                document.getElementById("loginError").style.display = "block";
            }
        },
        failure: function() {
            document.getElementById("loginError").innerHTML = "Can't connect to server";
        }
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