<template>
    <v-container fill-height fluid grid-list-xl>
        <v-layout justify-center wrap>
            <v-flex xs12 md8>
                <material-card color="blue" title="Login">
                    <v-form @submit.prevent="handleSubmit">
                        <v-container py-0>
                            <v-layout wrap>
                                <v-flex xs12 md12>
                                    <v-text-field label="Email Address" class="blue-input" v-model="email" required autofocus></v-text-field>
                                </v-flex>
                                <v-flex xs12 md12>
                                    <v-text-field label="Password" class="blue-input" v-model="password"
                                                  :type="showPassword ? 'text' : 'password'"
                                                  :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                                                  @click:append="showPassword = !showPassword"
                                                  required></v-text-field>
                                </v-flex>
                                <v-flex xs12 text-xs-right>
                                    <v-btn class="mx-0 font-weight-light" color="blue" type="submit">Login</v-btn>
                                    <img v-show="loggingIn" src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" alt="loading gif"/>
                                </v-flex>
                            </v-layout>
                        </v-container>
                    </v-form>
                </material-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script lang="js">
    export default {
        data() {
            return {
                email: '',
                password: '',
                submitted: false,
                showPassword: false,
            };
        },
        computed: {
            loggingIn() {
                return this.$store.state.authentication.status.loggingIn;
            },
        },
        created() {
            // reset login status
            this.$store.dispatch('authentication/logout');
        },
        methods: {
            handleSubmit(e) {
                this.submitted = true;
                const { email, password } = this;
                const { dispatch } = this.$store;
                if (email && password) {
                    dispatch('authentication/login', { email, password });
                }
            },
        },
    };
</script>

