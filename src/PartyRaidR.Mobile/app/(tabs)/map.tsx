import { StyleSheet } from 'react-native';
import React from 'react';

import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';

const Map = () => {
  return (
    <ThemedView safe>
        <ThemedText>Map</ThemedText>
    </ThemedView>
  )
}

export default Map;

const styles = StyleSheet.create({});