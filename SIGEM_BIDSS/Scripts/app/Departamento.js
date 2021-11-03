var contador = 0;


//-----------Bloquear el Drop de texto en rlos textbox
$(window).ready(function () {

    var vdep_Codigo = document.getElementById('dep_Codigo');
    vdep_Codigo.ondrop = function (e) {
        e.preventDefault();
        //alert("esta acción está prohibida");
    }


    var vdep_Nombre = document.getElementById('dep_Nombre');
    vdep_Nombre.ondrop = function (e) {
        e.preventDefault();
        //alert("esta acción está prohibida");
    }
    //---------------------------------------------------------------------------------

    var vmun_codigo = document.getElementById('mun_codigo');
    vmun_codigo.ondrop = function (e) {
        e.preventDefault();
        //alert("esta acción está prohibida");
    }

    var vmun_nombre = document.getElementById('mun_nombre');
    vmun_nombre.ondrop = function (e) {
        e.preventDefault();
        //alert("esta acción está prohibida");
    }
})


//-----------Bloquear Ctrl + C y Ctrl + V



//-----------Aceptar solo numeros en los textbox
function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9]+$/.test(tecla);
}

function noespaciosincio(e) {
    var valor = e.value.replace(/^ */, '');
    e.value = valor;
}

///----------validar solo letras
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " -+'/áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}


//---------Normalizar los textos
var normalize = (function () {
    var from = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÑñÇç",
        to = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuunncc",
        mapping = {};

    for (var i = 0, j = from.length; i < j; i++)
        mapping[from.charAt(i)] = to.charAt(i);

    return function (str) {
        var ret = [];
        for (var i = 0, j = str.length; i < j; i++) {
            var c = str.charAt(i);
            if (mapping.hasOwnProperty(str.charAt(i)))
                ret.push(mapping[c]);
            else
                ret.push(c);
        }
        return ret.join('');
    }

})();


//var valTipoSalida = normalize(lblTipoSalida.toUpperCase());



//-------Obtener los values de los textbox Codigo y Nonbre
function GetMunicipio() {
    _muncodigo = $('#dep_Codigo').val() + $('#mun_codigo').val()
    var Municipio = {
        mun_Codigo: _muncodigo.trim(),
        mun_Nombre: $('#mun_nombre').val(),
    };
    return Municipio;
}




//--------Obtiene los valores de los textbox y los agrega a la tabla(con Datatables)
function AgregarMunicipiosDT() {
    var dep_Codigo = $('#dep_Codigo').val();
    var dep_Nombre = $('#dep_Nombre').val();
    var MunCodigo = $('#mun_codigo').val();
    var MunNombre = $('#mun_nombre').val();
    var table = $('#tblMunicipio').DataTable();
    _depCodigo = true;
    _depLength = true;
    _MunCodigo = true;
    _munLength = true;
    _munNombreLength = true;
    _MunNombre = true;
    _MunNombreSP = true;
    _DepNombre = true;


    //------------------------------Validaciones Departamento------------------------------
    //---------------------------------------------------------------------------------------
    //------------------------------Validaciones Codigo Departamento------------------------------

    if (dep_Codigo == '') {
        $('#dep_CodigoCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Codigo Departamento Requerido</ul>');
        _depCodigo = false;
    }
    else {
        $('#dep_CodigoCodigoError').text('');
    }

    if (dep_Codigo.length > 2) {
        $('#dep_CodigoLegthCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoLegthCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se aceptan mas de 2 números.</ul>');
        _depLength = false;
    } else {
        $('#dep_CodigoLegthCodigoError').text('');
    }
    if (_depCodigo && dep_Codigo.length < 2) {
        $('#dep_CodigoLegthLessCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoLegthLessCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se aceptan menos de 2 números.</ul>');
        _depLength = false;
    } else {
        $('#dep_CodigoLegthLessCodigoError').text('');
    }
    //------------------------------Fin Validaciones Codigo Departamento------------------------------

    if (dep_Nombre == '') {
        $('#DepNombreNombreError').text('');
        $('#ValidationDepNombreCreate').after('<ul id="DepNombreNombreError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Departamento Requerido</ul>');
        _DepNombre = false;
    } else {
        $('#DepNombreNombreError').text('');

    }
    //------------------------------Fin Validaciones Departamento------------------------------
  
    //------------------------------Validaciones Municipio------------------------------
    //---------------------------------------------------------------------------------------
    //------------------------------Validaciones Codigo Municipio------------------------------

    if (MunCodigo == '') {
        $('#MunCodigoCodigoError').text('');
        $('#ValidationMunCodigoCreate').after('<ul id="MunCodigoCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Codigo Municipio Requerido</ul>');
        _MunCodigo = false;
    } else {
        $('#MunCodigoCodigoError').text('');
    }

    if (MunCodigo.length > 2) {
        $('#MunCodigoLegthMaxCodigoError').text('');
        $('#ValidationMunCodigoCreate').after('<ul id="MunCodigoLegthMaxCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se aceptan mas de 2 números.</ul>');
        _munLength = false;
    } else {
        $('#MunCodigoLegthCodigoError').text('');

    }
    if (_MunCodigo && MunCodigo.length < 2) {
        $('#MunCodigoLegthLessCodigoError').text('');
        $('#ValidationMunCodigoCreate').after('<ul id="MunCodigoLegthLessCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se aceptan menos de 2 números.</ul>');
        _munLength = false;
    } else {
        $('#MunCodigoLegthLessCodigoError').text('');

    }
    //------------------------------Fin Validaciones Codigo Municipio------------------------------


    if (MunNombre == '') {
        $('#MunNombreNombreError').text('');
        $('#ValidationMunNombreCreate').after('<ul id="MunNombreNombreError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Municipio Requerido</ul>');
        _MunNombre = false;
    } else {
        $('#MunNombreNombreError').text('');

    }

    if (MunNombre.substring(0, 1) == " ") {
        $('#MunNombreSNNombreError').text('');
        $('#ValidationMunNombreCreate').after('<ul id="MunNombreSNNombreError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se Aceptan Espacios en blanco.</ul>');
        _munNombreLength = false;
    } else {
        $('#MunNombreSNNombreError').text('');
    }
    //------------------------------Fin Validaciones Municipio------------------------------


    //-----------------------------Validaciones de Municipio Existente------------------------------
    if ($('#tblMunicipio >tbody >tr').length > 0) {
        $('#tblMunicipio >tbody >tr').each(function () {
            _stringCodigoTable = $(this).find("td:eq(0)").text()

            _MunCodigoTabla = _stringCodigoTable.substring(2, 4)

            if (_MunCodigo && MunCodigo == _MunCodigoTabla) {
                $('#MunCodigoExistCodigoError').text('');
                $('#ValidationMunCodigoCreate').after('<ul id="MunCodigoExistCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Ya existe el código de municipio</ul>');
                _MunCodigo = false;
            } else if (_MunCodigo) {
                $('#MunCodigoExistCodigoError').text('');
            }

            _MunNombreTabla = $(this).find("td:eq(1)").text()
            if (_MunNombre && MunNombre == _MunNombreTabla) {
                $('#MunNombreExistCodigoError').text('');
                $('#ValidationMunNombreCreate').after('<ul id="MunNombreExistCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Ya existe el nombre del municipio</ul>');
                _MunNombre = false;
            } else if (_MunNombre) {
                $('#MunNombreExistCodigoError').text('');
            }
        })
    }

        $('#dep_Codigo').prop('readonly', true);
        $('#dep_Nombre').prop('readonly', true);
        $('#Valida').prop('disabled', false);

        var tbMunicipio = GetMunicipio();

        contador = contador + 1;
        table.row.add([
            tbMunicipio.mun_Codigo,
            tbMunicipio.mun_Nombre.toUpperCase().trim(),
            '<button id = "removeMunicipios" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>',
        ]).draw(false);
        _ValuesTrue = { _Bool : true, _tbMunicipio : tbMunicipio }
        return _ValuesTrue
}




//--------Obtiene los valores de los textbox y los Elimina a la tabla(con Datatables)
function EliminarMunicipiosDT(_table) {
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



//-------Agrega los datos a la variable de sesion en el Controlador
$('#AgregarMunicipio').click(function () {
    _add = AgregarMunicipiosDT()
    if (_add._Bool) {
        console.log("True")
        $.ajax({
            url: "/Departamento/AnadirMunicipio",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Municipio: _add._tbMunicipio }),
        })
            .done(function (data) {
                $('#mun_codigo').val('');
                $('#mun_nombre').val('');
                $('#MessageError').text('');
                $('#NombreError').text('');
                $('#CodigoError').text('');
            });
    } else {
        console.log("False")
    }
        
});



//-------Elimina los datos a la variable de sesion en el Controlador
$(document).on("click", "#tblMunicipio tbody tr td button#removeMunicipios", function () {
    _values = EliminarMunicipiosDT(this)
   
    $.ajax({
        url: "/Departamento/RemoveMunicipios",
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





///////GuardarMunicipio
$('#btnNuevoEdit').click(function () {
    var depCodigo = $('#dep_Codigo').val();
    $.ajax({
        url: "/Departamento/GuardarMuninicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ depCodigo: depCodigo }),
    })
        .done(function (data) {
            if (data == '-1') {
                $('#ValidationCreate').text('');
                $('#ValidationCreate_').after('<ul id="ValidationCreate" class="validation-summary-errors text-danger">No se pudo guardar el registro, contacte al administrador</ul>');
            }
            else {
                window.location.href = '/Departamento/Edit/' + depCodigo;
            }
        })
});






/////////Abrir modal y obtener los valores para los textbox
function EditMunicipioRecord(mun_Codigo) {
    $.ajax({
        url: "/Departamento/getMunicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ munCodigo: mun_Codigo }),

    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#mun_CodigoEdit_").val(item.mun_Codigo);
                    $("#mun_NombreEdit_").val(item.mun_Nombre);
                    $("#EditMunicipioModal").modal();
                })
            }
        })
}


//------------------Actualizar Municipio(Modal)     Guardar los cambios del modal de Editar
$("#BtnsubmitMunicipio").click(function () {
    var depCodigo = $('#dep_Codigo').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Departamento/ActualizarMunicipio",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                window.location.href = '/Departamento/Edit/' + depCodigo;
        }
    });
})




//-----Cargar datatable
$(document).ready(function () {
    $('#tblMunicipio').DataTable(
        {
            "searching": true,
            "lengthChange": true,
            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            }
        });
});


$(document).ready(function () {
    $('#tblMunicipios').DataTable(
        {
            "searching": true,
            "lengthChange": true,
            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            }
        });
});

$('#dep_Codigo').keyup(function () {
    $('#ValidationDepCodigoCreate').hide();
});

$('#dep_CodigoCodigoError').keyup(function () {
    $('#ValidationDepCodigoCreate').hide();
});

$('#dep_CodigoLegthCodigoError').keyup(function () {
    $('#ValidationDepCodigoCreate').hide();
});

$('#dep_CodigoLegthLessCodigoError').keyup(function () {
    $('#ValidationDepCodigoCreate').hide();
});

$('#mun_CodigoEdit_').keyup(function () {
    $('#MunCodigoCodigoError').hide();
});





$('#dep_Nombre').keyup(function () {
    $('#ValidationDepNombreCreate').hide();
    
});

$('#mun_codigo').keyup(function () {
    $('#ValidationMunCodigoCreate').hide();
});

$('#mun_nombre').keyup(function () {
    $('#ValidationMunNombreCreate').hide();
});

$('#mun_CodigoEdit_').keyup(function () {
    $('#mun_CodigoEdit_').hide();
});

$('#mun_nombre').keyup(function () {
    $('#mun_NombreEdit_').hide();
});

$('#dep_Codigo').keyup(function () {
    $('#ValidationDepCodigoCreate').hide();
});

$('#dep_Nombre').keyup(function () {
    $('#ValidationDepNombreCreate').hide();
});


$('#mun_codigo').keyup(function () {
    $('#ValidationMunCodigoCreate').hide();
});

$('#mun_nombre').keyup(function () {
    $('#ValidationMunNombreCreate').hide();
});


$('#mun_NombreEdit_').keyup(function () {
    $('#DescripcionErrorEdit').hide();
});


$('#mun_NombreEdit_').blur(function () {
    if ($.trim($('#mun_NombreEdit_').val()) == 0) {
        $('#DescripcionErrorEdit').text('');
        $('#DescripcionErrorEdit').after('<ul id="DescripcionErrorEdit" class="validation-summary-errors text-danger">Campo Descripcion Requerido</ul>');
    }
});
