// import { userService } from '@/services';
// import { router } from '@/router/router';
//
// const token = localStorage.getItem('jwtToken');
// const initialState = token
//     ? { status: { loggedIn: true }, token }
//     : { status: {}, token: null };
//
// export const authentication = {
//     namespaced: true,
//     state: initialState,
//     actions: {
//         login({ dispatch, commit }: any, { email, password }: any) {
//             commit('loginRequest', { email });
//
//             userService.login(email, password)
//                 .then(
//                     (response) => {
//                         commit('loginSuccess', response.jwtToken);
//                         router.push({name: 'Dashboard'});
//                         location.reload();
//                     },
//                     (error) => {
//                         commit('loginFailure', error);
//                         dispatch('alert/error', error, { root: true });
//                     },
//                 );
//         },
//         logout({ commit }: any) {
//             userService.logout();
//             commit('logout');
//         },
//     },
//     mutations: {
//         loginRequest(state: { status: { loggingIn: boolean; }; token: any; }, jwtToken: string) {
//             state.status = { loggingIn: true };
//             state.token = jwtToken;
//         },
//         loginSuccess(state: { status: { loggedIn: boolean; }; token: string; }, jwtToken: string) {
//             state.status = { loggedIn: true };
//             state.token = jwtToken;
//         },
//         loginFailure(state: { status: {}; token: null; }) {
//             state.status = {};
//             state.token = null;
//         },
//         logout(state: { status: {}; token: null; }) {
//             state.status = {};
//             state.token = null;
//         },
//     },
// };


import ApiService from '@/plugins/api.axios';
import JwtService from '@/services/jwt-service';

const state = {
    errors: null,
    user: {},
    status: {},
    isAuthenticated: !!JwtService.getToken(),
};

const getters = {
    currentUser(state: { user: any; }) {
        return state.user;
    },
    isAuthenticated(state: { isAuthenticated: any; }) {
        return state.isAuthenticated;
    },
};

const actions = {
    login(context: any, credentials: any) {
        return new Promise((resolve) => {
            context.commit('authRequest');
            ApiService.post('auth/authenticate', credentials)
                .then(({ data }) => {
                    context.commit('setAuth', data.jwtToken);
                    resolve(data);
                })
                .catch(({ response }) => {
                    if (response !== undefined) {
                        context.commit('setError', response.data.errors);
                    } else {
                        context.commit('setError', 'Unknown Error');
                    }
                });
        });
    },
    logout(context: any) {
        context.commit('purgeAuth');
    },
    // register(context: any, credentials: any) {
    //     return new Promise((resolve, reject) => {
    //         ApiService.post('users', { user: credentials })
    //             .then(({ data }) => {
    //                 context.commit(SET_AUTH, data.user);
    //                 resolve(data);
    //             })
    //             .catch(({ response }) => {
    //                 context.commit(SET_ERROR, response.data.errors);
    //                 reject(response);
    //             });
    //     });
    // },
    checkAuth(context: any) {
        const token = JwtService.getToken();
        if (!!token) {
            // ApiService.setHeader();
            context.commit('setAuth', token);
        } else {
            context.commit('purgeAuth');
        }
    },
    // updateUser(context, payload) {
    //     const { email, username, password, image, bio } = payload;
    //     const user = {
    //         email,
    //         username,
    //         bio,
    //         image
    //     };
    //     if (password) {
    //         user.password = password;
    //     }
    //
    //     return ApiService.put("user", user).then(({ data }) => {
    //         context.commit(SET_AUTH, data.user);
    //         return data;
    //     });
    // }
};

const mutations = {
    authRequest(state: { status: any; }) {
        state.status = { loggingIn: true };
    },
    setError(state: { errors: any; }, error: string) {
        state.errors = error;
    },
    setAuth(state: { status: {}; isAuthenticated: boolean;
                     user: { name: string; email: string; role: string; token: string; };
                     errors: {}; }, jwtToken: string) {
        const userData = JSON.parse(atob(jwtToken.split('.')[1]));
        state.user = {
            name: userData.unique_name,
            email: userData.email,
            role: userData.role,
            token: jwtToken,
        };
        state.isAuthenticated = true;
        state.status = { loggedIn: true};
        state.errors = {};
        JwtService.saveToken(jwtToken);
    },
    purgeAuth(state: { status: {}; isAuthenticated: boolean; user: {}; errors: {}; }) {
        state.isAuthenticated = false;
        state.status = {};
        state.user = {};
        state.errors = {};
        JwtService.destroyToken();
    },
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
};
