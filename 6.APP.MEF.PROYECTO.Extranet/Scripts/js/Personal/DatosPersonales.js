function callProv(idDep, desc_id) {
    prov_n = $("#COD_PROV_NAC").val();
    prov = $("#COD_PROV").val();
    $.getJSON(baseUrl + "Seguridad/ListarProvinciaCbo",
        { CCODDEPARTAMENTO: idDep }, function (e) {
            $("#" + desc_id).empty();
            $.each(e, function (e, o) {
                $("#" + desc_id).append($("<option></option>").val(o.CCODPROVINCIA).html(o.CNOMPROVINCIA));
                if (prov_n != '') {
                    $("#COD_PROVINCIA_NAC").val(prov_n);
                }
                if (prov != '') {
                    $("#COD_PROVINCIA").val(prov);
                }
            })
        });
}
function callDist(dep, idProv, desc_id) {
    dis_n = $("#COD_DIST_NAC").val();
    dis = $("#COD_DIST").val();
    $.getJSON(baseUrl + "Seguridad/ListarDistritoCbo",
        {
            CCODDEPARTAMENTO: $("#" + dep).val(),
            CCODPROVINCIA: idProv
        },
        function (i) {
            $("#" + desc_id).empty();
            $.each(i, function (i, t) {
                $("#" + desc_id).append($("<option></option>").val(t.CCODDISTRITO).html(t.CNOMDISTRITO));
                if (dis_n != '') {
                    $("#COD_DISTRITO_NAC").val(dis_n);
                }
                if (dis != '') {
                    $("#COD_DISTRITO").val(dis);
                }
            })

        });
}
function fn_UpdateDatosPersonales() {
    var e = new FormData;
    e.append("FECHA_NACIMIENTO", $("#FechaNacimientoTexto").val());
    e.append("ID_SEXO", $("#DdlTipoSex").val());
    e.append("ID_TIPO_DOCUMENTO", $('#DdlTipoDocumento').val());
    e.append("TELEFONO", $("#TELEFONO").val());
    e.append("CELULAR", $('#CELULAR').val());
    e.append("EMAIL", $('#EMAIL').val());
    e.append("RUC", $('#RUC').val());
    e.append("ID_TIPO_NACIONALIDAD", $('#ID_TIPO_NACIONALIDAD').val());
    e.append("COD_DEPA_NAC", $('#COD_DEPA_NAC').val());
    e.append("COD_PROV_NAC", $('#COD_PROVINCIA_NAC').val());
    e.append("COD_DIST_NAC", $('#COD_DISTRITO_NAC').val());
    e.append("DPTO_PROV_EXTRANJERO", $('#DPTO_PROV_EXTRANJERO').val().toUpperCase());
    e.append("COD_DEPA", $('#COD_DEPA').val());
    e.append("COD_PROV", $('#COD_PROVINCIA').val());
    e.append("COD_DIST", $('#COD_DISTRITO').val());
    e.append("DIRECCION", $('#DIRECCION').val().toUpperCase());
    e.append("ID_TIPO_BANCO", $('#ID_TIPO_BANCO').val());
    e.append("CUENTA_BANCARIA", $('#CUENTA_BANCARIA').val());
    e.append("CUENTA_CCI", $('#CUENTA_CCI').val());
    e.append("APE_NOMBRES_CONTACTO", $('#APE_NOMBRES_CONTACTO').val().toUpperCase());
    e.append("TELEFONO_CONTACTO", $('#TELEFONO_CONTACTO').val().toUpperCase());
    e.append("CELULAR_CONTACTO", 0);
    e.append("ESTADO_CIVIL", $('#DdlEstadoCivil').val());
    e.append("GRADOS", $('#GRADOS').val().toUpperCase());
    e.append("TITULOS", $('#TITULOS').val().toUpperCase());
    $.ajax({
        url: baseUrl + 'Usuario/Personal/UpdatePersonal',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                $("#inputHdDatosPer").val("1");
                if ($('.wizard')) {
                    $('#btnNextStep1').trigger('click');
                }
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
        }
    });
}
function fn_pais(pais) {
    if (pais == "173") {
        $("#DptoNac").css('display', 'block');
        $("#ProvNac").css('display', 'block');
        $("#DistNac").css('display', 'block');
        $("#Extranjero").css('display', 'none');
        //$("#COD_DEPA_NAC").prop('disabled', false);
        //$("#COD_PROV_NAC").prop('disabled', false);
        //$("#COD_DIST_NAC").prop('disabled', false);
    }
    if (pais != "173") {
        $("#DptoNac").css('display', 'none');
        $("#ProvNac").css('display', 'none');
        $("#DistNac").css('display', 'none');
        $("#Extranjero").css('display', 'block');
    }
}
$("#COD_DEPA_NAC").change(function () {
    var e = $(this).val();
    callProv(e, "COD_PROVINCIA_NAC");
});
$("#COD_PROVINCIA_NAC").change(function () {
    var i = $(this).val();
    callDist("COD_DEPA_NAC", i, "COD_DISTRITO_NAC");
});
$("#COD_DEPA").change(function () {
    var e = $(this).val();
    callProv(e, "COD_PROVINCIA");
});
$("#COD_PROVINCIA").change(function () {
    var i = $(this).val();
    callDist("COD_DEPA", i, "COD_DISTRITO");
});
jQuery("#btnGrabar").click(procesarMisDatos);
jQuery("#btnGrabar").click(procesarMisDatosStep2);

function procesarMisDatosWizard() {
    if (!($("#frmMisDatos").valid())) { return false; }

    if (!DESARROLLO.RUC_VALIDO($('#RUC').val())) {
        DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente su RUC.");
        return false;
    }

    if (!DESARROLLO.V_EMAIL("EMAIL")) {
        $("#EMAIL").addClass('is-invalid');
        DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente su correo electrónico.");
        return false;
    }
    $("#EMAIL").removeClass('is-invalid');

    if (!validate_telefonos()) {
        DESARROLLO.PROCESO_ALERT("Atención", "Error en el formulario, realizar las correcciones.");
        return false;
    }

    return true;
}

function procesarMisDatos() {
    let result = true;
    var text = "</br>¿Desea <b> Actualizar</b> los datos personales.? </br >";
    if ($("#frmMisDatos").valid()) {

        if (DESARROLLO.RUC_VALIDO($('#RUC').val())) {

            if (DESARROLLO.V_EMAIL("EMAIL")) {
                $("#EMAIL").removeClass('is-invalid');

                if (validate_telefonos()) {
                    DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                        if (r) {
                            fn_UpdateDatosPersonales();
                        }
                    });
                } else {
                    DESARROLLO.PROCESO_ALERT("Atención", "Error en el formulario, realizar las correcciones.");
                    result = false;
                }
                
            } else {
                $("#EMAIL").addClass('is-invalid');
                DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente su correo electrónico.");
                result = false;
            }

        } else {
            DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente su RUC.");
            result = false;
        }
    } else { result = false; }
    return result;
};
function validate_telefonos() {
    var result = true;
    if ($("#TELEFONO").val() == "") { $("#TELEFONO").addClass('is-invalid'); result = false; } else { $("#TELEFONO").removeClass('is-invalid'); }
    if ($("#CELULAR").val() == "") { $("#CELULAR").addClass('is-invalid'); result = false; } else { $("#CELULAR").removeClass('is-invalid'); }
    if ($("#TELEFONO").val() == "000000000") { $("#TELEFONO").addClass('is-invalid'); result = false; } else { $("#TELEFONO").removeClass('is-invalid'); }
    if ($("#TELEFONO_CONTACTO").val() == "000000000") { $("#TELEFONO_CONTACTO").addClass('is-invalid'); result = false; } else { $("#TELEFONO_CONTACTO").removeClass('is-invalid'); }
    if ($("#CELULAR").val().charAt(0) != "9") { $("#CELULAR").addClass('is-invalid'); result = false; } else { $("#CELULAR").removeClass('is-invalid'); }
    return result;
}

$("#frmMisDatos").validate({
    rules: {
        "DdlTipoDocumento": {
            required: true,
        },
        "DdlTipoSex": {
            required: true,
        },
        "NUM_DOCUMENTO": {
            required: true,
        },
        "RUC": {
            required: true,
        },
        "APELLIDO_PATERNO": {
            required: true,
        },
        "APELLIDO_MATERNO": {
            required: true,
        },

        "NOMBRES": {
            required: true,
        }, "EMAIL": {
            required: true,
            email: true,
        }, "FechaNacimientoTexto": {
            required: true,
        }, "DdlEstadoCivil": {
            required: true,
        }, "CELULAR": {
            required: true,
            maxlength: 9,
            min: 1
        }, "TELEFONO": {
            maxlength: 9,
            min: 1
        },
        "ID_TIPO_NACIONALIDAD": {
            required: true,
        }, "DPTO_PROV_EXTRANJERO": {
            required: true,
            email: true,
        }, "COD_DEPA_NAC": {
            required: true,
        }, "COD_PROVINCIA_NAC": {
            required: true,
        }, "COD_DISTRITO_NAC": {
            required: true,
        }, "COD_DEPA": {
            required: true,
        },
        "COD_PROVINCIA": {
            required: true,
        }, "COD_DISTRITO": {
            required: true,
        }, "DIRECCION": {
            required: true,
        }, "ID_TIPO_BANCO": {
            required: true,
        }, "CUENTA_BANCARIA": {
            required: true,
        }, "CUENTA_CCI": {
            required: true,
            maxlength: 20,
            minlength: 20
        },
        "APE_NOMBRES_CONTACTO": {
            required: true,
        }, "TELEFONO_CONTACTO": {
            required: true,
        },
    },
    errorElement: "P",
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }
});

function procesarMisDatosStep2() {
    let result = true;
    var text = "</br>¿Desea <b> Actualizar</b> los datos su curriculum.? </br >";
    //alert('¿Desea Actualizar los datos su curriculum.?');
    //if ($("#frmMisDatos").valid()) {

    //    if (DESARROLLO.RUC_VALIDO($('#RUC').val())) {

    //        if (DESARROLLO.V_EMAIL("EMAIL")) {
    //            $("#EMAIL").removeClass('is-invalid');

    //            if (validate_telefonos()) {
    //                DESARROLLO.PROCESO_CONFIRM(text, function (r) {
    //                    if (r) {
    //                        fn_UpdateDatosEstudios();
    //                    }
    //                });
    //            } else {
    //                DESARROLLO.PROCESO_ALERT("Atención", "Error en el formulario, realizar las correcciones.");
    //                result = false;
    //            }

    //        } else {
    //            $("#EMAIL").addClass('is-invalid');
    //            DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente su correo electrónico.");
    //            result = false;
    //        }

    //    } else {
    //        DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente su RUC.");
    //        result = false;
    //    }
    //}
    return result;
};

function fn_UpdateDatosEstudios() {
    var e = new FormData;
    e.append("FECHA_NACIMIENTO", $("#FechaNacimientoTexto").val());
    e.append("ID_SEXO", $("#DdlTipoSex").val());
    e.append("ID_TIPO_DOCUMENTO", $('#DdlTipoDocumento').val());
    e.append("TELEFONO", $("#TELEFONO").val());
    e.append("CELULAR", $('#CELULAR').val());
    e.append("EMAIL", $('#EMAIL').val());
    e.append("RUC", $('#RUC').val());
    e.append("ID_TIPO_NACIONALIDAD", $('#ID_TIPO_NACIONALIDAD').val());
    e.append("COD_DEPA_NAC", $('#COD_DEPA_NAC').val());
    e.append("COD_PROV_NAC", $('#COD_PROVINCIA_NAC').val());
    e.append("COD_DIST_NAC", $('#COD_DISTRITO_NAC').val());
    e.append("DPTO_PROV_EXTRANJERO", $('#DPTO_PROV_EXTRANJERO').val().toUpperCase());
    e.append("COD_DEPA", $('#COD_DEPA').val());
    e.append("COD_PROV", $('#COD_PROVINCIA').val());
    e.append("COD_DIST", $('#COD_DISTRITO').val());
    e.append("DIRECCION", $('#DIRECCION').val().toUpperCase());
    e.append("ID_TIPO_BANCO", $('#ID_TIPO_BANCO').val());
    e.append("CUENTA_BANCARIA", $('#CUENTA_BANCARIA').val());
    e.append("CUENTA_CCI", $('#CUENTA_CCI').val());
    e.append("APE_NOMBRES_CONTACTO", $('#APE_NOMBRES_CONTACTO').val().toUpperCase());
    e.append("TELEFONO_CONTACTO", $('#TELEFONO_CONTACTO').val().toUpperCase());
    e.append("CELULAR_CONTACTO", 0);
    e.append("ESTADO_CIVIL", $('#DdlEstadoCivil').val());
    e.append("GRADOS", $('#GRADOS').val().toUpperCase());
    e.append("TITULOS", $('#TITULOS').val().toUpperCase());
    $.ajax({
        url: baseUrl + 'Usuario/Personal/DML_MantenimientoEstudio',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                $("#inputHdDatosPer").val("1");
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
        }
    });
};