import { Tabs } from 'expo-router';
import React from 'react';

import { HapticTab } from '@/components/haptic-tab';
import { IconSymbol } from '@/components/ui/icon-symbol';
import { Colors } from '@/constants/theme';
import { Platform } from 'react-native';
import { useTranslation } from 'react-i18next';

const TRANSLATION_PREFIX = 'tabs.';

export default function MainTabs() {
  const { t } = useTranslation();

  return (
    <Tabs
      screenOptions={{
        tabBarActiveTintColor: Colors.primary,
        headerShown: false,
        tabBarButton: HapticTab,
      }}>
      <Tabs.Screen
        name="index"
        options={{
          title: t(`${TRANSLATION_PREFIX}home.title`),
          tabBarIcon: ({ color }) => <IconSymbol size={28} name="house.fill" color={color} />,
          headerShown: true,
          headerTitle: t(`${TRANSLATION_PREFIX}home.welcome`),
          headerTitleAlign: 'left',
        }}
      />
      <Tabs.Screen
        name="browse"
        options={{
          title: t(`${TRANSLATION_PREFIX}browse.title`),
          tabBarIcon: ({ color }) => <IconSymbol size={28} name="magnifyingglass" color={color} />,
        }}
      />
      <Tabs.Screen
        name="create"
        options={{
          title: '',
          tabBarIcon: ({ color }) => <IconSymbol size={44} name="plus.circle" color={color} />,
          tabBarIconStyle: {
            justifyContent: 'center',
            alignItems: 'center',
            alignSelf: 'center',
            marginTop: Platform.OS === 'ios' ? 6 : 0,
          }
        }}
      />
      <Tabs.Screen
        name="map"
        options={{
          title: t(`${TRANSLATION_PREFIX}map.title`),
          tabBarIcon: ({ color }) => <IconSymbol size={28} name="map" color={color} />,
        }}
      />
      <Tabs.Screen
        name="profile"
        options={{
          title: t(`${TRANSLATION_PREFIX}profile.title`),
          tabBarIcon: ({ color }) => <IconSymbol size={28} name="person.circle" color={color} />,
        }}
      />
    </Tabs>
  );
}