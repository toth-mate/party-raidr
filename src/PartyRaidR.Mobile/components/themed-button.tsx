import { ActivityIndicator, Pressable, StyleProp, StyleSheet, Text, View, ViewStyle } from 'react-native'
import React from 'react'
import Ionicons from 'react-native-vector-icons/Ionicons';
import { Colors } from '@/constants/theme';

type ButtonVariant = 'primary' | 'secondary' | 'tertiary' | 'danger' | 'success' | 'warning' | 'info';

type ThemedButtonProps = {
    title?: string;
    onPress: () => void;
    variant?: ButtonVariant,
    isLoading?: boolean;
    disabled?: boolean;
    style?: StyleProp<ViewStyle>;
    icon?: string;
};

const ThemedButton = ({
    title,
    icon,
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
            <>
                {icon && (
                    <Ionicons
                        name={icon}
                        size={20}
                        color="#fff"
                        style={[title ? { marginRight: 8 } : null]}
                    />
                )}
                <Text style={styles.text}>{title}</Text>
            </>
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
        flexDirection: 'row',
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
});