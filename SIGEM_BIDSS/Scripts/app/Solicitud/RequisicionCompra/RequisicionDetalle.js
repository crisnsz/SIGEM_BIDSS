var contador = 0;
var prodtable = $("#tblBusquedaGenerica").DataTable({
    "searching": true,
    "lengthChange": true,
    "language": {
        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
    }
})
$(document).ready(function () {
    $("#Reqde_Justificacion")[0].maxLength = 50;
    $('.select2').select2();
});
function GetDetalle() {
    var CompraDetalle = {
        prod_Id: $('#prod_Id').val(),
        Reqde_Cantidad: $('#Cantidad').val(),
        Reqde_Justificacion: $('#Reqde_Justificacion').val(),
        prod_Text: document.getElementById('prod_Text').value,
    }
    return CompraDetalle;
};
document.getElementById("RequisicionDetalle").addEventListener("submit", function (e) {
    var _length = document.getElementById("dataTable").rows.length;
    console.log(_length);
    if (_length <= 0) {
        e.preventDefault();
    }
});

//document.getElementById("seleccionar").onclick = function () {

//}



$(document).on("click", "#tblBusquedaGenerica tbody tr td button#seleccionar", function () {
    var prod_Text = $(this).closest('tr').find('td:eq(0)').text()
    var prod_Id = this.value;
    document.getElementById("prod_Id").value = prod_Id
    document.getElementById("prod_Text").value = prod_Text.trimStart().trimEnd()
    console.log(prod_Id)
});


document.getElementById("Cantidad").onblur = function () {
    if (this.value != "") {
        //number-format the user input
        this.value = parseFloat(this.value.replace(/,/g, ""))
            .toFixed(2)
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");

        //set the numeric value to a number input
        document.getElementById("number").value = this.value.replace(/,/g, "")
    }
}



//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $(".nospace", 'form').each(function (e) {
        var _id = $(this).attr("id");
        var el = document.getElementById('' + _id + '')
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();
    });
    $(".normalize", 'form').each(function (e) {
        if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
        }
    });
});
const Add = document.getElementById('')



$('#addTable').click(function () {
    var table = $('#dataTable').DataTable();

    var CompraDetalle = GetDetalle();
    console.log(CompraDetalle)
    var prod_Text = $('#prod_Text').val();
    var Reqde_Cantidad = $('#Cantidad').val();
    var Reqde_Justificacion = $('#Reqde_Justificacion').val();
    _prod_Text = true;
    _Reqde_Cantidad = true;
    _Reqde_Justificacion = true;

    //------------------------------Validaciones Producto------------------------------

    if (CompraDetalle.prod_Text == '') {
        $('#prod_TextCodigoError').text('');
        $('#Validationprod_TextCreate').after('<span id="prod_TextCodigoError" class="validation-summary-errors field-validation-error text-danger"><span class="fa fa-ban text-danger"></span> Campo Producto Requerido</span>');
        _prod_Text = false;
    }
    else {
        $('#prod_TextCodigoError').text('');
    }
    if (CompraDetalle.Reqde_Cantidad == '' || Reqde_Cantidad <= 0.00) {
        $('#CantidadCodigoError').text('');
        $('#ValidationCantidadCreate').after('<span id="CantidadCodigoError" class="validation-summary-errors field-validation-error text-danger"><span class="fa fa-ban text-danger"></span> Campo Cantidad Requerido</span>');
        _Reqde_Cantidad = false;
    } else {
        $('#CantidadCodigoError').text('');
    }
    if (CompraDetalle.Reqde_Justificacion == '') {
        $('#JustificacionCodigoError').text('');
        $('#ValidationJustificacionCreate').after('<span id="JustificacionCodigoError" class="validation-summary-errors field-validation-error text-danger"><span class="fa fa-ban text-danger"></span>  Campo Justificacion Requerido.</span>');
        _Reqde_Justificacion = false;
    } else {
        $('#JustificacionCodigoError').text('');
    }
    //------------------------------Fin Validaciones Codigo Departamento------------------------------
    if (!_prod_Text || !_Reqde_Cantidad || !_Reqde_Justificacion) {
        return false
    }
    else {
        console.log('Before Ajax/ ')
        console.log(CompraDetalle)
        $.ajax({
            url: "/RequisionCompraDetalle/SaveCompraDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbRequisionCompraDetalle: CompraDetalle }),
        })
            .done(function (data) {
                console.log(data)
                if (CompraDetalle.tpv_Id == data) {

                    $('#dataTable td').each(function () {
                        var constId = $(this).text();

                        if (CompraDetalle.prod_Id == constId) {
                            var q = table.row($(this).parents('tr')).remove().draw()
                            var t = $(this).closest('tr').find('td:eq(1)').text()
                            var suma = parseInt(CompraDetalle.Reqde_Cantidad) + parseInt(t);
                            table.row.add([
                                CompraDetalle.prod_Text,
                                suma,
                                CompraDetalle.Reqde_Justificacion,
                                '<button id = "remove" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'
                            ]).draw(false)
                        }
                    });
                }
                else {

                    contador = contador + 1
                    table.row.add([
                        CompraDetalle.prod_Text,
                        CompraDetalle.Reqde_Cantidad,
                        CompraDetalle.Reqde_Justificacion,
                        '<button id = "remove" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'
                    ]).draw(false)

                }

                document.getElementById('prod_Id').value = '';
                document.getElementById('prod_Text').value = '';
                document.getElementById('Cantidad').value = '';
                document.getElementById('Reqde_Justificacion').value = '';
            });
        console.log('After Ajax/ ')
        console.log(CompraDetalle)

    }
});



//--------Obtiene los valores de los textbox y los Elimina a la tabla(con Datatables)
function EliminarProductos(_table) {
    var table = $('#tblMunicipio').DataTable();
    _mun_codigo = $(_table).closest("tr").find("td:eq(0)").text();

    table
        .row($(_table).parents('tr'))
        .remove()
        .draw();
    _tableLegth = table.column(0).data().length
    Municipios = {
        mun_codigo: _mun_codigo,
    };
    return { _tableLegth: _tableLegth, Municipios: Municipios }
}

//-------Elimina los datos a la variable de sesion en el Controlador
$(document).on("click", "#dataTable tbody tr td button#remove", function () {
    _values = EliminarProductos(this)

    $.ajax({
        url: "/RequisionCompraDetalle/Remove",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Municipios: _values.Municipios }),
    }).done(function (data) {
        if (_values._tableLegth <= 0) {

            $('#dep_Codigo').prop('disabled', false);
            $('#dep_Nombre').prop('disabled', false);
            $('#Valida').prop('disabled', true);
        }
        console.log(data)
    });
});







// #myInput is a <input type="text"> element
$('#prodSearch').on('keyup', function () {
    prodtable.search(this.value).draw();
});


////SOLO NUMEROS 
function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9]+$/.test(tecla);
}
