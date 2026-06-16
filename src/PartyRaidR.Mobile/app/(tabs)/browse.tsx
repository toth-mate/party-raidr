import { useEffect, useState } from 'react';
import { StyleSheet, FlatList, View } from 'react-native';

import { EventDisplayDto } from '@/types/event.types';
import { eventService } from '@/services/eventService';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { Colors } from '@/constants/theme';

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
      <ThemedView style={styles.listItem}>
        <View style={styles.listItemHeader}>
          <ThemedText type="subtitle"
            style={styles.eventTitle}>
            {item.title}
          </ThemedText>
          <ThemedText style={styles.eventDate}>
            {item.dateCreated}
          </ThemedText>
        </View>
        <ThemedText>
          {item.description}
        </ThemedText>
      </ThemedView>
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

const styles = StyleSheet.create({
  listItem: {
    borderWidth: 1,
    borderColor: '#999',
    borderRadius: 5,
    padding: 10,
    marginBottom: 10,
  },
  listItemHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  eventTitle: {
    color: Colors.secondary,
    marginBottom: 5,
  },
  eventDate: {
    color: '#999',
    fontSize: 12,
  },
});