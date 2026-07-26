import { Stack } from 'expo-router';
import React from 'react';

const AuthNav = () => {
  return (
    <Stack
      screenOptions={{
        headerShown: false,
      }}>
      <Stack.Screen name='login' />
      <Stack.Screen name='register' />
    </Stack>
  );
};

export default AuthNav;
