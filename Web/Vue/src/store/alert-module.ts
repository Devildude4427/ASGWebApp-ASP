export const alert = {
    namespaced: true,
    state: {
        type: null,
        message: null,
    },
    actions: {
        success({ commit }: any, message: any) {
            commit('success', message);
        },
        error({ commit }: any, message: any) {
            commit('error', message);
        },
        clear({ commit }: any, message: any) {
            commit('success', message);
        },
    },
    mutations: {
        success(state: { type: string; message: any; }, message: any) {
            state.type = 'alert-success';
            state.message = message;
        },
        error(state: { type: string; message: any; }, message: any) {
            state.type = 'alert-danger';
            state.message = message;
        },
        clear(state: { type: null; message: null; }) {
            state.type = null;
            state.message = null;
        },
    },
};
