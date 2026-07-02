import { Stack } from 'expo-router';
import React from 'react';

const ProfileNav = () => {
  return (
    <Stack>
      <Stack.Screen name="settings/index" options={{ title: 'Settings' }} />
    </Stack>
  );
};

export default ProfileNav;