<script>
export default {
  created() {
    
  },
};
</script>

<template>
  <div id="preloader" class="preloader">
    <div class="preloader__image"></div>
  </div>
</template>

<style scoped>
.preloader {
  position: fixed;
  left: 0;
  top: 0;
  right: 0;
  bottom: 0;
  overflow: hidden;
  background: #fff;
  z-index: 1001;
}

.preloader__image {
  position: relative;
  top: 50%;
  left: 50%;
  width: 64px;
  height: 64px;
  margin-top: -32px;
  margin-left: -32px;
  background: url("https://i.gifer.com/origin/d3/d3f472b06590a25cb4372ff289d81711_w200.gif")
    no-repeat 50% 50%;
}

.loaded_hiding .preloader {
  transition: 0.3s opacity;
  opacity: 0;
}

.loaded .preloader {
  display: none;
}
</style>