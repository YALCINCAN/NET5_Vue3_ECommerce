<template>
    <q-input dense style="width: 50%" standout="bg-primary" v-model="search" placeholder="Search">
    <template v-slot:append>
      <q-icon @click="Search" name="search" />
    </template>
  </q-input>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const search = ref("");

function Search(){
  router.push({path:"/products",query:{'search':search.value}})
}
</script>

<style lang="scss" scoped>

</style>