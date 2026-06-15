import { StyleSheet } from 'react-native';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';

export default function SettingsScreen() {
  return (
    <>
      <ThemedView safe={true}>
        <ThemedText type="title">
          Settings
        </ThemedText>
      </ThemedView>
    </>
  );
}

const styles = StyleSheet.create({});
