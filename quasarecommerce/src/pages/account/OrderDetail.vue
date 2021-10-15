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
              Order Detail
            </div>
          </div>
        </div>
        <div class="bordered q-pa-md">
          <div class="row q-mb-md">
            <div class="col-6">
              <div>Order Number : {{ state.order.orderNumber }}</div>
            </div>
            <div class="col-6">
              <div class="float-right">
                Order Date : {{ formatDate(state.order.orderDate) }}
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <q-table
                hide-bottom
                :rows="state.order.orderItems"
                :columns="columns"
                :separator="separator"
              >
                <template v-slot:body-cell-Image="props">
                  <q-td :props="props">
                    <q-img
                      height="72px"
                      width="72px"
                      :src="config.url + props.row.mainImage"
                    ></q-img>
                  </q-td>
                </template>
                <template v-slot:body-cell-Quantity="props">
                  <q-td :props="props">
                    <div>x {{ props.row.quantity }}</div>
                  </q-td>
                </template>
                <template v-slot:body-cell-Price="props">
                  <q-td :props="props">
                    <div>$ {{ props.row.price }}</div>
                  </q-td>
                </template>
              </q-table>
            </div>
          </div>
          <div class="row q-mt-md">
            <div class="col-6">
              <div class="row q-mb-md">
                <div>Address</div>
              </div>
              <div class="row">
                <div>
                  {{ state.order.address.description }} /
                  {{ state.order.address.city }} /
                  {{ state.order.address.country }}
                </div>
              </div>
              <div class="row">
                <div>
                  {{ state.order.user.firstName }}
                  {{ state.order.user.lastName }} -
                  {{ state.order.address.phone }}
                </div>
              </div>
            </div>
            <div class="col-6">
              <div class="row">
                <div class="col-12">
                  <div class="float-right">
                    Order Status : {{ state.order.orderStatus }}
                  </div>
                </div>
              </div>
              <div class="row q-mt-md">
                <div class="col-12">
                  <div class="float-right">
                    Total Price : $ {{ state.order.totalPrice }}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import ProfileSidebar from "components/account/ProfileSidebar.vue";
import { reactive, ref } from "vue";
import { useRoute } from "vue-router";
import { api } from "src/boot/axios.js";
import { date } from "quasar";
import config from "src/config.js";
const separator = ref("cell");
const columns = [
  {
    name: "Image",
    align: "center",
    label: "Image",
    field: "mainImage",
  },
  {
    name: "Product Name",
    align: "center",
    label: "Product Name",
    field: "name",
  },
  {
    name: "Quantity",
    align: "center",
    label: "Quantity",
    field: "quantity",
  },
  {
    name: "Price",
    align: "left",
    label: "Price",
    field: "price",
  },
];
const route = useRoute();
const state = reactive({
  order: {
    id: "",
    orderDate: "",
    orderNumber: "",
    orderStatus: "",
    totalPrice: "",
    orderItems: [
      {
        id: "",
        mainImage: "",
        name: "",
        price: "",
        productId: "",
        quantity: "",
      },
    ],
    address: {
      id: "",
      country: "",
      city: "",
      phone: "",
      description: "",
    },
    user: {
      id: "",
      firstName: "",
      lastName: "",
    },
  },
});
getOrderDetail();
function getOrderDetail() {
  api.get("orders/" + route.params.ordernumber).then((response) => {
    if (response) {
      state.order = response.data.data;
    }
  });
}

function formatDate(orderdate) {
  return date.formatDate(orderdate, "DD/MM/YYYY HH:mm");
}
</script>

<style lang="scss" scoped>
.bordered {
  border: 1px solid black;
}
</style>
