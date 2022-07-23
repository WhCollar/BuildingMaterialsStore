<script>
import axios from "axios";
import apiConfig from "../../../api/config";
import notificationSevices from "../../services/notificationServices";
import { mapGetters, mapActions } from "vuex";

export default {
  props: {
    modalData: {
      modalId: {
        type: String,
        default() {
          return 'deleteModal'
        }
      },
      endpoint: String,
      id: Number,
      redirectTo: String,
    },
  },
  computed: {
    ...mapGetters(["ADMINTOKEN"]),
  },
  methods: {
    ...mapActions(["DELETE_UNDERCATEGORY"]),
    remove() {
      axios
        .delete(
          apiConfig.host + this.modalData.endpoint + "?id=" + this.modalData.id,
          {
            headers: {
              Authorization: `Bearer ${this.ADMINTOKEN.token}`,
            },
          }
        )
        .then((response) => {
          notificationSevices.showNotification(response.data);
          if (this.modalData.endpoint == "AdminPanel/SubCategories") {
            this.DELETE_UNDERCATEGORY(this.modalData.id)
          }
          this.$router.push({ name: this.modalData.redirectTo });
        })
        .catch((error) => {
          notificationSevices.showNotification(error.response.data);
        });
    },
  },
};
</script>

<template>
  <div
    class="modal fade"
    :id="this.modalData.modalId"
    data-bs-backdrop="static"
    data-bs-keyboard="false"
    tabindex="-1"
    :aria-labelledby="this.modalData.modalId"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" :id="this.modalData.modalId">Удаление</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">Востановить будет нельзя, вы уверены?</div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Отмена
          </button>
          <button
            v-on:click="remove"
            type="button"
            class="btn btn-danger"
            data-bs-dismiss="modal"
          >
            Удалить
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
</style>