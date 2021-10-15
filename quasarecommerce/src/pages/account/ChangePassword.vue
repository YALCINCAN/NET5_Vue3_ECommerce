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
              Change Password
            </div>
          </div>
        </div>
        <div class="row justify-center q-col-gutter-xl">
          <div class="col-lg-8 col-md-8 col-sm-6 col-xs-12">
            <Alert></Alert>
            <div class="row q-my-md">
              <div class="col-lg-12 col-xs-12">
                <q-input
                  type="password"
                  filled
                  v-model="ChangePasswordData.currentpassword"
                  label="Current Password"
                  :error="$v.currentpassword.$error"
                >
                  <template #error>
                    <li
                      v-for="error in $v.currentpassword.$errors"
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
                  type="password"
                  filled
                  v-model="ChangePasswordData.newpassword"
                  label="New Password"
                  :error="$v.newpassword.$error"
                >
                  <template #error>
                    <li
                      v-for="error in $v.newpassword.$errors"
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
                  type="password"
                  filled
                  label="Confirm Password"
                  v-model="ChangePasswordData.confirmpassword"
                  :error="$v.confirmpassword.$error"
                >
                  <template #error>
                    <li
                      v-for="error in $v.confirmpassword.$errors"
                      :key="error.$message"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md justify-center">
              <q-btn
                @click="changePassword"
                fab
                color="primary"
                label="Change Password"
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
import { required, helpers, sameAs, minLength } from "@vuelidate/validators";
import { oneUpperCase } from "src/utilities/validators.js";

const store = useStore();
const ChangePasswordData = reactive({
  currentpassword: "",
  newpassword: "",
  confirmpassword: "",
});

function changePassword() {
  store.dispatch("user/changePassword", ChangePasswordData);
}

const rules = computed(() => {
  return {
    currentpassword: {
      required: helpers.withMessage("Current Password is required", required),
      minLength: minLength(9),
      $autoDirty: true,
    },
    newpassword: {
      required: helpers.withMessage("New Password is required", required),
      minLength: minLength(9),
      oneUpperCase: helpers.withMessage(
        "Password need one must upper case",
        oneUpperCase
      ),
      $autoDirty: true,
    },
    confirmpassword: {
      required: helpers.withMessage("Confirm password is required", required),
      sameAs: helpers.withMessage(
        "Confirm password dont match with new password",
        sameAs(ChangePasswordData.newpassword)
      ),
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, ChangePasswordData);
</script>

<style lang="scss" scoped></style>
