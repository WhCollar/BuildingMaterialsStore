<script>
import axios from "axios";
import notificationSevices from "./services/notificationServices.js";
import validate from "./services/formValidationServices.js";
import { mapActions, mapGetters } from "vuex";
import apiConfig from "../api/config.js";

export default {
  data() {
    return {
      orderData: {
        firstName: "",
        secondName: "",
        address: "",
        region: "Тюмень",
        postcode: "",
        telephone: "",
        email: "",
      },
      validationError: {
        firstName: false,
        secondName: false,
        address: false,
        region: false,
        postcode: false,
        telephone: false,
        email: false,
      },
    };
  },
  watch: {
    "orderData.firstName"() {
      this.validationError.firstName = validate.minlength(
        this.orderData.firstName,
        2
      );
    },
    "orderData.secondName"() {
      this.validationError.secondName = validate.minlength(
        this.orderData.secondName,
        2
      );
    },
    "orderData.postcode"() {
      this.validationError.postcode =
        validate.minlength(this.orderData.postcode, 6) ||
        validate.maxlength(this.orderData.postcode, 6);
    },
    "orderData.email"() {
      this.validationError.email = validate.email(this.orderData.email);
    },
    "orderData.telephone"() {
      this.validationError.telephone = validate.phone(this.orderData.telephone);
    },
  },
  computed: {
    ...mapGetters(["GET_ORDER_DATA", "PRODUCTS_IN_CART", "TOTAL_COST"]),
  },
  methods: {
    ...mapActions(["SET_ORDER_DATA", "UPDATE_ORDER_DATA_IN_SESSION_STAGE"]),
    setOrderData() {
      this.SET_ORDER_DATA(this.orderData);
      this.UPDATE_ORDER_DATA_IN_SESSION_STAGE();
    },
    sendOrderToServer() {
      if (
        this.orderData.firstName == "" ||
        this.orderData.secondName == "" ||
        this.orderData.address == "" ||
        this.orderData.postcode == "" ||
        this.orderData.telephone == "" ||
        this.orderData.email == ""
      ) {
        this.validationError = {
          firstName: true,
          secondName: true,
          address: true,
          postcode: true,
          telephone: true,
          email: true,
        };
        return notificationSevices.showNotification(
          "Заполните обязательные поля"
        );
      } else {
        console.log("POST");
        console.log(this.PRODUCTS_IN_CART);
        axios
          .post(apiConfig.host + "AcceptOrder", {
            detailsOfPayment: this.GET_ORDER_DATA,
            orderPositions: this.PRODUCTS_IN_CART,
          })
          .then((response) => {
            notificationSevices.showNotification("Заявка успешно отправлена.");
            notificationSevices.showNotification("Проверьте папку спам в указанной почте!");
            this.$router.push({ name: "Home" });
          })
          .catch(function (error) {
            notificationSevices.showNotification(error.message);
          });
      }
    },
    redirect_to_product(id) {
      this.$router.push({ name: "ProductDitails", params: { id: id } });
    },
  },
  mounted() {
    this.orderData = this.GET_ORDER_DATA;
  },
};
</script>

<template>
  <section>
    <div class="container">
      <div class="row row-cols-1 my-5">
        <div class="d-none d-lg-block col-lg-2"></div>
        <div class="col-lg-8">
          <h2 class="my-4">Оформление заказа</h2>
          <form v-on:change="setOrderData">
            <div class="input-group mb-3">
              <span class="input-group-text">Имя и Фамилия</span>
              <input
                v-model.trim="this.GET_ORDER_DATA.firstName"
                type="text"
                placeholder="Имя"
                aria-label="First name"
                class="form-control"
                v-bind:class="{ 'is-invalid': validationError.firstName }"
              />
              <input
                v-model.trim="this.GET_ORDER_DATA.secondName"
                type="text"
                placeholder="Фамилия"
                aria-label="Last name"
                class="form-control"
                v-bind:class="{ 'is-invalid': validationError.secondName }"
              />
            </div>
            <div class="input-group mb-3">
              <span class="input-group-text">+7</span>
              <input
                v-model.trim="this.GET_ORDER_DATA.telephone"
                type="phone"
                placeholder="Телефон"
                aria-label="Phone"
                class="form-control"
                v-bind:class="{ 'is-invalid': validationError.telephone }"
              />
            </div>
            <div class="mb-3">
              <input
                v-model.trim="this.GET_ORDER_DATA.email"
                type="email"
                placeholder="Электронная почта"
                aria-label="Email"
                class="form-control"
                v-bind:class="{ 'is-invalid': validationError.email }"
              />
            </div>
            <input
              v-model="this.GET_ORDER_DATA.region"
              type="text"
              placeholder="Город"
              aria-label="Town"
              class="form-control mb-3"
            />
            <input
              v-model="this.GET_ORDER_DATA.address"
              type="text"
              placeholder="Адрес доставки"
              aria-label="Address"
              class="form-control mb-3"
            />
            <input
              v-model.trim="this.GET_ORDER_DATA.postcode"
              type="text"
              placeholder="Почтовый индекс"
              aria-label="Post index"
              class="form-control"
              :class="{ 'is-invalid': validationError.postcode }"
            />
          </form>

          <div class="my-5">
            <!-- <div class="table-lable mb-4">Список товаров и услуг</div> -->

            <table class="table table-borderless">
              <thead class="">
                <tr>
                  <th scope="col">#</th>
                  <th scope="col">Название</th>
                  <th scope="col">
                    <div class="float-end me-1">Количество</div>
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="(position, index) in PRODUCTS_IN_CART"
                  :key="position.id"
                  class="table-item"
                >
                  <th>{{ index + 1 }}</th>
                  <td
                    class="item-name"
                    v-on:click="redirect_to_product(position.id)"
                  >
                    {{ position.title }}
                  </td>
                  <td>
                    <div class="float-end me-5">{{ position.quantity }}</div>
                  </td>
                </tr>
              </tbody>
              <tfoot>
                <tr>
                  <td></td>
                  <td></td>
                  <th></th>
                </tr>
              </tfoot>
            </table>
          </div>
          <div>
            <button
              v-on:click="sendOrderToServer"
              class="btn btn-dark float-end"
            >
              Отправить заявку
            </button>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
table {
  color: #656565;
}
.table-lable {
  border-bottom: 1px solid rgba(0, 0, 0, 0.125);
}
.item-name {
  color: #3793b5;
  cursor: pointer;
}
.table-item {
  border-bottom: 1px solid rgba(0, 0, 0, 0.125);
  height: 50px;
}
</style>