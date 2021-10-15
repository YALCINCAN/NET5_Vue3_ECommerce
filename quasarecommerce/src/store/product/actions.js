import { api } from "boot/axios";

export  function addProduct(context, product) {
  return api
    .post("products", product)
    .then((response) => {
     if(response && response.data.success){
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

export  function updateProduct(context, product) {
  return api
    .put("products", product)
    .then((response) => {
      if(response && response.data.success){
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



