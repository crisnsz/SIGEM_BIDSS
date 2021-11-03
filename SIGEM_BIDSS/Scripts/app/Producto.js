var pscat_Id;
var pcat_Id;

function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}

function noespaciosincio(e) {
    var valor = e.value.replace(/^ */, '');
    e.value = valor;
}

function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9]+$/.test(tecla);
}

//////////////////////////////////////////////////////////////////////////////
$('#btnGuardar').click(function () {
    $("#ErrorValidacionGeneral").remove();
    $("#errorActividad").remove();
    $("#errorActividada").remove();
    $("#errorcategoria").remove();
    $("#errorproveedor").remove();
    var unidad = $("#uni_Id").find('option:selected').val();
    var subcategoria = $("#pscat_Id").find('option:selected').val();
    var categoria = $("#pcat_Id").find('option:selected').val();
    var proveedor = $("#prov_Id").find('option:selected').val();
    if (unidad == '') {
        $('#unidad').text('');
        $('#errorActividad').text('');
        $('#validationUnidad').after('<ul id="errorActividad" class="validation-summary-errors text-danger">Campo Unidad de Medida Requerido</ul>');
    }
    if (subcategoria == '') {
        $('#subcategoria').text('');
        $('#errorActividada').text('');
        $('#validationSubCategoria').after('<ul id="errorActividada" class="validation-summary-errors text-danger">Campo SubCategoria Requerido</ul>');
    }
    if (categoria == '') {
        $('#categoria').text('');
        $('#errorcategoria').text('');
        $('#validationCategoria').after('<ul id="errorcategoria" class="validation-summary-errors text-danger">Campo Categoria Requerido</ul>');
    }
    if (proveedor == '') {
        $('#proveedor').text('');
        $('#errorproveedor').text('');
        $('#validationproveedor').after('<ul id="errorproveedor" class="validation-summary-errors text-danger">Campo Proveedor Requerido</ul>');
    }
});
/////////////////////////////////////////////////////////////
//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $("input[type='text']", 'form').each(function () {
        _id = $(this).attr("id");
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();
    });

    $("textarea").each(function () {
        _id = $(this).attr("id");
        console.log("Hi:", _id);
    });

});
//////////////////////////////////////////////////////////
$(document).ready(function () {
  
    var idcate = $('#pcat_Id').val();
   var idsubcate = $('#idsubcategoria').val();
    if (idcate !== "") {
        $.ajax({
            url: "/Producto/GetSubCategoriaProducto",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ idcate: idcate })
        })
            .done(function (data) {
                if (data.length > 0) {
                    $('#pscat_Id').empty();
                    $('#pscat_Id').append("<option value=''>Seleccione subcategoría</option>");
                    $.each(data, function (key, val) {
                        if (muncod !== "") {
                            if (val.idsubcate === idsubcate) {
                                $('#pscat_Id').append("<option selected='selected' value=" + val.pscat_Id + ">" + val.pscat_Descripcion + "</option>");
                            }
                            else
                                $('#pscat_Id').append("<option value=" + val.pscat_Id + ">" + val.pscat_Descripcion + "</option>");
                        }
                        else
                            $('#pscat_Id').append("<option value=" + val.pscat_Id + ">" + val.pscat_Descripcion + "</option>");
                    });
                    $('#pscat_Id').trigger("chosen:updated");
                }
                else {
                    $('#pscat_Id').empty();
                    $('#pscat_Id').append("<option value=''>Seleccione</option>");
                }
            });
    }
});

//////////////////////////////////////////////////////////
$(document).on("change", "#pcat_Id", function () {
    GetSubCategoriaProducto();
});

function GetSubCategoriaProducto() {
    var idcate = $('#pcat_Id').val();
    if (idcate !== "") {
        $.ajax({
            url: "/Producto/GetSubCategoriaProducto",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ idcate: idcate })
        })
            .done(function (data) {
                if (data.length > 0) {
                    $('#pscat_Id').empty();
                    $('#pscat_Id').append("<option value=''>Seleccione subcategoría</option>");
                    $.each(data, function (key, val) {
                        $('#pscat_Id').append("<option value=" + val.pscat_Id + ">" + val.pscat_Descripcion + "</option>");
                    });

                    $('#pscat_Id').trigger("chosen:updated");
                }
                else {
                    $('#pscat_Id').empty();
                    $('#pscat_Id').append("<option value=''>Seleccione</option>");
                }
            });
    }
    else {
        $('#pscat_Id').empty();
        $('#pscat_Id').append("<option value=''>Seleccione</option>");
    }
}
///////////////////////////////////////////////////////////////////
$("#pcat_Descripcion").keyup(function () {
    $('#categoria').hide();
});
///////////////////////////////////////////////////////////////////

$('#pcat_Id').change(function () {
    $('#dep').hide();
});

$('#pscat_Id').change(function () {
    $('#subcate').hide();
});

$('#prod_Codigo').change(function () {
    $('#codigo').hide();
});


$('#prod_Codigo').keyup(function () {
    $('#codigo').hide();
});

$('#prod_CodigoBarras').keyup(function () {
    $('#barras').hide();
});

$('#prod_Descripcion').keyup(function () {
    $('#descripcion').hide();
});


$('#prod_Descripcion').keyup(function () {
    $('#descripcion').hide();
});


$('#prod_Marca').keyup(function () {
    $('#marca').hide();
});
////////////


$('#prod_Talla').keyup(function () {
    $('#talla').hide();
});

$('#prod_Color').keyup(function () {
    $('#color').hide();
});

$('#uni_Id').change(function () {
    $('#medida').hide();
});

$('#prov_Id').change(function () {
    $('#proveedor').hide();
});

