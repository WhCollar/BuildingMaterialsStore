<script>
import { mapGetters } from "vuex";
import Toast from "./Toast.vue";

export default {
  components: {
    Toast,
  },
  computed: {
    ...mapGetters(["TOASTS"]),
  },
};
</script>

<template>
  <div
    class="toast-container position-fixed bottom-0 end-0 me-4 mb-3"
    id="toastPlacement"
  >
    <Toast
      v-for="(toast, index) in TOASTS"
      :key="index"
      :toastData="toast"
      :id="'toast' + index"
    />
  </div>
</template>

<style>
</style>