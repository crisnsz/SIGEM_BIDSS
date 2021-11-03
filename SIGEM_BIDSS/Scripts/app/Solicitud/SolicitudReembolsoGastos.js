///Date picket
$('#Reemga_FechaViaje').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1900",
    language: "es",
 
}.datepicker("setDate", new Date()));


////MAX LENGTH DE LOS CAMPOS

$(document).ready(function () {
    $("#Reemga_Cliente")[0].maxLength = 100;
    $("#Reemga_PropositoVisita")[0].maxLength = 100;
    $("#Reemga_Comentario")[0].maxLength = 250;

})


///Conjunto para usar departamento y municipios 
$(document).ready(function () {


    var CodDepartamento = $('#dep_codigo').val();
    var muncod = $('#munCodigo').val();
    if (CodDepartamento !== "") {
        $.ajax({
            url: "/SolicitudReembolsoGastos/GetMunicipios",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodDepartamento: CodDepartamento })
        })
            .done(function (data) {
                if (data.length > 0) {
                    $('#mun_codigo').empty();
                    $('#mun_codigo').append("<option value=''>Seleccione Municipio</option>");
                    $.each(data, function (key, val) {
                        if (muncod !== "") {
                            if (val.mun_codigo === muncod) {
                                $('#mun_codigo').append("<option selected='selected' value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                            }
                            else
                                $('#mun_codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                        }
                        else
                            $('#mun_codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                    });
                    $('#mun_codigo').trigger("chosen:updated");
                }
                else {
                    $('#mun_codigo').empty();
                    $('#mun_codigo').append("<option value=''>Seleccione Municipio</option>");
                }
            });
    }
});


$(document).on("change", "#dep_codigo", function () {
    GetMunicipios();
});



function GetMunicipios() {
    var CodDepartamento = $('#dep_codigo').val();
    if (CodDepartamento !== "") {
        $.ajax({
            url: "/SolicitudReembolsoGastos/GetMunicipios",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodDepartamento: CodDepartamento })
        })
            .done(function (data) {
                if (data.length > 0) {
                    $('#mun_codigo').empty();
                    $('#mun_codigo').append("<option value=''>Seleccione Municipio</option>");
                    $.each(data, function (key, val) {
                        $('#mun_codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                    });



                    $('#mun_codigo').trigger("chosen:updated");
                }
                else {
                    $('#mun_codigo').empty();
                    $('#mun_codigo').append("<option value=''>Seleccione Municipio</option>");
                }
            });
    }
    else {
        $('#mun_codigo').empty();
        $('#mun_codigo').append("<option value=''>Seleccione Municipio</option>");
    }
}

//Fecha Solicitud
$('#Reemga_GralFechaSolicitud').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1900",
    language: "es",
    daysOfWeekDisabled: "0",
}).datepicker("setDate", new Date());;

//campos vacios con mensaje
document.getElementById("Guardar").addEventListener("click", function () {
    //
    var v_Reemga_JefeInmediato = document.getElementById('Reemga_JefeInmediato').value;
    var b_Reemga_JefeInmediato = true;
    //
    var v_Reemga_Cliente = document.getElementById('Reemga_Cliente').value;
    var b_Reemga_Cliente = true;
    //
    var v_Reemga_PropositoVisita = document.getElementById('Reemga_PropositoVisita').value;
    var b_Reemga_PropositoVisita = true;
    //
    var v_Reemga_UsuarioCrea = document.getElementById('Reemga_UsuarioCrea').value;
    var b_Reemga_UsuarioCrea = true;
    //
    var v_mun_codigo = document.getElementById('mun_codigo').value;
    var b_mun_codigo = true;



    if (v_Ansal_JefeInmediato === "") {
        document.getElementById('spanJefeInmediato').innerHTML = "Campo Requerido"
        b_Ansal_JefeInmediato = false;
    }
    if (v_Cantidad === "") {



        document.getElementById('spanCantidad').innerText = "Campo Requerido"
        b_Cantidad = false;
    }
    if (v_tpsal_id === "") {
        document.getElementById('spantpsal_id').innerText = "Campo Requerido"
        b_tpsal_id = false;
    }
    if (v_Ansal_Justificacion === "") {
        document.getElementById('spanJustificacion').innerText = "Campo Requerido"
        b_Ansal_Justificacion = false;
    }
    if (b_Ansal_JefeInmediato && b_Cantidad && b_tpsal_id && b_Ansal_Justificacion) {
        $('#ModalAuth').modal();
    }
})








///Solo números
function soloNumeros(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
    return /^[0-9]+$/.test(tecla);
}
//$('#AgregarDetalle').click(function () {
//    console.log('boton');
//    var table = $('#dataTable').DataTable();
//    var LianvideArchivo = $('#Lianvide_Archivo').val();
//    var tpv_Id = $('#tpv_Id').val();
//    var Lianvide_FechaGasto = $('#Lianvide_FechaGasto').val();
//    var Lianvide_MontoGasto = $('#Lianvide_MontoGasto').val();

//    var Lianvide_Concepto = $('#Lianvide_Concepto').val();


//    if (Lianvide_FechaGasto == '') {
//        $('#MessageError').text('');
//        $('#Error_Producto').text('');
//        $('#Error_PuntoReorden').text('');
//        $('#Error_CantidadMinima').text('');
//        $('#Error_CantidadMaxima').text('');
//        $('#Error_Costo').text('');
//        $('#Error_CostoPromedioo').text('');
//        $('#ErrorProducto_Create').after('<ul id="Error_Producto" class="validation-summary-errors text-danger">*Codigo De Barra Requerido</ul>');

//    }
//    else if (tpv_Id == '') {

//        $('#MessageError').text('');
//        $('#Error_Producto').text('');
//        $('#Error_PuntoReorden').text('');
//        $('#Error_CantidadMinima').text('');
//        $('#Error_CantidadMaxima').text('');
//        $('#Error_Costo').text('');
//        $('#Error_CostoPromedioo').text('');
//        $('#Error_Barras').text('');
//        $('#ErrorBarras_Create').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Codigo De Barras Requerido</ul>');
//    }
//    else if (Lianvide_Concepto == '') {

//        $('#MessageError').text('');
//        $('#Error_Producto').text('');
//        $('#Error_PuntoReorden').text('');
//        $('#Error_CantidadMinima').text('');
//        $('#Error_CantidadMaxima').text('');
//        $('#Error_Costo').text('');
//        $('#Error_CostoPromedioo').text('');
//        $('#ErrorCantidadMinima_Create').after('<ul id="Error_CantidadMinima" class="validation-summary-errors text-danger">*Cantidad Miníma Requerido</ul>');
//    }

//    else if (LianvideArchivo == '') {
//        $('#MessageError').text('');
//        $('#Error_Producto').text('');
//        $('#Error_PuntoReorden').text('');
//        $('#Error_CantidadMinima').text('');
//        $('#Error_CantidadMaxima').text('');
//        $('#Error_Costo').text('');
//        $('#Error_CostoPromedioo').text('');
//        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">*Campo Punto Reorden Requerido</ul>');
//    }
//    else if (Lianvide_MontoGasto == '') {
//        $('#MessageError').text('');
//        $('#Error_Producto').text('');
//        $('#Error_PuntoReorden').text('');
//        $('#Error_CantidadMinima').text('');
//        $('#Error_CantidadMaxima').text('');
//        $('#Error_Costo').text('');
//        $('#Error_CostoPromedioo').text('');
//        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">*Campo Punto Reorden Requerido</ul>');
//    }

//    else {

//        var tbLiquidacionAnticipoViaticoDetalle = GetLiquidacionViatico();
//        $.ajax({
//            url: "/LiquidacionAnticipoViaticoDetalle/SaveLiquidacionAnticipoDetalle",
//            method: "POST",
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ LIQUIDACIONDETALLE: tbLiquidacionAnticipoViaticoDetalle }),
//        }).done(function (data) {

//            if (data == tpv_Id) {
//                $("#tbLiquidacionViatico td").each(function () {
//                    var prueba = $(this).text()
//                    if (prueba == tpv_Id) {

//                        table.row($(this).parents('tr')).remove().draw();

//                        table.row.add([
//                            $('#Lianvide_FechaGasto').val(),
//                            $('#tpv_Id').val(),
//                            $('#Lianvide_Concepto').val(),
//                            $('#Archivo_Name').text(),
//                            $('#Lianvide_MontoGasto').val(),

//                            '<button id = "RemoveDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>',


//                        ]).draw(false);
//                    }
//                });

//            }
//            else {
//                table.row.add([
//                    $('#Lianvide_FechaGasto').val(),
//                    $('#tpv_Id').val(),
//                    $('#Lianvide_Concepto').val(),
//                    $('#Archivo_Name').text(),
//                    $('#Lianvide_MontoGasto').val(),
//                    '<button id = "RemoveDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>',

//                ]).draw(false);
//            }

//        }).done(function (data) {
//            $('#Lianvide_FechaGasto').val(''),
//                $('#tpv_Id').val(''),
//                $('#Lianvide_Concepto').val(''),
//                $('#Archivo_Name').text(''),
//                $('#Lianvide_MontoGasto').val(''),

//                $('#MessageError').text('');
//            $('#Error_Producto').text('');
//            $('#Error_Barras').text('');
//            $('#Error_PuntoReorden').text('');
//            $('#Error_CantidadMinima').text('');
//            $('#Error_CantidadMaxima').text('');
//            $('#Error_Costo').text('');
//            $('#Error_CostoPromedioo').text('');
//        });
//    }
//});