import { ActivityIndicator, Pressable, StyleSheet, Text } from 'react-native'
import React from 'react'
import Ionicons from 'react-native-vector-icons/Ionicons';
import { Colors } from '@/constants/theme';
import { useThemeColor } from '@/hooks/use-theme-color';
import { ThemedButtonProps } from '@/types/props.types';

const ThemedButton = ({
    title,
    icon,
    onPress,
    variant = 'primary',
    outline = false,
    isLoading = false,
    disabled = false,
    color = '#fff',
    borderColor,
    style,
}: ThemedButtonProps) => {
    const applicableStyles = [
        styles.base,
        styles[variant],
        disabled || isLoading ? styles.disabled : null,
        style
    ];
    const textColor = useThemeColor({}, 'text');

  return (
    <Pressable
      onPress={onPress}
      disabled={disabled || isLoading}
      style={({ pressed }) => [
        ...applicableStyles,
        pressed && !disabled && !isLoading ? styles.pressed : null,
        outline ? { borderWidth: 1, borderColor: textColor, backgroundColor: 'transparent' } : null,
        borderColor ? { borderColor } : null,
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
                        color={outline ? textColor : 'white'}
                        style={[
                            title ? { marginRight: 8 } : null,
                            { color }
                        ]}
                    />
                )}
                {title && (
                    <Text style={[
                        styles.text,
                        { color },
                        outline ? { color: textColor} : null,
                    ]}>
                        {title}
                    </Text>
                )}
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
        flexDirection: 'row',
        justifyContent: 'center',
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
        textAlign: 'center',
        fontWeight: 600,
    },
});