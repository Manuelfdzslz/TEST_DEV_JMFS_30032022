$(document).ready(function () {
    $("#formLog").validate({
        rules: {
            Password: "required",
            Email: {
                required: true,
                email: true
            }
        }
    });
});