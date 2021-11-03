



//////SOLO LETRAS
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}

$("#tpv_Descripcion").keyup(function () {
    $('#errorviatico').hide();
});



////SOLO NUMEROS 
function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[.0-9]+$/.test(tecla);
}

