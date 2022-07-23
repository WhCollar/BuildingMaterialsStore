<script>
import axios from "axios";
import VKIcon from "../components/icons/VKIcon.vue";
import TelegramIcon from "../components/icons/TelegramIcon.vue";
import WhatsAppIcon from "../components/icons/WhatsAppIcon.vue";
import InstagrammIcon from "../components/icons/InstagrammIcon.vue";
import validate from "./services/formValidationServices";
import apiConfig from "../api/config";

export default {
  data() {
    return {
      callBackData: {
        firstname: "",
        phone: "",
        theme: "",
        massege: "",
      },
      validationError: {
        firstname: "",
        phone: "",
        theme: "",
        massege: "",
      },
    };
  },
  components: {
    VKIcon,
    TelegramIcon,
    WhatsAppIcon,
    InstagrammIcon,
  },
  watch: {
    'callBackData.firstname'() {
      this.validationError.firstname = validate.minlength(this.callBackData.firstname, 2)
    },
    'callBackData.phone'() {
      this.validationError.phone = validate.phone(this.callBackData.phone)
    },
    'callBackData.theme'() {
      this.validationError.theme = validate.minlength(this.callBackData.theme.trim(), 1)
    },
    'callBackData.massege'() {
      this.validationError.massege = validate.minlength(this.callBackData.massege.trim(), 1)
    },
  },
  methods: {
    sendCallBackForm() {
      axios
        .post(apiConfig.host + "order/accepting", {
          callBackData: this.callBackData,
        })
        .then(function (response) {
          console.log(response);
          this.router.push("/");
        })
        .catch(function (error) {
          console.log(error);
        });
    },
  },
};
</script>

<template>
  <div>
    <section class="contact-section">
      <div class="container py-5 px-4">
        <div class="row mt-5 text-light">
          <div class="col-md-6">
            <div class="row">
              <div class="col-mb-12 mb-4">
                <h4>Контактная информация</h4>
              </div>
              <div class="col-mb-12 mb-3">
                <p>
                  <span>Адрес:</span>
                  г.Тюмень, ул. Коммунистическая д.70, корп.3, стр.6, офис №59.
                </p>
              </div>
              <div class="col-mb-12 mb-3">
                <p>
                  <span>Телефон:</span>
                  <a href="tel://89224876546">8 922 487 65 46</a>
                </p>
              </div>
              <div class="col-mb-12 mb-3">
                <p>
                  <span>Почта:</span>
                  <a href="mailto:cstroy@inbox.ru">cstroy@inbox.ru</a>
                </p>
              </div>
              <div class="col-mb-12 mb-4 mt-4">
                <h4>Социальные сети</h4>
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
              </div>
              <div class="col-mb-12 mb-4 mt-4">
                <h4>Мессенджеры</h4>
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
              </div>
            </div>
          </div>
          <div class="col-md-6">
            <h4>Оставьте заявку и мы с вами свяжемся</h4>
            <div class="mt-4">
              <div class="input-group mb-3">
                <input
                  type="text"
                  class="form-control"
                  placeholder="Ваше имя"
                  aria-label="Firstname"
                  v-model="callBackData.firstname"
                  :class="{ 'is-invalid': validationError.firstname }"
                />
                <span class="input-group-text" id="countryCode">+7</span>
                <input
                  type="number"
                  class="form-control"
                  placeholder="Номер телефона"
                  aria-label="Phone"
                  aria-describedby="countrCode"
                  v-model="callBackData.phone"
                  :class="{ 'is-invalid': validationError.phone }"
                />
              </div>
              <div class="mb-3">
                <input
                  type="text"
                  class="form-control"
                  placeholder="Тема"
                  aria-label="Theme"
                  v-model="callBackData.theme"
                  :class="{ 'is-invalid': validationError.theme }"
                />
              </div>
              <div class="mb-3">
                <textarea
                  class="form-control"
                  rows="8"
                  placeholder="Сообщение"
                  atia-lable="Massege"
                  v-model="callBackData.massege"
                  :class="{ 'is-invalid': validationError.massege }"
                ></textarea>
              </div>
              <button
                v-on:click="sendCallBackForm"
                type="submit"
                class="btn btn-primary"
              >
                Отправить
              </button>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- <section>
        <div class="container py-3">
            <div class="row my-5">
                <div class="col">

                </div>
            </div>
        </div>
    </section> -->
  </div>
</template>

<style scoped>
a {
  color: floralwhite;
}
span::after {
  content: " ";
}
textarea {
  resize: none;
}
.contact-section {
  background: no-repeat center
      linear-gradient(rgba(0, 0, 0, 0.75), rgba(0, 0, 0, 0.75)),
    url("https://sun9-59.userapi.com/impf/oly3NRZMgxKShVN8-0XHbC88upWYwzSQ52YwGA/BWYx6wp66qw.jpg?size=1200x586&quality=95&sign=1fe22d3acc3660f38251753dbf4e65de&type=album");
  background-size: cover;
}
h4 {
  color: white;
}
</style>