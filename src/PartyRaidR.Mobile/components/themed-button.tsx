import { ActivityIndicator, Pressable, StyleProp, StyleSheet, Text, View, ViewStyle } from 'react-native'
import React from 'react'
import { Colors } from '@/constants/theme';

export type ButtonVariant = 'primary' | 'secondary' | 'tertiary' | 'danger' | 'success' | 'warning' | 'info';

export type ThemedButtonProps = {
    title: string;
    onPress: () => void;
    variant?: ButtonVariant,
    isLoading?: boolean;
    disabled?: boolean;
    style?: StyleProp<ViewStyle>;
};

const ThemedButton = ({
    title,
    onPress,
    variant = 'primary',
    isLoading = false,
    disabled = false,
    style,
}: ThemedButtonProps) => {
    const applicableStyles = [
        styles.base,
        styles[variant],
        disabled || isLoading ? styles.disabled : null,
        style
    ];

  return (
    <Pressable
      onPress={onPress}
      disabled={disabled || isLoading}
      style={({ pressed }) => [
        ...applicableStyles,
        pressed && !disabled && !isLoading ? styles.pressed : null
      ]}
    >
        {isLoading ? (
            <ActivityIndicator size="small" color={variant === 'secondary' ? 'white' : 'black'} />
        ) : (
            <Text style={styles.text}>{title}</Text>
        )}
    </Pressable>
  )
}

export default ThemedButton;

const styles = StyleSheet.create({
    base: {
        backgroundColor: Colors.primary,
        padding: 18,
        borderRadius: 6,
        marginVertical: 10,
        color: '#fff',
        textAlign: 'center',
    },
    primary: {
        backgroundColor: Colors.primary,
    },
    secondary: {
        backgroundColor: Colors.secondary,
    },
    tertiary: {
        backgroundColor: Colors.tertiary,
    },
    danger: {
        backgroundColor: '#f44336',
    },
    success: {
        backgroundColor: '#4caf50',
    },
    warning: {
        backgroundColor: '#ff9800',
    },
    info: {
        backgroundColor: '#2196f3',
    },
    pressed: {
        opacity: 0.9,
    },
    disabled: {
        backgroundColor: '#9ca3af',
    },
    text: {
        fontSize: 16,
        letterSpacing: 0.5,
        color: '#fff',
        textAlign: 'center',
        fontWeight: 600,
    },
})