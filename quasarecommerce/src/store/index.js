import { store } from "quasar/wrappers";
import { createStore } from "vuex";

import auth from "./auth";
import alert from "./alert";
import user from "./user";
import slider from "./slider";
import brand from "./brand";
import address from "./address";
import option from "./option";
import optionvalue from "./optionvalue";
import category from "./category";
import product from "./product";
import basket from "./basket";
import review from "./review";
/*
 * If not building with SSR mode, you can
 * directly export the Store instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Store instance.
 */

export default store(function (/* { ssrContext } */) {
  const Store = createStore({
    modules: {
      auth,
      alert,
      user,
      slider,
      brand,
      address,
      option,
      optionvalue,
      category,
      product,
      basket,
      review
    },

    // enable strict mode (adds overhead!)
    // for dev mode and --debug builds only
    strict: process.env.DEBUGGING,
  });

  return Store;
});
