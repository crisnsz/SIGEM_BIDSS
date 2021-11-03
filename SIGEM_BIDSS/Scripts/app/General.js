//JS General para Pantalla Index

$(function () {
    //Inicializacion de la funcion
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });




    function noespaciosincio(e) {
        var valor = e.value.replace(/^ */, '');
        e.value = valor;
    }



    //Funcion no aceptar espacios en el textbox
    document.addEventListener("input", function () {
        $("textarea", 'body').each(function (e) {
            var _id = $(this).attr("id");
            _value = document.getElementById(_id).value;
            _value = _value.trimStart();

        });
        $(".normalize", 'body').each(function (e) {
            if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
                this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
            }
        });


    });



    //Mensaje que mostrar segun funcion
    _Action = $('#vSwal').val();
    console.log(_Action)
    if (_Action == "Created") {
        Toast.fire({
            type: 'success',
            title: 'Se ha guardado con Éxito.'
        })
    }
    else if (_Action == "Edited") {
        Toast.fire({
            type: 'success',
            title: 'Se ha actualizado con Éxito.'
        })
    }
    else if (_Action == "Delete") {
        Toast.fire({
            type: 'warning',
            title: 'Se ha borrado con Éxito.'
        })
    }
    else if (_Action == "Inactiva") {
        Toast.fire({
            type: 'warning',
            title: 'Se ha Inactivado con Éxito.'
        })
    }
    else if (_Action == "Activa") {
        Toast.fire({
            type: 'success',
            title: 'Se ha Activado con Éxito.'
        })
    }




    else if (_Action == "Dependencias") {
        Toast.fire({
            type: 'warning',
            title: 'Este municipio está siendo usado por otra tabla, no se puede Eliminar.'
        })
    }
    else if (_Action == "DependenciaCate") {
        Toast.fire({
            type: 'warning',
            title: 'Esta Categoria está siendo usado por otra tabla, no se puede Eliminar.'
        })
    }
    else if (_Action == "DependenciaSubCate") {
        Toast.fire({
            type: 'warning',
            title: 'Esta SubCategoria está siendo usado por otra tabla, no se puede Eliminar.'
        })
    }
    else if (_Action == "DependenciaCateIn") {
        Toast.fire({
            type: 'warning',
            title: 'Esta Categoria está siendo usado por otra tabla, no se puede Inactivar.'
        })
    }
    else if (_Action == "DependenciaSubCateIn") {
        Toast.fire({
            type: 'warning',
            title: 'Esta SubCategoria está siendo usado por otra tabla, no se puede Inactivar.'
        })
    }
    else if (_Action == "ExisteCo") {
        Toast.fire({
            type: 'warning',
            title: 'Este nombre ya éxiste, por favor revise la tabla de Municipios.'
        })
    }
    else if (_Action == "ExisteNom") {
        Toast.fire({
            type: 'warning',
            title: 'Este nombre ya éxiste, por favor revise la tabla de Municipios.'
        })
    }
    else if (_Action == "Rechazada") {
        Toast.fire({
            type: 'success',
            title: 'Se ha rechazado con Éxito.'
        })
    }
});
