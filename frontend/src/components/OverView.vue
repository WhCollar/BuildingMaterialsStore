<script>
import axios from "axios";
import apiConfig from "../api/config";

export default {
  data() {
    return {
      overviewData: {},
      imgUrlFixed: "",
    };
  },
  props: {
    slug: String,
  },
  methods: {},
  created() {
    axios
      .get(apiConfig.host + "OverviewProducts/" + this.slug)
      .then((response) => {
        this.overviewData = response.data;
        this.imgUrlFixed =
          "'" + apiConfig.host + "/" + this.overviewData.imageUrl + "'";
      });
  },
};
</script>

<template>
  <section
    class="section-background"
    :style="
      'background: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url(' +
      this.imgUrlFixed +
      ');background-repeat: no-repeat, no-repeat; background-position: center center, center center; background-size: cover, cover; display: block;'
    "
  >
    <div class="container">
      <div class="category_block1 d-flex flex-column justify-content-center">
        <div class="text-white mt-auto">
          <h1>{{ overviewData.title }}</h1>
          <span>{{ overviewData.subtitle }}</span>
          <p>
            {{ overviewData.description }} <br />
            <br />
          </p>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
.category_block1 {
  padding-bottom: 80px;
}
.category_block1 h1 {
  font-size: 60px;
  margin-top: 70px;
  margin-bottom: 50px;
  color: white;
  text-shadow: 1px 1px 1px rgba(150, 150, 150, 1);
  position: relative;
}
.category_block1 span {
  font-size: 24px;
  display: block;
  text-shadow: 1px 1px 1px rgba(150, 150, 150, 1);
  color: white;
  margin-bottom: 40px;
}

@media (min-width: 768px) {
  .category_block1 {
    width: 50%;
    padding-bottom: 80px;
  }
  .category_block1 p {
    width: auto;
    max-width: 465px;
  }
}
</style>