import { useThemeColor } from '@/hooks/use-theme-color';
import { StyleSheet, View } from 'react-native';
import ColoredLink from '../colored-link';

const CalendarListCard = ({
  eventId,
  title,
}: {
  eventId: string;
  title: string;
}) => {
  const borderColor = useThemeColor({}, 'icon');

  return (
    <View style={[styles.card, { borderColor }]}>
      <ColoredLink href={`/event/${eventId}`}>{title}</ColoredLink>
    </View>
  );
};

export default CalendarListCard;

const styles = StyleSheet.create({
  card: {
    borderWidth: 0.5,
    borderRadius: 10,
    padding: 10,
    width: '100%',
  },
});
