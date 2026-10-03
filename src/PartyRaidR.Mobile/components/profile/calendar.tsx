import { getVisibleDates } from '@/helpers/dateHelper';
import { useCalendar } from '@/hooks/use-event-queries';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { StyleProp, StyleSheet, ViewStyle } from 'react-native';
import { Calendar, DateData } from 'react-native-calendars';
import { ThemedText } from '../themed-text';
import { ThemedView } from '../themed-view';

const ActivityCalendar = ({ style }: { style?: StyleProp<ViewStyle> }) => {
  const { t } = useTranslation();
  const [visibleDates, setVisibleDates] = useState<string[]>([]);

  const { data } = useCalendar(
    visibleDates[0],
    visibleDates[visibleDates.length - 1],
  );
  console.log(data);

  return (
    <ThemedView style={style}>
      <ThemedText>{t('tabs.profile.calendar.description')}</ThemedText>
      <Calendar
        i18nIsDynamicList
        onMonthChange={(dateData: DateData) =>
          setVisibleDates(getVisibleDates(dateData.dateString))
        }
      />
    </ThemedView>
  );
};

export default ActivityCalendar;

const styles = StyleSheet.create({});
