import axios from 'axios'
import apiConfig from '../../api/config'

const moduleUndercategory = {
  state() {
    return {
      undercategories: []
    }
  },
  mutations: {
    SET_UNDERCATEGORIES_TO_STATE: (state, undercategories) => {
      state.undercategories = undercategories
    },
    ADD_UNDERCATEGORY_TO_STATE(state, undercategoryData) {
      state.undercategories.push(undercategoryData)
    },
    DELETE_UNDERCATEGORY_FROM_STATE(state, id) {
      state.undercategories = state.undercategories.filter(el => el.id != id)
    }
  },
  actions: {
    GET_UNDERCATEGORIES_FROM_API({ commit }, category_slug) {
      axios.get(apiConfig.host + 'Catalog/Sub?slug=' + category_slug).then(response => { commit('SET_UNDERCATEGORIES_TO_STATE', response.data)})
    },
    ADD_UNDERCATEGORY({ commit }, undercategoryData) {
      return commit('ADD_UNDERCATEGORY_TO_STATE', undercategoryData);
    },
    DELETE_UNDERCATEGORY({ commit }, id) {
      return commit('DELETE_UNDERCATEGORY_FROM_STATE', id);
    },
  },
  getters: {
    UNDERCATEGORIES(state) {
      return state.undercategories
    }
  }
}

export default moduleUndercategory