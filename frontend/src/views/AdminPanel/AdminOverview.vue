<script>
import axios from "axios";
import apiConfig from "../../api/config";
import notificationSevices from "../services/notificationServices";
import { mapActions, mapGetters } from "vuex";

export default {
  data() {
    return {
      overviewData: {
        title: "",
        subtitle: "",
        description: "",
      },
      imgUrlFixed: "",
      file: {},
      overviewIsExists: false,
      imageIsChanged: false,
    };
  },
  computed: {
    ...mapGetters(["ADMINTOKEN"]),
  },
  watch: {
    slug() {
      axios
        .get(apiConfig.host + "OverviewProducts/" + this.slug)
        .then((response) => {
          this.overviewData = response.data;
          this.imgUrlFixed = apiConfig.host + this.overviewData.imageUrl;
          if (response.status == 204) {
            this.overviewData = {
              title: "",
              subtitle: "",
              description: "",
            };
            this.overviewIsExists = false;
          } else {
            this.overviewIsExists = true;
          }
        })
        .catch((error) => {
          this.overviewIsExists = false;
        });
    },
  },
  methods: {
    get_image_src() {
      this.file = this.$refs.file.files[0];
      this.imgUrlFixed = (window.URL || window.webkitURL).createObjectURL(
        this.file
      );
      this.imageIsChanged = true;
    },
    save_overview() {
      if (this.overviewIsExists == false) {
        var formData = new FormData();

        formData.append("image", this.file);
        formData.append("overview", JSON.stringify(this.overviewData));
        formData.append("categorySlug", this.slug);

        axios
          .post(apiConfig.host + "AdminPanel/Overview", formData, {
            headers: {
              Authorization: `Bearer ${this.ADMINTOKEN.token}`,
            },
          })
          .then((response) => {
            notificationSevices.showNotification("Изменения успешно сохранены");
          })
          .catch(function (error) {
            notificationSevices.showNotification(error);
          });
      } else {
        var formData = new FormData();

        formData.append("image", this.file);
        formData.append("overview", JSON.stringify(this.overviewData));
        formData.append("categoryId", this.categoryId);
        formData.append("imageIsChanged", this.imageIsChanged);

        axios
          .put(
            apiConfig.host + "AdminPanel/Overview?id=" + this.overviewData.id,
            formData,
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
            notificationSevices.showNotification(error);
          });
      }
    },
  },
  props: {
    categoryId: Number,
    slug: String,
  },
};
</script>

<template>
  <div>
    <div class="mb-3">
      <label for="inputName" class="form-label">Заголовок</label>
      <input
        type="text"
        class="form-control"
        id="inputName"
        v-model="overviewData.title"
      />
    </div>
    <div class="mb-3">
      <img :src="imgUrlFixed" class="img-thumbnail" />
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
    </div>
    <div class="mb-3">
      <label for="inputDescription" class="form-label">Подзагаловок</label>
      <input
        type="text"
        class="form-control"
        id="inputDescription"
        v-model="overviewData.subtitle"
      />
    </div>
    <div class="mb-3">
      <label for="inputDescription" class="form-label">Описание</label>
      <textarea
        type="text"
        class="form-control"
        id="inputDescription"
        rows="5"
        v-model="overviewData.description"
        canresize="false"
      ></textarea>
    </div>
  </div>
</template>

<style scoped>
textarea {
  resize: none;
}
</style>