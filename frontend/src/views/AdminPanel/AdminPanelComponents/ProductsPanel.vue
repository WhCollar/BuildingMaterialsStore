<script>
import { mapActions, mapGetters } from "vuex";
import SortedProductList from "./SortedProductList.vue";
import ScrollToTop from "../../../components/ScrollToTop.vue";

export default {
  data() {
    return {};
  },
  components: { SortedProductList, ScrollToTop },
  computed: {
  },
  watch: {},
  methods: {
    add_new_product() {
      this.$router.push({ name: "AdminProduct", params: { id: 0 } });
    },
    back_redirect() {
      this.$router.push({ name: "AdminPanel" });
    },
  },
  created() {
  },
};
</script>

<template>
  <div class="container mt-3">
    <div class="w-100 d-flex justify-content-between">
      <button v-on:click="back_redirect" class="btn btn-primary mb-3">
        Назад
      </button>
      <button v-on:click="add_new_product" class="btn btn-primary mb-3">
        Добавить товар
      </button>
    </div>
    <SortedProductList />
    <ScrollToTop />
  </div>
</template>

<style scoped>
</style>