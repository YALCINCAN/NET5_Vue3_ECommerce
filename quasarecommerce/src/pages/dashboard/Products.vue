<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Products"
        :rows="products"
        :hide-header="mode === 'grid'"
        :columns="columns"
        :grid="mode == 'grid'"
        :filter="filter"
        :pagination="pagination"
      >
        <template v-slot:top-right="props">
          <q-btn
            @click="showmodal = true"
            outline
            color="primary"
            to="/dashboard/products/add"
            label="Add New"
            class="q-mr-xs"
          />
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
            <q-tooltip :disable="$q.platform.is.mobile" v-close-popup
              >{{
                props.inFullscreen ? "Exit Fullscreen" : "Toggle Fullscreen"
              }}
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
            <q-tooltip :disable="$q.platform.is.mobile" v-close-popup
              >{{ mode === "grid" ? "List" : "Grid" }}
            </q-tooltip>
          </q-btn>

          <q-btn
            color="primary"
            icon-right="archive"
            label="Export to csv"
            no-caps
            @click="exportTable"
          />
        </template>
        <template v-slot:body-cell-action="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-btn
                dense
                :to="{
                  name: 'DashboardProductUpdate',
                  params: { id: props.row.id },
                }"
                color="primary"
                icon="edit"
              />
              <q-btn
                dense
                @click="removeProduct(props.row.id)"
                color="red"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Image="props">
          <q-td :props="props">
            <q-img
              height="72px"
              width="72px"
              :src="config.url + props.row.image"
            ></q-img>
          </q-td>
        </template>
        <template v-slot:body-cell-Description="props">
          <q-td :props="props">
            <div v-html="props.row.description"></div>
          </q-td>
        </template>
      </q-table>
    </q-card>
  </q-page>
</template>

<script setup>
import { ref } from "vue";
import { useExportCsv } from "src/composables/useExportCsv";
import { api } from "boot/axios";
import config from "src/config";
import { useQuasar } from "quasar";
const $q = useQuasar();
const columns = [
  {
    name: "Name",
    align: "left",
    label: "Name",
    field: "name",
    sortable: true,
  },
  {
    name: "Price",
    align: "left",
    label: "Price",
    field: "price",
    sortable: true,
  },
  {
    name: "Description",
    align: "left",
    label: "Description",
    field: "description",
    sortable: true,
  },
  {
    name: "Category Name",
    align: "left",
    label: "Category Name",
    field: "categoryName",
    sortable: true,
  },
  {
    name: "Brand Name",
    align: "left",
    label: "Brand Name",
    field: "brandName",
    sortable: true,
  },
  {
    name: "action",
    align: "left",
    label: "Actions",
    field: "action",
  },
];
const filter = ref("");
const mode = ref("list");
const pagination = {
  rowsPerPage: 20,
};
getAdminProducts();
const products = ref([]);

function getAdminProducts() {
  api.get("products/adminproducts").then((response) => {
    products.value = response.data.data;
  });
}
const { exportTable } = useExportCsv(products, columns, "products");

function removeProduct(id) {
  api.delete("products/" + id).then((response) => {
    if (response && response.data && response.data.success) {
      let index = products.value.findIndex((c) => c.id == id);
      if (index > -1) {
        products.value.splice(index, 1);
      }
      $q.notify({
        message: response.data.message,
        type: "positive",
      });
    }
  });
}
</script>
<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
