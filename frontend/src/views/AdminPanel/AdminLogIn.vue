<script>
import { mapActions, mapGetters } from "vuex";

export default {
  data() {
    return {
      adminData: {
        userName: "",
        password: "",
      },
    };
  },
  computed: { ...mapGetters(["ADMINTOKEN"]) },
  methods: {
    ...mapActions(["GET_ADMIN_TOKEN"]),
    async LogIn() {
      await this.GET_ADMIN_TOKEN(this.adminData);
      if (this.ADMINTOKEN.role == "Administrator") {
        this.$router.push({ name: "AdminPanel" });
      }
    },
  },
  created() {
    if (this.ADMINTOKEN.role == "Administrator") {
      this.$router.push({ name: "AdminPanel" });
    }
  },
};
</script>

<template>
  <section
    class="
      adminLogin
      d-flex
      flex-column
      justify-content-center
      align-items-center
    "
  >
    <div class="content">
      <h1 class="mb-3">Панель администратора</h1>
      <form>
        <div class="mb-3">
          <label for="inputLogin" class="form-label">Логин</label>
          <input
            class="form-control"
            id="inputLogin"
            aria-describedby="login"
            v-model="adminData.userName"
          />
        </div>
        <div class="mb-3">
          <label for="inputPassword" class="form-label">Пароль</label>
          <input
            type="password"
            class="form-control"
            id="inputPassword"
            v-model="adminData.password"
          />
        </div>
      </form>
      <button v-on:click="LogIn" type="submit" class="btn btn-primary">
        Вход
      </button>
    </div>
  </section>
</template>

<style soped>
.adminLogin {
  height: 100vh;
}
.content {
  max-width: fit-content;
}
</style>