const validateForm = formSelector => {
    const formElement = document.querySelector(formSelector);
    if (!formElement) return; // Exit if form not found
    
    const passwordRegExErrorMsg = "Password must be atleast 8 characters in length and include atleast one uppercase letter, one lowercase letter, one number, and one special character "
    const validationOptions = [
        {
            attribute: 'minlength',
            isValid: input =>
                input.value && input.value.length >= parseInt(input.minLength, 10),
            errorMessage: (input, label) =>
                `${label.textContent} m\u00E5ste inneh\u00E5lla minst ${input.minLength } bokst\u00E4ver.`
        },
        {
            attribute: 'custommaxlength',
            isValid: input =>
                input.value.length <=
                parseInt(input.getAttribute('custommaxlength'), 10),
            errorMessage: (input, label) =>
                `${label.textContent} needs to be less than ${input.getAttribute(
                    'custommaxlength'
                )} characters`,
        },
        {
            attribute: 'pattern',
            isValid: input => {
                const patternRegex = new RegExp(input.pattern);
                return patternRegex.test(input.value);
            },
            errorMessage: (input, label) => {
                if (label.textContent == "Password") {
                    return `${passwordRegExErrorMsg}`
                } else {
                    return `Inte en giltig ${label.textContent}`
                }
            }
        },
        {
            attribute: 'match',
            isValid: input => {
                const matchSelector = input.getAttribute('match');
                const matchedElem = document.querySelector(`#${matchSelector}`);
                return matchedElem && matchedElem.value.trim() === input.value.trim();
            },
            errorMessage: (input, label) => {
                return `Password does not match`;
            },
        },
        {
            attribute: 'required',
            isValid: input => input.value.trim() !== '',
            errorMessage: (input, label) => `${label.textContent} \u00E4r ett obligatoriskt f\u00E4lt`,
        },
    ];

    const validateSingleFormGroup = formGroup => {
        // formGroup might be .relative container - get parent for label and errormsg
        const parentGroup = formGroup.classList.contains('relative') ? formGroup.parentElement : formGroup;
        
        const label = parentGroup.querySelector('label');
        const input = formGroup.querySelector('input, textarea, select');
        const errorContainer = parentGroup.querySelector('.errormsg');
        const errorIcon = formGroup.querySelector('.icon-error');
        const successIcon = formGroup.querySelector('.icon-success');
        
        if (!input || !errorIcon || !successIcon) return;
        
        let formGroupError = false;
        for (const option of validationOptions) {
            if (input.hasAttribute(option.attribute) && !option.isValid(input)) {
                if (errorContainer) errorContainer.textContent = option.errorMessage(input, label);
                errorIcon.classList.remove('hidden')
                successIcon.classList.add('hidden')
                formGroupError = true;
                break;
            }
        }

        if (!formGroupError) {
            if (errorContainer) errorContainer.textContent = '';
            errorIcon.classList.add('hidden')
            successIcon.classList.remove('hidden')
        }
    };


    Array.from(formElement.elements).forEach(element =>
        element.addEventListener('keyup', event => {
            validateSingleFormGroup(event.srcElement.parentElement);
        })
    );

    // Validate entire form before submission
    const validateEntireForm = () => {
        let hasErrors = false;
        // Find all inputs/textarea that have validation indicators nearby
        const inputs = formElement.querySelectorAll('input:not([type="hidden"]):not(.bg-transparent), textarea:not(.bg-transparent), select:not(.bg-transparent)');
        
        inputs.forEach(input => {
            // Skip honeypot fields and submit buttons
            if (input.type === 'submit' || input.type === 'button' || input.classList.contains('bg-transparent')) {
                return;
            }
            
            // Find the parent form group (look for closest parent with validation indicators)
            let formGroup = input.closest('.relative');
            if (!formGroup) {
                formGroup = input.parentElement;
            }
            
            const label = formGroup.querySelector('label');
            const errorContainer = formGroup.querySelector('.errormsg');
            const errorIcon = formGroup.querySelector('.icon-error');
            const successIcon = formGroup.querySelector('.icon-success');
            let formGroupError = false;
            
            for (const option of validationOptions) {
                if (input.hasAttribute(option.attribute) && !option.isValid(input)) {
                    if (errorContainer) errorContainer.textContent = option.errorMessage(input, label);
                    if (errorIcon) errorIcon.classList.remove('hidden');
                    if (successIcon) successIcon.classList.add('hidden');
                    formGroupError = true;
                    hasErrors = true;
                    break;
                }
            }
            
            if (!formGroupError && input.value.trim() !== '') {
                if (errorContainer) errorContainer.textContent = '';
                if (errorIcon) errorIcon.classList.add('hidden');
                if (successIcon) successIcon.classList.remove('hidden');
            }
        });
        
        return !hasErrors;
    };

    // Prevent form submission if there are errors
    formElement.addEventListener('submit', event => {
        if (!validateEntireForm()) {
            event.preventDefault();
            event.stopPropagation();
            
            // Scroll to first error
            const firstError = formElement.querySelector('.icon-error:not(.hidden)');
            if (firstError) {
                const formGroup = firstError.closest('.relative, [class*="form-group"]');
                if (formGroup) {
                    formGroup.scrollIntoView({ behavior: 'smooth', block: 'center' });
                    const input = formGroup.querySelector('input, textarea, select');
                    if (input) {
                        setTimeout(() => input.focus(), 300);
                    }
                }
            }
            
            return false;
        }
    });
};

validateForm('.validate-form');