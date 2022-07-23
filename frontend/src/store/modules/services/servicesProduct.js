const servicesProduct = {

    productFiltration(products, filterData) {
        if (filterData.underCategoriesId.length == 0) {
            return products
        } else {
            let filteredProducts = []
            filterData.underCategoriesId.forEach(undercategoryId => {
                products.forEach(product => {
                    if (product.subCategory.id == undercategoryId) {
                        filteredProducts.push(product)
                    }
                });
            });
            return filteredProducts
        }
    }

}

export default servicesProduct