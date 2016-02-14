function submitForm(formName) {
    var form = $(document.forms[formName]);

    form.validate();

    if (form.valid()) {
        document.forms[formName].submit();
    }
}