import { ref } from "vue"
import { defineStore } from "pinia"
import authService from "@/api/authService"

export const useAuthStore = defineStore('auth', () => {
    const token = ref(localStorage.getItem('token') || null)
    const user = ref(JSON.parse(localStorage.getItem('user')) || null)

    async function login(creds) {
        try {
            const tokenResponse = await authService.login(creds)
            
            token.value = tokenResponse.data
            localStorage.setItem('token', token.value)

            const userResponse = await authService.me()
            
            if(userResponse.status === 200) {
                user.value = userResponse.data
                localStorage.setItem('user', JSON.stringify(user.value))
            }
            return true
        } catch(e) {
            console.error(e)
        }
    }

    return { token, login }
})
