import { api } from "boot/axios";

export function getAddresses(context) {
  return api
    .get("addresses")
    .then((response) => {
      if (response && response.data.success) {
        context.commit("setAddresses", response.data.data);
        context.dispatch("alert/setMessage", response.data.message, {
          root: true,
        });
      }
    })
    .catch((error) => {
      if (error.response.data) {
        context.dispatch("alert/setErrors", error.response.data.Errors, {
          root: true,
        });
      }
    });
}

export function getAddress(context, id) {
  return api
    .get("addresses/" + id)
    .then((response) => {
      if (response && response.data.success) {
        return response.data.data;
      }
    })
    .catch((error) => {
      if (error.response.data) {
        context.dispatch("alert/setErrors", error.response.data.Errors, {
          root: true,
        });
      }
    });
}

export function addAddress(context, address) {
  return api
    .post("addresses", address)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("addAddress", response.data.data);
        context.dispatch("alert/setMessage", response.data.message, {
          root: true,
        });
        return response.data;
      }
    })
    .catch((error) => {
      if (error.response.data) {
        context.dispatch("alert/setErrors", error.response.data.Errors, {
          root: true,
        });
      }
    });
}

export function updateAddress(context, address) {
  return api
    .put("addresses", address)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("updateAddress", address);
        context.dispatch("alert/setMessage", response.data.message, {
          root: true,
        });
        return response.data;
      }
    })
    .catch((error) => {
      if (error.response.data) {
        context.dispatch("alert/setErrors", error.response.data.Errors, {
          root: true,
        });
      }
    });
}

export function removeAddress(context, id) {
  return api
    .delete("addresses/" + id)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("removeAddress", id);
        context.dispatch("alert/setMessage", response.data.message, {
          root: true,
        });
        return response.data;
      }
    })
    .catch((error) => {
      if (error.response.data) {
        context.dispatch("alert/setErrors", error.response.data.Errors, {
          root: true,
        });
      }
    });
}
