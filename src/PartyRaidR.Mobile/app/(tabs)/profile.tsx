import { useRouter } from 'expo-router';
import * as SecureStorage from 'expo-secure-store';
import { useTranslation } from 'react-i18next';
import { StyleSheet, View } from 'react-native';

import ActivityCalendar from '@/components/profile/calendar';
import ThemedButton from '@/components/themed-button';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { manifestToast } from '@/helpers/toastHelper';
import { useCurrentUser } from '@/hooks/use-auth-queries';
import { useThemeColor } from '@/hooks/use-theme-color';
import Feather from '@react-native-vector-icons/feather';
import { useQueryClient } from '@tanstack/react-query';

const TRANSLATION_PREFIX = 'tabs.profile.';

export default function ProfileScreen() {
  const { t } = useTranslation();
  const router = useRouter();
  const queryClient = useQueryClient();

  const textColor = useThemeColor({}, 'text');
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');
  const secondaryColor = useThemeColor({}, 'icon');

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

  return (
    <>
      <ThemedView safe={true}>
        {isLoggedIn ? (
          <ThemedView>
            <ThemedView style={[styles.content, { backgroundColor }]}>
              <View style={styles.accountEntryRow}>
                <Feather
                  name='user'
                  color={textColor}
                  size={24}
                />
                <ThemedText style={styles.usernameText}>
                  {user?.username}
                </ThemedText>
              </View>
              <View style={styles.accountEntryRow}>
                <Feather
                  name='mail'
                  color={textColor}
                />
                <ThemedText style={styles.accountEntryText}>
                  {user?.email}
                </ThemedText>
              </View>
              <View style={styles.accountEntryRow}>
                <Feather
                  name='calendar'
                  color={textColor}
                />
                <ThemedText style={styles.accountEntryText}>
                  {user?.birthDate}
                </ThemedText>
              </View>

              <ThemedText
                style={[styles.memberDateText, { color: secondaryColor }]}>
                {t(`${TRANSLATION_PREFIX}memberSince`, {
                  date: user.registerDate.split('T')[0],
                })}
              </ThemedText>
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
  accountEntryRow: {
    marginBottom: 2,
    flexDirection: 'row',
    alignItems: 'baseline',
  },
  usernameText: {
    marginStart: 5,
    fontSize: 26,
    fontWeight: 'bold',
  },
  accountEntryText: {
    marginStart: 5,
    fontSize: 14,
    fontWeight: '300',
  },
  memberDateText: {
    fontSize: 12,
    textAlign: 'center',
  },
});
