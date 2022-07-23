<script>
import { mapActions, mapGetters } from "vuex";
import DeleteModal from "./AdminPanelComponents/DeleteModal.vue";
import notificationSevices from "../services/notificationServices";
import UndercategoryList from "./AdminPanelComponents/UndercategoryList.vue";
import AdminOverview from "./AdminOverview.vue";
import axios from "axios";
import apiConfig from "../../api/config";

export default {
  data() {
    return {
      categoryData: {
        type: Object,
        default() {
          return {};
        },
      },
      modalData: {
        endpoint: String,
        id: Number,
        redirectTo: String,
        modalId: "deleteModal",
      },
    };
  },
  components: { DeleteModal, UndercategoryList, AdminOverview },
  computed: {
    ...mapGetters(["ADMINTOKEN"]),
  },
  methods: {
    async save_category() {
      if (this.$route.params.id == 0) {
        axios
          .post(apiConfig.host + "AdminPanel/Categories", this.categoryData, {
            headers: {
              Authorization: `Bearer ${this.ADMINTOKEN.token}`,
            },
          })
          .then((response) => {
            notificationSevices.showNotification("Категория успешно добавлена");
            this.$router.push({ name: "CategoryPanel" });
          })
          .catch(function (error) {
            notificationSevices.showNotification(error.response.data);
          });
      } else {
        axios
          .put(
            apiConfig.host +
              "AdminPanel/Categories?id=" +
              this.$route.params.id,
            this.categoryData,
            {
              headers: {
                Authorization: `Bearer ${this.ADMINTOKEN.token}`,
              },
            }
          )
          .then((response) => {
            notificationSevices.showNotification("Изменения успешно сохранены");
            this.$router.push({ name: "CategoryPanel" });
          })
          .catch(function (error) {
            notificationSevices.showNotification(error.response.data);
          });
      }
      await this.$refs.AdminOverview1.save_overview();
    },
    cancel_change() {
      this.$router.push({ name: "CategoryPanel" });
    },
    delete_category() {
      this.modalData = {
        endpoint: "AdminPanel/Categories",
        id: this.$route.params.id,
        redirectTo: "CategoryPanel",
        modalId: "deleteModal",
      };
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
  },
  created() {
    this.ADMINTOKEN == "" ? this.$router.push({ name: "AdminLogIn" }) : null;
    if (this.$route.params.id == 0) {
      this.categoryData = { id: 0, slug: "", title: "" };
    } else {
      axios
        .get(apiConfig.host + "catalog/Category?id=" + this.$route.params.id)
        .then((response) => {
          this.categoryData = response.data;
        });
    }
  },
};
</script>

<template>
  <section>
    <div class="row">
      <div class="col">
        <div class="container">
          <div>
            <div class="mb-3">
              <label for="inputName" class="form-label"
                >Название категории</label
              >
              <input
                type="text"
                class="form-control"
                id="inputName"
                v-model="categoryData.name"
              />
            </div>
            <div
              class="mb-3"
              style="border-bottom: 1px solid #b5b5b5"
              v-if="this.$route.params.id != 0"
            >
              <div class="d-inline-flex align-items-center">
                <h2 class="me-4">Overview</h2>
                <button
                  class="btn overview-button"
                  data-bs-toggle="collapse"
                  ref="button1"
                  v-on:click="plus_minus_switcher_button1"
                  data-bs-target="#collapseOverview"
                >+</button>
              </div>

              <div class="collapse" id="collapseOverview">
                <AdminOverview
                  :categoryId="categoryData.id"
                  :slug="categoryData.slug"
                  ref="AdminOverview1"
                />
              </div>
            </div>
            <div
              class="mb-3"
              style="border-bottom: 1px solid #b5b5b5"
              v-if="this.$route.params.id != 0"
            >
              <div class="d-inline-flex align-items-center">
                <h2 class="me-4">Подкатегории</h2>
                <button
                  class="btn overview-button"
                  data-bs-toggle="collapse"
                  ref="button2"
                  v-on:click="plus_minus_switcher_button2"
                  data-bs-target="#collapseUndercategory"
                >+</button>
              </div>

              <div class="collapse" id="collapseUndercategory">
                <UndercategoryList :slug="categoryData.slug" />
              </div>
            </div>
          </div>
          <div class="w-100">
            <button
              data-bs-toggle="modal"
              data-bs-target="#deleteModal"
              v-on:click="delete_category"
              class="btn btn-outline-danger"
              v-if="this.$route.params.id != 0"
            >
              Удалить категорию
            </button>
            <div class="float-end">
              <button v-on:click="cancel_change" class="btn btn-secondary me-3">
                Отмена
              </button>
              <button v-on:click="save_category" class="btn btn-primary">
                Сохранить
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <DeleteModal :modalData="this.modalData" />
  </section>
</template>

<style scoped>
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