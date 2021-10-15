<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="OptionsValues"
        :rows="optionValues"
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
                @click="getOptionValue(props.row.id)"
                color="primary"
                icon="edit"
              />
              <q-btn
                dense
                @click="removeOptionValue(props.row.id)"
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
            <span v-if="optionValue.id > 0">Update Option Value</span>
            <span v-else>Add New Option Value</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetOptionValue()"
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
                    v-model="optionValue.optionId"
                    :options="options"
                    option-value="id"
                    map-options
                    dense
                    outlined
                    emit-value
                    option-label="name"
                    label="Option Name"
                    :error="$v.optionId.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $v.optionId.$errors"
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
                    v-model="optionValue.value"
                    label="Option Value"
                    :error="$v.value.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $v.value.$errors"
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
            @click="updateOptionValue"
            color="primary"
            :disable="$v.$invalid"
            v-if="optionValue.id > 0"
          />
          <q-btn
            label="Add"
            @click="addOptionValue"
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
    label: "Option Name",
    field: (row) => row.option.name,
    sortable: true,
  },
  {
    name: "value",
    align: "left",
    label: "Option Value",
    field: "value",
    sortable: true,
    sort: (a, b) => parseInt(a, 10) - parseInt(b, 10),
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
store.dispatch("optionvalue/getOptionValues");
store.dispatch("option/getOptions");
const initialOptionValue = {
  id: 0,
  optionId: null,
  value: null,
};
const optionValue = ref({ ...initialOptionValue });
const optionValues = computed(() => {
  return store.getters["optionvalue/getOptionValues"];
});
const options = computed(() => {
  return store.getters["option/getOptions"];
});
const { exportTable } = useExportCsv(optionValues, columns, "optionvalues");

function getOptionValue(id) {
  if (id > 0) {
    showmodal.value = true;
    store.dispatch("optionvalue/getOptionValue", id).then((response) => {
      optionValue.value = response;
    });
  }
}
function addOptionValue() {
  store
    .dispatch("optionvalue/addOptionValue", optionValue.value)
    .then((response) => {
      if (response && response.success) {
        $q.notify({
          message: response.message,
          type: "positive",
        });
        resetOptionValue();
      }
    });
}

function updateOptionValue() {
  store
    .dispatch("optionvalue/updateOptionValue", optionValue.value)
    .then((response) => {
      if (response && response.success) {
        $q.notify({
          message: response.message,
          type: "positive",
        });
        resetOptionValue();
      }
    });
}
function removeOptionValue(id) {
  store.dispatch("optionvalue/removeOptionValue", id).then((response) => {
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
    value: {
      required: helpers.withMessage("Value is required", required),
      $autoDirty: true,
    },
    optionId: {
      required: helpers.withMessage("Option is required", required),
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, optionValue);

function resetOptionValue() {
  optionValue.value = {...initialOptionValue};
  showmodal.value = false;
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
