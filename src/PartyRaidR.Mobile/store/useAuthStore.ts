import { create } from 'zustand';
import * as SecureStorage from 'expo-secure-store';
import Toast from 'react-native-toast-message';

import { authService } from '@/services/authService';
import { UserDto } from "@/types/auth.types";

export const useAuthStore = create<AuthState>((set) => ({
    user: null,
    isAuthenticated: false,
    isLoading: true,

    setUser: (user) => set({user, isAuthenticated: !!user}),

    initializeAuth: async () => {
        set({ isLoading: true });
        try {
            const token = await SecureStorage.getItemAsync('auth_token');

            if(!token) {
                set({ user: null, isAuthenticated: false });
                return;
            }

            const userData = await authService.me();
            if(userData) {
                set({ user: userData, isAuthenticated: true });
            }
        } catch(error) {
            console.error(`An error occured when initializing authentication: ${error}`);
            await SecureStorage.deleteItemAsync('auth_token');
            set({ user: null, isAuthenticated: false });

            Toast.show({
                type: 'error',
                text1: 'Error!',
                text2: 'An error occured when logging in. Please try again later.',
                autoHide: true,
            });
        } finally {
            set({ isLoading: false });
        }
    },
    logout: async () => {
        await SecureStorage.deleteItemAsync('auth_token');
        set({ user: null, isAuthenticated: false });
        Toast.show({
            type: 'info',
            text1: 'Successfully logged out',
            text2: 'You have been logged out.',
            autoHide: true,
        });
    },
}));

interface AuthState {
    user: UserDto | null;
    isAuthenticated: boolean;
    isLoading: boolean;

    setUser: (user: UserDto) => void;
    initializeAuth: () => Promise<void>;
    logout: () => Promise<void>;
}