<script>
import { mapGetters } from "vuex";
import ProductListItem from "./ProductsPanelComponents/ProductListItem.vue";
import axios from "axios";
import apiConfig from "../../../api/config";

export default {
  data() {
    return {
      sortedCatalog: [],
    };
  },
  computed: {
    ...mapGetters(["ADMINTOKEN"]),
  },
  components: { ProductListItem },
  mounted() {
    axios
      .get(apiConfig.host + "AdminPanel/Products?fillProducts=true", {
        headers: {
          Authorization: `Bearer ${this.ADMINTOKEN.token}`,
        },
      })
      .then((response) => {
        this.sortedCatalog = response.data;
      });
  },
};
</script>

<template>
  <ul class="list-unstyled py-3">
    <li class="mb-1" v-for="(el, i) in this.sortedCatalog" :key="i">
      <button
        class="btn d-inline-flex align-items-center w-100 mb-1 list-button"
        data-bs-toggle="collapse"
        :data-bs-target="'#undercategories-colaps' + i"
      >
        {{ el.name }}
      </button>
      <div class="collapse" :id="'undercategories-colaps' + i">
        <ul class="list-unstyled fw-normal pb-1 small">
          <li class="mb-1" v-for="(subEl, j) in el.subCategories" :key="j">
            <div class="d-grid gap-2">
              <button
                class="
                  btn
                  d-inline-flex
                  align-items-center
                  ms-5
                  mb-1
                  list-button
                "
                data-bs-toggle="collapse"
                :data-bs-target="'#products-colaps' + j"
              >
                {{ subEl.name }}
              </button>
              <div class="collapse ms-5" :id="'products-colaps' + j">
                <div class="list-group">
                  <div v-for="product in subEl.products" :key="product.id">
                    <ProductListItem :productData="product" />
                  </div>
                </div>
              </div>
            </div>
          </li>
        </ul>
      </div>
    </li>
  </ul>
</template>

<style scoped>
.list-button {
  border-bottom: 1px solid #f0f0f0;
  border-radius: 0px;
}
.list-button:focus {
  box-shadow: 0 0 0 0.25rem rgb(255, 255, 255, 0);
}
</style>