function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}

function noespaciosincio(e) {
    var valor = e.value.replace(/^ */, '');
    e.value = valor;
}

function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9]+$/.test(tecla);
}



$('#Inactivar').click(function () {
    var prod_Id = $('#prod_Id').val();
    var Activo = 0
    var Razon_Inactivacion = $('#razonInac').val();
    console.log(prod_Codigo)
    console.log(Activo)
    console.log(Razon_Inactivacion)
    if (Razon_Inactivacion == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón inactivación es requerida";
    }
    else {
        $.ajax({
            url: "/Producto/EstadoInactivar",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prod_Id: prod_Id, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion }),

        })
            .done(function (data) {
                if (data.length > 0) {
                    var url = $("#RedirectTo").val();
                    location.reload();
                }
                else {
                    alert("Registro No Actualizado");
                }
            });
    }

})

const _id = document.getElementById('razonInac');
_id.addEventListener("input", function () {
    this.value = this.value.trimStart()
    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(_id.value)) {
        this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
    }
})

$('#prod_Codigo').keyup(function () {
    $('#codigo').hide();
});

$('#prod_CodigoBarras').keyup(function () {
    $('#barras').hide();
});

$('#prod_Descripcion').keyup(function () {
    $('#descripcion').hide();
});


$('#prod_Descripcion').keyup(function () {
    $('#descripcion').hide();
});


$('#prod_Marca').keyup(function () {
    $('#marca').hide();
});
////////////


$('#prod_Talla').keyup(function () {
    $('#talla').hide();
});

$('#prod_Color').keyup(function () {
    $('#prod_Color').hide();
});

$('#uni_Id').keyup(function () {
    $('#medida').hide();
});

$('#prov_Id').keyup(function () {
    $('#proveedor').hide();
});
