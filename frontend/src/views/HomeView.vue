<script>
import { mapActions, mapGetters } from "vuex";
import CatalogButtonItem from "../components/CatalogButtonItem.vue";
import ScrollIcon from "../components/icons/ScrollIcon.vue";

export default {
  components: {
    CatalogButtonItem,
    ScrollIcon,
  },
  computed: {
    ...mapGetters(["CATEGORIES"]),
  },
  methods: {
    ...mapActions(["GET_CATEGORIES_FROM_API"]),
  },
  mounted() {
    this.GET_CATEGORIES_FROM_API();
  },
};
</script>

<template>
  <div>
    <section class="preview-section d-flex justify-content-center">
      <div class="d-flex flex-column justify-content-center">
        <div class="text-white text-center mt-auto">
          <h3>ТК «ЦентрСтрой»</h3>
          <p>Надёжный поставщик строительных материалов для ваших проектов.</p>
        </div>
        <div class="scroll-row mt-auto d-flex justify-content-center mb-5">
          <a href="#advantagesSection">
            <ScrollIcon />
          </a>
        </div>
      </div>
    </section>
    <!-- End Preview Section -->

    <!-- <section style="background: #f8f9fa;">
            <div class="container py-3">
                <div class="row my-5">
                    <div class="col text-center">
                        <h2 class="text-uppercase">
                            Приглашаем к сотрудничеству
                        </h2>
                        <p>
                            Для Вас мы подготовили выгодные условия и эксклюзивные цены!
                        </p>
                    </div>
                </div>
                <div class="row my-5">
                    <div class="col-sm-4">
                        <div class="d-flex justify-content-center text-center">
                            <p>
                                Строительные компании
                            </p>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="d-flex justify-content-center text-center">
                            <p>
                                Всех кто работает с сухими строительными смесями
                            </p>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="d-flex justify-content-center text-center">
                            <p>
                                Дизайнеров
                            </p>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="d-flex justify-content-center text-center">
                            <p>
                                Прорабов
                            </p>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="d-flex justify-content-center text-center">
                            <p>
                                 Мастеров по укладке плитки
                            </p>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="d-flex justify-content-center text-center">
                            <p>
                                Строительные бригады
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section> -->
    <!-- End Invite To Cooperate Section -->

    <section id="advantagesSection">
      <div class="container py-3">
        <div class="row my-5">
          <div class="col text-center">
            <h2 class="text-uppercase">Наши преиущества</h2>
          </div>
        </div>
        <div class="row my-5">
          <div class="col-md-4">
            <div class="container-fluid">
              <div class="d-flex justify-content-center text-center">
                <p>СТРОИТЕЛЬНЫЕ МАТЕРИАЛЫ ОПТОМ</p>
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="container-fluid">
              <div class="d-flex justify-content-center text-center">
                <p>У НАС ИСКЛЮЧИТЕЛЬНО СЕРИТФИЦИРОВАННАЯ ПРОДУКЦИЯ</p>
              </div>
            </div>
          </div>
          <div class="col-md-4">
            <div class="container-fluid">
              <div class="d-flex justify-content-center text-center">
                <p>ОФИЦИАЛЬНЫЙ ДИЛЕР ЗАВОДА «BERGAUF»</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- End Advantages Section -->

    <!-- <section style="background: #f8f9fa">
      <div class="container py-3">
        <div class="row my-5">
          <div class="col text-center">
            <h2 class="text-uppercase">Карта постоянного клиента</h2>
          </div>
        </div>
        <div class="row">
          <div class="col-md-6 d-flex justify-content-center">
            <img
              src="https://sun9-59.userapi.com/impf/RcErTVMHv4pOjhMTA-jvz8DcGgLSazvwJcxO5g/q9oLRyMfdT4.jpg?size=1064x674&quality=96&sign=61ccbf00c24443a44465fe98aa274181&type=album"
              class="img-fluid"
              style="max-height: 275px"
              alt="..."
            />
          </div>
          <div class="col-md-6 d-flex justify-content-center">
            <p>
              Наши ценности. Мы стараемся работать так, чтобы высокая репутация
              фирмы формировалась на основе мнения обычного клиента. Наше
              видение. Мы хотим стать крупнейшим поставщиком строительных и
              отделочных материалов в г. Тюмень и Тюменской области.
            </p>
          </div>
        </div>
      </div>
    </section> -->
    <!-- End Bonus Card Section -->

    <section class="about-block d-md-flex">
      <div class="half img"></div>
      <div class="half-1 text">
        <div class="overlap">
          <h2>О нас</h2>
          <p>
            Компания «ЦентрСтрой» (ООО «ТК «ЦентрСтрой») основана в июне 2012
            года. Основное направление компании оптовая продажа строительных и
            отделочных материалов. Первый магазин-офис компании был открыт в г.
            Ишим Тюменской области, второй офис продаж открылся в г. Тюмень в
            мае 2022 года.
          </p>
          <h3>Наши ценности.</h3>
          <p>
            Мы стараемся работать так, чтобы высокая репутация фирмы
            формировалась на основе мнения обычного клиента.
          </p>
          <h3>Наша цель</h3>
          <p>
            Цель нашей компании состоит в удовлетворении потребностей
            российского строительного рынка в использовании качественных
            стройматериалов.
          </p>
          <h3>Миссия компании</h3>
          <p>
            Двигаться вперёд вместе с клиентами, делать жизнь людей лучше,
            предлагая современные и качественные материалы по разумным ценам.
          </p>
        </div>
      </div>
    </section>
    <!-- End Half Section -->

    <section id="catalog">
      <div class="container py-3">
        <div class="row my-5">
          <div class="col text-center">
            <h2 class="text-uppercase">Продукция</h2>
          </div>
        </div>
        <div class="row my-5">
          <CatalogButtonItem
            v-for="category in CATEGORIES"
            :key="category.id"
            :slug="category.slug"
            :title="category.name"
          />
        </div>
      </div>
    </section>
    <!-- End Category Section -->

    <!-- <section style="height:100vh; width:100vw;">
      <iframe src="http://monserka.beget.tech/" frameborder="0"></iframe>
    </section> -->
    <!-- End Tetst Section -->
  </div>
</template>

<style scoped>
.preview-section {
  background: no-repeat center
      linear-gradient(rgba(0, 0, 0, 0.4), rgba(0, 0, 0, 0.4)),
    url("https://cdn.iz.ru/sites/default/files/article-2020-04/_KKP8962_0.jpg");
  background-repeat: no-repeat, no-repeat;
  background-position: center center, center center;
  background-size: cover, cover;
  width: 100%;
  height: calc(100vh - 72px);
}
.scroll-row {
  width: 100%;
}
.about-block .half {
  width: 100%;
  padding: 150px 15px;
}
.about-block .half-1 {
  width: 100%;
  padding: 30px 15px;
}
.img {
  background-image: url(https://sun9-8.userapi.com/impf/FZ9KFy6eznlRTZVK4GVCGt51tYKWV9lMx7R-dw/RtnJqHRxmC8.jpg?size=750x1000&quality=95&sign=de772489b24665bd777015871e460fe3&type=album);
  background-repeat: no-repeat, no-repeat;
  background-position: center center, center center;
  background-size: cover, cover;
  display: block;
}
.text {
  background-color: white;
}
@media (min-width: 768px) {
  .about-block .half {
    width: 50%;
    padding: 5% 10% 5% 5%;
  }
  .about-block .half-1 .overlap {
    margin-left: -150px;
    padding: 40px;
    background: rgba(255, 255, 255, 0.9);
  }
  .text {
    background-color: #343a40;
  }
}
</style>