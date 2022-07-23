<script>
import _ from "lodash";
import axios from "axios";
import apiConfig from "../../api/config";
import { mapActions, mapGetters } from "vuex";
import notificationSevices from "../services/notificationServices";

export default {
  data() {
    return {
      productDitails: {
        gost: "",
        compound: "",
        advantages: [],
        areasOfUse: [],
        featureList: [],
        notes: [],
      },
      advantages: "",
      areasOfUse: {
        title: "",
        items: [],
      },
      featureList: {
        title: "",
        items: [],
      },
      notes: "",
      ditailsIsExists: false,
    };
  },
  computed: {
    ...mapGetters(["ADMINTOKEN"]),
  },
  methods: {
    save_ditails_of_products() {
      console.log("Ok");
      if (this.ditailsIsExists) {
        console.log("PUT");
        axios
          .put(
            apiConfig.host +
              "AdminPanel/MoreAtProduct?productId=" +
              this.$route.params.id,
            this.productDitails,
            {
              headers: {
                Authorization: `Bearer ${this.ADMINTOKEN.token}`,
              },
            }
          )
          .then((response) => {
            notificationSevices.showNotification("Изменения успешно сохранены");
          })
          .catch(function (error) {
            notificationSevices.showNotification("Ошибка сохранения");
          });
      } else {
        console.log("POST");
        axios
          .post(
            apiConfig.host +
              "AdminPanel/MoreAtProduct?productId=" +
              this.$route.params.id,
            this.productDitails,
            {
              headers: {
                Authorization: `Bearer ${this.ADMINTOKEN.token}`,
              },
            }
          )
          .then((response) => {
            notificationSevices.showNotification("Изменения успешно сохранены");
          })
          .catch(function (error) {
            notificationSevices.showNotification("Ошибка сохранения");
          });
      }
    },
    add_advantages() {
      this.productDitails.advantages.push(this.advantages);
      this.advantages = "";
    },
    delete_advantages(index) {
      this.productDitails.advantages.splice(index, 1);
    },
    add_areasOfUse() {
      this.productDitails.areasOfUse.push(_.cloneDeep(this.areasOfUse));
      this.areasOfUse.title = "";
    },
    delete_areasOfUse(index) {
      this.productDitails.areasOfUse.splice(index, 1);
    },
    add_areaOfUse(index) {
      this.productDitails.areasOfUse[index].items.push(
        document.getElementById(["areasInput" + index]).value
      );
      document.getElementById(["areasInput" + index]).value = "";
      // document.location.href=`#areasInput + ${index}`
    },
    delete_areaOfUse(index1, index2) {
      this.productDitails.areasOfUse[index1].items.splice(index2, 1);
    },

    add_featureList() {
      this.productDitails.featureList.push(_.cloneDeep(this.featureList));
      this.featureList.title = "";
    },
    delete_featureList(index) {
      this.productDitails.featureList.splice(index, 1);
    },
    add_feature(index) {
      this.productDitails.featureList[index].items.push(
        document.getElementById(["featureInput" + index]).value +
          "|" +
          document.getElementById(["featureValueInput" + index]).value
      );
      document.getElementById(["featureInput" + index]).value = "";
      document.getElementById(["featureValueInput" + index]).value = "";
    },
    delete_feature(index1, index2) {
      this.productDitails.featureList[index1].items.splice(index2, 1);
    },

    add_notes() {
      this.productDitails.notes.push(this.notes);
      this.notes = "";
    },
    delete_notes(index) {
      this.productDitails.notes.splice(index, 1);
    },
    plus_minus_switcher_button1() {
      if (this.$refs.button1.innerHTML === "+") {
        this.$refs.button1.innerHTML = "-";
      } else {
        this.$refs.button1.innerHTML = "+";
      }
    },
    plus_minus_switcher_button2() {
      if (this.$refs.button2.innerHTML === "+") {
        this.$refs.button2.innerHTML = "-";
      } else {
        this.$refs.button2.innerHTML = "+";
      }
    },
    plus_minus_switcher_button3() {
      if (this.$refs.button3.innerHTML === "+") {
        this.$refs.button3.innerHTML = "-";
      } else {
        this.$refs.button3.innerHTML = "+";
      }
    },
    plus_minus_switcher_button4() {
      if (this.$refs.button4.innerHTML === "+") {
        this.$refs.button4.innerHTML = "-";
      } else {
        this.$refs.button4.innerHTML = "+";
      }
    },
  },
  created() {
    if (this.$route.params.id != 0) {
      axios
        .get(apiConfig.host + "MoreAtProduct?id=" + this.$route.params.id)
        .then((response) => {
          if (response.data != "") {
            this.productDitails = response.data;
            this.imageUrlFixed =
              "'" + apiConfig.host + this.productDitails.product.imageUrl + "'";
            this.ditailsIsExists = true;
          }
        });
    }
  },
};
</script>

<template>
  <div>
    <div class="mb-3">
      <label for="inputName" class="form-label">Гост</label>
      <input
        type="text"
        class="form-control"
        id="inputName"
        v-model="productDitails.gost"
      />
    </div>
    <div class="d-inline-flex align-items-center ms-5">
      <h3 class="me-4">Преимущества</h3>
      <button
        class="btn overview-button"
        data-bs-toggle="collapse"
        ref="button1"
        v-on:click="plus_minus_switcher_button1"
        data-bs-target="#advantages-collapse"
      >+</button>
    </div>

    <div class="collapse ms-5" id="advantages-collapse">
      <div class="mb-3">
        <div class="input-group mb-3">
          <input
            type="text"
            class="form-control"
            id="advantages"
            v-model="advantages"
            placeholder="Введите преимущество"
          />
          <button v-on:click="add_advantages" class="btn btn-primary">
            Добавить
          </button>
        </div>
        <ul class="list-group mb-3">
          <li
            class="
              list-group-item
              d-flex
              justify-content-between
              align-items-center
              subitem
            "
            v-for="(item, index) in this.productDitails.advantages"
            :key="index"
          >
            <span>#{{ index + 1 }} {{ item }}</span>

            <button
              v-on:click="delete_advantages(index)"
              class="btn btn-sm btn-outline-danger"
            >
              Удалить
            </button>
          </li>
        </ul>
      </div>
    </div>

    <div class="ms-5">
      <div class="d-inline-flex align-items-center">
        <h3 class="me-4">Область применения</h3>
        <button
          class="btn overview-button"
          data-bs-toggle="collapse"
          ref="button2"
          v-on:click="plus_minus_switcher_button2"
          data-bs-target="#areasOfUse-collapse"
        >+</button>
      </div>

      <div class="collapse" id="areasOfUse-collapse">
        <div class="mb-3">
          <ul class="list-group mb-3">
            <li
              class="
                list-group-item
                d-flex
                justify-content-between
                align-items-center
                mt-4
                mb-4
                parent-item
              "
              v-for="(item, index1) in this.productDitails.areasOfUse"
              :key="index1"
            >
              <div class="w-100">
                <div class="d-flex justify-content-between pb-2 mb-4 mt-1 me-3">
                  <h4 class="me-5">{{ item.title }}</h4>
                  <!-- <span>#{{ index1 }}</span> -->

                  <button
                    v-on:click="delete_areasOfUse(index1)"
                    class="btn btn-sm btn-outline-danger"
                  >
                    Удалить
                  </button>
                </div>
                <div class="input-group mb-2">
                  <input
                    type="text"
                    class="form-control"
                    :id="'areasInput' + index1"
                    placeholder="Введите область"
                  />
                  <button
                    v-on:click="add_areaOfUse(index1)"
                    class="btn btn-primary"
                  >
                    Добавить
                  </button>
                </div>
                
                <!-- <div class="d-inline-flex align-items-center">
                    <button
                        class="btn overview-button"
                        data-bs-toggle="collapse"
                        ref="button3"
                        v-on:click="plus_minus_switcher_button3"
                        :data-bs-target="'#collapse-area-' + index1"
                    >+</button>
                </div>

                <div class="collapse" :id="'#collapse-area-' + index1">
                </div> -->

                <ul class="list-group mb-3">
                  <li
                    class="
                      list-group-item
                      d-flex
                      justify-content-between
                      align-items-center
                      subitem
                      mt-3
                    "
                    v-for="(item, index2) in this.productDitails.areasOfUse[
                      index1
                    ].items"
                    :key="index2"
                  >
                    <span>#{{ index2 + 1 }} {{ item }}</span>

                    <button
                      v-on:click="delete_areaOfUse(index1, index2)"
                      class="btn btn-sm btn-outline-danger"
                    >
                      Удалить
                    </button>
                  </li>
                </ul>
              </div>
            </li>
          </ul>
          <div class="input-group mt-2">
            <input
              type="text"
              class="form-control"
              id="advantages"
              v-model="areasOfUse.title"
              placeholder="Название области применения"
            />
            <button v-on:click="add_areasOfUse" class="btn btn-primary">
              Добавить
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="ms-5">
      <div class="d-inline-flex align-items-center">
        <h3 class="me-4">Характеристики</h3>
        <button
          class="btn overview-button"
          data-bs-toggle="collapse"
          ref="button3"
          v-on:click="plus_minus_switcher_button3"
          data-bs-target="#collapse-featureList"
        >+</button>
      </div>

      <div class="collapse" id="collapse-featureList">
        <!-- ----------------------------------------------------------------------------------------- -->
        <div class="mb-3">
          <label for="inputName" class="form-label">Состав</label>
          <input
            type="text"
            class="form-control"
            id="inputName"
            v-model="productDitails.compound"
          />
        </div>
        <!-- ----------------------------------------------------------------------------------------- -->
        <div class="mb-3">
          <ul class="list-group mb-3">
            <li
              class="
                list-group-item
                d-flex
                justify-content-between
                align-items-center
                parent-item
                mt-5
              "
              v-for="(item, index1) in this.productDitails.featureList"
              :key="index1"
            >
              <div class="w-100">
                <div class="d-flex justify-content-between pb-2 mb-4 mt-1 me-3">
                  <h5 class="me-5">{{ item.title }}</h5>
                  <!-- <span>#{{ index1 }}</span> -->

                  <button
                    v-on:click="delete_featureList(index1)"
                    class="btn btn-sm btn-outline-danger"
                  >
                    Удалить
                  </button>
                </div>
                <div class="input-group mb-3">
                  <input
                    type="text"
                    class="form-control"
                    :id="'featureInput' + index1"
                    placeholder="Введите название"
                  />
                  <input
                    type="text"
                    class="form-control"
                    :id="'featureValueInput' + index1"
                    placeholder="Введите значение"
                  />
                  <button
                    v-on:click="add_feature(index1)"
                    class="btn btn-primary"
                  >
                    Добавить
                  </button>
                </div>
                <ul class="list-group mb-3">
                  <li
                    class="
                      list-group-item
                      d-flex
                      justify-content-between
                      align-items-center
                      subitem
                      mt-3
                    "
                    v-for="(item, index2) in this.productDitails.featureList[
                      index1
                    ].items"
                    :key="index2"
                  >
                    <span
                      >#{{ index2 + 1 }} {{ item.split("|")[0] }} :
                      {{ item.split("|")[1] }}</span
                    >

                    <button
                      v-on:click="delete_feature(index1, index2)"
                      class="btn btn-sm btn-outline-danger"
                    >
                      Удалить
                    </button>
                  </li>
                </ul>
              </div>
            </li>
          </ul>
          <div class="input-group mb-3">
            <input
              type="text"
              class="form-control"
              id="featureList"
              v-model="featureList.title"
              placeholder="Введите подзаголовок"
            />
            <button v-on:click="add_featureList" class="btn btn-primary">
              Добавить
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- ----------------------------------------------------------------------------------------- -->
    <!-- ----------------------------------------------------------------------------------------- -->
    <div class="ms-5">
      <div class="d-inline-flex align-items-center">
        <h3 class="me-4">Примечание</h3>
        <button
          class="btn overview-button"
          data-bs-toggle="collapse"
          ref="button4"
          v-on:click="plus_minus_switcher_button4"
          data-bs-target="#collapse-notes"
        >+</button>
      </div>

      <div class="collapse" id="collapse-notes">
        <div class="input-group mb-3">
          <input
            type="text"
            class="form-control"
            id="undercategoryName"
            v-model="notes"
            placeholder="Введите примечание"
          />
          <button v-on:click="add_notes" class="btn btn-primary">
            Добавить примечание
          </button>
        </div>
        <ul class="list-group mb-3 card">
          <li
            class="
              list-group-item
              d-flex
              justify-content-between
              align-items-center
              subitem
              mt-3
            "
            v-for="(item, index) in this.productDitails.notes"
            :key="index"
          >
            <span>#{{ index + 1 }} {{ item }}</span>

            <button
              v-on:click="delete_notes(item.id)"
              class="btn btn-sm btn-outline-danger"
            >
              Удалить
            </button>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<style scoped>
textarea {
  resize: none;
}
input:focus {
  box-shadow: none;
}
.overview-button {
  border: 0px;
  border-radius: 100px;
  font-size: 30px;
  width: 60px;
  height: fit-content;
  padding-bottom: 10px;
}
.overview-button:focus {
  box-shadow: none;
}
.overview-button:hover {
  background-color: #f5f5f5;
}
.subitem {
  border-radius: 0px !important;
  border-top: 0px;
  border-left: 0px;
  border-right: 0px;
}
.parent-item {
  border-top: 1px solid rgba(0, 0, 0, 0.125) !important;
}
.card {
  border: 1px solid rgba(0, 0, 0, 0.125) !important;
}
</style>