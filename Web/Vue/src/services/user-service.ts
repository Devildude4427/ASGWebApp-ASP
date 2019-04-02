// import config from 'config';
import { authHeader } from '@/services/auth-header';

export const userService = {
    login,
    logout,
    // getAll,
};

async function login(email: string, password: string) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Email: email, Password: password }),
    };

    const response = await fetch(`https://localhost:5000/api/v1/auth/login`, requestOptions);
    const user = await handleResponse(response);
    // login successful if there's a jwt token in the response
    // @ts-ignore
    if (user.token) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(user));
    }
    return user;
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader(),
    };

    // @ts-ignore
    return fetch(`https://localhost:5000/users`, requestOptions).then(handleResponse);
}

function handleResponse(response: { text: () => { then: (arg0: (text: any) => any)
            => void; }; ok: any; status: number; statusText: any; }) {
    return response.text().then((text) => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api
                logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}
