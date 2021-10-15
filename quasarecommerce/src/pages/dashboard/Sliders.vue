<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Sliders"
        :rows="sliders"
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
                @click="getSlider(props.row.id)"
                color="primary"
                icon="edit"
              />
              <q-btn
                dense
                @click="removeSlider(props.row.id)"
                color="red"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Image="props">
          <q-td :props="props">
            <q-img
              width="120px"
              fit
              :src="config.url + props.row.image"
            ></q-img>
          </q-td>
        </template>
        <template v-slot:body-cell-Active="props">
          <q-td :props="props">
            <q-checkbox dense outlined disable v-model="props.row.active" />
          </q-td>
        </template>
      </q-table>
    </q-card>
    <q-dialog v-model="showmodal" persistent>
      <q-card style="width: 600px; max-width: 60vw">
        <q-card-section>
          <Alert></Alert>
          <div class="text-h6">
            <span v-if="slider.id > 0">Update Slider</span>
            <span v-else>Add New Slider</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetSlider()"
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
                    :error="imagefile==null && slider.id==0"
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
                  <q-checkbox
                    dense
                    outlined
                    v-model="slider.active"
                    label="Active"
                  />
                </q-item-section>
              </q-item>

              <div class="row justify-center">
                <div class="col-3" v-if="previewimageurl != null">
                  <div class="row justify-center">
                    <label class="text-dark q-mb-xs">Selected Image</label>
                  </div>
                  <div class="row justify-center">
                    <q-img
                      style="width: 80px; height: 80px"
                      fit
                      :src="previewimageurl"
                    />
                  </div>
                </div>
                <div class="col-3" v-if="slider.image">
                  <div class="row justify-center">
                    <label class="text-dark q-mb-xs">Current Image</label>
                  </div>
                  <div class="row justify-center">
                    <q-img
                      fit
                      style="width: 80px; height: 80px"
                      :src="config.url + slider.image"
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
            @click="updateSlider"
            color="primary"
            v-if="slider.id > 0"
          />
          <q-btn label="Add" :disable="imagefile==null" @click="addSlider" color="primary" v-else />
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
const columns = [
  {
    name: "Image",
    align: "left",
    label: "Image",
    field: "image",
    sortable: true,
  },
  {
    name: "Active",
    align: "left",
    label: "Active",
    field: "active",
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
  rowsPerPage: 7,
};
const showmodal = ref(false);
const store = useStore();
const $q = useQuasar();
store.dispatch("slider/getSliders");
const initialSlider = {
  id: 0,
  active: false,
  image: null,
};
const slider = reactive({ ...initialSlider });
const sliders = computed(() => {
  return store.getters["slider/getSliders"];
});
const { exportTable } = useExportCsv(sliders, columns, "sliders");
const { imagefile, previewimageurl } = useImageUpload();

function getSlider(id) {
  if (id > 0) {
    showmodal.value = true;
    store.dispatch("slider/getSlider", id).then((response) => {
      Object.assign(slider, response);
    });
  }
}
function addSlider() {
  let formdata = new FormData();
  formdata.append("ImageFile", imagefile.value);
  formdata.append("Active", slider.active);
  store.dispatch("slider/addSlider", formdata).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      resetSlider();
    }
  });
}

function updateSlider() {
  let formdata = new FormData();
  formdata.append("Id", slider.id);
  formdata.append("Active", slider.active);
  formdata.append("ImageFile", imagefile.value);
  store.dispatch("slider/updateSlider", formdata).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      resetSlider();
    }
  });
}

function removeSlider(id) {
  store.dispatch("slider/removeSlider", id).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
    }
  });
}
function resetSlider() {
  showmodal.value = false;
  Object.assign(slider, initialSlider);
  imagefile.value = null;
  store.dispatch("alert/clearAlert");
}

</script>
<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
