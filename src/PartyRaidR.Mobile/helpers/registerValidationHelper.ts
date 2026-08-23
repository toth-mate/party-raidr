import { AxiosError } from 'axios';

const VALIDATION_PREFIX = 'screens.auth.register.validation.';

/**
 * Validates the username by length and format - it can not contain special characters.
 * @param value The username to validate
 * @returns The error key if any.
 */
export const validateUsername = (value: string): string => {
  const username = value.trim();
  let error: string = '';

  if (username.length < 3 || username.length > 15) {
    error = VALIDATION_PREFIX.concat('usernameLength');
  } else if (!username.match(/^[a-zA-Z][a-zA-Z0-9._]{2,15}$/)) {
    error = VALIDATION_PREFIX.concat('invalidUsernameFormat');
  }

  return error.length > 0 ? error : '';
};

/**
 * Validates the email address.
 * @param value The email to validate
 * @returns The error key if any.
 */
export const validateEmail = (value: string): string => {
  const emailRegex = /(^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$)/;
  const email = value.trim();
  let error: string = '';

  if (!emailRegex.test(email)) {
    error = VALIDATION_PREFIX.concat('emailInvalid');
  }

  return error.length > 0 ? error : '';
};

/**
 * Validates password.
 * @param value The password to validate.
 * @returns The error key if any.
 */
export const validatePassword = (value: string): string => {
  const password = value.trim();

  if (password.length < value.length) {
    return VALIDATION_PREFIX.concat('passwordContainsWhitespace');
  }

  if (password.length < 8) {
    return VALIDATION_PREFIX.concat('passwordLength');
  }

  let noUppercase = true,
    noLowercase = true,
    noNumber = true;
  for (let s of password) {
    if (noUppercase && s === s.toUpperCase()) {
      noUppercase = false;
    }
    if (noLowercase && s === s.toLowerCase()) {
      noLowercase = false;
    }
    if (noNumber && !isNaN(parseInt(s))) {
      noNumber = false;
    }
  }

  if (noUppercase) return VALIDATION_PREFIX.concat('passwordNoUppercase');
  if (noLowercase) return VALIDATION_PREFIX.concat('passwordNoLowercase');
  if (noNumber) return VALIDATION_PREFIX.concat('passwordNoNumber');

  return '';
};

/**
 * Returns the appropriate error message key based on the Axios error for registration.
 * @param error An Axios error exception
 * @returns The complete translation key for the error message
 */
export const getErrorMessageKey = (error: AxiosError): string => {
  const toastPrefix = 'screens.auth.register.toast.error.';

  if (error.response?.data && typeof error.response.data === 'string') {
    if (error.response.status === 409) {
      if (error.response.data.includes('username')) {
        return `${toastPrefix}usernameInUse`;
      }
      return `${toastPrefix}emailInUse`;
    } else if (error.response.data.includes('16')) {
      return `${toastPrefix}tooYoung`;
    } else if (error.response.data.includes('password')) {
      return `${toastPrefix}invalidPassword`;
    } else if (error.response.data.includes('email')) {
      return `${toastPrefix}invalidEmail`;
    } else if (error.response.data.includes('username')) {
      return `${toastPrefix}invalidUsername`;
    }
  }
  return `${toastPrefix}unknown`;
};
