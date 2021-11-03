/////////AÑADIR MUNICIPIO

contador = 0;
$('#AgregarMunicipiosEdit').click(function () {
    var MunCodigo = $('#mun_CodigoEdit').val();
    var MunNombre = $('#mun_NombreEdit').val();


    if (MunCodigo == '') {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunCodigoEdit').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
    }
    else if (MunCodigo.length < 4) {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunCodigoEdit').after('<ul id="CodigoError" class="validation-summary-errors text-danger">No se Aceptan menos de 4 números.</ul>');
    }
    else if (MunNombre == '') {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunNombreEdit').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
    }
    else if (MunNombre.substring(0, 1) == " ") {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunNombreEdit').after('<ul id="NombreError" class="validation-summary-errors text-danger">No se Aceptan Espacios en blanco.</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'MunCodigo'>" + $('#mun_CodigoEdit').val() + "</td>";
        copiar += "<td id = 'MunNombre'>" + $('#mun_NombreEdit').val() + "</td>";
        copiar += "<td>" + '<button id="removeMunicipiosEdit" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#tblMunicipiosEdit').append(copiar);

        var Municipio = {
            mun_Codigo: $('#mun_CodigoEdit').val(),
            mun_Nombre: $('#mun_NombreEdit').val(),
            mun_UsuarioCrea: contador,
        };
        $.ajax({
            url: "/Departamento/AnadirMunicipio",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Municipio: Municipio }),
        })
            .done(function (data) {
                $('#ValidationCreate').text('');
                $('#mun_CodigoEdit').val('');
                $('#mun_NombreEdit').val('');
                $('#CodigoError').text('');
                $('#NombreError').text('');
            });
    }

});

///////GuardarMunicipio
$('#btnNuevoEdit').click(function () {
    var depCodigo = $('#dep_Codigo').val();
    $.ajax({
        url: "/Departamento/GuardarMuninicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ depCodigo: depCodigo }),
    })
        .done(function (data) {
            if (data == '-1') {
                $('#ValidationCreate').text('');
                $('#ValidationCreate_').after('<ul id="ValidationCreate" class="validation-summary-errors text-danger">No se pudo guardar el registro, contacte al administrador</ul>');
            }
            else {
                window.location.href = '/Departamento/Edit/' + depCodigo;
            }
        })
});


//Edit Municipios en Edit Departamento
function EditMunicipioRecord(mun_Codigo) {
    $.ajax({
        url: "/Departamento/getMunicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ munCodigo: mun_Codigo }),

    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#mun_CodigoEdit_").val(item.mun_Codigo);
                    $("#mun_NombreEdit_").val(item.mun_Nombre);
                    $("#EditMunicipioModal").modal();
                })
            }
        })
}
////////actualizarmunicipio
$("#BtnsubmitMunicipio").click(function () {
    var depCodigo = $('#dep_Codigo').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Departamento/ActualizarMunicipio",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                window.location.href = '/Departamento/Edit/' + depCodigo;
        }
    });
})