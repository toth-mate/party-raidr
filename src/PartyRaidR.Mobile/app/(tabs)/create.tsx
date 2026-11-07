import DateTimePicker from '@expo/ui/community/datetime-picker';
import { Feather } from '@expo/vector-icons';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { StyleSheet, View } from 'react-native';

import Card from '@/components/card';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import Dropdown from '@/components/ui/dropdown';
import LabeledInput from '@/components/ui/labeled-input';
import { Colors } from '@/constants/theme';
import { toMap } from '@/helpers/collectionHelper';
import { useThemeColor } from '@/hooks/use-theme-color';

const TRANSLATION_PREFIX = 'tabs.create.';
const NOW = new Date();

export default function CreateScreen() {
  const { t } = useTranslation();
  const inputBgColor = useThemeColor({}, 'background');
  const secondaryColor = useThemeColor({}, 'icon');

  const [category, setCategory] = useState<number | undefined>(undefined);
  const [startDate, setStartDate] = useState<Date>(NOW);
  const [endDate, setEndDate] = useState<Date>(NOW);

  const data = toMap([
    'common.eventCategory.none',
    'common.eventCategory.outdoors',
    'common.eventCategory.indoors',
    'common.eventCategory.concert',
    'common.eventCategory.festival',
    'common.eventCategory.party',
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
          label={t(`${TRANSLATION_PREFIX}category`)}
          data={data}
          onSelect={d => setCategory(data.get(d as string))}
          style={styles.categoryDropdown}
        />
      </Card>
      <Card>
        <View style={{ flexDirection: 'row', alignItems: 'baseline' }}>
          <Feather
            name='calendar'
            size={16}
            style={{ marginRight: 4 }}
            color={Colors.secondary}
          />
          <ThemedText type='subtitle'>
            {t(`${TRANSLATION_PREFIX}time`)}
          </ThemedText>
        </View>
        <View style={styles.dateContainer}>
          <ThemedText style={[styles.dateText, { color: secondaryColor }]}>
            {t(`${TRANSLATION_PREFIX}start`)}
          </ThemedText>
          <DateTimePicker
            value={startDate}
            minimumDate={NOW}
            onValueChange={(_, selected) => setStartDate(selected)}
            mode='datetime'
            i18nIsDynamicList
            locale='hu'
          />
        </View>
        <View style={styles.dateContainer}>
          <ThemedText style={[styles.dateText, { color: secondaryColor }]}>
            {t(`${TRANSLATION_PREFIX}end`)}
          </ThemedText>
          <DateTimePicker
            value={endDate}
            minimumDate={startDate}
            onValueChange={(_, selected) => setEndDate(selected)}
            mode='datetime'
            i18nIsDynamicList
            locale='hu'
            style={{
              marginTop: 10,
            }}
          />
        </View>
      </Card>
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingHorizontal: 10,
  },
  categoryDropdown: {
    marginTop: 8,
  },
  dateContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  dateText: {
    fontSize: 20,
  },
});
