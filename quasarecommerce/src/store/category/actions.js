import { api } from "boot/axios";

export function getCategories(context) {
  return api
    .get("categories")
    .then((response) => {
      if (response && response.data.success) {
        context.commit("setCategories", response.data.data);
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

export function getCategoryWithOptions(context, id) {
  return api
    .get("categories/" + id + "/options")
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

export function addCategory(context, category) {
  return api
    .post("categories", category)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("addCategory", response.data.data);
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

export function updateCategory(context, category) {
  return api
    .put("categories", category)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("updateCategory", category);
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

export function removeCategory(context, id) {
  return api
    .delete("categories/" + id)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("removeCategory", id);
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
