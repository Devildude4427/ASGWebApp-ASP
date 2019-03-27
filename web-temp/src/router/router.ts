import Vue from 'vue';
import VueAnalytics from 'vue-analytics';
import Router from 'vue-router';
import Meta from 'vue-meta';
import paths from './routes';


function route(path: string, view: string, name: string) {
  return {
    name: name || view,
    path,
    component: (resolve: ((value: any) => any) | null | undefined) => import(
        `@/views/${view}.vue`).then(resolve),
  };
}

Vue.use(Router);

// Create a new router
const router = new Router({
  mode: 'history',
  // @ts-ignore
  routes: paths.map((path) => route(path.path, path.view, path.name)).concat([
    { path: '*', redirect: '/dashboard' },
  ]),
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
