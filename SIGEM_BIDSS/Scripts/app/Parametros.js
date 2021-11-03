function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgpreview').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}



$("#CargarFoto").change(function () {
    readURL(this);
});


//Solo numeros
$('#par_TelefonoEmpresa').keypress(function (e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
    return /^[0-9\-]+$/.test(tecla);

});

////////////////rango de porcentaje
document.getElementById("porcentaje").onkeyup = function () {

    valido = document.getElementById('errorPorcentaje');
    var input = parseInt(this.value);
    if (input < 0 || input > 100)
    {
        valido.innerText = "El Campo Porcentaje de Salario no debe ser mayor al 100%"
        $('#porcentaje').focus();
        $('#porcentaje').addClass('is-invalid');
    } else {
        valido.innerText = "";
        $('#porcentaje').removeClass('is-invalid');
       
       
    } 
}
/////////////rango de numero

document.getElementById("frecuencia").onkeyup = function () {

    valido = document.getElementById('errorfrecuencia');
    var input = parseInt(this.value);
    if (input < 0 || input > 14) {
        valido.innerText = "El Campo Frecuencia de Salario no debe ser mayor a 14"
        $('#frecuencia').focus();
        $('#frecuencia').addClass('is-invalid');
    } else {
        valido.innerText = "";
        $('#frecuencia').removeClass('is-invalid');


    }
}

document.addEventListener("input", function () {
    $("input[type='text']", 'form').each(function () {
        var _id = $(this).attr("id");
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();
       
    });
    $(".normalize", 'form').each(function (e) {
        if (!/^[ A-Záéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ .,A-Záéíóúüñ]+/ig, "");
        }
    });
})








//Validar que se borre mensaje mientras se escribe

$("#par_NombreEmpresa").keyup(function () {
    $('#errornombreempresa').hide();
});

$("#CargarFoto").change(function () {
    $('#errorlogo').hide();
});



$('#par_TelefonoEmpresa').keyup(function () {
    $('#errortelefonoempresa').hide();
    $('#errorformatotelefono').hide();
});

$('#par_CorreoEmpresa').keyup(function () {
    $('#emailCorre').hide();
});

$('#par_CorreoEmisor').keyup(function () {
    $('#emailEmi').hide();
});

$('#par_Password').keyup(function () {
    $('#errorpassword').hide();
});

$('#par_CorreoRRHH').keyup(function () {
    $('#emailRHHH').hide();
});


$('#par_Puerto').keyup(function () {
    $('#errorPuerto').hide();
});

$('#par_PorcentajeAdelantoSalario').keyup(function () {
    $('#errorPorcentaje').hide();
});

$('#par_FrecuenciaAdelantoSalario').keyup(function () {
    $('#errorfrecuencia').hide();
});




$('#CargarFoto').keyup(function () {
    $('#errorlogo').hide();
});

//Numero de telefono
function validartel(e) {
    campo = event.target;
    $(campo).on("input", function (event) {
        var Telefono = this.value.match(/[0-9\s]+/);

        if (Telefono !== null) {
            this.value = '+' + ((Telefono).toString().replace(/[^ 0-9a-záéíóúñ@._-\s]\d +/ig, ""));
        }
        else {
            this.value = null;
        }
    });
}


//campos especiales
$('#par_NombreEmpresa').keypress(function (e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla === 8) {
        return true;
    }
    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /[A-Za-z0-9]/;
    //tecla_final = String.fromCharCode(tecla);
    //return patron.test(tecla_final);
});


//Correo electronico
function Caracteres_Email(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890@.-_]+$/.test(tecla);

}

function CorreoElectronico(string) {//Algunos caracteres especiales para el correo
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890@ .-_';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) !== -1)
            out += string.charAt(i);

    return out;
}


document.getElementById('par_CorreoEmpresa').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('emailOK');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#par_CorreoEmpresa').removeClass('is-invalid');
    } else {
        valido.innerText = "Correo Inválido";
        $('#par_CorreoEmpresa').focus();
        $('#par_CorreoEmpresa').addClass('is-invalid');
    }
});


document.getElementById('par_CorreoEmisor').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('emailEoK');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#par_CorreoEmisor').removeClass('is-invalid');
    } else {
        valido.innerText = "Correo Inválido";
        $('#par_CorreoEmisor').focus();
        $('#par_CorreoEmisor').addClass('is-invalid');
    }
});
document.getElementById('par_CorreoRRHH').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('emailEoKi');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#par_CorreoRRHH').removeClass('is-invalid');
    } else {
        valido.innerText = "Correo Inválido";
        $('#par_CorreoRRHH').focus();
        $('#par_CorreoRRHH').addClass('is-invalid');
    }
});


document.getElementById('par_Servidor').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('emailSer');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#par_Servidor').removeClass('is-invalid');
    } else {
        valido.innerText = "Direccion de Servidor Inválida";
        $('#par_Servidor').focus();
        $('#par_Servidor').addClass('is-invalid');
    }
});


function soloNumerosP(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
    return /^[0-9.]+$/.test(tecla);
}

function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
    return /^[0-9]+$/.test(tecla);
}

$(document).on('blur', '#par_Password', function () {
    var usu_Password = $('#par_Password').val().trim();
    valido = document.getElementById('Password');
    if (usu_Password.length < 8) {
        valido.innerText = "Longitud debe ser de 8 caracteres.";
        //$('#usu_Password').focus();
    }
});

function Passworddd(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
    return /^[0-9a-zA-Z-_.#*]+$/.test(tecla);
}



function mostrarPassword() {
    var cambio = document.getElementById("txtPassword");
    if (cambio.type === "password") {
        cambio.type = "text";
        $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
    } else {
        cambio.type = "password";
        $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
    }
}

$(document).ready(function () {
    //CheckBox mostrar contraseña
    $('#ShowPassword').click(function () {
        $('#Password').attr('type', $(this).is(':checked') ? 'text' : 'password');
    });
});

///----------validar solo letras
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " -+'/áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false;
    for (var i in especiales) {
        if (key === especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) === -1 && !tecla_especial) {
        return false;
    }
}


$("#frmEditParametro").submit(function (event) {
    var CorreoEmpresa = $("#emailOK").text();
    var CorreoEmisor = $("#emailEoK").text();
    var CorreoRRHH = $("#emailEoKi").text();
    var Servidor = $("#emailSer").text();
    if (CorreoEmpresa !== "" || CorreoEmisor !== "" || CorreoRRHH !== "" || Servidor !== "") {
        event.preventDefault();
    }
});

$("#frmCreateParametro").submit(function (event) {
    var CorreoEmpresa = $("#emailOK").text();
    var CorreoEmisor = $("#emailEoK").text();
    var CorreoRRHH = $("#emailEoKi").text();
    var Servidor = $("#emailSer").text();
    if (CorreoEmpresa !== "" || CorreoEmisor !== "" || CorreoRRHH !== "" || Servidor !== "") {
        event.preventDefault();
    }
});

/////////////////////////////////////////////////////////
function MASK(form, n, mask, format) {
    if (format == "undefined") format = false;
    if (format || NUM(n)) {
        dec = 0, point = 0;
        x = mask.indexOf(".") + 1;
        if (x) { dec = mask.length - x; }



        if (dec) {
            n = NUM(n, dec) + "";
            x = n.indexOf(".") + 1;
            if (x) { point = n.length - x; } else { n += "."; }
        } else {
            n = NUM(n, 0) + "";
        }
        for (var x = point; x < dec; x++) {
            n += "0";
        }
        x = n.length, y = mask.length, XMASK = "";
        while (x || y) {
            if (x) {
                while (y && "#0.".indexOf(mask.charAt(y - 1)) == -1) {
                    if (n.charAt(x - 1) != "-")
                        XMASK = mask.charAt(y - 1) + XMASK;
                    y--;
                }
                XMASK = n.charAt(x - 1) + XMASK, x--;
            } else if (y && "$0".indexOf(mask.charAt(y - 1)) + 1) {
                XMASK = mask.charAt(y - 1) + XMASK;
            }
            if (y) { y-- }
        }
    } else {
        XMASK = "";
    }
    if (form) {
        form.value = XMASK;
        if (NUM(n) < 0) {
            form.style.color = "#FF0000";
        } else {
            form.style.color = "#000000";
        }
    }
    return XMASK;
}



// Convierte una cadena alfanumérica a numérica (incluyendo formulas aritméticas)
//
// s   = cadena a ser convertida a numérica
// dec = numero de decimales a redondear
//
// La función devuelve el numero redondeado



