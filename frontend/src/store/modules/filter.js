const moduleFilter = {
    state () {
        return {
            underCategoriesId: []
        }
      },
      mutations: {
        ADD_UNDERCATEGORIES_ID_TO_STATE( state, undercategoryId ) {
            state.underCategoriesId.push(undercategoryId)
        },
        DELETED_UNDERCATEGORIES_ID_FROM_STATE( state, undercategoryId ) {
            state.underCategoriesId = state.underCategoriesId.filter(Id => Id != undercategoryId)
        }
      },
      actions: {
        SET_UNDERCATEGORIES_ID({commit}, data) {
            if(data.isSelected){
                return commit('ADD_UNDERCATEGORIES_ID_TO_STATE', data.id);
            } else {
                return commit('DELETED_UNDERCATEGORIES_ID_FROM_STATE', data.id);
            }
        }
      },
      getters: {
        FILTER(state) {
            return { underCategoriesId: state.underCategoriesId }
        }
      }
}

export default moduleFilter