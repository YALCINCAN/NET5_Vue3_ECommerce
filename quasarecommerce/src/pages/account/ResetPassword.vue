<template>
  <div class="container q-pa-lg q-my-xl">
    <div class="row justify-center q-my-md">
      <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
        <Alert></Alert>
        <q-input
          type="password"
          color="primary"
          rounded
          filled
          v-model="ResetPasswordData.newpassword"
          label="Password"
          :error="$v.newpassword.$error"
        >
          <template #error>
            <li v-for="error in $v.newpassword.$errors" :key="error.$message">
              <span>{{ error.$message }}</span>
            </li>
          </template>
        </q-input>
      </div>
    </div>
    <div class="row justify-center q-my-md">
      <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
        <q-input
          type="password"
          color="primary"
          rounded
          filled
          v-model="ResetPasswordData.confirmpassword"
          label="Confirm Password"
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
        @click="resetPassword"
        fab
        color="primary"
        label="Change Password"
        :disable="$v.$invalid"
      ></q-btn>
    </div>
  </div>
</template>

<script setup>
import Alert from "components/Alert.vue";
import { reactive, computed } from "vue";
import { useRoute } from "vue-router";
import { useStore } from "vuex";
import useVuelidate from "@vuelidate/core";
import { required, helpers, sameAs, minLength } from "@vuelidate/validators";
import { oneUpperCase } from "src/utilities/validators.js";

const store = useStore();
const route = useRoute();
const ResetPasswordData = reactive({
  newpassword: "",
  confirmpassword: "",
  Token: route.params.token,
  Email: route.params.email,
});

function resetPassword() {
  store.dispatch("user/resetPassword", ResetPasswordData);
}

const rules = computed(() => {
  return {
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
        sameAs(ResetPasswordData.newpassword)
      ),
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, ResetPasswordData);
</script>

<style lang="scss" scoped></style>
