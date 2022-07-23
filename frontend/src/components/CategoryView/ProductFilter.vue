<script>
import { mapActions, mapGetters } from "vuex";
import UndercategoryListElement from "./ProductFilter/UndercategoryListElement.vue";

export default {
  data() {
    return {};
  },
  props: {
    categorySlug: String,
  },
  computed: {
    ...mapGetters(["UNDERCATEGORIES"]),
  },
  methods: {
    ...mapActions(["GET_UNDERCATEGORIES_FROM_API"]),
  },
  mounted() {
    this.GET_UNDERCATEGORIES_FROM_API(this.$route.params.slug);
  },
  components: { UndercategoryListElement },
};
</script>

<template>
  <div class="row my-5">
    <UndercategoryListElement
      v-for="underCategory in UNDERCATEGORIES"
      :key="underCategory.id"
      :id="underCategory.id"
      :title="underCategory.name"
    />
  </div>
</template>

<style>
</style>