<script setup>
    import { ref, onMounted } from 'vue'
    import { useRouter } from 'vue-router'
    import { useEventStore } from '@/stores/event'
    import { usePlaceStore } from '@/stores/place'
    import { useAuthStore } from '@/stores/auth'

    const router = useRouter()

    const eventStore = useEventStore()
    const placeStore = usePlaceStore()
    const authStore = useAuthStore()
    
    const error = ref(false)
    const now = new Date()
    const event = ref({
        id: '',
        title: '',
        description: '',
        startingDate: now.toISOString(),
        endingDate: now.toISOString(),
        placeId: null,
        category: 0,
        authorId: authStore.user.id,
        room: 0,
        ticketPrice: 0,
        dateCreated: now.toISOString(),
        isActive: true,
        eventStatus: 0
    })

    async function create() {
        const res = await eventStore.createEvent(event.value)

        if(res) {
            router.push('/')
        } else {
            error.value = true
        }
    }

    onMounted(async () => {
        await placeStore.loadPlaces()
    })
</script>
<template>
    <h1>Create event</h1>
    <p class="text-center text-bg-danger w-75 mx-auto p-1" v-show="error">An error occured while trying to create the event.</p>
    <div class="w-75 mx-auto">
        <div class="mb-2">
            <label for="title" class="form-label">Title</label>
            <input type="text" class="form-control" id="title" placeholder="Event title" v-model="event.title">
        </div>
        <div class="mb-2">
            <label for="description" class="form-label">Description</label>
            <textarea class="form-control" id="description" placeholder="Event description" v-model="event.description"></textarea>
        </div>
        <div class="mb-2 d-flex justify-content-between">
            <div class="w-50 me-3">
                <label for="start-date" class="form-label">Start date</label>
                <input type="datetime-local" class="form-control" id="start-date" v-model="event.startingDate">
            </div>
            <div class="w-50">
                <label for="end-date" class="form-label">End date</label>
                <input type="datetime-local" class="form-control" id="end-date" v-model="event.endingDate">
            </div>
        </div>
        <div class="mb-2 d-flex justify-content-evenly">
            <div class="w-50 me-3">
                <label for="place" class="form-label">Place</label>
                <select id="place" class="form-select" v-model="event.placeId">
                    <option :value="p.id" v-for="p in placeStore.places">{{ p.name }}</option>
                </select>
            </div>
            <div class="w-50 me-3">
                <label for="category" class="form-label">Category</label>
                <select id="category" class="form-select" v-model="event.category">
                    <option value="0">None</option>
                    <option value="1">Outdoors activity</option>
                    <option value="2">Indoors activity</option>
                    <option value="3">Concert</option>
                    <option value="4">Festival</option>
                    <option value="5">Party</option>
                </select>
            </div>
        </div>
        <div class="mb-2">
            <label for="room" class="form-label">Max room</label>
            <input type="number" class="form-control" id="room" placeholder="250" v-model="event.room">
        </div>
        <div class="mb-2">
            <label for="price" class="form-label">Ticket price</label>
            <input type="number" class="form-control" id="price" placeholder="250" v-model="event.ticketPrice">
            <div class="form-text">Set ticket price '0' if there is no fee.</div>
        </div>
        <button class="btn btn-primary w-100" @click="create">Create</button>
    </div>
</template>