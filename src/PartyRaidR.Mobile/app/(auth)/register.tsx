import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { StyleSheet } from 'react-native';

import ThemedButton from '@/components/themed-button';
import { ThemedText } from '@/components/themed-text';
import { ThemedView } from '@/components/themed-view';
import LabeledInput from '@/components/ui/labeled-input';

const TRANSLATION_PREFIX = 'screens.auth.register.';

const Register = () => {
  const { t } = useTranslation();

  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [passwordConfirm, setPasswordConfirm] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState(new Date());

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
          value={username}
          onChangeText={setUsername}
        />
      </ThemedView>
      <ThemedView style={styles.inputSection}>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}email`)}
          placeholder={t(`${TRANSLATION_PREFIX}placeholders.emailExample`)}
          value={email}
          onChangeText={setEmail}
        />
      </ThemedView>
      <ThemedView style={styles.inputSection}>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}password`)}
          placeholder={t(`${TRANSLATION_PREFIX}placeholders.password`)}
          secureTextEntry
          value={password}
          onChangeText={setPassword}
        />
      </ThemedView>
      <ThemedView style={styles.inputSection}>
        <LabeledInput
          labelKey={t(`${TRANSLATION_PREFIX}passwordConfirm`)}
          placeholder={t(`${TRANSLATION_PREFIX}placeholders.passwordConfirm`)}
          secureTextEntry
          value={passwordConfirm}
          onChangeText={setPasswordConfirm}
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
        title={t(`${TRANSLATION_PREFIX}buttonTitle`)}
        onPress={() => {}}
      />
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
