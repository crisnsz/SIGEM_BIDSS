

$('#Inactivar').click(function () {
    insf_Id = $("#instfinanciera").val()
    $.ajax({
        url: "/InstitucionFinanciera/InactivarInstitucionFinanciera",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ insf_Id: insf_Id }),
    })
        .done(function (data) {
            $("_tbInac_Id").val("");
            window.location.href = "/institucionFinanciera/Index"
        });
});



document.getElementById('insf_Correo').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('errorcorreo');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#insf_Correo').removeClass('is-invalid');
    } else {
        valido.innerText = "Correo Inválido";
        $('#insf_Correo').focus();
        $('#insf_Correo').addClass('is-invalid');
    }
});

$('#insf_Telefono').inputmask('(999) 9999-9999')

$('#insf_Nombre').change(function () {
    $('#Nombres').hide();
});

$('#insf_Contacto').change(function () {
    $('#Contacto').hide();
});
$('#insf_Telefono').change(function () {
    $('#Telefono').hide();
});

$("#insf_Correo").keyup(function () {
    $('#correo').hide();
});

$("#frmEditInst").submit(function (event) {
    var Correo = $("#errorcorreo").text();
   
    if (Correo !== "") {
        event.preventDefault();
    }
});