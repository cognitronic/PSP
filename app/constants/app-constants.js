/**
 * Created by Danny Schreiber on 4/29/14.
 */

app.constant('Constants',{
    VALIDATION_ERRORS: {
        INVALID_PASSWORD: "You must enter a valid password.",
        INVALID_PHONE: "You must enter a valid phone number.",
        INVALID_EMAIL: "You must enter a valid email address.",
        PASSWORD_MISMATCH: "Your new password does not match your confirmation password.",
        MISSING_QUESTION: "You must enter four security questions and answers.",
        MISSING_PASSWORD_CONFIRM: "You must re-type your password for confirmation."
    },
    ERR_TXT: {
        DATE: "Invalid date. Please use the format: MM/DD/YYYY.",
        FUTURE_DATE: "Invalid Date. Date cannot be in the future.",
        PHONE: "Invalid number. Please use the format: (XXX) XXX-XXXX",
        SSN: "Invalid SSN. Please use the format: XXX-XX-XXXX",
        EMAIL: "Invalid email address format.",
        URL: "Invalid URL format.",
        ZIP: "Invalid ZIP format.",
        REQUIRED: "This is a required field.",
        EXISTS: "This patient already exists.",
        PASSWORD: {
            MIN_LENGTH: "Password must be at least 7 characters",
            MAX_LENGTH: "Password cannot be longer than 50 characters",
            ILLEGAL_CHARS: "Illegal characters in password: only digits (0-9), lowercase letters (a-z), and uppercase letters (A-Z) are allowed",
            CONTAINS_DIGIT: "Password must contain at least one digit (0-9)",
            CONTAINS_LETTER: "Password must contain at least one letter (a-zA-Z)"
        }
    }
});