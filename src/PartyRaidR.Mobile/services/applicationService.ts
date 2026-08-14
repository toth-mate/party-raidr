import { apiClient } from '@/api/apiClient';

export const applicationService = {
  exists: async (id: string): Promise<boolean> => {
    const response = await apiClient.get<boolean>('/application/exists', {
      params: id,
    });
    return response.data;
  },
};
