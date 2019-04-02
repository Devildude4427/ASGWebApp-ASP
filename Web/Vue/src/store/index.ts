import Vue from 'vue';
import Vuex from 'vuex';

import { alert } from '@/store/alert-module';
import { authentication } from '@/store/authentication-module';
import { users } from '@/store/users-module';
import { app } from '@/store/app-module';

Vue.use(Vuex);

export const store = new Vuex.Store({
    modules: {
        alert,
        authentication,
        users,
        app,
    },
});
