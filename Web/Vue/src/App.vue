<template>
    <v-app>
        <core-filter v-if="hideLayout"></core-filter>

        <core-toolbar v-if="hideLayout"></core-toolbar>

        <core-drawer v-if="hideLayout"></core-drawer>

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

<script>
    export default {
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
