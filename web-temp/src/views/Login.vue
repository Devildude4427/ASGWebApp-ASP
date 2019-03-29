<template>
  <v-container fill-height fluid grid-list-xl>
    <v-layout justify-center wrap>
      <v-flex xs12 md8>
        <material-card color="blue" title="Login">
          <v-form>
            <v-container py-0>
              <v-layout wrap>
                <v-flex xs12 md12>
                  <v-text-field label="Email Address" class="black-input"></v-text-field>
                </v-flex>
                <v-flex xs12 md12>
                  <v-text-field label="Password" class="blue-input"></v-text-field>
                </v-flex>
                <v-flex xs12 text-xs-right>
                  <v-btn class="mx-0 font-weight-light" color="blue">Login</v-btn>
                </v-flex>
              </v-layout>
            </v-container>
          </v-form>
        </material-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
