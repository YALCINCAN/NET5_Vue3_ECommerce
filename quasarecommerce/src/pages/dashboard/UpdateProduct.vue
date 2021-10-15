<template>
  <q-page class="q-pa-sm">
    <div class="row q-col-gutter-md">
      <div class="col-12">
        <Alert></Alert>
        <q-card>
          <q-card-section>
            <div class="text-h6">
              <span>Update Product</span>
            </div>
          </q-card-section>
          <q-separator inset></q-separator>
          <q-card-section class="q-pt-none">
            <q-form class="q-gutter-md">
              <q-list>
                <q-item>
                  <q-item-section>
                    <q-input
                      dense
                      outlined
                      v-model="product.name"
                      label="Name"
                      :error="$v.name.$error"
                    >
                      <template #error>
                        <li
                          v-for="error in $v.name.$errors"
                          :key="error.$message"
                        >
                          <span>{{ error.$message }}</span>
                        </li>
                      </template>
                    </q-input>
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section>
                    <q-file
                      clearable
                      v-model="imagefile"
                      accept="image/png, image/jpeg"
                      label="Select Main Image"
                    >
                      <template v-slot:prepend>
                        <q-icon name="cloud_upload" />
                      </template>
                    </q-file>
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section>
                    <q-file
                      clearable
                      v-model="imagefiles"
                      append
                      multiple
                      use-chips
                      accept="image/png, image/jpeg"
                      label="Select Image"
                    >
                      <template v-slot:prepend>
                        <q-icon name="cloud_upload" />
                      </template>
                    </q-file>
                  </q-item-section>
                </q-item>
                <div class="row justify-center">
                  <div class="col-3" v-if="previewimageurl != null">
                    <div class="row justify-center">
                      <label class="text-dark q-mb-xs"
                        >Selected Main Image</label
                      >
                    </div>
                    <div class="row justify-center">
                      <div class="col-3">
                        <q-img :src="previewimageurl" />
                      </div>
                    </div>
                  </div>
                  <div class="col-3" v-if="previewimageurls.length > 0">
                    <div class="row justify-center">
                      <label class="text-dark q-mb-xs">Selected Images</label>
                    </div>
                    <div class="row justify-center q-col-gutter-sm">
                      <div
                        class="col-3"
                        v-for="(item, index) in previewimageurls"
                        :key="index"
                      >
                        <q-img :src="item"></q-img>
                      </div>
                    </div>
                  </div>
                  <div class="col-3" v-if="product.mainImage">
                    <div class="row justify-center">
                      <label class="text-dark q-mb-xs"
                        >Current Main Image</label
                      >
                    </div>
                    <div class="row justify-center">
                      <div class="col-3">
                        <q-img
                          style="width: 80px; height: 80px"
                          :src="config.url + product.mainImage"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="col-3">
                    <div class="row justify-center">
                      <label class="text-dark q-mb-xs">Current Images</label>
                    </div>
                    <div class="row justify-center q-col-gutter-sm">
                      <div
                        class="col-3"
                        v-for="productimage in product.productImages"
                        :key="productimage.id"
                      >
                        <q-img
                          style="width: 80px; height: 80px"
                          :src="config.url + productimage.image"
                        />
                        <div class="text-center">
                          <i
                            @click="confirm(productimage.id)"
                            class="fas fa-trash cursor-pointer"
                          >
                            <q-tooltip>Remove Image</q-tooltip>
                          </i>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <q-item>
                  <q-item-section>
                    <q-input
                      dense
                      v-model="product.price"
                      label="Price"
                      mask="#.###,##"
                      hint="#.###,## - reversed"
                      reverse-fill-mask
                      :error="$v.price.$error"
                    >
                      <template #error>
                        <li
                          v-for="error in $v.price.$errors"
                          :key="error.$message"
                        >
                          <span>{{ error.$message }}</span>
                        </li>
                      </template>
                    </q-input>
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section>
                    <q-editor v-model="product.description" min-height="15rem">
                    </q-editor>
                    <span v-if="$v.description.$error" class="text-red"
                      >Desciption is required</span
                    >
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section>
                    <q-select
                      v-model="product.brandId"
                      :options="brands"
                      emit-value
                      map-options
                      option-value="id"
                      option-label="name"
                      label="Brand Name"
                      :error="$v.brandId.$error"
                    >
                      <template #error>
                        <li
                          v-for="error in $v.brandId.$errors"
                          :key="error.$message"
                        >
                          <span>{{ error.$message }}</span>
                        </li>
                      </template>
                    </q-select>
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section>
                    <q-select
                      v-model="product.categoryId"
                      :options="categories"
                      emit-value
                      map-options
                      disable
                      option-value="id"
                      option-label="name"
                      label="Category Name"
                    />
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section>
                    <div
                      class="row q-col-gutter-sm"
                      v-for="(option, index) in options"
                      :key="index"
                    >
                      <div class="col-6">
                        <q-select
                          v-model="product.productOptionValues[index].optionId"
                          :options="options"
                          emit-value
                          disable
                          map-options
                          option-value="id"
                          option-label="name"
                          label="Option"
                        />
                      </div>
                      <div class="col-6">
                        <q-select
                          v-model="
                            product.productOptionValues[index].optionValueId
                          "
                          :options="option.optionValues"
                          emit-value
                          map-options
                          option-value="id"
                          option-label="value"
                          label="Option Value"
                        />
                      </div>
                    </div>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-form>
          </q-card-section>
          <q-card-actions align="center" class="text-teal">
            <q-btn
              label="Update Product"
              @click="updateProduct"
              :disable="$v.$invalid"
              color="primary"
            />
          </q-card-actions>
        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script setup>
import { computed, watch, ref, onMounted } from "vue";
import { useStore } from "vuex";
import { useRoute } from "vue-router";
import { useRouter } from "vue-router";
import { useQuasar } from "quasar";
import { useMultipleImageUpload } from "src/composables/useMultipleImageUpload";
import { useImageUpload } from "src/composables/useImageUpload";
import { api } from "boot/axios";
import Alert from "src/components/Alert.vue";
import config from "src/config";
import useVuelidate from "@vuelidate/core";
import { required, helpers, numeric } from "@vuelidate/validators";

const product = ref({
  id: 0,
  name: "",
  brandId: 0,
  categoryId: 0,
  mainImage: "",
  price: 0,
  description: "",
  productOptionValues: [],
  productImages: [],
});
const $q = useQuasar();
const { imagefiles, previewimageurls } = useMultipleImageUpload();
const { imagefile, previewimageurl } = useImageUpload();
const store = useStore();
const route = useRoute();
const router = useRouter();
const options = ref([]);
store.dispatch("brand/getBrands");
store.dispatch("category/getCategories");
const brands = computed(() => {
  return store.getters["brand/getBrands"];
});
const categories = computed(() => {
  return store.getters["category/getCategories"];
});

function getAdminProduct(id) {
  api.get("products/" + id + "/adminproduct").then((response) => {
    product.value = response.data.data;
  });
}
onMounted(() => {
  getAdminProduct(route.params.id);
});
watch(
  () => product.value.categoryId,
  () => {
    product.value.productOptionValues.values = [];
    api
      .get("categories/" + product.value.categoryId + "/optionsvalues")
      .then((response) => {
        options.value = response.data.data.options;
      });
  }
);
function confirm(id) {
  $q.dialog({
    title: "Remove Image",
    message: "Would you like to remove image",
    cancel: true,
    persistent: true,
  }).onOk(() => {
    removeImage(id);
  });
}
function updateProduct() {
  let formdata = new FormData();
  formdata.append("Id", product.value.id);
  formdata.append("Name", product.value.name);
  formdata.append("CategoryId", product.value.categoryId);
  if (imagefile != null) {
    formdata.append("Image", imagefile.value);
  }
  formdata.append("Price", product.value.price);
  formdata.append("BrandId", product.value.brandId);
  formdata.append("Description", product.value.description);
  for (var i = 0; i < product.value.productOptionValues.length; i++) {
    formdata.append(
      "OptionValues[" + i + "].OptionId",
      product.value.productOptionValues[i].optionId
    );
    formdata.append(
      "OptionValues[" + i + "].OptionValueId",
      product.value.productOptionValues[i].optionValueId
    );
  }
  if (imagefiles.value.length > 0) {
    for (var i = 0; i < imagefiles.value.length; i++) {
      formdata.append("Images", imagefiles.value[i]);
    }
  }

  store.dispatch("product/updateProduct", formdata).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      router.push({ name: "DashboardProducts" });
    }
  });
}
function removeImage(id) {
  api.delete("productimages/" + id).then((response) => {
    if (response && response.data && response.data.success) {
      let index = product.value.productImages.findIndex((c) => c.id == id);
      if (index > -1) {
        product.value.productImages.splice(index, 1);
      }
      $q.notify({
        message: response.data.message,
        type: "positive",
      });
    }
  });
}

const rules = computed(() => {
  return {
    name: {
      required: helpers.withMessage("Name is required", required),
      $autoDirty: true,
    },
    description: {
      required: helpers.withMessage("Description are required", required),
      $autoDirty: true,
    },
    price: {
      required: helpers.withMessage("Price is required", required),
      $autoDirty: true,
    },
    brandId: {
      required: helpers.withMessage("Brand is required", required),
      numeric,
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, product);
</script>

<style lang="scss" scoped></style>

<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
