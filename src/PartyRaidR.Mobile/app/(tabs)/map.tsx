import { StyleSheet } from 'react-native';
import React, { useEffect, useState } from 'react';
import MapView, { Callout, Marker } from 'react-native-maps';
import { useRouter } from 'expo-router';

import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import { eventService } from '@/services/eventService';
import { EventMarkerDto } from '@/types/event.types';
import MarkerContent from '@/components/marker-content';

const Map = () => {
  const router = useRouter();
  const [events, setEvents] = useState<EventMarkerDto[]>([]);

  useEffect(() => {
    const fetchEvents = async () => {
      const result = await eventService.getMarkerEvents();
      setEvents(result);
    };

    fetchEvents();
  }, []);

  return (
    <ThemedView style={styles.container}>
      <MapView
        initialRegion={{
          latitude: 46.253,
          longitude: 20.148,
          latitudeDelta: 0.0922,
          longitudeDelta: 0.0421,
        }}
        rotateEnabled={false}
        showsPointsOfInterest={false}
        style={styles.map}>
        {events.map(marker => (
          <Marker
            key={marker.id}
            coordinate={{
              latitude: marker.latitude,
              longitude: marker.longitude,
            }}
            title={marker.title}
            description={marker.address}>
            <Callout
              onPress={() => router.navigate(`/event/${marker.id}`)}
              tooltip>
              <MarkerContent event={marker} />
            </Callout>
          </Marker>
        ))}
      </MapView>
    </ThemedView>
  );
};

export default Map;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  map: {
    width: '100%',
    height: '100%',
  },
});
