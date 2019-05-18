import ApiService from '@/plugins/api.axios';

const state = {
    all: {},
};
const actions = {
    fetchUsers() {
        return ApiService.get('user')
            .then(({ data }) => {
                // context.commit(SET_PROFILE, data.profile);
                return data;
            })
            .catch(() => {
                // #todo SET_ERROR cannot work in multiple states
                // context.commit(SET_ERROR, response.data.errors)
            });
    },
};

const mutations = {
    getAllRequest(state: { all: { loading: boolean; }; }) {
        state.all = { loading: true };
    },
    getAllSuccess(state: { all: { items: any; }; }, users: any) {
        state.all = { items: users };
    },
    getAllFailure(state: { all: { error: any; }; }, error: any) {
        state.all = { error };
    },
};

export default {
    state,
    actions,
    mutations,
};
