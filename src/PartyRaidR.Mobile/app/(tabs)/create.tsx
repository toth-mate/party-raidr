import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { StyleSheet } from 'react-native';

import Card from '@/components/card';
import { ThemedView } from '@/components/themed-view';
import Dropdown from '@/components/ui/dropdown';
import LabeledInput from '@/components/ui/labeled-input';
import { toMap } from '@/helpers/collectionHelper';
import { useThemeColor } from '@/hooks/use-theme-color';

const TRANSLATION_PREFIX = 'tabs.create.';

export default function CreateScreen() {
  const { t } = useTranslation();
  const inputBgColor = useThemeColor({}, 'background');

  const [category, setCategory] = useState<number | undefined>(undefined);

  const data = toMap([
    'None',
    'Outdoors activity',
    'Indoors activity',
    'Concert',
    'Festival',
    'Party',
  ]);

  return (
    <ThemedView style={styles.container}>
      <Card>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}eventTitle`)}
          labelType='big'
          placeholder={t(`${TRANSLATION_PREFIX}titleExample`)}
          inputStyle={{ backgroundColor: inputBgColor }}
        />
        <Dropdown
          data={data}
          placeholderKey='asd'
          onSelect={d => setCategory(data.get(d as string))}
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
