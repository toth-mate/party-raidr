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

    <section>
        <ul class="list-group">
            <li class="list-group-item" v-for="a in applications">
                <h5>{{ a.eventId }}</h5>
                <p class="text-body-tertiary">You applied at: {{ a.timeOfApplication }}</p>
            </li>
        </ul>
    </section>
</template>