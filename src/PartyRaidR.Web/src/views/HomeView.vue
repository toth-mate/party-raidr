<script setup>
  import { ref, onMounted } from 'vue'
  import { useRouter } from 'vue-router'
  import { useEventStore } from '@/stores/event'

  const router = useRouter()
  const eventStore = useEventStore()

  onMounted(async () => {
    await eventStore.loadEvents()
  })

  const goToEventDetails = (id) => {
    router.push(`/events/${id}`)
  }

  const hideFilter = ref(true)

  const toggleFilter = () => {
    hideFilter.value = !hideFilter.value
  }
</script>

<template>
  <h1>Party RaidR</h1>
  <section id="search" class="container mb-5">
    <div class="input-group">
      <input type="text" class="form-control" placeholder="Search for events...">
      <button class="btn btn-primary">Search</button>
    </div>
    <button class="btn btn-secondary mt-2" v-if="hideFilter" @click="toggleFilter">More filtering options</button>
    <button class="btn btn-secondary mt-2" v-if="!hideFilter" @click="toggleFilter">Hide filtering options</button>
  </section>
  <section id="event-list" class="container">
    <div class="row gy-4">
      <div class="col-12 col-md-6 col-lg-4 col-xl-3" v-for="e in eventStore.events" :key="e.id">
        <div class="card">
          <div class="card-body position-relative">
            <div id="status" class="position-absolute">
              <div v-if="e.status === 3" class="bg-danger"></div>
              <div v-else-if="e.status === 2" class="bg-success"></div>
              <div v-else-if="e.status === 1" class="bg-warning"></div>
              <div v-else class="bg-secondary"></div>
            </div>
            <h5 class="card-title">{{ e.title }}</h5>
            <h6 class="card-subtitle text-body-secondary mb-2">{{ e.startingDate }}-{{ e.endingDate }}</h6>
            <p class="card-text">{{ e.description }}</p>
            <p class="card-text text-body-secondary">Here: {{ e.city }}, {{ e.placeName }}</p>
          </div>
          <div class="card-footer d-flex justify-content-between align-items-center">
            <p class="card-text" v-if="e.ticketPrice == 0">Price: Not paid</p>
            <p class="card-text" v-else>Price: {{ e.ticketPrice }}</p>
            <button class="btn btn-primary" @click="goToEventDetails(e.id)">Details</button>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
<style scoped>
  #status {
    border-radius: 50%;
    margin-bottom: 10px;
    right: 10px
  }
  #status div {
    border-radius: 50%;
    padding: 5px
  }
</style>
