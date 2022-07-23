<script>
import { mapActions } from "vuex";
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
    ...mapActions(["DELETE_FROM_CART", "UPDATE_PRODUCT"]),
    delete_fom_cart() {
      this.DELETE_FROM_CART(this.productData.id);
    },
    countValidator() {
      let productQuantity = this.productData.quantity;
      if (1 > productQuantity || productQuantity > 10000) {
        this.productData.quantity = 1;
      }
      this.UPDATE_PRODUCT();
    },
    incriment() {
      if (this.productData.quantity < 10000) {
        this.productData.quantity++;
      }
      this.UPDATE_PRODUCT();
    },
    decriment() {
      if (this.productData.quantity > 1) {
        this.productData.quantity--;
      }
      this.UPDATE_PRODUCT();
    },
    redirect_to_product_ditails() {
      this.$router.push({ name: "ProductDitails", params: { id: this.productData.id } });
    }
  },
  created() {
    this.imgUrlFixed = "'" + apiConfig.host + this.productData.imageUrl + "'";
    if (this.productData["quantity"] == undefined) {
      this.productData["quantity"] = 1;
      this.UPDATE_PRODUCT();
    }
  },
};
</script>

<template>
  <div class="col">
    <div class="card mb-4 box-shadow p-4 d-flex">
      <div class="row g-0">
        <div class="col-md-2">
          <div
            class="product-img"
            v-bind:style="
              'background:url(' +
              imgUrlFixed +
              ') center no-repeat; background-size:contain;'
            "
          ></div>
        </div>
        <div class="col-md-7">
          <div class="product-title" v-on:click="redirect_to_product_ditails">
            {{ productData.title }}
          </div>
          <p class="short-description">{{ productData.shortDescription }}</p>
        </div>
        <div
          class="
            col-md-3
            d-flex
            flex-column
            align-items-center
            justify-content-center
          "
        >
          <div class="g-0 d-flex justify-content-center">
            <div v-on:click="decriment" class="me-2 counter-controller">-</div>
            <input
              class="counter-input"
              type="number"
              placeholder="Кол-во"
              v-model.number="productData.quantity"
              v-on:change="countValidator"
            />
            <div v-on:click="incriment" class="ms-2 counter-controller">+</div>
          </div>
          <div v-on:click="delete_fom_cart" class="remove-button">Удалить</div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.card {
  border-radius: 0px;
  border-top: 0px;
  border-left: 0px;
  border-right: 0px;
  border-bottom: 1px solid #e5e5e5;
}
.product-title {
  text-transform: uppercase;
  font-family: "Fira Mono", monospace;
  font-weight: 500;
  position: relative;
  font-size: 20px;
  color: #1B617C;
  cursor: pointer;
}
.product-img {
  height: 110px;
  width: 100%;
}
.remove-button {
  text-decoration-line: underline;
  cursor: pointer;
}
.counter-input {
  width: 80px;
}
.counter-controller {
  font-size: 20px;
  cursor: pointer;
}
.short-description {
  height: 70px;
  overflow: hidden;
  position: relative;
}
.short-description:after {
  content: "";
  text-align: right;
  position: absolute;
  bottom: 0;
  right: 0;
  width: 70%;
  height: 1.2em;
  background: linear-gradient(to right, rgba(255, 255, 255, 0), white 100%);
  pointer-events: none;
}
</style>