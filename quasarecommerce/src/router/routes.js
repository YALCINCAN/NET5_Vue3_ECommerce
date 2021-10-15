const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      { path: "/", component: () => import("src/pages/Home.vue") },
      { path: "/products", component: () => import("src/pages/Products.vue") },
      {
        path: "/:slug",
        name: "ProductDetail",
        component: () => import("src/pages/ProductDetail.vue"),
      },
      { path: "/basket", component: () => import("src/pages/Basket.vue") },
      {
        path: "/profile",
        component: () => import("src/pages/account/Profile.vue"),
        meta: { requiresLogin: true },
      },
      {
        path: "/changepassword",
        component: () => import("src/pages/account/ChangePassword.vue"),
        meta: { requiresLogin: true },
      },
      { path: "/login", component: () => import("src/pages/auth/Login.vue") },
      {
        path: "/register",
        component: () => import("src/pages/auth/Register.vue"),
      },
      {
        path: "/forgotpassword",
        component: () => import("src/pages/account/ForgotPassword.vue"),
      },
      {
        path: "/address",
        component: () => import("src/pages/account/Address.vue"),
        meta: { requiresLogin: true },
      },
      {
        path: "/checkout",
        component: () => import("src/pages/Checkout.vue"),
        meta: { requiresLogin: true },
      },
      {
        path: "/orders",
        name: "Orders",
        component: () => import("src/pages/account/Orders.vue"),
        meta: { requiresLogin: true },
      },
      {
        path: "/order/:ordernumber",
        name: "OrderDetail",
        component: () => import("pages/account/OrderDetail.vue"),
        meta: { requiresLogin: true },
      },
      {
        path: "/confirmemail/:userId/:token",
        name: "ConfirmEmail",
        component: () => import("pages/auth/ConfirmEmail.vue"),
      },
      {
        path: "/resetpassword/:email/:token",
        name: "ResetPassword",
        component: () => import("pages/account/ResetPassword.vue"),
      },
      {
        path: "/accessdenied",
        name: "AccessDenied",
        component: () => import("pages/AccessDenied.vue"),
      },
      {
        path: "/notfound",
        name: "NotFound",
        component: () => import("pages/NotFound.vue"),
      },
      { path: '/:pathMatch(.*)*', name: 'not-found', component:()=>import("pages/NotFound.vue")},
    ],
  },
  {
    path: "/dashboard",
    component: () => import("layouts/DashboardLayout.vue"),
    children: [
      {
        path: "/dashboard/sliders",
        name: "DashboardSliders",
        component: () => import("pages/dashboard/Sliders.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/brands",
        name: "DashboardBrands",
        component: () => import("pages/dashboard/Brands.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/options",
        name: "DashboardOptions",
        component: () => import("pages/dashboard/Options.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/optionvalues",
        name: "DashboardOptionValues",
        component: () => import("pages/dashboard/OptionValues.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/categories",
        name: "DashboardCategories",
        component: () => import("pages/dashboard/Categories.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/products",
        name: "DashboardProducts",
        component: () => import("pages/dashboard/Products.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/orders",
        name: "DashboardOrders",
        component: () => import("pages/dashboard/Orders.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/products/add",
        name: "DashboardProductAdd",
        component: () => import("pages/dashboard/AddProduct.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/products/update/:id",
        name: "DashboardProductUpdate",
        component: () => import("pages/dashboard/UpdateProduct.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/reviews",
        name: "DashboardReviews",
        component: () => import("pages/dashboard/Reviews.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/users",
        name: "DashboardUsers",
        component: () => import("pages/dashboard/Users.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/dashboard/roles",
        name: "DashboardRoles",
        component: () => import("pages/dashboard/Roles.vue"),
        meta: { requiresAuth: true },
      },
    ],
  },
];

export default routes;
