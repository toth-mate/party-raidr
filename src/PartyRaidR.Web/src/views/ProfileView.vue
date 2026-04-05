<script setup>
    import { ref, onMounted } from 'vue'
    import { RouterLink } from 'vue-router'
    import { useAuthStore } from '@/stores/auth'
    import { useApplicationStore } from '@/stores/application'

    const authStore = useAuthStore()
    const applicationStore = useApplicationStore()

    const applications = ref([])

    onMounted(async () => {
        applications.value = await applicationStore.getMyApplications()
    })

    const withdraw = async (id) => {
        await applicationStore.deleteApplication(id)
        applications.value = await applicationStore.getMyApplications()
    }
</script>
<template>
    <h1>Profile</h1>
    <p class="text-center text-secondary fs-1 mt-3">Hello <span class="fw-semibold">{{ authStore.user.username }}!</span></p>

    <section>
        <h2 class="text-center mb-3">Your applications:</h2>
        <ul class="list-group" v-if="applications.length > 0">
            <li class="list-group-item d-flex justify-content-between" v-for="a in applications">
                <div>
                    <RouterLink :to="`/event/${a.eventId}`">
                        <h5>{{ a.title }}</h5>
                    </RouterLink>
                    <p class="text-body-tertiary">You applied at: {{ a.dateOfApplication }}</p>
                </div>
                <div class="d-flex flex-column justify-content-center">
                    <button class="btn btn-danger" @click="withdraw(a.id)">Withdraw</button>
                </div>
            </li>
        </ul>

        <p v-else class="text-body-secondary text-center fs-5">You haven't applied to any events yet.</p>
    </section>
</template>