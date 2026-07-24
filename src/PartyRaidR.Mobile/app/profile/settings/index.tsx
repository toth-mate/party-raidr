import { useTranslation } from 'react-i18next';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';

export default function SettingsScreen() {
  const { t } = useTranslation();

  return (
    <>
      <ThemedView safe={true}>
        <ThemedText type='title'>{t('screens.settings.title')}</ThemedText>
      </ThemedView>
    </>
  );
}
