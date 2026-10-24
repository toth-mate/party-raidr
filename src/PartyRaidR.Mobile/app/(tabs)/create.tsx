import { useTranslation } from 'react-i18next';

import Card from '@/components/card';
import { ThemedView } from '@/components/themed-view';
import LabeledInput from '@/components/ui/labeled-input';
import { useThemeColor } from '@/hooks/use-theme-color';
import { StyleSheet } from 'react-native';

const TRANSLATION_PREFIX = 'tabs.create.';

export default function CreateScreen() {
  const { t } = useTranslation();
  const inputBgColor = useThemeColor({}, 'background');

  return (
    <ThemedView style={styles.container}>
      <Card>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}eventTitle`)}
          labelType='big'
          placeholder={t(`${TRANSLATION_PREFIX}titleExample`)}
          inputStyle={{ backgroundColor: inputBgColor }}
        />
      </Card>
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingHorizontal: 10,
  },
  inputField: {},
});
