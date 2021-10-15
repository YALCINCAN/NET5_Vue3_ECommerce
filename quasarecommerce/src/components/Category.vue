<template>
  <div class="container">
    <div class="row justify-center text-center">
      <q-item-label class="q-gutter-sm" header>
        <q-btn
          v-for="category in categories"
          :key="category.id"
          :label="category.name"
          color="primary"
          :to="{ path: '/products', query: { 'category.slug': category.slug,page:1,pageSize:3 } }"
        >
        </q-btn>
      </q-item-label>
    </div>
  </div>
</template>

<script setup>
import { useStore } from "vuex"
import { computed } from "vue";
const store = useStore();
store.dispatch("category/getCategories");
const categories = computed(() => {
  return store.getters["category/getCategories"];
});
</script>

<style lang="scss" scoped>
</style>
