<template>
  <div
    class="col-lg-2 col-md-2 col-sm-4 col-xs-6"
    v-for="brand in brands"
    :key="brand.id"
  >
    <q-img height="120px" fit class="img-border" :src="config.url + brand.image"></q-img>
  </div>
</template>

<script setup>
import { useStore } from "vuex";
import { computed } from "vue";
import config from "src/config";

const store = useStore();
store.dispatch("brand/getBrands");
const brands = computed(() => {
  return store.getters["brand/getBrands"];
});
</script>

<style lang="scss" scoped>
.img-border {
  border: 1px solid black;
}
</style>
