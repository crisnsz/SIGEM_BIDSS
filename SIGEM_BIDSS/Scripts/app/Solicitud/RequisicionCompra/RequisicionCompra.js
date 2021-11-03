//--------------------------------------------------------------------------------------//
$("#frmsubmit").click(function () {
    document.getElementById('spinnerForm').classList.add("overlay");
    document.getElementById('spinnerDiv').removeAttribute("hidden"); 
    document.getElementById('frmRequisicionCompra').submit()
})

$(document).ready(function () {
    $("#Reqco_Comentario")[0].maxLength = 250;
});

//--Date Picker--//
$('#Reqco_GralFechaSolicitud').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0",
}).datepicker("setDate", new Date());















