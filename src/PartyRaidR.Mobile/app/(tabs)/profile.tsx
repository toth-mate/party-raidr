import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';

export default function ProfileScreen() {
  return (
    <>
      <ThemedView safe={true}>
        <ThemedText type="title">
          Profile
        </ThemedText>
      </ThemedView>
    </>
  );
}