<script setup>
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useEventStore } from '@/stores/event'
import { useApplicationStore } from '@/stores/application'

const route = useRoute()
const eventStore = useEventStore()
const applicationStore = useApplicationStore()

let event = ref({})
const hasApplied = ref(false)
const eventId = route.params.id

async function applyToEvent() {
    await applicationStore.apply(eventId)
    hasApplied.value = await applicationStore.applicationExists(eventId)
}

onMounted(async () => {
    event.value = await eventStore.getEventDisplay(eventId)
    hasApplied.value = await applicationStore.applicationExists(eventId)
})
</script>
<template>
    <div class="container w-75 border rounded p-4">
        <h3>{{ event.title || 'Event title' }}</h3>
        <p class="text-body-secondary">{{ event.description || 'Event description...' }}</p>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 col-md-6">
                    <p>Starting date: {{ event.startingDate }}</p>
                    <p>Ending date: {{ event.endingDate }}</p>
                    <p>Place: {{ event.placeName }}</p>
                    <p>City: {{ event.city || 'City' }}</p>
                </div>
                <div class="col-12 col-md-6">
                    <p>Category: {{ event.category }}</p>
                    <p>Room: {{ event.room === 0 ? 'Not limited' : event.room }}</p>
                    <p>Ticket price: {{ event.ticketPrice === 0 ? 'Not paid' : event.ticketPrice + ' Ft' }}</p>
                </div>
            </div>
        </div>
        <button class="btn btn-success w-100 mt-2" :disabled="hasApplied" @click="applyToEvent">Apply</button>
    </div>
</template>