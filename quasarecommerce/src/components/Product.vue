<template>
  <div
    class="col-lg-3 col-md-4 col-sm-6 col-xs-12 q-mx-sm"
    v-for="product in props.products"
    :key="product.id"
  >
    <q-card class="my-card text-center fit">
      <q-img
        class="q-mt-sm"
        style="width: 150px; height: 150px"
        :src="config.url + product.mainImage"
      />
      <q-card-section>
        <div class="row">
          <div class="col text-weight-bold text-bold">
            <router-link
              :to="{ name: 'ProductDetail', params: { slug: product.slug } }"
              class="text-primary"
              style="text-decoration: none"
              >{{ truncateString(product.name, 25) }}</router-link
            >
          </div>
        </div>
      </q-card-section>
      <q-separator />
      <div class="q-mt-sm text-primary text-weight-bold text-h5">
        {{ "$ " + product.price }}
      </div>
      <q-card-section>
        <span>
          <q-rating
            readonly
            v-model="product.ratingAverage"
            class="q-mx-xs"
            color="deep-orange"
            :max="1"
            size="20px"
          />
          <span v-if="product.ratingAverage">{{ product.ratingAverage }}</span>
          <span v-else>0</span>
        </span>
        <q-btn
          class="q-ml-md q-mt-md"
          color="primary"
          @click="addToBasket(product.id)"
          icon="shopping_cart"
          label="Add To Basket"
        />
      </q-card-section>
    </q-card>
  </div>
</template>

<script setup>
import { defineProps, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";
import config from "src/config";
const store = useStore();
const router = useRouter();
const $q = useQuasar();
const props = defineProps({
  products: Array,
});

function truncateString(str, num) {
  if (str.length > num) {
    return str.slice(0, num) + "...";
  } else {
    return str;
  }
}

const loggedIn = computed(() => {
  return store.getters["auth/getLoggedIn"];
});

function addToBasket(id) {
  if (loggedIn.value) {
    const basketitem = {
      ProductId: id,
      Quantity: 1,
    };
    store.dispatch("basket/addToBasket", basketitem).then((response) => {
      if (response && response.success) {
        $q.notify({
          message: response.message,
          type: "positive",
        });
      }
    });
  } else {
    router.push({ path: "/login" });
  }
}
</script>

<style lang="sass" scoped></style>
