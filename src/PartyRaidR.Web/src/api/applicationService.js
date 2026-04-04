import apiClient from './api.js'

export default {
    getMyApplications() {
        return apiClient.get('/application/my-applications')
    },
    exists(eventId) {
        return apiClient.get(`/application/exists?eventId=${eventId}`)
    },
    apply(application) {
        return apiClient.post('/application', application)
    }
}