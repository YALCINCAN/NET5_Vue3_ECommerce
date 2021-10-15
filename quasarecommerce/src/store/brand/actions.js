import { api } from "boot/axios";

export function getBrands(context) {
  return api
    .get("brands")
    .then((response) => {
      if (response && response.data.success) {
        context.commit("setBrands", response.data.data);
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

export function getBrand(context, id) {
  return api
    .get("brands/" + id)
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

export function addBrand(context, brand) {
  return api
    .post("brands", brand)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("addBrand", response.data.data);
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

export function updateBrand(context, brand) {
  return api
    .put("brands", brand)
    .then((response) => {
      if (response && response.data.success) {
        context.dispatch("getBrands");
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

export function removeBrand(context, id) {
  return api
    .delete("brands/" + id)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("removeBrand", id);
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
