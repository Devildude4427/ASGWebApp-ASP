<template>
  <v-container fill-height fluid grid-list-xl>
    <v-layout justify-center wrap>
      <v-flex xs12 md8>
        <material-card color="blue" title="Update Contact Details" text="So that we can remain in contact">
          <v-form>
            <v-container py-0>
              <v-layout wrap>
                <v-flex xs12 md6>
                  <v-text-field label="Address Line 1" class="info-input"/>
                </v-flex>
                <v-flex xs12 md6> 
                  <v-text-field label="Address Line 2" class="info-input"/>
                </v-flex>
                <v-flex xs12 md6>
                  <v-text-field label="City" class="info-input"/>
                </v-flex>
                <v-flex xs12 md6>
                  <v-text-field label="Postal Code" class="info-input"/>
                </v-flex>
                <v-flex xs12 md12>
                  <v-spacer></v-spacer>
                </v-flex>
                <v-flex xs12 md6>
                  <v-text-field label="Phone Number" class="info-input"/>
                </v-flex>
                <v-flex xs12 md6>
                  <v-text-field label="Email Address" class="info-input"/>
                </v-flex>
                <v-flex xs12 text-xs-right>
                  <v-btn class="mx-0 font-weight-light" color="blue">Update Profile</v-btn>
                </v-flex>
              </v-layout>
            </v-container>
          </v-form>
        </material-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
  //
};
</script>
