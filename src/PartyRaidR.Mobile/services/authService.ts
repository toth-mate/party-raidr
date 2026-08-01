import { apiClient } from '@/api/apiClient';
import { UserDto, UserLoginDto, UserRegisterDto } from '@/types/auth.types';

export const authService = {
  login: async (creds: UserLoginDto): Promise<string | undefined> => {
    try {
      const response = await apiClient.post('/auth/login', creds);
      return response.data;
    } catch (error) {
      console.error(`Failed to get user data: ${error}`);
    }
  },
  register: async (creds: UserRegisterDto): Promise<void> => {
    const response = await apiClient.post('/auth/register', creds);
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
