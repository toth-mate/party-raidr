import apiClient from "./api"

export default {
    login(creds) {
        return apiClient.post('/auth/login', creds)
    },
    register(data) {
        return apiClient.post('/auth/register', data)
    }
}