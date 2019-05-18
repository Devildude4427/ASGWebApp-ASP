import '@/plugins';

export const userService = {
    login,
    logout,
};

async function login(email: string, password: string) {
    // test();
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Email: email, Password: password }),
    };

    return await fetch(`https://localhost:5000/api/v1/auth/authenticate`, requestOptions)
        .then((response) => {
            return response.json();
        })
        .then((responseData) => {
            if (responseData.jwtToken != null) {
                localStorage.setItem('jwtToken', responseData.jwtToken);
            }
            return responseData;
        });
}

// function test(this: any) {
//     this.$http.post('/auth/authenticate', {Email: 'data', Password: 'password'});
// }

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('jwtToken');
}

// function handleResponse(response: { text: () => { then: (arg0: (text: any) => any)
//             => void; }; ok: any; status: number; statusText: any; }) {
//     return response.text().then((text) => {
//         const data = text && JSON.parse(text);
//         if (!response.ok) {
//             if (response.status === 401) {
//                 // auto logout if 401 response returned from api
//                 logout();
//                 location.reload(true);
//             }
//
//             const error = (data && data.message) || response.statusText;
//             return Promise.reject(error);
//         }
//
//         return data;
//     });
// }
