import { DarkTheme, DefaultTheme, ThemeProvider } from '@react-navigation/native';
import { Stack } from 'expo-router';
import { StatusBar } from 'expo-status-bar';
import 'react-native-reanimated';
import Toast from 'react-native-toast-message';

import { useColorScheme } from '@/hooks/use-color-scheme';
import { useEffect } from 'react';
import { useAuthStore } from '@/store/useAuthStore';

export const unstable_settings = {
  anchor: '(tabs)',
};

export default function RootLayout() {
  const initializeAuth = useAuthStore((state) => state.initializeAuth);
  const colorScheme = useColorScheme();

  useEffect(() => {
    initializeAuth();
  }, []);

  return (
    <ThemeProvider value={colorScheme === 'dark' ? DarkTheme : DefaultTheme}>
      <Stack>
        <Stack.Screen name="(tabs)" options={{ headerShown: false }} />
        <Stack.Screen name="(auth)" options={{headerShown: false}}/>
        <Stack.Screen name="event/[id]" options={{headerShown: false}}/>
      </Stack>
      <Toast position='bottom' swipeable/>
      <StatusBar style="auto" />
    </ThemeProvider>
  );
}
