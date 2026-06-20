import { StyleSheet, Text, TextInput, View } from 'react-native';
import React from 'react';
import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';
import { Colors } from '@/constants/theme';
import { useThemeColor } from '@/hooks/use-theme-color';

const Login = () => {
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
              backgroundColor: backgroundColor,
            }]}
          />
        </View>
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
    textAlign: 'center'
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
});