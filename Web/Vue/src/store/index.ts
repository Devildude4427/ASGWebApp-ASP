import Vue from 'vue';
import Vuex from 'vuex';

import { alert } from '@/store/alert-module';
import { authentication } from '@/store/authentication-module';
import { users } from '@/store/users-module';

// TODO decide whether or not I need this
// @ts-ignore
import { modules } from '@/store/modules';

Vue.use(Vuex);

export const store = new Vuex.Store({
    modules: {
        alert,
        authentication,
        users,
        modules,
    },
});
