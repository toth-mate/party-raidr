import { Stack } from 'expo-router';
import { StatusBar } from 'expo-status-bar';
import 'react-native-reanimated';
import Toast from 'react-native-toast-message';
import '../i18n';

import { useColorScheme } from '@/hooks/use-color-scheme';

import { useEffect } from 'react';

import { useAuthStore } from '@/store/useAuthStore';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5, // 5 minutes
      retry: 2,
    },
  },
});

export const unstable_settings = {
  anchor: '(tabs)',
};

export default function RootLayout() {
  const initializeAuth = useAuthStore(state => state.initializeAuth);
  const colorScheme = useColorScheme();

  useEffect(() => {
    initializeAuth();
  }, []);

  return (
    <QueryClientProvider client={queryClient}>
      <Stack>
        <Stack.Screen
          name='(tabs)'
          options={{ headerShown: false }}
        />
        <Stack.Screen
          name='(auth)'
          options={{ headerShown: false }}
        />
        <Stack.Screen
          name='event/[id]'
          options={{ headerBackButtonDisplayMode: 'generic' }}
        />
        <Stack.Screen
          name='profile'
          options={{ headerShown: false }}
        />
      </Stack>
      <Toast
        position='bottom'
        swipeable
      />
      <StatusBar style='auto' />
    </QueryClientProvider>
  );
}
