////var listaUbigeo;
////var listaRegion;
////var listaProvincia;
////var listaDistrito;


$(function () {
    //Validando numeros
    $(".integer_input").on('keypress', function (e) {
        DESARROLLO.SOLONUMEROS(e);
    });
    //Validando decimales
    $(".decimal_input").on('keypress', function (e) {
        DESARROLLO.NUMERODECIMAL(e);
    });
})

var ContratosMantenimiento = function () {
    var Iniciar = function () {
        loadCargaInicial();
        callProv($("#REGION").val(), $("#txtProvincia").val());
        //loadUbigeos();
    }
    //var loadUbigeos = function () {
    //    let region = $("#txtRegion").val();
    //    let provincia = $("#txtProvincia").val();

    //    $.ajax({
    //        type: 'GET',
    //        url: baseUrl + 'Carga/Contrato/Ubigeo?region=' + region + '&provincia=' + provincia,
    //        data: null,
    //        contentType: 'application/json; charset=utf-8',
    //        success: function (result) {
    //            listaUbigeo = result;

    //            //Listando regiones
    //            listaRegion = [];
    //            listaRegion.push();

    //            loadRegiones();
    //            //CCODDEPARTAMENTO: "01"
    //            //CCODDISTRITO: "02"
    //            //CCODPROVINCIA: "02"
    //            //CNOMDEPARTAMENTO: "AMAZONAS"
    //            //CNOMDISTRITO: "ACHAYA"
    //            //CNOMPROVINCIA: "BAGUA"
    //            
    //            //loadRegiones(result.Region);
    //            //loadProvincias(result.Provincia);
    //            //loadDistritos(result.Distrito);
    //        }
    //    });
    //}
    var loadingOpen = function () {
        $(".container_spinner").removeClass("hide");
        $(".container_spinner").addClass("show");
    };
    var loadingClose = function () {
        $(".container_spinner").removeClass("show");
        $(".container_spinner").addClass("hide");
    };
    var loadCargaInicial = function () {
        $.ajax({
            type: 'POST',
            url: baseUrl + 'Carga/Contrato/ListaMaestra',
            data: null,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                let html = "";

                /* Estado */
                let valEstado = $("#txtEstado").val();
                for (var i in result.Estado) {
                    html += '<option value="' + result.Estado[i]['Id'] + '" ' + (valEstado == result.Estado[i]['Id'] ? 'selected' : '') + '>' + result.Estado[i]['Descripcion'] + '</option>';
                }
                $('#lstEstado').html(html);

                /* Si No */
                html = "";
                let valLey31419 = $("#txtLey31419").val();
                for (var i in result.SiNo) {
                    html += '<option value="' + result.SiNo[i]['Id'] + '" ' + (valLey31419 == result.SiNo[i]['Id'] ? 'selected' : '') + '>' + result.SiNo[i]['Descripcion'] + '</option>';
                }
                $("#lstLey31419").html(html);

                /* Sexo */
                html = "";
                let valSexo = $("#txtSexo").val();
                for (var i in result.Sexo) {
                    html += '<option value="' + result.Sexo[i]['Id'] + '" ' + (valSexo == result.Sexo[i]['Id'] ? 'selected' : '') + '>' + result.Sexo[i]['Descripcion'] + '</option>';
                }
                $("#lstSexo").html(html);

                /* Entidad */
                let valEntidad = $("#txtCodigoEntidad").val();
                let valEntidadBenef = $("#txtEntidadBenef").val();
                
                EnviarCombo({}, baseUrl + 'Carga/Contrato/GetEntidades', '#DdlEntidadFAGModal', valEntidad);
                EnviarCombo({}, baseUrl + 'Carga/Contrato/GetEntidades', '#DdlEntidadBenefFAGModal', valEntidadBenef);
            }
        });

    };

    var EnviarCombo = function (data, ruta, id, valSelect) {
        $.ajax({
            type: 'POST',
            url: ruta,
            data: data,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                let html = "";
                let data = JSON.parse(result);
                html += "<option value='0'> SELECCIONAR </option>";
                for (var i = 0; i < data.length; i++) {
                    html += '<option value="' + data[i].Id + '" ' + (valSelect == data[i].Id ? 'selected' : '') + '>' + data[i].Descripcion + '</option>';
                }
                $(id).html(html);
            }
        });
    };
    //var loadDistritos = function (distritos) {
    //    let html = "";
    //    for (var i in distritos) {
    //        html += '<option value="' + distritos[i]["Id"] + '" ' + (distritos[i]["Id"] == $("#txtDistrito").val() ? 'selected' : '') + ' >' + distrito[i]["Descripcion"] + '</option>';
    //    }
    //    $("#lstDistrito").html(html);
    //}
    //var loadProvincias = function (provincias) {
    //    let html = "";
    //    for (var i in provincias) {
    //        html += '<option value="' + provincias[i]["Id"] + '" ' + (provincias[i]["Id"] == $("#txtProvincia").val() ? 'selected' : '') + ' >' + provincias[i]["Descripcion"] + '</option>';
    //    }
    //    $("#lstProvincia").html(html);
    //}
    //var loadRegiones = function () {
    //    let html = "";
    //    for (var i in listaUbigeo) {
    //        html += '<option value="' + listaUbigeo[i]["CCODDEPARTAMENTO"] + '" ' + (listaUbigeo[i]["CCODDEPARTAMENTO"] == $("#txtRegion").val() ? 'selected' : '') + ' >' + listaUbigeo[i]["CNOMDEPARTAMENTO"] + '</option>';
    //    }
    //    $("#lstRegion").html(html);
    //}

    var callProv = function (idDep, provSelect) {
        $.getJSON(baseUrl + "Carga/Contrato/ListarProvinciaCbo",
            { CCODDEPARTAMENTO: idDep }, function (e) {
                $("#COD_PROVINCIA").empty();
                $("#COD_DISTRITO").empty();
                $.each(e, function (e, o) {
                    $("#COD_PROVINCIA").append($("<option></option>").val(o.CCODPROVINCIA).html(o.CNOMPROVINCIA));
                    if (provSelect.trim() != '') {
                        $("#COD_PROVINCIA").val(provSelect);
                        callDist(provSelect, $("#txtDistrito").val());
                    }
                })
            });
    }
    var callDist = function (idProv, disSelect) {
        $.getJSON(baseUrl + "Carga/Contrato/ListarDistritoCbo",
            {
                CCODDEPARTAMENTO: $("#REGION").val(),
                CCODPROVINCIA: idProv
            },
            function (i) {
                $("#COD_DISTRITO").empty();
                $.each(i, function (i, t) {
                    $("#COD_DISTRITO").append($("<option></option>").val(t.CCODDISTRITO).html(t.CNOMDISTRITO));
                    if (disSelect.trim() != '') {
                        $("#COD_DISTRITO").val(disSelect);
                    }
                })
            });

    }

    var guardar = function () {
        DESARROLLO.PROCESO_CONFIRM("¿Desea actualizar el contrato? ", (rpta) => {

            if (rpta) {
                loadingOpen();
                var entidad = {
                    "ID_REGISTRO": $("#txtIdRegistro").val(),
                    "ID_CARGA": $("#txtIdCarga").val(),
                    "COD_CONTRATO": $("#txtCodContrato").val(),
                    "FEC_REGISTRO": $("#txtFecRegistro").val(),
                    "ESTADO": $("#txtEstado").val(),
                    "NRO_EXP_SISPER": $("#txtNroSisper").val(),
                    "LEY31419": $("#txtLey31419").val(),
                    "APELLIDO_PATERNO": $("#txtApellidoPaterno").val(),
                    "APELLIDO_MATERNO": $("#txtApellidoMaterno").val(),
                    "NOMBRES": $("#txtNombres").val(),
                    "DNI": $("#txtDni").val(),
                    "RUC": $("#txtRuc").val(),
                    "SEXO": $("#txtSexo").val(),
                    "FEC_NACIMIENTO": $("#txtFecNacimiento").val(),
                    "EDAD": $("#txtEdad").val(),
                    "TELEFONO_CELULAR": $("#txtTelefonoCelular").val(),
                    "CORREO_ELECTRONICO": $("#txtCorreoElectronico").val(),
                    "DIRECCION": $("#txtDireccion").val(),
                    "DISTRITO": $("#COD_DISTRITO").val(),
                    "PROVINCIA": $("#COD_PROVINCIA").val(),
                    "REGION": $("#REGION").val(),
                    "NIVEL_GOBIERNO": $("#txtNivelGobierno").val(),
                    "CODIGO_ENTIDAD": $("#DdlEntidadFAGModal").val(),
                    "TIPO_DOC_SOLICITA_REG": $("#txtTipoDocSolicitaReg").val(),
                    "FEC_DOC_SOLICITA_REG": $("#txtFecDocSolicitaReg").val(),
                    "HR_DOC_SOLICITA_REG": $("#txtHrDocSolicitaReg").val(),
                    "ENTIDAD_BENEF": $("#DdlEntidadBenefFAGModal").val(),
                    "FLAG_DESIGNADO": $("#txtFlagDesignado").val(),
                    "CARGO_FUNCIONARIO_SUSCRIBE": $("#txtCargoFuncionarioSuscribe").val(),
                    "IMPORTE_HONORARIO": $("#txtImporteHonorario").val(),
                    "FEC_SUSCRIPCION": $("#txtFechaSuscripcion").val(),
                    "FEC_INICIO": $("#txtFechaInicio").val(),
                    "FEC_CULMINACION": $("#txtFechaCulminacion").val(),
                    "FEC_RECEPCION": $("#txtFechaRecepcion").val(),
                    "NRO_OFICIO": $("#txtNroOficio").val(),
                    "FIN_CONTRATO": $("#txtExpRemitido").val(),
                    "ADENDA_VIG_NRO": $("#txtAdendaVigenteNro").val(),
                    "ADENDA_VIG_FEC_INICIO": $("#txtAdendaVigenteFechaInicio").val(),
                    "ADENDA_VIG_FEC_FIN": $("#txtAdendaVigenteFechaFin").val(),
                    "ADENDA_VIG_HOJARUTA": $("#txtAdendaVigenteHojaRuta").val(),
                    "ADENDA_VIG_SUSCRIPCION": $("#txtAdendaVigenteSuscripcion").val(),
                    "GRADO_ACADEMICO_GEN": $("#txtGradoAcademicoGen").val(),
                    "GRADO_ACADEMICO_FAG": $("#txtGradoAcademicoFag").val(),
                    "CARRERA_PROFESIONAL": $("#txtCarreraProfesional").val(),
                    "UNIVERSIDAD": $("#TXT_UNIVERSIDAD").val(),
                    "FLAG_REG_SUNEDU": $("#txtFlagRegSunedu").val(),
                    "GRADO_ACADEMICO_ESP": $("#txtGradoAcademicoEsp").val(),
                    "REQ_HAB_PROF": $("#txtReqHabProf").val(),
                    "FISCAL_POST": $("#txtFiscalPost").val(),
                    "EXP_LAB_GENERAL": $("#txtExpLaboralGeneral").val(),
                    "EXP_LAB_ESPECIFICA": $("#txtExpLabEspecifica").val(),
                    "EXP_LAB_GENERAL_GRADO": $("#txtExpLabGeneralGrado").val(),
                    "EXP_LAB_ACT_TDRS": $("#txtExpLabActTdrs").val(),
                    "RES_CONTRATO_FEC_CULM": $("#txtResContratoFecCulm").val(),
                    "RES_CONTRATO_DOC": $("#txtResContratoDoc").val(),
                    "RES_CONTRATO_HR": $("#txtResContratoHR").val(),
                    "SEC_RESPONSABLE": $("#txtSecResponsbe").val(),
                    "ENT_FIN_NOMBRE": $("#txtEntidadFinanciera").val(),
                    "ENT_FIN_CTA_AHORRO": $("#txtEntidadFinancieraCuentaAhorros").val(),
                    "ENT_FIN_CTA_BANCARIA": $("#txtEntidadFinancieraCuentaBancaria").val(),
                    "ENT_FIN_CTA_CCI": $("#txtEntidadFinancieraCuentaCCI").val(),
                };

                $.ajax({
                    type: 'POST',
                    url: baseUrl + 'Carga/Contrato/EditarContrato',
                    data: JSON.stringify({
                        idContrato: $("#txtIdRegistro").val(), contrato: entidad
                    }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {

                        if (!result.Error) {
                            loadingClose();
                            window.parent.Contratos.ListarContratos();
                            DESARROLLO.PROCESO_CORRECTO("Registro actualizado correctamente");

                            $("#modalMantenimientoContratoFAG").modal("hide");
                            $("#modalMantenimientoContratoPAC").modal("hide");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se actualizó el registro");
                            loadingClose();
                        }
                    }
                });

            }
        });
    }

    return {
        Iniciar: function () { Iniciar(); },
        Guardar: function () { guardar(); },
        CallProv: function (idDep, provSelect) { callProv(idDep, provSelect); },
        CallDist: function (idProv, disSelect) { callDist(idProv, disSelect); }
    }
}();


$("#btnGrabar").click(function () {
    ContratosMantenimiento.Guardar();
})
$("#REGION").change(function () {
    var e = $(this).val();
    ContratosMantenimiento.CallProv(e, "");
});
$("#COD_PROVINCIA").change(function () {
    var i = $(this).val();
    ContratosMantenimiento.CallDist(i, "");
});