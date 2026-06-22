import { StyleSheet, TextInput, View } from 'react-native';
import React, { useState } from 'react';
import { useRouter, Link } from 'expo-router';
import * as SecureStorage from 'expo-secure-store';

import { useThemeColor } from '@/hooks/use-theme-color';
import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import ThemedButton from '@/components/themed-button';
import { Colors } from '@/constants/theme';
import { authService } from '@/services/authService';
import { useAuthStore } from '@/store/useAuthStore';

const Login = () => {
  const router = useRouter();

  const inputTextColor = useThemeColor({}, 'text');
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');
  const secondaryTextColor = useThemeColor({}, 'secondaryText');

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const initialize = useAuthStore((state) => state.initializeAuth);

  const login = async () => {
    if(email && password) {
      const token = await authService.login({ email: email, password: password });

      if(token) {
        await SecureStorage.setItemAsync('auth_token', token);
        await initialize();
        router.replace('/profile');
      }
    }
  };

  return (
    <ThemedView safe={true}>
      <ThemedView style={styles.container}>
        <ThemedText
          type='title'
          style={[styles.textCentered, {
            marginBottom: 5,
          }]}>Login</ThemedText>

        <ThemedText style={[styles.textCentered, { color: secondaryTextColor }]}>Log in to your account!</ThemedText>

        <View style={styles.inputSection}>
          <ThemedText style={styles.inputLabel}>Email</ThemedText>
          <TextInput
            placeholder='example@mail.org'
            inputMode='email'
            onChangeText={(newEmail) => setEmail(newEmail)}
            style={[styles.input, {
              color: inputTextColor,
              backgroundColor: backgroundColor,
            }]}
          />
        </View>

        <View style={styles.inputSection}>
          <ThemedText style={styles.inputLabel}>Password</ThemedText>
          <TextInput
            placeholder='Password'
            inputMode='text'
            onChangeText={(newPassword) => setPassword(newPassword)}
            secureTextEntry
            style={[styles.input, {
              color: inputTextColor,
              backgroundColor: backgroundColor,
            }]}
          />
        </View>

        <ThemedButton
          style={{
            marginTop: 15,
          }}
          title='Login'
          onPress={login}/>

        <ThemedText style={styles.textCentered}>
          Don't have an account yet? <Link href='/' style={styles.link}>Register here!</Link>
        </ThemedText>
      </ThemedView>
    </ThemedView>
  );
}

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