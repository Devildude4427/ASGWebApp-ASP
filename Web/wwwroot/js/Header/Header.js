function logout() {
    axios.post('/api/v1/account/logout')
        .then(function (response) {
            console.log(response);
            if(response.data.success) {
                window.location = '/authentication/login';
            }
        })
        .catch(function (error) {
            console.log(error);
        });
}