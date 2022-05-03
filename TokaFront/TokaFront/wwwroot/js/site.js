function setFormValidation(id) {
    $(id).validate({
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-danger');
            $(element).closest('.form-check').removeClass('has-success').addClass('has-danger');
            $(element).closest('.form-control').removeClass('is-valid').addClass('is-invalid');
            $(element).closest('.form-group').children('.error').removeClass('valid-feedback').addClass("invalid-feedback");
        },
        success: function (label, element) {
            $(label).closest('.form-group').removeClass('has-danger').addClass('has-success');
            $(label).closest('.form-check').removeClass('has-danger').addClass('has-success');
            $(label).closest('.form-group').children('.form-control').removeClass('is-invalid').addClass('is-valid');

            label.removeClass('invalid-feedback').addClass("valid-feedback");

        },
        errorPlacement: function (error, element) {
            $(error).removeClass('valid-feedback').addClass('invalid-feedback');
            $(element).closest('.form-group').append(error).addClass('has-danger');
        },
    });
}

function resetFormValidations() {
    $('.form-group').removeClass('has-success');
    $('.form-group').removeClass('has-danger');
    $('.form-control').removeClass('is-invalid');
    $('.form-control').removeClass('is-valid');
}
