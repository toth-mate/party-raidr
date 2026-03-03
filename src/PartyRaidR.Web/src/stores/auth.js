import { ref, computed } from "vue"
import { defineStore } from "pinia"
import authService from "@/api/authService"

export const useAuthStore = defineStore('auth', () => {
    const token = ref(localStorage.getItem('token') || null)
    const user = ref(JSON.parse(localStorage.getItem('user')) || null)

    const isAuthenticated = computed(() => Boolean(token.value))
    const isAdmin = computed(() => user.value?.role === 1 )

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

    function logout() {
        token.value = null
        user.value = null

        // Clear local storage
        localStorage.removeItem('token')
        localStorage.removeItem('user')
    }

    return { token, user, isAuthenticated, isAdmin, login, logout }
})
