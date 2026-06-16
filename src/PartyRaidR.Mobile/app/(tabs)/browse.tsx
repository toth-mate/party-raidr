import { useEffect, useState } from 'react';

import { eventService } from '@/services/eventService';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { EventDisplayDto } from '@/types/event.types';
import { FlatList } from 'react-native';

export default function BrowseScreen() {
  const [events, setEvents] = useState<EventDisplayDto[]>();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setIsLoading(true);

    eventService.getAllDisplay()
      .then((data) => setEvents(data));

    setIsLoading(false);
  }, []);

  const renderItem = (item: EventDisplayDto) => {
    return (
      <ThemedText>
        {item.title}
      </ThemedText>
    )
  };

  return (
    <>
      <ThemedView safe={true}>
        <FlatList
          data={events}
          renderItem={({item}) => renderItem(item)}
          keyExtractor={(item) => item.id}
          />
      </ThemedView>
    </>
  );
}