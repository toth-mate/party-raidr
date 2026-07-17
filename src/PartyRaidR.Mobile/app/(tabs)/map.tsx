import { StyleSheet } from 'react-native';
import React, { useEffect, useState } from 'react';
import MapView, { Marker } from 'react-native-maps';

import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import { eventService } from '@/services/eventService';
import { EventMarkerDto } from '@/types/event.types';

const Map = () => {
  const [events, setEvents] = useState<EventMarkerDto[]>([]);

  useEffect(() => {
    const fetchEvents = async() => {
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
            style={styles.map}
        >
            {events.map((marker) => (
                <Marker
                    key={marker.id}
                    coordinate={{
                        latitude: marker.latitude,
                        longitude: marker.longitude
                    }}
                    title={marker.title}
                />
            ))}
        </MapView>
    </ThemedView>
  )
}

export default Map;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center'
    },
    map: {
        width: '100%',
        height: '100%',
    },
});