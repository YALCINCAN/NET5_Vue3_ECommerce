<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Categories"
        :rows="categories"
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
                @click="getCategoryWithOptions(props.row.id)"
                color="primary"
                icon="edit"
              />
              <q-btn
                dense
                @click="removeCateogry(props.row.id)"
                color="red"
                icon="delete"
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
            <span v-if="category.id > 0">Update Category</span>
            <span v-else>Add New Category</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetCategory()"
            ></q-btn>
          </div>
        </q-card-section>
        <q-separator inset></q-separator>
        <q-card-section class="q-pt-none">
          <q-form class="q-gutter-md">
            <q-list>
              <q-item>
                <q-item-section>
                  <q-select
                    v-model="category.optionIds"
                    :options="selectfilteroptions"
                    option-value="id"
                    option-label="name"
                    map-options
                    @filter="filterFn"
                    dense
                    outlined
                    use-input
                    use-chips
                    multiple
                    emit-value
                    label="Option Name"
                    :error="$v.optionIds.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $v.optionIds.$errors"
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
                  <q-input
                    dense
                    type="text"
                    outlined
                    v-model="category.name"
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
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn
            label="Update"
            @click="updateCategory"
            color="primary"
            v-if="category.id > 0"
            :disable="$v.$invalid"
          />
          <q-btn
            label="Add"
            @click="addCategory"
            :disable="$v.$invalid"
            color="primary"
            v-else
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { ref, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useExportCsv } from "src/composables/useExportCsv";
import Alert from "components/Alert.vue";
import useVuelidate from "@vuelidate/core";
import { required, helpers } from "@vuelidate/validators";
const columns = [
  {
    name: "name",
    align: "left",
    label: "Category Name",
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
store.dispatch("category/getCategories");
store.dispatch("option/getOptions");
const initialCategory = {
  id: 0,
  name: "",
  optionIds: [],
};
const selectfilteroptions = ref([]);
const category = ref({ ...initialCategory });
const categories = computed(() => {
  return store.getters["category/getCategories"];
});
const options = computed(() => {
  return store.getters["option/getOptions"];
});
selectfilteroptions.value = options.value;
const { exportTable } = useExportCsv(categories, columns, "categories");

function getCategoryWithOptions(id) {
  if (id > 0) {
    showmodal.value = true;
    store.dispatch("category/getCategoryWithOptions", id).then((response) => {
      category.value = response;
      const arr = new Array();
      for (let i = 0; i < response.categoryOptions.length; i++) {
        arr.push(response.categoryOptions[i].optionId);
      }
      selectfilteroptions.value = options.value;
      category.value.optionIds = arr;
    });
  }
}
function addCategory() {
  store.dispatch("category/addCategory", category.value).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      resetCategory();
    }
  });
}

function updateCategory() {
  store.dispatch("category/updateCategory", category.value).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      resetCategory();
    }
  });
}
function removeCateogry(id) {
  store.dispatch("category/removeCategory", id).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
    }
  });
}

function filterFn(val, update) {
  update(() => {
    if (val === "") {
      selectfilteroptions.value = options.value;
    } else {
      const needle = val.toLowerCase();
      selectfilteroptions.value = options.value.filter(
        (v) => v.name.toLowerCase().indexOf(needle) > -1
      );
    }
  });
}

const rules = computed(() => {
  return {
    name: {
      required: helpers.withMessage("Name is required", required),
      $autoDirty: true,
    },
    optionIds: {
      required: helpers.withMessage("Options are required", required),
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, category);

function resetCategory() {
  showmodal.value = false;
  category.value = {...initialCategory};
  store.dispatch("alert/clearAlert");
  $v.value.$reset();
}
</script>
<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
