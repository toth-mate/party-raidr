import { StyleSheet, Text, View } from 'react-native';
import React from 'react';
import { EventMarkerDto } from '@/types/event.types';
import { Link } from 'expo-router';
import { Colors } from '@/constants/theme';

type MarkerContentProps = {
  event: EventMarkerDto;
}

const MarkerContent = ({ event }: MarkerContentProps) => {
  return (
    <View style={styles.container}>
      <Text style={{fontWeight: 'bold'}}>{event.title}</Text>
      <Text>{event.address}</Text>
      <Text style={styles.tooltip}>Tap here to view details</Text>
    </View>
  )
};

export default MarkerContent;

const styles = StyleSheet.create({
  container: {
    backgroundColor: Colors.light.background,
    padding: 10,
    borderRadius: 10,
    boxShadow: '2px 4px 6px rgba(0, 0, 0, 0.3)',
  },
  tooltip: {
    color: '#777',
    marginTop: 3,
  },
});