<template>
  <q-layout view="hhh lpR fff">
    <q-header class="bg-grey-3" height-hint="58">
      <q-toolbar class="container q-pa-md">
        <Header></Header>
        <q-btn
          v-if="$q.screen.lt.sm"
          flat
          dense
          round
          @click="leftDrawerOpen = !leftDrawerOpen"
          aria-label="Menu"
          icon="menu"
          color="primary"
        />
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      bordered
      content-class="bg-grey-2"
      :width="240"
    >
      <q-scroll-area class="fit">
        <q-list padding>
          <q-item to="/" exact clickable v-close-popup>
            <q-item-section>
              <q-item-label>Home</q-item-label>
            </q-item-section>
          </q-item>
          <q-item to="/basket" exact clickable v-close-popup>
            <q-item-section>
              <q-item-label>Basket</q-item-label>
            </q-item-section>
          </q-item>
          <q-item to="/profile" v-if="loggedIn" exact clickable v-close-popup>
            <q-item-section>
              <q-item-label>Profile</q-item-label>
            </q-item-section>
          </q-item>
          <q-item
            clickable
            v-close-popup
            to="/dashboard/sliders"
            v-if="loggedIn && user != null && roles.includes('Admin')"
          >
            <q-item-section>Dashboard</q-item-section>
          </q-item>
          <q-item v-if="!loggedIn" to="/login" exact clickable v-close-popup>
            <q-item-section>
              <q-item-label>Login</q-item-label>
            </q-item-section>
          </q-item>
          <q-item v-if="!loggedIn" to="/register" exact clickable v-close-popup>
            <q-item-section>
              <q-item-label>Register</q-item-label>
            </q-item-section>
          </q-item>
          <q-item
            v-if="loggedIn"
            clickable
            @click.prevent="logout"
            v-close-popup
          >
            <q-item-section>
              <q-item-label>Logout</q-item-label>
            </q-item-section>
          </q-item>
          <q-separator class="q-my-md" />
        </q-list>
      </q-scroll-area>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>

    <Footer></Footer>
  </q-layout>
</template>

<script setup>
import { ref, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import Footer from "components/Footer.vue";
import Header from "components/Header.vue";

const leftDrawerOpen = ref(false);
const store = useStore();
const $q = useQuasar();

const loggedIn = computed(() => {
  return store.getters["auth/getLoggedIn"];
});

const user = computed(() => {
  return store.getters["auth/getAuthenticatedUser"];
});

const roles = computed(() => {
  return store.getters["auth/getAuthenticatedUserRoles"];
});

function logout() {
  store.dispatch("auth/logout").then((response) => {
    if (response.data.success) {
      router.push("/");
    }
  });
}
</script>

<style></style>
