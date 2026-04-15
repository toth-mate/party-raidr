<script setup>
    import { ref, onMounted, Teleport } from 'vue'
    import { RouterLink } from 'vue-router'
    import { Modal } from 'bootstrap'
    import { useAuthStore } from '@/stores/auth'
    import { useEventStore } from '@/stores/event'
    import { useApplicationStore } from '@/stores/application'
    import { usePlaceStore } from '@/stores/place'

    const authStore = useAuthStore()
    const eventStore = useEventStore()
    const applicationStore = useApplicationStore()
    const placeStore = usePlaceStore()

    const modal = ref(null)
    let modalInstance = null

    const applications = ref([])
    const events = ref([])
    const places = ref([])

    let selected = ref(null)

    onMounted(async () => {
        applications.value = await applicationStore.getMyApplications()
        events.value = await eventStore.getMyEvents()
        places.value = await placeStore.getMyPlaces()

        console.log(places.value)

        modalInstance = new Modal(modal.value)
    })

    const openModal = () => {
        modalInstance.show()
    }

    const closeModal = () => {
        modalInstance.hide()
    }

    const deleteEvent = async () => {
        closeModal()

        await eventStore.deleteEvent(selected.value)
        events.value = await eventStore.getMyEvents()

        selected.value = null
    }

    const withdraw = async (id) => {
        await applicationStore.deleteApplication(id)
        applications.value = await applicationStore.getMyApplications()
    }
</script>
<template>
    <h1>Profile</h1>
    <p class="text-center text-secondary fs-1 mt-3">Hello <span class="fw-semibold">{{ authStore.user.username }}!</span></p>

    <section class="bg-body-tertiary rounded mb-3 p-3 position-relative">

        <button id="edit-button" class="btn btn-outline-secondary position-absolute">
            <i class="fa-solid fa-pen-to-square"></i>
        </button>

        <h2 class="h4 text-body-tertiary">User info:</h2>

        <ul class="list-group list-group-flush">
            <li class="list-group-item">
                <!--<i class="fa-regular fa-calendar"></i>-->
                <i class="fa-solid fa-cake-candles"></i>
                Date of birth:
                {{ authStore.user.birthDate }}
            </li>
            <li class="list-group-item">
                <!-- <i class="fa-solid fa-envelope"></i> -->
                <i class="fa-solid fa-at"></i>
                Email address:
                {{ authStore.user.email }}
            </li>
            <li class="list-group-item">
                <i class="fa-solid fa-calendar"></i>
                Date of register:
                {{ authStore.user.registerDate.split('T')[0] }}
            </li>
        </ul>
        
    </section>
    
    <section>
        <h2 class="text-center mb-3">Your applications:</h2>
        <ul class="list-group" v-if="applications.length > 0">
            <li class="list-group-item d-flex justify-content-between py-3" v-for="a in applications">
                <div>
                    <RouterLink :to="`/event/${a.eventId}`">
                        <h5>{{ a.title }}</h5>
                    </RouterLink>

                    <p class="text-body-tertiary">You applied at: {{ a.dateOfApplication }}</p>

                    <div class="fs-5">
                        <div class="mb-2">
                            Starts at:
                            <time>{{ a.startDate }}</time>
                        </div>
                        <div>
                            Ends at:
                            <time>{{ a.endDate }}</time>
                        </div>
                    </div>
                </div>
                <div class="d-flex flex-column justify-content-center">
                    <button class="btn btn-danger" @click="withdraw(a.id)">Withdraw</button>
                </div>
            </li>
        </ul>

        <p v-else class="text-body-secondary text-center fs-5">You haven't applied to any events yet.</p>
    </section>

    <section class="bg-body-tertiary mt-3 p-3 rounded">
        <h2>Your events</h2>
        <p class="text-body-tertiary">Click on one of the items to be able to edit or delete them.</p>

        <ul class="list-group">
            <!-- When clicking on an element, the 'active' Bootstrap class is added to it -->
            <li :class="`list-group-item${selected === e.id ? ' active' : ''}`" v-for="e in events" :key="e.id" @click="selected = e.id">
                <h5>{{ e.title }}</h5>
                <p class="mb-1"><i class="fa-regular fa-calendar"></i> Created: {{ e.dateCreated.split('T')[0] }}</p>
            </li>
        </ul>

        <div class="mt-2">
            <button class="btn btn-primary me-2 fs-5" :disabled="selected == null">Edit</button>
            <button class="btn btn-outline-danger fs-5" :disabled="selected == null" @click="openModal">Delete</button>
        </div>
    </section>

    <Teleport to="body">
        <div class="modal" tabindex="-1" ref="modal">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-danger">Warning!</h5>
                        <button class="btn-close" type="button" @click="closeModal"></button>
                    </div>
                    <div class="modal-body">
                        <p>Are you sure you want to delete this event?</p>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-secondary" @click="closeModal">Cancel</button>
                        <button class="btn btn-outline-danger" @click="deleteEvent">Yes</button>
                    </div>
                </div>
            </div>
        </div>
    </Teleport>
</template>
<style scoped>
    #edit-button { right: 10px; top: 5px }
</style>