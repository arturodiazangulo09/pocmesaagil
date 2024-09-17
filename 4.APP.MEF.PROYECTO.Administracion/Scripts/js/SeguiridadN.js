function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
$(document).ready(function () {
    cargarVista(baseUrl + "Home/Presentacion")
    ListarMenu();
});
/*TABLAS MAESTRAS*/
var rutaEntidad = baseUrl + "Administracion/Entidad/Index/";
var rutaCoordinador = baseUrl + "Administracion/Coordinadores/Index/";
var rutaInformacionMef = baseUrl + "Administracion/Entidad/DatosMef/";
var rutaEvaluadorEntidad = baseUrl + "Administracion/Entidad/EvaluadorEntidad/";
jQuery('#aEntidades').click(function (e) {
    
    var item =
    {
    };
    cargarVista(rutaEntidad, item);
});
jQuery('#aCoordinadores').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaCoordinador, item);
});
jQuery('#aInformacionMef').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaInformacionMef, item);
});
jQuery('#aEvaluadorEntidad').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaEvaluadorEntidad, item);
});
/*SOLICITUDES*/
var rutaSolicitud = baseUrl + "Solicitudes/Solicitudes/Index/";
var rutaConsultores = baseUrl + "Solicitudes/Consultores/Index/";
var rutaExpedienteDigital = baseUrl + "Solicitudes/ExpedienteDigital/Index/";
jQuery('#aSolicitudes').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaSolicitud, item);
});
jQuery('#aConsultores').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaConsultores, item);
});
jQuery('#aExpedienteD').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaExpedienteDigital, item);
});
/*REPORTES*/
var rutaAltasBajas = baseUrl + "AltasBajas/AltasBajas/Index/";
var rutaPdt = baseUrl + "AltasBajas/Pdt/Index/";
var rutaPlanilla = baseUrl + "AltasBajas/Planilla/Index/";
jQuery('#aAltasBajas').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaAltasBajas, item);
});
jQuery('#aGpdt').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaPdt, item);
});
jQuery('#aPlanilla').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaPlanilla, item);
});
/*CARGA*/
var rutaCarga = baseUrl + "Carga/Carga/Index/";
var rutaContratos = baseUrl + "Carga/Contrato/Index/";


jQuery('#aCargaDatos').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaCarga, item);
});

jQuery('#aContratos').click(function (e) {
    var item =
    {
    };
    cargarVista(rutaContratos, item);
});
function ValidarIngreso(Validar) {
    
    var item =
    {
    };
    switch (Validar) {
        case "aEntidades": cargarVista(rutaEntidad, item); break;
        case "aCoordinadores": cargarVista(rutaCoordinador, item); break;
        case "aInformacionMef": cargarVista(rutaInformacionMef, item); break;
        case "aEvaluadorEntidad": cargarVista(rutaEvaluadorEntidad, item); break;
        case "aSolicitudes": cargarVista(rutaSolicitud, item); break;
        case "aConsultores": cargarVista(rutaConsultores, item); break;
        case "aExpedienteD": cargarVista(rutaExpedienteDigital, item); break;
        case "aAltasBajas": cargarVista(rutaAltasBajas, item); break;
        case "aGpdt": cargarVista(rutaPdt, item); break;
        case "aPlanilla": cargarVista(rutaPlanilla, item); break;
        case "aCargaDatos": cargarVista(rutaCarga, item); break;
        case "aContratos": cargarVista(rutaContratos, item); break;
    }
}
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
    window.location = baseUrl + "Seguridad/LoGout";
}
function ListarMenu() {
    var urlListar = baseUrl + 'Home/Modulos_Listar?CODIGO=' + $("#inputHddTkn").val() + "&ID_PER=" + $("#inputHddPer").val();
    $.ajax({
        url: urlListar,
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (response) {
            var detalle = response;
            if (detalle.extra != '0') {
                $('#navbarCollapse').html('');
                $('#navbarCollapse').append(detalle.extra);

            }

        }
    });
}
//////////////////////********** INICIO LOGIN **********/////////////
function GenerarCapchaIngreso() {
    contador += 1;
    jQuery('#imgCapcha').attr("src", "");
    jQuery('#imgCapcha').attr("src", DESARROLLO.GENERARCAPCHALOGIN(contador));
}
function Ingresar() {
    
    var item =
    {
        USUARIO: $("#LOGIN").val(),
        PASSWORD: $("#PASSWORD").val()
    };
    var url = baseUrl + 'Seguridad/Validar_usuario';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta.success == true) {
        setCookie('MEF-TOKEN-MIGUEL', respuesta.TOKEN, 1);
        window.location = baseUrl + "Home/Index";
    }
    else {
        DESARROLLO.PROCESO_ALERT('Atención', respuesta.codigo);
        ni += 1;
    }
}
function fn_ValidarCapchaLogin() {
    $("#CapchaError").text("");
    var urls = baseUrl + 'Seguridad/ValidarCaptcha_Login?Captcha=' + $("#UCaptcha").val();
    var respuesta = DESARROLLO.Ajax(urls, null, false);
    return respuesta.success;
}
function validate() {
    var result = true;
    if ($("#LOGIN").val() == "") {
        $("#LOGIN").addClass('is-invalid'); result = false;
    } else { $("#LOGIN").removeClass('is-invalid'); }

    if ($("#PASSWORD").val() == "") {
        $("#PASSWORD").addClass('is-invalid'); result = false;
    } else { $("#PASSWORD").removeClass('is-invalid'); }
    return result;
}
//////////////////////********** FIN LOGIN **********/////////////
