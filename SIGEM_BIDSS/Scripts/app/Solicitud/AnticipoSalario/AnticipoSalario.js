
const formatter = new Intl.NumberFormat('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
});

$(document).ready(function () {
    $("#Ansal_Justificacion")[0].maxLength = 250;
    $("#Ansal_Comentario")[0].maxLength = 250;
});

document.getElementById("frmsubmit").addEventListener("click", function () {
    document.getElementById('spinnerForm').classList.add("overlay");
    document.getElementById('spinnerDiv').removeAttribute("hidden");
    document.getElementById('frmAnticipoSalario').submit();
})

document.getElementById("openModal").addEventListener("click", function () {
    //
    var v_Ansal_JefeInmediato = document.getElementById('Ansal_JefeInmediato').value;
    var b_Ansal_JefeInmediato = true;
    //
    var v_Cantidad = document.getElementById('Cantidad').value;
    var b_Cantidad = true;
    //
    var v_tpsal_id = document.getElementById('tpsal_id').value;
    var b_tpsal_id = true;
    //
    var v_Ansal_Justificacion = document.getElementById('Ansal_Justificacion').value;
    var b_Ansal_Justificacion = true;

    if (v_Ansal_JefeInmediato === "") {
        document.getElementById('spanJefeInmediato').innerHTML = "El campo Jefe Inmediato es obligatorio."
        b_Ansal_JefeInmediato = false;
    }
    if (v_Cantidad === "") {

        document.getElementById('spanCantidad').innerText = "El campo Monto a solicitar es obligatorio."
        b_Cantidad = false;
    }
    if (v_tpsal_id === "") {
        document.getElementById('spantpsal_id').innerText = "El campo Tipo de Salario es obligatorio."
        b_tpsal_id = false;
    }
    if (v_Ansal_Justificacion === "") {
        document.getElementById('spanJustificacion').innerText = "El campo Justificación es obligatorio."
        b_Ansal_Justificacion = false;
    }
    if (b_Ansal_JefeInmediato && b_Cantidad && b_tpsal_id && b_Ansal_Justificacion) {
        $('#ModalAuth').modal();
    }
})



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

function GetDecimales() {
    var Decimales = {
        empSueldo: parseInt(document.getElementById("Sueldo").value).toFixed(2),
        empPorcetanje: parseInt(document.getElementById("Porcentaje").value).toFixed(2),
        empMonto: parseInt(document.getElementById("number").value).toFixed(2)
    };
    return Decimales;
}

var monto = document.getElementById("Cantidad");
monto.addEventListener("input", function () {
    document.getElementById("number").value = this.value.replace(/,/g, "")
    vDecimales = GetDecimales()
    $.ajax({
        url: "/AnticipoSalario/Calcular",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cCalDecimal: vDecimales }),
    })
        .done(function (data) {
            if (data !== "" || data !== null) {
                var v_spanCant = document.getElementById('spanCantidad').innerHTML;
                if (!v_spanCant.startsWith("Solo se")) {
                    document.getElementById("spanCantidad").innerText = data.spanCantidad

                }
            }
        });
});


//--Date Picker--//
$('#Ansal_GralFechaSolicitud').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1900",
    language: "es",
    daysOfWeekDisabled: "0",
}).datepicker("setDate", new Date());;



//Quitar mensajes de error cuando el valor cambia
//DropdownList
$("#Ansal_JefeInmediato").change(function () {
    var JefeInmediato = $("#Ansal_JefeInmediato").val();
    if (JefeInmediato != "") {
        $("#JefeInmediato").text("");
        document.getElementById('spanJefeInmediato').innerHTML = ""
    }
});

$("#tpsal_id").change(function () {
    var tpsal_id = $("#tpsal_id").val();
    if (tpsal_id !== "") {
        $("#spantpsal_id").text("");
    }
});


//TextBox
$("#Cantidad").change(function () {
    var v_spanCant = document.getElementById('spanCantidad').innerHTML;
    var Cantidad = $("#Cantidad").val();
    if (Cantidad !== "") {

        if (!v_spanCant.startsWith("Solo se")) {
            document.getElementById('spanCantidad').innerText = "";
        }
        else if (!v_spanCant.startsWith("El monto no")) {
            document.getElementById('spanCantidad').innerText = "";
        }
    }
});



$("#Ansal_Justificacion").change(function () {
    var Justificacion = $("#Ansal_Justificacion").val();
    if (Justificacion !== "") {
        $("#spanJustificacion").text("");
    }
});

$("#Ansal_Comentario").change(function () {
    var Comentario = $("#Ansal_Comentario").val();
    if (Comentario !== "") {
        $("#spanComentario").text("");
    }
});