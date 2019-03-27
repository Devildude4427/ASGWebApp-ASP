<template>
    <v-app>
        <core-filter></core-filter>

        <core-toolbar></core-toolbar>

        <core-drawer></core-drawer>

        <core-view></core-view>
    </v-app>
</template>

<style lang="scss">
    @import 'sass/material-dashboard';

    /* Remove in 1.2 */
    .v-datatable thead th.column.sortable i {
        vertical-align: unset;
    }
</style>
