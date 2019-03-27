import Vue from 'vue';
import VueAnalytics from 'vue-analytics';
import Router from 'vue-router';
import Meta from 'vue-meta';

import Dashboard from '@/views/Dashboard.vue';
import UserProfile from '@/views/UserProfile.vue';
import TableList from '@/views/TableList.vue';
import Typography from '@/views/Typography.vue';
import Icons from '@/views/Icons.vue';
import Maps from '@/views/Maps.vue';
import Notifications from '@/views/Notifications.vue';
import Upgrade from '@/views/Upgrade.vue';


Vue.use(Router);

const router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/dashboard',
      // Relative to /src/views
      component: Dashboard,
    },
    {
      path: '/user-profile',
      name: 'User Profile',
      component: UserProfile,
    },
    {
      path: '/table-list',
      name: 'Table List',
      component: TableList,
    },
    {
      path: '/typography',
      component: Typography,
    },
    {
      path: '/icons',
      component: Icons,
    },
    {
      path: '/maps',
      component: Maps,
    },
    {
      path: '/notifications',
      component: Notifications,
    },
    {
      path: '/upgrade',
      name: 'Upgrade to PRO',
      component: Upgrade,
    },
  ],
});

Vue.use(Meta);

// Bootstrap Analytics
// Set in .env
// https://github.com/MatteoGabriele/vue-analytics
if (process.env.GOOGLE_ANALYTICS) {
  Vue.use(VueAnalytics, {
    id: process.env.GOOGLE_ANALYTICS,
    router,
    autoTracking: {
      page: process.env.NODE_ENV !== 'development',
    },
  });
}

export default router;
