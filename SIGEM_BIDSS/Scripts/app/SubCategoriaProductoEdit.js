//$(document).on("change", "#pcat_Id", function () {
//    GetSubCategoriaProducto();
//});

//function GetSubCategoriaProducto() {
//    var CodCategoria = $('#pcat_Id').val();
//    $.ajax({
//        url: "/Producto/GetSubCategoriaProducto",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ CodCategoria: CodCategoria }),
//    })
//        .done(function (data) {
//            if (data.length > 0) {
//                $('#pscat_Id').empty();
//                $('#pscat_Id').append("<option value=''>Seleccione SubCategoria</option>");
//                $.each(data, function (key, val) {
//                    $('#pscat_Id').append("<option value=" + val.pscat_Id + ">" + val.pscat_Descripcion + "</option>");
//                });
//                $('#pscat_Id').trigger("chosen:updated");
//            }
//            else {
//                $('#pscat_Id').empty('');
//                $('#pscat_Id').append("<option value=''>Sin SubCategoria</option>");
//            }
//        });
//}