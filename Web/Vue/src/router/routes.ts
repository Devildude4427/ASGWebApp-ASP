import Dashboard from '@/views/Dashboard.vue';
import UserProfile from '@/views/UserProfile.vue';
import UpdateContact from '@/views/UpdateContactDetails.vue';
import TableList from '@/views/TableList.vue';
import Typography from '@/views/Typography.vue';
import Icons from '@/views/Icons.vue';
import Maps from '@/views/Maps.vue';
import Notifications from '@/views/Notifications.vue';
import Login from '@/views/Login.vue';

export default [
    {
        path: '/',
        name: 'Dashboard',
        component: Dashboard,
    },
    {
        path: '/user-profile',
        name: 'User Profile',
        component: UserProfile,
    },
    {
        path: '/update-contact',
        name: 'Update Contact Details',
        component: UpdateContact,
    },
    {
        path: '/table-list',
        name: 'Table List',
        component: TableList,
    },
    {
        path: '/typography',
        name: 'Typography',
        component: Typography,
    },
    {
        path: '/icons',
        name: 'Icons',
        component: Icons,
    },
    {
        path: '/maps',
        name: 'Maps',
        component: Maps,
    },
    {
        path: '/notifications',
        name: 'Notifications',
        component: Notifications,
    },
    {
        path: '/login',
        name: 'Login',
        component: Login,
    },
    {
        path: '*',
        redirect: '/',
    },
];
