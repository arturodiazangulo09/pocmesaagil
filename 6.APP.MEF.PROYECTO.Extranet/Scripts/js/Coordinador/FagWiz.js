
function Fun_ValidarPersonaDni() {
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/ListaDetallePersona?dni=" + $("#NUM_DOCUMENTO_CONSULTOR").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (p) {
            if (p.ACCION == '1') {
                if (p.APELLIDO_PATERNO != "-") {
                    $("#APELLIDO_PAT_CONSULTOR").attr('disabled', 'disabled');
                    $("#APELLIDO_MAT_CONSULTOR").attr('disabled', 'disabled');
                    $("#NOMBRES_CONSULTOR").attr('disabled', 'disabled');
                    $("#APELLIDO_PAT_CONSULTOR").val(p.APELLIDO_PAT_CONSULTOR);
                    $("#APELLIDO_MAT_CONSULTOR").val(p.APELLIDO_MAT_CONSULTOR);
                    $("#NOMBRES_CONSULTOR").val(p.NOMBRES_CONSULTOR);
                } else {
                    $("#NUM_DOCUMENTO_CONSULTOR").val("");
                    $("#APELLIDO_PAT_CONSULTOR").val("");
                    $("#APELLIDO_MAT_CONSULTOR").val("");
                    $("#NOMBRES_CONSULTOR").val("");
                    DESARROLLO.PROCESO_ALERT('Atención', 'El número de documento no es válido.');
                }
                $("#myModalCargando").css("display", "none");
            } else {
                $("#APELLIDO_PAT_CONSULTOR").removeAttr('disabled');
                $("#APELLIDO_MAT_CONSULTOR").removeAttr('disabled');
                $("#NOMBRES_CONSULTOR").removeAttr('disabled');
                DESARROLLO.PROCESO_ALERT('Atención', 'Problemas al consultar el número de documento, ingrese su información manualmente.');
                console.log(p.DES_ERROR);
                $("#myModalCargando").css("display", "none");
            }
        }
    });

}
function Validar_Designado_V2() {
    if ($("#FLG_CHECK_DESIGNADO").is(":checked")) {
        $('.DESG').show();

    } else {
        $('.DESG').hide();
        $('#NR_RESOLUCION').val("");
        $('#FechaAsignacion').val("");
    }

}