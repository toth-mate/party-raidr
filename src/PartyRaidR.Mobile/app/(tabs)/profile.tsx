import { useRouter } from 'expo-router';
import { StyleSheet } from 'react-native';
import { useTranslation } from 'react-i18next';

import { useAuthStore } from '@/store/useAuthStore';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import ThemedButton from '@/components/themed-button';
import { useThemeColor } from '@/hooks/use-theme-color';

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
        <ThemedView style={styles.header}>
          <ThemedText type="title" style={styles.title}>
            {t(`${TRANSLATION_PREFIX}welcome`)}
          </ThemedText>
          <ThemedButton
            onPress={() => router.push('/profile/settings')}
            icon="settings"
            outline
            color="#888"
            borderColor="#888"
            style={styles.headerButton}
          />
        </ThemedView>
        {isLoggedIn ? (
          <ThemedView>
              <ThemedView style={[styles.content, { backgroundColor }]}>
              <ThemedText style={styles.usernameText}>{user?.username}</ThemedText>
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
  header: {
    borderBottomWidth: 0.8,
    borderColor: '#999',
    padding: 10,
    marginBottom: 10,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  title: {
    fontWeight: '100',
  },
  headerButton: {
    borderRadius: '50%',
    width: 35,
    height: 35,
    fontSize: 10,
    alignItems: 'center',
    padding: 0,
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