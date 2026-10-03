import { useThemeColor } from '@/hooks/use-theme-color';
import { View } from 'react-native';

const Divider = () => {
  const color = useThemeColor({}, 'tabIconDefault');
  return (
    <View
      style={{
        height: 1,
        width: '100%',
        backgroundColor: color,
        borderRadius: 10,
        marginVertical: 10,
      }}></View>
  );
};

export default Divider;
