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

    return { applicationExists }
})