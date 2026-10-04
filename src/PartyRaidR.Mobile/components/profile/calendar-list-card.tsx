import { Colors } from '@/constants/theme';
import { useThemeColor } from '@/hooks/use-theme-color';
import Feather from '@react-native-vector-icons/feather';
import { useTranslation } from 'react-i18next';
import { StyleSheet, View } from 'react-native';
import { CalendarEntryDto } from '../../types/dto';
import { ThemedText } from '../themed-text';

const TRANSLATION_PREFIX = 'tabs.profile.calendar.modal.card.';

const CalendarListCard = ({ entry }: { entry: CalendarEntryDto }) => {
  const { t } = useTranslation();
  const backgroundColor = useThemeColor({}, 'inputFieldBackground'),
    secondaryTextColor = useThemeColor({}, 'secondaryText'),
    textColor = useThemeColor({}, 'tabIconDefault');

  const getStatusColor = () => {
    if (entry.isAuthor) return Colors.info;

    switch (entry.status) {
      case 0:
      case 2:
        return Colors.success;
      case 1:
      case 4:
        return Colors.warning;
      default:
        return Colors.danger;
    }
  };

  const getStatusText = (): string => {
    switch (entry.status) {
      case 0:
      case 2:
        return `${TRANSLATION_PREFIX}participant`;
      case 1:
        return `${TRANSLATION_PREFIX}pending`;
      case 3:
        return `${TRANSLATION_PREFIX}rejeced`;
      case 4:
        return `${TRANSLATION_PREFIX}waitList`;
      default:
        return '';
    }
  };

  return (
    <View
      style={{
        flexDirection: 'row',
        boxShadow: '2px 2px 10px rgba(0,0,0,0.15)',
        marginBottom: 10,
      }}>
      <View style={[styles.body, { backgroundColor: getStatusColor() }]}></View>
      <View style={[styles.cardContainer, { backgroundColor }]}>
        <View style={styles.cardHeader}>
          <ThemedText style={styles.title}>{entry.title}</ThemedText>
          {entry.isAuthor && (
            <ThemedText style={{ color: secondaryTextColor, marginBottom: 5 }}>
              {t(`${TRANSLATION_PREFIX}organizer`)}
            </ThemedText>
          )}
        </View>
        <View style={{ flexDirection: 'row', justifyContent: 'space-between' }}>
          <View
            style={{ flexDirection: 'row', gap: 5, alignItems: 'baseline' }}>
            <Feather
              name='map-pin'
              color={textColor}
              size={14}
            />
            <ThemedText>{entry.locationName}</ThemedText>
          </View>
          {!entry.isAuthor && (
            <ThemedText style={{ color: getStatusColor() }}>
              [ {t(getStatusText())} ]
            </ThemedText>
          )}
        </View>
      </View>
    </View>
  );
};

export default CalendarListCard;

const styles = StyleSheet.create({
  body: {
    width: 5,
    borderStartStartRadius: 10,
    borderBottomStartRadius: 10,
  },
  cardContainer: {
    padding: 15,
    borderTopEndRadius: 10,
    borderBottomEndRadius: 10,
    width: '100%',
  },
  cardHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 5,
  },
  title: {
    fontWeight: 'bold',
    fontSize: 24,
  },
});
