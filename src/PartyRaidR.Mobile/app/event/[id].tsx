import React, { useEffect, useState } from 'react';
import { ActivityIndicator, StyleSheet } from 'react-native';
import { Link, Stack, useLocalSearchParams } from 'expo-router';
import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import { eventService } from '@/services/eventService';
import { EventDisplayDto } from '@/types/event.types';
import { Colors } from '@/constants/theme';
import ColoredLink from '@/components/colored-link';

const EventDetails = () => {
  const { id } = useLocalSearchParams<{ id: string }>();
  const [isLoading, setIsLoading] = useState(true);
  const [event, setEvent] = useState<EventDisplayDto | undefined>(undefined);

  useEffect(() => {
    const fetchEvent = async () => {
        const result = await eventService.getDisplayById(id);
        //setEvent(result);
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
            <ThemedText>{event.title}</ThemedText>
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
    errorContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        marginTop: -50,
        gap: 2,
    },
})