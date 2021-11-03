//JS General para pantallas Details


document.addEventListener('DOMContentLoaded', function () {
    Messages()
});

//window.addEventListener('DOMContentLoaded', (event) => {
//    Messages()
//});
//document.addEventListener('DOMContentLoaded', (event) => {
//    Messages()
//});
//https://developer.mozilla.org/en-US/docs/Web/API/Window/DOMContentLoaded_event

function Messages() {
    console.log('DOM fully loaded and parsed');
    //Inicializacion de la funcion
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });
    //Mensaje que mostrar segun funcion
    _Action = $('#vSwal').val();
    console.log(_Action)
    if (_Action == "Revisada") {
        Toast.fire({
            type: 'success',
            title: 'Solicitud Revisada.'
        })
    }
    else if (_Action == "Aceptada") {
        Toast.fire({
            type: 'success',
            title: 'Se ha aprobado la solicitud.'
        })
    } else if (_Action == "Rechazada") {
        Toast.fire({
            type: 'error',
            title: 'Se ha rechazado la solicitud.'
        })
    }
}


//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function (event) {
    var elements = document.body.getElementsByClassName('cTextarea');

    for (var i = 0; i < elements.length; i++) {
        var _getid = this.activeElement.id;
        var _object = document.getElementById('' + _getid + '')
        var _elementValue = _object.value;
        var _strLength = _elementValue.length;
        var _ContainsSpace = _elementValue.includes(" ");
        _object.value = _elementValue.trimStart();
        if (_ContainsSpace) {
            var startindexSpace = _elementValue.indexOf(" ");
            var startWithSpace = _elementValue.startsWith(" ");
            var lastindexSpace = _elementValue.lastIndexOf(" ");
            var _strLastChar = _elementValue.slice(-1);
            var _strEndWithSpace = _elementValue.endsWith(" ");
            if (_strEndWithSpace && event.data === _strLastChar) {
                _object.value = _elementValue.replace('  ', ' ');
                if (startWithSpace) {
                    _object.value = _elementValue.trimStart();
                }
            }
        }

        if (!/^[ a-z0-9áéíóúüñ]*$/i.test(_elementValue)) {
            _object.value = _object.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
        }
    }
});

//function NoSpace(_event) {
//    const elementst = document.querySelectorAll('cTextarea');
//    elementst.forEach(
//        console.log(this)
//    );

//}