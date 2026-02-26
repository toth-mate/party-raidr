import { ref } from 'vue'
import { defineStore } from 'pinia'
import eventService from '@/api/eventService'

export const useEventStore = defineStore('event', () => {
  const events = ref([])

  async function loadEvents() {
    try {
      const res = await eventService.get()

      if(res.data) {
        events.value = res.data
      }
    } catch(e) {
      console.warn(e)
    }
  }

    return { events, loadEvents }
})
