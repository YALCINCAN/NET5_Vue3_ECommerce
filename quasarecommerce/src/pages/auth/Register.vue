<template>
  <div class="container q-pa-lg q-my-xl">
    <div class="row justify-center">
      <div class="text-h2 text-primary">REGISTER</div>
    </div>
    <div class="row justify-center q-my-md">
      <div class="col-lg-8 col-md-10 col-sm-12 col-xs-12 q-pa-lg">
        <div class="row justify-center">
          <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
            <Alert></Alert>
          </div>
        </div>
        <div class="row justify-center q-py-md q-col-gutter-sm">
          <div class="col-lg-3 col-md-3 col-sm-4 col-xs-6">
            <q-input
              type="text"
              rounded
              autofocus
              filled
              color="primary"
              v-model="registercredentials.firstName"
              label="First Name"
              :error="$v.firstName.$error"
            >
              <template #error>
                <li v-for="error in $v.firstName.$errors" :key="error.$message">
                  <span>{{ error.$message }}</span>
                </li>
              </template>
            </q-input>
          </div>
          <div class="col-lg-3 col-md-3 col-sm-4 col-xs-6">
            <q-input
              type="text"
              rounded
              filled
              color="primary"
              v-model="registercredentials.lastName"
              label="Last Name"
              :error="$v.lastName.$error"
            >
              <template #error>
                <li v-for="error in $v.lastName.$errors" :key="error.$message">
                  <span>{{ error.$message }}</span>
                </li>
              </template>
            </q-input>
          </div>
        </div>
        <div class="row justify-center q-my-md">
          <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
            <q-input
              type="text"
              rounded
              filled
              color="primary"
              v-model="registercredentials.username"
              label="Username"
              :error="$v.username.$error"
            >
              <template #error>
                <li v-for="error in $v.username.$errors" :key="error.$message">
                  <span>{{ error.$message }}</span>
                </li>
              </template>
            </q-input>
          </div>
        </div>
        <div class="row justify-center q-my-md">
          <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
            <q-input
              type="email"
              rounded
              filled
              color="primary"
              v-model="registercredentials.email"
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
        <div class="row justify-center q-my-md">
          <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
            <q-input
              type="password"
              color="primary"
              rounded
              filled
              v-model="registercredentials.password"
              label="Password"
              :error="$v.password.$error"
            >
              <template #error>
                <li v-for="error in $v.password.$errors" :key="error.$message">
                  <span>{{ error.$message }}</span>
                </li>
              </template>
            </q-input>
          </div>
        </div>
        <div class="row justify-center q-my-md">
          <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
            <q-input
              type="password"
              color="primary"
              rounded
              filled
              v-model="registercredentials.confirmpassword"
              label="Password Confirm"
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
        <div class="row text-center q-my-md">
          <div class="col">
            <q-btn
              @click="register()"
              glossy
              color="primary"
              label="Register"
              :disable="$v.$invalid"
            ></q-btn>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive, computed } from "vue";
import { useStore } from "vuex";
import Alert from "components/Alert.vue";
import useVuelidate from "@vuelidate/core";
import {
  required,
  helpers,
  email,
  sameAs,
  minLength,
} from "@vuelidate/validators";
import { oneUpperCase } from "src/utilities/validators.js";


const store = useStore();
const registercredentials = reactive({
  username: "",
  password: "",
  confirmpassword: "",
  email: "",
  firstName: "",
  lastName: "",
});

function register() {
  store.dispatch("auth/register", registercredentials);
}

const rules = computed(() => {
  return {
    username: {
      required: helpers.withMessage("Username is required", required),
      $autoDirty: true,
    },
    firstName: {
      required: helpers.withMessage("First Name is required", required),
      $autoDirty: true,
    },
    lastName: {
      required: helpers.withMessage("Last Name is required", required),
      $autoDirty: true,
    },
    password: {
      required: helpers.withMessage("Password is required", required),
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
        "Confirm password dont match with password",
        sameAs(registercredentials.password)
      ),
      $autoDirty: true,
    },
    email: {
      required: helpers.withMessage("Email is required", required),
      email: email,
      $autoDirty: true,
    },
  };
});
const $v = useVuelidate(rules, registercredentials);
</script>

<style lang="css" scoped>
.bordered {
  border: 1px solid blue;
  border-radius: 15px;
}
</style>
