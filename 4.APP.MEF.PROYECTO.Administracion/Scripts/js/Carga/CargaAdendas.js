var CargaAdendas = function () {
    var loadingOpen = function () {
        $(".container_spinner").removeClass("hide");
        $(".container_spinner").addClass("show");
    };
    var loadingClose = function () {
        $(".container_spinner").removeClass("show");
        $(".container_spinner").addClass("hide");
    };
    var eliminarAdenda = function (idCargaDetalle) {
        DESARROLLO.PROCSO_CONFIRM("¿Desea eliminar la carga seleccionada?", (rpta) => {
            if (rpta) {
                loadingOpen();
                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Carga/"
                });
            }
        });
    };

    return {

    }
}();

$("#").click(function () {

});