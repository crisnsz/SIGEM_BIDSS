
var contador = 0;
 
/////DATE PICKER//////////////////////
$('#Lianvi_FechaInicioViaje,#Lianvi_FechaFinViaje,#Lianvi_FechaLiquida,#Lianvide_FechaGasto').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"


});

////////////////////////////ARCHIVO///////////////////////////////////////////




//ObtenerCampos








function GetFechas() {
    var Fechas = {
        FechaInicio: document.getElementById("Lianvi_FechaInicioViaje").value,
        FechaFin: document.getElementById("Lianvi_FechaFinViaje").value
    };
    return Fechas;
}



document.getElementById("Lianvi_FechaInicioViaje").addEventListener("blur", function () {
    CalcularFechas()

});
document.getElementById("Lianvi_FechaFinViaje").addEventListener("blur", function () {
    CalcularFechas()
});
//CalculodeFechas
function CalcularFechas() {
    vFechas = GetFechas()
    console.log("JS: " + vFechas);

    $.ajax({
        url: "/LiquidacionAnticipoViatico/CalcularFecha",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cCalFechas: vFechas }),
    })
        .done(function (data) {
            console.log(data);
         
                document.getElementById("spanFechaInicio").innerText = data.MASspan
            
       
            document.getElementById("spanFechaFin").innerText = data.MASspan1
            
        });
}



