import { userService } from '@/services';
import { router } from '@/router/router';

const token = localStorage.getItem('jwtToken');
const initialState = token
    ? { status: { loggedIn: true }, token }
    : { status: {}, token: null };

export const authentication = {
    namespaced: true,
    state: initialState,
    actions: {
        login({ dispatch, commit }: any, { email, password }: any) {
            commit('loginRequest', { email });

            userService.login(email, password)
                .then(
                    (response) => {
                        commit('loginSuccess', response.jwtToken);
                        router.push({name: 'Dashboard'});
                        location.reload();
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
        loginRequest(state: { status: { loggingIn: boolean; }; token: any; }, jwtToken: string) {
            state.status = { loggingIn: true };
            state.token = jwtToken;
        },
        loginSuccess(state: { status: { loggedIn: boolean; }; token: string; }, jwtToken: string) {
            state.status = { loggedIn: true };
            state.token = jwtToken;
        },
        loginFailure(state: { status: {}; token: null; }) {
            state.status = {};
            state.token = null;
        },
        logout(state: { status: {}; token: null; }) {
            state.status = {};
            state.token = null;
        },
    },
};
