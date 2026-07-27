import React from 'react';
import { StyleSheet, TextInput, View } from 'react-native';

import { Colors } from '@/constants/theme';
import { useThemeColor } from '@/hooks/use-theme-color';
import { LabeledInputProps } from '@/types/props.types';

import { ThemedText } from '../themed-text';

const LabeledInput = (props: LabeledInputProps) => {
  const inputTextColor = useThemeColor({}, 'text');
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');
  const labelTextColor = useThemeColor({}, 'icon');

  return (
    <View>
      <ThemedText style={{ color: labelTextColor }}>
        {props.labelKey}
      </ThemedText>
      <TextInput
        style={[styles.input, { color: inputTextColor, backgroundColor }]}
        {...props}
      />
    </View>
  );
};

export default LabeledInput;

const styles = StyleSheet.create({
  input: {
    marginTop: 5,
    padding: 10,
    borderRadius: 15,
    fontSize: 16,
    shadowColor: Colors.primaryDarkText,
    shadowOpacity: 0.5,
    shadowOffset: { width: 0.5, height: 1 },
    shadowRadius: 1,
  },
  labelText: {},
});
