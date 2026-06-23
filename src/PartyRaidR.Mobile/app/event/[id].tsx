import React, { useEffect, useState } from 'react';
import { ActivityIndicator, StyleSheet } from 'react-native';
import { Link, Redirect, useLocalSearchParams } from 'expo-router';
import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import { eventService } from '@/services/eventService';
import { EventDisplayDto } from '@/types/event.types';
import { Colors } from '@/constants/theme';
import Toast from 'react-native-toast-message';

const EventDetails = () => {
  const { id } = useLocalSearchParams<{ id: string }>();
  const [isLoading, setIsLoading] = useState(true);
  const [event, setEvent] = useState<EventDisplayDto | undefined>(undefined);

  useEffect(() => {
    console.log(`ID: ${id}`);
    const fetchEvent = async () => {
        const result = await eventService.getDisplayById(id);
        setEvent(result);
    };
    fetchEvent();
    setIsLoading(false);
  }, []);

  if(isLoading) {
    return <ActivityIndicator size="large" color={Colors.primary} />
  }

  if(!event) {
    return (
      <ThemedView safe>
        <ThemedText>Failed to load event.</ThemedText>
        <Link href="/browse">Go back</Link>
      </ThemedView>
    );
  }

  return (
    <ThemedView safe>
      <ThemedText>{event.title}</ThemedText>
    </ThemedView>
  );
}

export default EventDetails;

const styles = StyleSheet.create({})