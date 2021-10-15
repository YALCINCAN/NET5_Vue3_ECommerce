<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Options"
        :rows="options"
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
                @click="getOption(props.row.id)"
                color="primary"
                icon="edit"
              />
              <q-btn
                dense
                @click="removeOption(props.row.id)"
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
            <span v-if="option.id > 0">Update Option</span>
            <span v-else>Add New Option</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetOption()"
            ></q-btn>
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
                    type="text"
                    outlined
                    v-model="option.name"
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
            @click="updateOption"
            :disable="$v.$invalid"
            color="primary"
            v-if="option.id > 0"
          />
          <q-btn
            label="Add"
            @click="addOption"
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
    name: "Name",
    align: "left",
    label: "Option Name",
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
store.dispatch("option/getOptions");
const initialOption = {
  id: 0,
  name: "",
};
const option = ref({ ...initialOption });
const options = computed(() => {
  return store.getters["option/getOptions"];
});
const { exportTable } = useExportCsv(options, columns, "options");

function getOption(id) {
  if (id > 0) {
    showmodal.value = true;
    store.dispatch("option/getOption", id).then((response) => {
      option.value = response;
    });
  }
}
function addOption() {
  store.dispatch("option/addOption", option.value).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      resetOption();
    }
  });
}

function updateOption() {
  store.dispatch("option/updateOption", option.value).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
      resetOption();
    }
  });
}
function removeOption(id) {
  store.dispatch("option/removeOption", id).then((response) => {
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

const $v = useVuelidate(rules, option);
function resetOption() {
  showmodal.value = false;
  option.value = {...initialOption};
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
