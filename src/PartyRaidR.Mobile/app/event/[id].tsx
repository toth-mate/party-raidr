import { Stack, useLocalSearchParams } from 'expo-router';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { ActivityIndicator, StyleSheet } from 'react-native';

import ColoredLink from '@/components/colored-link';
import ThemedButton from '@/components/themed-button';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { Colors } from '@/constants/theme';
import { useApply } from '@/hooks/use-apply';
import { useThemeColor } from '@/hooks/use-theme-color';
import { applicationService } from '@/services/applicationService';
import { eventService } from '@/services/eventService';
import { useAuthStore } from '@/store/useAuthStore';
import { EventDisplayDto } from '@/types/event.types';
import { useQuery } from '@tanstack/react-query';

const TRANSLATION_PREFIX = 'screens.event.';

const EventDetails = () => {
  const { t } = useTranslation();
  const { id: eventId } = useLocalSearchParams<{ id: string }>();
  const [isLoading, setIsLoading] = useState(true);
  const [event, setEvent] = useState<EventDisplayDto | undefined>(undefined);
  const contentBackgroundColor = useThemeColor({}, 'inputFieldBackground');
  const applyMutation = useApply();
  const user = useAuthStore(state => state.user);

  const { data: applicationExists } = useQuery({
    queryKey: ['application', 'exists', eventId, user?.id],
    queryFn: () => applicationService.exists(eventId),
    enabled: !!user && !!eventId,
  });

  const cannotApply =
    applicationExists ||
    isLoading ||
    user?.username === event?.authorName;

  useEffect(() => {
    setIsLoading(true);
    const fetchEvent = async () => {
      const result = await eventService.getDisplayById(eventId);
      setEvent(result);
    };

    fetchEvent();
    setIsLoading(false);
  }, [eventId]);

  return (
    <ThemedView style={styles.container}>
      <Stack.Screen
        options={{
          headerShown: true,
          title: event ? event.title : t(`${TRANSLATION_PREFIX}fallbackTitle`),
          headerTitleStyle: { fontWeight: '600' },
        }}
      />
      {isLoading ? (
        <ActivityIndicator
          size='large'
          color={Colors.primary}
        />
      ) : !event ? (
        <ThemedView style={styles.errorContainer}>
          <ThemedText>{t(`${TRANSLATION_PREFIX}loadFail`)}</ThemedText>
          <ColoredLink
            href='/browse'
            replace>
            {t(`${TRANSLATION_PREFIX}goBack`)}
          </ColoredLink>
        </ThemedView>
      ) : (
        <ThemedView
          style={[styles.content, { backgroundColor: contentBackgroundColor }]}>
          <ThemedText type='subtitle'>{event.title}</ThemedText>
          <ThemedText style={styles.description}>
            {event.description}
          </ThemedText>
          <ThemedText>
            {t(`${TRANSLATION_PREFIX}location`)}:{' '}
            <ThemedText style={styles.location}>
              {event.city}, {event.placeName}
            </ThemedText>
          </ThemedText>
          <ThemedView style={styles.dateContainer}>
            <ThemedText>
              {t(`${TRANSLATION_PREFIX}from`)}: {event.startingDate}
            </ThemedText>
            <ThemedText>
              {t(`${TRANSLATION_PREFIX}to`)}: {event.endingDate}
            </ThemedText>
          </ThemedView>
          <ThemedText>
            {t(`${TRANSLATION_PREFIX}maxRoom`)}: {event.room}
          </ThemedText>
          <ThemedText style={{ color: '#888' }}>
            {t(`${TRANSLATION_PREFIX}organizer`)}: {event.authorName}
          </ThemedText>
          <ThemedText style={{ color: '#888' }}>
            {t(`${TRANSLATION_PREFIX}createdDate`)}: {event.dateCreated}
          </ThemedText>
          <ThemedButton
            onPress={() =>
              applyMutation.mutate({
                id: '',
                userId: user?.id ?? '',
                eventId: eventId,
                timeOfApplication: new Date().toISOString(),
                status: 0,
              })
            }
            title={t(`${TRANSLATION_PREFIX}applyButton`)}
            variant='primary'
            disabled={cannotApply}
          />
        </ThemedView>
      )}
    </ThemedView>
  );
};

export default EventDetails;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 15,
  },
  content: {
    gap: 10,
    padding: 15,
    borderRadius: 10,
  },
  description: {
    marginBottom: 10,
    color: '#888',
  },
  location: {
    fontWeight: '600',
  },
  dateContainer: {
    alignItems: 'center',
    marginTop: 10,
    marginBottom: 10,
    padding: 10,
    gap: 5,
    borderRadius: 15,
  },
  errorContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: -50,
    gap: 2,
  },
});
