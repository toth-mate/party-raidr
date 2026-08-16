import { applicationService } from '@/services/applicationService';
import { ApplicationDto } from '@/types/application.types';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useTranslation } from 'react-i18next';
import Toast from 'react-native-toast-message';

export const useApply = () => {
  const queryClient = useQueryClient();
  const { t } = useTranslation();

  return useMutation({
    mutationFn: (application: ApplicationDto) =>
      applicationService.apply(application),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ['application', 'exists'],
      });

      Toast.show({
        text1: t('screens.event.toast.success.title'),
        text2: t('screens.event.toast.success.text'),
      });
    },
    onError: () =>
      Toast.show({
        type: 'error',
        text1: t('screens.event.toast.error.title'),
        text2: t('screens.event.toast.error.text'),
      }),
  });
};
