<script>
import { mapActions, mapGetters } from "vuex";
import axios from "axios";
import apiConfig from "../../../api/config";

export default {
  data() {
    return {
      sortedCatalog: [],
    };
  },
  computed: {
    ...mapGetters(["ADMINTOKEN"]),
  },
  methods: {
    choose_subcategory(undercategory) {
      this.$emit("choosenUndercategory", {
        undercategoryData: undercategory,
      });
    },
  },
  created() {
    axios
      .get(apiConfig.host + "AdminPanel/Products?fillProducts=False", {
        headers: {
          Authorization: `Bearer ${this.ADMINTOKEN.token}`,
        },
      })
      .then((response) => {
        this.sortedCatalog = response.data;
      });
  },
};
</script>

<template>
  <div
    class="modal fade"
    id="selectUndercategoryModal"
    tabindex="-1"
    aria-labelledby="selectUndercategoryModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            Выберите подкатегорию
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <ul class="list-unstyled py-3">
            <li
              class="mb-1"
              v-for="(category, index) in this.sortedCatalog"
              :key="index"
            >
              <button
                class="btn d-inline-flex align-items-center list-button w-100"
                data-bs-toggle="collapse"
                :data-bs-target="'#undercategories-colaps' + index"
              >
                {{ category.name }}
              </button>
              <div class="collapse" :id="'undercategories-colaps' + index">
                <ul class="list-unstyled fw-normal pb-1 small">
                  <li
                    v-for="(undercategory, index) in category.subCategories"
                    :key="index"
                  >
                    <div>
                      <button
                        class="
                          btn
                          d-inline-flex
                          align-items-center
                          list-button
                          w-75
                          ms-5
                        "
                        data-bs-dismiss="modal"
                        v-on:click="choose_subcategory(undercategory)"
                      >
                        {{ undercategory.name }}
                      </button>
                    </div>
                  </li>
                </ul>
              </div>
            </li>
          </ul>
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Отмена
          </button>
          <!-- <button type="button" class="btn btn-primary">Save changes</button> -->
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.list-button {
  border-top: 0px;
  border-left: 0px;
  border-right: 0px;
  border-bottom: 1px solid #f0f0f0;
  border-radius: 0px;
}
.list-button:focus {
  box-shadow: 0 0 0 0.25rem rgb(255, 255, 255, 0);
}
</style>