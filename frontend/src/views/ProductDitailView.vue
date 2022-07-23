<script>
import { mapActions } from "vuex";
import apiConfig from "../api/config";
import axios from "axios";
import CheckMark from "../components/icons/CheckMark.vue";
import AreasOfUse from "../components/DitailView/AreasOfUse.vue";
import FeatureList from "../components/DitailView/FeatureList.vue";

export default {
  data() {
    return {
      productDitails: {
        product: {
          title: "",
          fullDescription: "",
          gost: "",
        },
      },
      switch_window: true,
      imageUrlFixed: "",
    };
  },
  components: {
    CheckMark,
    AreasOfUse,
    FeatureList,
  },
  methods: {
    ...mapActions(["ADD_TO_CART"]),
    add_to_cart() {
      this.ADD_TO_CART(this.productDitails.product);
    },
    show_AreasOfUse() {
      this.switch_window = true;
    },
    show_featureList() {
      this.switch_window = false;
    },
  },
  mounted() {
    axios
      .get(apiConfig.host + "MoreAtProduct?id=" + this.$route.params.id)
      .then((response) => {
        this.productDitails = response.data;
        this.imageUrlFixed =
          "'" + apiConfig.host + this.productDitails.product.imageUrl + "'";
      })
      .catch((error) => {
        this.$router.push({ name: "PageNotFound" });
      });
  },
};
</script>

<template>
  <div>
    <section class="header-section">
      <div class="container">
        <div class="row py-5">
          <div class="col-md-6">
            <div
              class="product-image"
              :style="
                'background:url(' +
                this.imageUrlFixed +
                ') center no-repeat; background-size:contain;'
              "
            ></div>
          </div>
          <div class="col-md-6 text-white py-5">
            <h1>{{ this.productDitails.product.title }}</h1>
            <p class="mb-5">
              {{ this.productDitails.product.fullDescription }},
              {{ this.productDitails.gost }}
            </p>
            <ul class="list-unstyled mb-4">
              <li
                v-for="(advantage, index) in this.productDitails.advantages"
                :key="index"
                class="d-flex mb-4"
              >
                <div class="check-mark me-4">
                  <CheckMark />
                </div>
                {{ advantage }}
              </li>
            </ul>
            <button
              class="btn btn-link add-to-cart-button"
              v-on:click="add_to_cart"
            >
              Добавить в корзину
            </button>
          </div>
        </div>
      </div>
    </section>
    <section class="ditail-menu-section">
      <div class="container">
        <div class="row py-3">
          <div class="d-none d-lg-block col-lg-2"></div>
          <div class="col-lg-8">
            <div>
              <button
                class="btn btn-link ditail-menu-button me-4"
                v-on:click="show_AreasOfUse"
                :class="{ active: this.switch_window }"
              >
                Применение
              </button>
              <button
                class="btn btn-link ditail-menu-button"
                v-on:click="show_featureList"
                :class="{ active: !this.switch_window }"
              >
                Характеристики
              </button>
            </div>
          </div>
        </div>
      </div>
    </section>
    <AreasOfUse
      :areasOfUse="this.productDitails.areasOfUse"
      v-if="this.switch_window"
    />
    <FeatureList
      :featureList="this.productDitails.featureList"
      :compound="this.productDitails.compound"
      :notes="this.productDitails.notes"
      v-if="!this.switch_window"
    />
  </div>
</template>

<style scoped>
p {
  font-weight: 100;
  font-size: 18px;
}
h1 {
  font-family: "Fira Mono", monospace;
  font-weight: 600;
  font-size: 70px;
}
li {
  font-size: 22px;
}
.header-section {
  background: #1f6e8f
    url(https://bergauf.ru/wp-content/themes/template/img/shipment_background.png)
    left 0% top no-repeat;
}
.ditail-menu-section {
  background: #02405b;
}
.ditail-menu-button {
  font-size: 20px;
  color: white;
  text-decoration-line: none;
}
.ditail-menu-button:hover {
  color: white;
}
.add-to-cart-button {
  font-size: 20px;
  color: white;
  background: #14556e;
  text-decoration-line: none;
}
.add-to-cart-button:hover {
  color: white;
  background: #02405b;
}
.ditail-menu-button:focus {
  box-shadow: none;
}
.product-image {
  margin-top: 120px;
  height: 400px;
}
.check-mark svg {
  fill: white;
  height: 25px;
}

.active {
  border-bottom: 2px solid white;
  border-radius: 0px;
}
</style>