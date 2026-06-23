import React, { useEffect, useState } from 'react';
import { ActivityIndicator, StyleSheet } from 'react-native';
import { Link, Stack, useLocalSearchParams } from 'expo-router';
import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import { eventService } from '@/services/eventService';
import { EventDisplayDto } from '@/types/event.types';
import { Colors } from '@/constants/theme';
import ColoredLink from '@/components/colored-link';
import { useThemeColor } from '@/hooks/use-theme-color';
import ThemedButton from '@/components/themed-button';

const EventDetails = () => {
  const { id } = useLocalSearchParams<{ id: string }>();
  const [isLoading, setIsLoading] = useState(true);
  const [event, setEvent] = useState<EventDisplayDto | undefined>(undefined);
  const contentBackgroundColor = useThemeColor({}, 'inputFieldBackground');

  useEffect(() => {
    const fetchEvent = async () => {
        const result = await eventService.getDisplayById(id);
        setEvent(result);
    };
    fetchEvent();
    setIsLoading(false);
  }, []);

  return (
    <ThemedView style={styles.container}>
        <Stack.Screen
            options={{
                headerShown: true,
                title: event ? event.title : 'Event details...',
                headerTitleStyle: { fontWeight: '600' },
            }}
        />
        {isLoading ? (
            <ActivityIndicator size="large" color={Colors.primary} />
        ) : !event ? (
            <ThemedView style={styles.errorContainer}>
                <ThemedText>Failed to load event.</ThemedText>
                <ColoredLink href="/browse" replace>Go back</ColoredLink>
            </ThemedView>
        ) : (
            <ThemedView style={[styles.content, { backgroundColor: contentBackgroundColor }]}>
                <ThemedText type="subtitle">{event.title}</ThemedText>
                <ThemedText style={styles.description}>{event.description}</ThemedText>
                <ThemedText>Location: {event.city}, {event.placeName}</ThemedText>
                <ThemedView style={styles.dateContainer}>
                  <ThemedText>From: {event.startingDate}</ThemedText>
                  <ThemedText>To: {event.endingDate}</ThemedText>
                </ThemedView>
                <ThemedText>Max Room: {event.room}</ThemedText>
                <ThemedText style={{color: '#888'}}>Organizer: {event.authorName}</ThemedText>
                <ThemedText style={{color: '#888'}}>Created: {event.dateCreated}</ThemedText>
                <ThemedButton
                  onPress={() => console.log('Apply')}
                  title="Apply"
                  variant="primary"
                />
            </ThemedView>
        )}
    </ThemedView>
  );
}

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
})