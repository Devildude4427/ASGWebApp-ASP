import Vue from 'vue';
import VueAnalytics from 'vue-analytics';
import Router from 'vue-router';
import Meta from 'vue-meta';
import routes from './routes';

Vue.use(Router);

// Create a new router
const router = new Router({
  mode: 'history',
  routes,
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
