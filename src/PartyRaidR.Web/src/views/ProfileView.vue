<script setup>
    import { ref, onMounted, onUpdated } from 'vue'
    import { useAuthStore } from '@/stores/auth'
    import { useApplicationStore } from '@/stores/application'

    const authStore = useAuthStore()
    const applicationStore = useApplicationStore()

    const applications = ref([])

    onMounted(async () => {
        applications.value = await applicationStore.getMyApplications()
    })

    onUpdated(async () => {
        applications.value = await applicationStore.getMyApplications()
    })
</script>
<template>
    <h1>Profile</h1>
    <p class="text-center text-secondary fs-1 mt-3">Hello <span class="fw-semibold">{{ authStore.user.username }}!</span></p>

    <section>
        <ul class="list-group">
            <li class="list-group-item d-flex justify-content-between" v-for="a in applications">
                <div>
                    <h5>{{ a.eventId }}</h5>
                    <p class="text-body-tertiary">You applied at: {{ a.timeOfApplication }}</p>
                </div>
                <div class="d-flex flex-column justify-content-center">
                    <button class="btn btn-danger" @click="applicationStore.deleteApplication(a.id)">Withdraw</button>
                </div>
            </li>
        </ul>
    </section>
</template>