import { apiClient } from "@/api/apiClient";
import { UserLoginDto } from "@/types/auth.types";

export const authService = {
    login: async (creds: UserLoginDto): Promise<string | undefined> => {
        try {
            const response = await apiClient.post('/auth/login', creds);
            return response.data;
        } catch(error) {
            console.error(`Failed to get user data: ${error}`);
        }
    },
};