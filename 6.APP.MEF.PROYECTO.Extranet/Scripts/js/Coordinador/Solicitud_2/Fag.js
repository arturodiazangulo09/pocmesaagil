/*INICIO REGISTRO ARCHIVO MULTIPLE DOCUMENTO*/
$("#FileArchivoSust").change(function (e) {
    var val = $(this).val();
    var input = document.getElementById('FileArchivoSust');
    var file = input.files[0];
    var filename = e.target.files[0].name;
    if (file.size > 11534300) {
        $(this).val('');
        DESARROLLO.PROCESO_ALERT('Alerta', "El archivo que está subiendo pesa más de 10Mb");
        return false
    }
    switch (val.substring(val.lastIndexOf('.') + 1).toLowerCase()) {
        case 'pdf':
            if (DESARROLLO.VALIDAR_CONTEN_PDF("FileArchivoSust")) {
                $('#archivo_sust').text(filename);
                return true;
            }
            break;
        default:
            $(this).val('');
            DESARROLLO.PROCESO_ALERT('Alerta', "formato de archivo no válido, solo se permite archivos .pdf");
            break;
    }
});
function ValidArchivo() {
    var result = true;
    if ($('#FileArchivoSust')[0].files.length === 0) { $("#LblfileArchivoSust").text("Es necesario que adjunte el documento de sustento."); result = false; } else { $("#LblfileArchivoSust").text(""); }
    if ($('#txtDesArchivo').val().trim().length == 0) {
        $('#txtDesArchivo').val("").focus();
        $("#txtDesArchivo").addClass('is-invalid');
        result = false;
    } else {
        $("#txtDesArchivo").removeClass('is-invalid');
    }
    return result;
}
function fn_Aregar_Archivo() {
    if (ValidArchivo()) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea registrar el archivo de sustento.?", function (r) {
            if (r) {
                var doc = document.getElementById("FileArchivoSust").files.length;
                var e = new FormData;
                if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoSust").files[l], e.append("FileArchivoSust", a);
                let theForm = $("#btnAgregarArchivo").parents('form:first').attr('id');
                let items = GetData('#' + theForm);

                e.append("RUTA_ARCHIVO", "0");
                e.append("ID_DOCUMENTO", 0);
                e.append("FLG_TIPO", "S");
                e.append("ID_PROCESO", $('#ID_SOLICITUD').val());
                e.append("DES_DOCUMENTO", $('#txtDesArchivo').val());
                e.append("Items", JSON.stringify(items));
                $.ajax({
                    url: baseUrl + "Coordinador/Solicitud/GenerarHTML_Multiple",
                    type: "POST",
                    data: e,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    contentType: !1,
                    processData: !1,
                    success: function (p) {
                        if (p.success) {
                            $("#FileArchivoSust").val("");
                            $("#archivo_sust").text("0 Archivos");
                            $("#txtDesArchivo").val("");
                            $("#gridBody").html(p.extra);
                        }
                        else {
                            DESARROLLO.PROCESO_ERROR(p.message);
                        }
                    }
                });
            }
        })
    }
}
function GetData(theForm = "#frmNuevaSolicitudFag") {
    var dataRequest = new Array();
    $(theForm).find("#Tabla_Documento" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_DOCUMENTO = entorno.find(".tdID_DOCUMENTO", "id").html();
        var ID_PROCESO = entorno.find(".tdID_PROCESO", "id").html();
        var FLG_TIPO = entorno.find(".tdFLG_TIPO", "id").html();
        var ID_LF = entorno.find(".tdID_LF", "id").html();
        var NOM_DOCUMENTO = entorno.find(".tdNOM_DOCUMENTO", "id").html();
        var DES_DOCUMENTO = entorno.find("input.tdDES_DOCUMENTO", "id").val();
        dataRequest.push({
            ID_DOCUMENTO: ID_DOCUMENTO,
            ID_PROCESO: ID_PROCESO,
            FLG_TIPO: FLG_TIPO,
            ID_LF: ID_LF,
            NOM_DOCUMENTO: NOM_DOCUMENTO,
            DES_DOCUMENTO: DES_DOCUMENTO,
        });
    });
    return dataRequest;
}
function fn_MostrarArchivoInicial() {
    var e = new FormData;
    e.append("ID_DOCUMENTO", 1);
    e.append("FLG_TIPO", "S");
    e.append("ID_PROCESO", $('#ID_SOLICITUD').val());
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/GenerarHTML_Archivo",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                $("#gridBody").html(p.extra);
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.message);
            }
        }
    });
}
function fn_MostrarDocumentoInicial() {
    var e = new FormData;
    e.append("ID_DOCUMENTO", 2);
    e.append("FLG_TIPO", "S");
    e.append("ID_PROCESO", $('#ID_SOLICITUD').val());
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/GenerarHTML_Archivo",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                $("#gridBody").html(p.extra);
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.message);
            }
        }
    });
}
/*FIN REGISTRO ARCHIVO MULTIPLE*/
/*INICIO FORMACION*/
function fn_Aregar_Formacion_V2() {
    var cant = 0;
    if (ValidFormacion()) {
        fn_AgregarFormacion();
    }
}
function ValidFormacion() {
    var result = true;
    var serepite = true;

    if ($('#txtFormacion').val().trim().length == 0) {
        $('#txtFormacion').val("").focus();
        $("#txtFormacion").addClass('is-invalid');
        result = false;
    } else {
        $("#txtFormacion").removeClass('is-invalid');
    }

    if ($('#ID_GRADO_ACADEMICO').val()=="") {
        $("#ID_GRADO_ACADEMICO").addClass('is-invalid');
        result = false;
    } else {
        $("#ID_GRADO_ACADEMICO").removeClass('is-invalid');
    }

    serepite = ValidarRepeticionFormacion($('#ID_GRADO_ACADEMICO').val(), $('#txtFormacion').val())
    result = !serepite;
    if (serepite) { //se repite
        DESARROLLO.PROCESO_ALERT("Información académica ya existe");
        $("#txtFormacion").addClass('is-invalid');
        $("#ID_GRADO_ACADEMICO").addClass('is-invalid');
    } else {
        $("#ID_GRADO_ACADEMICO").removeClass('is-invalid');
        $("#txtFormacion").removeClass('is-invalid');
    }

    return result;
}
function ValidarRepeticionFormacion(grado, descripcion) {
    serepite = false;
    $("#frmNuevaSolicitudFag").find("#Tabla_Formacion" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_NIVEL_GRADO = entorno.find(".tdID_NIVEL_GRADO", "id").html();
        var DESC_ACADEMICA = entorno.find("input.tdDESC_ACADEMICA", "id").val();
        if ((ID_NIVEL_GRADO + DESC_ACADEMICA) == (grado + descripcion)) {
            serepite= true;
        }
    });
    return serepite;
}
function fn_AgregarFormacion() {
    DESARROLLO.PROCESO_CONFIRM("¿Desea <b>Registrar</b> la formación académica.?", function (r) {
        if (r) {
            let theForm = $("#btnAgregarFormacion").parents('form:first').attr('id');
            console.log(theForm);
            item = {
                ID_ACADEMICA: "0",
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
                DESC_ACADEMICA: $("#txtFormacion").val(),
                ID_NIVEL_GRADO: $("#ID_GRADO_ACADEMICO").val(),
                NOMBRE_NIVEL: $('select[name="ID_GRADO_ACADEMICO"] option:selected').text(),
                Items: GetDataFormacion('#' + theForm)
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarFormacionHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#ID_GRADO_ACADEMICO").val("");
                    $("#txtFormacion").val("");
                    $('#txtFormacion').val('').focus();
                    $("#gridBodyForm").html(respuesta.extra);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.message);
                }

            }
        }
    })
}
function GetDataFormacion(theForm = "#frmNuevaSolicitudFag") {
    var dataRequest = new Array();
    $(theForm).find("#Tabla_Formacion" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_ACADEMICA = entorno.find(".tdID_ACADEMICA", "id").html();
        var ID_SOLICITUD = entorno.find(".tdID_SOLICITUD", "id").html();
        var ID_NIVEL_GRADO = entorno.find(".tdID_NIVEL_GRADO", "id").html();
        var DESC_ACADEMICA = entorno.find("input.tdDESC_ACADEMICA", "id").val();
        var NOMBRE_NIVEL = entorno.find("label.tdNOMBRE_NIVEL", "id").text();
        dataRequest.push({
            ID_ACADEMICA: ID_ACADEMICA,
            ID_SOLICITUD: ID_SOLICITUD,
            ID_NIVEL_GRADO: ID_NIVEL_GRADO,
            DESC_ACADEMICA: DESC_ACADEMICA,
            NOMBRE_NIVEL: NOMBRE_NIVEL,
        });
    });
    return dataRequest;
}

function fn_MostrarFormacionIni() {
            item = {
                ID_ACADEMICA: 1,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarFormacionHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyForm").html(respuesta.extra);
                } 
            }
}
/*FIN FORMACION*/
/*INICIO CURSO*/
function fn_Aregar_Curso_V2() {
    var cant = 0;
    if ($('#txtCurso').val().trim().length == 0) {
        $('#txtCurso').val("").focus();
        $("#txtCurso").addClass('is-invalid')
    } else {
        fn_AgregarCurso();
    }
}
function fn_AgregarCurso() {
    DESARROLLO.PROCESO_CONFIRM("¿Desea <b>Registrar</b> el Cursos y/o Programas de Especialización.?", function (r) {
        if (r) {
            let theForm = $("#btnAgregarCurso").parents('form:first').attr('id');
            item = {
                ID_CURSOS_PRO: "0",
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
                DESC_CURSO_PRO: $("#txtCurso").val(),
                Items: GetDataCurso('#' + theForm)
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarCursoHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyCurs").html(respuesta.extra);
                    $('#txtCurso').val("").focus();
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.message);
                }

            }
        }
    })
}
function GetDataCurso(theForm = "#frmNuevaSolicitudFag") {
    var dataRequest = new Array();
    $(theForm).find("#Tabla_Curso" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_CURSOS_PRO = entorno.find(".tdID_CURSOS_PRO", "id").html();
        var ID_SOLICITUD = entorno.find(".tdID_SOLICITUD", "id").html();
        var DESC_CURSO_PRO = entorno.find("input.tdDESC_CURSO_PRO", "id").val();
        dataRequest.push({
            ID_CURSOS_PRO: ID_CURSOS_PRO,
            ID_SOLICITUD: ID_SOLICITUD,
            DESC_CURSO_PRO: DESC_CURSO_PRO,
        });
    });
    return dataRequest;
}
function fn_MostrarCursoIni() {
            item = {
                ID_CURSOS_PRO: 1,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarCursoHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyCurs").html(respuesta.extra);
                } 
            }
}
/*FIN CURSO*/
/*INICIO CONO*/
function fn_Aregar_Cono_V2() {
    var cant = 0;
    if ($('#txtConocimientos').val().trim().length == 0) {
        $('#txtConocimientos').val("").focus();
        $("#txtConocimientos").addClass('is-invalid')
    } else {
        fn_AgregarCono();
    }
}
function fn_AgregarCono() {
    DESARROLLO.PROCESO_CONFIRM("¿Desea <b>Registrar</b> conocimientos.?", function (r) {
        if (r) {
            let theForm = $("#btnAgregarConocimientos").parents('form:first').attr('id');
            item = {
                ID_CONOCIMIENTOS: "0",
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
                DESC_CONOCIMIENTO: $("#txtConocimientos").val(),
                Items: GetDataCono('#'+theForm)
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarConoHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyCono").html(respuesta.extra);
                    $('#txtConocimientos').val("").focus();
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.message);
                }

            }
        }
    })
}
function GetDataCono(theForm = "#frmNuevaSolicitudFag") {
    var dataRequest = new Array();
    $(theForm).find("#Tabla_Cono" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_CONOCIMIENTOS = entorno.find(".tdID_CONOCIMIENTOS", "id").html();
        var ID_SOLICITUD = entorno.find(".tdID_SOLICITUD", "id").html();
        var DESC_CONOCIMIENTO = entorno.find("input.tdDESC_CONOCIMIENTO", "id").val();
        dataRequest.push({
            ID_CONOCIMIENTOS: ID_CONOCIMIENTOS,
            ID_SOLICITUD: ID_SOLICITUD,
            DESC_CONOCIMIENTO: DESC_CONOCIMIENTO,
        });
    });
    return dataRequest;
}
function fn_MostrarConoini() {
            item = {
                ID_CONOCIMIENTOS: 1,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarConoHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyCono").html(respuesta.extra);
                } 
            }
}
/*FIN CONO*/
/*INICIO EXP*/
function fn_Aregar_Exp_V2() {
    var cant = 0;
    if (ValidExp()) {
        fn_AgregarExp();
    }
}
function ValidExp() {
    var result = true;
    if ($('#ID_TIPO_EXPERIENCIA').val() == "") {
        $("#ID_TIPO_EXPERIENCIA").addClass('is-invalid');
        result = false;
    } else {
        $("#ID_TIPO_EXPERIENCIA").removeClass('is-invalid');
    }
    if ($('#ID_TIPO_SECTOR').val() == "") {
        $("#ID_TIPO_SECTOR").addClass('is-invalid');
        result = false;
    } else {
        $("#ID_TIPO_SECTOR").removeClass('is-invalid');
    }
    if ($('#txtAnioExperiencia').val().trim().length == 0) {
        $('#txtAnioExperiencia').val("").focus();
        $("#txtAnioExperiencia").addClass('is-invalid');
        result = false;
    } else {
        $("#txtAnioExperiencia").removeClass('is-invalid');
    }
    if ($('#txtExperiencia').val().trim().length == 0) {
        $('#txtExperiencia').val("").focus();
        $("#txtExperiencia").addClass('is-invalid');
        result = false;
    } else {
        $("#txtExperiencia").removeClass('is-invalid');
    }
    return result;
}
function fn_AgregarExp() {
    DESARROLLO.PROCESO_CONFIRM("¿Desea <b>Registrar</b> la experiencia.?", function (r) {
        if (r) {
            let theForm = $("#btnAgregarExperiencia").parents('form:first').attr('id');
            item = {
                ID_EXPERIENCIA : 0,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
                DESC_EXPERIENCIA: $("#txtExperiencia").val(),
                ID_TIPO_EXPERIENCIA: $("#ID_TIPO_EXPERIENCIA").val(),
                ANOS: $("#txtAnioExperiencia").val(),
                ID_TIPO_SECTOR: $("#ID_TIPO_SECTOR").val(),
                DESC_TIPO_EXPERIENCIA: $('select[name="ID_TIPO_EXPERIENCIA"] option:selected').text(),
                DESC_TIPO_SECTOR: $('select[name="ID_TIPO_SECTOR"] option:selected').text(),
                Items: GetDataExp('#' + theForm)
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarExpHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyExp").html(respuesta.extra);
                    $('#ID_TIPO_EXPERIENCIA').val("").focus();
                    $('#ID_TIPO_SECTOR').val("");
                    $('#txtAnioExperiencia').val("");
                    $('#txtExperiencia').val("");
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.message);
                }

            }
        }
    })
}
function GetDataExp(theForm = "#frmNuevaSolicitudFag") {
    var dataRequest = new Array();
    $(theForm).find("#Tabla_Exp" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_EXPERIENCIA = entorno.find(".tdID_EXPERIENCIA", "id").html();
        var ID_SOLICITUD = entorno.find(".tdID_SOLICITUD", "id").html();
        var ID_TIPO_EXPERIENCIA = entorno.find(".tdID_TIPO_EXPERIENCIA", "id").html();
        var ID_TIPO_SECTOR = entorno.find(".tdID_TIPO_SECTOR", "id").html();
        var ANOS = entorno.find("input.tdANOS", "id").val();
        var DESC_EXPERIENCIA = entorno.find("input.tdDESC_EXPERIENCIA", "id").val();
        var DESC_TIPO_EXPERIENCIA = entorno.find("label.tdDESC_TIPO_EXPERIENCIA", "id").text();
        var DESC_TIPO_SECTOR = entorno.find("label.tdDESC_TIPO_SECTOR", "id").text();
        dataRequest.push({
            ID_EXPERIENCIA: ID_EXPERIENCIA,
            ID_SOLICITUD: ID_SOLICITUD,
            ID_TIPO_EXPERIENCIA: ID_TIPO_EXPERIENCIA,
            ID_TIPO_SECTOR: ID_TIPO_SECTOR,
            ANOS: ANOS,
            DESC_EXPERIENCIA: DESC_EXPERIENCIA,
            DESC_TIPO_EXPERIENCIA: DESC_TIPO_EXPERIENCIA,
            DESC_TIPO_SECTOR: DESC_TIPO_SECTOR,
        });
    });
    return dataRequest;
}
function fn_MostrarExpini() {
            item = {
                ID_EXPERIENCIA: 1,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarExpHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyExp").html(respuesta.extra);
                } 
            }
}
/*FIN EXP*/
/*INICIO ACTIVIDAD*/
function fn_Aregar_Act_V2() {
    var cant = 0;
    if ($('#txtDescActividad').val().trim().length == 0) {
        $('#txtDescActividad').val("").focus();
        $("#txtDescActividad").addClass('is-invalid')
    } else {
        fn_AgregarAct();
    }
}
function fn_AgregarAct() {
    DESARROLLO.PROCESO_CONFIRM("¿Desea <b>Registrar</b> actividad.?", function (r) {
        if (r) {
            let theForm = $("#btnAgregarActividad").parents('form:first').attr('id');
            item = {
                ID_ACTIVIDAD: 0,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
                DESC_ACTIVIDAD: $("#txtDescActividad").val(),
                Items: GetDataAct('#' + theForm)
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarActHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyAct").html(respuesta.extra);
                    $('#txtDescActividad').val("").focus();
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.message);
                }

            }
        }
    })
}
function GetDataAct(theForm = "#frmNuevaSolicitudFag") {
    var dataRequest = new Array();
    $(theForm).find("#Tabla_Actividad" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_ACTIVIDAD = entorno.find(".tdID_ACTIVIDAD", "id").html();
        var ID_SOLICITUD = entorno.find(".tdID_SOLICITUD", "id").html();
        var DESC_ACTIVIDAD = entorno.find("input.tdDESC_ACTIVIDAD", "id").val();
        dataRequest.push({
            ID_ACTIVIDAD: ID_ACTIVIDAD,
            ID_SOLICITUD: ID_SOLICITUD,
            DESC_ACTIVIDAD: DESC_ACTIVIDAD,
        });
    });
    return dataRequest;
}
function fn_MostrarActini() {
            item = {
                ID_ACTIVIDAD: 1,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarActHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyAct").html(respuesta.extra);
                } 
            }
}
/*FIN ACTIVIDAD*/
/*INICIO ACTIVIDAD OTRO*/
function fn_Aregar_ActOtro_V2() {
    var cant = 0;
    if ($('#txtDescOtro').val().trim().length == 0) {
        $('#txtDescOtro').val("").focus();
        $("#txtDescOtro").addClass('is-invalid')
    } else {
        fn_AgregarActOtro();
    }
}
function fn_AgregarActOtro() {
    DESARROLLO.PROCESO_CONFIRM("¿Desea <b>Registrar</b> otras actividades.?", function (r) {
        if (r) {
            let theForm = $("#btnAgregarOtro").parents('form:first').attr('id');
            item = {
                ID_OTRO_ACT: 0,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
                DESC_ACT_OTRO: $("#txtDescOtro").val(),
                Items: GetDataActOtro('#'+theForm)
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarActOtroHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyActOtro").html(respuesta.extra);
                    $('#txtDescOtro').val("").focus();
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.message);
                }

            }
        }
    })
}
function GetDataActOtro(theForm = "#frmNuevaSolicitudFag") {
    var dataRequest = new Array();
    $(theForm).find("#Tabla_ActOtro" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_OTRO_ACT = entorno.find(".tdID_OTRO_ACT", "id").html();
        var ID_SOLICITUD = entorno.find(".tdID_SOLICITUD", "id").html();
        var DESC_ACT_OTRO = entorno.find("input.tdDESC_ACT_OTRO", "id").val();
        dataRequest.push({
            ID_OTRO_ACT: ID_OTRO_ACT,
            ID_SOLICITUD: ID_SOLICITUD,
            DESC_ACT_OTRO: DESC_ACT_OTRO,
        });
    });
    return dataRequest;
}
function fn_MostrarActOtroini() {
            item = {
                ID_OTRO_ACT: 1,
                ID_SOLICITUD: $("#ID_SOLICITUD").val(),
            };
            var url = baseUrl + 'Coordinador/ProcesoFag/GenerarActOtroHTML';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    $("#gridBodyActOtro").html(respuesta.extra);
                } 
            }
}
/*FIN ACTIVIDAD OTRO*/
/*INICIO PAGO*/
function fn_GenerarPeriodoRenumeracion_V2() {
    fn_NivelPerfilSalario();
    var i = $('#FechaInicial').val();
    var f = $('#FechaFin').val();
    var Remuneracion = parseInt($('#MONTO_MENSUAL').val());
    var Permitido = parseInt($('#txtMontoFijo').val());
    var lista = new Array();
    var Rows = "";
    var fec_inicio = new Date(i.split('/')[2], i.split('/')[1] - 1, i.split('/')[0]);
    var fec_fin = new Date(f.split('/')[2], f.split('/')[1] - 1, f.split('/')[0]);

    if (fec_inicio > fec_fin)
        return DESARROLLO.PROCESO_ALERT("Atención", "La fecha inicial no puede ser mayor a la fecha final");

    if (fec_inicio.getFullYear() != fec_fin.getFullYear())
        return DESARROLLO.PROCESO_ALERT("Atención", "El periodo inicial no puede ser diferente al periodo final");
    else
        Anio = fec_inicio.getFullYear();
    if (Remuneracion>=5000) {
        var MontInit = fec_inicio.getMonth();
        var MontFin = fec_fin.getMonth();
        var DiasMontInit = parseInt(i.split('/')[0]) - 1; // dias primer mes 
        var DiasMontFin = parseInt(f.split('/')[0]);// dias ultimo mes 
        if (Remuneracion > Permitido) {
            if (Permitido==0) {
                DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente la información de acuerdo al cuadro de <b>Requisitos mínimos</b>");
            } else {
                DESARROLLO.PROCESO_ALERT("Atención", "El honorario debe ser inferior al valor permitido");
            }
            var Table = $('#TablaMeses tbody');
            Table[0].innerHTML = "";
        } else {
            if (MontInit != MontFin) {

                var idGrid = 0
                for (var i = MontInit; i <= MontFin; i++) {
                    idGrid = idGrid + 1
                    var _Dias = diasEnUnMes(i, Anio);
                    var _Remu = Remuneracion;
                    var _disabled = "disabled";
                    if (i == MontInit) {
                        if (DiasMontInit != 0) {
                            _Dias = (_Dias - DiasMontInit);
                            _Remu = Number(((_Dias * _Remu) / 30)).toFixed(2);
                            _disabled = "";
                        }
                    }
                    if (i == MontFin) {
                        if (DiasMontFin != 0) {
                            _Dias = DiasMontFin;
                            _Remu = Number(((_Dias * _Remu) / 30)).toFixed(2);
                            _disabled = "";
                        }
                    }
                    Rows += "<tr>"
                        + "<td>" + Anio + "</td>"
                        + "<td>" + (i + 1) + "</td>"
                        + "<td>" + ModelMeses[i] + "</td>"
                        + "<td> <input type=\"text\" " + _disabled + " value=" + Remuneracion + " /></td>"
                        + "</tr>"
                }
            } else {
                Rows += "<tr>"
                    + "<td>" + Anio + "</td>"
                    + "<td>" + (MontInit + 1) + "</td>"
                    + "<td>" + ModelMeses[MontInit] + "</td>"
                    + "<td> <input type=\"text\" " + _disabled + "  value=" + Remuneracion + " /></td>"
                    + "</tr>"
            }
            var Table = $('#TablaMeses tbody');
            Table[0].innerHTML = "";
            Table[0].innerHTML = Rows;
        }

    } else {
        DESARROLLO.PROCESO_ALERT("Atención", "El honorario debe ser inferior a S/ 5,000.00");
        var Table = $('#TablaMeses tbody');
        Table[0].innerHTML = "";
    }
}
function fn_NivelPerfilSalario() {
    var monto = 0;
    let theForm = $("#btnGerarPagos").parents('form:first').attr('id');
    if (theForm == "frmWizContrato") { theForm = "frmWizRequisitos"; }
    
    item = {
        ID_SOLICITUD:0,
        listaExperiencia: GetDataExp('#' + theForm),
        listaAcademico: GetDataFormacion('#' + theForm)
    };
    var url = baseUrl + 'Coordinador/ProcesoFag/ObtenerNivelPerfil';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    console.log(respuesta.nivel);
    if (respuesta != null && respuesta != "") {
        switch (respuesta.nivel) {
            case 0:
                monto = 0;
                break;
            case 1:
                monto = 15600;
                break;
            case 2:
                monto = 13000;
                break;
            case 3:
                monto = 10000;
                break;
            case 4:
                monto = 7000;
                break;
        }
    }
    $('#txtMontoFijo').val(monto);
}
function fn_NivelPerfil() {
    var nivel = 0;
    item = {
        ID_SOLICITUD: 0,
        listaExperiencia: GetDataExp(),
        listaAcademico: GetDataFormacion()
    };
    var url = baseUrl + 'Coordinador/ProcesoFag/ObtenerNivelPerfil';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        nivel=respuesta.nivel
    }
    return nivel;
}
function fn_ValidarRenumeracion() {
    var cant = $('#TablaMeses >tbody >tr').length;
    if (cant > 0) {
        DESARROLLO.PROCESO_ALERT("Atención", "Debe generar nuevamente el periodo de remuneración, de acuerdo a los cambios realizados");
        var Table = $('#TablaMeses tbody');
        Table[0].innerHTML = "";
    }
}
function fn_NivelPerfilSalarioIni() {
    var monto = 0;
    switch ($("#FLG_NIVEL").val()) {
            case "0":
                monto = 0;
                break;
        case "1":
                monto = 15600;
                break;
        case "2":
                monto = 13000;
                break;
        case "3":
                monto = 10000;
                break;
        case "4":
                monto = 7000;
                break;
        }
    $('#txtMontoFijo').val(monto);
}
function fn_GenerarPeriodoRenumeracionCeu_V2() {
    fn_NivelPerfilSalario();
    var i = $('#FechaInicial').val();
    var f = $('#FechaFin').val();
    var Remuneracion = parseInt($('#MONTO_MENSUAL').val());
    var Rows = "";
    var fec_inicio = new Date(i.split('/')[2], i.split('/')[1] - 1, i.split('/')[0]);
    var fec_fin = new Date(f.split('/')[2], f.split('/')[1] - 1, f.split('/')[0]);

    if (fec_inicio > fec_fin)
        return DESARROLLO.PROCESO_ALERT("Atención", "La fecha inicial no puede ser mayor a la fecha final");

    if (fec_inicio.getFullYear() != fec_fin.getFullYear())
        return DESARROLLO.PROCESO_ALERT("Atención", "El periodo inicial no puede ser diferente al periodo final");
    else
        Anio = fec_inicio.getFullYear();
    if (Remuneracion = 3000) {
        var MontInit = fec_inicio.getMonth();
        var MontFin = fec_fin.getMonth();
        var DiasMontInit = parseInt(i.split('/')[0]) - 1; // dias primer mes 
        var DiasMontFin = parseInt(f.split('/')[0]);// dias ultimo mes 
            if (MontInit != MontFin) {

                var idGrid = 0
                for (var i = MontInit; i <= MontFin; i++) {
                    idGrid = idGrid + 1
                    var _Dias = diasEnUnMes(i, Anio);
                    var _Remu = Remuneracion;
                    var _disabled = "disabled";
                    if (i == MontInit) {
                        if (DiasMontInit != 0) {
                            _Dias = (_Dias - DiasMontInit);
                            _Remu = Number(((_Dias * _Remu) / 30)).toFixed(2);
                            _disabled = "";
                        }
                    }
                    if (i == MontFin) {
                        if (DiasMontFin != 0) {
                            _Dias = DiasMontFin;
                            _Remu = Number(((_Dias * _Remu) / 30)).toFixed(2);
                            _disabled = "";
                        }
                    }
                    Rows += "<tr>"
                        + "<td>" + Anio + "</td>"
                        + "<td>" + (i + 1) + "</td>"
                        + "<td>" + ModelMeses[i] + "</td>"
                        + "<td> <input type=\"text\" " + _disabled + " value=" + Remuneracion + " /></td>"
                        + "</tr>"
                }
            } else {
                Rows += "<tr>"
                    + "<td>" + Anio + "</td>"
                    + "<td>" + (MontInit + 1) + "</td>"
                    + "<td>" + ModelMeses[MontInit] + "</td>"
                    + "<td> <input type=\"text\" " + _disabled + "  value=" + Remuneracion + " /></td>"
                    + "</tr>"
            }
            var Table = $('#TablaMeses tbody');
            Table[0].innerHTML = "";
            Table[0].innerHTML = Rows;
    } else {
        DESARROLLO.PROCESO_ALERT("Atención", "El honorario debe ser de a S/ 3,000.00");
        var Table = $('#TablaMeses tbody');
        Table[0].innerHTML = "";
    }
}
/*FIN PAGO*/
/*INICIO DESIGNADO*/
function Validar_Designado_V2() {
    if ($("#FLG_CHECK_DESIGNADO").is(":checked")) {
        $('.DESG').show();

    } else {
        $('.DESG').hide();
        $('#NR_RESOLUCION').val("");
        $('#FechaAsignacion').val("");
    }

}
/*FIN DESIGNADO*/
function fn_MantenimientoSolicitudFag_V2() {
    var e = new FormData;
    var DESIG = "0";
    var NR_RESOLUCION_ = "";
    var FECHA_RESOLUCION_ = "";
    if ($("#FLG_CHECK_DESIGNADO").is(":checked")) {
        DESIG = "1";
        NR_RESOLUCION_ = $("#NR_RESOLUCION").val();
        FECHA_RESOLUCION_ = $("#FechaAsignacion").val();
    }
    var Nr_Mes = 0;
    var Meses = "";
    var Monto = 0
    var Anio = 0;
    var Lista_Periodo_pago = new Array();
    $("#TablaMeses tbody tr").each(function (index) {
        $(this).children("td").each(function (index2) {
            switch (index2) {
                case 0:
                    Anio = $(this).text();
                    break;
                case 1:
                    Nr_Mes = $(this).text();
                    break;
                case 2:
                    Meses = $(this).text();
                    break;
                case 3:
                    Monto = $(this).children('input').val();
                    break;
            }
        });
        var item = {

            DES_MES: Meses,
            ID_MES: Nr_Mes,
            MONTO: Monto,
            ANIO: Anio,
            ID_ENTIDAD: $("#inputHddIdEntidad").val(),
            TIPO_PROCESO: "F",
        };
        Lista_Periodo_pago.push(item);
    });
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("ID_ENTIDAD", $("#inputHddIdEntidad").val());
    e.append("DEPENDENCIA", $("#DEPENDENCIA").val());
    e.append("FECHA_INICIO", $("#FechaInicial").val());
    e.append("FECHA_FIN", $("#FechaFin").val());
    e.append("MONTO_MENSUAL", $("#MONTO_MENSUAL").val());
    e.append("DENOMINACION_PUESTO", $("#DENOMINACION_PUESTO").val());
    e.append("CONFORMIDAD_SERVICIO", $("#CONFORMIDAD_SERVICIO").val());
    e.append("APE_NOMB_CERTIFICA", $("#APE_NOMB_CERTIFICA").val().toUpperCase());
    e.append("NUM_DOCUMENTO_CONSULTOR", $("#NUM_DOCUMENTO_CONSULTOR").val());
    e.append("APELLIDO_PAT_CONSULTOR", $("#APELLIDO_PAT_CONSULTOR").val());
    e.append("APELLIDO_MAT_CONSULTOR", $("#APELLIDO_MAT_CONSULTOR").val());
    e.append("NOMBRES_CONSULTOR", $("#NOMBRES_CONSULTOR").val());
    e.append("CORREO_CONSULTOR", $("#CORREO_CONSULTOR").val());
    e.append("FLG_PROCESO", $("#FLG_PROCESO").val());
    e.append("FLG_VERSION", $("#FLG_VERSION").val());
    e.append("FLG_NIVEL", fn_NivelPerfil());
    e.append("OFICINA_CERTIFICA", $("#OFICINA_CERTIFICA").val().toUpperCase());
    e.append("FLG_DESIGNADO", DESIG);
    e.append("NR_RESOLUCION", NR_RESOLUCION_);
    e.append("DESC_ORGANO",  $("#DESC_ORGANO").val());
    e.append("FECHA_RESOLUCION", FECHA_RESOLUCION_);
    e.append("listaExperiencia", JSON.stringify(GetDataExp()));
    e.append("listaCurso", JSON.stringify(GetDataCurso()));
    e.append("listaAcademico", JSON.stringify(GetDataFormacion()));
    e.append("listaConocimiento", JSON.stringify(GetDataCono()));
    e.append("listaActividad", JSON.stringify(GetDataAct()));
    e.append("listaAcividadOtro", JSON.stringify(GetDataActOtro()));
    e.append("listaRenumeracion", JSON.stringify(Lista_Periodo_pago));
    e.append("listaDocumentos", JSON.stringify(GetData()));
    $.ajax({
        url: baseUrl + "Coordinador/ProcesoFag/MantenimientoSolicitudFag_V2",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarEditarSolicitudFag();
                CargarGrillaSolicitudesFag();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                if (p.message2 == "0") {
                    fn_CerrarEditarSolicitudFag();
                    CargarGrillaSolicitudesFag();
                    DESARROLLO.PROCESO_ALERT("Atención", "Se registro la solicitud, Se generó inconvenientes al registrar los archivos de la solicitud, intente nuevamente en breves minutos.");
                } else {
                    DESARROLLO.PROCESO_ERROR(p.extra);
                }
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function fn_ValidarPago_V2() {
    var Test = false;
    var Lista = new Array();
    var Nr_Mes = 0;
    var Meses = "";
    var Monto = 0
    var Anio = 0;
    $("#TablaMeses tbody tr").each(function (index) {
        $(this).children("td").each(function (index2) {
            switch (index2) {
                case 0:
                    Anio = $(this).text();
                    break;
                case 1:
                    Nr_Mes = $(this).text();
                    break;
                case 2:
                    Meses = $(this).text();
                    break;
                case 3:
                    Monto = $(this).children('input').val();
                    break;
            }
        });

        var item = {

            DES_MES: Meses,
            ID_MES: Nr_Mes,
            MONTO: Monto,
            ANIO: Anio,
            ID_ENTIDAD: $("#inputHddIdEntidad").val(),
            TIPO_PROCESO: "F",
            ID_SOLICITUD: $("#ID_SOLICITUD").val(),
        };
        Lista.push(item);
    });
    var item =
    {
        Lista: Lista,
    };
    var urls = baseUrl + 'Coordinador/Solicitud/ValidarRemuneracion';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            if (respuesta.codigo == '0') {
                DESARROLLO.PROCESO_ALERT('Verificar Periodos', respuesta.message)
            } else {
                Test = true;
            }

        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
    return Test;
}
jQuery("#btnGrabarSolicitud_V2").click(function (e) {
    var text = $("#ID_SOLICITUD").val() == 0 ? "¿Desea <b> Registrar</b> la solicitud.? </br >" : "</br>¿Desea <b> Actualizar</b> la solicitud.?";
    var text_ = $("#ID_SOLICITUD").val() == 0 ? "Para proceder con el  <b> Registro</b>  debe generar el periodo de remuneración. </br >" : "</br>Para proceder con la  <b> Actualización</b>  debe generar el periodo de remuneración.";
    var text__ = $("#ID_SOLICITUD").val() == 0 ? "Para proceder con el  <b> Registro</b>  debe registrar como mínimo un archivo de sustento. </br >" : "</br>Para proceder con la  <b> Actualización</b>  debe registrar como mínimo un archivo de sustento.";
    if ($("#frmNuevaSolicitudFag").valid()) {
            var cant = $('#TablaMeses >tbody >tr').length;
        var cant_ = $('#Tabla_Documento >tbody >tr').length;
        if (DESARROLLO.V_EMAIL("CORREO_CONSULTOR")) {
            if (cant_ > 0) {
                if (cant > 0) {
                    if (fn_ValidarPago_V2()) {
                        DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                            if (r) {
                                $("#myModalCargando").fadeIn(100, function () {
                                    fn_MantenimientoSolicitudFag_V2();
                                });

                            }
                        });
                    }
                } else {
                    DESARROLLO.PROCESO_ALERT("Atención", text_);
                }
            } else {
                DESARROLLO.PROCESO_ALERT("Atención", text__);
            }
        } else {
            DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente el correo del consultor.");
        }
        
    }
});
$("#frmNuevaSolicitudFag").validate({
    rules: {
        "OFICINA_CERTIFICA": {
            required: true,
        },
        "DEPENDENCIA": {
            required: true,
        },
        "FechaInicial": {
            required: true,
        },
        "FechaFin": {
            required: true,
        },
        "MONTO_MENSUAL": {
            required: true,
            minlength: 1,
            min: 5000,
        },
        "MONTO_MENSUAL_CEU": {
            required: true,
            minlength: 1,
        },
        "DcboPuestos": {
            required: true,
            min: 1,
        },
        "CONFORMIDAD_SERVICIO": {
            required: true,
        },
        "APE_NOMB_CERTIFICA": {
            required: true,
        },
        "NUM_DOCUMENTO_CONSULTOR": {
            required: true,
            minlength: 8,
            maxlength: 12,
        },
        "APELLIDO_PAT_CONSULTOR": {
            required: true,
        },
        "APELLIDO_MAT_CONSULTOR": {
            required: true,
        },
        "NOMBRES_CONSULTOR": {
            required: true,
        },
        "CORREO_CONSULTOR": {
            required: true,
            email: true,
        },
        "NR_RESOLUCION": {
            required: true
        },
        "FechaAsignacion": {
            required: true,
        },
        "DENOMINACION_PUESTO": {
            required: true,
        },
        "NR_RESOLUCION": {
            required: true
        },
        "FechaAsignacion": {
            required: true,
        },
        "DESC_ORGANO": {
            required: true,
        },
        
    },
    errorElement: "span",
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }
});
jQuery("#btnGrabarSolicitudCeu_V2").click(function (e) {
    var text = $("#ID_SOLICITUD").val() == 0 ? "¿Desea <b> Registrar</b> la solicitud.? </br >" : "</br>¿Desea <b> Actualizar</b> la solicitud.?";
    var text_ = $("#ID_SOLICITUD").val() == 0 ? "Para proceder con el  <b> Registro</b>  debe generar el periodo de remuneración. </br >" : "</br>Para proceder con la  <b> Actualización</b>  debe generar el periodo de remuneración.";
    var text__ = $("#ID_SOLICITUD").val() == 0 ? "Para proceder con el  <b> Registro</b>  debe registrar como mínimo un archivo de sustento. </br >" : "</br>Para proceder con la  <b> Actualización</b>  debe registrar como mínimo un archivo de sustento.";
    if ($("#frmNuevaSolicitudFag").valid()) {
        var cant = $('#TablaMeses >tbody >tr').length;
        var cant_ = $('#Tabla_Documento >tbody >tr').length;
        if (DESARROLLO.V_EMAIL("CORREO_CONSULTOR"))  {
            if (cant_ > 0) {
                if (cant > 0) {
                    if (fn_ValidarPago_V2()) {
                        DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                            if (r) {
                                $("#myModalCargando").fadeIn(100, function () {
                                    fn_MantenimientoSolicitudFagCeu_V2();
                                });

                            }
                        });
                    }
                } else {
                    DESARROLLO.PROCESO_ALERT("Atención", text_);
                }
            } else {
                DESARROLLO.PROCESO_ALERT("Atención", text__);
            }
        } else {
            DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente el correo del consultor.");
        }
    }
});
function fn_MantenimientoSolicitudFagCeu_V2() {
    var e = new FormData;
    var DESIG = "0";
    var NR_RESOLUCION_ = "";
    var FECHA_RESOLUCION_ = "";
    if ($("#FLG_CHECK_DESIGNADO").is(":checked")) {
        DESIG = "1";
        NR_RESOLUCION_ = $("#NR_RESOLUCION").val();
        FECHA_RESOLUCION_ = $("#FechaAsignacion").val();
    }
    var Nr_Mes = 0;
    var Meses = "";
    var Monto = 0
    var Anio = 0;
    var Lista_Periodo_pago = new Array();
    $("#TablaMeses tbody tr").each(function (index) {
        $(this).children("td").each(function (index2) {
            switch (index2) {
                case 0:
                    Anio = $(this).text();
                    break;
                case 1:
                    Nr_Mes = $(this).text();
                    break;
                case 2:
                    Meses = $(this).text();
                    break;
                case 3:
                    Monto = $(this).children('input').val();
                    break;
            }
        });
        var item = {

            DES_MES: Meses,
            ID_MES: Nr_Mes,
            MONTO: Monto,
            ANIO: Anio,
            ID_ENTIDAD: $("#inputHddIdEntidad").val(),
            TIPO_PROCESO: "F",
        };
        Lista_Periodo_pago.push(item);
    });
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("ID_ENTIDAD", $("#inputHddIdEntidad").val());
    e.append("DEPENDENCIA", $("#DEPENDENCIA").val());
    e.append("FECHA_INICIO", $("#FechaInicial").val());
    e.append("FECHA_FIN", $("#FechaFin").val());
    e.append("MONTO_MENSUAL", $("#MONTO_MENSUAL_CEU").val());
    e.append("DENOMINACION_PUESTO", $("#DENOMINACION_PUESTO").val());
    e.append("CONFORMIDAD_SERVICIO", $("#CONFORMIDAD_SERVICIO").val());
    e.append("APE_NOMB_CERTIFICA", $("#APE_NOMB_CERTIFICA").val().toUpperCase());
    e.append("NUM_DOCUMENTO_CONSULTOR", $("#NUM_DOCUMENTO_CONSULTOR").val());
    e.append("APELLIDO_PAT_CONSULTOR", $("#APELLIDO_PAT_CONSULTOR").val());
    e.append("APELLIDO_MAT_CONSULTOR", $("#APELLIDO_MAT_CONSULTOR").val());
    e.append("NOMBRES_CONSULTOR", $("#NOMBRES_CONSULTOR").val());
    e.append("CORREO_CONSULTOR", $("#CORREO_CONSULTOR").val());
    e.append("FLG_PROCESO", $("#FLG_PROCESO").val());
    e.append("FLG_VERSION", $("#FLG_VERSION").val());
    e.append("FLG_NIVEL", fn_NivelPerfil());
    e.append("OFICINA_CERTIFICA", $("#OFICINA_CERTIFICA").val().toUpperCase());
    e.append("FLG_DESIGNADO", DESIG);
    e.append("NR_RESOLUCION", NR_RESOLUCION_);
    e.append("DESC_ORGANO", $("#DESC_ORGANO").val());
    e.append("FECHA_RESOLUCION", FECHA_RESOLUCION_);
    e.append("listaActividad", JSON.stringify(GetDataAct()));
    e.append("listaAcividadOtro", JSON.stringify(GetDataActOtro()));
    e.append("listaRenumeracion", JSON.stringify(Lista_Periodo_pago));
    e.append("listaDocumentos", JSON.stringify(GetData()));
    $.ajax({
        url: baseUrl + "Coordinador/ProcesoFag/MantenimientoSolicitudFagCeu_V2",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarEditarSolicitudFag();
                CargarGrillaSolicitudesFag();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                if (p.message2 == "0") {
                    fn_CerrarEditarSolicitudFag();
                    CargarGrillaSolicitudesFag();
                    DESARROLLO.PROCESO_ALERT("Atención", "Se registro la solicitud, Se generó inconvenientes al registrar los archivos de la solicitud, intente nuevamente en breves minutos.");
                } else {
                    DESARROLLO.PROCESO_ERROR(p.extra);
                }
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}