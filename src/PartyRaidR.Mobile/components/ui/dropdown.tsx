import { Feather } from '@expo/vector-icons';
import { useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { FlatList, Pressable, StyleSheet, View } from 'react-native';

import { Colors } from '@/constants/theme';
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
  const inputBackgroundColor = useThemeColor({}, 'background');

  const [isOpen, setIsOpen] = useState(false);

  const dataTypeIsMap = useMemo(() => {
    return (typeof data).toLowerCase().includes('map');
  }, [data]);

  const dataResult = useMemo(
    () => (dataTypeIsMap ? (data as string[]) : data.keys().toArray()),
    [data, dataTypeIsMap],
  );

  const [selected, setSelected] = useState<string>(dataResult[0] as string);

  const option = (dataLabel: string) => (
    <Pressable
      onPress={() => {
        onSelect(dataLabel);
        setSelected(dataLabel);
        setIsOpen(false);
      }}>
      <ThemedText>{dataLabel}</ThemedText>
    </Pressable>
  );

  return (
    <>
      {label && <ThemedText type='subtitle'>{label}</ThemedText>}
      <View style={[{ backgroundColor }]}>
        <Pressable
          onPress={() => setIsOpen(!isOpen)}
          style={[styles.box, { backgroundColor: inputBackgroundColor }]}>
          <ThemedText>{selected}</ThemedText>
          <Feather
            name={isOpen ? 'chevron-up' : 'chevron-down'}
            size={14}
          />
        </Pressable>
        {isOpen && (
          <FlatList
            data={dataResult as string[]}
            renderItem={({ item }) => option(item)}
          />
        )}
      </View>
    </>
  );
};

export default Dropdown;

const styles = StyleSheet.create({
  box: {
    marginTop: 5,
    padding: 10,
    borderRadius: 15,
    fontSize: 16,
    shadowColor: Colors.primaryDarkText,
    shadowOpacity: 0.5,
    shadowOffset: { width: 0.5, height: 1 },
    shadowRadius: 1,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'baseline',
  },
});
