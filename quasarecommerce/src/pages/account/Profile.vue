<template>
  <div class="container q-pa-lg">
    <div class="row q-col-gutter-lg justify-center">
      <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
        <profile-sidebar></profile-sidebar>
      </div>
      <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
        <div class="row justify-center q-mb-md">
          <div class="col">
            <div class="text-h5 text-center text-primary text-weight-bold">
              Profile
            </div>
          </div>
        </div>
        <div class="row justify-center q-col-gutter-xl">
          <div class="col-lg-8 col-md-8 col-sm-6 col-xs-12">
            <Alert></Alert>
            <div class="row q-col-gutter-md">
              <div class="col-lg-6 col-md-6 col-sm-12 col-xs-6">
                <q-input
                  autofocus
                  filled
                  v-model="user.firstName"
                  label="First Name"
                  :error="$v.firstName.$error"
                >
                  <template #error>
                    <li
                      v-for="error in $v.firstName.$errors"
                      :key="error.$message"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12 col-xs-6">
                <q-input
                  filled
                  v-model="user.lastName"
                  label="Last Name"
                  :error="$v.lastName.$error"
                >
                  <template #error>
                    <li
                      v-for="error in $v.lastName.$errors"
                      :key="error.$message"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md">
              <div class="col-lg-12 col-xs-12">
                <q-input
                  filled
                  v-model="user.email"
                  label="Email"
                  type="email"
                  :error="$v.email.$error"
                >
                  <template #error>
                    <li v-for="error in $v.email.$errors" :key="error.$message">
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md justify-center">
              <q-btn
                @click="updateProfile"
                fab
                color="primary"
                label="Update"
                :disable="$v.$invalid"
              ></q-btn>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import ProfileSidebar from "components/account/ProfileSidebar.vue";
import { reactive, computed } from "vue";
import { useStore } from "vuex";
import Alert from "components/Alert.vue";
import useVuelidate from "@vuelidate/core";
import { required, helpers, email } from "@vuelidate/validators";

const store = useStore();

const user = computed(() => {
  return reactive({ ...store.getters["auth/getAuthenticatedUser"] });
});


function updateProfile() {
  store.dispatch("user/updateProfile", user.value);
  clearAlert();
}

function clearAlert() {
  setTimeout(() => {
    store.dispatch("alert/clearAlert");
  }, 1500);
}

const rules = computed(() => {
  return {
    firstName: {
      required: helpers.withMessage("First Name is required", required),
      $autoDirty: true,
    },
    lastName: {
      required: helpers.withMessage("Last Name is required", required),
      $autoDirty: true,
    },
    email: {
      required: helpers.withMessage("Email is required", required),
      email: email,
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, user);
</script>

<style lang="css" scoped></style>
