
//Cargar Codigo Producto
//$('#pscat_Id').change(function () {
//    //console.log('Entra pero no hace nada');
//    var cate = $('#pcat_Id').val();
//    var subcate = $('#pscat_Id').val();
//    var prodcod = $('#prod_Codigo').val();
//    console.log(cate);
//    console.log(subcate);

//    $.ajax({
//        type: 'POST',
//        url: '/Producto/GetValue',
//        data: JSON.stringify({ pcat_Id: cate, pscat_Id: subcate }),
//        contentType: 'application/json',
//        success: function (data) {
//            $('#CodigoPro').val(data);
//            console.log(data);

//        }

//    })
//});


//Validacion de solo letras
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toUpperCase();
    letras = " ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-+()$.";
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

$("#prod_CodigoBarras").focus();

$(document).keydown(function (e) {

    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {

        e.preventDefault();

        //alert("Ctrl-g pressed");

        $("form").submit();

        return false;
    }
    return true;
});


$('#prod_Descripcion').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

$('#prod_Marca').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

$('#prod_Modelo').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

$('#prod_Color').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

$('#prod_Talla').on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);

});

function solonumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toUpperCase();
    letras = "1234567890";
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


$("#prod_CodigoBarras").on("keypress keyup blur", function (event) {
    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
