import axios from 'axios'
import apiConfig from '../../api/config.js'
import notificationServices from '../../views/services/notificationServices'

const moduleAdmin = {
  state() {
    return {
      adminsToken: sessionStorage.getItem('adminsToken') ? JSON.parse(sessionStorage.getItem('adminsToken')) : {},
      categoryList: [],
      undercategoryList: [],
      productsList: [],
    }
  },
  mutations: {
    SET_ADMIN_TOKEN(state, tokenObj) {
      state.adminsToken = tokenObj
      sessionStorage.setItem('adminsToken', JSON.stringify(state.adminsToken))
    },
    SET_ADMIN_CATEGORIES_TO_STATE: (state, categories) => {
      state.categoryList = categories
    },
    SET_ADMIN_PRODUCTS_TO_STATE: (state, products) => {
      state.productsList = products
    },
    SET_ADMIN_UNDERCATEGORY_TO_STATE: (state, undercategory) => {
      state.undercategoryList = undercategory
    },
    ADD_ADMIN_NEW_UNDERCATEGORY_TO_STATE(state, undercategoryData) {
      state.undercategoryList.push(undercategoryData)
    },
    DELETE_ADMIN_UNDERCATEGORY_FOR_ID_FROM_STATE(state, id) {
      state.undercategory = state.undercategory.filter(el => el.id != id)
    }
  },
  actions: {
    async GET_ADMIN_TOKEN({ commit }, adminData) {

      await axios
        .post(apiConfig.host + "Authentication/Login", {
          email: adminData.userName,
          password: adminData.password,
        })
        .then(async (response) => {
          await commit('SET_ADMIN_TOKEN', response.data)
        })
        .catch(function (error) {
          notificationServices.showNotification(error)
          console.log(error)
        });
    },
    GET_ADMIN_CATEGORIES_FROM_API({ commit }) {
      axios.get(apiConfig.host + 'catalog').then(response => { commit('SET_ADMIN_CATEGORIES_TO_STATE', response.data) })
    },
    GET_ADMIN_PRODUCTS_FROM_API({ commit }, category_slug) {
      axios.get(apiConfig.host + 'Catalog/' + category_slug).then(response => (commit('SET_ADMIN_PRODUCTS_TO_STATE', response.data)))
    },
    GET_ADMIN_ALL_UNDERCATEGORY_FROM_API({ commit }) {
      axios.get(apiConfig.host + 'Catalog/SubCategories/All').then(response => (commit('SET_ADMIN_UNDERCATEGORY_TO_STATE', response.data)))
    },
    ADD_ADMIN_NEW_UNDERCATEGORY({ commit }, undercategoryData) {
      return commit('ADD_ADMIN_NEW_UNDERCATEGORY_TO_STATE', undercategoryData);
    },
    DELETE_ADMIN_UNDERCATEGORY_FOR_ID({ commit }, id) {
      return commit('DELETE_ADMIN_UNDERCATEGORY_FOR_ID_FROM_STATE', id);
    },
  },
  getters: {
    ADMINTOKEN(state) {
      return state.adminsToken
    },
    ADMINS_CATEGORY(state) {
      return state.categoryList
    },
    ADMINS_CATEGORY_FOR_ID: (state) => (id) => {
      return state.categoryList.find(item => item.id == id)
    },
    ADMINS_PRODUCTS(state) {
      return state.productsList
    },
    ADMINS_PRODUCT_FOR_ID: (state) => (id) => {
      return state.productsList.find(item => item.id == id)
    },
    ADMINS_UNDERCATEGORY(state) {
      return state.undercategoryList
    },
    ADMINS_UNDERCATEGORY_FOR_ID: (state) => (id) => {
      return state.undercategoryList.find(item => item.id == id)
    },
  }
}

export default moduleAdmin
