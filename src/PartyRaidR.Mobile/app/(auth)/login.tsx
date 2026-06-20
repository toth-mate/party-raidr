import { StyleSheet, Text, View } from 'react-native';
import React from 'react';
import { ThemedView } from '@/components/themed-view';
import { ThemedText } from '@/components/themed-text';

const Login = () => {
  return (
    <ThemedView safe={true}>
      <ThemedText>Login</ThemedText>
    </ThemedView>
  );
}

export default Login;

const styles = StyleSheet.create({});