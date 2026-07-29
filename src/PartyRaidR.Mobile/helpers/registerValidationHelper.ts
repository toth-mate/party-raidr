const VALIDATION_PREFIX = 'register.validation.';

/**
 * Validates the username by length.
 * @param value The username to validate
 * @returns The error key if any.
 */
export const validateUsername = (value: string): string => {
    const username = value.trim();
    let error: string = '';
    
    if(username.length < 3 || username.length > 15) {
        error = VALIDATION_PREFIX.concat('usernameLength');
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

    if(!emailRegex.test(email)) {
        error = VALIDATION_PREFIX.concat('emailInvalid');
    }

    return error.length > 0 ? error : '';
};
