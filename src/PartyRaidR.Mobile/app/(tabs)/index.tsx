import { StyleSheet, View } from 'react-native';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { useEffect, useState } from 'react';
import { UpcomingEventDto } from '@/types/event.types';
import { eventService } from '@/services/eventService';
import { useThemeColor } from '@/hooks/use-theme-color';

export default function HomeScreen() {
  const [upcomingEvents, setUpcomingEvents] = useState<UpcomingEventDto[]>([]);
  const panelBgColor = useThemeColor({}, 'inputFieldBackground');

  useEffect(() => {
    const fetchUpcomingEvents = async () => {
      const result = await eventService.getUpcomingEvents();
      setUpcomingEvents(result);
    };

    fetchUpcomingEvents();
  }, []);

  return (
    <ThemedView safe>
      <ThemedView style={styles.container}>
        <ThemedText style={styles.title}>
          Welcome!
        </ThemedText>
        <View style={[styles.upcomingEventsContainer, { backgroundColor: panelBgColor }]}>
          <ThemedText type="subtitle">
            Upcoming events:
          </ThemedText>
          {upcomingEvents.map((event) => (
            <View key={event.id}>
              <ThemedText>{event.title}</ThemedText>
            </View>
          ))}
        </View>
      </ThemedView>
    </ThemedView>
  );
};

const styles = StyleSheet.create({
  container: {
    paddingHorizontal: 5,
    paddingVertical: 10,
  },
  title: {
    fontSize: 24,
    fontWeight: '200',
  },
  upcomingEventsContainer: {
    padding: 10,
    borderRadius: 10,
    marginTop: 10,
  },
});