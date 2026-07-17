import { StyleSheet } from 'react-native';
import React from 'react';
import MapView, { Marker } from 'react-native-maps';

import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';

const Map = () => {
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
            <Marker coordinate={{ latitude: 46.253, longitude: 20.148 }} title='Event name' description='Elm street 401.' />
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