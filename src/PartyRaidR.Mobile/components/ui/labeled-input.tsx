import { StyleSheet, TextInput, View } from 'react-native';

import { Colors } from '@/constants/theme';
import { useThemeColor } from '@/hooks/use-theme-color';
import { LabeledInputProps } from '@/types/props.types';

import { ThemedText } from '../themed-text';

const LabeledInput = ({
  labelKey,
  errorKey,
  labelType = 'small',
  ...props
}: LabeledInputProps) => {
  const inputTextColor = useThemeColor({}, 'text');
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');
  const labelTextColor = useThemeColor({}, 'icon');

  return (
    <View>
      <ThemedText style={[{ color: labelTextColor }, styles[labelType]]}>
        {labelKey}
      </ThemedText>
      <>
        <TextInput
          style={[
            styles.input,
            { color: inputTextColor, backgroundColor },
            props.inputStyle,
          ]}
          {...props}
        />
        {errorKey && (
          <ThemedText style={styles.errorText}>{errorKey}</ThemedText>
        )}
      </>
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
  small: {
    fontSize: 16,
  },
  big: {
    fontSize: 20,
  },
  errorText: {
    color: 'red',
    marginTop: 5,
    fontSize: 14,
  },
});
