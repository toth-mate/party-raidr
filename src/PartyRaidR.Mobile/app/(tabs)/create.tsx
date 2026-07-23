import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import { useTranslation } from 'react-i18next';

export default function CreateScreen() {
  const { t } = useTranslation();

  return (
    <>
      <ThemedView safe={true}>
        <ThemedText type="title">
          {t('tabs.create.title')}
        </ThemedText>
      </ThemedView>
    </>
  );
}