<template>
  <div class="container q-pa-lg">
    <div class="row">
      <div class="col">
        <h5>Checkout Form</h5>
      </div>
    </div>
    <div class="row q-col-gutter-md">
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <Alert></Alert>
        <q-form>
          <div class="row q-mb-sm">
            <div class="col">
              <q-select
                v-model="order.addressId"
                :options="addresses"
                emit-value
                map-options
                option-value="id"
                option-label="name"
                label="Select Address"
                :error="$v.addressId.$error"
              >
                <template #error>
                  <li
                    v-for="error in $v.addressId.$errors"
                    :key="error.$message"
                  >
                    <span>{{ error.$message }}</span>
                  </li>
                </template>
              </q-select>
            </div>
          </div>
          <div class="row q-mb-sm">
            <div class="col">
              <q-input
                filled
                type="text"
                v-model="order.cardName"
                label="Card Display Name"
                :error="$v.cardName.$error"
              >
                <template #error>
                  <li
                    v-for="error in $v.cardName.$errors"
                    :key="error.$message"
                  >
                    <span>{{ error.$message }}</span>
                  </li>
                </template>
              </q-input>
            </div>
          </div>
          <div class="row q-mb-sm">
            <div class="col">
              <q-input
                filled
                type="text"
                fill-mask="#"
                unmasked-value
                mask="#### #### #### ####"
                v-model="order.cardNumber"
                label="Card Number"
                :error="$v.cardNumber.$error"
              >
                <template #error>
                  <li
                    v-for="error in $v.cardNumber.$errors"
                    :key="error.$message"
                  >
                    <span>{{ error.$message }}</span>
                  </li>
                </template>
              </q-input>
            </div>
          </div>
          <div class="row q-col-gutter-sm">
            <div class="col-4">
              <q-input
                filled
                type="text"
                mask="##"
                hint="06"
                v-model="order.expirationMonth"
                label="Expire Month"
                :error="$v.expirationMonth.$error"
              >
                <template #error>
                  <li
                    v-for="error in $v.expirationMonth.$errors"
                    :key="error.$message"
                  >
                    <span>{{ error.$message }}</span>
                  </li>
                </template>
              </q-input>
            </div>
            <div class="col-4">
              <q-input
                filled
                type="text"
                mask="####"
                hint="2021"
                v-model="order.expirationYear"
                label="Expire Year"
                :error="$v.expirationYear.$error"
              >
                <template #error>
                  <li
                    v-for="error in $v.expirationYear.$errors"
                    :key="error.$message"
                  >
                    <span>{{ error.$message }}</span>
                  </li>
                </template>
              </q-input>
            </div>
            <div class="col-4">
              <q-input
                filled
                type="text"
                mask="###"
                v-model="order.cvc"
                label="Cvc"
                :error="$v.cvc.$error"
              >
                <template #error>
                  <li v-for="error in $v.cvc.$errors" :key="error.$message">
                    <span>{{ error.$message }}</span>
                  </li>
                </template>
              </q-input>
            </div>
          </div>
        </q-form>
      </div>
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <div
          class="row q-mb-md"
          v-for="basketitem in basket.basketItems"
          :key="basketitem.id"
        >
          <div class="col-10">{{ basketitem.name }}</div>
          <div class="col-2">
            <div class="float-right">x {{ basketitem.quantity }}</div>
          </div>
        </div>
        <div class="row q-mb-md">
          <div class="col">Total Price</div>
          <div class="col">
            <div class="float-right">$ {{ basket.totalPrice }}</div>
          </div>
        </div>
        <div class="row text-center">
          <div class="col">
            <q-btn
              @click="addOrder"
              :disable="$v.$invalid"
              label="Checkout"
              color="orange"
            ></q-btn>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed, reactive, ref } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import { useQuasar } from "quasar";
import { api } from "../boot/axios.js";
import Alert from "src/components/Alert.vue";
import useVuelidate from "@vuelidate/core";
import { required, helpers, numeric } from "@vuelidate/validators";

const store = useStore();

store.dispatch("address/getAddresses");
const addresses = computed(() => {
  return store.getters["address/getAddresses"];
});

const order = reactive({
  cardName: "",
  cardNumber: "",
  expirationMonth: "",
  expirationYear: "",
  cvc: "",
  addressId: "select address",
});

const $q = useQuasar();
const router = useRouter();

function addOrder() {
  if (typeof order.addressId === typeof String()) {
    order.addressId = 0;
    api
      .post("orders", order)
      .then((response) => {
        if (response && response.data.success) {
          store.commit("basket/clearBasket");
          $q.notify({
            message: response.data.message,
            type: "positive",
          });
          router.push({ name: "Orders" });
        }
      })
      .catch((error) => {
        store.dispatch("alert/setErrors", error.response.data.Errors);
      });
  } else {
    api
      .post("orders", order)
      .then((response) => {
        if (response && response.data.success) {
          store.commit("basket/clearBasket");
          $q.notify({
            message: response.data.message,
            type: "positive",
          });
          router.push({ name: "Orders" });
        }
      })
      .catch((error) => {
        store.dispatch("alert/setErrors", error.response.data.Errors);
      });
  }
}

const rules = computed(() => {
  return {
    cardName: {
      required: helpers.withMessage("Card Name is required", required),
      $autoDirty: true,
    },
    cardNumber: {
      required: helpers.withMessage("Card Number is required", required),
      $autoDirty: true,
    },
    cvc: {
      required: helpers.withMessage("Cvc is required", required),
      $autoDirty: true,
    },
    expirationMonth: {
      required: helpers.withMessage("Expiration Month is required", required),
      $autoDirty: true,
    },
    expirationYear: {
      required: helpers.withMessage("Expiration Year is required", required),
      $autoDirty: true,
    },
    addressId: {
      required: helpers.withMessage("Address is required", required),
      numeric,
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, order);

const basket = computed(() => {
  return store.getters["basket/getBasket"];
});
</script>

<style lang="css" scoped></style>
