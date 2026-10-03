import { Colors } from '@/constants/theme';
import { getVisibleDates } from '@/helpers/dateHelper';
import { useCalendar } from '@/hooks/use-event-queries';
import { useThemeColor } from '@/hooks/use-theme-color';
import { Feather } from '@react-native-vector-icons/feather';
import { format, formatDate, getDate } from 'date-fns';
import { enUS, hu } from 'date-fns/locale';
import { useLocales } from 'expo-localization';
import { useEffect, useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import {
  Modal,
  Pressable,
  StyleProp,
  StyleSheet,
  View,
  ViewStyle,
} from 'react-native';
import { Calendar, DateData } from 'react-native-calendars';
import { ThemedText } from '../themed-text';
import { ThemedView } from '../themed-view';
import CalendarListCard from './calendar-list-card';

const TRANSLATION_PREFIX = 'tabs.profile.calendar.';

const ActivityCalendar = ({ style }: { style?: StyleProp<ViewStyle> }) => {
  const locales = useLocales();
  const { t } = useTranslation();
  const [visibleDates, setVisibleDates] = useState<string[]>([]);
  const [selectedDay, setSelectedDay] = useState<string>('');
  const [modalVisible, setModalVisible] = useState<boolean>(false);

  const modalBgColor = useThemeColor({}, 'secondaryBackground'),
    secondaryTextColor = useThemeColor({}, 'secondaryText');

  const { data: entries } = useCalendar(
    visibleDates[0],
    visibleDates[visibleDates.length - 1],
  );

  const closeModal = () => {
    setModalVisible(false);
    // This check is needed because otherwise the modal date formatting would report an error while still holding a value.
    if (!modalVisible) setSelectedDay('');
  };

  useEffect(() => {
    if (!selectedDay) {
      setModalVisible(false);
      return;
    }

    if (
      entries?.find(e => e.startDate?.startsWith(selectedDay)) !== undefined
    ) {
      setModalVisible(true);
    }
  }, [selectedDay]);

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

  const ModalHeader = () => {
    const { languageCode } = locales[0];
    const locale = languageCode === 'en' ? enUS : hu;
    const date = format(selectedDay, 'yyyy-MM-dd');
    const dayOfWeek = formatDate(date, 'EEEE', { locale });
    const month = formatDate(date, 'LLLL', { locale });

    return (
      <View style={styles.modalHeader}>
        <View style={styles.titleSection}>
          <ThemedText
            type='title'
            style={{ fontSize: 28 }}>
            {t(`${TRANSLATION_PREFIX}modal.title`)}
          </ThemedText>
          <Pressable
            onPress={closeModal}
            style={styles.modalXButton}>
            <Feather
              name='x'
              color='#888'
              size={24}
            />
          </Pressable>
        </View>
        <View style={styles.dateSection}>
          <ThemedText style={styles.dayOfMonth}>{getDate(date)}</ThemedText>
          <View style={styles.secondaryDateContainer}>
            <ThemedText style={styles.dayOfWeek}>{dayOfWeek}</ThemedText>
            <ThemedText style={[styles.month, { color: secondaryTextColor }]}>
              {month}
            </ThemedText>
          </View>
        </View>
      </View>
    );
  };

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
        visible={modalVisible}
        onDismiss={closeModal}
        animationType='slide'>
        <ThemedView
          style={[styles.modalContent, { backgroundColor: modalBgColor }]}>
          <ModalHeader />
          {entries
            ?.filter(e => e.startDate?.startsWith(selectedDay))
            .map(e => {
              return (
                <CalendarListCard
                  key={e.id}
                  eventId={e.id!}
                  title={
                    e.title || t(`${TRANSLATION_PREFIX}modal.titleNotFound`)
                  }
                />
              );
            })}
        </ThemedView>
      </Modal>
    </ThemedView>
  );
};

export default ActivityCalendar;

const styles = StyleSheet.create({
  modalContent: {
    width: '100%',
    height: '80%',
    marginTop: 'auto',
    borderRadius: 15,
    paddingHorizontal: 10,
    paddingTop: 20,
  },
  titleSection: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingEnd: 5,
  },
  modalHeader: {},
  modalXButton: {
    fontSize: 24,
    borderRadius: '50%',
  },
  dateSection: {
    flexDirection: 'row',
    marginTop: 12,
    gap: 10,
  },
  dayOfMonth: {
    fontWeight: 'bold',
    fontSize: 84,
    lineHeight: 84,
  },
  secondaryDateContainer: {
    width: '100%',
    justifyContent: 'center',
    gap: 8,
  },
  dayOfWeek: {
    fontWeight: 'bold',
    fontSize: 24,
    textTransform: 'capitalize',
  },
  month: {
    fontSize: 24,
    textTransform: 'capitalize',
  },
});
