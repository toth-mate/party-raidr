import { StyleSheet, TextInput, View } from 'react-native';
import React from 'react';
import { Link } from 'expo-router';

import { useThemeColor } from '@/hooks/use-theme-color';
import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import ThemedButton from '@/components/themed-button';
import { Colors } from '@/constants/theme';

const Login = () => {
  const inputTextColor = useThemeColor({}, 'text');
  const backgroundColor = useThemeColor({}, 'inputFieldBackground');

  return (
    <ThemedView safe={true}>
      <ThemedView style={styles.container}>
        <ThemedText
          type='title'
          style={[styles.textCentered, {
            marginBottom: 5,
          }]}>Login</ThemedText>

        <ThemedText style={styles.textCentered}>Log in to your account!</ThemedText>


        <View style={styles.inputSection}>
          <ThemedText style={styles.inputLabel}>Email</ThemedText>
          <TextInput
            placeholder='example@mail.org'
            inputMode='email'
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
          onPress={() => console.log('Login')}/>

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