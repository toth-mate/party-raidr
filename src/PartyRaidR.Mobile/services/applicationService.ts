import { apiClient } from '@/api/apiClient';
import { ApplicationDto } from '@/types/application.types';

export const applicationService = {
  apply: async (application: ApplicationDto): Promise<void> => {
    await apiClient.post('/application', application);
  },
  exists: async (id: string): Promise<boolean> => {
    const response = await apiClient.get<boolean>('/application/exists', {
      params: { eventId: id },
    });
    return response.data;
  },
};
