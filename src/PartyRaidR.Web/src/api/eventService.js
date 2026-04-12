import apiClient from "./api"

export default {
    get() {
        return apiClient.get('/event')
    },
    getById(id) {
        return apiClient.get(`/event/${id}`)
    },
    getWithDetails() {
        return apiClient.get('/event/display-all')
    },
    getWithDetailsById(id) {
        return apiClient.get(`/event/display/${id}`)
    },
    getMyEvents() {
        return apiClient.get('/event/my-events')
    },
    insert(event) {
        return apiClient.post('/event', event)
    },
    filter(filter) {
        return apiClient.post('/event/filter', filter)
    }
}