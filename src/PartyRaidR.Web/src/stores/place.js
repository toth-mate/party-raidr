import { ref } from 'vue'
import { defineStore } from 'pinia'
import placeService from '@/api/placeService'

export const usePlaceStore = defineStore('place', () => {
  const places = ref([])

  async function loadPlaces() {
    try {
        const res = await placeService.getAll()

        if(res.data) {
            places.value = res.data
        }
    } catch(e) {
        console.warn(e)
    }
  }

  return { places, loadPlaces }
})
