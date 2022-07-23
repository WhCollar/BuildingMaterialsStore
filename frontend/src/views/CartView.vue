<script>
import { mapActions, mapGetters } from "vuex";
import ProductItem from "../components/CartView/ProductItem.vue";

export default {
  components: {
    ProductItem,
  },
  computed: {
    ...mapGetters(["PRODUCTS_IN_CART"]),
  },
};
</script>

<template>
  <section class="cart-section">
    <div class="container cart-view">
      <div class="row my-5">
        <div class="col">
          <h2 class="text-uppercase ms-4">Корзина</h2>
        </div>
      </div>
      <div class="row row-cols-1 my-5">
        <h6 v-if="PRODUCTS_IN_CART.length == 0">Тут пусто, добавьте товар</h6>
        <div v-else>
          <ProductItem
            v-for="product in PRODUCTS_IN_CART"
            :key="product.id"
            :productData="product"
          />
          <button v-on:click="$router.push('/chekout')" class="btn btn-dark float-end">
            Оформить заказ
          </button>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
.cart-view {
  min-height: 60vh;
  background: white;
}
</style>