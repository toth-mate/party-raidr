import { useThemeColor } from '@/hooks/use-theme-color';
import { Stack } from 'expo-router';
import { useTranslation } from 'react-i18next';

const ProfileNav = () => {
  const { t } = useTranslation();
  const headerBackgroundColor = useThemeColor({}, 'background');
  const headerTextColor = useThemeColor({}, 'text');

  return (
    <Stack
      screenOptions={{
        headerStyle: { backgroundColor: headerBackgroundColor },
        headerTintColor: headerTextColor,
      }}>
      <Stack.Screen
        name='settings/index'
        options={{ title: t('screens.settings.title') }}
      />
    </Stack>
  );
};

export default ProfileNav;
