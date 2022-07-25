<script>
import VKIcon from "./icons/VKIcon.vue";
import TelegramIcon from "./icons/TelegramIcon.vue";
import WhatsAppIcon from "./icons/WhatsAppIcon.vue";
import InstagrammIcon from "./icons/InstagrammIcon.vue";

export default {
  components: {
    VKIcon,
    TelegramIcon,
    WhatsAppIcon,
    InstagrammIcon,
  },
};
</script>

<template>
  <!-- Footer -->
  <footer class="text-center text-lg-start bg-dark text-muted pt-4">
    <!-- Section: Social media -->

    <!-- Section: Links  -->
    <section>
      <div class="container-fluid text-center text-md-start mt-4">
        <!-- Grid row -->
        <div class="row mt-3">
          <!-- Grid column -->
          <div class="col-md-3 col-lg-4 col-xl-3 mx-auto mb-4">
            <!-- Content -->
            <h6 class="text-uppercase fw-bold mb-4">
              <i class="fas fa-gem me-3"></i>ЦентрСтрой
            </h6>
            <p>
              "ТОРГОВАЯ КОМПАНИЯ "ЦЕНТРСТРОЙ" занимается оптовой продажей
              строительных и отделочных материалов.
            </p>
          </div>
          <!-- Grid column -->

          <!-- Grid column -->
          <div class="col-md-2 col-lg-2 col-xl-2 mx-auto mb-4">
            <!-- Links -->
            <h6 class="text-uppercase fw-bold mb-4">Социльные сети</h6>
            <p>
              <a
                href="https://vk.com/cstroy72"
                target="_blank"
                class="text-reset me-3"
              >
                <VKIcon />
              </a>
              <!-- <a
                href="https://instagram.com/cstroy72?igshid=YmMyMTA2M2Y="
                target="_blank"
                class="text-reset"
              >
                <InstagrammIcon />
              </a> -->
            </p>
            <h6 class="text-uppercase fw-bold mb-4">Мессенджеры</h6>
            <p>
              <a
                href="https://t.me/ESergeyN"
                target="_blank"
                class="text-reset me-3"
              >
                <TelegramIcon />
              </a>
              <a
                href="https://wa.me/79224876546"
                target="_blank"
                class="text-reset"
              >
                <WhatsAppIcon />
              </a>
            </p>
          </div>
          <!-- Grid column -->

          <!-- Grid column -->
          <div class="col-md-3 col-lg-2 col-xl-2 mx-auto mb-4">
            <!-- Links -->
            <h6 class="text-uppercase fw-bold mb-4">Меню</h6>
            <p>
              <a href="/" class="text-reset">Каталог</a>
            </p>
            <p>
              <a href="/aboutus" class="text-reset">О нас</a>
            </p>
            <p>
              <a href="/contacts" class="text-reset">Контакты</a>
            </p>
            <p>
              <a href="#!" class="text-reset"></a>
            </p>
          </div>
          <!-- Grid column -->

          <!-- Grid column -->
          <div class="col-md-4 col-lg-3 col-xl-3 mx-auto mb-md-0 mb-4">
            <!-- Links -->
            <h6 class="text-uppercase fw-bold mb-4">Контакты</h6>
            <p>
              <i class="fas fa-home me-3"></i> г.Тюмень, ул. Коммунистическая
              д.70, корп.3, стр.6, офис №59.
            </p>
            <p>
              <i class="fas fa-envelope me-3"></i>
              Мы работаем с 09:00 до 18:00 (Пн-Пт)⠀
            </p>
            <p><i class="fas fa-phone me-3"></i>Телефон: 8-922-487-65-46</p>
            <p><i class="fas fa-email me-3"></i>Почта: cstroy@inbox.ru</p>
          </div>
          <!-- Grid column -->
        </div>
        <!-- Grid row -->
      </div>
    </section>
    <!-- Section: Links  -->

    <!-- Copyright -->
    <div class="text-center p-1" style="background-color: rgba(0, 0, 0, 0.05)">
      © {{ new Date().getFullYear() }} Created by:
      <a class="text-reset fw-bold" href="">SnailBee</a>
    </div>
    <!-- Copyright -->
  </footer>
  <!-- Footer -->
</template>

<style>
</style>