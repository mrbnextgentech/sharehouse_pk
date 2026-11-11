var $form = $("#signUpForm");
$form.unbind();
$form.data("validator", null);
$.validator.unobtrusive.parse($form);
$form.validate($form.data("unobtrusiveValidation").options);

