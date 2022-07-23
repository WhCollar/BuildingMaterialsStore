import store from '../../store/index.js'
import dateServices from '../../Services/dateServices.js'

const notificationSevices = {

    async showNotification(message) {
        await store.dispatch('ADD_TOAST', {
            imgUrl: '',
            dataTime: dateServices.dateTimeFormatingForNotification(new Date()),
            message: message,
        })
        store.dispatch('SHOW_NOTIFICATION')
    },

}

export default notificationSevices