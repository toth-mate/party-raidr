import { useRouter } from 'expo-router';

import { useAuthStore } from '@/store/useAuthStore';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { useEffect } from 'react';
import ThemedButton from '@/components/themed-button';

export default function ProfileScreen() {
  const router = useRouter();
  const isLoggedIn = useAuthStore(state => state.isAuthenticated);
  const logout = useAuthStore(state => state.logout);

  if(isLoggedIn) {
    return (
      <>
        <ThemedView safe={true}>
          <ThemedText type="title">
            Profile
          </ThemedText>
          <ThemedButton
            title="Logout"
            variant='danger'
            onPress={logout}
          />
        </ThemedView>
      </>
    );
  }

  return (
    <ThemedView safe={true}>
      <ThemedButton
        title="Login"
        variant='secondary'
        onPress={() => router.push('/login')}
      />
      <ThemedButton
        title="Create an account"
        variant='primary'
        onPress={() => router.push('/')}
        outline
      />
    </ThemedView>
  );
}