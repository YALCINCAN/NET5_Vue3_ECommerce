import { api } from "boot/axios";

export  function getBasket(context) {
  return  api
    .get("baskets")
    .then((response) => {
     if(response && response.data.success){
      context.commit("setBasket", response.data.data);
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

export  function addToBasket(context, basketitem) {
  return api
    .post("baskets", basketitem)
    .then((response) => {
      if (response && response.data.success) {
        context.dispatch("getBasket");
        context.dispatch("alert/setMessage", response.data.message, {
          root: true,
        });
        return response.data
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

export  function updateBasket(context, basketitem) {
  return  api
    .put("baskets", basketitem)
    .then((response) => {
      if (response && response.data.success) {
        context.dispatch("getBasket");
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

export  function removeFromBasket(context, productid) {
  return  api
    .delete("baskets/" + productid)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("removeFromBasket", productid);
        context.dispatch("getBasket");
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

export  function clearBasket(context) {
  return api
    .delete("baskets")
    .then((response) => {
      if (response && response.data.success) {
        context.commit("clearBasket");
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
