const VALIDATION_PREFIX = 'screens.auth.register.validation.';

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

/**
 * Validates password.
 * @param value The password to validate.
 * @returns The error key if any.
 */
export const validatePassword = (value: string): string => {
    const password = value.trim();

    if(password.length < value.length) {
        return VALIDATION_PREFIX.concat('passwordContainsWhitespace');
    }

    if(password.length < 8) {
        return VALIDATION_PREFIX.concat('passwordLength');
    }

    let noUppercase = true, noLowercase = true, noNumber = true;
    for(let s of password) {
        if(noUppercase && s === s.toUpperCase()) {
            noUppercase = false;
        }
        if(noLowercase && s === s.toLowerCase()) {
            noLowercase = false;
        }
        if(noNumber && !isNaN(parseInt(s))) {
            noNumber = false;
        }
    }

    if(noUppercase) return VALIDATION_PREFIX.concat('passwordNoUppercase');
    if(noLowercase) return VALIDATION_PREFIX.concat('passwordNoLowercase');
    if(noNumber) return VALIDATION_PREFIX.concat('passwordNoNumber');

    return '';
};
