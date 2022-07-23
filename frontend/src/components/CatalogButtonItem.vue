<script>
export default {
  data() {
    return {};
  },
  props: {
    slug: String,
    title: String,
  },
};
</script>

<template>
  <div class="col-md-4">
    <div
      v-on:click="this.$router.push('/category/' + slug)"
      class="card mb-4 box-shadow text-center button-text custom-card"
    >
      {{ title }}
    </div>
  </div>
</template>

<style scoped>

.custom-card {
  border-radius: 0px;
  border-color: #92CAE0;
}
</style>