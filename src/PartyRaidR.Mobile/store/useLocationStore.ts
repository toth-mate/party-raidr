import * as Location from 'expo-location';
import { Alert } from 'react-native';
import { create } from 'zustand';

import { LocationState } from '@/types/state.type';

export const useLocationStore = create<LocationState>(set => ({
  lat: undefined,
  lng: undefined,
  loadLocation: async () => {
    try {
      const { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== 'granted') {
        Alert.alert(
          'Permission to access location was denied.',
          'Please grant permission to access location for better experience.',
        );
        return;
      }
      const location = await Location.getCurrentPositionAsync({});
      set({ lat: location.coords.latitude, lng: location.coords.longitude });
    } catch (error) {
      console.error(`An error occured when loading location: ${error}`);
    }
  },
}));
