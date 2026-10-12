import { useRouter } from 'expo-router';
import * as SecureStorage from 'expo-secure-store';
import { useTranslation } from 'react-i18next';
import { StyleSheet } from 'react-native';

import ActivityCalendar from '@/components/profile/calendar';
import ThemedButton from '@/components/themed-button';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { manifestToast } from '@/helpers/toastHelper';
import { useCurrentUser } from '@/hooks/use-auth-queries';
import { useThemeColor } from '@/hooks/use-theme-color';
import { useQueryClient } from '@tanstack/react-query';

const TRANSLATION_PREFIX = 'tabs.profile.';

export default function ProfileScreen() {
  const { t } = useTranslation();
  const router = useRouter();
  const queryClient = useQueryClient();

  const { data: user } = useCurrentUser();
  const isLoggedIn = !!user;

  const logout = async () => {
    await SecureStorage.deleteItemAsync('auth_token');
    queryClient.setQueryData(['auth', 'user'], null);
    queryClient.removeQueries({
      queryKey: ['events', 'calendar'],
    });
    manifestToast({
      title: t('common.auth.toast.logout.title'),
      text: t('common.auth.toast.logout.text'),
      type: 'info',
    });
  };

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
            <ActivityCalendar style={{ marginTop: 10 }} />
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
              onPress={() => router.push('/register')}
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
