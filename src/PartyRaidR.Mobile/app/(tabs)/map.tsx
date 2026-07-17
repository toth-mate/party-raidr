import { StyleSheet } from 'react-native';
import React from 'react';
import MapView from 'react-native-maps';

import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';

const Map = () => {
  return (
    <ThemedView style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
        <MapView
            initialRegion={{
                latitude: 46.253,
                longitude: 20.148,
                latitudeDelta: 0.0922,
                longitudeDelta: 0.0421,
            }}
            style={styles.map}
/>
    </ThemedView>
  )
}

export default Map;

const styles = StyleSheet.create({
    map: {
        width: '100%',
        height: '100%',
    }
});