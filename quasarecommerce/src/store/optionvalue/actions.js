import { api } from "boot/axios";

export function getOptionValues(context) {
  return api
    .get("optionvalues/option")
    .then((response) => {
     if(response && response.data.success){
      context.commit("setOptionValues", response.data.data);
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

export function getOptionValue(context, id) {
  return api
    .get("optionvalues/" + id)
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

export function addOptionValue(context, optionvalue) {
  return api
    .post("optionvalues", optionvalue)
    .then((response) => {
      if (response && response.data.success) {
        context.dispatch("alert/setMessage", response.data.message, {
          root: true,
        });
        context.dispatch("getOptionValues");
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

export function updateOptionValue(context, optionvalue) {
  return api
    .put("optionvalues", optionvalue)
    .then((response) => {
      if (response && response.data.success) {
        context.dispatch("alert/setMessage", response.data.message, {
          root: true,
        });
        context.dispatch("getOptionValues");
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

export function removeOptionValue(context, id) {
  return api
    .delete("optionvalues/" + id)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("removeOptionValue", id);
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
