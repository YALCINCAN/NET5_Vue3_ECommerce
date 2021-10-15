<template>
  <q-page class="q-pa-sm">
    <div class="row q-col-gutter-md">
      <div class="col-12">
        <q-card>
          <Alert></Alert>
          <q-card-section>
            <div class="text-h6">
              <span>Add Product</span>
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
                      :error="imagefile == null"
                    >
                      <template #error>
                        <span>Please select image</span>
                      </template>
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
                      :error="imagefiles.length == 0"
                    >
                      <template #error>
                        <span>Please select image</span>
                      </template>
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
                      option-value="id"
                      option-label="name"
                      label="Category Name"
                      :error="$v.categoryId.$error"
                    >
                      <template #error>
                        <li
                          v-for="error in $v.categoryId.$errors"
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
                    <div
                      class="row q-col-gutter-sm"
                      v-for="(option, index) in options"
                      :key="index"
                    >
                      <div class="col-6">
                        <q-select
                          v-model="product.productoptionvalues[index].optionid"
                          :options="options"
                          emit-value
                          disable
                          map-options
                          option-value="id"
                          option-label="name"
                          label="Option"
                          :error="$v.productoptionvalues.$each.$response.$errors[index].optionid.length>0"
                        >
                          <template #error>
                            <li
                              v-for="error in $v.productoptionvalues.$each
                                .$response.$errors[index].optionid"
                              :key="error.$message"
                            >
                              <span>{{ error.$message }}</span>
                            </li>
                          </template>
                        </q-select>
                      </div>
                      <div class="col-6">
                        <q-select
                          v-model="
                            product.productoptionvalues[index].optionvalueid
                          "
                          :options="option.optionValues"
                          emit-value
                          map-options
                          option-value="id"
                          option-label="value"
                          label="Option Value"
                          :error="
                            $v.productoptionvalues.$each.$response.$errors[index].optionvalueid.length>0
                          "
                        >
                          <template #error>
                            <li
                              v-for="error in $v.productoptionvalues.$each
                                .$response.$errors[index].optionvalueid"
                              :key="error.$message"
                            >
                              <span>{{ error.$message }}</span>
                            </li>
                          </template>
                        </q-select>
                      </div>
                    </div>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-form>
          </q-card-section>
          <q-card-actions align="center" class="text-teal">
            <q-btn
              label="Add Product"
              :disable="imagefile == null || $v.$invalid"
              @click="addProduct"
              color="primary"
            />
          </q-card-actions>
        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script setup>
import { reactive, computed, watch, ref } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";
import { useMultipleImageUpload } from "src/composables/useMultipleImageUpload";
import { useImageUpload } from "src/composables/useImageUpload";
import { api } from "boot/axios";
import Alert from "src/components/Alert.vue";
import useVuelidate from "@vuelidate/core";
import { required, helpers, numeric } from "@vuelidate/validators";
const $q = useQuasar();
const router = useRouter();
const product = reactive({
  id: 0,
  price: null,
  categoryId: "Select Category",
  brandId: "Select Brand",
  name: "",
  description: "",
  productoptionvalues: [],
});
const { imagefiles, previewimageurls } = useMultipleImageUpload();
const { imagefile, previewimageurl } = useImageUpload();
const store = useStore();
const options = ref([]);
store.dispatch("brand/getBrands");
store.dispatch("category/getCategories");
const brands = computed(() => {
  return store.getters["brand/getBrands"];
});
const categories = computed(() => {
  return store.getters["category/getCategories"];
});


watch(
  () => product.categoryId,
  () => {
    api
      .get("categories/" + product.categoryId + "/optionsvalues")
      .then((response) => {
        options.value = response.data.data.options;
        product.productoptionvalues = [];
        options.value.forEach((option) => {
          let productoptionvalue = {
            optionid: option.id,
            optionvalueid: "Select Option Value",
          };
          product.productoptionvalues.push(productoptionvalue);
        });
      });
  }
);
function addProduct() {
  let formdata = new FormData();
  formdata.append("Name", product.name);
  formdata.append("CategoryId", product.categoryId);
  formdata.append("Price", product.price);
  formdata.append("Image", imagefile.value);
  formdata.append("BrandId", product.brandId);
  formdata.append("Description", product.description);
  for (var i = 0; i < product.productoptionvalues.length; i++) {
    formdata.append(
      "OptionValues[" + i + "].OptionId",
      product.productoptionvalues[i].optionid
    );
    formdata.append(
      "OptionValues[" + i + "].OptionValueId",
      product.productoptionvalues[i].optionvalueid
    );
  }
  for (var i = 0; i < imagefiles.value.length; i++) {
    formdata.append("Images", imagefiles.value[i]);
  }
  store.dispatch("product/addProduct", formdata).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      router.push({ name: "DashboardProducts" });
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
    categoryId: {
      required: helpers.withMessage("Category is required", required),
      numeric,
      $autoDirty: true,
    },
    productoptionvalues: {
      $each:helpers.forEach({
        optionid:{required,numeric},
        optionvalueid:{required,numeric}
      })
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
