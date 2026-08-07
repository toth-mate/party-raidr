import { getErrorMessageKey } from '@/helpers/registerValidationHelper';
import { authService } from '@/services/authService';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { useRouter } from 'expo-router';
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
