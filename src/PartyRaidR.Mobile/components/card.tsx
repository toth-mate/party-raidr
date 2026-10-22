import { useThemeColor } from '@/hooks/use-theme-color';
import { StyleSheet, View } from 'react-native';

const Card = ({
  children,
  topSpacing = 20,
}: React.PropsWithChildren & { topSpacing?: number }) => {
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');

  return (
    <View
      style={[{ backgroundColor, marginTop: topSpacing }, styles.container]}>
      {children}
    </View>
  );
};

export default Card;

const styles = StyleSheet.create({
  container: {
    padding: 10,
    borderRadius: 10,
    boxShadow: '2px 2px 10px rgba(0,0,0,0.15)',
  },
});
