<script>
import { mapActions, mapGetters } from "vuex";

export default {
  computed: { ...mapGetters(["ADMINTOKEN"]) },
  methods: {
    redirect_category_panel() {
      this.$router.push({ name: "CategoryPanel" });
    },
    redirect_product_panel() {
      this.$router.push({ name: "ProductPanel" });
    },
    redirect_to_site() {
      this.$router.push({ name: "Home" })
    },
  },
  created() {
    this.ADMINTOKEN == "" ? this.$router.push({ name: "AdminLogIn" }) : null;
  },
};
</script>

<template>
  <section>
    <div class="row my-5">
      <div class="col">
        <div class="container d-flex justify-content-evenly">
          <button v-on:click="redirect_to_site" class="btn btn-primary">
            Вернуться на сайт
          </button>
          <button v-on:click="redirect_category_panel" class="btn btn-primary">
            Панель категорий
          </button>
          <button v-on:click="redirect_product_panel" class="btn btn-primary">
            Панель товаров
          </button>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
</style>