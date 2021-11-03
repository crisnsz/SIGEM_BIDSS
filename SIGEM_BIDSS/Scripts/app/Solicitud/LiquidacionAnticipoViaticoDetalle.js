

var contador = 0;

$("#CargarArchivo").change(function () {
    readURL(this);
});



$('#AgregarDetalle').click(function () {
    var table = $('#dataTable').DataTable();
    var LiquidacionAnticipoViatico = GetLiquidacionViatico();
  

    $.ajax({
        url: "/LiquidacionAnticipoViaticoDetalle/SaveLiquidacionAnticipoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ tbLiquidacionAnticipoViaticoDetalle: LiquidacionAnticipoViatico, Archivo: LiquidacionAnticipoViatico.Lianvide_Archivo }),
    })

        .done(function (data) {
            console.log(contador + 'hid');

            if (LiquidacionAnticipoViatico.tpv_Id == data) {
                $('#dataTable td').each(function () {
                    var constId = $(this).text();

                    if (LiquidacionAnticipoViatico.tpv_IdText == constId) {
                        var q = table.row($(this).parents('tr')).remove().draw()
                        var t = $(this).closest('tr').find('td:eq(3)').text()
                        var suma = parseInt(LiquidacionAnticipoViatico.Lianvide_MontoGasto) + parseInt(t);
                        table.row.add([
                            LiquidacionAnticipoViatico.Lianvide_FechaGasto,
                            LiquidacionAnticipoViatico.tpv_IdText,
                            LiquidacionAnticipoViatico.Lianvide_Concepto,
                            suma,
                            '<button id = "RemoveDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'



                        ]).draw(false)
                    }
                });
            }

            else {


                contador = contador + 1;
                console.log(data);
                table.row.add([

                    LiquidacionAnticipoViatico.Lianvide_FechaGasto,
                    LiquidacionAnticipoViatico.tpv_IdText,
                    LiquidacionAnticipoViatico.Lianvide_Concepto,
                    LiquidacionAnticipoViatico.Lianvide_MontoGasto,

                    '<button id = "RemoveDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'


                ]).draw(false)
            
            }
            
        });

});


function GetLiquidacionViatico() {

    var R = document.getElementById('tpv_Id')
    var LIQUIDACIONDETALLE = {


        Lianvide_Id: contador,
        Lianvi_Id: $('#Lianvi_Id').val(),
        tpv_Id: $('#tpv_Id').val(),
        tpv_IdText: R.options[R.selectedIndex].text,
        Lianvide_FechaGasto: $('#Lianvide_FechaGasto').val(),
        Lianvide_MontoGasto: $('#Lianvide_MontoGasto').val(),
        Lianvide_Concepto: $('#Lianvide_Concepto').val(),
        Lianvide_Archivo: $('#LianvideArchivo').val(),
    };
    return LIQUIDACIONDETALLE;
}



//////DatePicker/////////////////////

$('#Lianvide_FechaGasto').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"


});



////Remover detalle///
$(document).on("click", "#dataTable tbody tr td button#RemoveDetalle", function () {
    idItem = $(this).closest('tr').data('id');
    var vIdDetalle = $(this).closest("tr").find("td:eq(0)").text();
    var tbLquidacionDetalle = {
        Lianvi_Id: vIdDetalle,
        Lianvide_UsuarioCrea: vIdDetalle
    };
    var table = $('#dataTable').DataTable();
    table
        .row($(this).parents('tr'))
        .remove()
        .draw();

    $.ajax({
        url: "/LiquidacionAnticipoViaticoDetalle/RemoveDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ LiquidacionDetalle: tbLquidacionDetalle }),
        success: function (data) {
            contador = contador - 1

        }
    });
});
