
const formValidationServices = {
    minlength(iter, length) {
        return !(iter.length >= length)
    },
    maxlength(iter, length) {
        return !(iter.length <= length)
    },
    email(email) {
        var pattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
        return !pattern.test(String(email).toLowerCase());
    },
    phone(phone) {
        let regex = /^\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$/;
        return !regex.test(phone);
    },
}

export default formValidationServices