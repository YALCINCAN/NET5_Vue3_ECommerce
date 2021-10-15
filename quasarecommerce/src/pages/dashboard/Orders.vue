<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Orders"
        :rows="orders"
        :hide-header="mode === 'grid'"
        :columns="columns"
        :grid="mode == 'grid'"
        :filter="filter"
        :pagination="pagination"
      >
        <template v-slot:top-right="props">
          <q-input
            outlined
            dense
            debounce="300"
            v-model="filter"
            placeholder="Search"
          >
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
          <q-btn
            flat
            round
            dense
            :icon="props.inFullscreen ? 'fullscreen_exit' : 'fullscreen'"
            @click="props.toggleFullscreen"
            v-if="mode === 'list'"
          >
            <q-tooltip :disable="$q.platform.is.mobile" v-close-popup>
              {{ props.inFullscreen ? "Exit Fullscreen" : "Toggle Fullscreen" }}
            </q-tooltip>
          </q-btn>

          <q-btn
            flat
            round
            dense
            :icon="mode === 'grid' ? 'list' : 'grid_on'"
            @click="
              mode = mode === 'grid' ? 'list' : 'grid';
              separator = mode === 'grid' ? 'none' : 'horizontal';
            "
            v-if="!props.inFullscreen"
          >
            <q-tooltip :disable="$q.platform.is.mobile" v-close-popup>{{
              mode === "grid" ? "List" : "Grid"
            }}</q-tooltip>
          </q-btn>

          <q-btn
            color="primary"
            icon-right="archive"
            label="Export to csv"
            no-caps
            @click="exportTable"
          />
        </template>
        <template v-slot:body-cell-actions="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-btn
                dense
                @click="getOrderDetail(props.row.orderNumber)"
                color="primary"
                icon="edit"
              />
            </div>
          </q-td>
        </template>
      </q-table>
    </q-card>
    <q-dialog v-model="showmodal" persistent>
      <q-card style="width: 600px; max-width: 60vw">
        <q-card-section>
          <Alert></Alert>
          <div class="text-h6">
            <span>Update Order</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetOrder()"
            ></q-btn>
          </div>
        </q-card-section>
        <q-separator inset></q-separator>
        <q-card-section class="q-pt-none">
          <q-form class="q-gutter-md">
            <q-list>
              <q-item>
                <q-item-section>
                  <q-radio
                    v-for="orderstatus in state.orderstatuses"
                    :key="orderstatus.id"
                    class="q-ma-sm"
                    dense
                    :val="orderstatus.id"
                    v-model="state.order.orderStatusId"
                    :label="orderstatus.status"
                  />
                </q-item-section>
              </q-item>
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn label="Update" @click="updateOrder" color="primary" />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { ref, reactive } from "vue";
import { useExportCsv } from "src/composables/useExportCsv";
import Alert from "components/Alert.vue";
import { api } from "src/boot/axios.js";
import { date } from "quasar";
import { useQuasar } from "quasar";

const columns = [
  {
    name: "OrderNumber",
    align: "left",
    label: "Order Number",
    field: "orderNumber",
  },
  {
    name: "OrderStatus",
    align: "left",
    label: "Status",
    field: "orderStatus",
  },
  {
    name: "OrderDate",
    align: "left",
    label: "Date",
    field: (row) => formatDate(row.orderDate),
  },
  {
    name: "TotalPrice",
    align: "left",
    label: "Total Price",
    field: (row) => "$ " + row.totalPrice,
  },
  {
    name: "actions",
    align: "left",
    label: "Actions",
    field: "actions",
  },
];
const filter = ref("");
const mode = ref("list");
const pagination = {
  rowsPerPage: 7,
};
const $q = useQuasar();
const showmodal = ref(false);
const orders = ref([]);
const state = reactive({
  order: {
    id: "",
    orderStatusId: "",
  },
  orderstatuses: [
    {
      id: "",
      status: "",
    },
  ],
});

getAdminOrders();
function getAdminOrders() {
  api.get("orders/getadminorders").then((response) => {
    if (response && response.data) {
      orders.value = response.data.data;
    }
  });
}

const { exportTable } = useExportCsv(orders, columns, "orders");

function getOrderStatuses() {
  api.get("orderstatuses").then((response) => {
    if (response && response.data) {
      state.orderstatuses = response.data.data;
    }
  });
}
function getOrderDetail(ordernumber) {
  showmodal.value = true;
  getOrderStatuses();
  api.get("orders/getadminorderdetail/" + ordernumber).then((response) => {
    if (response && response.data) {
      state.order = response.data.data;
    }
  });
}

function updateOrder() {
  api.put("orders", state.order).then((response) => {
    if (response && response.data.success) {
      $q.notify({
        message: response.data.message,
        type: "positive",
      });
      getAdminOrders();
      resetOrder();
    }
  });
}

function resetOrder() {
  showmodal.value = false;
  state.order.id = "";
  state.order.orderStatusId = "";
}

function formatDate(orderdate) {
  return date.formatDate(orderdate, "DD/MM/YYYY HH:mm");
}
</script>
<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
