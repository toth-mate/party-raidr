import { Feather } from '@expo/vector-icons';
import { useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import {
  FlatList,
  Pressable,
  StyleProp,
  StyleSheet,
  View,
  ViewStyle,
} from 'react-native';

import { Colors } from '@/constants/theme';
import { useThemeColor } from '@/hooks/use-theme-color';

import { ThemedText } from '../themed-text';

type DropdownProps = {
  label?: string;
  data: string[] | Map<string, number>;
  onSelect<T>(s: T): void;
  style?: StyleProp<ViewStyle>;
};

const Dropdown = ({ label, data, onSelect, style }: DropdownProps) => {
  const { t } = useTranslation();
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');
  const inputBackgroundColor = useThemeColor({}, 'background');
  const labelColor = useThemeColor({}, 'icon');
  const textColor = useThemeColor({}, 'text');

  const [isOpen, setIsOpen] = useState(false);

  const dataTypeIsMap = useMemo(() => {
    return (typeof data).toLowerCase().includes('map');
  }, [data]);

  const dataResult = useMemo(
    () => (dataTypeIsMap ? (data as string[]) : data.keys().toArray()),
    [data, dataTypeIsMap],
  );

  const [selected, setSelected] = useState<string>(t(dataResult[0] as string));

  const option = (dataLabel: string) => (
    <Pressable
      onPress={() => {
        onSelect(dataLabel);
        setSelected(t(dataLabel));
        setIsOpen(false);
      }}
      style={[styles.option, { borderColor: backgroundColor }]}>
      <ThemedText>{t(`${dataLabel}`)}</ThemedText>
    </Pressable>
  );

  return (
    <View style={style}>
      {label && (
        <ThemedText
          type='subtitle'
          style={{ color: labelColor, fontWeight: 'normal' }}>
          {label}
        </ThemedText>
      )}
      <View style={[{ backgroundColor }]}>
        <Pressable
          onPress={() => setIsOpen(!isOpen)}
          style={[
            styles.box,
            { backgroundColor: inputBackgroundColor },
            isOpen && { ...styles.boxOpen, borderColor: backgroundColor },
          ]}>
          <ThemedText>{selected}</ThemedText>
          <Feather
            name={isOpen ? 'chevron-up' : 'chevron-down'}
            size={14}
            color={textColor}
          />
        </Pressable>
        {isOpen && (
          <FlatList
            data={dataResult as string[]}
            renderItem={({ item }) => option(item)}
            style={{
              backgroundColor: inputBackgroundColor,
              borderBottomLeftRadius: 15,
              borderBottomRightRadius: 15,
            }}
          />
        )}
      </View>
    </View>
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
  boxOpen: {
    borderBottomLeftRadius: 0,
    borderBottomRightRadius: 0,
    borderBottomWidth: 1,
  },
  option: {
    paddingVertical: 5,
    paddingHorizontal: 10,
    borderBottomWidth: 1,
  },
});
