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

  async function getEvent(id) {
    try{
      const res = await eventService.getById(id)
      return res.data
    } catch(e) {
      console.warn(e)
    }
  }

  async function loadEventsDisplay() {
    try {
      const res = await eventService.getWithDetails()

      if(res.data) {
        events.value = res.data
      }
    } catch(e) {
      console.warn(e)
    }
  }

  async function getEventDisplay(id) {
    try {
      const res = await eventService.getWithDetailsById(id)
      console.log(res)
      return res.data
    } catch(e) {
      console.warn(e)
    }
  }

  return { events, loadEvents, getEvent, loadEventsDisplay, getEventDisplay }
})
