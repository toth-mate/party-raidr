<script setup>
  import { useRoute, useRouter, RouterLink } from 'vue-router'
  import { useAuthStore } from '@/stores/auth'

  const route = useRoute()
  const router = useRouter()
  const authStore = useAuthStore()

  function doLogout() {
    authStore.logout()
    if(route.meta.requiresAuthentication && !authStore.isAuthenticated) {
        router.push('/')
    }
  }
</script>
<template>
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
        <div class="container-fluid">
            <RouterLink class="navbar-brand" to="/">Party RaidR</RouterLink>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                    <li class="nav-item">
                        <RouterLink class="nav-link" to="/">
                            <i class="fa-solid fa-house"></i>
                            Home
                        </RouterLink>
                    </li>
                    <li class="nav-item dropdown">
                        <a href="#" class="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="fa-regular fa-user"></i>
                            User
                        </a>
                        <ul class="dropdown-menu">
                            <li v-if="!authStore.isAuthenticated">
                                <RouterLink to="/login" class="dropdown-item">
                                    <i class="fa-regular fa-user"></i>
                                    Login
                                </RouterLink>
                            </li>
                            <li v-if="!authStore.isAuthenticated">
                                <RouterLink to="/register" class="dropdown-item">
                                    <i class="fa-solid fa-user-plus"></i>
                                    Register
                                </RouterLink>
                            </li>
                            <li v-else>
                                <RouterLink to="/profile" class="dropdown-item">Profile</RouterLink>
                                <hr class="dropdown-divider">
                                <button @click="doLogout" to="/logout" class="dropdown-item">
                                    <i class="fa-solid fa-arrow-right-to-bracket"></i>
                                    Logout
                                </button>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>
<style scoped>
    #profile {
        width: 40px;
        height: 40px;
        padding: 5px;
        border-radius: 50%
    }
</style>