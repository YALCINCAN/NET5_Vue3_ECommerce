import { route } from 'quasar/wrappers'
import { createRouter, createMemoryHistory, createWebHistory, createWebHashHistory } from 'vue-router'
import routes from './routes'
import qs from "qs"

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Router instance.
 */

export default route(function (/* { store, ssrContext } */) {
  const createHistory = process.env.SERVER
    ? createMemoryHistory
    : (process.env.VUE_ROUTER_MODE === 'history' ? createWebHistory : createWebHashHistory)

  const Router = createRouter({
    scrollBehavior: () => ({ left: 0, top: 0 }),
    stringifyQuery: query => {
      let result = qs.stringify(query, { format: 'RFC1738' })
      return result ? (result) : ''
    },
    routes,

    // Leave this as is and make changes in quasar.conf.js instead!
    // quasar.conf.js -> build -> vueRouterMode
    // quasar.conf.js -> build -> publicPath
    
    history: createHistory(process.env.MODE === 'ssr' ? void 0 : process.env.VUE_ROUTER_BASE)
  })
  Router.beforeEach((to, from) => {
    if (to.meta.requiresAuth) {
      var token = window.localStorage.getItem("token");
      if (token != null) {
        var decodedtoken = JSON.parse(atob(token.split(".")[1]));
        var role =
          decodedtoken[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ];
        if (role != "Admin") {
          return{
            path:'/accessdenied'
          }
        }
      } else {
        return{
          path:'/login'
        }
      }
    } 

    if (to.meta.requiresLogin) {
      var token = window.localStorage.getItem("token");
      if (token == null) {
        return{
          path:'/login'
        }
      }
    }
  });
  return Router
})
