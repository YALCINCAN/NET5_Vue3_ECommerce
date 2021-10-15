<template>
  <q-list class="bg-primary text-white">
    <q-item clickable v-ripple to="/profile" exact-active-class="active-class">
      <q-item-section>Profile</q-item-section>
    </q-item>
    <q-item clickable v-ripple to="/changepassword" exact-active-class="active-class">
      <q-item-section>Change Password</q-item-section>
    </q-item>
    <q-item clickable v-ripple to="/address" exact-active-class="active-class">
      <q-item-section>My Address</q-item-section>
    </q-item>
    <q-item clickable v-ripple to="/orders" exact-active-class="active-class">
      <q-item-section>My Orders</q-item-section>
    </q-item>
  </q-list>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style lang="css" scoped>
.active-class {
  color: yellow;
}
</style>
