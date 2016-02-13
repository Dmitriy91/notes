function submitForm(formName) {
    if (document.querySelector("input[class~='input-validation-error']") == null) {
        document.forms[formName].submit();
    }
}