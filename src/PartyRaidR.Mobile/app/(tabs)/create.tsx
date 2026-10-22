import { useTranslation } from 'react-i18next';

import { ThemedView } from '@/components/themed-view';

export default function CreateScreen() {
  const { t } = useTranslation();

  return (
    <>
      <ThemedView safe={true}></ThemedView>
    </>
  );
}
