import { eventService } from '@/services/eventService';
import { BoundingBox } from '@/types/event.types';
import { keepPreviousData, useQuery } from '@tanstack/react-query';
import { useCurrentUser } from './use-auth-queries';

export const useMapEvents = (bounds: BoundingBox | null) => {
  return useQuery({
    queryKey: ['events', 'map', bounds],
    queryFn: () => eventService.getMarkerEvents(bounds!),
    enabled: !!bounds,
    placeholderData: keepPreviousData,
  });
};

export const useCalendar = (minDate: string, maxDate: string) => {
  const { data: user, isPending } = useCurrentUser();

  return useQuery({
    queryKey: ['events', 'calendar', minDate, maxDate],
    queryFn: () => eventService.getCalendarEntries(minDate, maxDate),
    enabled: !isPending && !!user,
  });
};
