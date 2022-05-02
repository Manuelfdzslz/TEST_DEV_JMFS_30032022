
$(document).ready(function () {
    $('#personaFisicaTable').DataTable();
    setFormValidation('#frmPersonaFisica')
});

function crearPersona() {
    cleanInputs();
    $('#modalCreateEdit').modal('show');
}

function cleanInputs() {
    
    $('#Nombre').val('');
    $('#ApellidoPaterno').val('');
    $('#ApellidoMaterno').val('');
    $('#RFC').val('');
    $('#FechaNacimiento').val('');
    $('#IdPersonaFisica').val('');

    $('#Nombre').removeClass("disabled");
    $('#ApellidoPaterno').removeClass("disabled");
    $('#ApellidoMaterno').removeClass("disabled");
    $('#RFC').removeClass("disabled");
    $('#FechaNacimiento').removeClass("disabled");
    $('#IdPersonaFisica').removeClass("disabled");
}
function detail(Id) {
    $.ajax({
        type: 'Get',
        url: '/PersonaFisica/Details',
        dataType: 'json',
        data: { id: Id },
        beforeSend: function () {

        },
        success: function (data) {
            console.log(data);
            if (data.isSuccess) {
                cleanInputs();
                let fecha = data.r.fechaNacimiento.split("T")
                $('#Nombre').val(data.r.nombre);
                $('#ApellidoPaterno').val(data.r.apellidoPaterno);
                $('#ApellidoMaterno').val(data.r.apellidoMaterno);
                $('#RFC').val(data.r.rfc);
                $('#FechaNacimiento').val(fecha[0]);
                $('#IdPersonaFisica').val(data.r.idPersonaFisica);

                $('#Nombre').addClass("disabled");
                $('#ApellidoPaterno').addClass("disabled");
                $('#ApellidoMaterno').addClass("disabled");
                $('#RFC').addClass("disabled");
                $('#FechaNacimiento').addClass("disabled");
                $('#IdPersonaFisica').addClass("disabled");
                $('#btnSave').hide();
                $('#btnUpdate').hide();
                $('#modalCreateEdit').modal('show');
            } else {
                console.log(data.message);
                Swal.fire({
                    type: 'error',
                    html: '<span>' + data.message + '</span>',
                    title: 'Error',
                    footer: ''
                });
            }
        },
        complete: function () {

        },
        error: function (xhr, status, error) {

            Swal.fire({
                type: 'error',
                title: '',
                text: error,
                footer: ''
            });
        }
    });
}

var validator
function create() {
    var form = $("#frmPersonaFisica");
    validator= form.validate();
    if (!form.valid())
        return;

    $.ajax({
        type: 'POST',
        url: '/PersonaFisica/Create',
        dataType: 'json',
        data: $("#frmPersonaFisica").serialize(),
        beforeSend: function () {
           
        },
        success: function (data) {
            if (data.isSuccess) {
                Swal.fire({
                    type: 'success',
                    title: 'Success',
                    text: data.message,
                    footer: '',
                    onAfterClose: () => {
                        
                        location.href = '/PersonaFisica/Index';
                        $('#modalCreateEdit').modal('hide');
                    }
                });
            } else {
                Swal.fire({
                    type: 'error',
                    html: '<span>' + data.message + '</span>',
                    title: 'Error',
                    footer: ''
                });
            }
        },
        complete: function () {
           
        },
        error: function (xhr, status, error) {
           
            Swal.fire({
                type: 'error',
                title: '',
                text: error,
                footer: ''
            });
        }
    });
}

function edit(Id) {
    $.ajax({
        type: 'Get',
        url: '/PersonaFisica/Details',
        dataType: 'json',
        data: { id: Id},
        beforeSend: function () {

        },
        success: function (data) {
            if (data.isSuccess) {
                cleanInputs();
                let fecha = data.r.fechaNacimiento.split("T")
                $('#Nombre').val(data.r.nombre);
                $('#ApellidoPaterno').val(data.r.apellidoPaterno);
                $('#ApellidoMaterno').val(data.r.apellidoMaterno);
                $('#RFC').val(data.r.rfc);
                $('#FechaNacimiento').val(fecha[0]);
                $('#IdPersonaFisica').val(data.r.idPersonaFisica);
                $('#btnSave').hide();
                $('#btnUpdate').show();
                $('#modalCreateEdit').modal('show');
            } else {
                console.log(data.message);
                Swal.fire({
                    type: 'error',
                    html: '<span>' + data.message + '</span>',
                    title: 'Error',
                    footer: ''
                });
            }
        },
        complete: function () {

        },
        error: function (xhr, status, error) {

            Swal.fire({
                type: 'error',
                title: '',
                text: error,
                footer: ''
            });
        }
    });
}

function update() {
    var form = $("#frmPersonaFisica");
    validator = form.validate();
    if (!form.valid())
        return;

    $.ajax({
        type: 'POST',
        url: '/PersonaFisica/Update',
        dataType: 'json',
        data: $("#frmPersonaFisica").serialize(),
        beforeSend: function () {

        },
        success: function (data) {
            if (data.isSuccess) {
                Swal.fire({
                    type: 'success',
                    title: 'Success',
                    text: data.message,
                    footer: '',
                    onAfterClose: () => {

                        location.href = '/PersonaFisica/Index';
                        $('#modalCreateEdit').modal('hide');
                    }
                });
            } else {
                Swal.fire({
                    type: 'error',
                    html: '<span>' + data.message + '</span>',
                    title: 'Error',
                    footer: ''
                });
            }
        },
        complete: function () {

        },
        error: function (xhr, status, error) {

            Swal.fire({
                type: 'error',
                title: '',
                text: error,
                footer: ''
            });
        }
    });
}


function deleteRecord(Id) {


    $.ajax({
        type: 'POST',
        url: '/PersonaFisica/Delete',
        dataType: 'json',
        data: { id: Id, __RequestVerificationToken: $('#deleteRecord input[name=__RequestVerificationToken]').val() },
        beforeSend: function () {

        },
        success: function (data) {
            if (data.isSuccess) {
                Swal.fire({
                    type: 'success',
                    title: 'Success',
                    text: data.message,
                    footer: '',
                    onAfterClose: () => {

                        location.href = '/PersonaFisica/Index';
                        $('#modalCreateEdit').modal('hide');
                    }
                });
            } else {
                Swal.fire({
                    type: 'error',
                    html: '<span>' + data.message + '</span>',
                    title: 'Error',
                    footer: ''
                });
            }
        },
        complete: function () {

        },
        error: function (xhr, status, error) {

            Swal.fire({
                type: 'error',
                title: '',
                text: error,
                footer: ''
            });
        }
    });
}
