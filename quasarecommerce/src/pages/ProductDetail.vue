<template>
  <Categories></Categories>
  <div class="container q-pa-md q-my-sm" v-if="notfound == false">
    <div class="row">
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <div class="row q-mb-md">
          <q-carousel
            swipeable
            animated
            v-model="slide"
            infinite
            arrows
            thumbnails
            control-color="primary"
            control-type="push"
            style="width: 500px; height: 500px"
          >
            <q-carousel-slide
              v-for="(image, index) in images"
              :key="index"
              :name="index"
              :img-src="config.url + image"
            />
          </q-carousel>
        </div>
      </div>
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <div class="row">
          <div class="text-primary text-h5 text-weight-bold">
            {{ product.name }}
          </div>
        </div>
        <div class="row">
          <div class="text-h6 text-black text-weight-bold">
            {{ product.brand.name }}
          </div>
        </div>
        <div class="row">
          <div class="q-my-sm">{{ product.description }}</div>
        </div>
        <div class="row q-py-md">
          <div class="text-primary text-weight-bold text-h4">
            {{ "$ " + product.price }}
          </div>
        </div>
        <div class="row q-py-md">
          <div class="col-6">
            <span class="input-number-decrement" @click="decreaseQuantity"
              >–</span
            >
            <input
              class="input-number"
              type="text"
              min="1"
              :value="quantity"
              @input="setQuantity($event)"
            />
            <span class="input-number-increment" @click="increaseQuantity"
              >+</span
            >
          </div>
          <div class="col-6">
            <q-btn
              color="primary"
              @click="addToBasket"
              icon="add_shopping_cart"
              label="Add To Basket"
            />
          </div>
        </div>
      </div>
    </div>
    <div class="row q-mt-md">
      <div class="col">
        <div class="q-pa-md">
          <q-table
            hide-header
            hide-bottom
            :rows="product.productOptionValues"
            :columns="columns"
            :separator="separator"
            :pagination="pagination"
          />
        </div>
      </div>
    </div>
    <div class="row q-mt-md">
      <div class="col">
        <q-list>
          <q-item-label header class="text-weight-bold">Reviews</q-item-label>
          <div v-for="review in product.reviews" :key="review.id">
            <q-item>
              <q-item-section>
                <q-item-label>
                  <span>
                    {{ review.user.firstName }} {{ review.user.lastName }} -
                    {{ formatDate(review.reviewDate) }}
                  </span>
                </q-item-label>
                <q-item-label class="q-mb-sm">
                  <q-rating
                    readonly
                    v-model="review.ratingValue"
                    color="blue"
                    :max="5"
                    size="20px"
                  />
                </q-item-label>

                <q-item-label>{{ review.description }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-separator spaced="10px" />
          </div>
        </q-list>
      </div>
    </div>
    <div v-if="loggedIn">
      <div class="row justify-center q-my-md">
        <q-rating
          v-model="state.review.ratingValue"
          color="blue"
          :max="5"
          size="20px"
        />
      </div>
      <div class="row justify-center">
        <span v-if="$v.ratingValue.$error" class="text-red"
          >Rating Value is required</span
        >
      </div>
      <div class="row justify-center q-my-md">
        <q-input
          label="Your Review"
          v-model="state.review.description"
          class="col-10"
          filled
          type="textarea"
          :error="$v.description.$error"
        >
          <template #error>
            <li v-for="error in $v.description.$errors" :key="error.$message">
              <span>{{ error.$message }}</span>
            </li>
          </template>
        </q-input>
      </div>
      <div class="row justify-center q-my-md">
        <q-btn
          rounded
          @click="addReview"
          color="primary"
          :disable="$v.$invalid"
          label="Send Review"
        ></q-btn>
      </div>
    </div>
  </div>
  <div class="container" v-else>
    <q-banner class="text-white bg-red q-pa-md q-ma-md">
      <div class="text-center">Product Not Found</div>
    </q-banner>
  </div>
</template>

<script setup>
import { ref, watch, reactive, computed } from "vue";
import { useRoute } from "vue-router";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { api } from "boot/axios";
import Categories from "src/components/Category.vue";
import config from "src/config";
import { date } from "quasar";
import useVuelidate from "@vuelidate/core";
import { required, helpers, minValue } from "@vuelidate/validators";

const columns = [
  {
    name: "Option",
    align: "center",
    label: "Option",
    field: "optionName",
  },
  {
    name: "Value",
    align: "center",
    label: "Value",
    field: "optionValue",
  },
];
const pagination = {
  rowsPerPage: 50,
};
const route = useRoute();
const router = useRouter();
const store = useStore();
const $q = useQuasar();
getProductDetail(route.params.slug);
const slide = ref(0);
const separator = ref("cell");
const quantity = ref(1);
const images = ref([]);
const product = ref({
  id: 0,
  name: "",
  description: "",
  brand: {},
  price: 0,
  mainImage: "",
  reviews: [],
  productImages: [],
  productOptionValues: [],
  slug: "",
});
const state = reactive({
  review: {
    productId: product.value.id,
    description: "",
    ratingValue: 0,
  },
});
const notfound = ref(false);
const loggedIn = computed(() => {
  return store.getters["auth/getLoggedIn"];
});
function formatDate(reviewdate) {
  return date.formatDate(reviewdate, "DD/MM/YYYY HH:mm");
}
function setQuantity(event) {
  quantity.value = event.target.value;
}
function increaseQuantity() {
  quantity.value++;
}
function decreaseQuantity() {
  if (quantity.value > 1) {
    quantity.value--;
  } else quantity.value = 1;
}
watch(
  () => route.params.slug,
  () => {
    if (route.params.slug) {
      getProductDetail(route.params.slug);
    }
  }
);

function getProductDetail(slug) {
  api
    .get("products/" + slug)
    .then((response) => {
      state.review.productId = response.data.data.id;
      product.value = response.data.data;
      let array = new Array();
      array.push(response.data.data.mainImage);
      for (let i = 0; i < response.data.data.productImages.length; i++) {
        array.push(response.data.data.productImages[i].image);
      }
      images.value = array;
    })
    .catch((error) => {
      notfound.value = true;
    });
}

function addToBasket() {
  if (loggedIn.value) {
    let basketitem = {
      ProductId: product.value.id,
      Quantity: quantity.value,
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

function addReview() {
  store
    .dispatch("review/addReview", state.review)
    .then((response) => {
      if (response && response.success) {
        $q.notify({
          message: response.message,
          type: "positive",
          position: "bottom-right",
        });
        resetReview();
        getProductDetail(route.params.slug);
      }
    })
    .catch((error) => {
      error.forEach(function (error) {
        $q.notify({
          message: error,
          multiLine: true,
          type: "negative",
          position: "bottom-right",
        });
      });
    });
}

const rules = computed(() => {
  return {
    description: {
      required: helpers.withMessage("Description is required", required),
      $autoDirty: true,
    },
    ratingValue: {
      minValue: minValue(1),
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, state.review);

function resetReview(){
  state.review.description="",
  state.review.ratingValue=0
  $v.value.$reset();
}
</script>

<style lang="sass" scoped>
.input-number
  width: 80px
  padding: 0 12px
  vertical-align: top
  text-align: center
  outline: none

.input-number,
.input-number-decrement,
.input-number-increment
  border: 1px solid #ccc
  height: 40px
  user-select: none

.input-number-decrement,
.input-number-increment
  display: inline-block
  width: 30px
  line-height: 38px
  background: #f1f1f1
  color: #444
  text-align: center
  font-weight: bold
  cursor: pointer

  &:active
    background: #ddd

.input-number-decrement
  border-right: none
  border-radius: 4px 0 0 4px

.input-number-increment
  border-left: none
  border-radius: 0 4px 4px 0
</style>
