<script setup>
    import { RouterLink } from 'vue-router'
    import { useAuthStore } from '@/stores/auth'
    import { ref } from 'vue'

    const authStore = useAuthStore()
    const creds = ref({
        email: null,
        password: null
    })
</script>
<template>
    <h1>Login</h1>
    <div class="col-12 col-md-8 col-lg-6 col-xl-4 mx-auto mt-4 px-3 pt-3 border">
        <div class="form-floating mb-2">
            <input type="email" class="form-control" id="email" placeholder="example@mail.org" v-model="creds.email">
            <label for="email">Email address</label>
        </div>
        <div class="form-floating mb-2">
            <input type="password" class="form-control" id="password" placeholder="Password" v-model="creds.password">
            <label for="password">Password</label>
        </div>
        <button type="submit" class="btn btn-primary w-100" @click="authStore.login({ email: creds.email, password: creds.password })">Login</button>
        <p class="mt-3 text-center">
            Don't have an account?
            <RouterLink to="/register">Register here!</RouterLink>
        </p>
    </div>
</template>