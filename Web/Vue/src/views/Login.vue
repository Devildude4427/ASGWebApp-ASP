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
                  <v-text-field label="Password" class="blue-input" v-model="password" required></v-text-field>
                </v-flex>
                <v-flex xs12 text-xs-right>
                  <v-btn class="mx-0 font-weight-light" color="blue" type="submit">Login</v-btn>
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

