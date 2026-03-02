import { ref } from "vue"
import { defineStore } from "pinia"
import authService from "@/api/authService"

export const useAuthStore = defineStore('auth', () => {
    const token = ref(localStorage.getItem('token') || null)

    async function login(creds) {
        console.log(creds)

        try {
            const res = await authService.login(creds)
            token.value = res.data

            localStorage.setItem('token', token.value)
            console.log(res)

            return true
        } catch(e) {
            console.error(e)
        }
    }

    return { token, login }
})
