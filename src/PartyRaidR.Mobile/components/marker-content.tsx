import React from 'react';
import { useTranslation } from 'react-i18next';
import { StyleSheet, Text, View } from 'react-native';

import { Colors } from '@/constants/theme';
import { MarkerContentProps } from '@/types/props.types';

const TRANSLATION_PREFIX = 'tabs.map.marker.';

const MarkerContent = ({ event }: MarkerContentProps) => {
  const { t } = useTranslation();
  const calculateStartTime = (): string => {
    const startDate = new Date(event.startingDate);
    const now = new Date();

    const diffInMs = startDate.getTime() - now.getTime();
    const diffInHours = diffInMs / (1000 * 60 * 60);
    const diffInDays = Math.floor(diffInHours / 24);
    const diffInWeeks = Math.floor(diffInDays / 7);
    const diffInMonths = Math.floor(diffInDays / 30);
    const diffInYears = Math.floor(diffInDays / 365);

    if (diffInYears > 0) {
      return `${diffInYears} ${t(`${TRANSLATION_PREFIX}years`)}`;
    } else if (diffInMonths > 0) {
      return `${diffInMonths} ${t(`${TRANSLATION_PREFIX}months`)}`;
    } else if (diffInWeeks > 0) {
      return `${diffInWeeks} ${t(`${TRANSLATION_PREFIX}weeks`)}`;
    } else if (diffInDays > 0) {
      return `${diffInDays} ${t(`${TRANSLATION_PREFIX}days`)}`;
    } else if (diffInHours > 0) {
      return `${diffInHours} ${t(`${TRANSLATION_PREFIX}hours`)}`;
    } else {
      return t(`${TRANSLATION_PREFIX}lessThan`);
    }
  };

  return (
    <View style={styles.container}>
      <Text style={{ fontWeight: 'bold' }}>{event.title}</Text>
      <Text>{event.address}</Text>
      <Text>
        {t(`${TRANSLATION_PREFIX}start`)} {calculateStartTime()}
      </Text>
      <Text style={styles.tooltip}>{t(`${TRANSLATION_PREFIX}details`)}</Text>
    </View>
  );
};

export default MarkerContent;

const styles = StyleSheet.create({
  container: {
    backgroundColor: Colors.light.background,
    padding: 10,
    borderRadius: 10,
    boxShadow: '2px 4px 6px rgba(0, 0, 0, 0.3)',
  },
  tooltip: {
    color: '#777',
    marginTop: 3,
  },
});
