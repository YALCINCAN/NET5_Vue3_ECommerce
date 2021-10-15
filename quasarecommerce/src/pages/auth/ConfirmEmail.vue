<template>
  <div class="container q-pa-lg">
    <div class="row justify-center">
      <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <Alert></Alert>
      </div>
    </div>
  </div>
</template>

<script setup>
import Alert from "components/Alert.vue";
import { useStore } from "vuex";
import { useRoute } from "vue-router";
import { reactive } from "vue";

const store = useStore();
const route = useRoute();
const confirmemaildata = reactive({
  userId: route.params.userId,
  token: route.params.token,
});
confirmEmail();
function confirmEmail() {
  store.dispatch("auth/confirmemail", confirmemaildata);
}
</script>

<style lang="scss" scoped></style>
