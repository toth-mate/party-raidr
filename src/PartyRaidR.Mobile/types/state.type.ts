import { UserDto } from './auth.types';

export interface AuthState {
  user: UserDto | null;
  isAuthenticated: boolean;
  isLoading: boolean;

  setUser: (user: UserDto) => void;
  initializeAuth: () => Promise<void>;
  logout: () => Promise<void>;
}

export interface LocationState {
  lat: number | undefined;
  lng: number | undefined;
  loadLocation: () => Promise<void>;
}
