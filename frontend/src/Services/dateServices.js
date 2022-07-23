
const dateServices = {
    dateTimeFormatingForNotification(date) {
        let monthNumberToTitle = {
            0: 'Января',
            1: 'Февраля',
            2: 'Марта',
            3: 'Апреля',
            4: 'Мая',
            5: 'Июня',
            6: 'Июля',
            7: 'Августа',
            8: 'Сентября',
            9: 'Октября',
            10: 'Ноября',
            11: 'Декабря',
        }
        let dateData = {
            day: date.getDay(),
            month: monthNumberToTitle[date.getMonth()],
            hours: date.getHours(),
            minutes: date.getMinutes(),
        }
        return `${dateData.day} ${dateData.month} ${dateData.hours}:${dateData.minutes < 10 ? `0${dateData.minutes}` : dateData.minutes}`
    }
}

export default dateServices