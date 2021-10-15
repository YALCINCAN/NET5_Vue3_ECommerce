<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Brands"
        :rows="brands"
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
        <template v-slot:body-cell-action="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-btn
                dense
                @click="getBrand(props.row.id)"
                color="primary"
                icon="edit"
              />
              <q-btn
                dense
                @click="removeBrand(props.row.id)"
                color="red"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Image="props">
          <q-td :props="props">
            <q-img
              fit
              height="72px"
              width="72px"
              :src="config.url + props.row.image"
            ></q-img>
          </q-td>
        </template>
      </q-table>
    </q-card>
    <q-dialog v-model="showmodal" persistent>
      <q-card style="width: 600px; max-width: 60vw">
        <q-card-section>
          <Alert></Alert>
          <div class="text-h6">
            <span v-if="brand.id > 0">Update Brand</span>
            <span v-else>Add New Brand</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetBrand()"
            ></q-btn>
          </div>
        </q-card-section>
        <q-separator inset></q-separator>
        <q-card-section class="q-pt-none">
          <q-form class="q-gutter-md">
            <q-list>
              <q-item>
                <q-item-section>
                  <q-file
                    clearable
                    v-model="imagefile"
                    accept="image/png, image/jpeg"
                    label="Select Image"
                    :error="imagefile == null && brand.id == 0"
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
                  <q-input
                    dense
                    type="text"
                    outlined
                    v-model="brand.name"
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

              <div class="row justify-center">
                <div class="col-3" v-if="previewimageurl != null">
                  <div class="row justify-center">
                    <label class="text-dark q-mb-xs">Selected Image</label>
                  </div>
                  <div class="row justify-center">
                    <q-img
                      fit
                      style="width: 80px; height: 80px"
                      :src="previewimageurl"
                    />
                  </div>
                </div>
                <div class="col-3" v-if="brand.image">
                  <div class="row justify-center">
                    <label class="text-dark q-mb-xs">Current Image</label>
                  </div>
                  <div class="row justify-center">
                    <q-img
                      fit
                      style="width: 80px; height: 80px"
                      :src="config.url + brand.image"
                    />
                  </div>
                </div>
              </div>
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn
            label="Update"
            @click="updateBrand"
            :disable="$v.$invalid"
            color="primary"
            v-if="brand.id > 0"
          />
          <q-btn
            label="Add"
            :disable="imagefile == null || $v.$invalid"
            @click="addBrand"
            color="primary"
            v-else
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { reactive, ref, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useImageUpload } from "src/composables/useImageUpload";
import { useExportCsv } from "src/composables/useExportCsv";
import Alert from "components/Alert.vue";
import config from "src/config";
import useVuelidate from "@vuelidate/core";
import { required, helpers } from "@vuelidate/validators";
const columns = [
  {
    name: "Image",
    align: "left",
    label: "Image",
    field: "image",
    sortable: true,
  },
  {
    name: "Name",
    align: "left",
    label: "Name",
    field: "name",
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
const showmodal = ref(false);
const store = useStore();
const $q = useQuasar();
store.dispatch("brand/getBrands");
const initialBrand = {
  id: 0,
  name: "",
  image: null,
};
const brand = reactive({ ...initialBrand });
const brands = computed(() => {
  return store.getters["brand/getBrands"];
});
const { exportTable } = useExportCsv(brands, columns, "brands");
const { imagefile, previewimageurl } = useImageUpload();

function getBrand(id) {
  if (id > 0) {
    showmodal.value = true;
    store.dispatch("brand/getBrand", id).then((response) => {
      Object.assign(brand, response);
    });
  }
}
function addBrand() {
  let formdata = new FormData();
  formdata.append("ImageFile", imagefile.value);
  formdata.append("Name", brand.name);
  store.dispatch("brand/addBrand", formdata).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      resetBrand();
    }
  });
}

function updateBrand() {
  let formdata = new FormData();
  formdata.append("Id", brand.id);
  formdata.append("Name", brand.name);
  formdata.append("ImageFile", imagefile.value);
  store.dispatch("brand/updateBrand", formdata).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      resetBrand();
    }
  });
}

function removeBrand(id) {
  store.dispatch("brand/removeBrand", id).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
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
  };
});

const $v = useVuelidate(rules, brand);

function resetBrand() {
  showmodal.value = false;
  Object.assign(brand, initialBrand);
  imagefile.value = null;
  store.dispatch("alert/clearAlert");
  $v.value.$reset()
}
</script>
<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
