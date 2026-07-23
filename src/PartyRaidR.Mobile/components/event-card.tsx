import { StyleSheet } from 'react-native';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { UpcomingEventDto } from '@/types/event.types';
import { ThemedText } from './themed-text';
import { ThemedView } from './themed-view';
import { IconSymbol } from './ui/icon-symbol';
import { Colors } from '@/constants/theme';

const EventCard = ({ event }: { event: UpcomingEventDto }) => {
  const { t } = useTranslation();
  const date = new Date(event.startTime);
  return (
    <ThemedView style={styles.container}>
      <ThemedText type="defaultSemiBold">{event.title}</ThemedText>
      <ThemedText>{t('tabs.home.eventCard.start')}{date.toLocaleDateString()} {date.toLocaleTimeString().slice(0, 5)}</ThemedText>
      <ThemedText>{t('tabs.home.eventCard.start')}{event.cityName}, {event.placeName}</ThemedText>
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