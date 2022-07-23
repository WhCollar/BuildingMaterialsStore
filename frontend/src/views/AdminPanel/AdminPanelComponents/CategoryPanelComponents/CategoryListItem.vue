<script>
import { mapActions, mapGetters } from "vuex";

export default {
  data() {
    return {};
  },
  props: {
    categoryData: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  computed: {},
  methods: {
    change_category() {
      this.$router.push({
        name: "AdminCategory",
        params: {
          id: this.categoryData.id,
        },
      });
    },
  },
};
</script>

<template>
  <label class="list-group-item d-flex justify-content-between list-button">
    {{ this.categoryData.name }}
    <button v-on:click="change_category" class="btn btn-primary btn-sm">
      Изменить
    </button>
  </label>
</template>

<style scoped>
.list-button {
  border-top: 0px;
  border-left: 0px;
  border-right: 0px;
  border-bottom: 1px solid #f0f0f0;
  border-radius: 0px;
  cursor: pointer;
}
</style>