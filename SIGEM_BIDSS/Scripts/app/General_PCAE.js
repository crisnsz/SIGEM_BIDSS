

//Funcion no aceptar espacios en el textbox
//document.addEventListener("input", function () {
//    $(".nospace", 'form').each(function (e) {
//        var _id = $(this).attr("id");
//        var el = document.getElementById('' + _id + '')
//        _value = document.getElementById(_id).value;
//        document.getElementById(_id).value = _value.trimStart();
//    });
//    $(".normalize", 'form').each(function (e) {
//        if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
//            this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
//        }
//    });
//});


//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function (event) {
    var elements = document.body.getElementsByClassName('nospace');
    console.log(elements.length)
    for (var i = 0; i < elements.length; i++) {
        var _getid = this.activeElement.id;
        var _object = document.getElementById('' + _getid + '')
        var _elementValue = _object.value;
        var _strLength = _elementValue.length;
        var _ContainsSpace = _elementValue.includes(" ");
        _object.value = _elementValue.trimStart();
        if (_ContainsSpace) {
            var startindexSpace = _elementValue.indexOf(" ");
            var startWithSpace = _elementValue.startsWith(" ");
            var lastindexSpace = _elementValue.lastIndexOf(" ");
            var _strLastChar = _elementValue.slice(-1);
            var _strEndWithSpace = _elementValue.endsWith(" ");
            if (_strEndWithSpace && event.data === _strLastChar) {
                _object.value = _elementValue.replace('  ', ' ');
                if (startWithSpace) {
                    _object.value = _elementValue.trimStart();
                }
            }
        }
        if (!/^[ .,a-z0-9áéíóúüñ]*$/i.test(_elementValue)) {
            _object.value = _object.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
        }
    }
});






//Funcion de Solo letras en el textbox
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " '/áéíóúabcdefghijklmnñopqrstuvwxyz";
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

////SOLO NUMEROS 
function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9]+$/.test(tecla);
}

document.addEventListener("input", function (e) {
    $(".justNumber", 'form').each(function () {
        console.log(e.data);

        var _Getid = $(this).attr("id");
        var _id = document.getElementById('' + _Getid + '')
        var _eValue = _id.value;
        //var regEx = /^[0-9]+$/;
        var regEx = ""
        var _ContainsDot = _eValue.includes(".");
        tecla = (document.all) ? e.keyCode : e.which;
        tecla = String.fromCharCode(tecla)
        if (_eValue !== "" && !_ContainsDot) {
            //regEx = /^[0-9.]+$/;
        }
        if (_ContainsDot) {
            var indexDot = _eValue.indexOf(".");
            var res = _eValue.substring(indexDot);
            var resTwo = _eValue.substring(indexDot).length;
            var _spanID = document.getElementById("" + _id.nextElementSibling.id + "");
            if (res.length >= 3) {
                var _beforeDot = _eValue.substring(0, indexDot);
                var _afterDot = _eValue.substr(indexDot, 3);
                _id.value = _beforeDot + _afterDot;
                document.getElementById("" + _Getid + "").value = _beforeDot + _afterDot;
                _spanID.innerText = "Solo se aceptan dos decimales.";
            }
        }
        return regEx.test(tecla);
    });
})

function soloNumerosCantidad(e) {
    var _id = document.getElementById("" + e.target.id + "");
    var _eValue = _id.value;
    var regEx = /^[0-9]+$/;
    var _ContainsDot = _eValue.includes(".");
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    if (_eValue !== "" && !_ContainsDot) {
        regEx = /^[0-9.]+$/;
    }
    if (_ContainsDot) {
        var indexDot = _eValue.indexOf(".");
        var res = _eValue.substring(indexDot);
        var resTwo = _eValue.substring(indexDot).length;
        var _spanID = document.getElementById("" + _id.nextElementSibling.id + "");
        if (res.length >= 2) {
            var _beforeDot = _eValue.substring(0, indexDot);
            var _afterDot = _eValue.substr(indexDot, 2);
            _id.value = _beforeDot + _afterDot;
            document.getElementById("" + e.target.id + "").value = _beforeDot + _afterDot;
            _spanID.innerText = "Solo se aceptan dos decimales.";
        }
    }
    return regEx.test(tecla);
}


function soloLetrastIPOSANGRE(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " '+-áéíóúabcdefghijklmnñopqrstuvwxyz";
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


//Mostrar Mensaje:Campo requerido en tiempo real
$("#tps_Descripcion").change(function () {
    var tps_Id = $("#tps_Descripcion").val();
    if (tps_Id != '') {
        $('#tps_IdRequered').text('');
    }
    else {
        $('#tps_IdRequered').after('<p id="tps_IdRequered" style="color:red">Campo Descripción requerido</p>');
    }
});

//borrar mensajes en tiempo real
$("#tps_Descripcion").keyup(function () {
    $('#tps_IdRequered').text('');
    $('#tps_IdRequered').text('');
    $('#tps_IdRequered').text('');
    $('#tps_IdRequered  ').text('');
});