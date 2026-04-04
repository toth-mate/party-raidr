import { ref } from "vue"
import { defineStore } from "pinia"
import applicationService from "@/api/applicationService"

export const useApplicationStore = defineStore('application', () => {
    async function getMyApplications() {
        try {
            const res = await applicationService.getMyApplications()
            return res.data
        } catch(e) {
            console.warn(e)
        }
    }

    async function deleteApplication(id) {
        try {
            await applicationService.withdraw(id)
        } catch(e) {
            console.warn(e)
        }
    }

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

    return { applicationExists, apply, getMyApplications, deleteApplication }
})