//JS de Anticipo de Salario(Index)
//Details Anticipo Salario
$(function () {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false
    });
})


$(document).on("click", "#Approve", function () {
    idItem = $('#Ansal_Id').val();
});
$(document).on("click", "#Reject", function () {
    idItem = $('#Ansal_Id').val();
});




//---------------------Rechazar-----------------------------------------
$(document).on("click", "#_ModalReject", function () {
    var _spanRR = document.getElementById("RazonRechazo")
    console.log(_spanRR.value)

    //    
    if (_spanRR.value == "" || _spanRR.value == null) {
        document.getElementById("spanRazonRechazo").innerText = "Razon de Rechazo Requerida";
    }
    else {
        document.getElementById("spanRazonRechazo").innerText = "";
        SendData(_idPrimary = "Ansal_Id", _controller = "AnticipoSalario", _action = "Reject", _spinnerbody = "spinner-body-reject", _spinner = "spinnerd-reject");
    }
});

$(document).on("click", "#_ModalApprove", function () {
    SendData(_idPrimary = "Ansal_Id", _controller = "AnticipoSalario", _action = "Approve", _spinnerbody = "spinner-body", _spinner = "spinnerd");
});


$("#RazonRechazo").change(function () {
    var RazonRechazo = $("#RazonRechazo").val();
    if (RazonRechazo !== "") {
        $("#spanRazonRechazo").text("");
    }
});




//<---------------------/Rechazar/----------------------------------------->//






//---------------------Approve-----------------------------------------
//<---------------------/Approve/----------------------------------------->//



//---------------------------------------------------------------//
//                  Funcion Ejecutar Estado                     //

function SendData(_idPrimary, _controller, _action, _spinnerbody, _spinner) {
    var _Id = $("#" + _idPrimary + "").val(),
        RazonRechazo = $('#RazonRechazo').val();
    var _url = "/" + _controller + "/" + _action;
    console.log(" || _idPrimary: " + _idPrimary + " || _controller: " + _controller + " || _Id: " + _Id + " || _action: " + _action + " || _url: " + _url + " || _spinnerbody: " + _spinnerbody + " || _spinner: " + _spinner);

    document.getElementById("" + _spinnerbody + "").classList.add("overlay");
    document.getElementById("" + _spinner + "").removeAttribute("hidden");

    $.ajax({
        url: _url,
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: _Id, RazonRechazo: RazonRechazo }),
    }).done(function (data) {
        var str = data.toString();
        if (!str.startsWith("-1")) {
            console.log(str + " || " + data);
            location.reload();
        }
        else {
            console.log("false || " + str);
        }
    }).fail(function (xhr, a, error) {
        console.log("error", error);
        Toast.fire({
            type: 'error',
            title: 'Error al actualizar el estado.'
        })
    })
}

//                                                                //
//---------------------------------------------------------------//


