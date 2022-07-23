<script>
import { mapActions, mapGetters } from "vuex";
import apiConfig from "../../../../api/config";

export default {
  data() {
    return {};
  },
  props: {
    productData: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  computed: {},
  methods: {
    change_product() {
      this.$router.push({
        name: "AdminProduct",
        params: {
          id: this.productData.id,
        },
      });
    },
  },
  created() {
    this.imgUrlFixed = "'" + apiConfig.host + this.productData.imageUrl + "'";
  },
};
</script>

<template>
  <a class="d-flex list-group-item list-group-item-action list-button mb-1 ms-5" v-on:click="change_product">
    <div
      class="product-img"
      v-bind:style="
        'background:url(' +
        imgUrlFixed +
        ') center no-repeat; background-size:contain;'
      "
    ></div>
    <div class="w-100">
      <div class="d-flex w-100 justify-content-between">
        <h5 class="mb-1">{{ this.productData.title }}</h5>
        <small class="text-muted">{{
          this.productData.isActive ? "Активен" : "Не активен"
        }}</small>
      </div>
      <p class="mb-1 short-description">{{ this.productData.shortDescription }}</p>
      <div class="d-flex w-100 justify-content-between">
        <small class="mb-1 text-muted">{{ this.productData.packaging }}</small>
        <button
          v-on:click="change_product"
          class="btn btn-primary btn-sm d-none d-sm-block"
        >
          Изменить
        </button>
      </div>
    </div>
  </a>
</template>

<style scoped>
.product-img {
  height: 110px;
  width: 100%;
  max-width: 200px;
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

.list-button {
  border-top: 0px;
  border-left: 0px;
  border-right: 0px;
  border-bottom: 1px solid #f0f0f0;
  border-radius: 0px;
}
</style>