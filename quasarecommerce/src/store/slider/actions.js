import { api } from "boot/axios";

export function getSliders(context) {
  return api
    .get("sliders")
    .then((response) => {
      if (response && response.data.success) {
        context.commit("setSliders", response.data.data);
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

export function getSlider(context, id) {
  return api
    .get("sliders/" + id)
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

export function addSlider(context, slider) {
  return api
    .post("sliders", slider)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("addSlider", response.data.data);
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

export function updateSlider(context, slider) {
  return api
    .put("sliders", slider)
    .then((response) => {
      if (response && response.data.success) {
        context.dispatch("getSliders");
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

export function removeSlider(context, id) {
  return api
    .delete("sliders/" + id)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("removeSlider", id);
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
