<script>
import { mapActions, mapGetters } from "vuex";
import apiConfig from "../../api/config";


export default {
  data() {
    return {
      imgUrlFixed: "",
    };
  },
  props: {
    productData: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  methods: {
    ...mapActions(["ADD_TO_CART"]),
    add_to_cart() {
      this.ADD_TO_CART(this.productData);
    },
    redirect_to_product_ditails() {
      this.$router.push({ name: "ProductDitails", params: { id: this.productData.id } });
    }
  },
  created() {
    this.imgUrlFixed = "'" + apiConfig.host + this.productData.imageUrl + "'";
  },
};
</script>

<template>
  <div class="col-md-4">
    <div class="card mb-4 box-shadow p-4">
      <div
        class="
          hover-preload
          d-flex
          flex-column
          align-items-center
          justify-content-end
        "
      >
        <div class="mb-3">
          <button v-on:click="add_to_cart" class="btn btn-dark">
            Добавить в корзину
          </button>
        </div>
        <div class="mb-3">
          <button v-on:click="redirect_to_product_ditails" class="btn btn-dark">Подробнее</button>
        </div>
      </div>
      <div class="product-title">
        {{ productData.title }}
      </div>
      <p class="shortDescription">{{ productData.shortDescription }}</p>
      <div
        class="product-img"
        v-bind:style="
          'background:url(' +
          this.imgUrlFixed +
          ') center no-repeat; background-size:contain;'
        "
      ></div>
      <p class="packaging-info mt-4">Фасовка: {{ productData.packaging }}</p>
    </div>
  </div>
</template>

<style scoped>
.card {
  border: 0px;
}
.product-title {
  text-transform: uppercase;
  font-weight: 600;
  position: relative;
  font-size: 18px;
}
.shortDescription {
  height: 75px;
  overflow: hidden;
}
.product-img {
  height: 180px;
  width: 100%;
}
.hover-preload {
  width: 100%;
  height: 100%;
  background: rgba(220, 220, 220, 0);
  opacity: 50%;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  position: absolute;
  z-index: 1;
  opacity: 0;
}
.hover-preload:hover {
  background: rgba(220, 220, 220, 0.5);
  opacity: 1;
}
.packaging-info {
  margin-bottom: -15px;
}
</style>