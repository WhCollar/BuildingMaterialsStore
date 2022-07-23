<script>
import { mapActions, mapGetters } from "vuex";

export default {
  data() {
    return {
      isSelected: false,
    };
  },
  props: {
    title: String,
    id: Number,
  },
  methods: {
    ...mapActions(["SET_UNDERCATEGORIES_ID"]),
    underCategoryActivator() {
      this.isSelected = !this.isSelected
      let data = {
        isSelected: this.isSelected,
        id: this.id,
      };
      this.SET_UNDERCATEGORIES_ID(data);
    },
  },
};
</script>

<template>
  <div class="col-md-4">
    <div
      v-on:click="underCategoryActivator"
      class="card mb-4 box-shadow text-center button-text custom-card"
      :class="{ 'scoped-card': this.isSelected }"
    >
      {{ title }}
    </div>
  </div>
</template>

<style scoped>
.scoped-card {
  background-color: rgba(220, 220, 220, 1);
}

.custom-card {
  border-radius: 0px;
  border-color: #92CAE0;
}
</style>