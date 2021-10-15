<template>
  <div class="container q-pa-lg q-my-xl">
    <div class="row justify-center">
      <div class="text-h2 text-primary">LOGIN</div>
    </div>
    <div class="row justify-center q-my-md">
      <div class="col q-pa-lg">
        <div class="row justify-center q-my-md">
          <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
            <Alert></Alert>
            <q-input
              type="text"
              rounded
              filled
              autofocus
              color="primary"
              v-model="logincredentials.username"
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
        <div class="row justify-center">
          <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
            <q-input
              type="password"
              color="primary"
              rounded
              filled
              v-model="logincredentials.password"
              :error="$v.password.$error"
              label="Password"
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
          <div class="col-lg-4 col-md-6 col-sm-8 col-xs-12">
            <router-link
              style="text-decoration: none; color: black"
              to="/forgotpassword"
              >Forgot Password</router-link
            >
          </div>
        </div>
        <div class="row text-center q-my-md">
          <div class="col">
            <q-btn
              @click="login()"
              glossy
              :disable="$v.$invalid"
              color="primary"
              label="Login"
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
import { useRouter } from "vue-router";
import useVuelidate from "@vuelidate/core";
import {
  required,
  helpers,
} from "@vuelidate/validators";
import Alert from "components/Alert.vue";

const store = useStore();
const router = useRouter();
const logincredentials = reactive({
  username: "",
  password: "",
});
function login() {
  store.dispatch("auth/login", logincredentials).then((response) => {
    if (response && response.data.success) {
      router.push("/");
    }
  });
}

const rules = computed(() => {
  return {
    username: {
      required: helpers.withMessage("Username is required", required),
      $autoDirty: true,
    },
    password: {
      required: helpers.withMessage("Password is required", required),
      $autoDirty: true,
    },
  };
});
const $v = useVuelidate(rules, logincredentials);
</script>

<style lang="css" scoped>
.bordered {
  border: 1px solid blue;
  border-radius: 15px;
}
</style>
