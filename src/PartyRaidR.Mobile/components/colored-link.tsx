import { Link, LinkProps } from 'expo-router';
import React from 'react';
import { StyleSheet } from 'react-native';

import { Colors } from '@/constants/theme';

const ColoredLink = (props: LinkProps) => {
  return (
    <Link
      {...props}
      style={styles.link}
    />
  );
};

export default ColoredLink;

const styles = StyleSheet.create({
  link: {
    color: Colors.secondary,
    textDecorationLine: 'underline',
  },
});
