import { apiClient } from '@/api/apiClient';
import { UserDto, UserLoginDto } from '@/types/auth.types';

export const authService = {
  login: async (creds: UserLoginDto): Promise<string | undefined> => {
    try {
      const response = await apiClient.post('/auth/login', creds);
      return response.data;
    } catch (error) {
      console.error(`Failed to get user data: ${error}`);
    }
  },
  me: async (): Promise<UserDto | undefined> => {
    try {
      const response = await apiClient.get('/auth/me');
      return response.data;
    } catch (error) {
      console.error(`Failed to get user data: ${error}`);
    }
  },
};
