$(function () {
    $("#btnGrabarSalario").click(function () {
        DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Actualizar</b> el registro seleccionado? </br>", function (r) {
            if (r) {
                grabarMantenimientoSalario();
            }
        });
    });
    //Validando numeros
    $(".integer_input").on('keypress', function (e) {
        DESARROLLO.SOLONUMEROS(e);
    });
    //Validando decimales
    $(".decimal_input").on('keypress', function (e) {
        DESARROLLO.NUMERODECIMAL(e);
    });
})

function loadingOpen(mensaje) {
    $(".container_spinner").removeClass("hide");
    $("#mensaje_spinner").text(mensaje);
    $(".container_spinner").addClass("show");
};
function loadingClose() {
    $(".container_spinner").removeClass("show");
    $(".container_spinner").addClass("hide");
};

function grabarMantenimientoSalario() {

    loadingOpen();

    var entidad = {
        ID_REGISTRO: $("#txtIdRegistro").val(),
        PERIODO: $("#DdlPeriodoPagoModal").val(),
        COD_CONTRATO: $("#TXT_COD_CONTRATO").val(),
        NUM_DOCUMENTO: $("#TXT_NUM_DOCUMENTO").val(),
        RUC_CAS: $("#TXT_RUC_CAS").val(),
        NOM_DEPENDENCIA: $("#DdlEntidadModal").val(),
        ING_ENE: $("#TXT_ING_ENE").val(),
        EGR_ENE: $('#TXT_EGR_ENE').val(),
        EMP_ENE: $('#TXT_EMP_ENE').val(),
        NET_ENE: $('#TXT_NET_ENE').val(),
        ING_FEB: $("#TXT_ING_FEB").val(),
        EGR_FEB: $('#TXT_EGR_FEB').val(),
        EMP_FEB: $('#TXT_EMP_FEB').val(),
        NET_FEB: $('#TXT_NET_FEB').val(),
        ING_MAR: $("#TXT_ING_MAR").val(),
        EGR_MAR: $('#TXT_EGR_MAR').val(),
        EMP_MAR: $('#TXT_EMP_MAR').val(),
        NET_MAR: $('#TXT_NET_MAR').val(),
        ING_ABR: $("#TXT_ING_ABR").val(),
        EGR_ABR: $('#TXT_EGR_ABR').val(),
        EMP_ABR: $('#TXT_EMP_ABR').val(),
        NET_ABR: $('#TXT_NET_ABR').val(),
        ING_MAY: $("#TXT_ING_MAY").val(),
        EGR_MAY: $('#TXT_EGR_MAY').val(),
        EMP_MAY: $('#TXT_EMP_MAY').val(),
        NET_MAY: $('#TXT_NET_MAY').val(),
        ING_JUN: $("#TXT_ING_JUN").val(),
        EGR_JUN: $('#TXT_EGR_JUN').val(),
        EMP_JUN: $('#TXT_EMP_JUN').val(),
        NET_JUN: $('#TXT_NET_JUN').val(),
        ING_JUL: $("#TXT_ING_JUL").val(),
        EGR_JUL: $('#TXT_EGR_JUL').val(),
        EMP_JUL: $('#TXT_EMP_JUL').val(),
        NET_JUL: $('#TXT_NET_JUL').val(),
        ING_AGO: $("#TXT_ING_AGO").val(),
        EGR_AGO: $('#TXT_EGR_AGO').val(),
        EMP_AGO: $('#TXT_EMP_AGO').val(),
        NET_AGO: $('#TXT_NET_AGO').val(),
        ING_SET: $("#TXT_ING_SET").val(),
        EGR_SET: $('#TXT_EGR_SET').val(),
        EMP_SET: $('#TXT_EMP_SET').val(),
        NET_SET: $('#TXT_NET_SET').val(),
        ING_OCT: $("#TXT_ING_OCT").val(),
        EGR_OCT: $('#TXT_EGR_OCT').val(),
        EMP_OCT: $('#TXT_EMP_OCT').val(),
        NET_OCT: $('#TXT_NET_OCT').val(),
        ING_NOV: $("#TXT_ING_NOV").val(),
        EGR_NOV: $('#TXT_EGR_NOV').val(),
        EMP_NOV: $('#TXT_EMP_NOV').val(),
        NET_NOV: $('#TXT_NET_NOV').val(),
        ING_DIC: $("#TXT_ING_DIC").val(),
        EGR_DIC: $('#TXT_EGR_DIC').val(),
        EMP_DIC: $('#TXT_EMP_DIC').val(),
        NET_DIC: $('#TXT_NET_DIC').val(),
        ING_TOT: $("#TXT_ING_TOT").val(),
        EGR_TOT: $('#TXT_EGR_TOT').val(),
        EMP_TOT: $('#TXT_EMP_TOT').val(),
        NET_TOT: $('#TXT_NET_TOT').val()
    }

    $.ajax({
        type: 'POST',
        url: baseUrl + "Carga/Carga/EditarCargaSalario",
        data: JSON.stringify(entidad),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result) {
                jQuery("#modalCargaSalario").html('');
                $("#modalCargaSalario").modal("hide");
                DESARROLLO.PROCESO_CORRECTO("Registro Actualizado Correctamente");
            } else {
                DESARROLLO.PROCESO_ERROR("No se actualizó el registro");
            }
            
            var text = $("#DcboCargas").find("option:selected").html();
            const myArray = text.split("-");
            var tipoDoc = myArray[1];
            var idCarga = $("#DcboCargas").val();

            window.parent.Carga.CargarGrillaCargaCabDet(idCarga, tipoDoc, false);

            loadingClose();
            jQuery("#modalCargaSalario").html('');
            $('#modalCargaSalario').modal('hide');
        }
    })
}