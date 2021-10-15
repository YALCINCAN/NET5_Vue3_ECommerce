<template>
  <router-view />
</template>
<script setup>
import { useStore } from "vuex";
const store = useStore();
checkToken();
function checkToken() {
  let token = localStorage.getItem("token");
  if (token) {
    store.dispatch("basket/getBasket");
    store.dispatch("auth/getAuthenticatedUserWithRoles");
  }
}
</script>
