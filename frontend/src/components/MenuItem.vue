<script>
import { mapActions, mapGetters } from 'vuex'
import CartIcon from './icons/CartIcon.vue'
import CompanyLogoIcon from './icons/CompanyLogoIcon.vue'

export default {
    data() {
      return {
        cartQuantityShow: true,
      }
    },
    components: { 
      CartIcon,
      CompanyLogoIcon,
    },
    computed: {
        ...mapGetters([
            'PRODUCTS_IN_CART',
        ])
    }
}
</script>

<template>
<nav class="fixed-top navbar navbar-expand-lg navbar-dark bg-dark">
  <div class="container">
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarTogglerDemo03" aria-controls="navbarTogglerDemo03" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <a class="navbar-brand" href="/">
      <CompanyLogoIcon />
    </a>
    <div class="collapse navbar-collapse" id="navbarTogglerDemo03">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a class="nav-link active" aria-current="page" href="/#catalog">Каталог</a>
        </li>
        <li class="nav-item">
          <a class="nav-link active" href="/aboutus">О нас</a>
        </li>
        <li class="nav-item">
          <a class="nav-link active" href="/contacts" tabindex="-1">Контакты</a>
        </li>
      </ul>
      <div class="d-flex align-items-center">
        <RouterLink to="/cart">
          <div>
            <CartIcon />
            <span class="badge rounded-pill bg-danger translate-middle disactivae" :class="{ active: this.PRODUCTS_IN_CART.length == 0 ? false : true }">
              {{ PRODUCTS_IN_CART.length }}
            </span>
          </div>
        </RouterLink>
      </div>
      <!-- <form class="d-flex">
        <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
        <button class="btn btn-outline-success" type="submit">Search</button>
      </form> -->
    </div>
  </div>
</nav>
</template>

<style scoped>
.disactivae {
  display: none;
}
.active {
  display: inline-block;
} 
</style>