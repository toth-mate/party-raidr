import { ref } from "vue"
import { defineStore } from "pinia"
import applicationService from "@/api/applicationService"

export const useApplicationStore = defineStore('application', () => {
    async function applicationExists(eventId) {
        try {
            const res = await applicationService.exists(eventId)
            return res.data
        } catch(e) {
            console.warn(e)
        }
    }

    async function apply(eventId) {
        try {
            const res = await applicationService.apply({
                id: '',
                userId: '',
                eventId: eventId,
                timeOfApplication: '2000-01-01',
                status: 0
            })
        } catch(e) {
            console.warn(e)
        }
    }

    return { applicationExists, apply }
})