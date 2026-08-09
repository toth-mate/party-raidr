import { eventService } from '@/services/eventService';
import { BoundingBox } from '@/types/event.types';
import { keepPreviousData, useQuery } from '@tanstack/react-query';

export const useMapEvents = (bounds: BoundingBox) => {
  return useQuery({
    queryKey: ['events', 'map', bounds],
    queryFn: () => eventService.getMarkerEvents(bounds),
    enabled: !!bounds,
    placeholderData: keepPreviousData
  });
};
