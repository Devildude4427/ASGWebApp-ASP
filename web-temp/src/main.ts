import Vue from 'vue';
import App from '@/App.vue';
import router from '@/router/routes';
// @ts-ignore
import store from '@/store';

import '@/components';
import '@/plugins';
import { sync } from 'vuex-router-sync';

sync(store, router);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
