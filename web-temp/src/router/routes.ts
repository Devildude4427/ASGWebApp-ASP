import Vue from 'vue';
import Router from 'vue-router';

import Home from '../views/Home.vue';
import Login from '../views/Login.vue';
import DashboardLayout from '@/components/layouts/_DashboardLayout.vue';

Vue.use(Router);

const router = new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    },
    {
      path: '/client-dashboard',
      name: 'client-dashboard',
      component: DashboardLayout,
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ '../views/About.vue'),
    },
    {
      path: '/404',
      name: '404',
      component: require('../views/_404').default,
      // Allows props to be passed to the 404 page through route
      // params, such as `resource` to define what wasn't found.
      props: true,
    },
    // Redirect any unmatched routes to the 404 page. This may
    // require some server configuration to work in production:
    // https://router.vuejs.org/en/essentials/history-mode.html#example-server-configurations
    {
      path: '*',
      redirect: '404',
    },
  ],
});

export default router;
