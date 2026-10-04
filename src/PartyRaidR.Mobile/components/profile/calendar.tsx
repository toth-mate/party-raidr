import dateLocales from '@/constants/dateLocales';
import { Colors } from '@/constants/theme';
import { getVisibleDates } from '@/helpers/dateHelper';
import { useCalendar } from '@/hooks/use-event-queries';
import { useThemeColor } from '@/hooks/use-theme-color';
import { Feather } from '@react-native-vector-icons/feather';
import {
  addDays,
  format,
  formatDate,
  getDate,
  isWithinInterval,
} from 'date-fns';
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
import { Calendar, DateData, LocaleConfig } from 'react-native-calendars';
import {
  Gesture,
  GestureDetector,
  GestureHandlerRootView,
} from 'react-native-gesture-handler';
import Animated, {
  useAnimatedStyle,
  useSharedValue,
  withSpring,
} from 'react-native-reanimated';
import { runOnJS } from 'react-native-worklets';
import Divider from '../divider';
import { ThemedText } from '../themed-text';
import { ThemedView } from '../themed-view';
import CalendarListCard from './calendar-list-card';

const TRANSLATION_PREFIX = 'tabs.profile.calendar.';

LocaleConfig.locales['hu'] = dateLocales.hu;
LocaleConfig.defaultLocale = 'hu';

const TODAY = new Date().toISOString().split('T')[0];

const ActivityCalendar = ({ style }: { style?: StyleProp<ViewStyle> }) => {
  const locales = useLocales();
  const { t } = useTranslation();
  const [visibleDates, setVisibleDates] = useState<string[]>([]);
  const [selectedDay, setSelectedDay] = useState<string>('');
  const [currentDate, setCurrentDate] = useState<string>(TODAY);
  const [modalVisible, setModalVisible] = useState<boolean>(false);
  const modalPosition = useSharedValue<number>(0);

  const modalBgColor = useThemeColor({}, 'secondaryBackground'),
    dayTextColor = useThemeColor({}, 'text'),
    secondaryTextColor = useThemeColor({}, 'secondaryText'),
    calendarBgColor = useThemeColor({}, 'inputFieldBackground'),
    textDisabledColor = useThemeColor({ light: '#aaa' }, 'secondaryText');

  const { data: entries } = useCalendar(
    visibleDates[0],
    visibleDates[visibleDates.length - 1],
  );

  const closeModal = () => {
    setModalVisible(false);
    // This check is needed because otherwise the modal date formatting would report an error while still holding a value.
    if (!modalVisible) setSelectedDay('');
  };

  const handleClose = () => {
    setModalVisible(false);
    if (!modalVisible) modalPosition.value = 0;
  };

  useEffect(() => {
    if (!selectedDay) {
      setModalVisible(false);
      return;
    }

    if (
      entries?.find(
        e =>
          e.startDate &&
          e.endDate &&
          isWithinInterval(selectedDay, {
            start: e.startDate,
            end: e.endDate,
          }),
      ) !== undefined
    ) {
      setModalVisible(true);
      modalPosition.value = 0;
    }
  }, [entries, selectedDay]);

  useEffect(() => {
    setVisibleDates(getVisibleDates(currentDate));
  }, [currentDate]);

  const markedDates = useMemo(() => {
    const marked: Record<string, any> = {};

    entries?.forEach(e => {
      if (
        (e.startDate && !e.endDate) ||
        (e.startDate && e.endDate && e.startDate === e.endDate)
      ) {
        marked[e.startDate] = {
          marked: true,
          dotColor: Colors.primary,
        };
      } else if (!e.startDate && e.endDate) {
        marked[e.endDate] = {
          marked: true,
          dotColor: Colors.primary,
        };
      } else if (e.startDate && e.endDate) {
        let d = format(e.startDate, 'yyyy-MM-dd');

        do {
          marked[d] = {
            marked: true,
            dotColor: Colors.primary,
          };
          d = format(addDays(d, 1), 'yyyy-MM-dd');
        } while (d !== format(e.endDate, 'yyyy-MM-dd'));

        marked[e.endDate] = {
          marked: true,
          dotColor: Colors.primary,
        };
      }
    });

    return marked;
  }, [entries]);

  const closeModalFlingGesture = Gesture.Pan()
    .onUpdate(e => {
      if (e.translationY > 0) {
        modalPosition.value = e.translationY;
      }
    })
    .onEnd(e => {
      if (e.translationY > 100 || e.velocityY > 500) {
        runOnJS(handleClose)();
      } else {
        modalPosition.value = withSpring(0);
      }
    });

  const animatedStyle = useAnimatedStyle(() => ({
    transform: [{ translateY: modalPosition.value }],
  }));

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
        current={currentDate}
        onMonthChange={(dateData: DateData) =>
          setCurrentDate(dateData.dateString)
        }
        markedDates={markedDates}
        onDayPress={day => setSelectedDay(day.dateString)}
        theme={{
          calendarBackground: calendarBgColor,
          arrowColor: Colors.primary,
          todayBackgroundColor: Colors.primary,
          todayTextColor: 'white',
          dayTextColor,
          textDisabledColor,
          monthTextColor: dayTextColor,
        }}
        style={{ borderRadius: 10, marginTop: 8 }}
      />
      <Modal
        transparent
        visible={modalVisible}
        onDismiss={closeModal}
        animationType='slide'>
        <GestureHandlerRootView style={{ flex: 1 }}>
          <View
            style={{
              flex: 1,
              justifyContent: 'flex-end',
            }}>
            <GestureDetector gesture={closeModalFlingGesture}>
              <Animated.View style={animatedStyle}>
                <ThemedView
                  style={[
                    styles.modalContent,
                    { backgroundColor: modalBgColor },
                  ]}>
                  <ModalHeader />
                  <Divider />
                  {entries
                    ?.filter(e =>
                      isWithinInterval(selectedDay, {
                        start: e.startDate!,
                        end: e.endDate!,
                      }),
                    )
                    .map(e => {
                      return (
                        <CalendarListCard
                          key={e.id}
                          entry={e}
                        />
                      );
                    })}
                </ThemedView>
              </Animated.View>
            </GestureDetector>
          </View>
        </GestureHandlerRootView>
      </Modal>
    </ThemedView>
  );
};

export default ActivityCalendar;

const styles = StyleSheet.create({
  modalContent: {
    width: '100%',
    height: '90%',
    marginTop: 'auto',
    borderTopLeftRadius: 15,
    borderTopRightRadius: 15,
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
