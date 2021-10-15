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
              My Orders
            </div>
          </div>
        </div>
        <div class="row justify-center">
          <div class="col">
            <q-table
              :pagination="pagination"
              :rows="state.orders"
              :columns="columns"
              :separator="separator"
            >
              <template v-slot:body-cell-detail="props">
                <q-td :props="props">
                  <div class="q-gutter-sm text-center">
                    <router-link :to="'/order/' + props.row.orderNumber">
                      <i class="fas fa-eye text-black">
                        <q-tooltip>See Detail</q-tooltip>
                      </i>
                    </router-link>
                  </div>
                </q-td>
              </template>
               <template v-slot:body-cell-totalPrice="props">
                <q-td :props="props">
                  <div class="q-gutter-sm text-center">
                    <div>$ {{props.row.totalPrice}}</div>
                  </div>
                </q-td>
              </template>
               <template v-slot:body-cell-orderDate="props">
                <q-td :props="props">
                    <div>{{formatDate(props.row.orderDate)}}</div>
                </q-td>
              </template>
            </q-table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import ProfileSidebar from "components/account/ProfileSidebar.vue";
import { reactive, ref } from "vue";
import { api } from "src/boot/axios.js";
import { date } from "quasar";

const pagination = {
  rowsPerPage: 7,
};
const separator = ref("cell");
const columns = [
  {
    name: "orderNumber",
    align: "center",
    label: "Order Number",
    field: "orderNumber",
  },
  {
    name: "orderDate",
    align: "center",
    label: "Date",
    field: "orderDate",
  },
  {
    name: "orderStatus",
    align: "center",
    label: "Status",
    field: "orderStatus",
  },
  {
    name: "totalPrice",
    align: "center",
    label: "Total Price",
    field: "totalPrice",
  },
  {
    name: "detail",
    align: "left",
    label: "Detail",
    field: "detail",
  },
];

getUserOrders();
const state = reactive({
  orders: [
    {
      id: "",
      orderNumber: "",
      orderStatus: "",
      orderDate: "",
      totalPrice: "",
    },
  ],
});

function getUserOrders() {
  api.get("orders").then((response) => {
    if (response && response.data) {
      state.orders = response.data.data;
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
