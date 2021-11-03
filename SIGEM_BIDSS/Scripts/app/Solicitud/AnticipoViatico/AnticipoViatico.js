var Formulario=[];
var token;
var Anvi_JefeInmediato;
var Anvi_GralFechaSolicitud;
var Anvi_Cliente;
var Anvi_FechaViaje;
var dep_codigo;
var mun_Codigo;
var Anvi_PropositoVisita;
var Anvi_DiasVisita;
var Anvi_tptran_Id;

function GetFechas() {
    var Fechas = {
        FechaInicio: document.getElementById("Anvi_GralFechaSolicitud").value,
        FechaFin: document.getElementById("Anvi_FechaViaje").value
    };
    return Fechas;
}
$("#frmsubmit").click(function () {
    document.getElementById('spinnerForm').classList.add("overlay");
    document.getElementById('spinnerDiv').removeAttribute("hidden");
    $("form").submit()
})

document.getElementById("Anvi_GralFechaSolicitud").addEventListener("blur", function () {
    CalcularFechas()
});

document.getElementById("Anvi_FechaViaje").addEventListener("blur", function () {
    CalcularFechas()
});

function CalcularFechas() {
    vFechas = GetFechas()
    console.log("JS: " + vFechas);

    $.ajax({
        url: "/AnticipoViatico/CalcularFecha",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cCalFechas: vFechas }),
    })
        .done(function (data) {
            console.log(data);
            document.getElementById("spanFechaInicio").innerText = data.MASspan
            document.getElementById("spanFechaFin").innerText = data.MASspanFecha
        });
}


$(document).ready(function () {
   
    $("#Anvi_Comentario")[0].maxLength = 250;
});

function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
    return /^[0-9]+$/.test(tecla);
}

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

$(document).ready(function () {
    $("#Anvi_Cliente")[0].maxLength = 100;
    $('#Anvi_GralFechaSolicitud').val(GetTodayDate());
    var CodDepartamento = $('#dep_codigo').val();
    var muncod = $('#munCodigo').val();
    if (CodDepartamento !== "") {
        $.ajax({
            url: "/AnticipoViatico/GetMunicipios",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodDepartamento: CodDepartamento })
        })
            .done(function (data) {
                if (data.length > 0) {
                    $('#mun_Codigo').empty();
                    $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
                    $.each(data, function (key, val) {
                        if (muncod !== "") {
                            if (val.mun_codigo === muncod) {
                                $('#mun_Codigo').append("<option selected='selected' value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                            }
                            else
                                $('#mun_Codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                        }
                        else
                            $('#mun_Codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                    });
                    $('#mun_Codigo').trigger("chosen:updated");
                }
                else {
                    $('#mun_Codigo').empty();
                    $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
                }
            });
    }
});

//Funcion no aceptar espacios en el textbox
//document.addEventListener("input", function () {
//    $("input[type='text']", 'form').each(function () {
//        _id = $(this).attr("id");
//        _value = document.getElementById(_id).value;
//        document.getElementById(_id).value = _value.trimStart();
//    });

//    $("textarea").each(function () {
//        _id = $(this).attr("id");
//        console.log("Hi:", _id);
//    });
    
//});


//

//$("#btnAutorizacion").click(function () {
//    Formulario={
//        Anvi_JefeInmediato: Anvi_JefeInmediato,
//        Anvi_GralFechaSolicitud: Anvi_GralFechaSolicitud,
//        Anvi_Cliente: Anvi_Cliente,
//        Anvi_FechaViaje: Anvi_FechaViaje,
//        mun_Codigo: mun_Codigo,
//        Anvi_PropositoVisita: Anvi_PropositoVisita,
//        Anvi_DiasVisita: Anvi_DiasVisita,
//        Anvi_tptran_Id: Anvi_tptran_Id,
//        Anvi_Hospedaje: Anvi_Hospedaje,
//        Anvi_Comentario: Anvi_Comentario

//    };

//    $.ajax({
//        url: "/AnticipoViatico/Create",
//        method: "POST",
//        dataType: 'json',
//        data: { __RequestVerificationToken: token, tbAnticipoViatico: Formulario, dep_codigo: "ajax" }
//    })
//        .done(function (data) {
//            if (data === "Index") {
//                $("#AutorizacionModal").modal("hide");
//                window.location.href = "/AnticipoViatico/Index";
//            }
//            else {
//                Anvi_JefeInmediato = $("#Anvi_JefeInmediato").val();
//                $("#AutorizacionModal").modal("hide");
//                //Anvi_GralFechaSolicitud = $("#Anvi_GralFechaSolicitud").val();
//                //Anvi_Cliente = $("#Anvi_Cliente").val();
//                //Anvi_FechaViaje = $("#Anvi_FechaViaje").val();
//                //dep_codigo = $("#dep_codigo").val();
//                //mun_Codigo = $("#mun_Codigo").val();
//                //Anvi_PropositoVisita = $("#Anvi_PropositoVisita").val();
//                //Anvi_DiasVisita = $("#Anvi_DiasVisita").val();
//                //Anvi_tptran_Id = $("#Anvi_tptran_Id").val();
//            }
//        }).fail(function (xhr, a, error) {
//            console.log("error",error);
//        });
//});

//--Date Picker--//
$('#Anvi_GralFechaSolicitud,#Anvi_FechaViaje').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0",
}).datepicker("setDate", new Date());

$(document).on("change", "#dep_codigo", function () {
    GetMunicipios();
});

function GetMunicipios() {
    var CodDepartamento = $('#dep_codigo').val();
    if (CodDepartamento !== "") {
        $.ajax({
            url: "/AnticipoViatico/GetMunicipios",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodDepartamento: CodDepartamento })
        })
            .done(function (data) {
                if (data.length > 0) {
                    $('#mun_Codigo').empty();
                    $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
                    $.each(data, function (key, val) {
                        $('#mun_Codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                    });

                    $('#mun_Codigo').trigger("chosen:updated");
                }
                else {
                    $('#mun_Codigo').empty();
                    $('#mun_Codigo').append("<option value=''>Seleccione Municipo</option>");
                }
            });
    }
    else {
        $('#mun_Codigo').empty();
        $('#mun_Codigo').append("<option value=''>Seleccione Municipio</option>");
    }
}

//Get Current Date
function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "/" + (MM + 1) + "/" + yyyy;
    return currentDate;
}

//Prevent Submit
//$("#frmAnticipoViatico").submit(function (event) {    
//    token = $('[name=__RequestVerificationToken]').val();
//    Anvi_JefeInmediato = $("#Anvi_JefeInmediato").val();
//    Anvi_GralFechaSolicitud = $("#Anvi_GralFechaSolicitud").val();
//    Anvi_Cliente = $("#Anvi_Cliente").val();
//    Anvi_FechaViaje = $("#Anvi_FechaViaje").val();
//    dep_codigo = $("#dep_codigo").val();
//    mun_Codigo = $("#mun_Codigo").val();
//    Anvi_PropositoVisita = $("#Anvi_PropositoVisita").val();
//    Anvi_DiasVisita = $("#Anvi_DiasVisita").val();
//    Anvi_tptran_Id = $("#Anvi_tptran_Id").val();
//    Anvi_Hospedaje = $("#Anvi_Hospedaje").val();
//    Anvi_Comentario = $("#Anvi_Comentario").val();

//    if (Anvi_JefeInmediato !== "" && Anvi_GralFechaSolicitud !== "" && Anvi_Cliente !== "" && Anvi_FechaViaje !== "" && dep_codigo !== "" && mun_Codigo !== "" && Anvi_PropositoVisita !== "" && Anvi_DiasVisita !== "" && Anvi_tptran_Id !== "" && Anvi_Hospedaje !== "" && Anvi_Comentario !== "") {
//        event.preventDefault();
//        dep_codigo = "ajax";
//        $("#AutorizacionModal").modal();
//     }
//});

//Quitar mensajes de error cuando el valor cambia
//DropdownList
$("#Anvi_JefeInmediato").change(function () {
    var JefeInmediato = $("#Anvi_JefeInmediato").val();
    if (JefeInmediato !== "") {
        $("#JefeInmediato").text("");
    }
});

$("#dep_codigo").change(function () {
    var UsuarioCrea = $("#dep_codigo").val();
    if (UsuarioCrea !== "") {
        $("#UsuarioCrea").text("");
    }
});

$("#mun_Codigo").change(function () {
    var CodigoMun = $("#mun_Codigo").val();
    if (CodigoMun !== "") {
        $("#CodigoMun").text(""
        );
    }
});

$("#Anvi_tptran_Id").change(function () {
    var tptran_Id = $("#Anvi_tptran_Id").val();
    if (tptran_Id !== "") {
        $("#tptran_Id").text("");
    }
});

//Textbox
$("#Anvi_Cliente").change(function () {
    var Cliente = $("#Anvi_Cliente").val();
    if (Cliente !== "") {
        $("#Cliente").text("");
    }
});
//---------------------messs-----------------------------------------


$('#Anvi_JefeInmediato').change(function () {
    $('#JefeInmediato').hide();
});

$('#Anvi_GralFechaSolicitud').keyup(function () {
    $('#GralFechaSolicitud').hide();
});

$('#Anvi_Cliente').keyup(function () {
    $('#Cliente').hide();
});


$('#Anvi_FechaViaje').keyup(function () {
    $('#FechaViaje').hide();
    $('#spanFechaFin').hide();
});



$('#Anvi_UsuarioCrea').change(function () {
    $('#UsuarioCrea').hide();
});

$('#mun_Codigo').change(function () {
    $('#CodigoMun').hide();
});

$('#Anvi_PropositoVisita').keyup(function () {
    $('#PropositoVisita').hide();
});

$('#Anvi_DiasVisita').keyup(function () {
    $('#DiasVisita').hide();
});

$('#Anvi_tptran_Id').change(function () {
    $('#tptran_Id').hide();
});

$('#Anvi_Hospedaje').keyup(function () {
    $('#Hospedaje').hide();
});

$('#Anvi_Comentario').keyup(function () {
    $('#Comentario').hide();
});

////////////////////////////////////
////////////////////////////////////
//const _id = document.getElementById('RazonRechazo');
//_id.addEventListener("input", function () {
//    this.value = this.value.trimStart()
//    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(_id.value)) {
//        this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
//    }
//})