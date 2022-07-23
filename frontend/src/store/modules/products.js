import axios from 'axios'
import apiConfig from '../../api/config'
import servicesProduct from './services/servicesProduct'

const moduleProducts = {
    state() {
        return {
            products: [],
            filter: {
                underCategoriesId: []
            }
        }
    },
    mutations: {
        SET_PRODUCTS_TO_STATE(state, products) {
            state.products = products
        },
        ADD_UNDERCATEGORIES_ID_TO_STATE(state, undercategoryId) {
            state.filter.underCategoriesId.push(undercategoryId)
        },
        DELETED_UNDERCATEGORIES_ID_FROM_STATE(state, undercategoryId) {
            state.filter.underCategoriesId = state.filter.underCategoriesId.filter(Id => Id != undercategoryId)
        }
    },
    actions: {
        GET_PRODUCTS_FROM_API({ commit }, category_slug) {
            axios.get(apiConfig.host + 'Catalog/' + category_slug).then(response => (commit('SET_PRODUCTS_TO_STATE', response.data)))
        },
        SET_UNDERCATEGORIES_ID({ commit }, data) {
            if (data.isSelected) {
                return commit('ADD_UNDERCATEGORIES_ID_TO_STATE', data.id);
            } else {
                return commit('DELETED_UNDERCATEGORIES_ID_FROM_STATE', data.id);
            }
        }
    },
    getters: {
        PRODUCTS(state) {
            return servicesProduct.productFiltration(state.products, state.filter)
        }
    }
}

export default moduleProducts