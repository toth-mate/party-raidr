import { useRouter } from 'expo-router';
import React, { useState } from 'react';
import { StyleSheet } from 'react-native';
import MapView, { Callout, Marker, Region } from 'react-native-maps';

import MarkerContent from '@/components/marker-content';
import { ThemedView } from '@/components/themed-view';
import { useMapEvents } from '@/hooks/use-event-queries';
import { BoundingBox } from '@/types/event.types';

const INITIAL_REGION: Region = {
  latitude: 46.253,
  longitude: 20.148,
  latitudeDelta: 0.0922,
  longitudeDelta: 0.0421,
};

const Map = () => {
  const [bounds, setBounds] = useState<BoundingBox | null>(null);
  const { data: events } = useMapEvents(bounds);
  const router = useRouter();

  const handleRegionChange = (newRegion: Region) => {
    if (newRegion.latitudeDelta > 0.5) {
      return;
    }

    // Calculate the bounding box
    const minLat = newRegion.latitude - newRegion.latitudeDelta / 2,
      maxLat = newRegion.latitude + newRegion.latitudeDelta / 2,
      minLng = newRegion.longitude - newRegion.longitudeDelta / 2,
      maxLng = newRegion.longitude + newRegion.longitudeDelta / 2;

    setBounds({ minLat, maxLat, minLng, maxLng });
  };

  return (
    <ThemedView style={styles.container}>
      <MapView
        initialRegion={INITIAL_REGION}
        onRegionChangeComplete={handleRegionChange}
        rotateEnabled={false}
        showsPointsOfInterest={false}
        style={styles.map}>
        {events?.map(marker => (
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
