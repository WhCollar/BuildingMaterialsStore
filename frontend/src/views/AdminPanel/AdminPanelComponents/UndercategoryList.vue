<script>
import { mapActions, mapGetters } from "vuex";
import axios from "axios";
import apiConfig from "../../../api/config";
import notificationSevices from "../../services/notificationServices";
import DeleteModal from "../AdminPanelComponents/DeleteModal.vue";

export default {
  data() {
    return {
      undercategoryData: {
        name: "",
        id: 0,
        categoryList: true,
      },
      modalData: {
        endpoint: String,
        id: Number,
        redirectTo: String,
        modalId: "deleteModalSubCut",
      },
    };
  },
  props: {
    slug: String,
  },
  components: {
    DeleteModal,
  },
  computed: { ...mapGetters(["UNDERCATEGORIES", "ADMINTOKEN"]) },
  methods: {
    ...mapActions([
      "GET_UNDERCATEGORIES_FROM_API",
      "DELETE_UNDERCATEGORY",
      "ADD_UNDERCATEGORY",
    ]),
    delete_undercategory(id) {
      this.modalData = {
        endpoint: "AdminPanel/SubCategories",
        id: id,
        redirectTo: "",
        modalId: "deleteModalSubCut",
      };
    },
    add_new_undercategory() {
      console.log(this.undercategoryData)
      axios
        .post(
          apiConfig.host + "AdminPanel/SubCategories?categorySlug=" + this.slug,
          this.undercategoryData,
          {
            headers: {
              Authorization: `Bearer ${this.ADMINTOKEN.token}`,
            },
          }
        )
        .then((response) => {
          notificationSevices.showNotification("Подкатегория успешно добавлена");
          console.log(response)
          let copyObj = JSON.parse(JSON.stringify(response.data));

          this.ADD_UNDERCATEGORY(copyObj);
          this.undercategoryData.name = "";
        })
        .catch(function (error) {
          notificationSevices.showNotification(error.response.data);
        });
    },
  },
  watch: {
    slug() {
      this.GET_UNDERCATEGORIES_FROM_API(this.slug);
    },
  },
};
</script>

<template>
  <div>
    <div class="input-group mb-3">
      <input
        type="text"
        class="form-control"
        id="undercategoryName"
        v-model="undercategoryData.name"
        placeholder="Введите название подкатегории"
      />
      <button v-on:click="add_new_undercategory" class="btn btn-primary">
        Добавить подкатегорию
      </button>
    </div>
    <ul class="list-group mb-3">
      <li
        class="
          list-group-item
          d-flex
          justify-content-between
          align-items-center
        "
        v-for="(item, index) in this.UNDERCATEGORIES"
        :key="index"
      >
        <span>#{{ index + 1 }} {{ item.name }}</span>

        <button
          v-on:click="delete_undercategory(item.id)"
          data-bs-toggle="modal"
          data-bs-target="#deleteModalSubCut"
          class="btn btn-sm btn-outline-danger"
        >
          Удалить
        </button>
      </li>
    </ul>
    <DeleteModal :modalData="this.modalData" />
  </div>
</template>

<style scoped>
</style>