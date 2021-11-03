var contador = 0;


//Validacion de solo letras
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "áéíóúabcdefghijklmnñopqrstuvwxyz ";
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



//Validar Los campos numericos
function format(input) {
    var num = input.value.replace(/\,/g, '');
    if (!isNaN(num)) {
        input.value = num;
    }
    else {
        //alert('Solo se permiten numeros');
        input.value = input.value.replace(/[^\d\.]*/g, '');
    }
}
//fin
///Editar por medio de una Modal, tambien obtiene los datos para mostrarlos


$(document).on("click", "#TablaSub tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
})

$(document).on("click", "#Datatable tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
});

$('#pscat_Descripcion').keyup(function () {
    $('#ErrorDescripcion').hide();
});

$('#AgregarSubCategorias').click(function () {

    var Descripcion = $('#pscat_Descripcion').val();

    var Categoria = $('#pcat_Descripcion').val();

    if (Categoria == '') {
        $('#MessageError').text('');
        $('#categoria').text('');
        $('#ErrorISV').text('');
        $('#categoria').after('<ul id="categoria" class="validation-summary-errors text-danger">Campo Categoría Requerido</ul>');

    }

    if (Descripcion == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo SubCategorías Requerido</ul>');

    }
 

    else {
      

      
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'Descripcion'>" + $('#pscat_Descripcion').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td>" + '<button id="removerSubCategoria" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#Datatable').append(copiar);


        var SubCategoria = GetSubCategoria();

        $.ajax({
            url: "/ProductoCategoria/GuardarSubCategoria",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbsubcategoria: SubCategoria }),
        })
            .done(function (data) {
                $('#pscat_Descripcion').val('');
                $('#pscat_ISV').val('');

                $('#MessageError').text('');
                $('#ErrorDescripcion').text('');
    
            });
    }

});

function GetSubCategoria() {
    var SubCategoria = {
        pscat_Descripcion: $('#pscat_Descripcion').val(),
 
        pscat_UsuarioCrea: contador,
        pscat_Id: contador,

    };
    return SubCategoria;
}


$('#CrearSubCategoria').click(function () {

    var Descripcion = $('#pscat_Descripcion').val();

    var Categoria = $('#pcat_Descripcion').val();


    if (Categoria == '') {
        $('#MessageError').text('');
        $('#categoria').text('');
        $('#ErrorISV').text('');
        $('#categoria').after('<ul id="categorias" class="validation-summary-errors text-danger">Campo Categoría Requerido</ul>');

    }


    if (Descripcion == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcions" class="validation-summary-errors text-danger">Campo SubCategorías Requerido</ul>');

    }

    else {

        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'Descripcion'>" + $('#pscat_Descripcion').val() + "</td>";
      
        copiar += "<td>" + '<button id="removerSubCategoria" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#TablaSub').append(copiar);


        var SubCategoria = GetSubCategoria();

        $.ajax({
            url: "/ProductoCategoria/GuardarSubCategoria",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbsubcategoria: SubCategoria }),
        })
            .done(function (data) {
                $('#pscat_Descripcion').val('');
                $('#pscat_ISV').val('');

                validar();

                $('#MessageError').text('');
                $('#ErrorDescripcion').text('');
                $('#ErrorISV').text('');
            });
    }
  
});

function GetSubCategoria() {
    var SubCategoria = {
        pscat_Descripcion: $('#pscat_Descripcion').val(),
        pscat_ISV: $('#pscat_ISV').val(),
        pscat_UsuarioCrea: contador,
        pscat_Id: contador,

    };
    return SubCategoria;
}

///REMOVER EL DETALLE
$(document).on("click", "#Datatable tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
})


///REMOVER EL DETALLE
$(document).on("click", "#TablaSub tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
})

//$('#pscat_Descripcion').blur(function () {
//    if ($.trim($('#pscat_Descripcion').val()) == 0) {
//        $('#errorDescripcion').text('');
//        $('#validationDescripcion').after('<ul id="errorDescripcion" class="validation-summary-errors text-danger">Campo Descripcion Requerido</ul>');
//    }
//});
////////////////////////////////////////////////////////////////////////
/////////Abrir modal y obtener los valores para los textbox
function EditStudentRecord(pscat_Id) {
    $.ajax({
        url: "/ProductoCategoria/GetSubCate",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ pscat_Id: pscat_Id }),

    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#pscat_Id").val(item.pscat_Id);
                    $("#pscat_Descripcion_edit").val(item.pscat_Descripcion);
                    $("#MyModal").modal();
                })
            }
        })
}


//------------------Actualizar Municipio(Modal)     Guardar los cambios del modal de Editar
$("#Btnsubmit").click(function () {
   
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/ProductoCategoria/UpdateSubCategoria",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#Msj").text("No se pudo actualizar el registro, contacte al administrador");
            else
                location.reload();
        }
    });
})


$("#pcat_Nombre").change(function () {
    var str = $("#pcat_Nombre").val();
    var res = str.toUpperCase();
    $("#pcat_Nombre").val(res);
});

$("#pscat_Descripcion").change(function () {
    var str = $("#pscat_Descripcion").val();
    var res = str.toUpperCase();
    $("#pscat_Descripcion").val(res);
});

$("#pscat_Descripcion_edit").change(function () {
    var str = $("#pscat_Descripcion_edit").val();
    var res = str.toUpperCase();
    $("#pscat_Descripcion_edit").val(res);
});

$(document).keydown(function (e) {

    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {

        e.preventDefault();
        $("form").submit();
        return false;
    }
    return true;

});

$("#bton").keypress(function (e) {
    if (e.which == 13) {

        $("form").submit();
    }
});

$('#pscat_Descripcion_edit').blur(function () {
    if ($.trim($('#pscat_Descripcion_edit').val()) == 0) {
        $('#editerror').text('');
        $('#editerror').after('<ul id="editerror" class="validation-summary-errors text-danger">Campo Subcategoría Requerido</ul>');
    }
});


$('#pcat_Descripcion').keyup(function () {
    $('#categorias').hide();
});

$('#pscat_Descripcion').keyup(function () {
    $('#ErrorDescripcions').hide();
});



$('#pscat_Descripcion').keyup(function () {
    $('#DescripcionErrorEdit').hide();
});

$('#pscat_Descripcion_edit').keyup(function () {
    $('#DescripcionErrorEdit').hide();
});


$('#pscat_Descripcion_edit').keyup(function () {
    $('#DescripcionErrorEdit').hide();
});





////////////////////////////////////////////////////////////


//$("#frmCreateCategoria").submit(function (event) {
//    var Categoria = $("#categoria").text();
//    var subcategoria = $("#DescripcionError").text();
//    if (Categoria !== "" || subcategoria !== "") {
//        event.preventDefault();
//    }
//});

//$("#frmEditCategoria").submit(function (event) {
//    var Categoria = $("#categoria").text();
//    var subcategoria = $("#DescripcionError").text();
//    if (Categoria !== "" || subcategoria !== "") {
//        event.preventDefault();
//    }
//});
///////////////////////////////////////////////////////////////



