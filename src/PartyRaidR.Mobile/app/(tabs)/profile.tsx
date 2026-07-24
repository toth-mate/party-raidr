import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { StyleSheet } from 'react-native';

import ThemedButton from '@/components/themed-button';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { useThemeColor } from '@/hooks/use-theme-color';
import { useAuthStore } from '@/store/useAuthStore';

const TRANSLATION_PREFIX = 'tabs.profile.';

export default function ProfileScreen() {
  const { t } = useTranslation();
  const router = useRouter();
  const user = useAuthStore(state => state.user);
  const isLoggedIn = useAuthStore(state => state.isAuthenticated);
  const logout = useAuthStore(state => state.logout);

  const backgroundColor = useThemeColor({}, 'inputFieldBackground');

  return (
    <>
      <ThemedView safe={true}>
        {isLoggedIn ? (
          <ThemedView>
            <ThemedView style={[styles.content, { backgroundColor }]}>
              <ThemedText style={styles.usernameText}>
                {user?.username}
              </ThemedText>
              <ThemedText style={styles.emailText}>{user?.email}</ThemedText>
            </ThemedView>
            <ThemedButton
              title={t(`${TRANSLATION_PREFIX}logout`)}
              variant='danger'
              onPress={logout}
            />
          </ThemedView>
        ) : (
          <ThemedView style={styles.buttonWrapper}>
            <ThemedButton
              title={t(`${TRANSLATION_PREFIX}login`)}
              variant='secondary'
              onPress={() => router.push('/login')}
            />
            <ThemedButton
              title={t(`${TRANSLATION_PREFIX}createAccount`)}
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
