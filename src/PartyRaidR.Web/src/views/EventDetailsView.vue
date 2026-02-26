<script setup>
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useEventStore } from '@/stores/event'

const route = useRoute()
const eventStore = useEventStore()

let event = ref({})
const eventId = route.params.id

onMounted(async () => {
    event.value = await eventStore.getEvent(eventId)
    console.log(event)
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
                    <p>Place: {{ event.placeId }}</p>
                    <p>City: {{ event.cityName || 'City' }}</p>
                </div>
                <div class="col-12 col-md-6">
                    <p>Category: {{ event.category }}</p>
                    <p>Room: {{ event.room }}</p>
                    <p>Ticket price: {{ event.ticketPrice }} Ft</p>
                </div>
            </div>
        </div>
        <button class="btn btn-success w-100 mt-2">Apply</button>
    </div>
</template>