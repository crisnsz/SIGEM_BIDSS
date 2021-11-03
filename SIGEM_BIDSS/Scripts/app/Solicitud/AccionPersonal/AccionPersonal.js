$(function () {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false
    });
})

//--Date Picker--//
//$('#Acp_FechaSolicitud,').datepicker({
//    format: "dd/mm/yyyy",
//    startDate: "01/01/1990",
//    language: "es",
//    daysOfWeekDisabled: "0"
//});
//Funcion no aceptar espacios en el textbox
//Funcion no aceptar espacios en el textbox
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



//Get Current Date
function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "/" + (MM + 1) + "/" + yyyy;
    return currentDate;
}
//Quitar mensajes de error cuando el valor cambia
//DropdownList
$("#Acp_JefeInmediato").change(function () {
    var JefeInmediato = $("#Acp_JefeInmediato").val();
    if (JefeInmediato !== "") {
        $("#JefeInmediato").text("");
    }
});


$("#tipmo_id").change(function () {
    var CodigoMun = $("#tipmo_id").val();
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
$("#Acp_Comentario").change(function () {
    var Cliente = $("#Acp_Comentario").val();
    if (Cliente !== "") {
        $("#Cliente").text("");
    }
});




