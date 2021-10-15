<template>
  <div class="container q-pa-lg">
    <div class="row text-primary text-center text-h6 text-weight-bold">
      <div class="col">Your Basket</div>
    </div>
    <q-separator spaced="20px" size="3px"></q-separator>
    <div
      v-if="basket.basketItems.length>0"
      class="row justify-end q-my-md text-red text-weight-bold cursor-pointer"
      @click="clearBasket"
    >
      Clear Basket
    </div>
    <div
      class="row q-my-md text-center justify-center"
      v-if="basket.basketItems.length>0"
    >
      <div class="col-lg-8 col-md-8">
        <div
          class="row q-my-lg justify-center"
          v-for="basketitem in basket.basketItems"
          :key="basketitem.id"
        >
          <div class="col-4 col-md-4 col-sm-12 col-xs-12">
            <q-img
              :src="config.url + basketitem.mainImage"
              width="150px"
              height="150px"
            ></q-img>
          </div>
          <div class="col-6 col-md-8 col-sm-12 col-xs-12">
            <div class="row justify-center">
              <div class="text-primary text-h6 text-weight-bold">
                {{ basketitem.name }}
                <div class="float-right">
                  <i
                    style="color: red"
                    class="fas fa-trash cursor-pointer q-mx-sm"
                    @click="removeFromBasket(basketitem.productId)"
                  >
                    <q-tooltip>Remove</q-tooltip>
                  </i>
                </div>
              </div>
            </div>
            <div class="row justify-center">
              <div class="text-primary text-weight-bold text-h5 q-mt-md">
                {{ "$ " + basketitem.price }}
                <div class="q-my-md">
                  <span
                    class="input-number-decrement"
                    @click="decreaseQuantity(basketitem.productId)"
                    >–</span
                  >
                  <input
                    class="input-number"
                    type="text"
                    min="1"
                    :value="basketitem.quantity"
                    @input="updateBasket(basketitem.productId,$event)"
                  />
                  <span
                    class="input-number-increment"
                    @click="increaseQuantity(basketitem.productId)"
                    >+</span
                  >
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
        <div class="row justify-end q-my-md" :class="ifltmd">
          <q-card bordered>
            <q-card-section>
              <div class="text-primary text-h6 text-weight-bold">
                Total Price
              </div>
              <div class="text-primary text-h6 text-weight-bold">
                {{ "$ " + basket.totalPrice }}
              </div>
            </q-card-section>
            <q-card-section>
              <q-btn to="/checkout" color="orange" label="Checkout"></q-btn>
            </q-card-section>
          </q-card>
        </div>
      </div>
    </div>
    <div class="text-center" v-else>
      <p>Your basket is empty</p>
    </div>
    <q-separator spaced="20px" size="3px"></q-separator>
  </div>
</template>

<script setup>
import { computed } from "vue";
import { useQuasar } from "quasar";
import { useStore } from "vuex";
import config from "src/config";
const store = useStore();
const $q = useQuasar();
const ifltmd = computed(() => {
  return $q.screen.lt.md ? "justify-center" : "justify-end";
});
const basket = computed(() => {
  return store.getters["basket/getBasket"];
});

function updateBasket(ProductId,event){
   let basketitem = basket.value.basketItems.find(
    (x) => x.productId === ProductId
  ); 
   var copybasketitem = { ...basketitem };
  copybasketitem.quantity=event.target.value;
  store.dispatch("basket/updateBasket", copybasketitem);
}
function increaseQuantity(ProductId) {
  let basketitem = basket.value.basketItems.find(
    (x) => x.productId === ProductId
  );
  var copybasketitem = { ...basketitem };
  copybasketitem.quantity++;
  store.dispatch("basket/updateBasket", copybasketitem);
}

function decreaseQuantity(ProductId) {
  let basketitem = basket.value.basketItems.find(
    (x) => x.productId === ProductId
  );
  var copybasketitem = { ...basketitem };
  if (basketitem.quantity > 1) {
    copybasketitem.quantity--;
    store.dispatch("basket/updateBasket", copybasketitem);
  }
}

function removeFromBasket(productid){
  store.dispatch("basket/removeFromBasket",productid)
}

function clearBasket(){
  store.dispatch("basket/clearBasket");
}

</script>

<style lang="css" scoped>
.input-number {
  width: 80px;
  padding: 0 12px;
  vertical-align: top;
  text-align: center;
  outline: none;
}

.input-number,
.input-number-decrement,
.input-number-increment {
  border: 1px solid #ccc;
  height: 40px;
  user-select: none;
}

.input-number-decrement,
.input-number-increment {
  display: inline-block;
  width: 30px;
  line-height: 38px;
  background: #f1f1f1;
  color: #444;
  text-align: center;
  font-weight: bold;
  cursor: pointer;
}

.input-number-decrement {
  border-right: none;
  border-radius: 4px 0 0 4px;
}

.input-number-increment {
  border-left: none;
  border-radius: 0 4px 4px 0;
}
</style>
