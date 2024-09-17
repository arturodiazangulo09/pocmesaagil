var ContratosMantenimientoAdendas = function () {
    var Iniciar = function () {

    };
    var loadingOpen = function () {
        $(".container_spinner").removeClass("hide");
        $(".container_spinner").addClass("show");
    };
    var loadingClose = function () {
        $(".container_spinner").removeClass("show");
        $(".container_spinner").addClass("hide");
    };
    var guardar = function () {
        DESARROLLO.PROCESO_CONFIRM("¿Desea actualizar la adenda?", (rpta) => {

            if (rpta) {
                loadingOpen();

                var entidad = {
                    "ID_CARGA_ADENDA": $("#txtIdCargaDetalle").val(),
                    "ID_CARGA": $("#txtIdCarga").val(),
                    "COD_CONTRATO": $("#txtCodigoContrato").val(),
                    "DOC_SOLICITUD": $("#txtDocumentoSolicitud").val(),
                    "HOJA_RUTA": $("#txtHojaRuta").val(),
                    "FEC_SUSCRIPCION": $("#txtFechaSuscripcion").val(),
                    "NUMERO": $("#txtNumero").val(),
                    "FEC_INICIO": $("#txtFechaInicio").val(),
                    "FEC_FIN": $("#txtFechaCulminacion").val(),
                    "FEC_RECEPCION": $("#txtFechaRecepcion").val(),
                    "DOCUMENTO": $("#txtDocumento").val(),
                };

                $.ajax({
                    type: 'POST',
                    url: baseUrl + 'Carga/Contrato/EditarAdenda',
                    data: JSON.stringify({
                        idAdenda: $("#txtIdCargaDetalle").val(), adenda: entidad
                    }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {

                        if (!result.Error) {
                            loadingClose();
                            DESARROLLO.PROCESO_CORRECTO("Registro actualizado correctamente");

                            $("#modalMantenimientoAdendas").modal("hide");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se actualizó el registro");
                            loadingClose();
                        }
                    }
                });

            }

        });
    };

    return {
        Guardar: function () { guardar(); }
    }
}();

$("#btnGrabarAdenda").click(function () {
    ContratosMantenimientoAdendas.Guardar();
});