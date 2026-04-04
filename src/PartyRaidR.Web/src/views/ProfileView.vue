<script setup>
    import { ref, onMounted } from 'vue'
    import { useAuthStore } from '@/stores/auth'
    import { useApplicationStore } from '@/stores/application'

    const authStore = useAuthStore()
    const applicationStore = useApplicationStore()

    const applications = ref([])

    onMounted(async () => {
        applications.value = await applicationStore.getMyApplications()
    })
</script>
<template>
    <h1>Profile</h1>
    <p class="text-center text-secondary fs-1 mt-3">Hello <span class="fw-semibold">{{ authStore.user.username }}!</span></p> 
</template>