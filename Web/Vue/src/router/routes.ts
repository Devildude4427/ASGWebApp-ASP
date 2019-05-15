export default [
    {
        path: '/',
        name: 'Dashboard',
        component: () => import('@/views/Dashboard.vue'),
    },
    {
        path: '/user-profile',
        name: 'User Profile',
        component: () => import('@/views/UserProfile.vue'),
    },
    {
        path: '/update-contact',
        name: 'Update Contact Details',
        component: () => import('@/views/UpdateContactDetails.vue'),
    },
    },



    {
        path: '/table-list',
        name: 'Table List',
        component: () => import('@/views/TableList.vue'),
    },
    {
        path: '/typography',
        name: 'Typography',
        component: () => import('@/views/Typography.vue'),
    },
    {
        path: '/icons',
        name: 'Icons',
        component: () => import('@/views/Icons.vue'),
    },
    {
        path: '/maps',
        name: 'Maps',
        component: () => import('@/views/Maps.vue'),
    },
    {
        path: '/notifications',
        name: 'Notifications',
        component: () => import('@/views/Notifications.vue'),
    },


    {
        path: '/login',
        name: 'Login',
        component: () => import('@/views/Login.vue'),
    },
    {
        path: '*',
        redirect: '/',
    },
];
