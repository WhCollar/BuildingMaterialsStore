import axios from 'axios'
import apiConfig from '../../api/config'

const moduleCategory = {
  state() {
    return {
      categories: [],
    }
  },
  mutations: {
    SET_CATEGORIES_TO_STATE: (state, categories) => {
      state.categories = categories
    }
  },
  actions: {
    GET_CATEGORIES_FROM_API({ commit }) {
      axios.get( apiConfig.host + 'catalog').then(response => (commit('SET_CATEGORIES_TO_STATE', response.data)))
    }
  },
  getters: {
    CATEGORIES(state) {
      return state.categories
    }
  }
}

export default moduleCategory