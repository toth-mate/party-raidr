import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';

export default function HomeScreen() {
  return (
    <>
      <ThemedView safe={true}>
        <ThemedText type="title">
          Home
        </ThemedText>
      </ThemedView>
    </>
  );
}