import { useMemo } from 'react';
import { useTranslation } from 'react-i18next';
import { FlatList, Pressable, StyleSheet, View } from 'react-native';

import { useThemeColor } from '@/hooks/use-theme-color';

import { ThemedText } from '../themed-text';

type DropdownProps = {
  placeholderKey: string;
  label?: string;
  data: string[] | Map<string, number>;
  onSelect<T>(s: T): void;
};

const Dropdown = ({ placeholderKey, label, data, onSelect }: DropdownProps) => {
  const { t } = useTranslation();
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');

  const dataTypeIsMap = useMemo(() => {
    return (typeof data).toLowerCase().includes('map');
  }, [data]);

  const dataResult = useMemo(
    () => (dataTypeIsMap ? (data as string[]) : data.keys().toArray()),
    [data, dataTypeIsMap],
  );

  const option = (dataLabel: string) => (
    <Pressable onPress={() => onSelect(dataLabel)}>
      <ThemedText>{dataLabel}</ThemedText>
    </Pressable>
  );

  console.log(data);

  return (
    <>
      {label && <ThemedText type='subtitle'>{label}</ThemedText>}
      <View style={[{ backgroundColor }]}>
        <FlatList
          data={dataResult as string[]}
          renderItem={({ item }) => option(item)}
        />
      </View>
    </>
  );
};

export default Dropdown;

const styles = StyleSheet.create({});
