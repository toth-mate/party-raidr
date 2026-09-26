import { useTranslation } from 'react-i18next';
import { StyleProp, StyleSheet, ViewStyle } from 'react-native';
import { Calendar } from 'react-native-calendars';
import { ThemedText } from '../themed-text';
import { ThemedView } from '../themed-view';

const ActivityCalendar = ({ style }: { style?: StyleProp<ViewStyle> }) => {
  const { t } = useTranslation();

  return (
    <ThemedView style={style}>
      <ThemedText>{t('tabs.profile.calendar.description')}</ThemedText>
      <Calendar i18nIsDynamicList />
    </ThemedView>
  );
};

export default ActivityCalendar;

const styles = StyleSheet.create({});
