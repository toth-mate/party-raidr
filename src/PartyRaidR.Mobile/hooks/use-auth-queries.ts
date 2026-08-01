import { authService } from '@/services/authService';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const useRegister = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: authService.register,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['user'] });
    },
    onError: error => {
      console.error(`Failed to register user: ${error}`);
    },
  });
};
