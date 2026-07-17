import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { StyleSheet } from 'react-native';

export default function HomeScreen() {
  return (
    <ThemedView safe>
      <ThemedView style={styles.container}>
        <ThemedText style={styles.title}>
          Welcome!
        </ThemedText>
      </ThemedView>
    </ThemedView>
  );
};

const styles = StyleSheet.create({
  container: {
    paddingHorizontal: 5,
    paddingVertical: 10,
  },
  title: {
    fontSize: 24,
    fontWeight: '200',
  },
});