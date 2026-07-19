import { StyleSheet, View } from 'react-native';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { useEffect, useState } from 'react';
import { UpcomingEventDto } from '@/types/event.types';
import { eventService } from '@/services/eventService';
import { useThemeColor } from '@/hooks/use-theme-color';
import { Collapsible } from '@/components/ui/collapsible';
import EventCard from '@/components/event-card';
import ThemedButton from '@/components/themed-button';

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
          <ThemedText type="subtitle" style={{ marginBottom: 5 }}>
            Upcoming events:
          </ThemedText>

          <Collapsible title="Show/Hide upcoming events" defaultOpen>
            {upcomingEvents.map((event) => (
              <EventCard key={event.id} event={event}/>
            ))}
          </Collapsible>
        </View>

        <ThemedView>
          <ThemedText type="title" centered>Join our community!</ThemedText>
          <View style={styles.buttonContainer}>
            <ThemedButton title="Register" onPress={() => null} style={styles.button} />
            <ThemedButton title="Login" onPress={() => null} outline style={styles.button} />
          </View>
        </ThemedView>
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
    marginBottom: 20,
  },
  buttonContainer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    marginTop: 8,
  },
  button: {
    width: '45%',
  },
});