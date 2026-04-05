<script setup>
    import { ref } from 'vue'
    import { useAuthStore } from '@/stores/auth'
    const authStore = useAuthStore()
</script>
<template>
    <h1>Profile</h1>
    <p class="text-center mt-3">You are logged in as <span class="fw-semibold">{{ authStore.user.username }}</span></p>
</template>