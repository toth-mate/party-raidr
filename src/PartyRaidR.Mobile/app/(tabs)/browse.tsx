import { useEffect, useState } from 'react';

import { eventService } from '@/services/eventService';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { EventDisplayDto } from '@/types/event.types';

export default function BrowseScreen() {
  const [events, setEvents] = useState<EventDisplayDto[]>();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setIsLoading(true);

    eventService.getAllDisplay()
      .then((data) => setEvents(data));

    setIsLoading(false);
  }, []);

  return (
    <>
      <ThemedView safe={true}>
        <ThemedText type="title">
          Browse
        </ThemedText>
      </ThemedView>
    </>
  );
}