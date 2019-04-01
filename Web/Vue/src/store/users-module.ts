import { userService } from '@/services';

export const users = {
    namespaced: true,
    state: {
        all: {},
    },
    actions: {
        getAll({ commit }: any) {
            commit('getAllRequest');

            // @ts-ignore
            userService.getAll()
                .then(
                    (users: any) => commit('getAllSuccess', users),
                    (error: any) => commit('getAllFailure', error),
                );
        },
    },
    mutations: {
        getAllRequest(state: { all: { loading: boolean; }; }) {
            state.all = { loading: true };
        },
        getAllSuccess(state: { all: { items: any; }; }, users: any) {
            state.all = { items: users };
        },
        getAllFailure(state: { all: { error: any; }; }, error: any) {
            state.all = { error };
        },
    },
};
