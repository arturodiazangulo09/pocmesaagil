var TableSolicitudPago_ = null;
function CrearGrillaPeriodoEntidad() {
    DESARROLLO.configurarGrilla(true);
    TableSolicitudPago_ = $("#TableSolicitudPago").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_PAGO", "data": "ID_PAGO", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVOS", "data": "", "class": "text-center", "width": "1%" },
            { "title": "FECHA DE REGISTRO", "data": "FEC_INGRESO", "class": "text-center", "width": "1%" },
            { "title": "H.R. ASIGNADA", "data": "NR_TRAMITE", "class": "text-center", },
            { "title": "DNI / CE", "data": "NUM_DOCUMENTO", "class": "text-center", },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left", "width": "1%" },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-center", },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left", },
            { "title": "FECHA DE INICIO", "data": "FECHA_INICIO", "class": "text-center", },
            { "title": "FECHA DE FIN", "data": "FECHA_FIN", "class": "text-center", },
            { "title": "HONORARIOS S/", "data": "IMPORTE_COMPROBANTE", "class": "text-center", },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "N° SOLICITUD", "data": "CODIGO_TDR", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_RECIBO", "data": "ID_ARCHIVO_RECIBO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_CPE", "data": "ID_ARCHIVO_CPE", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ID_CONFORMIDAD", "data": "ID_CONFORMIDAD", "class": "text-center", "visible": false, },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_CONFORMIDAD", "data": "ID_ARCHIVO_CONFORMIDAD", "class": "text-center", "visible": false, },
            { "title": "NUM_MES", "data": "NUM_MES", "class": "text-center", "visible": false, },
            { "title": "CANT_REEVALUACION_PAGO", "data": "CANT_REEVALUACION_PAGO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_INFORME", "data": "ID_ARCHIVO_INFORME", "class": "text-center", "visible": false, },

            
            
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if ($("#DdlEstado").val() =="011") {
                        if (row.TIPO_PROCESO == "F") {
                            html = '<button  class="btn btn-xs"  onClick="fn_GenerarConformidadFag(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ');" type="button" title="Descargar formato de conformidad"><i class="far fa-file-word" style="color:#008db8;font-size:18px"></i></button>';
                        } else {
                            html = '<button  class="btn btn-xs"  onClick="fn_GenerarConformidadPac(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ');" type="button" title="Descargar formato de conformidad"><i class="far fa-file-word" style="color:#008db8;font-size:18px"></i></button>';

                        }
                    }
                    if ($("#DdlEstado").val() == "011") {
                        html += '<button  class="btn btn-xs"  onClick="fn_ver_Adjuntar_Conformidad(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ');" type="button" title="Cargar archivo debidamente firmado"><i class="fas fa-plus-circle" style="color:#e30613;font-size:18px"></i></button>';
                        if (row.CANT_REEVALUACION_PAGO>0) {
                            html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';

                        }


                        html += '<button  class="btn btn-xs"  onClick="fn_NuevoSubsanacion(' + row.ID_SOLICITUD + ',' + row.ID_CONFORMIDAD + ',' + row.NUM_MES + ');" type="button" title="Enviar a consultor para subsanar observaciones"><i class="far fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';

                    }
                    if ($("#DdlEstado").val() == "016") {
                        html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';
                    }
                    else {
                        if ($("#DdlEstado").val() == "012" || $("#DdlEstado").val() == "015"){
                            html += '<button  class="btn btn-xs"  onClick="fn_ver_Adjuntar_Conformidad(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ');" type="button" title="Remplazar conformidad"><i class="fas fa-file-import" style="color:#e30613;font-size:18px"></i></button>';
                            html += '<button  class="btn btn-xs"  onClick="fn_EliminarConformidad(' + row.ID_SOLICITUD + ',' + row.ID_CONFORMIDAD + ',' + row.NUM_MES + ');" type="button" title="Eliminar conformidad"><i class="fas fa-times-circle" style="color:blue;font-size:18px"></i></button>';
                            if ($("#DdlEstado").val() == "015") {
                                html += '<button  class="btn btn-xs"  onClick="fn_VerobservacionPago(' + row.ID_SOLICITUD + ');" type="button" title=" Ver observaciones"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';
                                html += '<button  class="btn btn-xs"  onClick="fn_EnviarSolicitudPeriodo(' + row.ID_SOLICITUD + ',' + row.ID_CONFORMIDAD + ',' + row.NUM_MES + ');" type="button" title="Enviar solicitud de pago"><i class="far fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';

                            }
                        } else {
                            if ($("#DdlEstado").val() == "014") {
                                html += '<button  class="btn btn-xs" type="button" title="Solicitud aprobada"><i class="far fa-check-circle" style="color:#0093f8;font-size:18px"></i></button>';
                            } else {
                                if ($("#DdlEstado").val() == "013") {
                                    html += '<button  class="btn btn-xs" type="button" title="Solicitud enviada"><i class="fas fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';
                                }
                            }
                        }

                    }

                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_INFORME + ');" type="button" title="Descargar informe consultor"><i class="fas fa-user-tie" style="color:#606060;font-size:18px"></i></button>';

                    html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_RECIBO + ');" type="button" title="Descargar Recibo por Honorario"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                    html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CPE + ');" type="button" title="Descargar Constancia de validez de Comprobante de Pago"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                    if (row.ID_ARCHIVO_CONFORMIDAD > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONFORMIDAD + ');" type="button" title="Descargar Conformidad"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO);;
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INGRESO);;
                    return html;
                }
            },
            {
                'targets': 11,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);;
                    return html;
                }
            },
            {
                "targets": [12],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
        ],

    });
}
function CargarGrillaPeriodoEntidad() {
    
    var item =
    {
        TIPO_PROCESO: $('#DdlTipoC').val(),
        NUM_PROCESO: $('#txtTdr').val(),
        ANIO_PROCESO: $('#txtAnio').val(),
        NUM_DOCUMENTO: $('#txtDni').val(),
        NUM_MES: $('#DdlMes').val(),
        IDEDOCODIGO: $('#DdlEstado').val(),
    };
    var url = baseUrl + 'Coordinador/SolicitudPago/ListaSolicitudPagoEntidad';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitudPago_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableSolicitudPago_.rows.add(respuesta).draw();
    }
}
function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}
function fn_GenerarConformidadFag (ID_SOLICITUD, NUM_MES) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/SolicitudPago/VerConformidad?ID_SOLICITUD=" + ID_SOLICITUD + "&NR_MES=" + NUM_MES + "&TIPO=F"   , function (responseText, textStatus, request) {
    });
}
function fn_GenerarConformidadPac(ID_SOLICITUD, NUM_MES) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/SolicitudPago/VerConformidad?ID_SOLICITUD=" + ID_SOLICITUD + "&NR_MES=" + NUM_MES + "&TIPO=P" , function (responseText, textStatus, request) {
    });
}
function fn_CerrarConformidad() {
    jQuery("#modalConformidad").html('');
    $('#modalConformidad').modal('hide');
}
function fn_ver_Adjuntar_Conformidad(ID_SOLICITUD, NR_MES) {
    jQuery("#modalConformidad").html('');
    jQuery("#modalConformidad").load(baseUrl + "Coordinador/SolicitudPago/UpdateArchivoConformidad?ID_SOLICITUD=" + ID_SOLICITUD + "&NR_MES=" + NR_MES , function (responseText, textStatus, request) {
        $("#modalConformidad").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_Mnt_Conformidad_Pago() {   
    var e = new FormData;
    if ($("#HD_ID_ARCHIVO_CONFORMIDAD").val() == "0") {
        var doc = document.getElementById("FileArchivo").files.length;
        if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
    } else {
        if ($("#inputVerificaDelete").val() == "1") {
            var doc = document.getElementById("FileArchivo").files.length;
            if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
        }
    }
    
    if ($("#HD_ID_ARCHIVO_INFORME").val() == "0") {
        var doc = document.getElementById("FileArchivoCon").files.length;
        if (doc > 0)
            for (var a, l = 0; doc > l; l++)
                a = document.getElementById("FileArchivoCon").files[l], e.append("FileArchivoCon", a);
    } else {
        if ($("#inputVerificaDeleteCon").val() == "1") {
            var doc = document.getElementById("FileArchivoCon").files.length;
            if (doc > 0)
                for (var a, l = 0; doc > l; l++)
                    a = document.getElementById("FileArchivoCon").files[l], e.append("FileArchivoCon", a);
        }
    }

    
    if ($("#HD_ID_ARCHIVO_RECIBO").val() == "0") {
        var doc = document.getElementById("FileArchivoRH").files.length;
        if (doc > 0)
            for (var a, l = 0; doc > l; l++)
                a = document.getElementById("FileArchivoRH").files[l], e.append("FileArchivoRH", a);
    } else {
        if ($("#inputVerificaDeleteRH").val() == "1") {
            var doc = document.getElementById("FileArchivoHR").files.length;
            if (doc > 0)
                for (var a, l = 0; doc > l; l++)
                    a = document.getElementById("FileArchivoHR").files[l], e.append("FileArchivoHR", a);
        }
    }


    e.append("ID_CONFORMIDAD", $("#HD_ID_CONFORMIDAD").val());
    e.append("ID_CONTRATO", $("#HD_ID_CONTRATO").val());
    e.append("ID_ENTIDAD", $("#HD_ID_ENTIDAD").val());
    e.append("IMPORTE_COMPROBANTE", $("#HD_IMPORTE_COMPROBANTE").val());
    e.append("NUM_MES", $("#HD_NR_MES").val());
    e.append("PORCENTAJE", $("#PORCENTAJE").val());
    e.append("ACCION", $("#XHD_ACCION").val());
    e.append("ID_SOLICITUD", $("#HD_ID_SOLICITUD").val());
    e.append("ID_ARCHIVO_CONFORMIDAD", $("#HD_ID_ARCHIVO_CONFORMIDAD").val());
    e.append("ID_ARCHIVO_INFORME", $("#HD_ID_ARCHIVO_INFORME").val());
    e.append("ID_ARCHIVO_RECIBO", $("#HD_ID_ARCHIVO_RECIBO").val());
    e.append("ANIO", $("#XHD_ANIO").val());
    e.append("NIVEL_CONFORMIDAD", $("#DdlCAFILICA").val());
   // e.append("ID_SOLICITUD", $("#HD_ID_SOLICITUD").val());

    
    $.ajax({
        url: baseUrl + "Coordinador/SolicitudPago/Mnt_Conformidad_Pago",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarConformidad();
                CargarGrillaPeriodoEntidad();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function fn_UpdateEnvioPagoUTP() {
    var doc = document.getElementById("FileArchivoOficio").files.length;
    var doc_ = document.getElementById("FileArchivoResumen").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoOficio").files[l], e.append("FileArchivoOficio", a);
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoResumen").files[l], e.append("FileArchivoResumen", a);


    e.append("NR_OFICIO", $("#txtOficio").val());
    e.append("NR_FOLIOS", $("#txtFolios").val());
    e.append("ASUNTO", $("#txtAsunto").val());
    e.append("ID_CONFORMIDAD", 0);
    e.append("NUM_MES", $('#NUM_MES').val());
    e.append("ID_SOLICITUD", 0);
    e.append("IDEDOCODIGO", "013");
    e.append("ID_ENTIDAD", 0);
    e.append("ACCION", "E_UTP");
    e.append("TIPO_PROCESO", $("#TIPO_PROCESO").val());
    e.append("FLG_CON_HR", $("#Ddlflgtramite").val());
    e.append("NR_TRAMITE", $("#txtNTramite").val());
    $.ajax({
        url: baseUrl + 'Coordinador/SolicitudPago/Update_Conformidad_Pago',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                jQuery("#modalEnvioUtp").html('');
                $('#modalEnvioUtp').modal('hide');
                CargarGrillaPeriodoEntidad();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });

    //item = {
    //    ID_CONFORMIDAD: 0,
    //    NUM_MES: $('#NUM_MES').val(),
    //    ID_SOLICITUD: 0,
    //    IDEDOCODIGO: "013",
    //    ID_ENTIDAD: 0,
    //    ACCION: "E_UTP",
    //    TIPO_PROCESO: $('#TIPO_PROCESO').val(),
    //};
    //var url = baseUrl + 'Coordinador/SolicitudPago/Update_Conformidad_Pago';
    //var respuesta = DESARROLLO.Ajax(url, item, false);
    //if (respuesta != null && respuesta != "") {
    //    if (respuesta.success) {
    //        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
    //       // jQuery("#modalEnvioUtp").html('');
    //        //$('#modalEnvioUtp').modal('hide');
    //        CargarGrillaPeriodoEntidad();
    //        //if (respuesta.message2 != "0") {
    //        //    DESARROLLO.PROCESO_ALERT("NUMERO DE TRÁMITE", respuesta.message2);
    //        //}

    //    } else {
    //        DESARROLLO.PROCESO_ERROR(respuesta.extra);
    //    }
    //    $("#myModalCargando").css("display", "none");
    //}
}
function fn_VerobservacionPago(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Coordinador/Solicitud/VerObservacionProfesional?ID_SOLICITUD=" + ID + "&TIPO=015", function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_Cerrarobservacion() {
    jQuery("#modalObservacion").html('');
    $('#modalObservacion').modal('hide');
}
function fn_EnviarSolicitudPeriodo(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Enviar</b> la solicitud de pago a la Oficina de UTP del Ministerio Económica y Finanzas.? </br>", function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_UpdateEnvioPagoUTP_I(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES);
            });

        }
    })
 
}
function fn_EliminarConformidad(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Eliminar</b> la conformidad realizada.? </br>", function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_UpdateElimiaConformidad(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES);
            });

        }
    })

}
function fn_UpdateEnvioPagoUTP_I(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES) {
    item = {
        ID_CONFORMIDAD: ID_CONFORMIDAD,
        NUM_MES: NR_MES,
        ID_SOLICITUD: ID_SOLICITUD,
        IDEDOCODIGO: "013",
        ID_ENTIDAD: 0,
        ACCION: "E_UTP_I",
        TIPO_PROCESO: 0,
    };
    var url = baseUrl + 'Coordinador/SolicitudPago/Update_Conformidad_Pago';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            // jQuery("#modalEnvioUtp").html('');
            //$('#modalEnvioUtp').modal('hide');
            CargarGrillaPeriodoEntidad();
            //if (respuesta.message2 != "0") {
            //    DESARROLLO.PROCESO_ALERT("NUMERO DE TRÁMITE", respuesta.message2);
            //}

        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
function fn_UpdateElimiaConformidad(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES) {
    item = {
        ID_CONFORMIDAD: ID_CONFORMIDAD,
        NUM_MES: NR_MES,
        ID_SOLICITUD: ID_SOLICITUD,
        IDEDOCODIGO: "011",
        ID_ENTIDAD: 0,
        ACCION: "E_UTP_DE",
        TIPO_PROCESO: 0,
    };
    var url = baseUrl + 'Coordinador/SolicitudPago/Update_Conformidad_Pago';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaPeriodoEntidad();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
function fn_NuevoSubsanacion(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Coordinador/SolicitudPago/ObservacionProfesional?ID_SOLICITUD=" + ID_SOLICITUD + "&ID_CONFORMIDAD=" + ID_CONFORMIDAD + "&NR_MES=" + NR_MES, function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_InsReevaluacionPeriodo() {
    item = {
        ID_SOLICITUD: $("#ID_SOLICITUD").val(),
        DESCRIPCION: $("#TxtObservaciones").val(),
       // CONSULTOR: $("#inputHddUserNombreCompleto").val(),
        TIPO_PROCESO: "CP",
        ID_CONFORMIDAD: $("#ID_CONFORMIDAD").val(),
        NR_MES: $("#NR_MES").val(),
    };
    var url = baseUrl + 'Coordinador/SolicitudPago/UpdateReevaluacionPago';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            fn_Cerrarobservacion();
            $("#myModalCargando").css("display", "none");
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaPeriodoEntidad();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
            $("#myModalCargando").css("display", "none");
        }

    }
}
function fn_Verobservacion(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Coordinador/Solicitud/VerObservacionProfesional?ID_SOLICITUD=" + ID + "&TIPO=016", function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_CerrarEnvioUTPDocumentos() {
    jQuery("#modalEnvioUtp").html('');
    $('#modalEnvioUtp').modal('hide');
}