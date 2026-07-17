import { apiClient } from "@/api/apiClient";
import { EventDisplayDto, EventMarkerDto } from "@/types/event.types";

export const eventService = {
    getAllDisplay: async (): Promise<EventDisplayDto[]> => {
        try {
            const response = await apiClient.get<EventDisplayDto[]>('/event/display-all');
            return response.status === 200 ? response.data : [];
        } catch(error) {
            console.error(`Failed to fetch events: ${error}`);
            return [];
        }
    },
    getDisplayById: async (id: string): Promise<EventDisplayDto | undefined> => {
        try {
            const response = await apiClient.get<EventDisplayDto>(`/event/display/${id}`);
            return response.data;
        } catch(error) {
            console.error(`Failed to fetch event: ${error}`);
            return undefined;
        }
    },
    getMarkerEvents: async (): Promise<EventMarkerDto[]> => {
        try {
            const response = await apiClient.get<EventMarkerDto[]>('/event/marker-details');
            return response.data;
        } catch(error) {
            console.error(`Failed to fetch event marker details: ${error}`);
            return [];
        }
    },
};