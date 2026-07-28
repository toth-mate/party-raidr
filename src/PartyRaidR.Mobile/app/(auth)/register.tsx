import React from 'react';
import { useTranslation } from 'react-i18next';
import { StyleSheet } from 'react-native';

import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import LabeledInput from '@/components/ui/labeled-input';
import ThemedButton from '@/components/themed-button';

const TRANSLATION_PREFIX = 'screens.auth.register.';

const Register = () => {
  const { t } = useTranslation();

  return (
    <ThemedView
      safe
      style={styles.container}>
      <ThemedText
        type='title'
        centered>
        {t(`${TRANSLATION_PREFIX}title`)}
      </ThemedText>
      <ThemedView style={styles.inputSection}>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}username`)}
          placeholder={t(`${TRANSLATION_PREFIX}placeholders.usernameExample`)}
        />
      </ThemedView>
      <ThemedView style={styles.inputSection}>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}email`)}
          placeholder={t(`${TRANSLATION_PREFIX}placeholders.emailExample`)}
        />
      </ThemedView>
      <ThemedView style={styles.inputSection}>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}password`)}
          placeholder={t(`${TRANSLATION_PREFIX}placeholders.password`)}
          secureTextEntry
        />
      </ThemedView>
      <ThemedView style={styles.inputSection}>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}passwordConfirm`)}
          placeholder={t(`${TRANSLATION_PREFIX}placeholders.passwordConfirm`)}
          secureTextEntry
        />
      </ThemedView>
      <ThemedView style={styles.inputSection}>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}dateOfBirth`)}
          placeholder={t(`${TRANSLATION_PREFIX}placeholders.emailExample`)}
          type='date'
        />
      </ThemedView>

      <ThemedButton
        title={t(`${TRANSLATION_PREFIX}buttonTitle`)} />
    </ThemedView>
  );
};

export default Register;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  inputSection: {
    width: '95%',
    marginTop: 15,
  },
});
