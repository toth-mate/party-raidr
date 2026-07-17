import { StyleSheet, Text, View } from 'react-native';
import React from 'react';
import { EventMarkerDto } from '@/types/event.types';
import { Link } from 'expo-router';
import { Colors } from '@/constants/theme';

type MarkerContentProps = {
  event: EventMarkerDto;
}

const MarkerContent = ({ event }: MarkerContentProps) => {
  const calculateStartTime = (): string => {
    const startDate = new Date(event.startingDate);
    const now = new Date();

    const diffInMs = startDate.getTime() - now.getTime();
    const diffInHours = diffInMs / (1000 * 60 * 60);
    const diffInDays = Math.floor(diffInHours / 24);
    const diffInWeeks = Math.floor(diffInDays / 7);
    const diffInMonths = Math.floor(diffInDays / 30);
    const diffInYears = Math.floor(diffInDays / 365);
    
    if(diffInYears > 0) {
      return `${diffInYears} year(s)`;
    } else if(diffInMonths > 0) {
      return `${diffInMonths} month(s)`;
    } else if(diffInWeeks > 0) {
      return `${diffInWeeks} week(s)`;
    } else if(diffInDays > 0) {
      return `${diffInDays} day(s)`;
    } else if(diffInHours > 0) {
      return `${diffInHours} hour(s)`;
    } else {
      return 'less than an hour';
    }
  };

  return (
    <View style={styles.container}>
      <Text style={{fontWeight: 'bold'}}>{event.title}</Text>
      <Text>{event.address}</Text>
      <Text>Starts in: {calculateStartTime()}</Text>
      <Text style={styles.tooltip}>Tap here to view details</Text>
    </View>
  )
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