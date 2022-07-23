import axios from 'axios'

const moduleChekout = {
  state() {
    return {
      orderData: sessionStorage.getItem('orderData')
        ? JSON.parse(sessionStorage.getItem('orderData'))
        : {
          firstName: '', secondName: '', address: '',
          region: '', postcode: '', telephone: '', email: ''
        }
    }
  },
  mutations: {
    SET_ORDER_DATA_TO_STATE(state, data) {
      state.orderData = data
    },
    SET_ORDER_DATA_IN_SESSION_STAGE(state) {
      sessionStorage.setItem('orderData', JSON.stringify(state.orderData))
    }
  },
  actions: {
    UPDATE_ORDER_DATA_IN_SESSION_STAGE({ commit }) {
      return commit('SET_ORDER_DATA_IN_SESSION_STAGE')
    },
    SET_ORDER_DATA( { commit }, data ) {
      return commit('SET_ORDER_DATA_TO_STATE', data)
    }
  },
  getters: {
    GET_ORDER_DATA(state) {
      return state.orderData
    }
  }
}

export default moduleChekout