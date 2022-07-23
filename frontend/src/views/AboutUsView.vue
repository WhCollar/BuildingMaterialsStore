<script>
export default {};
</script>

<template>
  <section class="about-block d-md-flex">
    <div class="half img"></div>
    <div class="half text">
      <div class="overlap">
        <h2>О нас</h2>
        <p>
          Компания «ЦентрСтрой» (ООО «ТК «ЦентрСтрой») основана в июне 2012
          года. Основное направление компании оптовая продажа строительных и
          отделочных материалов. Первый магазин-офис компании был открыт в г.
          Ишим Тюменской области, второй офис продаж открылся в г. Тюмень в мае
          2022 года.
        </p>
        <h3>Наши ценности.</h3>
        <p>
          Мы стараемся работать так, чтобы высокая репутация фирмы формировалась
          на основе мнения обычного клиента.
        </p>
        <h3>Наша цель</h3>
        <p>
          Цель нашей компании состоит в удовлетворении потребностей российского
          строительного рынка в использовании качественных стройматериалов.
        </p>
        <h3>Миссия компании</h3>
        <p>
          Двигаться вперёд вместе с клиентами, делать жизнь людей лучше,
          предлагая современные и качественные материалы по разумным ценам.
        </p>
      </div>
    </div>
  </section>
</template>

<style scoped>
.about-block .half {
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
  .about-block .half .overlap {
    margin-left: -150px;
    padding: 40px;
    background: rgba(255, 255, 255, 0.9);
  }
  .text {
    background-color: #343a40;
  }
}
</style>