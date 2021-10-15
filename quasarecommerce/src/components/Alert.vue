<template>
  <div v-if="show">
    <q-banner class="text-white q-mb-md" v-if="show" :class="success">
      <div class="row items-center">
        <div class="col-2">
          <i :class="isError"></i>
        </div>
        <div class="col-10">
          <div class="q-ma-xs" v-if="message != null">
            {{ message }}
          </div>
          <div class="q-ma-xs" v-else>
            <div v-for="(error, index) in errors" :key="index">
              {{ error }}
            </div>
          </div>
        </div>
      </div>
    </q-banner>
  </div>
</template>

<script setup>
import { computed, watch, onMounted } from "vue";
import { useStore } from "vuex";
import { useRoute } from "vue-router";

const store = useStore();
const route = useRoute();
const success = computed(() => {
  return message.value != null ? "bg-green" : "bg-red";
});
const isError = computed(() => {
  return errors.value.length > 0
    ? "fas fa-exclamation-circle fa-2x"
    : "fas fa-check-circle fa-2x";
});
const message = computed(() => {
  return store.getters["alert/getMessage"];
});

const show = computed(() => {
  return store.getters["alert/getShowStatus"];
});

const errors = computed(() => {
  return store.getters["alert/getErrors"];
});
onMounted(() => {
  store.dispatch("alert/clearAlert");
});
watch(
  () => route.path,
  () => {
    store.dispatch("alert/clearAlert");
  }
);
</script>

<style lang="scss" scoped></style>
