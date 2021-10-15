import { api } from "boot/axios";

export function getOptions(context) {
  return api
    .get("options")
    .then((response) => {
     if(response && response.data.success){
      context.commit("setOptions", response.data.data);
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

export function getOption(context, id) {
  return api
    .get("options/" + id)
    .then((response) => {
      if(response && response.data.success){
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

export function addOption(context, option) {
  return api
    .post("options", option)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("addOption", response.data.data);
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

export function updateOption(context, option) {
  return api
    .put("options", option)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("updateOption", option);
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

export function removeOption(context, id) {
  return api
    .delete("options/" + id)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("removeOption", id);
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
