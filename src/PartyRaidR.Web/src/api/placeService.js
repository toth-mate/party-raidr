import apiClient from "./api"

export default {
    getAll() {
        return apiClient.get('/place')
    },
    getMyPlaces() {
        return apiClient.get('/place/my-places')
    }
}