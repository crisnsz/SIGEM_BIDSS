var prodtable = $('#tblBusquedaGenerica').DataTable();




var prodtable = $("#tblBusquedaGenerica").DataTable({
    "searching": true,
    "lengthChange": true,
    "language": {
        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
    }
})
$(document).on("click", "#tblBusquedaGenerica tbody tr td button#seleccionar", function () {
    var emp_Text = $(this).closest('tr').find('td:eq(0)').text()
    var emp_Id = this.value;
    document.getElementById("emp_Id").value = emp_Id
    document.getElementById("emp_Text").value = emp_Text.trimStart().trimEnd()
});



$('#empSearch').on('keyup', function () {
    prodtable.search(this.value).draw();
});