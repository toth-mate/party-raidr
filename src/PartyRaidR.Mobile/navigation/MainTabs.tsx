import { Tabs, useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { Platform, StyleSheet } from 'react-native';

import ThemedButton from '@/components/themed-button';
import { IconSymbol } from '@/components/ui/icon-symbol';
import { Colors } from '@/constants/theme';
import { useThemeColor } from '@/hooks/use-theme-color';

const TRANSLATION_PREFIX = 'tabs.';

export default function MainTabs() {
  const { t } = useTranslation();
  const router = useRouter();
  const headerBgColor = useThemeColor({}, 'background');
  const buttonBgColor = useThemeColor({}, 'inputFieldBackground');
  const buttonTextColor = useThemeColor({}, 'text');

  return (
    <Tabs
      screenOptions={{
        tabBarActiveTintColor: Colors.primary,
        headerShown: false,
      }}>
      <Tabs.Screen
        name='index'
        options={{
          title: t(`${TRANSLATION_PREFIX}home.title`),
          tabBarIcon: ({ color }) => (
            <IconSymbol
              size={28}
              name='house.fill'
              color={color}
            />
          ),
          headerShown: true,
          headerTitle: t(`${TRANSLATION_PREFIX}home.welcome`),
          headerTitleAlign: 'left',
        }}
      />
      <Tabs.Screen
        name='browse'
        options={{
          title: t(`${TRANSLATION_PREFIX}browse.title`),
          tabBarIcon: ({ color }) => (
            <IconSymbol
              size={28}
              name='magnifyingglass'
              color={color}
            />
          ),
        }}
      />
      <Tabs.Screen
        name='create'
        options={{
          title: '',
          tabBarIcon: ({ color }) => (
            <IconSymbol
              size={44}
              name='plus.circle'
              color={color}
            />
          ),
          tabBarIconStyle: {
            justifyContent: 'center',
            alignItems: 'center',
            alignSelf: 'center',
            marginTop: Platform.OS === 'ios' ? 6 : 0,
          },
        }}
      />
      <Tabs.Screen
        name='map'
        options={{
          title: t(`${TRANSLATION_PREFIX}map.title`),
          tabBarIcon: ({ color }) => (
            <IconSymbol
              size={28}
              name='map'
              color={color}
            />
          ),
        }}
      />
      <Tabs.Screen
        name='profile'
        options={{
          title: t(`${TRANSLATION_PREFIX}profile.title`),
          tabBarIcon: ({ color }) => (
            <IconSymbol
              size={28}
              name='person.circle'
              color={color}
            />
          ),
          headerShown: true,
          headerStyle: { backgroundColor: headerBgColor },
          headerTitle: t(`${TRANSLATION_PREFIX}profile.welcome`),
          headerTitleAlign: 'left',
          headerTitleStyle: styles.customHeaderTitle,
          headerRight: () => (
            <ThemedButton
              onPress={() => router.push('/profile/settings')}
              icon='settings'
              color={buttonTextColor}
              borderColor='#888'
              style={[styles.headerButton, { backgroundColor: buttonBgColor }]}
            />
          ),
        }}
      />
    </Tabs>
  );
}

const styles = StyleSheet.create({
  headerButton: {
    borderRadius: '50%',
    width: 35,
    height: 35,
    fontSize: 10,
    alignItems: 'center',
    padding: 0,
    marginRight: 8,
    borderWidth: 0.3,
  },
  customHeaderTitle: {
    fontWeight: 300,
    fontSize: 20,
  },
});
