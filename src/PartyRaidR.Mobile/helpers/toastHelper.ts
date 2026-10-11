import { useTranslation } from 'react-i18next';
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
  const { t } = useTranslation();

  Toast.show({
    text1: t(config.title),
    text2: t(config.text),
    ...config,
  });
};
