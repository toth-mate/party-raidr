import { apiClient } from '@/api/apiClient';
import {
  BoundingBox,
  EventDisplayDto,
  EventMarkerDto,
  UpcomingEventDto,
} from '@/types/event.types';

export const eventService = {
  getAllDisplay: async (): Promise<EventDisplayDto[]> => {
    try {
      const response =
        await apiClient.get<EventDisplayDto[]>('/event/display-all');
      return response.status === 200 ? response.data : [];
    } catch (error) {
      console.error(`Failed to fetch events: ${error}`);
      return [];
    }
  },
  getDisplayById: async (id: string): Promise<EventDisplayDto | undefined> => {
    try {
      const response = await apiClient.get<EventDisplayDto>(
        `/event/display/${id}`,
      );
      return response.data;
    } catch (error) {
      console.error(`Failed to fetch event: ${error}`);
      return undefined;
    }
  },
  getMarkerEvents: async (
    boundingBox: BoundingBox,
  ): Promise<EventMarkerDto[]> => {
    const response = await apiClient.get<EventMarkerDto[]>(
      `/event/marker-details`,
      {
        params: boundingBox,
      },
    );
    return response.data;
  },
  getUpcomingEvents: async (): Promise<UpcomingEventDto[]> => {
    try {
      const response =
        await apiClient.get<UpcomingEventDto[]>('/event/upcoming');
      return response.data;
    } catch (error) {
      console.error(`Failed to fetch upcoming events: ${error}`);
      return [];
    }
  },
  getNearbyEvents: async (
    latitude: number,
    longitude: number,
    radius: number,
  ): Promise<UpcomingEventDto[]> => {
    try {
      const response = await apiClient.get<UpcomingEventDto[]>(
        `/event/nearby?latitude=${latitude}&longitude=${longitude}&radiusInKm=${radius}`,
      );
      return response.data;
    } catch (error) {
      console.error(`Failed to fetch nearby events: ${error}`);
      return [];
    }
  },
};
