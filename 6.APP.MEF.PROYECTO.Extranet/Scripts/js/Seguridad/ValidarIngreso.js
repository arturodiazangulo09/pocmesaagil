var contador_X = 0;
var validar_Coodinador = 0;
function Ingresar() {
    var url = "";
    var item =
    {
        USUARIO: $("#LOGIN").val(),
        CONTRASENA: $("#PASSWORD").val()
    };
    if ($("#DdlTipoPersona").val() == "1") {
         url = baseUrl + 'Seguridad/Validar_usuario';
    } else {
        url = baseUrl + 'Seguridad/Validar_usuario_Personal';
    }
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta.success == true) {
        setCookie('MEF-ID-U-FAGPAC', respuesta.codigo, 1);
        setCookie('MEF-TIPO-U-FAGPAC', respuesta.extra2, 1);
        if ($("#DdlTipoPersona").val() == "1") {
            if (respuesta.message == 1) {
                window.location = baseUrl + "Home/Index";
            } else {
                fn_NuevoCambioClave();
            }
        }
        else {
            if (respuesta.message == 1) {
                window.location = baseUrl + "Home/Index";
            } else {
                fn_NuevoCambioClavePersonal();
            }
        }

  

    } else {
        DESARROLLO.PROCESO_ALERT('Atención', respuesta.message);
    }
}
function validate() {
    var result = true;
    $(".form-control").each(function (index) {
        let tagId = $(this).attr('id');
        let tagError = "#" + tagId + "-error";
        if ($(this).val() == "") {
            $(tagError).removeClass('err-hide'); result = false;
        } else { $(tagError).addClass('err-hide'); }
    });
    return result;
}
function GenerarCapchaIngreso() {
    contador_X += 1;
    jQuery('#imgCapcha').attr("src", "");
    jQuery('#imgCapcha').attr("src", DESARROLLO.GENERARCAPCHALOGIN(contador_X));
}
function fn_NuevoUsuario() {
    jQuery("#modalNuevoUsuario").html('');
    jQuery("#modalNuevoUsuario").load(baseUrl + "Seguridad/NuevoUsuario", function (responseText, textStatus, request) {
        $("#modalNuevoUsuario").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_ValidarCapchaLogin() {
    $("#CapchaError").text("");
    var urls = baseUrl + 'Seguridad/ValidarCaptcha_Login?Captcha=' + $("#UCaptcha").val();
    var respuesta = DESARROLLO.Ajax(urls, null, false);
    return respuesta.success;
}
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function fn_NuevoCambioClave() {
    jQuery("#modalCambioClave").html('');
    jQuery("#modalCambioClave").load(baseUrl + "Seguridad/CambiaClave", function (responseText, textStatus, request) {
        $("#modalCambioClave").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CambioClave() {
    var item =
    {
        ID_COORDINADOR: $("#ID_COORDINADOR").val(),
        CONTRASENA: $("#TxtContra").val(),
        ACCION:"CC"
    };
    var url = baseUrl + 'Seguridad/UpdateContrasenaCoordinador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta.success == true) {
        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        jQuery("#modalCambioClave").html('');
        $('#modalCambioClave').modal('hide');
        //window.location = baseUrl + "Home/Index?ID=1";
        window.location = baseUrl + "Home/Index";
    } else {
        DESARROLLO.PROCESO_ALERT('Atención', respuesta.message);
    }
}
function fn_NuevoCambioClavePersonal() {
    jQuery("#modalCambioClave").html('');
    jQuery("#modalCambioClave").load(baseUrl + "Seguridad/CambiaClavePersonal", function (responseText, textStatus, request) {
        $("#modalCambioClave").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CambioClavePersonal() {
    var item =
    {
        ID_PERSONAL: $("#ID_PERSONAL").val(),
        CONTRASENA: $("#TxtContra").val(),
        ACCION: "CC"
    };
    var url = baseUrl + 'Seguridad/UpdateContrasenaPersonal';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta.success == true) {
        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        jQuery("#modalCambioClave").html('');
        $('#modalCambioClave').modal('hide');
        //window.location = baseUrl + "Home/Index?ID=2";
        window.location = baseUrl + "Home/Index";
    } else {
        DESARROLLO.PROCESO_ALERT('Atención', respuesta.message);
    }
}
function fn_RecuperarClave_Coodinador() {
    jQuery("#modalCambioClave").html('');
    jQuery("#modalCambioClave").load(baseUrl + "Seguridad/restablecerClaveCoordinador", function (responseText, textStatus, request) {
        $("#modalCambioClave").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}


function fn_RecuperarClave() {
    jQuery("#modalCambioClave").html('');
    jQuery("#modalCambioClave").load(baseUrl + "Seguridad/restablecerClave", function (responseText, textStatus, request) {
        $("#modalCambioClave").modal('show');
    });
}
function Fun_ValidarCoodinadorEmail() {
    $.ajax({
        url: baseUrl + "Seguridad/ListaDetalleCoordinador?Correo=" + $("#TxtEmail").val() + "&usuario=" + $("#TxtUsuario").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (p) {
            if (p.ACCION == '1') {
                fn_RestablecerCoodinador(p.ID_COORDINADOR);
            } else {
                DESARROLLO.PROCESO_ERROR("No se encontró correo asociado, está tratando recuperar la clave del coordinador de una entidad");
                //console.log("El correo no se encuentra registrado, está tratando recuperar la clave del coordinador de una entidad");
            }
        }
    });

}
function Validate_Restablecer_C() {
    var result = true;
    if ($("#TxtEmail").val() == "") { $("#TxtEmail").addClass('is-invalid'); result = false; } else { $("#TxtEmail").removeClass('is-invalid'); }
    if ($("#TxtUsuario").val() == "") { $("#TxtUsuario").addClass('is-invalid'); result = false; } else { $("#TxtUsuario").removeClass('is-invalid'); }
    return result;
}
function fn_RestablecerCoodinador(ID) {
    var item =
    {
        ID_COORDINADOR: ID,
    };
    var url = baseUrl + 'Seguridad/UpdateRecuperarClaveCoordinador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta.success == true) {
        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        jQuery("#modalCambioClave").html('');
        $('#modalCambioClave').modal('hide');
    } else {
        DESARROLLO.PROCESO_ERROR('Atención', respuesta.message);
    }
}
function ValidTipoUser() {
    var result = true;
    $("label.otp-required").removeClass('otp-required');
    if ($("#DdlTipoPersona").val() == "") { $("#lblTipoPersona").addClass('opt-required'); result = false; } else { $("#lblTipoPersona").removeClass('opt-required'); }
    return result;
}
function Validate_Restablecer_P() {
    var result = true;
    if ($("#TxtEmail_Per").val() == "") { $("#TxtEmail_Per").addClass('is-invalid'); result = false; } else { $("#TxtEmail_Per").removeClass('is-invalid'); }
    return result;
}
function Fun_ValidarPersonalEmail() {
    $.ajax({
        url: baseUrl + "Seguridad/ListaDetallePersonal?Correo=" + $("#TxtEmail_Per").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (p) {
            if (p.ACCION == '1') {
                fn_RestablecerPersonal(p.ID_PERSONAL);
            } else {
                DESARROLLO.PROCESO_ERROR("No se encontró correo asociado, está tratando recuperar la clave del Profesional FAG / PAC");
                //console.log("El correo no se encuentra registrado, está tratando recuperar la clave del coordinador de una entidad");
            }
        }
    });

}
function fn_RestablecerPersonal(ID) {
    var item =
    {
        ID_PERSONAL: ID,
    };
    var url = baseUrl + 'Seguridad/UpdateRecuperarClavePersonal';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta.success == true) {
        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        jQuery("#modalCambioClave").html('');
        $('#modalCambioClave').modal('hide');
    } else {
        DESARROLLO.PROCESO_ERROR('Atención', respuesta.message);
    }
}
function fn_RecuperarClave_Personal() {
    jQuery("#modalCambioClave").html('');
    jQuery("#modalCambioClave").load(baseUrl + "Seguridad/restablecerClavePersonal", function (responseText, textStatus, request) {
        $("#modalCambioClave").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}

function toggleInputType(element) {
    let eyeIcon = $(element).find('i.fa')
    if ($(eyeIcon).hasClass("fa-eye")) { $(eyeIcon).removeClass('fa-eye').addClass('fa-eye-slash'); }
    else { $(eyeIcon).removeClass('fa-eye-slash').addClass('fa-eye'); }

    let target = $(element).data('target');
    let inputId = $('#' + target);
    let type = $(inputId).attr('type') === "password" ? 'text' : 'password';
    $(inputId).attr('type', type);
}

function validateInput(element) {
    let tagId = $(element).attr('id');
    let tagError = "#" + tagId + "-error";
    if ($(element).val() == "") {
        $(tagError).removeClass('err-hide'); result = false;
    } else { $(tagError).addClass('err-hide'); }
}