import { eventService } from '@/services/eventService';
import { useAuthStore } from '@/store/useAuthStore';
import { BoundingBox } from '@/types/event.types';
import { keepPreviousData, useQuery } from '@tanstack/react-query';

export const useMapEvents = (bounds: BoundingBox | null) => {
  return useQuery({
    queryKey: ['events', 'map', bounds],
    queryFn: () => eventService.getMarkerEvents(bounds!),
    enabled: !!bounds,
    placeholderData: keepPreviousData,
  });
};

export const useCalendar = (minDate: string, maxDate: string) => {
  const user = useAuthStore(state => state.user);

  return useQuery({
    queryKey: ['events', 'calendar', minDate, maxDate],
    queryFn: () => eventService.getCalendarEntries(minDate, maxDate),
    enabled: !!user,
  });
};
