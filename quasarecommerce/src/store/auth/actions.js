import { api } from "boot/axios";

export function login(context, credentials) {
  return api
    .post("auth/login", credentials)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("login", response.data.data);
        context.dispatch("basket/getBasket", "", { root: true });
        context.dispatch("getAuthenticatedUserWithRoles");
        return response;
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

export function register(context, credentials) {
  return api
    .post("auth/register", credentials)
    .then((response) => {
      context.dispatch("alert/setMessage", response.data.message, {
        root: true,
      });
    })
    .catch((error) => {
      if (error.response.data) {
        context.dispatch("alert/setErrors", error.response.data.Errors, {
          root: true,
        });
      }
    });
}

export function confirmemail(context, confirmemaildata) {
  return api
    .post("auth/confirmemail", confirmemaildata)
    .then((response) => {
      context.dispatch("alert/setMessage", response.data.message, {
        root: true,
      });
    })
    .catch((error) => {
      if (error.response.data) {
        context.dispatch("alert/setErrors", error.response.data.Errors, {
          root: true,
        });
      }
    });
}

export function getAuthenticatedUserWithRoles(context) {
  return api.get("auth").then((response) => {
    if (response && response.data.success) {
      context.commit("setAuthenticatedUserWithRoles", response.data.data);
    }
  });
}

export function logout(context) {
  context.commit("logout");
}
