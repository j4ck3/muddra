const validateForm = formSelector => {
    const formElement = document.querySelector(formSelector);
    const passwordRegExErrorMsg = "Password must be atleast 8 characters in length and include atleast one uppercase letter, one lowercase letter, one number, and one special character "
    const validationOptions = [
        {
            attribute: 'minlength',
            isValid: input =>
                input.value && input.value.length >= parseInt(input.minLength, 10),
            errorMessage: (input, label) =>
                `${label.textContent } m\u00E5ste inneh\u00E5lla minst ${ input.minLength } tecken.`
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
        const label = formGroup.querySelector('label');
        const input = formGroup.querySelector('input, textarea, select');
        const errorContainer = formGroup.querySelector('.errormsg');
        const errorIcon = formGroup.querySelector('.icon-error');
        const successIcon = formGroup.querySelector('.icon-success');
        //input.classList.add('border');
        let formGroupError = false;
        for (const option of validationOptions) {
            if (input.hasAttribute(option.attribute) && !option.isValid(input)) {
                errorContainer.textContent = option.errorMessage(input, label);
                //input.classList.add('border-danger');
                //input.classList.remove('border-success')
                errorIcon.classList.remove('d-none')
                successIcon.classList.add('d-none')
                formGroupError = true;
            }
        }

        if (!formGroupError) {
            errorContainer.textContent = '',
            //input.classList.add('border-success');
            //input.classList.remove('border-danger')
            errorIcon.classList.add('d-none')
            successIcon.classList.remove('d-none')
        }
    };


    Array.from(formElement.elements).forEach(element =>
        element.addEventListener('keyup', event => {
            validateSingleFormGroup(event.srcElement.parentElement);
        })
    );
};

validateForm('.validate-form');