<script setup>
  import { ref, onMounted } from 'vue'
  import { useRouter } from 'vue-router'
  import { useEventStore } from '@/stores/event'

  const router = useRouter()
  const eventStore = useEventStore()

  onMounted(async () => {
    await eventStore.loadEventsDisplay()
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
    <div class="row">
      <div class="input-group">
        <input type="text" class="form-control" placeholder="Search for events...">
        <button class="btn btn-primary">Search</button>
      </div>
    </div>
    <div class="row mt-2 bg-body-secondary px-1 py-2" v-if="!hideFilter">
      <h6>Advanced filtering</h6>
      <div class="mb-2 d-flex">
        <div class="form-floating flex-fill">
          <input type="text" class="form-control" placeholder="Search by event description" id="event-description">
          <label for="event-description">Search by event description</label>
        </div>
        <div class="form-floating flex-fill">
          <input type="text" class="form-control" placeholder="Search by place name" id="event-place-name">
          <label for="event-place-name">Search by place name</label>
        </div>
      </div>
      <div class="input-group mb-2">
        <span class="input-group-text">Start date</span>
        <input type="date" class="form-control form-control-lg" id="event-start-date">
        <span class="input-group-text">End date</span>
        <input type="date" class="form-control form-control-lg" id="event-end-date">
      </div>
      <div class="input-group mb-2">
        <span class="input-group-text">Minimum price</span>
        <input type="number" class="form-control" id="event-price-min" placeholder="0">
        <span class="input-group-text">Ft</span>
      </div>
      <div class="input-group mb-2">
        <span class="input-group-text">Maximum price</span>
        <input type="number" class="form-control" id="event-price-max" placeholder="0">
        <span class="input-group-text">Ft</span>
      </div>
      <div class="form-floating">
        <select class="form-select" id="event-category">
          <option value="0">None</option>
          <option value="1">Outdoors activity</option>
          <option value="2">Indoors activity</option>
          <option value="3">Concert</option>
          <option value="4">Festival</option>
          <option value="5">Party</option>
        </select>
        <label for="event-category">Category</label>
      </div>
    </div>
    <button class="btn btn-dark mt-2" v-if="hideFilter" @click="toggleFilter">More filtering options</button>
    <button class="btn btn-light mt-2" v-if="!hideFilter" @click="toggleFilter">Hide filtering options</button>
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
            <h6 class="card-subtitle text-body-secondary mb-2">{{ e.startingDate }} - {{ e.endingDate }}</h6>
            <p class="card-text">{{ e.description }}</p>
            <p class="card-text text-body-secondary">Here: {{ e.city }}, {{ e.placeName }}</p>
          </div>
          <div class="card-footer d-flex justify-content-between align-items-center">
            <p class="card-text" v-if="e.ticketPrice == 0">Price: Not paid</p>
            <p class="card-text" v-else>Price: {{ e.ticketPrice === 0 ? 'Not paid' : e.ticketPrice + ' Ft' }}</p>
            <button class="btn btn-secondary" @click="goToEventDetails(e.id)">Details</button>
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

  #event-description {
    border-top-right-radius: 0;
    border-bottom-right-radius: 0
  }
  #event-place-name {
    border-top-left-radius: 0;
    border-bottom-left-radius: 0;
    border-left: 0
  }
</style>
