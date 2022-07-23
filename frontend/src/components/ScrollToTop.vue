<script>
export default {
  data() {
    return {
      isButtonVisible: true,
    };
  },
  methods: {
    sctoll_to_top() {
      document.body.scrollTop = document.documentElement.scrollTop = 0;
    },
  },
  mounted() {
    document.addEventListener("scroll", (event) => {
      if (document.documentElement.scrollTop > 300) {
        this.isButtonVisible = false;
      } else {
        this.isButtonVisible = true;
      }
    });
  },
};
</script>


<template>
  <div
    class="scroll-button position-fixed bottom-0 end-0 me-4 mb-3"
    v-on:click="sctoll_to_top"
    v-bind:class="{ hiden: isButtonVisible }"
  >
    <div class="arrow mx-auto"></div>
  </div>
</template>

<style scoped>
.scroll-button {
  height: 60px;
  width: 60px;
  /* border: 2px solid; */
  border-radius: 100px;
  background-color: #eaeaea;
  cursor: pointer;
  opacity: 1;
  transition: all 0.5s ease;
}
.arrow {
  height: 15px;
  width: 15px;
  border-top: 3px solid #aaaaaa;
  border-left: 3px solid #aaaaaa;
  margin-top: 25px;
  transform: rotate(45deg);
}
.hiden {
  opacity: 0;
}
</style>