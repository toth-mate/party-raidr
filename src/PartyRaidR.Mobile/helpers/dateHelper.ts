import {
  eachDayOfInterval,
  endOfMonth,
  endOfWeek,
  format,
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
