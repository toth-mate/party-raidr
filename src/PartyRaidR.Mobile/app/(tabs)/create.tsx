import { StyleSheet } from 'react-native';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';

export default function CreateScreen() {
  return (
    <>
      <ThemedView safe={true}>
        <ThemedText type="title">
          Create
        </ThemedText>
      </ThemedView>
    </>
  );
}

const styles = StyleSheet.create({});
