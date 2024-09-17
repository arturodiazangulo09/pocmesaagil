var contadorN = 0;
var contadorDNI = 0;
function RegistroContacto() {
    var cantidad = $('#tabla_DatosContac >tbody >tr').length + 1;
    $("#tabla_DatosContac tbody").append("<tr id='" + cantidad + "'> <td style='text-align:center'>" + cantidad + "</td> <td style='text-align:center'>" + $('#txtTelefono').val() + "</td>  <td style='text-align:center'>" + $('#txtAnexo').val() + "</td>  <td style='text-align:center'>" + $('#txtCelular').val() + "</td> <td style='text-align:left'>" + $('#txtCorreo').val() + "</td> <td style='text-align:center'><a onclick='eliminar_contacto(" + cantidad + ");' href='javascript:void(0)'  title='Eliminar Contacto'><i class='fas fa-trash-alt' style='color:red'></i></a> </td></tr>");
    $("#txtTelefono").val("");
    $("#txtAnexo").val("");
    $("#txtCelular").val("");
    $("#txtCorreo").val("");   
};
function eliminar_contacto(id) {
    DESARROLLO.PROCESO_CONFIRM("¿Esta seguro que desea eliminar el registro? </br>", function (r) {
        if (r) {
            $("#tabla_DatosContac tbody tr[id=" + id + "]").remove();
        }
    });
}
function Fun_ValidDetalleEntidad() {
    $.ajax({
        url: baseUrl + "Seguridad/ListaEntidadesDetalle?id_entidad=" + $("#DcboEntidades").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (p) {
            
            if (p.length > 0) {
                if (p[0].FLG_PAC == "1") {
                    $("#inputFlgPac").text("1");
                    $("#ChecPacv").css("display", "block");
                } else {
                    $("#inputFlgPac").text("0");
                    $("#ChecPacv").css("display", "none");
                }

            }
        }
    });
}
function ValidateArchivos() {
    var result = true;
    if ($('#FileDniR')[0].files.length === 0) { $("#LblfileDniRegistro").text("Es necesario que adjunte su dni escaneado."); result = false; } else { $("#LblfileDniRegistro").text(""); }
    if ($('#FileDocumenR')[0].files.length === 0) { $("#LblfileDocumentoRegistro").text("Es necesario que adjunte su documento de acreditación."); result = false; } else { $("#LblfileDocumentoRegistro").text(""); }

    return result;
}
function fn_ValidarCapcha() {
    var item =
    {
    };
    var urls = baseUrl + 'Seguridad/ValidarCaptcha?Captcha=' + $("#txtUCaptchaN").val();
    var respuesta = DESARROLLO.Ajax(urls, null, false);
    return respuesta.success;
}
function callProv(idDep) {
    $.getJSON(baseUrl + "Seguridad/ListarProvinciaCbo",
        { CCODDEPARTAMENTO: idDep }, function (e) {
            $("#COD_PROVINCIA").empty();
            $.each(e, function (e, o) {
                $("#COD_PROVINCIA").append($("<option></option>").val(o.CCODPROVINCIA).html(o.CNOMPROVINCIA));
            })
        });
}
function callDist(idProv) {
    $.getJSON(baseUrl + "Seguridad/ListarDistritoCbo",
        {
            CCODDEPARTAMENTO: $("#Dep").val(),
            CCODPROVINCIA: idProv
        },
        function (i) {
            $("#COD_DISTRITO").empty();
            $.each(i, function (i, t) {
                $("#COD_DISTRITO").append($("<option></option>").val(t.CCODDISTRITO).html(t.CNOMDISTRITO));
            })

        });
}
function validate_con() {
    var result = true;
    if ($("#txtTelefono").val() == "") { $("#txtTelefono").addClass('is-invalid'); result = false; } else { $("#txtTelefono").removeClass('is-invalid'); }
    if ($("#txtAnexo").val() == "") { $("#txtAnexo").addClass('is-invalid'); result = false; } else { $("#txtAnexo").removeClass('is-invalid'); }
    if ($("#txtCelular").val() == "") { $("#txtCelular").addClass('is-invalid'); result = false; } else { $("#txtCelular").removeClass('is-invalid'); }

    if ($("#txtTelefono").val() == "000000000") { $("#txtTelefono").addClass('is-invalid'); result = false; } else { $("#txtTelefono").removeClass('is-invalid'); }
    if ($("#txtAnexo").val() == "0000") { $("#txtAnexo").addClass('is-invalid'); result = false; } else { $("#txtAnexo").removeClass('is-invalid'); }
    if ($("#txtCelular").val().charAt(0) != "9") { $("#txtCelular").addClass('is-invalid'); result = false; } else { $("#txtCelular").removeClass('is-invalid'); }

    if ($("#txtCorreo").val() == "") { $("#txtCorreo").addClass('is-invalid'); result = false; } else { $("#txtCorreo").removeClass('is-invalid'); }
    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if (!regex.test($('#txtCorreo').val().trim())) {
        $("#txtCorreo").addClass('is-invalid'); result = false;
    } else {
        $("#txtCorreo").removeClass('is-invalid');
    }
    return result;
}
function GenerarCapchaIngresoN() {
    contadorN += 1;
    jQuery('#imgCapchaN').attr("src", "");
    jQuery('#imgCapchaN').attr("src", DESARROLLO.GENERARCAPCHA(contadorN));
}
function Fun_ValidarPersonaDni() {
    $.ajax({
        url: baseUrl + "Seguridad/ListaDetallePersona?dni=" + $("#txtNumDni").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (p) {
            if (p.ACCION == '1') {
                
                if (p.APELLIDO_PATERNO != "-") {
                    $("#txtApellidoPaterno").attr('disabled', 'disabled');
                    $("#txtApellidoMaterno").attr('disabled', 'disabled');
                    $("#txtNombres").attr('disabled', 'disabled');

                    if (p.APELLIDO_PATERNO == $("#txtApellidoPValid").val().toUpperCase()) {
                        $("#txtApellidoPaterno").val(p.APELLIDO_PATERNO);
                        $("#txtApellidoMaterno").val(p.APELLIDO_MATERNO);
                        $("#txtNombres").val(p.NOMBRES);
                    } else {
                        $("#txtApellidoPaterno").val("");
                        $("#txtApellidoMaterno").val("");
                        $("#txtNombres").val("");
                        DESARROLLO.PROCESO_ALERT('Atención', 'El apellido de verificación no es valido.');
                    }
                           
                } else {
                    $("#txtApellidoPaterno").val("");
                    $("#txtApellidoMaterno").val("");
                    $("#txtNombres").val("");
                    DESARROLLO.PROCESO_ALERT('Atención', 'El número de documento no es válido.');            
                }
                $("#myModalCargando").css("display", "none");
            } else {
                DESARROLLO.PROCESO_ALERT('Atención', 'Problemas al consultar el número de documento, ingrese su información manualmente.');          
                $("#txtApellidoPaterno").removeAttr('disabled');
                $("#txtApellidoMaterno").removeAttr('disabled');
                $("#txtNombres").removeAttr('disabled');
                console.log(p.DES_ERROR);
                $("#myModalCargando").css("display", "none");
            }      
        }
    });
  
}
function Fun_Registrar_Coordinador() {
    var dni = document.getElementById("FileDniR").files.length;
    var doc = document.getElementById("FileDocumenR").files.length;
    var e = new FormData;
    if (dni > 0) for (var a, l = 0; dni > l; l++) a = document.getElementById("FileDniR").files[l], e.append("FileDniR", a);
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileDocumenR").files[l], e.append("FileDocumenR", a);
    var Contacto = new Array();
    /*
    $("#tabla_DatosContac tbody tr").each(function (index) {
        
        var Telefono = "";
        var Anexo = "";
        var Celular = "";
        var Correo = "";
        $(this).children("td").each(function (index2) {
            switch (index2) {
                case 1: Telefono = $(this).text();
                    break;
                case 2: Anexo = $(this).text();;
                    break;
                case 3: Celular = $(this).text();;
                    break;
                case 4: Correo = $(this).text();;
                    break;
            }
        })
        var ObjContacto = {
            TELEFONO: Telefono,
            ANEXO: Anexo,
            CELULAR: Celular,
            CORREO: Correo
        };
        Contacto.push(ObjContacto);
    });*/
    var Telefono = $("#txtTelefono").val();
    var Anexo = $("#txtAnexo").val();
    var Celular = $("#txtCelular").val();
    var Correo = $("#txtCorreoN").val();

    var ObjContacto = {
        TELEFONO: Telefono,
        ANEXO: Anexo,
        CELULAR: Celular,
        CORREO: Correo
    };
    Contacto.push(ObjContacto);

    var FLG_FAG = "0";
    var FLG_PAC = "0";
    if (document.getElementById('CheckFag').checked) {
        FLG_FAG = "1";
    }
    if ($("#inputFlgPac").text()=="1") {
        if (document.getElementById('CheckPac').checked) {
            FLG_PAC = "1";
        }
    }
    e.append("ID_COORDINADOR", 0);
    e.append("USUARIO", $("#txtNumDni").val());
    e.append("ID_ENTIDAD", $("#DcboEntidades").val());
    e.append("ID_TIPO_DOCUMENTO", $("#DdlTipoDocumento").val());
    e.append("NUM_DOCUMENTO", $("#txtNumDni").val());
    e.append("APELLIDO_PATERNO", $("#txtApellidoPaterno").val());
    e.append("APELLIDO_MATERNO", $("#txtApellidoMaterno").val());
    e.append("NOMBRES", $("#txtNombres").val());
    e.append("ID_TIPO_VINCULO_ENT", $("#DdlVinvuloLa").val());
    e.append("CARGO", $("#txtCargo").val().toUpperCase());
    e.append("DOCUMENTO_ACRE", $("#txtDocA").val().toUpperCase());
    e.append("CORREO_NOTIFICADOR", $("#txtCorreoN").val());
    e.append("COD_DEPARTAMENTO_ENT", $("#Dep").val());
    e.append("COD_PROVINCIA_ENT", $("#COD_PROVINCIA").val());
    e.append("COD_DISTRITO_ENT", $("#COD_DISTRITO").val());
    e.append("DIRECCION_ENT", $("#txtDireccion").val().toUpperCase());
    e.append("DESCRIPCION", $('select[name="DcboEntidades"] option:selected').text());
    e.append("FLG_FAG", FLG_FAG);
    e.append("FLG_PAC", FLG_PAC);
    e.append("DEPENDENCIA", $("#txtDependencia").val().toUpperCase());
    e.append("TIPO_USUSARIO", $("#DdlTipoUser").val() );
    e.append("listaContacto", JSON.stringify(Contacto));
    $.ajax({
        url: baseUrl + "Seguridad/RegistroSolicitudCoordinador",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                jQuery("#modalNuevoUsuario").html('');
                $('#modalNuevoUsuario').modal('hide');
                
            }
            else {
                 DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function fn_ValidarExisteUsuario(TIPO) {
    var urls = "";
    var item =
    {
        NUM_DOCUMENTO: $("#txtNumDni").val()
    };
    if (TIPO=="P") {
        urls = baseUrl + 'Seguridad/ValidarUserPendiente';
    } else {
        urls = baseUrl + 'Seguridad/ValidarUserAprobado';
    }
  
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}
function GenerarCapchaDni() {
    contadorDNI += 1;
    jQuery('#imgCapchaDni').attr("src", "");
    jQuery('#imgCapchaDni').attr("src", DESARROLLO.GENERARCAPCHADNI(contadorDNI));
}
function GenerarValidDni() {
    if ($("#DdlTipoDocumento").val() == "1") {
        $(".input-R").css("display", "block");
    } else {
        $(".input-R").css("display", "none");
    }
}
function fn_ValidarCapchaDni() {
    var item =
    {
    };
    var urls = baseUrl + 'Seguridad/ValidarCaptchaDni?Captcha=' + $("#txtUCaptchaDni").val();
    var respuesta = DESARROLLO.Ajax(urls, null, false);
    return respuesta.success;
}
function Validate_DNI() {
    var result = true;
    if ($("#DdlTipoDocumento").val() == "") { $("#DdlTipoDocumento").addClass('is-invalid'); result = false; } else { $("#DdlTipoDocumento").removeClass('is-invalid'); }
    if ($("#txtNumDni").val() == "") { $("#txtNumDni").addClass('is-invalid'); result = false; } else { $("#txtNumDni").removeClass('is-invalid'); }
    if ($("#txtUCaptchaDni").val() == "") { $("#txtUCaptchaDni").addClass('is-invalid'); result = false; } else { $("#txtUCaptchaDni").removeClass('is-invalid'); }
    if ($("#txtApellidoPValid").val() == "") { $("#txtApellidoPValid").addClass('is-invalid'); result = false; } else { $("#txtApellidoPValid").removeClass('is-invalid'); }
    return result;
}