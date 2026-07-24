import axios from 'axios';
import { Platform } from 'react-native';
import Constants from 'expo-constants';
import * as Device from 'expo-device';
import * as SecureStore from 'expo-secure-store';

const getBaseUrl = () => {
  if (!__DEV__) {
    // For now, there is no deployed backend, this is just a placeholder.
    return 'https://partyraidr.azurewebsites.net';
  }

  const debuggerHost = Constants.expoConfig?.hostUri;
  const localIp = debuggerHost ? debuggerHost.split(':')[0] : null;

  if (Platform.OS === 'android') {
    if (!Device.isDevice) {
      return 'http://10.0.2.2:8080/api';
    }
    return localIp ? `http://${localIp}:8080/api` : 'http://10.0.2.2:8080/api';
  } else {
    if (Device.isDevice && localIp) {
      return `http://${localIp}:8080/api`;
    }
    return 'http://localhost:8080/api';
  }
};

export const apiClient = axios.create({
  baseURL: getBaseUrl(),
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
    Accept: 'application/json',
  },
});

apiClient.interceptors.request.use(
  async config => {
    try {
      const token = await SecureStore.getItemAsync('auth_token');

      if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
      }
    } catch (error) {
      console.error(`Failed to retrieve authorization token: ${error}`);
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  },
);

apiClient.interceptors.response.use(
  response => response,
  async error => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        await SecureStore.deleteItemAsync('auth_token');
        // TODO: Implement navigation to login screen or refresh token.
      } catch (cleanUpError) {
        console.error(
          `Failed to clean up authorization token: ${cleanUpError}`,
        );
      }
    }

    return Promise.reject(error);
  },
);
