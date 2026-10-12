import Toast from 'react-native-toast-message';

type ToastConfig = {
  title: string;
  text: string;
  type: string;
  autoHide?: boolean;
  onShow?: () => void;
  onHide?: () => void;
};

export const manifestToast = (config: ToastConfig) => {
  Toast.show({
    text1: config.title,
    text2: config.text,
    ...config,
  });
};
