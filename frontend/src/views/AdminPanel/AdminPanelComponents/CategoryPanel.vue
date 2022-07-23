<script>
import { mapActions, mapGetters } from "vuex";
import CategoryListItem from "./CategoryPanelComponents/CategoryListItem.vue";

export default {
  data() {
    return {};
  },
  components: { CategoryListItem },
  computed: { ...mapGetters(["ADMINS_CATEGORY"]) },
  methods: {
    ...mapActions(["GET_ADMIN_CATEGORIES_FROM_API"]),
    add_new_category() {
      this.$router.push({ name: "AdminCategory", params: { id: 0 } });
    },
    back_redirect() {
      this.$router.push({ name: "AdminPanel" });
    },
  },
  created() {
    this.GET_ADMIN_CATEGORIES_FROM_API();
  },
};
</script>

<template>
  <div class="container mt-3">
    <div class="w-100 d-flex justify-content-between">
      <button v-on:click="back_redirect" class="btn btn-primary mb-3">
        Назад
      </button>
      <button v-on:click="add_new_category" class="btn btn-primary mb-3">
        Добавить категорию
      </button>
    </div>
    <div class="list-group">
      <CategoryListItem
        v-for="listItem in this.ADMINS_CATEGORY"
        :key="listItem.id"
        :categoryData="listItem"
      />
    </div>
  </div>
</template>

<style scoped>

</style>