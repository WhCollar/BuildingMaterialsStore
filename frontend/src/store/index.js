import { createStore } from 'vuex'
import moduleCategory from './modules/category.js'
import moduleProducts from './modules/products.js'
import moduleUndercategory from './modules/undercategory.js'
import moduleCart from './modules/cart.js'
import moduleChekout from './modules/chekout.js'
import moduleNotification from './modules/notification.js'
import moduleAdmin from './modules/admin.js'
// import moduleFilter from './modules/filter.js' Не понятна архитектура

const store = createStore({
  modules: {
    moduleCategory: moduleCategory,
    moduleUndercategory: moduleUndercategory,
    moduleProducts: moduleProducts,
    moduleCart: moduleCart,
    moduleChekout: moduleChekout,
    moduleNotification: moduleNotification,
    moduleAdmin: moduleAdmin,
    // moduleFilter: moduleFilter,
  }
})

export default store