import { StyleSheet } from 'react-native';
import React from 'react';
import { UpcomingEventDto } from '@/types/event.types';
import { ThemedText } from './themed-text';
import { ThemedView } from './themed-view';
import { IconSymbol } from './ui/icon-symbol';
import { Colors } from '@/constants/theme';

const EventCard = ({ event }: { event: UpcomingEventDto }) => {
  const date = new Date(event.startTime);
  return (
    <ThemedView style={styles.container}>
      <ThemedText type="defaultSemiBold">{event.title}</ThemedText>
      <ThemedText>Starts at: {date.toLocaleDateString()} {date.toLocaleTimeString().slice(0, 5)}</ThemedText>
      <ThemedText>Location: {event.cityName}, {event.placeName}</ThemedText>
    </ThemedView>
  )
};

export default EventCard;

const styles = StyleSheet.create({
    container: {
        padding: 10,
        borderRadius: 10,
        marginBottom: 5,
        borderWidth: 1,
        borderColor: '#ccc',
    },
});