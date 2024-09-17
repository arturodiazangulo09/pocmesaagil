var CargaMantenimiento = function () {    
    var ModalInit = function (idRegistro) {
        console.log("idRegistro:" + idRegistro);
        tableBody("/Carga/Carga/LoadAdendas?IdRegistro=" + idRegistro, '#tablaAdendasBody');
        iniciarValidaciones();
        console.log("region:");
        console.log($("#REGION").val());
        callProv($("#REGION").val(), $("#txtProvincia").val());
    }

    var Inicio = function () {
        callProv($("#REGION").val(), $("#txtProvincia").val());
    }
    var tableBody = function (ruta, id) {
        $.ajax({
            type: 'POST',
            url: ruta,
            data: null,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                let html = "";
                let data = JSON.parse(result);

                if (data.length > 0) {

                    for (var i = 0; i < data.length; i++) {
                        html += '<tr>';
                        html += '<td>' + data[i].COD_CONTRATO + '</td>';
                        html += '<td>' + data[i].DOC_SOLICITUD + '</td>';
                        html += '<td>' + data[i].HOJA_RUTA + '</td>';
                        html += '<td>' + data[i].NUMERO + '</td>';

                        html += '</tr>';
                    }

                    $(id).html(html);

                    $('#tablaAdendasBody').dataTable({
                        retrieve: true,
                        autoWidth: true,
                        searching: false,
                        responsive: true,
                        ordering: false,
                        paging: false,
                        scrollY: 320,
                        Processing: "Cargando...",
                        info: false,
                        language: {
                            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
                        },
                    });
                }
                
            }
        });
    }

    var callProv = function (idDep, provSelect) {
        $.getJSON(baseUrl + "Carga/Carga/ListarProvinciaCbo",
            { CCODDEPARTAMENTO: idDep }, function (e) {
                $("#COD_PROVINCIA").empty();
                $("#COD_DISTRITO").empty();
                $.each(e, function (e, o) {
                    $("#COD_PROVINCIA").append($("<option></option>").val(o.CNOMPROVINCIA).html(o.CNOMPROVINCIA));
                    if (provSelect.trim() != '') {
                        $("#COD_PROVINCIA").val(provSelect);
                        callDist(provSelect, $("#txtDistrito").val());
                    }
                })
            });
    }
    var callDist = function (idProv, disSelect) {
        $.getJSON(baseUrl + "Carga/Carga/ListarDistritoCbo",
            {
                CCODDEPARTAMENTO: $("#REGION").val(),
                CCODPROVINCIA: idProv
            },
            function (i) {
                $("#COD_DISTRITO").empty();
                $.each(i, function (i, t) {
                    $("#COD_DISTRITO").append($("<option></option>").val(t.CNOMDISTRITO).html(t.CNOMDISTRITO));
                    if (disSelect.trim() != '') {
                        $("#COD_DISTRITO").val(disSelect);
                    }
                })
            });

    }

    var fn_Mantenimiento_FAG = function (idRegistro) {
        if ($("#TXT_COD_CONTRATO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar un código de contrato");
            return false;
        }
        if ($("#TXT_ESTADO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe seleccionar un estado");
            return false;
        }
        if ($("#TXT_CARGO_FUNCIONARIO_SUSCRIBE").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el cargo de funcionario suscribe");
            return false;
        }
        if ($("#TXT_IMPORTE_HONORARIO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar importe honorario");
            return false;
        }
        if ($("#TXT_FEC_INICIO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de inicio");
            return false;
        }
        if ($("#TXT_FEC_CULMINACION").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de culminación");
            return false;
        }

        if ($("#TXT_APELLIDO_PATERNO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el apellido paterno");
            return false;
        }
        if ($("#TXT_NOMBRES").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el nombre");
            return false;
        }
        if ($("#TXT_DNI").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el documento de identidad");
            return false;
        }
        if ($("#TXT_RUC").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el RUC");
            return false;
        }
        if ($("#TXT_SEXO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe seleccionar el sexo");
            return false;
        }
        if ($("#TXT_FEC_NACIMIENTO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de nacimiento");
            return false;
        }
        if ($("#TXT_NIVEL_GOBIERNO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el nivel de gobierno");
            return false;
        }
       if ($("#TXT_CODIGO_ENTIDAD").val() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la Entidad");
            return false;
        }

        if ($("#TXT_ADENDA_VIG_NRO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el número de la Adenda");
            return false;
        }
        if ($("#TXT_ADENDA_VIG_FEC_INICIO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de Inicio de la Adenda");
            return false;
        }
        if ($("#TXT_ADENDA_VIG_FEC_FIN").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de Fin de la Adenda");
            return false;
        }

        if ($("#TXT_UNIVERSIDAD").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el nombre de la Univerdidad");
            return false;
        }
        if ($("#TXT_GRADO_ACADEMICO_GEN").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el grado académico GEN");
            return false;
        }
        if ($("#TXT_GRADO_ACADEMICO_FAG").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el grado académico FAG");
            return false;
        }
        if ($("#TXT_GRADO_ACADEMICO_ESP").val() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el grado académico ESP");
            return false;
        }
        if ($("#TXT_FLAG_REG_SUNEDU").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el título según SUNEDU");
            return false;
        }
        if ($("#TXT_EXP_LAB_GENERAL").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la experiencia laboral general");
            return false;
        }
        if ($("#TXT_EXP_LAB_ESPECIFICA").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la experiencia laboral específica");
            return false;
        }
        if ($("#TXT_EXP_LAB_GENERAL_GRADO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la experiencia general");
            return false;
        }
        if ($("#TXT_EXP_LAB_ACT_TDRS").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la experiencia en la materia");
            return false;
        }

        if ($("#TXT_RES_CONTRATO_FEC_CULM").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de culminación del Contrato");
            return false;
        }

        if ($("#TXT_ENT_FIN_NOMBRE").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el nombre de la entidad financiera");
            return false;
        }
        if ($("#TXT_ENT_FIN_CTA_AHORRO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el número de cuenta de ahorros");
            return false;
        }


        item = {
            ID_REGISTRO: idRegistro,
            COD_CONTRATO: $("#TXT_COD_CONTRATO").val(),
            FEC_REGISTRO: $("#TXT_FEC_REGISTRO").val(),
            ESTADO: $("#TXT_ESTADO").val(),
            NRO_EXP_SISPER: $("#TXT_NRO_EXP_SISPER").val(),
            LEY31419: $('#TXT_LEY31419').val(),
            APELLIDO_PATERNO: $("#TXT_APELLIDO_PATERNO").val(),
            APELLIDO_MATERNO: $("#TXT_APELLIDO_MATERNO").val(),
            NOMBRES: $('#TXT_NOMBRES').val(),
            DNI: $('#TXT_DNI').val(),
            RUC: $('#TXT_RUC').val(),
            SEXO: $('#TXT_SEXO').val(),
            FEC_NACIMIENTO: $('#TXT_FEC_NACIMIENTO').val() ,
            EDAD: $('#TXT_EDAD').val(),
            TELEFONO_CELULAR: $('#TXT_TELEFONO_CELULAR').val(),
            CORREO_ELECTRONICO: $('#TXT_CORREO_ELECTRONICO').val(),
            DIRECCION: $('#TXT_DIRECCION').val(),
            DISTRITO: $('#TXT_DISTRITO').val(),
            PROVINCIA: $('#TXT_PROVINCIA').val(),
            REGION: $('#TXT_REGION').val(),
            NIVEL_GOBIERNO: $('#TXT_NIVEL_GOBIERNO').val(),
            CODIGO_ENTIDAD: $('#TXT_CODIGO_ENTIDAD').val(),
            TIPO_DOC_SOLICITA_REG: $('#TXT_TIPO_DOC_SOLICITA_REG').val(),
            FEC_DOC_SOLICITA_REG: $('#TXT_FEC_DOC_SOLICITA_REG').val(),
            HR_DOC_SOLICITA_REG: $('#TXT_HR_DOC_SOLICITA_REG').val(),
            ENTIDAD_BENEF: $('#TXT_ENTIDAD_BENEF').val(),
            FLAG_DESIGNADO: $('#TXT_FLAG_DESIGNADO').val(),
            CARGO_FUNCIONARIO_SUSCRIBE: $('#TXT_CARGO_FUNCIONARIO_SUSCRIBE').val(),
            IMPORTE_HONORARIO: $('#TXT_IMPORTE_HONORARIO').val(),
            FEC_SUSCRIPCION: $('#TXT_FEC_SUSCRIPCION').val(),
            FEC_INICIO: $('#TXT_FEC_INICIO').val(),
            FEC_CULMINACION: $('#TXT_FEC_CULMINACION').val(),
            FEC_RECEPCION: $('#TXT_FEC_RECEPCION').val(),
            NRO_OFICIO: $('#TXT_NRO_OFICIO').val(),
            EXP_REMITIDO: $('#TXT_EXP_REMITIDO').val(),
            ADENDA_VIG_NRO: $('#TXT_ADENDA_VIG_NRO').val(),
            ADENDA_VIG_FEC_INICIO: $('#TXT_ADENDA_VIG_FEC_INICIO').val(),
            ADENDA_VIG_FEC_FIN: $('#TXT_ADENDA_VIG_FEC_FIN').val(),
            ADENDA_VIG_HOJARUTA: $('#TXT_ADENDA_VIG_HOJARUTA').val(),
            ADENDA_VIG_SUSCRIPCION: $('#TXT_ADENDA_VIG_SUSCRIPCION').val(),
            GRADO_ACADEMICO_GEN: $('#TXT_GRADO_ACADEMICO_GEN').val(),
            GRADO_ACADEMICO_FAG: $('#TXT_GRADO_ACADEMICO_FAG').val(),
            UNIVERSIDAD: $('#TXT_UNIVERSIDAD').val(),
            FLAG_REG_SUNEDU: $('#TXT_FLAG_REG_SUNEDU').val(),
            REQ_HAB_PROF: $('#TXT_REQ_HAB_PROF').val(),
            GRADO_ACADEMICO_ESP: $('#TXT_GRADO_ACADEMICO_ESP').val(),
            CARRERA_PROFESIONAL: $('#TXT_CARRERA_PROFESIONAL').val(),
            FISCAL_POST: $('#TXT_FISCAL_POST').val(),
            EXP_LAB_GENERAL: $('#TXT_EXP_LAB_GENERAL').val(),
            EXP_LAB_ESPECIFICA: $('#TXT_EXP_LAB_ESPECIFICA').val(),
            EXP_LAB_GENERAL_GRADO: $('#TXT_EXP_LAB_GENERAL_GRADO').val(),
            EXP_LAB_ACT_TDRS: $('#TXT_EXP_LAB_ACT_TDRS').val(),
            RES_CONTRATO_FEC_CULM: $('#TXT_RES_CONTRATO_FEC_CULM').val(),
            RES_CONTRATO_DOC: $('#TXT_RES_CONTRATO_DOC').val(),
            RES_CONTRATO_HR: $('#TXT_RES_CONTRATO_HR').val(),
            SEC_RESPONSABLE: $('#TXT_SEC_RESPONSABLE').val(),
            ENT_FIN_NOMBRE: $('#TXT_ENT_FIN_NOMBRE').val(),
            ENT_FIN_CTA_AHORRO: $('#TXT_ENT_FIN_CTA_AHORRO').val(),
            PJE_PUESTO: $('#TXT_PJE_PUESTO').val()
        };
        var url = baseUrl + 'Carga/Carga/ActualizarCargaCabecera';
        var respuesta = DESARROLLO.Ajax(url, item, false);
        if (respuesta != null && respuesta != "") {
            if (!respuesta.IsError) {
                DESARROLLO.PROCESO_CORRECTO("El contrato se  actualizo correctamente.");
                Carga.fn_CerrarMantenimiento();
            } else {
                DESARROLLO.PROCESO_ERROR(respuesta.Mensaje, "Mensaje Alerta");
            }
        }
    }

    var fn_Mantenimiento_PAC = function (idRegistro) {
        if ($("#TXT_COD_CONTRATO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar un código de contrato");
            return false;
        }
        if ($("#TXT_FEC_REGISTRO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe imgresar la fecha de registro");
            return false;
        }
        if ($("#TXT_ESTADO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe seleccionar un estado");
            return false;
        }
        if ($("#TXT_NIVEL_GOBIERNO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el nivel de gobierno");
            return false;
        }
        if ($("#TXT_CODIGO_ENTIDAD").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la Entidad");
            return false;
        }
        if ($("#TXT_ENTIDAD_BENEF").val() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la Entidad Benef.");
            return false;
        }
        if ($("#TXT_CARGO_FUNCIONARIO_SUSCRIBE").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el cargo de funcionario suscribe");
            return false;
        }
        if ($("#TXT_IMPORTE_HONORARIO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar importe honorario");
            return false;
        }
        if ($("#TXT_FEC_INICIO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de inicio");
            return false;
        }
        if ($("#TXT_FEC_CULMINACION").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de culminación");
            return false;
        }

        if ($("#TXT_GRADO_ACADEMICO_GEN").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el grado académico GEN");
            return false;
        }
        if ($("#TXT_GRADO_ACADEMICO_ESP").val() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el grado académico ESP");
            return false;
        }
        if ($("#TXT_UNIVERSIDAD").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el nombre de la Univerdidad");
            return false;
        }
        if ($("#TXT_EXP_LAB_GENERAL").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la experiencia laboral general");
            return false;
        }
        if ($("#TXT_EXP_LAB_ESPECIFICA").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la experiencia laboral específica");
            return false;
        }

        if ($("#TXT_APELLIDO_PATERNO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el apellido paterno");
            return false;
        }
        if ($("#TXT_NOMBRES").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el nombre");
            return false;
        }
        if ($("#TXT_DNI").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el documento de identidad");
            return false;
        }
        if ($("#TXT_RUC").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el RUC");
            return false;
        }
        if ($("#TXT_SEXO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe seleccionar el sexo");
            return false;
        }
        if ($("#TXT_FEC_NACIMIENTO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar la fecha de nacimiento");
            return false;
        }

        if ($("#TXT_ENT_FIN_NOMBRE").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el nombre de la entidad financiera");
            return false;
        }
        if ($("#TXT_ENT_FIN_CTA_AHORRO").val().trim() == "") {
            DESARROLLO.PROCESO_ALERT("Alerta", "Debe ingresar el número de cuenta de ahorros");
            return false;
        }

        item = {
            ID_REGISTRO:idRegistro,
            COD_CONTRATO: $("#TXT_COD_CONTRATO").val(),
            ALERTA_LA: $("#TXT_ALERTA_LA").val(),
            ALERTA_LA_PERSONA: $("#TXT_ALERTA_LA_PERSONA").val(),
            FEC_REGISTRO: $("#TXT_FEC_REGISTRO").val(),
            MODALIDAD: $("#TXT_MODALIDAD").val(),
            ESTADO: $("#TXT_ESTADO").val(),
            NRO_EXP_SISPER: $("#TXT_NRO_EXP_SISPER").val(),
            APELLIDO_PATERNO: $("#TXT_APELLIDO_PATERNO").val(),
            APELLIDO_MATERNO: $("#TXT_APELLIDO_MATERNO").val(),
            NOMBRES: $('#TXT_NOMBRES').val(),
            DNI: $('#TXT_DNI').val(),
            RUC: $('#TXT_RUC').val(),
            SEXO: $('#TXT_SEXO').val(),
            FEC_NACIMIENTO: $('#TXT_FEC_NACIMIENTO').val(),
            EDAD: $('#TXT_EDAD').val(),
            NACIONALIDAD: $('#TXT_NACIONALIDAD').val(),
            PADRE: $('#TXT_PADRE').val(),
            TELEFONO_CELULAR: $('#TXT_TELEFONO_CELULAR').val(),
            CORREO_ELECTRONICO: $('#TXT_CORREO_ELECTRONICO').val(),
            DIRECCION: $('#TXT_DIRECCION').val(),
            DISTRITO: $('#COD_DISTRITO').val(),
            PROVINCIA: $('#COD_PROVINCIA').val(),
            REGION: $('#REGION').val(),
            NIVEL_GOBIERNO: $('#TXT_NIVEL_GOBIERNO').val(), 
            CODIGO_ENTIDAD: $('#TXT_CODIGO_ENTIDAD').val(),
            TIPO_DOC_SOLICITA_REG: $('#TXT_TIPO_DOC_SOLICITA_REG').val(),
            FEC_DOC_SOLICITA_REG: $('#TXT_FEC_DOC_SOLICITA_REG').val(),
            HR_DOC_SOLICITA_REG: $('#TXT_HR_DOC_SOLICITA_REG').val(),
            DOC_RES_MINISTERIAL: $('#TXT_DOC_RES_MINISTERIAL').val(),
            ENTIDAD_BENEF: $('#TXT_ENTIDAD_BENEF').val(),
            CARGO_FUNCIONARIO_SUSCRIBE: $('#TXT_CARGO_FUNCIONARIO_SUSCRIBE').val(),
            IMPORTE_HONORARIO: $('#TXT_IMPORTE_HONORARIO').val(),
            FEC_SUSCRIPCION: $('#TXT_FEC_SUSCRIPCION').val(),
            FEC_INICIO: $('#TXT_FEC_INICIO').val(),
            FEC_CULMINACION: $('#TXT_FEC_CULMINACION').val(),
            FEC_RECEPCION: $('#TXT_FEC_RECEPCION').val(),
            NRO_OFICIO: $('#TXT_NRO_OFICIO').val(),
            EXP_REMITIDO: $('#TXT_EXP_REMITIDO').val(),
            ADENDA_VIG_DOC_SOL: $('#TXT_ADENDA_VIG_DOC_SOL').val(),
            ADENDA_VIG_NRO: $('#TXT_ADENDA_VIG_NRO').val(),
            ADENDA_VIG_FEC_INICIO: $('#TXT_ADENDA_VIG_FEC_INICIO').val(),
            ADENDA_VIG_FEC_FIN: $('#TXT_ADENDA_VIG_FEC_FIN').val(),
            ADENDA_VIG_HOJARUTA: $('#TXT_ADENDA_VIG_HOJARUTA').val(),
            NOT_FEC_RECEPCION: $('#TXT_NOT_FEC_RECEPCION').val(),
            NOT_DOCUMENTO: $('#TXT_NOT_DOCUMENTO').val(),
            FEC_CULMINACION_CONTRATO: $('#TXT_FEC_CULMINACION_CONTRATO').val(),
            GRADO_ACADEMICO_GEN: $('#TXT_GRADO_ACADEMICO_GEN').val(),
            UNIVERSIDAD: $('#TXT_UNIVERSIDAD').val(),
            GRADO_ACADEMICO_ESP: $('#TXT_GRADO_ACADEMICO_ESP').val(),
            CARRERA_PROFESIONAL: $('#TXT_CARRERA_PROFESIONAL').val(),
            FISCAL_POST: $('#TXT_FISCAL_POST').val(),
            EXP_LAB_GENERAL: $('#TXT_EXP_LAB_GENERAL').val(),
            EXP_LAB_ESPECIFICA: $('#TXT_EXP_LAB_ESPECIFICA').val(),
            RES_CONTRATO_FEC_CULM: $('#TXT_RES_CONTRATO_FEC_CULM').val(),
            RES_CONTRATO_DOC: $('#TXT_RES_CONTRATO_DOC').val(),
            RES_CONTRATO_HR: $('#TXT_RES_CONTRATO_HR').val(),
            SEC_RESPONSABLE: $('#TXT_SEC_RESPONSABLE').val(),
            ENT_FIN_NOMBRE: $('#TXT_ENT_FIN_NOMBRE').val(),
            ENT_FIN_CTA_AHORRO: $('#TXT_ENT_FIN_CTA_AHORRO').val(),
            PJE_PUESTO: $('#TXT_PJE_PUESTO').val()
        };

        console.log(item);
        var url = baseUrl + 'Carga/Carga/ActualizarCargaCabecera';
        var respuesta = DESARROLLO.Ajax(url, item, false);
        if (respuesta != null && respuesta != "") {
            if (!respuesta.IsError) {
                DESARROLLO.PROCESO_CORRECTO("El contrato se  actualizo correctamente.");
                Carga.fn_CerrarMantenimiento();
            } else {
                DESARROLLO.PROCESO_ERROR(respuesta.Mensaje,"Mensaje Alerta");
            }
        }
    }

    return {
        Inicio: function () { Inicio(); },
        ModalIniciar: function (idRegistro) { ModalInit(idRegistro); },
        fn_Mantenimiento_FAG: function (idRegistro) { fn_Mantenimiento_FAG(idRegistro); },
        fn_Mantenimiento_PAC: function (idRegistro) { fn_Mantenimiento_PAC(idRegistro); },
        CallProv: function (idDep, provSelect) { callProv(idDep, provSelect); },
        CallDist: function (idProv, disSelect) { callDist(idProv, disSelect); }

    }
}();
// Use datepicker on the date inputs
//$("input[type=date]").datepicker({
//    dateFormat: 'dd-mm-yyyy',
//    onSelect: function (dateText, inst) {
//        $(inst).val(dateText); // Write the value in the input
//    }
//});

// Code below to avoid the classic date-picker
$("input[type=date]").on('click', function () {
    return false;
});

$("#REGION").change(function () {
    var e = $(this).val();
    CargaMantenimiento.CallProv(e, "");
});
$("#COD_PROVINCIA").change(function () {
    var i = $(this).val();
    CargaMantenimiento.CallDist(i, "");
});