var CargaDetalle = function () {
    var loadingOpen = function (mensaje) {
        $(".container_spinner").removeClass("hide");
        $("#mensaje_spinner").text(mensaje);
        $(".container_spinner").addClass("show");
    };
    var loadingClose = function () {
        $(".container_spinner").removeClass("show");
        $(".container_spinner").addClass("hide");
    };
    var formatoFecha = function (valor) {
        if (valor != "" && valor != null) {
            let f = valor.toString().split('-');
            return f[2] + "/" + f[1] + "/" + f[0] + " 00:00:00";
        }
        return null;
    };
    var grabarMantenimientoDetalle = function () {

        DESARROLLO.PROCESO_CONFIRM("¿Desea Actualizar el registro?", (rpta) => {

            if (rpta) {

                loadingOpen();

                var entidad = {
                    ID_CARGA_DETALLE: $("#txtIdCargaDetalle").val(),
                    ID_CARGA: $("#txtIdCarga").val(),
                    COD_CONTRATO: $("#txtCodigoContrato").val(),
                    DOCUMENTO: $("#txtDocumento").val(),
                    DOC_SOLICITUD: $("#txtDocumentoSolicitud").val(),
                    FEC_FIN: formatoFecha($("#txtFechaCulminacion").val()),
                    FEC_INICIO: formatoFecha($("#txtFechaInicio").val()),
                    FEC_RECEPCION: formatoFecha($("#txtFechaRecepcion").val()),
                    FEC_SUSCRIPCION: formatoFecha($("#txtFechaSuscripcion").val()),
                    HOJA_RUTA: $("#txtHojaRuta").val(),
                    NUMERO: $("#txtNumero").val()
                }

                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Carga/EditarCargaDetalle",
                    data: JSON.stringify(entidad),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result) {
                            $("#modalCargaDetalle").modal("hide");
                            DESARROLLO.PROCESO_CORRECTO("Adenda Actualizada Correctamente");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se actualizó la Adenda");
                        }

                        loadingClose();
                    }
                })

            }

        });
        
    }
   

    return {
        grabarMantenimientoDetalle: function () { grabarMantenimientoDetalle(); }
    }

}();

$("#btnGrabar").click(function () {
    CargaDetalle.grabarMantenimientoDetalle();
});