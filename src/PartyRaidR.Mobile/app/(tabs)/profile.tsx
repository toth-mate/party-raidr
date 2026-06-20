import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { Redirect } from 'expo-router';

export default function ProfileScreen() {
  return (
    <>
      <Redirect href="/login"/>
      <ThemedView safe={true}>
        <ThemedText type="title">
          Profile
        </ThemedText>
      </ThemedView>
    </>
  );
}