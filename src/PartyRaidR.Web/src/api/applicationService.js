import apiClient from './api.js'

export default {
    exists(eventId) {
        return apiClient.get(`/application/exists?eventId=${eventId}`)
    },
    apply(application) {
        return apiClient.post('/application', application)
    }
}