<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Reviews"
        :rows="reviews"
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
                @click="removeReview(props.row.id)"
                color="red"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
         <template v-slot:body-cell-ratingvalue="props">
          <q-td :props="props">
            <q-rating
              readonly
              v-model="props.row.ratingValue"
              color="blue"
              :max="5"
              size="20px"
            />
          </q-td>
        </template>
      </q-table>
    </q-card>
  </q-page>
</template>

<script setup>
import { ref, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useExportCsv } from "src/composables/useExportCsv";
import { date } from "quasar";
const columns = [
  {
    name: "reviewDate",
    align: "left",
    label: "Review Date",
    field: (row) => formatDate(row.reviewDate),
    sortable: true,
  },
  {
    name: "description",
    align: "left",
    label: "Description",
    field: "description",
    sortable: true,
  },
  {
    name: "firstname",
    align: "left",
    label: "First Name",
    field: (row) => row.user.firstName,
    sortable: true,
  },
  {
    name: "lastName",
    align: "left",
    label: "Last Name",
    field: (row) => row.user.lastName,
    sortable: true,
  },
  {
    name: "ratingvalue",
    align: "left",
    label: "Rating Value",
    field: "ratingValue",
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

const store = useStore();
const $q = useQuasar();
store.dispatch("review/getReviews");

const reviews = computed(() => {
  return store.getters["review/getReviews"];
});

const { exportTable } = useExportCsv(reviews, columns, "reviews");

function removeReview(id) {
  store.dispatch("review/removeReview", id).then((response) => {
    if (response && response.success) {
      $q.notify({
        message: response.message,
        type: "positive",
      });
    }
  });
}

function formatDate(reviewdate) {
  return date.formatDate(reviewdate, "DD/MM/YYYY HH:mm");
}
</script>
<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
