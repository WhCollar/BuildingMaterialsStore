import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import CategoryView from '../views/CategoryView.vue'
import CartView from '../views/CartView.vue'
import Chekout from '../views/Chekout.vue'
import ContactsView from '../views/ContactsView.vue'
import AboutUsView from '../views/AboutUsView.vue'
import AdminLogIn from '../views/AdminPanel/AdminLogIn.vue'
import AdminPanel from '../views/AdminPanel/AdminPanel.vue'
import AdminProduct from '../views/AdminPanel/AdminProduct.vue'
import CategoryPanel from '../views/AdminPanel/AdminPanelComponents/CategoryPanel.vue'
import ProductsPanel from '../views/AdminPanel/AdminPanelComponents/ProductsPanel.vue'
import PageNotFound from '../views/PageNotFound.vue'
import ProductDitailView from '../views/ProductDitailView.vue'
import AdminCategory from '../views/AdminPanel/AdminCategory.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: HomeView
    },
    {
      path: '/category/:slug',
      name: 'Category',
      component: CategoryView,
      props: true
    },
    {
      path: '/category/product-ditails/:id',
      name: 'ProductDitails',
      component: ProductDitailView,
      props: true,
    },
    {
      path: '/aboutus',
      name: 'About',
      component: AboutUsView
    },
    {
      path: '/contacts',
      name: 'Contacts',
      component: ContactsView
    },
    {
      path: '/cart',
      name: 'Cart',
      component: CartView
    },
    {
      path: '/chekout',
      name: 'Chekout',
      component: Chekout
    },
    {
      path: '/admin',
      name: 'AdminLogIn',
      component: AdminLogIn,
    },
    {
      path: '/admin/AdminPanel',
      name: 'AdminPanel',
      component: AdminPanel,
    },
    {
      path: '/admin/AdminPanel/category',
      name: 'CategoryPanel',
      component: CategoryPanel,
    },
    {
      path: '/admin/AdminPanel/products',
      name: 'ProductPanel',
      component: ProductsPanel,
    },
    {
      path: '/admin/AdminPanel/product/:id',
      name: 'AdminProduct',
      component: AdminProduct,
      props: true,
    },
    {
      path: '/admin/AdminPanel/category/:id',
      name: 'AdminCategory',
      component: AdminCategory,
      props: true,
    },
    {
      path: '/:pathMatch(.*)*',
      name: "PageNotFound",
      component: PageNotFound,
    }
  ]
})

export default router
