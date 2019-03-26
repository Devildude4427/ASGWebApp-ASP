import Vue from 'vue';
import App from './App.vue';
import router from './router/routes';

import Dashboard from '@/layouts/Dashboard.vue';

Vue.component('dashboard-layout', Dashboard);

Vue.config.productionTip = false;

new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app');
