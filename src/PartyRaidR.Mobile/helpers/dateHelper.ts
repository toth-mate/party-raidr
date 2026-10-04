import {
  eachDayOfInterval,
  endOfMonth,
  endOfWeek,
  format,
  isSameDay,
  startOfMonth,
  startOfWeek,
} from 'date-fns';

export const getVisibleDates = (dateString: string) => {
  const currentDate = new Date(dateString);

  const startMonth = startOfMonth(currentDate),
    endMonth = endOfMonth(currentDate);

  // Get visible days from the preceding month on the grid
  const gridStart = startOfWeek(startMonth, { weekStartsOn: 1 }),
    gridEnd = endOfWeek(endMonth, { weekStartsOn: 1 });

  return eachDayOfInterval({
    start: gridStart,
    end: gridEnd,
  }).map(date => format(date, 'yyyy-MM-dd'));
};

export const getDateTimeDisplayInfo = (
  start: Date | string,
  end: Date | string,
) => {
  if (isSameDay(start, end)) {
    return {
      isTime: true,
      resultString: `${format(start, 'HH:mm')} - ${format(end, 'HH:mm')}`,
    };
  }
  return {
    isTime: false,
    resultString: `${format(start, 'yyyy-MM-dd')} - ${format(end, 'yyyy-MM-dd')}`,
  };
};
