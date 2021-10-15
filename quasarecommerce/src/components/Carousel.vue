<template>
  <q-carousel
    arrows
    control-color="primary"
    control-text-color="white"
    control-type="push"
    swipeable
    animated
    v-model="slide"
    thumbnails
    infinite
  >
    <q-carousel-slide
      v-for="(slider, index) in sliders"
      :key="slider.id"
      :name="index"
      :img-src="config.url + slider.image"
    />
  </q-carousel>
</template>

<script setup>
import { useStore } from "vuex";
import { computed, ref } from "vue";
import config from "src/config";

const store = useStore();
store.dispatch("slider/getSliders");
const sliders = computed(() => {
  return store.getters["slider/getActiveSliders"];
});

const slide = ref(0);
</script>

<style lang="scss" scoped></style>
