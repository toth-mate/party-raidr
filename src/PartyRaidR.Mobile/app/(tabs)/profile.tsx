import { useRouter } from 'expo-router';
import { StyleSheet } from 'react-native';

import { useAuthStore } from '@/store/useAuthStore';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import ThemedButton from '@/components/themed-button';
import { useThemeColor } from '@/hooks/use-theme-color';

export default function ProfileScreen() {
  const router = useRouter();
  const user = useAuthStore(state => state.user);
  const isLoggedIn = useAuthStore(state => state.isAuthenticated);
  const logout = useAuthStore(state => state.logout);

  const backgroundColor = useThemeColor({}, 'inputFieldBackground');

  return (
    <>
      <ThemedView safe={true}>
        <ThemedView style={styles.header}>
          <ThemedText type="title" style={styles.title}>
            Welcome back!
          </ThemedText>
        </ThemedView>
        {isLoggedIn ? (
          <ThemedView>
              <ThemedView style={[styles.content, { backgroundColor }]}>
              <ThemedText style={styles.usernameText}>{user?.username}</ThemedText>
              <ThemedText style={styles.emailText}>{user?.email}</ThemedText>
            </ThemedView>
            <ThemedButton
              title="Logout"
              variant='danger'
              onPress={logout}
            />
          </ThemedView>
        ) : (
          <ThemedView style={styles.buttonWrapper}>
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
        )}
        </ThemedView>
    </>
  );
}

const styles = StyleSheet.create({
  buttonWrapper: {
    flex: 1,
    flexDirection: 'column',
    justifyContent: 'center',
    gap: 8,
  },
  header: {
    borderBottomWidth: 0.8,
    borderColor: '#999',
    padding: 10,
    marginBottom: 10,
  },
  title: {
    fontWeight: '100',
  },
  content: {
    padding: 10,
    borderRadius: 5,
  },
  usernameText: {
    fontSize: 26,
    fontWeight: 'bold',
  },
  emailText: {
    fontSize: 14,
    fontWeight: '300',
  },
});