$(document).ready(function () {
    showSpinner(3000);
    if ($("#inputHddipo").val() == "2") {
        if ($("#inputHdDatosPer").val() != "1") {
            DESARROLLO.PROCESO_ALERT("Recuerde siempre tener sus datos actualizados </br>" + "<i class='fas fa-user-edit'></i>");
            cargarVista(baseUrl + "Usuario/Personal/MisDatos?ID_PERSONAL=" + $("#inputHddIdUsuarioSesion").val());
        }
        cargarVista(baseUrl + "Usuario/Personal/Inicio/");
    } else {
        cargarVista(baseUrl + "Home/Presentacion")
    }
});
//USUARIO COODINADO
var rutoInicio = baseUrl + "Home/Presentacion/";
var rutaFag = baseUrl + "Coordinador/ProcesoFag/Index/";
var rutaPac = baseUrl + "Coordinador/ProcesoPac/Index/";
var rutaSolicitudPago = baseUrl + "Coordinador/SolicitudPago/Index/";
var rutaSolicitudAdenda = baseUrl + "Coordinador/Adenda/Index/";
var rutaAltasBajas = baseUrl + "Coordinador/AltasBajas/Index/";
var rutaSuspension = baseUrl + "Coordinador/Suspension/Index/";
var rutaCovid = baseUrl + "Coordinador/Covid/Index/";
var rutaDatosPersonales = baseUrl + "Coordinador/Solicitud/DatosPersonales/";
jQuery('#aInicio').click(function (e) {
    var item =
    {
    };
    cargarVista(rutoInicio, item);
});
jQuery('#aFag').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaFag, item);
});
jQuery('#aPac').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaPac, item);
});
jQuery('#aSolicitudPago').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaSolicitudPago, item);
});
jQuery('#aAdenda').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaSolicitudAdenda, item);
});
jQuery('#aAltasBajas').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaAltasBajas, item);
});
jQuery('#aSolicitudSuspension').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaSuspension, item);
});
jQuery('#aSolicitudDescansoCovid').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaCovid, item);
});
jQuery('#aDatosEntidad').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaDatosPersonales, item);
});
//USUARIO PERSONAL
var rutaDatos = baseUrl + "Usuario/Personal/MisDatos?ID_PERSONAL=" + $("#inputHddIdUsuarioSesion").val();
var rutaInicioCon = baseUrl + "Usuario/Personal/Inicio/";
var rutaCv = baseUrl + "Usuario/Personal/MiCv/";
var rutaPostulaciones = baseUrl + "Usuario/Personal/MisPostulaciones/";



jQuery('#aInicioCon').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaInicioCon, item);
    cargarVista(rutaInicioCon, item);
});

jQuery('#aMisdatos').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaDatos, item);
});
jQuery('#aMicurriculum').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaCv, item);
});
jQuery('#aMisPostulaciones').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaPostulaciones, item);
});
function cargarVista(url) {
    $.ajax({
        url: url,
        dataType: 'html',
        type: 'get',
        contentType: 'text/xml; charset=utf-8',
        success: function (result) {
            $('#divProceso').html("");
            $('#divProceso').html(result);
        }
    });
}
function Inicio() {
    window.location = baseUrl + "Seguridad/LoGout";//   ?CODIGO=" + respuesta.codigo;
}

function showSpinner(time) {
    $('#loadingModal').modal('show');

    // Hide the modal after 3 seconds (for demonstration purposes)
    setTimeout(function () {
        $('#loadingModal').modal('hide');
    }, time);
}