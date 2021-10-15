<template>
  <div class="container q-pa-lg">
    <div class="row q-col-gutter-lg justify-center">
      <div class="col-lg-3 col-md-4 col-sm-10 col-xs-12">
        <profile-sidebar></profile-sidebar>
      </div>
      <div class="col-lg-9 col-md-8 col-sm-12 col-xs-12">
        <div class="row justify-center q-mb-md">
          <div class="col">
            <div class="text-h5 text-center text-primary text-weight-bold">
              My Addresses
            </div>
          </div>
        </div>
        <div
          class="
            row
            justify-center
            q-col-gutter-xl q-mb-md
            addresses
            text-center text-h6 text-weight-bold
          "
        >
          <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
            <div
              class="row q-my-md bordered q-pa-xs"
              v-for="address in addresses"
              :key="address.id"
            >
              <div class="col-lg-12 col-xs-12">
                <div class="float-left q-my-sm">{{ address.name }}</div>
                <div
                  class="float-right q-my-sm"
                  @click="removeAddress(address.id)"
                >
                  <i class="fas fa-trash q-ml-sm"></i>
                </div>
                <div
                  class="float-right q-my-sm"
                  @click="getAddressById(address.id)"
                >
                  <i class="fas fa-edit"></i>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row justify-center q-mb-md">
          <div class="col">
            <div
              class="text-h5 text-center text-primary text-weight-bold"
              v-if="address.id > 0"
            >
              Update Address
            </div>
            <div
              class="text-h5 text-center text-primary text-weight-bold"
              v-else
            >
              New Address
            </div>
          </div>
        </div>
        <div class="row justify-center q-col-gutter-xl">
          <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
            <Alert></Alert>
            <div class="row q-my-md">
              <div class="col-lg-12 col-xs-12">
                <q-input
                  type="text"
                  filled
                  v-model="address.name"
                  label="Address Name"
                  :error="$v.name.$error"
                >
                  <template #error>
                    <li v-for="error in $v.name.$errors" :key="error.$message">
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md">
              <div class="col-lg-12 col-xs-12">
                <q-input
                  type="text"
                  filled
                  v-model="address.country"
                  label="Country"
                  :error="$v.country.$error"
                >
                  <template #error>
                    <li
                      v-for="error in $v.country.$errors"
                      :key="error.$message"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md">
              <div class="col-lg-12 col-xs-12">
                <q-input
                  type="text"
                  filled
                  v-model="address.city"
                  label="City"
                  :error="$v.city.$error"
                >
                  <template #error>
                    <li v-for="error in $v.city.$errors" :key="error.$message">
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md">
              <div class="col-lg-12 col-xs-12">
                <q-input
                  type="text"
                  filled
                  v-model="address.zipCode"
                  mask="#####"
                  unmasked-value
                  label="ZipCode"
                  :error="$v.zipCode.$error"
                >
                  <template #error>
                    <li
                      v-for="error in $v.zipCode.$errors"
                      :key="error.$message"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md">
              <div class="col-lg-12 col-xs-12">
                <q-input
                  type="text"
                  filled
                  v-model="address.description"
                  label="Description"
                  :error="$v.description.$error"
                >
                  <template #error>
                    <li
                      v-for="error in $v.description.$errors"
                      :key="error.$message"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md">
              <div class="col-lg-12 col-xs-12">
                <q-input
                  type="text"
                  filled
                  mask="(###) ### - ####"
                  unmasked-value
                  v-model="address.phone"
                  label="Phone"
                  :error="$v.phone.$error"
                >
                  <template #error>
                    <li v-for="error in $v.phone.$errors" :key="error.$message">
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </div>
            </div>
            <div class="row q-my-md justify-center">
              <q-btn
                @click="updateAddress"
                fab
                v-if="address.id > 0"
                color="primary"
                label="Update Address"
                :disable="$v.$invalid"
              ></q-btn>
              <q-btn
                @click="addAddress"
                fab
                color="primary"
                label="Add Address"
                :disable="$v.$invalid"
                v-else
              ></q-btn>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import ProfileSidebar from "components/account/ProfileSidebar.vue";
import { computed, ref, reactive } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import Alert from "components/Alert.vue";
import useVuelidate from "@vuelidate/core";
import { required, helpers } from "@vuelidate/validators";

const store = useStore();
const $q = useQuasar();
const user = computed(() => {
  return reactive({ ...store.getters["auth/getAuthenticatedUser"] });
});

const initialAddress = {
  id: 0,
  country: "",
  city: "",
  zipCode: "",
  name: "",
  description: "",
  phone: "",
  userId: "",
};

const address = ref({ ...initialAddress });

store.dispatch("address/getAddresses");

const addresses = computed(() => {
  return store.getters["address/getAddresses"];
});

function getAddressById(id) {
  address.value = { ...store.getters["address/getAddressById"](id) };
}

function addAddress() {
  store.dispatch("address/addAddress", address.value).then((response) => {
    if (response.success) {
      clearAddress();
      $q.notify({
        message: response.message,
        type: "positive",
      });
    }
  });
}

function updateAddress() {
  store.dispatch("address/updateAddress", address.value).then((response) => {
    if (response.success) {
      clearAddress();
      $q.notify({
        message: response.message,
        type: "positive",
      });
    }
  });
}

function removeAddress(id) {
  store.dispatch("address/removeAddress", id).then((response) => {
    if (response.success) {
      clearAddress();
      $q.notify({
        message: response.message,
        type: "positive",
      });
    }
  });
}

const rules = computed(() => {
  return {
    country: {
      required: helpers.withMessage("Country is required", required),
      $autoDirty: true,
    },
    city: {
      required: helpers.withMessage("City is required", required),
      $autoDirty: true,
    },
    phone: {
      required: helpers.withMessage("Phone is required", required),
      $autoDirty: true,
    },
    name: {
      required: helpers.withMessage("Name is required", required),
      $autoDirty: true,
    },
    description: {
      required: helpers.withMessage("Description is required", required),
      $autoDirty: true,
    },
    zipCode: {
      required: helpers.withMessage("Zip Code is required", required),
      $autoDirty: true,
    },
  };
});
const $v = useVuelidate(rules, address);

function clearAddress() {
  address.value = initialAddress;
  store.dispatch("alert/clearAlert");
  $v.value.$reset();
}
</script>

<style lang="scss" scoped>
.bordered {
  border: 1px solid black;
}
</style>
