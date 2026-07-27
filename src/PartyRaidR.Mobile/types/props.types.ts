import { Href, Link } from 'expo-router';
import { ComponentProps, PropsWithChildren, ReactElement } from 'react';
import { StyleProp, TextInputProps, TextProps, ViewProps, ViewStyle } from 'react-native';

import { EventMarkerDto } from './event.types';

export type ExternalLinkProps = Omit<ComponentProps<typeof Link>, 'href'> & {
  href: Href & string;
};

export type ParallaxScrollViewProps = PropsWithChildren<{
  headerImage: ReactElement;
  headerBackgroundColor: { dark: string; light: string };
}>;

export type MarkerContentProps = {
  event: EventMarkerDto;
};

export type ThemedButtonProps = {
  title?: string;
  onPress: () => void;
  variant?: ButtonVariant;
  isLoading?: boolean;
  disabled?: boolean;
  style?: StyleProp<ViewStyle>;
  icon?: string;
  outline?: boolean;
  color?: string;
  borderColor?: string;
  shadow?: boolean;
};

export type ThemedViewProps = ViewProps & {
  lightColor?: string;
  darkColor?: string;
  safe?: boolean;
};

export type ThemedTextProps = TextProps & {
  lightColor?: string;
  darkColor?: string;
  type?: 'default' | 'title' | 'defaultSemiBold' | 'subtitle' | 'link';
  centered?: boolean;
};

export type LabeledInputProps = TextInputProps & {
  labelKey?: string;
  type?: 'text' | 'date' | 'time';
  date?: Date;
};

type ButtonVariant =
  | 'primary'
  | 'secondary'
  | 'tertiary'
  | 'danger'
  | 'success'
  | 'warning'
  | 'info';
