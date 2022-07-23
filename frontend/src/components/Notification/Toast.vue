<script>
import { mapActions, mapGetters } from "vuex";
import { Toast } from "bootstrap";

export default {
  props: {
    id: String,
    toastData: {
      type: Object,
      default() {
        return {
          imgUrl: String,
          dataTime: Object,
          message: String,
        };
      },
    },
  },
  computed: {
    ...mapGetters(["OPTIONS_FOR_TOASTS"]),
  },
  methods: {
    ...mapActions(["ADD_INITIALIZED_TOAST"]),
  },
  mounted() {
    let toast = document.getElementById(this.id);
    let newToast = new Toast(toast, this.OPTIONS_FOR_TOASTS);
    this.ADD_INITIALIZED_TOAST(newToast);
  },
};
</script>

<template>
  <div v-bind:id="id" class="toast">
    <div class="toast-header">
      <!-- <img v-bind:src="toastData.imgUrl" class="rounded me-2" alt="..." /> -->
      <strong class="me-auto">ЦентрСтрой</strong>
      <small>{{ toastData.dataTime }}</small>
    </div>
    <div class="toast-body">
      {{ toastData.message }}
    </div>
  </div>
</template>

<style>
</style>