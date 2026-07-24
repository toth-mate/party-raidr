import { Keyboard, StyleSheet, TextInput, View } from 'react-native';
import React, { useState } from 'react';
import { useRouter, Link } from 'expo-router';
import * as SecureStorage from 'expo-secure-store';
import Toast from 'react-native-toast-message';

import { useThemeColor } from '@/hooks/use-theme-color';
import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import ThemedButton from '@/components/themed-button';
import { Colors } from '@/constants/theme';
import { authService } from '@/services/authService';
import { useAuthStore } from '@/store/useAuthStore';
import { useTranslation } from 'react-i18next';

const TRANSLATION_PREFIX = 'screens.auth.login.';

const Login = () => {
  const { t } = useTranslation();
  const router = useRouter();

  const inputTextColor = useThemeColor({}, 'text');
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');
  const secondaryTextColor = useThemeColor({}, 'secondaryText');

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const initialize = useAuthStore(state => state.initializeAuth);

  const login = async () => {
    Keyboard.dismiss();

    if (email && password) {
      const token = await authService.login({
        email: email,
        password: password,
      });

      if (token) {
        await SecureStorage.setItemAsync('auth_token', token);
        await initialize();
        Toast.show({
          type: 'success',
          text1: t(`${TRANSLATION_PREFIX}toast.success`),
          text2: t(`${TRANSLATION_PREFIX}toast.welcome`),
          autoHide: true,
        });
        router.replace('/profile');
      }
    }
  };

  return (
    <ThemedView safe={true}>
      <ThemedView style={styles.container}>
        <ThemedText
          type='title'
          style={[
            styles.textCentered,
            {
              marginBottom: 5,
            },
          ]}>
          {t(`${TRANSLATION_PREFIX}title`)}
        </ThemedText>

        <ThemedText
          style={[styles.textCentered, { color: secondaryTextColor }]}>
          {t(`${TRANSLATION_PREFIX}secondaryTitle`)}
        </ThemedText>

        <View style={styles.inputSection}>
          <ThemedText style={styles.inputLabel}>
            {t(`${TRANSLATION_PREFIX}email`)}
          </ThemedText>
          <TextInput
            placeholder={t(`${TRANSLATION_PREFIX}placeholder.email`)}
            inputMode='email'
            onChangeText={newEmail => setEmail(newEmail)}
            style={[
              styles.input,
              {
                color: inputTextColor,
                backgroundColor: backgroundColor,
              },
            ]}
          />
        </View>

        <View style={styles.inputSection}>
          <ThemedText style={styles.inputLabel}>
            {t(`${TRANSLATION_PREFIX}password`)}
          </ThemedText>
          <TextInput
            placeholder={t(`${TRANSLATION_PREFIX}placeholder.password`)}
            inputMode='text'
            onChangeText={newPassword => setPassword(newPassword)}
            secureTextEntry
            style={[
              styles.input,
              {
                color: inputTextColor,
                backgroundColor: backgroundColor,
              },
            ]}
          />
        </View>

        <ThemedButton
          style={{
            marginTop: 15,
          }}
          title={t(`${TRANSLATION_PREFIX}title`)}
          onPress={login}
        />

        <ThemedText style={styles.textCentered}>
          {t(`${TRANSLATION_PREFIX}noAccountYet`)}{' '}
          <Link
            href='/'
            style={styles.link}>
            {t(`${TRANSLATION_PREFIX}register`)}
          </Link>
        </ThemedText>
      </ThemedView>
    </ThemedView>
  );
};

export default Login;

const styles = StyleSheet.create({
  container: {
    width: '98%',
    height: '50%',
    margin: 'auto',
    padding: 20,
  },
  textCentered: {
    textAlign: 'center',
  },
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
  inputLabel: {
    fontWeight: 600,
  },
  inputSection: {
    marginTop: 10,
  },
  link: {
    color: Colors.secondaryDarkText,
  },
});
