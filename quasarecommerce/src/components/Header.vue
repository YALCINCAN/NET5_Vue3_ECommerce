<template>
  <q-btn flat no-caps no-wrap>
    <q-toolbar-title shrink class="text-weight-bold">
      <router-link style="text-decoration: none" class="text-primary" to="/"
        >TECHNO</router-link
      >
    </q-toolbar-title>
  </q-btn>

  <q-space />

  <Search></Search>

  <q-space />

  <q-btn
    outline
    label="LOGIN"
    to="/login"
    color="primary"
    class="gt-xs q-mx-md"
    v-if="!loggedIn"
  ></q-btn>

  <q-btn
    outline
    label="REGISTER"
    to="register"
    color="green"
    class="gt-xs"
    v-if="!loggedIn"
  ></q-btn>
  <q-btn
    round
    class="q-mx-md"
    dense
    flat
    v-if="loggedIn && user != null"
    color="orange"
    icon="shopping_cart"
    to="/basket"
  >
    <q-badge
      color="primary"
      text-color="white"
      floating
      v-if="basketItemCount > 0"
      >{{ basketItemCount }}</q-badge
    >
  </q-btn>
  <q-btn
    color="primary"
    class="q-ml-md gt-xs"
    v-if="loggedIn && user != null"
    :label="userinfo"
  >
    <q-menu fit>
      <q-list style="min-width: 100px">
        <q-item
          clickable
          v-close-popup
          to="/dashboard/sliders"
          v-if="loggedIn && user != null && roles.includes('Admin')"
        >
          <q-item-section>Dashboard</q-item-section>
        </q-item>
        <q-item to="/profile" clickable v-close-popup>
          <q-item-section>Profile</q-item-section>
        </q-item>
        <q-separator />
        <q-item clickable v-close-popup>
          <q-item-section @click="logout">Logout</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
  </q-btn>
</template>

<script setup>
import { useStore } from "vuex";
import { computed } from "vue";
import { useRouter } from "vue-router";
import Search from "src/components/Search.vue";

const store = useStore();
const router = useRouter();
const basketItemCount = computed(() => {
  return store.getters["basket/getBasketItemCount"];
});
const user = computed(() => {
  return store.getters["auth/getAuthenticatedUser"];
});
const roles = computed(() => {
  return store.getters["auth/getAuthenticatedUserRoles"];
});

const userinfo = computed(() => {
  return store.getters["auth/getFirstNameandLastName"];
});

const loggedIn = computed(() => {
  return store.getters["auth/getLoggedIn"];
});

function logout() {
  store.dispatch("auth/logout");
  router.push("/");
}
</script>

<style lang="scss" scoped></style>
