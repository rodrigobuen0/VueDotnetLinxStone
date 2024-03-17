import { createApp } from "vue";
import App from "./App.vue";
import axios from "axios";
import { createRouter, createWebHistory } from "vue-router";
import ProductsList from "./components/pages/Products";
import ProductCreate from "./components/pages/ProductCreate.vue";
import ProductEdit from "./components/pages/ProductEdit.vue";
import PrimeVue from "primevue/config";
import VueBarcode from "@chenfengyuan/vue-barcode";

import "bootstrap/dist/css/bootstrap.css";
import "./assets/style.css";
import "@fortawesome/fontawesome-free/css/all.css";

axios.defaults.baseURL = "https://localhost:7217";
const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: "/", component: ProductsList },
    { path: "/create", component: ProductCreate },
    { path: "/edit/:id", component: ProductEdit },
  ],
});
const app = createApp(App);

app.use(router);
app.component(VueBarcode.name, VueBarcode);
app.use(PrimeVue, { unstyled: true });

app.mount("#app");
