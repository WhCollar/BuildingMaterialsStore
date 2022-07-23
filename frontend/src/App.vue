<script>
import MenuItem from "./components/MenuItem.vue";
import FooterItem from "./components/FooterItem.vue";
import PushNotificationContainer from "./components/Notification/PushNotificationContainer.vue";
import Preloader from "./components/Preloader.vue"

export default {
  data() {
    return {
      isAdmin: false,
      isAdminPage: false,
      adminPagesList: [
        "AdminLogIn",
        "AdminPanel",
        "CategoryPanel",
        "ProductPanel",
        "AdminProduct",
        "AdminCategory",
      ],
    };
  },
  components: {
    MenuItem,
    FooterItem,
    PushNotificationContainer,
    Preloader,
  },
  watch: {
    $route: function (to, from) {
      window.scrollTo(0, 0);
      this.isAdminPage = this.adminPagesList.includes(this.$route.name);
    },
  },
};
</script> 

<template>
  <div id="app">
    <!-- <Preloader /> -->
    <MenuItem v-if="!this.isAdminPage" />

    <div :class="{ 'mt-5 pt-4': !this.isAdminPage }">
      <router-view></router-view>
    </div>

    <FooterItem v-if="!this.isAdminPage" />

    <PushNotificationContainer />
  </div>
</template>

<style>
@import url("https://fonts.googleapis.com/css2?family=Fira+Mono:wght@400;500;700&display=swap");

::-webkit-scrollbar {
  width: 6px;
  height: 6px;
}
::-webkit-scrollbar-track {
  border-radius: 10px;
  background: rgba(0, 0, 0, 0.1);
}
::-webkit-scrollbar-thumb {
  border-radius: 10px;
  background: rgba(0, 0, 0, 0.2);
}
::-webkit-scrollbar-thumb:hover {
  background: rgba(0, 0, 0, 0.4);
}
::-webkit-scrollbar-thumb:active {
  background: rgba(0, 0, 0, 0.9);
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  /* display: none; <- Crashes Chrome on hover */
  -webkit-appearance: none;
  margin: 0;
}

body {
  width: 100%;
}

a {
  text-decoration: none;
}
.button-text {
  padding: 1em;
  text-transform: uppercase;
  font-weight: 500;
  cursor: pointer;
}
.button-text:hover {
  background: gainsboro;
}
</style>