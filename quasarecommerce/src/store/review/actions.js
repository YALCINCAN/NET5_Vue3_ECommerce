import { api } from "boot/axios";

export function getReviews(context){
  return api.get("reviews").then(response=>{
    if(response && response.data.success){
      context.commit("setReviews",response.data.data)
    }
  })
}

export function addReview(context, review) {
  return api
    .post("reviews", review)
    .then((response) => {
      if (response && response.data.success) {
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
        throw error.response.data.Errors;
      }
    });
}

export function removeReview(context, id) {
  return api
    .delete("reviews/" + id)
    .then((response) => {
      if (response && response.data.success) {
        context.commit("removeReview", id);
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
