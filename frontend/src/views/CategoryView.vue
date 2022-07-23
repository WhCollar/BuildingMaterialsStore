<script>
import { mapActions, mapGetters } from "vuex";
import ProductItem from "../components/CategoryView/ProductItem.vue";
import ProductFilter from "../components/CategoryView/ProductFilter.vue";
import OverView from "../components/OverView.vue";

export default {
  data() {
    return {
      title: "",
    };
  },
  components: {
    ProductItem,
    ProductFilter,
    OverView,
  },
  computed: {
    ...mapGetters(["PRODUCTS"]),
  },
  methods: {
    ...mapActions(["GET_PRODUCTS_FROM_API"]),
  },
  created() {
    this.GET_PRODUCTS_FROM_API(this.$route.params.slug);
  },
};
</script>

<template>
  <section class="products">
    <OverView :slug="this.$route.params.slug" />
    <div class="container">
      <!-- <div class="row my-5">
        <div class="col">
          <h2>
            {{ this.$route.params.slug }}
          </h2>
        </div>
      </div> -->
      <ProductFilter :categorySlug="this.$route.params.slug" />
      <div class="row my-5">
        <ProductItem
          v-for="product in PRODUCTS"
          :key="product.id"
          :productData="product"
        />
      </div>
    </div>
  </section>
</template>

<style scoped>
.products {
  background-color: #f8f8f8;
}
</style>