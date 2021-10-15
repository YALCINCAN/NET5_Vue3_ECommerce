<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Roles"
        :rows="roles"
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
                color="primary"
                @click="getRole(props.row.id)"
                icon="edit"
              />
              <q-btn
                dense
                color="red"
                @click="removeRole(props.row.id)"
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
            <span v-if="role.id === ''">Add New Role</span>
            <span v-else>Update Role</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetRole()"
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
                    outlined
                    v-model="role.name"
                    label="Role Name"
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
            @click="updateRole"
            color="primary"
            :disable="$v.$invalid"
            v-if="role.id != ''"
          />
          <q-btn
            label="Add"
            :disable="$v.$invalid"
            @click="addRole"
            color="primary"
            v-else
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { ref,computed } from "vue";
import { useQuasar } from "quasar";
import { useStore } from "vuex";
import { useExportCsv } from "src/composables/useExportCsv";
import Alert from "components/Alert.vue";
import { api } from "src/boot/axios.js";
import useVuelidate from "@vuelidate/core";
import { required, helpers } from "@vuelidate/validators";
const columns = [
  {
    name: "Role Name",
    align: "left",
    label: "Role Name",
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

const $q = useQuasar();
const store = useStore();
const initialRole = {
  id: "",
  name: "",
};
const role = ref({ ...initialRole });
const roles = ref([]);
const { exportTable } = useExportCsv(roles, columns, "roles");
const filter = ref("");
const mode = ref("list");
const pagination = {
  rowsPerPage: 20,
};
const showmodal = ref(false);
getRoles();
function getRoles() {
  api.get("roles").then((response) => {
    if (response && response.data && response.data.success) {
      roles.value = response.data.data;
    }
  });
}

function getRole(id) {
  if (id != "") {
    showmodal.value = true;
    api.get("roles/" + id).then((response) => {
      if (response && response.data && response.data.success) {
        role.value = response.data.data;
      }
    });
  }
}

function addRole() {
  showmodal.value = true;
  var payload = {
    name: role.value.name,
  };
  api
    .post("roles", payload)
    .then((response) => {
      if (response && response.data && response.data.success) {
        $q.notify({
          message: response.data.message,
          type: "positive",
        });
        getRoles();
        resetRole();
      }
    })
    .catch((error) => {
      store.dispatch("alert/setErrors", error.response.data.Errors);
    });
}

function updateRole() {
  showmodal.value = true;
  api
    .put("roles", role.value)
    .then((response) => {
      if (response && response.data && response.data.success) {
        $q.notify({
          message: response.data.message,
          type: "positive",
        });
        getRoles();
        resetRole();
      }
    })
    .catch((error) => {
      store.dispatch("alert/setErrors", error.response.data.Errors);
    });
}
function removeRole(id) {
  if (id != "") {
    api
      .delete("roles/" + id)
      .then((response) => {
        if (response && response.data && response.data.success) {
          $q.notify({
            message: response.data.message,
            type: "positive",
          });
          getRoles();
          resetRole();
        }
      })
      .catch((error) => {
        store.dispatch("alert/setErrors", error.response.data.Errors);
      });
  }
}
const rules = computed(() => {
  return {
    name: {
      required: helpers.withMessage("Name is required", required),
      $autoDirty: true,
    },
  };
});

const $v = useVuelidate(rules, role);

function resetRole() {
  showmodal.value = false;
  role.value = initialRole;
  store.dispatch("alert/clearAlert");
  $v.value.$reset();
}
</script>

<style lang="scss" scoped></style>
