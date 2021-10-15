<template>
  <div class="container q-pa-lg q-my-xl">
    <div class="row justify-center q-my-md">
      <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
        <Alert></Alert>
        <q-input
          type="email"
          rounded
          autofocus
          filled
          color="primary"
          v-model="ForgotPasswordData.email"
          label="E-mail"
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
        @click="forgotPassword"
        fab
        color="primary"
        label="Forgot Password"
        :disable="$v.$invalid"
      ></q-btn>
    </div>
  </div>
</template>

<script setup>
import Alert from "components/Alert.vue";
import { reactive, computed } from "vue";
import { useStore } from "vuex";
import useVuelidate from "@vuelidate/core";
import { required, email,helpers } from "@vuelidate/validators";

const store = useStore();
const ForgotPasswordData = reactive({
  email: "",
});

function forgotPassword() {
  store.dispatch("user/forgotPassword", ForgotPasswordData);
}

const rules = computed(() => {
  return {
    email: {
      required: helpers.withMessage("Email is required", required),
      email: email,
      $autoDirty: true,
    },
  };
});
const $v = useVuelidate(rules, ForgotPasswordData);
</script>

<style lang="scss" scoped></style>
