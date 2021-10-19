<template>
  <Categories></Categories>
  <q-page class="container q-pa-lg">
    <div class="row q-col-gutter-md justify-center">
      <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 q-mb-md">
        <div class="q-mb-sm">
          <q-select
            v-model="selectedsortoption"
            :options="sortoptions"
            emit-value
            clearable
            map-options
            option-value="value"
            option-label="label"
            label="Sort By Price"
          />
        </div>
        <div class="q-mb-sm">
          <q-toolbar class="bg-primary text-white shadow-2">
            <q-toolbar-title>Brands</q-toolbar-title>
            <q-item-label v-if="selectedbrands.length > 0" @click="resetBrand()"
              ><i class="fas fa-times float-right" style="color: red"
                ><q-tooltip>Remove Filter</q-tooltip></i
              ></q-item-label
            >
          </q-toolbar>
          <q-list bordered>
            <q-item tag="label" v-for="(brand, index) in brands" :key="index">
              <q-item-section avatar>
                <q-radio v-model="selectedbrands" :val="brand" color="blue" />
              </q-item-section>
              <q-item-section>
                <q-item-label>{{ brand }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </div>
        <div class="q-mb-sm">
          <q-toolbar class="bg-primary text-white shadow-2">
            <q-toolbar-title>Price</q-toolbar-title>
            <q-item-label v-if="selectedprice.length > 0" @click="resetPrice()"
              ><i class="fas fa-times float-right" style="color: red"
                ><q-tooltip>Remove Filter</q-tooltip></i
              ></q-item-label
            >
          </q-toolbar>
          <q-list bordered>
            <q-item tag="label">
              <q-item-section avatar>
                <q-radio
                  v-model="selectedprice"
                  val="1000"
                  @click="setMaxPrice(1000)"
                  color="blue"
                />
              </q-item-section>
              <q-item-section>
                <q-item-label>Under $1000</q-item-label>
              </q-item-section>
            </q-item>
            <q-item tag="label">
              <q-item-section avatar>
                <q-radio
                  v-model="selectedprice"
                  val="1000-1500"
                  @click="setPrice(1000, 1500)"
                  color="blue"
                />
              </q-item-section>
              <q-item-section>
                <q-item-label>$1000 - $1500</q-item-label>
              </q-item-section>
            </q-item>
            <q-item tag="label">
              <q-item-section avatar>
                <q-radio
                  v-model="selectedprice"
                  val="2000-2500"
                  @click="setPrice(2000, 2500)"
                  color="blue"
                />
              </q-item-section>
              <q-item-section>
                <q-item-label>$2000 - $2500</q-item-label>
              </q-item-section>
            </q-item>
            <q-item tag="label">
              <q-item-section avatar>
                <q-radio
                  v-model="selectedprice"
                  val="2000-5000"
                  @click="setPrice(2000, 5000)"
                  color="blue"
                />
              </q-item-section>
              <q-item-section>
                <q-item-label>$2000 - $5000</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </div>
        <div class="q-mb-sm" v-if="selectedvalues.length > 0 && selectedvalues">
          <div class="row justify-center" @click="resetOptionValueFilters()">
            <i class="fas fa-trash" style="color:red">
              <q-tooltip>Remove All Option Filters</q-tooltip>
            </i>
          </div>
        </div>
        <div
          class="q-mb-sm"
          v-for="(option, index) in options.options"
          :key="index"
        >
          <q-toolbar class="bg-primary text-white shadow-2">
            <q-toolbar-title>{{ option.name }}</q-toolbar-title>
          </q-toolbar>
          <q-list bordered>
            <q-item
              tag="label"
              v-for="optionvalue in option.optionValues"
              :key="optionvalue.id"
            >
              <q-item-section avatar>
                <q-radio
                  v-model="selectedvalues"
                  :val="optionvalue.value"
                  color="blue"
                />
              </q-item-section>
              <q-item-section>
                <q-item-label>{{ optionvalue.value }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </div>
      </div>
      <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
        <div class="row">
          <Product :products="products"></Product>
        </div>
      </div>
    </div>
    <div class="row justify-center q-mt-lg">
      <div>
        <q-pagination
          v-if="products.length > 0"
          v-model="query.page"
          :max="totalpage"
          :direction-links="true"
        >
        </q-pagination>
      </div>
    </div>
  </q-page>
</template>

<script setup>
import { ref, computed, watch } from "vue";
import { api } from "boot/axios";
import { useRoute } from "vue-router";
import Categories from "src/components/Category.vue";
import Product from "src/components/Product.vue";
import qs from "qs";

const route = useRoute();
const selectedvalues = ref([]);
const selectedbrands = ref([]);
const selectedprice = ref([]);
const selectedsortoption = ref(null);
const query = ref({
  page: parseInt(route.query.page) || 1,
  pageSize: parseInt(route.query.pagesize) || 3,
  "category.slug": route.query["category.slug"] || "",
  search: route.query["search"] || "",
  "ProductOptionValues.OptionValue.Value": [],
  "brand.name": [],
});

const sortoptions = [
  {
    label: "Price Ascending",
    value: "Ascending",
  },
  {
    label: "Price Descending",
    value: "Descending",
  },
];
const products = ref([]);
const options = ref({});
const totalItems = ref(0);
const brands = ref([]);
const totalpage = computed(() => {
  return Math.ceil(totalItems.value / query.value.pageSize);
});

getProducts();
function getProducts() {
  const q = qs.stringify(query.value, {
    arrayFormat: "repeat",
    format: "RFC1738",
  });
  api.get("products/getproducts/" + "?" + q).then((response) => {
    if (response && response.data && response.data.data) {
      products.value = response.data.data;
      totalItems.value = response.data.totalItems;
      var array = new Array();
      for (let i = 0; i < response.data.data.length; i++) {
        array.push(response.data.data[i].brandName);
      }
      brands.value = array;
      getOptionsWithValuesByCategory();
    }
  });
}

function getOptionsWithValuesByCategory() {
  const slug = route.query["category.slug"];
  if (slug != null) {
    api.get("categories/" + slug + "/optionsvalues").then((response) => {
      if (response && response.data && response.data.success) {
        options.value = response.data.data;
      }
    });
  }
}

function setMaxPrice(price) {
  delete query.value["Price.Min"];
  query.value["Price.Max"] = price;
}

function setPrice(minprice, maxprice) {
  query.value["Price.Min"] = minprice;
  query.value["Price.Max"] = maxprice;
}
function resetBrand() {
  selectedbrands.value = [];
}
function resetPrice() {
  delete query.value["Price.Min"];
  delete query.value["Price.Max"];
  selectedprice.value = [];
}

function resetOptionValueFilters() {
  selectedvalues.value = [];
}
watch(
  () => query.value.page,
  () => {
    query.value["brand.name"] = selectedbrands.value;
    getProducts();
  }
);

watch(
  () => route.query["category.slug"],
  () => {
    query.value["category.slug"] = route.query["category.slug"];
    selectedprice.value = null;
    delete query.value["Price.Min"];
    delete query.value["Price.Max"];
    query.value.page = 1;
    query.value.pageSize = 3;
    query.value["ProductOptionValues.OptionValue.Value"] = [];
    query.value["brand.name"] = [];
    selectedbrands.value = [];
    delete query.value["Sort"];
    delete query.value["SortBy"];
    selectedsortoption.value = null;
    selectedvalues.value = [];
    selectedprice.value=[];
    getProducts();
  }
);

watch(
  () => route.query["search"],
  () => {
    selectedprice.value = null;
    query.value["search"] = route.query["search"];
    delete query.value["Price.Min"];
    delete query.value["Price.Max"];
    query.value.page = 1;
    query.value.pageSize = 3;
    query.value["ProductOptionValues.OptionValue.Value"] = [];
    query.value["brand.name"] = [];
    delete query.value["Sort"];
    delete query.value["SortBy"];
    selectedsortoption.value = null;
    getProducts();
  }
);

watch(
  () => selectedvalues.value,
  () => {
    query.value["ProductOptionValues.OptionValue.Value"] = selectedvalues.value;
    query.value["category.slug"] = route.query["category.slug"];
    query.value.page = 1;
    query.value.pageSize = 3;
    query.value["brand.name"] = selectedbrands.value;
    getProducts();
  }
);

watch(
  () => selectedbrands.value,
  () => {
    query.value["category.slug"] = route.query["category.slug"];
    query.value.page = 1;
    query.value.pageSize = 3;
    query.value["brand.name"] = selectedbrands.value;
    getProducts();
  }
);

watch(
  () => selectedprice.value,
  () => {
    query.value["category.slug"] = route.query["category.slug"];
    query.value.page = 1;
    query.value.pageSize = 3;
    query.value["brand.name"] = selectedbrands.value;
    getProducts();
  }
);

watch(
  () => selectedsortoption.value,
  () => {
    if (selectedsortoption.value != null) {
      query.value["brand.name"] = selectedbrands.value;
      query.value["Sort"] = "Price";
      query.value["SortBy"] = selectedsortoption.value;
      getProducts();
    } else {
      delete query.value["Sort"];
      delete query.value["SortBy"];
      getProducts();
    }
  }
);
</script>
<style scoped></style>
