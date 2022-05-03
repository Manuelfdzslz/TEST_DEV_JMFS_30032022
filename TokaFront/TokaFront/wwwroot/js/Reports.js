
$(document).ready(function () {
     
    var filters = {};

    var table = $('#datatable').DataTable({
        processing: true,
        serverSide: true,
        ajax: {
            method: "POST",
            url: '/Reports/GetTableData',
            data: { filters: filters },
            dataType: "JSON",
            beforeSend: function () {
               
            },
            complete: function () {
              
            },
            dataSrc: function (data) {
                var d = data.data;
                console.log(data);
                return d;
            },
        },
        columns: [
            { data: "idCliente" },
            { data: "nombre" },
            { data: "paterno" },
            { data: "materno" },
            { data: "idViaje" },
            { data: "rfc" },
            { data: "sucursal" },
            { data: "razonSocial" },
            { data: "fechaRegistroEmpresa" }

        ],
        dom: 'Bfrtip',
        pageLength: 20,
        buttons: [],
        order: [2, "desc"],
        responsive: false,
        scrollX: false,
        searching: false,
    });
    
});

function CreateReport() {

    location.href ="Reports/CreateReport"
}
