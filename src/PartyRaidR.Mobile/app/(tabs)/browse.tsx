import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';

export default function BrowseScreen() {
  return (
    <>
      <ThemedView safe={true}>
        <ThemedText type="title">
          Browse
        </ThemedText>
      </ThemedView>
    </>
  );
}