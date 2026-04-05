import apiClient from './api.js'

export default {
    getMyApplications() {
        return apiClient.get('/application/my-applications-display')
    },
    exists(eventId) {
        return apiClient.get(`/application/exists?eventId=${eventId}`)
    },
    apply(application) {
        return apiClient.post('/application', application)
    },
    withdraw(id) {
        return apiClient.delete(`/application/${id}`)
    }
}