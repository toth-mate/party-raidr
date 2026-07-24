import { PropsWithChildren, useState } from 'react';
import { StyleSheet, TouchableOpacity } from 'react-native';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { IconSymbol } from '@/components/ui/icon-symbol';
import { Colors } from '@/constants/theme';
import { useColorScheme } from '@/hooks/use-color-scheme';
import { useThemeColor } from '@/hooks/use-theme-color';

export function Collapsible({
  children,
  title,
  defaultOpen = false,
}: PropsWithChildren & { title: string; defaultOpen?: boolean }) {
  const [isOpen, setIsOpen] = useState(defaultOpen);
  const theme = useColorScheme() ?? 'light';
  const titleColor = useThemeColor({}, 'icon');

  return (
    <ThemedView style={{ padding: 5, borderRadius: 5 }}>
      <TouchableOpacity
        style={styles.heading}
        onPress={() => setIsOpen(value => !value)}
        activeOpacity={0.8}>
        <IconSymbol
          name='chevron.right'
          size={18}
          weight='medium'
          color={theme === 'light' ? Colors.light.icon : Colors.dark.icon}
          style={{ transform: [{ rotate: isOpen ? '90deg' : '0deg' }] }}
        />

        <ThemedText
          type='defaultSemiBold'
          style={{ color: titleColor }}>
          {title}
        </ThemedText>
      </TouchableOpacity>
      {isOpen && <ThemedView style={styles.content}>{children}</ThemedView>}
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  heading: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  content: {
    marginTop: 6,
    marginLeft: 24,
  },
});
