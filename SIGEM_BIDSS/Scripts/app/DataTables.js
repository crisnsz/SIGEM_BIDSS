$(document).ready(function () {
    $('#dataTable').DataTable({
        "searching": true,
        "lengthChange": true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        }
    });


    $('#tblBusquedaGenerica').DataTable();
});

