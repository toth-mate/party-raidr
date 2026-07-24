import { useRouter } from 'expo-router';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { ScrollView, StyleSheet, View } from 'react-native';

import EventCard from '@/components/event-card';
import ThemedButton from '@/components/themed-button';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { Collapsible } from '@/components/ui/collapsible';
import { useThemeColor } from '@/hooks/use-theme-color';
import { eventService } from '@/services/eventService';
import { useAuthStore } from '@/store/useAuthStore';
import { useLocationStore } from '@/store/useLocationStore';
import { UpcomingEventDto } from '@/types/event.types';

const MAX_DISTANCE_IN_KM: number = 30;
const TRANSLATION_PREFIX = 'tabs.home.';

export default function HomeScreen() {
  const { t } = useTranslation();
  const router = useRouter();

  const [upcomingEvents, setUpcomingEvents] = useState<UpcomingEventDto[]>([]);
  const [nearbyEvents, setNearbyEvents] = useState<UpcomingEventDto[]>([]);

  const loadLocation = useLocationStore(state => state.loadLocation);
  const latitude = useLocationStore(state => state.lat),
    longitude = useLocationStore(state => state.lng);
  const isLoggedIn = useAuthStore(state => state.isAuthenticated);

  const backgroundColor = useThemeColor({}, 'background');
  const panelBgColor = useThemeColor({}, 'inputFieldBackground');
  const sublteTextColor = useThemeColor({}, 'icon');

  useEffect(() => {
    const fetchUpcomingEvents = async () => {
      const result = await eventService.getUpcomingEvents();
      setUpcomingEvents(result);
    };

    fetchUpcomingEvents();
    loadLocation();
  }, []);

  useEffect(() => {
    const fetchNearbyEvents = async () => {
      if (!latitude || !longitude) return;

      const result = await eventService.getNearbyEvents(
        latitude,
        longitude,
        MAX_DISTANCE_IN_KM,
      );
      setNearbyEvents(result);
    };

    fetchNearbyEvents();
  }, [latitude, longitude]);

  return (
    <ScrollView style={[styles.container, { backgroundColor }]}>
      <View
        style={[
          styles.upcomingEventsContainer,
          { backgroundColor: panelBgColor },
        ]}>
        <ThemedText
          type='subtitle'
          style={{ marginBottom: 5 }}>
          {t(`${TRANSLATION_PREFIX}upcoming`)}
        </ThemedText>

        <Collapsible
          title={t(`${TRANSLATION_PREFIX}showHideUpcoming`)}
          defaultOpen>
          {upcomingEvents.map(event => (
            <EventCard
              key={event.id}
              event={event}
            />
          ))}
        </Collapsible>
      </View>

      {!isLoggedIn && (
        <ThemedView>
          <ThemedText
            type='title'
            centered>
            {t(`${TRANSLATION_PREFIX}community`)}
          </ThemedText>
          <View style={styles.buttonContainer}>
            <ThemedButton
              title={t(`${TRANSLATION_PREFIX}register`)}
              onPress={() => router.push('/')}
              style={styles.button}
            />
            <ThemedButton
              title={t(`${TRANSLATION_PREFIX}login`)}
              onPress={() => router.push('/login')}
              outline
              style={styles.button}
            />
          </View>
        </ThemedView>
      )}

      <View
        style={[
          styles.upcomingEventsContainer,
          { backgroundColor: panelBgColor },
        ]}>
        <ThemedText
          type='subtitle'
          style={{ marginBottom: 5 }}>
          {t(`${TRANSLATION_PREFIX}nearby`)}
        </ThemedText>
        <ThemedText style={[styles.description, { color: sublteTextColor }]}>
          {t(`${TRANSLATION_PREFIX}inRadius`)}
        </ThemedText>

        <Collapsible
          title={t(`${TRANSLATION_PREFIX}showHideNearby`)}
          defaultOpen>
          {nearbyEvents.map(event => (
            <EventCard
              key={event.id}
              event={event}
            />
          ))}
        </Collapsible>
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    paddingHorizontal: 5,
    paddingVertical: 10,
    flex: 1,
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
  description: {
    marginBottom: 5,
    fontSize: 14,
  },
});
