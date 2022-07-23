<script>
import axios from "axios";
import apiConfig from "../../api/config";
import DeleteModal from "./AdminPanelComponents/DeleteModal.vue";
import ModalSelectUndercategory from "./AdminPanelComponents/ModalSelectUndercategory.vue";
import notificationSevices from "../services/notificationServices";
import { mapActions, mapGetters } from "vuex";
import AdminProductDitails from "./AdminProductDitails.vue";

export default {
  data() {
    return {
      productData: {
        type: Object,
        default() {
          return {};
        },
      },
      file: {},
      imageIsChanged: false,
      imgUrlFixed: "",
      modalData: {
        endpoint: String,
        id: Number,
        redirectTo: String,
        modalId: "deleteModal",
      },
      choosenUndercategory: {
        name: "",
      },
      validationError: {
        undercategory: false,
      }
    };
  },
  components: {
    DeleteModal,
    ModalSelectUndercategory,
    AdminProductDitails
},
  computed: { ...mapGetters(["ADMINTOKEN", "ADMINS_PRODUCT_FOR_ID"]) },
  methods: {
    ...mapActions([
      "GET_ADMIN_ALL_UNDERCATEGORY_FROM_API",
      "GET_ADMIN_CATEGORIES_FROM_API",
    ]),
    setChoosenUndercategory(data) {
      this.choosenUndercategory = data.undercategoryData;
    },
    get_image_src() {
      this.file = this.$refs.file.files[0];
      this.imgUrlFixed = (window.URL || window.webkitURL).createObjectURL(
        this.file
      );
      this.imageIsChanged = true;
    },
    save_product() {
      if (this.choosenUndercategory.name != "") {
        if (this.$route.params.id == 0) {
          var formData = new FormData();

          formData.append("image", this.file);
          formData.append("product", JSON.stringify(this.productData));
          formData.append("subCategoryId", this.choosenUndercategory.id);

          axios
            .post(apiConfig.host + "AdminPanel/Products", formData, {
              headers: {
                Authorization: `Bearer ${this.ADMINTOKEN.token}`,
              },
            })
            .then((response) => {
              notificationSevices.showNotification("Товар успешно добавлен");
              this.$router.push({ name: "ProductPanel" });
            })
            .catch(function (error) {
              notificationSevices.showNotification(error.response.data);
            });
        } else {
          var formData = new FormData();

          formData.append("image", this.file);
          formData.append("product", JSON.stringify(this.productData));
          formData.append("subCategoryId", this.choosenUndercategory.id);
          formData.append("imageIsChanged", this.imageIsChanged);

          axios
            .put(
              apiConfig.host + "AdminPanel/Products?id=" + this.$route.params.id,
              formData,
              {
                headers: {
                  Authorization: `Bearer ${this.ADMINTOKEN.token}`,
                },
              }
            )
            .then((response) => {
              notificationSevices.showNotification("Изменения успешно сохранены");
              this.$router.push({ name: "ProductPanel" });
            })
            .catch(function (error) {
              notificationSevices.showNotification("Ошибка сохранения");
            });
            this.$refs.adminProductDitails.save_ditails_of_products()
        }
      } else {
        this.validationError.undercategory = true;
        notificationSevices.showNotification("Заполните все обязательлные поля!")
      }
    },
    cancel_change() {
      this.$router.push({ name: "ProductPanel" });
    },
    delete_product() {
      this.modalData = {
        endpoint: "AdminPanel/Products",
        id: this.$route.params.id,
        redirectTo: "ProductPanel",
        modalId: "deleteModal",
      };
    },
    plus_minus_switcher_button1() {
      if (this.$refs.button1.innerHTML == "+") {
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
  },
  created() {
    this.ADMINTOKEN == "" ? this.$router.push({ name: "AdminLogIn" }) : null;
    if (this.$route.params.id == 0) {
      this.productData = {
        id: 0,
        SubCategoryProductId: 0,
        title: "",
        shortDescription: "",
        imageUrl: "",
        packaging: "",
        isActive: true,
      };
    } else {
      axios
        .get(apiConfig.host + "catalog/products?id=" + this.$route.params.id)
        .then((response) => {
          this.productData = response.data;
          this.choosenUndercategory = this.productData.subCategory;
          this.imgUrlFixed = apiConfig.host + this.productData.imageUrl;
        });
    }
    this.GET_ADMIN_CATEGORIES_FROM_API();
    this.GET_ADMIN_ALL_UNDERCATEGORY_FROM_API();
  },
};
</script>

<template>
  <section>
    <div class="row">
      <div class="col">
        <div class="container">
          <div class="mb-3">
            <label for="inputName" class="form-label">Название </label>
            <input
              type="text"
              class="form-control"
              id="inputName"
              v-model="productData.title"
            />
          </div>
          <div class="d-inline-flex align-items-center">
            <h2 class="me-4">Изображение товара</h2>
            <button
              class="btn overview-button"
              data-bs-toggle="collapse"
              ref="button1"
              v-on:click="plus_minus_switcher_button1"
              data-bs-target="#products-image"
            >+</button>
          </div>

          <div class="collapse" id="products-image">
            <div class="mb-3">
              <img
                :src="imgUrlFixed"
                class="img-thumbnail"
                style="max-width: 300px"
              />
            </div>
            <div class="mb-3">
              <label for="formFile" class="form-label">Изображение</label>
              <input
                class="form-control"
                type="file"
                ref="file"
                id="formFile"
                accept=".png"
                v-on:change="get_image_src"
              />
              <small>Рекомендуемое рзрешение 800x800 пикселей</small>
            </div>
          </div>
          <div>
            <div class="d-inline-flex align-items-center">
              <h2 class="me-4">Описание товара</h2>
              <button
                class="btn overview-button"
                data-bs-toggle="collapse"
                ref="button2"
                v-on:click="plus_minus_switcher_button2"
                data-bs-target="#products-decription"
              >+</button>
            </div>

            <div class="collapse" id="products-decription">
              <div class="input-group mb-3">
                <button
                  class="btn btn-outline-secondary"
                  data-bs-toggle="modal"
                  data-bs-target="#selectUndercategoryModal"
                  type="button"
                  id="selectUndercategoryButton"
                >
                  Выбрать подкатегорию
                </button>
                <input
                  type="text"
                  class="form-control"
                  id="selectUnderCategory"
                  aria-describedby="selectUndercategoryButton"
                  v-model="choosenUndercategory.name"
                  readonly
                  disabled
                  :class="{ 'is-invalid': validationError.undercategory }"
                />
              </div>
              <div class="mb-3">
                <label for="inputDescription" class="form-label"
                  >Краткое описание</label
                >
                <input
                  type="text"
                  class="form-control"
                  id="inputDescription"
                  v-model="productData.shortDescription"
                />
              </div>
              <div class="mb-3">
                <label for="inputDescription" class="form-label"
                  >Полное описание</label
                >
                <textarea
                  type="text"
                  class="form-control"
                  id="inputDescription"
                  rows="5"
                  v-model="productData.fullDescription"
                  canresize="false"
                ></textarea>
              </div>
              <div class="mb-3">
                <label for="inputDescription" class="form-label"
                  >Компания производитель</label
                >
                <input
                  type="text"
                  class="form-control"
                  id="inputDescription"
                  v-model="productData.manufacturerCompany"
                />
              </div>
              <div class="mb-3">
                <label for="inputPackeging" class="form-label">Фасовка</label>
                <input
                  type="text"
                  class="form-control"
                  id="inputPackeging"
                  v-model="productData.packaging"
                />
              </div>
              <div class="mb-3 form-check form-switch">
                <input
                  type="checkbox"
                  class="form-check-input"
                  id="isActiveCheckBox"
                  v-model="productData.isActive"
                />
                <label class="form-check-label" for="isActiveCheckBox">{{
                  this.productData.isActive ? "Активен" : "Не активен"
                }}</label>
              </div>
            </div>
          </div>
          <div v-if="this.$route.params.id != 0">
            <div class="d-inline-flex align-items-center">
              <h2 class="me-4">Редактирование страницы товара</h2>
              <button
                class="btn overview-button"
                data-bs-toggle="collapse"
                ref="button3"
                v-on:click="plus_minus_switcher_button3"
                data-bs-target="#collapseUndercategory"
              >+</button>
            </div>

            <div class="collapse" id="collapseUndercategory">
              <AdminProductDitails ref="adminProductDitails" />
            </div>
          </div>

          <div class="w-100">
            <button
              data-bs-toggle="modal"
              data-bs-target="#deleteModal"
              v-on:click="delete_product"
              class="btn btn-outline-danger"
              v-if="this.$route.params.id != 0"
            >
              Удалить
            </button>
            <div class="float-end">
              <button v-on:click="cancel_change" class="btn btn-secondary me-3">
                Отмена
              </button>
              <button v-on:click="save_product" class="btn btn-primary me-3">
                Сохранить
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <DeleteModal :modalData="this.modalData" />
    <ModalSelectUndercategory @choosenUndercategory="setChoosenUndercategory" />
  </section>
</template>

<style scoped>
textarea {
  resize: none;
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
</style>