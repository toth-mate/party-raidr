import { getErrorMessageKey } from '@/helpers/registerValidationHelper';
import { authService } from '@/services/authService';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { useRouter } from 'expo-router';
import * as SecureStorage from 'expo-secure-store';
import { useTranslation } from 'react-i18next';
import Toast from 'react-native-toast-message';

const TRANSLATION_PREFIX = 'screens.auth.register.toast.';

export const useRegister = () => {
  const queryClient = useQueryClient();
  const { t } = useTranslation();
  const router = useRouter();

  return useMutation({
    mutationFn: authService.register,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['user'] });

      Toast.show({
        type: 'success',
        text1: t(`${TRANSLATION_PREFIX}success.title`),
        text2: t(`${TRANSLATION_PREFIX}success.text`),
      });
      router.replace('/(auth)/login');
    },
    onError: (error: Error) => {
      const errorMessage = getErrorMessageKey(error as AxiosError);

      Toast.show({
        type: 'error',
        text1: t(`${TRANSLATION_PREFIX}error.title`),
        text2: t(errorMessage),
      });
    },
  });
};

export const useLogin = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: authService.login,
    onSuccess: async token => {
      if (token) {
        await SecureStorage.setItemAsync('auth_token', token);
        queryClient.invalidateQueries({ queryKey: ['auth', 'user'] });
      }
    },
  });
};

export const useCurrentUser = () => {
  return useQuery({
    queryKey: ['auth', 'user'],
    queryFn: () => authService.me(),
    retry: false,
    networkMode: 'online',
  });
};
