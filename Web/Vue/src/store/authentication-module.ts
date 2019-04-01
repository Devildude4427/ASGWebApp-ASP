import { userService } from '@/services';
import { router } from '@/router/router';

const user = JSON.parse(localStorage.getItem('user') || '{}');
const initialState = user
    ? { status: { loggedIn: true }, user }
    : { status: {}, user: null };

export const authentication = {
    namespaced: true,
    state: initialState,
    actions: {
        login({ dispatch, commit }: any, { email, password }: any) {
            commit('loginRequest', { email });

            userService.login(email, password)
                .then(
                    (user) => {
                        // console.log('Login success');
                        commit('loginSuccess', user);
                        router.push({name: 'Dashboard'});
                    },
                    (error) => {
                        commit('loginFailure', error);
                        dispatch('alert/error', error, { root: true });
                    },
                );
        },
        logout({ commit }: any) {
            userService.logout();
            commit('logout');
        },
    },
    mutations: {
        loginRequest(state: { status: { loggingIn: boolean; }; user: any; }, user: any) {
            state.status = { loggingIn: true };
            state.user = user;
        },
        loginSuccess(state: { status: { loggedIn: boolean; }; user: any; }, user: any) {
            state.status = { loggedIn: true };
            state.user = user;
        },
        loginFailure(state: { status: {}; user: null; }) {
            state.status = {};
            state.user = null;
        },
        logout(state: { status: {}; user: null; }) {
            state.status = {};
            state.user = null;
        },
    },
};
