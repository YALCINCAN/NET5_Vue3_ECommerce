import { api } from "boot/axios";

export function updateProfile(context, userdata) {
  return api
    .put("users", userdata)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("auth/updateProfile", userdata, { root: true });
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

export function changePassword(context, changepassworddata) {
  return api
    .put("users/changepassword", changepassworddata)
    .then((response) => {
      if (response && response.data.success) {
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

export function forgotPassword(context, forgotpassworddata) {
  return api
    .post("users/forgotpassword", forgotpassworddata)
    .then((response) => {
      if (response && response.data.success) {
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

export function resetPassword(context, resetpassworddata) {
  return api
    .post("users/resetpassword", resetpassworddata)
    .then((response) => {
      if (response && response.data.success) {
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
