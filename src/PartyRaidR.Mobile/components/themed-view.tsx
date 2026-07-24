import { View } from 'react-native';
import { useSafeAreaInsets } from 'react-native-safe-area-context';

import { ThemedViewProps } from '@/types/props.types';
import { useThemeColor } from '@/hooks/use-theme-color';

export function ThemedView({
  style,
  lightColor,
  darkColor,
  safe,
  ...otherProps
}: ThemedViewProps) {
  const backgroundColor = useThemeColor(
    { light: lightColor, dark: darkColor },
    'background',
  );
  const insets = useSafeAreaInsets();

  if (!safe) {
    return (
      <View
        style={[{ backgroundColor }, style]}
        {...otherProps}
      />
    );
  }
  return (
    <View
      style={[
        { backgroundColor },
        style,
        {
          paddingTop: insets.top,
          paddingBottom: insets.bottom,
          paddingLeft: 10,
          paddingRight: 10,
          flex: 1,
        },
      ]}
      {...otherProps}
    />
  );
}
