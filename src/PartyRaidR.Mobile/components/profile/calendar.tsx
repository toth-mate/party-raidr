import { Colors } from '@/constants/theme';
import { getVisibleDates } from '@/helpers/dateHelper';
import { useCalendar } from '@/hooks/use-event-queries';
import { useThemeColor } from '@/hooks/use-theme-color';
import { useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Modal, StyleProp, StyleSheet, ViewStyle } from 'react-native';
import { Calendar, DateData } from 'react-native-calendars';
import ThemedButton from '../themed-button';
import { ThemedText } from '../themed-text';
import { ThemedView } from '../themed-view';

const TRANSLATION_PREFIX = 'tabs.profile.calendar.';

const ActivityCalendar = ({ style }: { style?: StyleProp<ViewStyle> }) => {
  const { t } = useTranslation();
  const [visibleDates, setVisibleDates] = useState<string[]>([]);
  const [selectedDay, setSelectedDay] = useState<string>('');

  const modalBgColor = useThemeColor({}, 'inputFieldBackground'),
    modalBorderColor = useThemeColor({}, 'tabIconDefault');

  const { data: entries } = useCalendar(
    visibleDates[0],
    visibleDates[visibleDates.length - 1],
  );

  const markedDates = useMemo(() => {
    const marked: Record<string, any> = {};

    entries?.forEach(e => {
      if (e.startDate) {
        marked[e.startDate] = {
          marked: true,
          dotColor: Colors.primary,
        };
      }
    });

    return marked;
  }, [entries]);

  console.log(selectedDay);

  return (
    <ThemedView style={style}>
      <ThemedText>{t(`${TRANSLATION_PREFIX}description`)}</ThemedText>
      <Calendar
        i18nIsDynamicList
        onMonthChange={(dateData: DateData) =>
          setVisibleDates(getVisibleDates(dateData.dateString))
        }
        markedDates={markedDates}
        onDayPress={day => setSelectedDay(day.dateString)}
      />
      <Modal
        transparent
        visible={!!selectedDay}
        animationType='slide'>
        <ThemedView
          style={[
            styles.modalContent,
            { backgroundColor: modalBgColor, borderColor: modalBorderColor },
          ]}>
          {entries
            ?.filter(e => e.startDate?.startsWith(selectedDay))
            .map(e => {
              return <ThemedText key={e.id}>{e.title}</ThemedText>;
            })}
          <ThemedButton
            title={t(`${TRANSLATION_PREFIX}modal.close`)}
            onPress={() => setSelectedDay('')}
            outline
          />
        </ThemedView>
      </Modal>
    </ThemedView>
  );
};

export default ActivityCalendar;

const styles = StyleSheet.create({
  modalContent: {
    width: '75%',
    height: '75%',
    justifyContent: 'center',
    alignItems: 'center',
    margin: 'auto',
    borderWidth: 0.5,
    borderRadius: 5,
  },
});
