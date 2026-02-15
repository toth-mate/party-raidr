<script setup>
  import { ref } from 'vue'

  const events = ref([
    {
      id: 1,
      title: 'Event 1',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec a diam lectus. Sed sit amet ipsum mauris. Maecenas congue ligula ac quam viverra nec consectetur ante hendrerit. Donec et mollis dolor.',
      startingDate: new Date(),
      endingDate: new Date(),
      placeName: 'Place 1',
      city: 'City 1',
      categoryName: 'Party',
      room: 0,
      ticketPrice: 0,
      status: 1
    },
    {
      id: 2,
      title: 'Event 2',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec a diam lectus. Sed sit amet ipsum mauris. Maecenas congue ligula ac quam viverra nec consectetur ante hendrerit. Donec et mollis dolor.',
      startingDate: new Date(),
      endingDate: new Date(),
      placeName: 'Place 2',
      city: 'City 2',
      categoryName: 'Party',
      room: 0,
      ticketPrice: 3500,
      status: 2
    },
    {
      id: 3,
      title: 'Event 3',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec a diam lectus. Sed sit amet ipsum mauris. Maecenas congue ligula ac quam viverra nec consectetur ante hendrerit. Donec et mollis dolor.',
      startingDate: new Date(),
      endingDate: new Date(),
      placeName: 'Place 3',
      city: 'City 3',
      categoryName: 'Party',
      room: 0,
      ticketPrice: 0,
      status: 0
    },
    {
      id: 4,
      title: 'Event 4',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec a diam lectus. Sed sit amet ipsum mauris. Maecenas congue ligula ac quam viverra nec consectetur ante hendrerit. Donec et mollis dolor.',
      startingDate: new Date(),
      endingDate: new Date(),
      placeName: 'Place 4',
      city: 'City 4',
      categoryName: 'Party',
      room: 0,
      ticketPrice: 0,
      status: 3
    },
    {
      id: 5,
      title: 'Event 5',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec a diam lectus. Sed sit amet ipsum mauris. Maecenas congue ligula ac quam viverra nec consectetur ante hendrerit. Donec et mollis dolor.',
      startingDate: new Date(),
      endingDate: new Date(),
      placeName: 'Place 5',
      city: 'City 5',
      categoryName: 'Party',
      room: 0,
      ticketPrice: 0,
      status: 0
    },
    {
      id: 6,
      title: 'Event 6',
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec a diam lectus. Sed sit amet ipsum mauris. Maecenas congue ligula ac quam viverra nec consectetur ante hendrerit. Donec et mollis dolor.',
      startingDate: new Date(),
      endingDate: new Date(),
      placeName: 'Place 6',
      city: 'City 6',
      categoryName: 'Party',
      room: 0,
      ticketPrice: 0,
      status: 0
    }
  ])

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
    <div class="row gx-5">
      <div v-for="e in events" class="card col-12 col-md-6 col-lg-4 mb-3">
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
          <button class="btn btn-primary">Details</button>
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
