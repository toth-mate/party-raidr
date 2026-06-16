import { useEffect, useState } from 'react';
import { StyleSheet, FlatList, View, ActivityIndicator } from 'react-native';

import { EventDisplayDto } from '@/types/event.types';
import { eventService } from '@/services/eventService';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { Colors } from '@/constants/theme';

export default function BrowseScreen() {
  const [events, setEvents] = useState<EventDisplayDto[]>();
  const [isLoading, setIsLoading] = useState(true);
  const [isRefreshing, setIsRefreshing] = useState(false);

  const loadData = async () => {
    const data = await eventService.getAllDisplay();
    setEvents(data);
  };

  const handleRefresh = async () => {
    setIsRefreshing(true);
    await loadData();
    setIsRefreshing(false);
  };

  useEffect(() => {
    loadData().finally(() => setIsLoading(false));
  }, []);

  const renderItem = (item: EventDisplayDto) => {
    // Titles above 20 characters are shortened for more convenient display.
    const renderTitle = item.title.length < 20 ? item.title : item.title.slice(0, 20).trim().concat('...');

    return (
      <ThemedView style={styles.listItem}>
        <View style={styles.listItemHeader}>
          <ThemedText type="subtitle"
            style={styles.eventTitle}>
            {renderTitle}
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

  if(isLoading) {
    return <ActivityIndicator size="large" color={Colors.primary} />;
  }

  return (
    <ThemedView safe={true}>
      <ThemedText style={styles.descriptionText}>
        Tap on an event to view its details.
      </ThemedText>
      <FlatList
        data={events}
        renderItem={({item}) => renderItem(item)}
        keyExtractor={(item) => item.id}
        refreshing={isRefreshing}
        onRefresh={handleRefresh}
        />
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  descriptionText: {
    marginTop: 10,
    marginBottom: 15,
    textAlign: 'center',
  },
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