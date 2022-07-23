
const moduleNotification = {
    state() {
        return {
            optionsForToast: {
                animation: true,
                autohide: true,
                delay: 2000,
            },
            toasts: [],
            initializedToasts: [],
        }
    },
    mutations: {
        ADD_TOAST_TO_STATE: (state, toastData) => {
            state.toasts.push(toastData)
        },
        ADD_INITIALIZED_TOAST_TO_STATE: (state, initializedToast) => {
            state.initializedToasts.push(initializedToast)
        },
        DELETE_TOAST_FROM_STATE: (state, index) => {
            state.toasts.splice(index, 1)
        },
    },
    actions: {
        ADD_TOAST({ commit }, toastData) {
            return commit('ADD_TOAST_TO_STATE', toastData)
        },
        ADD_INITIALIZED_TOAST({ commit }, initializedToast) {
            return commit('ADD_INITIALIZED_TOAST_TO_STATE', initializedToast)
        },
        SHOW_NOTIFICATION({ commit, state }) {

            for (let index = 0; index < state.initializedToasts.length; index++) {
                state.initializedToasts[index].show()
                state.initializedToasts[index]._element.addEventListener('hidden.bs.toast', function () {
                    return commit('DELETE_TOAST_FROM_STATE', index)
                })
            }
        }
    },
    getters: {
        TOASTS(state) {
            return state.toasts
        },
        OPTIONS_FOR_TOASTS(state) {
            return state.optionsForToast
        }
    }
}

export default moduleNotification