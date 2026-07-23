import { Stack } from 'expo-router';
import React from 'react';
import { useTranslation } from 'react-i18next';

const ProfileNav = () => {
  const { t } = useTranslation();

  return (
    <Stack>
      <Stack.Screen name="settings/index" options={{ title: t('screens.settings.title') }} />
    </Stack>
  );
};

export default ProfileNav;