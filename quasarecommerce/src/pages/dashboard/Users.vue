<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Users"
        :rows="userswithroles"
        :hide-header="mode === 'grid'"
        :columns="columns"
        :grid="mode == 'grid'"
        :filter="filter"
        :pagination="pagination"
      >
        <template v-slot:top-right="props">
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
                @click="getUser(props.row.user.id)"
                label="Edit"
              />
              <q-btn
                dense
                color="purple"
                @click="getAssignedRoles(props.row.user.id)"
                label="Assign Role"
              />
              <q-btn
                dense
                color="green"
                @click="getUserForChangePassword(props.row.user.id)"
                label="Change Password"
              />
              <q-btn
                dense
                color="red"
                @click="deleteUser(props.row.user.id)"
                label="Remove"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Roles="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-badge
                v-for="role in props.row.roles"
                :key="role.id"
                color="teal"
                size="sm"
              >
                {{ role }}
              </q-badge>
            </div>
          </q-td>
        </template>
      </q-table>
    </q-card>
    <q-dialog v-model="edituser" persistent>
      <q-card style="width: 600px; max-width: 60vw">
        <q-card-section>
          <Alert></Alert>
          <div class="text-h6">
            <span>Update User</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetUser()"
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
                    v-model="user.userName"
                    label="Username"
                    :error="$vuser.userName.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $vuser.userName.$errors"
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
                  <q-input
                    dense
                    outlined
                    v-model="user.firstName"
                    :error="$vuser.firstName.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $vuser.firstName.$errors"
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
                  <q-input
                    dense
                    outlined
                    v-model="user.lastName"
                    label="Last Name"
                    :error="$vuser.lastName.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $vuser.lastName.$errors"
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
                  <q-input
                    dense
                    outlined
                    v-model="user.email"
                    label="Email"
                    :error="$vuser.email.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $vuser.email.$errors"
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
            :disable="$vuser.$invalid"
            @click="UpdateUserByAdmin"
            color="primary"
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-dialog v-model="showassignedroles" persistent>
      <q-card style="width: 600px; max-width: 60vw">
        <q-card-section>
          <Alert></Alert>
          <div class="text-h6">
            <span>Assigned Roles</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetUser()"
            ></q-btn>
            <div
              class="q-pa-md"
              v-for="userrole in userroles"
              :key="userrole.roleId"
            >
              <q-checkbox
                type="checkbox"
                color="deep-purple-7"
                v-model="userrole.exist"
              />
              {{ userrole.roleName }}
            </div>
          </div>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn label="Assign Role" @click="AssignRole" color="primary" />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-dialog v-model="changepassword" persistent>
      <q-card style="width: 600px; max-width: 60vw">
        <q-card-section>
          <Alert></Alert>
          <div class="text-h6">
            <span>Change Password</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetUser()"
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
                    type="password"
                    v-model="changepassworduser.newpassword"
                    label="New Password"
                    :error="$vchangepassword.newpassword.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $vchangepassword.newpassword.$errors"
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
                  <q-input
                    dense
                    outlined
                    type="password"
                    v-model="changepassworduser.confirmpassword"
                    label="Confirm Password"
                    :error="$vchangepassword.confirmpassword.$error"
                  >
                    <template #error>
                      <li
                        v-for="error in $vchangepassword.confirmpassword
                          .$errors"
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
            label="Change Password"
            @click="passwordchangebyadmin"
            color="primary"
            :disable="$vchangepassword.$invalid"
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { ref, computed } from "vue";
import { useQuasar } from "quasar";
import { useStore } from "vuex";
import { useExportCsv } from "src/composables/useExportCsv";
import Alert from "components/Alert.vue";
import { api } from "src/boot/axios.js";
import useVuelidate from "@vuelidate/core";
import {
  required,
  helpers,
  sameAs,
  minLength,
  email,
} from "@vuelidate/validators";
import { oneUpperCase } from "src/utilities/validators.js";
const columns = [
  {
    name: "userName",
    align: "left",
    label: "Username",
    field: (row) => row.user.userName,
    sortable: true,
  },
  {
    name: "email",
    align: "left",
    label: "Email",
    field: (row) => row.user.email,
    sortable: true,
  },
  {
    name: "firstName",
    label: "FirstName",
    align: "left",
    field: (row) => row.user.firstName,
    sortable: true,
  },
  {
    name: "lastName",
    align: "left",
    label: "LastName",
    field: (row) => row.user.lastName,
    sortable: true,
  },
  {
    name: "Roles",
    align: "left",
    label: "Roles",
    field: (row) => row.roles,
    sortable: true,
  },
  {
    name: "action",
    align: "left",
    label: "Actions",
    field: "action",
  },
];
const initialUser = {
  id: "",
  userName: "",
  firstName: "",
  lastName: "",
  email: "",
};

const initialchangepasswordUser = {
  id: "",
  newpassword: "",
  confirmpassword: "",
};

const user = ref({ ...initialUser });
const changepassworduser = ref({ ...initialchangepasswordUser });
const userswithroles = ref([
  {
    user: {
      id: "",
      userName: "",
      email: "",
      firstName: "",
      lastName: "",
    },
    roles: [],
  },
]);

const userroles = ref([]);
const edituser = ref(false);
const showassignedroles = ref(false);
const changepassword = ref(false);
const { exportTable } = useExportCsv(userswithroles, columns, "users");
const $q = useQuasar();
const store = useStore();
const filter = ref("");
const mode = ref("list");
const pagination = {
  rowsPerPage: 7,
};

getUsersWithRoles();

function getUsersWithRoles() {
  api.get("users/getuserswithroles").then((response) => {
    if (response && response.data && response.data.success) {
      userswithroles.value = response.data.data;
    }
  });
}

function UpdateUserByAdmin() {
  api
    .put("users/updateuserbyadmin", user.value)
    .then((response) => {
      if (response && response.data && response.data.success) {
        $q.notify({
          message: response.data.message,
          type: "positive",
        });
        getUsersWithRoles();
        resetUser();
      }
    })
    .catch((error) => {
      store.dispatch("alert/setErrors", error.response.data.Errors);
    });
}
function deleteUser(userid) {
  api
    .delete("users/" + userid)
    .then((response) => {
      if (response && response.data && response.data.success) {
        $q.notify({
          message: response.data.message,
          type: "positive",
        });
        getUsersWithRoles();
        resetUser();
      }
    })
    .catch((error) => {
      store.dispatch("alert/setErrors", error.response.data.Errors);
    });
}

function getUser(id) {
  if (id != "") {
    edituser.value = true;
    api.get("users/getUser/" + id).then((response) => {
      if (response && response.data && response.data.success) {
        user.value = response.data.data;
      }
    });
  }
}

function getAssignedRoles(id) {
  if (id != "") {
    showassignedroles.value = true;
    api.get("roles/getassignedroles/" + id).then((response) => {
      if (response && response.data && response.data.success) {
        userroles.value = response.data.data;
      }
    });
  }
}

function getUserForChangePassword(id) {
  if (id != "") {
    changepassword.value = true;
    changepassworduser.value.id = id;
  }
}

function passwordchangebyadmin() {
  api
    .put("users/passwordchangebyadmin", changepassworduser.value)
    .then((response) => {
      if (response && response.data && response.data.success) {
        $q.notify({
          message: response.data.message,
          type: "positive",
        });
        resetUser();
      }
    })
    .catch((error) => {
      store.dispatch("alert/setErrors", error.response.data.Errors);
    });
}

function AssignRole() {
  api
    .post("roles/roleassign", userroles.value)
    .then((response) => {
      if (response && response.data && response.data.success) {
        resetUser();
        $q.notify({
          message: response.data.message,
          type: "positive",
        });
        getUsersWithRoles();
      }
    })
    .catch((error) => {
      store.dispatch("alert/setErrors", error.response.data.Errors);
    });
}

const userrules = computed(() => {
  return {
    userName: {
      required: helpers.withMessage("Username is required", required),
      $autoDirty: true,
    },
    firstName: {
      required: helpers.withMessage("First Name is required", required),
      $autoDirty: true,
    },
    lastName: {
      required: helpers.withMessage("Last Name is required", required),
      $autoDirty: true,
    },
    email: {
      required: helpers.withMessage("Email  is required", required),
      email: helpers.withMessage("Email is not valid email address", email),
      $autoDirty: true,
    },
  };
});

const changepasswordrules = computed(() => {
  return {
    newpassword: {
      required: helpers.withMessage("New Password is required", required),
      minLength: minLength(9),
      oneUpperCase: helpers.withMessage(
        "Password need one must upper case",
        oneUpperCase
      ),
      $autoDirty: true,
    },
    confirmpassword: {
      required: helpers.withMessage("Confirm password is required", required),
      sameAs: helpers.withMessage(
        "Confirm password dont match with new password",
        sameAs(changepassworduser.value.newpassword)
      ),
      $autoDirty: true,
    },
  };
});

const $vchangepassword = useVuelidate(changepasswordrules, changepassworduser);
const $vuser = useVuelidate(userrules, user);

function resetUser() {
  user.value = initialUser;
  changepassworduser.value = initialchangepasswordUser;
  edituser.value = false;
  showassignedroles.value = false;
  changepassword.value = false;
  store.dispatch("alert/clearAlert");
  $vuser.value.$reset();
  $vchangepassword.value.$reset();
}
</script>

<style lang="scss" scoped></style>
