<template>
    <v-app>
        <core-filter v-if="hideLayout"></core-filter>

        <core-toolbar v-if="hideLayout"></core-toolbar>

        <core-drawer v-if="hideLayout"></core-drawer>

        <core-view></core-view>

        <div v-if="alert.message" :class="`alert ${alert.type}`">{{alert.message}}</div>
    </v-app>
</template>

<style lang="scss">
    @import 'sass/material-dashboard';

    /* Remove in 1.2 */
    .v-datatable thead th.column.sortable i {
        vertical-align: unset;
    }
</style>

<script>
    export default {
        name: 'app',
        computed: {
            alert() {
                return this.$store.state.alert;
            },
        },
        watch: {
            $route(to, from) {
                // clear alert on location change
                this.$store.dispatch('alert/clear');
            },
        },
        data() {
            return {
                hideLayout: false,
            };
        },
        beforeMount() {
            if (this.$route.name !== 'Login') {
                this.hideLayout = true;
            }
        },
    };
</script>
