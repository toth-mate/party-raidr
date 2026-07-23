import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import * as Localization from 'expo-localization';
import en from './locales/en.json';
import hu from './locales/hu.json';

const systemLng = Localization.getLocales()[0]?.languageCode ?? 'en';

i18n
    .use(initReactI18next)
    .init({
        resources: {
            en: { translation: en },
            hu: { translation: hu },
        },
        lng: systemLng,
        fallbackLng: 'en',
        interpolation: {
            escapeValue: false,
        },
    });

export default i18n;