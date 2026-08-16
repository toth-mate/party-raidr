import { applicationService } from '@/services/applicationService';
import { ApplicationDto } from '@/types/application.types';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const useApply = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (application: ApplicationDto) =>
      applicationService.apply(application),
    onSuccess: (_, application: ApplicationDto) => {
      queryClient.invalidateQueries({
        queryKey: ['application', 'exists', { eventId: application.eventId }],
      });
    },
  });
};
