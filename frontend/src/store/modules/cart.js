const moduleCart = {
    state () {
        return {
            cart: localStorage.getItem('cart') ? JSON.parse(localStorage.getItem('cart')) : []
        }
      },
      mutations: {
        SET_CART( state, product ) {
            state.cart = state.cart.filter(el => el.id != product.id)
            state.cart.push(product)
            localStorage.setItem('cart', JSON.stringify(state.cart))
        },
        REMOVE_FROM_CART( state, productId ) {
            state.cart = state.cart.filter(el => el.id != productId)
            localStorage.setItem('cart', JSON.stringify(state.cart))
        }
      },
      actions: {
        ADD_TO_CART( { commit }, product ) {
          return commit('SET_CART', product)
        },
        DELETE_FROM_CART( { commit }, productId ) {
          return commit('REMOVE_FROM_CART', productId)
        },
        UPDATE_PRODUCT( { state } ) {
          localStorage.setItem('cart', JSON.stringify(state.cart))
        }
      },
      getters: {
        PRODUCTS_IN_CART(state) {
          return state.cart
        },
        TOTAL_COST(state) {
          let total_coast = 0.0
          state.cart.forEach(position => {
            total_coast += position.quantity * position.price
          });
          return total_coast
        }
      }
}

export default moduleCart