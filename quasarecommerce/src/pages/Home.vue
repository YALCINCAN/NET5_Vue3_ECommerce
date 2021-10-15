<template>
  <Category></Category>
  <q-page class="container q-pa-lg">
    <div class="row justify-center">
      <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <Carousel></Carousel>
      </div>
    </div>
    <div class="row justify-center q-my-lg">
      <div class="text-h4 text-primary">Brands</div>
    </div>
    <div class="row justify-center q-col-gutter-md">
      <Brand></Brand>
    </div>
    <div class="row justify-center q-my-lg">
      <div class="text-h4 text-primary">Last Products</div>
    </div>
    <div class="row q-col-gutter-md justify-center">
      <Product :products="products"></Product>
    </div>
  </q-page>
</template>

<script setup>
import { ref, computed } from "vue";
import { api } from "boot/axios";
import { useQuasar } from "quasar";
import Category from "src/components/Category.vue";
import Carousel from "src/components/Carousel.vue";
import Brand from "src/components/Brand.vue";
import Product from "src/components/Product.vue";
const $q = useQuasar();
const products = ref([]);
const ifltmd = computed(() => {
  return $q.screen.lt.md ? "q-ml-md" : "";
});
getLast10Products();
function getLast10Products() {
  api.get("products").then((response) => {
    products.value = response.data.data;
  });
}
</script>

<style lang="scss" scoped></style>
