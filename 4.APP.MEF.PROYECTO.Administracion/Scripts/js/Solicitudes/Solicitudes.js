var TableSolicitud_ = null;
var TableSolicitudPago_ = null;
var TableSolicitudAdenda_ = null;
var TableSolicitudSuspension_ = null;
var TableSolicitudCovid_ = null;
function CrearGrillaSolicitudes() {
    DESARROLLO.configurarGrilla();
    var mostrar = $("#DdlEstado").val() == "006" ? true : false;
    TableSolicitud_ = $("#TableSolicitud").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
           { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ENTIDAD", "data": "ENTIDAD", "class": "text-left", },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left", },
            { "title": "DNI/CE", "data": "NUM_DOCUMENTO", "class": "text-center", },
            { "title": "APELLIDOS Y NOMBRES", "data": "NOMBRES", "class": "text-left", "width": "1%", },
            { "title": "N° DE CONTRATO", "data": "NUM_CONTRATO", "class": "text-center", },
            { "title": "H. R. ASIGNADA", "data": "NR_TRAMITE", "class": "text-left", "width": "1%" },
            { "title": "FECHA SOLICITUD", "data": "FECHA_ENVIO_UTP", "class": "text-center", },
            { "title": "FECHA APROBACIÓN", "data": "FECHA_APROBADO_UTP", "class": "text-center", },
            { "title": "IMPORTE S/", "data": "MONTO_MENSUAL", "class": "text-center", },
            { "title": "AÑO", "data": "ANIO_PROCESO", "class": "text-center", "width": "1%", "visible": false, },
            { "title": "NR° SOLICITUD", "data": "NUM_PROCESO", "class": "text-left", "width": "1%", "visible": false, },
            { "title": "APELLIDO_PAT_CONSULTOR", "data": "APELLIDO_PATERNO", "class": "text-center", "width": "1%", "visible": false,},
            { "title": "APELLIDO_MAT_CONSULTOR", "data": "APELLIDO_MATERNO", "class": "text-center", "width": "1%", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "width": "1%", "visible": false,},
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-left", "visible": false, },
            { "title": "FLG_CUMPLE_REQUISITOS_UTP", "data": "FLG_CUMPLE_REQUISITOS_UTP", "class": "text-left", "visible": false, },
            { "title": "FLG_CALIFICA_REQUISITOS_UTP", "data": "FLG_CALIFICA_REQUISITOS_UTP", "class": "text-left", "visible": false, },
            { "title": "CANT_OBSERVACION", "data": "CANT_OBSERVACION", "class": "text-left", "visible": false, },
            { "title": "ID_ANEXO4", "data": "ID_ANEXO4", "class": "text-left", "visible": false, },
            { "title": "ID_ARCHIVO_CONTRATO", "data": "ID_ARCHIVO_CONTRATO", "class": "text-left", "visible": false, },
            
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.IDEDOCODIGO=="006") {
                        if (row.TIPO_PROCESO == "F") {
                            if (1 == 1) {
                            //if (row.FLG_CALIFICA_REQUISITOS_UTP == "1" ) {
                                if (1 == 1) {
                                //if (row.FLG_CUMPLE_REQUISITOS_UTP == "1") {
                                    html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_F(' + row.ID_SOLICITUD + ');" type="button" title="Los anexos del coordinador son aprobados"><i class="fas fa-user-check" style="color:#008db8;font-size:18px"></i></button>';
                                    html += '<button  class="btn btn-xs"  onClick="Ver_R_Contrato_F(' + row.ID_SOLICITUD + ',' + row.ID_ANEXO4 +');" type="button" title="Registrar Contrato"><i class="fas fa-hand-holding-medical" style="color:#4a4a49;font-size:18px"></i></button>';

                                } else
                                {
                                    html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_F(' + row.ID_SOLICITUD + ');" type="button" title="Los anexos recibidos del coordinador no son correctos"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';
                                    if (row.CANT_OBSERVACION > 0) {
                                        html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:#0097d9;font-size:18px"></i></button>';
                                    }

                                    html += '<button  class="btn btn-xs"  onClick="fn_DeleteSolicitud(' + row.ID_SOLICITUD + ');" type="button" title="Rechazar solicitud"><i class="fas fa-times-circle" style="color:gray;font-size:18px"></i></button>';

                                    html += '<button  class="btn btn-xs"  onClick="EnviarCoordinador(' + row.ID_SOLICITUD + ');" type="button" title="Enviar las observaciones al coordinador para subsanación"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';

                                }
                            } else {
                                html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_F(' + row.ID_SOLICITUD + ');" type="button" title="Anexos recibidos del Coordinador"><i class="fas fa-list-ol" style="color:#606060;font-size:18px"></i></button>';
                            }
                        }
                        else {
                            if (row.FLG_CALIFICA_REQUISITOS_UTP == "1") {
                                if (row.FLG_CUMPLE_REQUISITOS_UTP == "1") {
                                    html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_P(' + row.ID_SOLICITUD + ');" type="button" title="Los anexos del coordinador son aprobados"><i class="fas fa-user-check" style="color:#008db8;font-size:18px"></i></button>';
                                    html += '<button  class="btn btn-xs"  onClick="Ver_R_Contrato_P(' + row.ID_SOLICITUD + ',0);" type="button" title="Generar Contrato"><i class="fas fa-hand-holding-medical" style="color:#4a4a49;font-size:18px"></i></button>';

                                } else {
                                    html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_P(' + row.ID_SOLICITUD + ');" type="button" title="Los anexos recibidos del coordinador no son correctos"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';
                                    if (row.CANT_OBSERVACION > 0) {
                                        html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:#0097d9;font-size:18px"></i></button>';
                                    }
                                    html += '<button  class="btn btn-xs "  onClick="fn_DeleteSolicitud(' + row.ID_SOLICITUD + ');" type="button" title="Rechazar solicitud"><i class="fas fa-times-circle" style="color:gray;font-size:18px"></i></button>';

                                    html += '<button  class="btn btn-xs"  onClick="EnviarCoordinador(' + row.ID_SOLICITUD + ');" type="button" title="Enviar las observaciones al coordinador para subsanación"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';

                                }
                            } else {
                                html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_P(' + row.ID_SOLICITUD + ');" type="button" title="Anexos recibidos del Coordinador"><i class="fas fa-list-ol" style="color:#606060;font-size:18px"></i></button>';
                            }
                        }
                    }

                    if (row.IDEDOCODIGO == "008") {
                        if (row.TIPO_PROCESO == "F") {
                            html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_F(' + row.ID_SOLICITUD + ');" type="button" title="Los anexos recibidos del coordinador no son correctos"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_P(' + row.ID_SOLICITUD + ');" type="button" title="Anexos recibidos del Coordinador"><i class="fas fa-list-ol" style="color:#606060;font-size:18px"></i></button>';

                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:#0097d9;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs "  onClick="fn_DeleteSolicitud(' + row.ID_SOLICITUD + ');" type="button" title="Rechazar solicitud"><i class="fas fa-times-circle" style="color:gray;font-size:18px"></i></button>';

                    }

                    if (row.IDEDOCODIGO == "009") {
                        if (row.TIPO_PROCESO == "F") {
                            html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_F(' + row.ID_SOLICITUD + ');" type="button" title="Los anexos recibidos del coordinador no son correctos"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_P(' + row.ID_SOLICITUD + ');" type="button" title="Los anexos recibidos del coordinador no son correctos"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';

                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion_D(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:#0097d9;font-size:18px"></i></button>';

                    }
                    if (row.IDEDOCODIGO == "007") {
                        if (row.TIPO_PROCESO == "F") {
                            html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_F(' + row.ID_SOLICITUD + ');" type="button" title="Ver Anexos"><i class="fas fa-list-ol" style="color:#606060;font-size:18px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="Ver_Anexo_P(' + row.ID_SOLICITUD + ');" type="button" title="Ver Anexos"><i class="fas fa-list-ol" style="color:#606060;font-size:18px"></i></button>';

                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONTRATO + ');" type="button" title="Descargar Contrato"><i class="fas fa-download" style="color:#0097d9;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = row.APELLIDO_PATERNO + " " + row.APELLIDO_MATERNO + " " + row.NOMBRES ;
                    return html;
                }
            },
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_ENVIO_UTP);
                    if (html == "31/12/0") {
                        html = '-';
                    }
                    return html;
                }
            },
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_APROBADO_UTP);
                    if (html == "31/12/0") {
                        html = '-';
                    }
                    return html;
                }
            },
            {
                "targets": [11],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
       
        ],

    });
}
function CargarGrillaSolicitudes() {

    var item =
    {
         ID_ENTIDAD: $("#DcboEntidades").val(),
        IDEDOCODIGO: $("#DdlEstado").val(),
        TIPO_PROCESO: $("#DdlProceso").val(),
        ANIO_PROCESO: $("#txtAniosOL").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaSolicitudesCoordinador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitud_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableSolicitud_.rows.add(respuesta).draw();
    }
}
function Ver_Anexo_F(ID) {
    jQuery("#modalAnexo").html('');
    jQuery("#modalAnexo").load(baseUrl + "Solicitudes/Solicitudes/Ver_Anexos_Solicitud?ID_SOLICITUD=" + ID + "&TIPO=F", function (responseText, textStatus, request) {
        $("#modalAnexo").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function Ver_Anexo_P(ID) {
    jQuery("#modalAnexo").html('');
    jQuery("#modalAnexo").load(baseUrl + "Solicitudes/Solicitudes/Ver_Anexos_Solicitud?ID_SOLICITUD=" + ID + "&TIPO=P", function (responseText, textStatus, request) {
        $("#modalAnexo").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_DescargarArchivo(ID) {
    DESARROLLO.DESCARGAR_DOCUMENTO_LF(ID);
    //jQuery("#myModalDescarga").html('');
    //jQuery("#myModalDescarga").load(baseUrl + "Administracion/Entidad/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    //});

}
function fn_verifica() {
    var Valor = "1";
    var text = "";
    $(".CALIFICAR").each(function () {
        text = $(this).val();
        if (text == "0" || text== "") {
            Valor = "0";
        }
    });
    $("#FLG_CUMPLE_REQUISITOS_UTP").val(Valor);
}
function fn_UPDCalificarDcocumento(Accion) {
    item = {
        ID_SOLICITUD: $("#ID_SOLICITUD").val(),
        FLG_ANEXO1: $("#FLG_ANEXO1").val(),
        FLG_ANEXO2: $("#FLG_ANEXO2").val(),
        FLG_ANEXO3: $("#FLG_ANEXO3").val(),
        FLG_ANEXO4: $("#FLG_ANEXO4").val(),
        FLG_ANEXO5: $("#FLG_ANEXO5").val(),
        FLG_ANEXO7: $("#FLG_ANEXO7").val(),
        FLG_BANCO: $("#FLG_BANCO").val(),
        FLG_H_PROFESIONAL: $("#FLG_H_PROFESIONAL").val(),
        FLG_OTROS: $("#FLG_OTROS").val(),
        FLG_INFORME_F: $("#FLG_INFORME_F").val(),
        FLG_DATOS_SECTOR: $("#FLG_DATOS_SECTOR").val(),
        FLG_FORMATOA: $("#FLG_FORMATOA").val(),
        FLG_FORMATOB: $("#FLG_FORMATOB").val(),
        FLG_FORMATOC: $("#FLG_FORMATOC").val(),
        FLG_FORMATOD: $("#FLG_FORMATOD").val(),
        FLG_FORMATOE: $("#FLG_FORMATOE").val(),
        FLG_FORMATOH: $("#FLG_FORMATOH").val(),
        FLG_PAC_ANEXO2: $("#FLG_PAC_ANEXO2").val(),
        FLG_PAC_H_PROFESIONAL: $("#FLG_PAC_H_PROFESIONAL").val(),
        FLG_CUMPLE_REQUISITOS_UTP: $("#FLG_CUMPLE_REQUISITOS_UTP").val(),
        FLG_CALIFICA_REQUISITOS_UTP: 1,
        ACCION: Accion
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/UPD_CALIFICAR_DOCUMENTOS';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            jQuery("#modalAnexo").html('');
            $('#modalAnexo').modal('hide');
            CargarGrillaSolicitudes();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function EnviarCoordinador(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Solicitudes/Solicitudes/ObservacionUTP?ID_SOLICITUD=" + ID +"&TIPO=O" , function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function CerrarModalObservacionUTP() {
    jQuery("#modalObservacion").html('');
    $('#modalObservacion').modal('hide');
}
function fn_DeleteSolicitud(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Solicitudes/Solicitudes/ObservacionUTP?ID_SOLICITUD=" + ID + "&TIPO=D", function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_InsReevaluacion() {
    item = {
        ID_SOLICITUD: $("#ID_SOLICITUD").val(),
        TIPO: "U",
        DES_REEVALUACION: $("#TxtObservaciones").val(),
        NOMBRE_COMPLETO: $("#inputHddUserNombreCompleto").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/InsReevaluacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            $("#myModalCargando").css("display", "none");
            jQuery("#modalObservacion").html('');
            $('#modalObservacion').modal('hide');
            CargarGrillaSolicitudes();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
            $("#myModalCargando").css("display", "none");
        }

    }
}
function fn_Verobservacion(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Solicitudes/Solicitudes/VerObservacionUTP?ID_SOLICITUD=" + ID +"&TIPO=U" , function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_Verobservacion_D(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Solicitudes/Solicitudes/VerObservacionUTP?ID_SOLICITUD=" + ID + "&TIPO=D" , function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_InsDeleteSolicitud() {
    item = {
        ID_SOLICITUD: $("#ID_SOLICITUD").val(),
        TIPO: "D",
        DES_REEVALUACION: $("#TxtObservaciones").val(),
        NOMBRE_COMPLETO: $("#inputHddUserNombreCompleto").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/InsReevaluacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            $("#myModalCargando").css("display", "none");
            jQuery("#modalObservacion").html('');
            $('#modalObservacion').modal('hide');
            CargarGrillaSolicitudes();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
            $("#myModalCargando").css("display", "none");
        }

    }
}
function Ver_R_Contrato_F(ID,ID_CONTRATO) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Solicitudes/Solicitudes/VerRegistroContrato?ID_SOLICITUD=" + ID + "&TIPO=F&ID_CONTRATO=" + ID_CONTRATO , function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function Ver_R_Contrato_P(ID, ID_CONTRATO) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Solicitudes/Solicitudes/VerRegistroContrato?ID_SOLICITUD=" + ID + "&TIPO=P&ID_CONTRATO=0", function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_RegistrarContratoSolicitud(Accion) {
    var flagHist = "N";
    var num_contrato  = 0;
    if ($('#cbxFlagHistorico').prop("checked")) {
        flagHist = 'S';
        num_contrato = $("#txtn_contrato").val();
    } 
    fecha_renovacion = $("#FEC_REN").val();
    var e = new FormData;
    if (Accion == "P") {
        var doc = document.getElementById("FileArchivo").files.length;
        if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
    }

    if ($("#customCheckbox1").is(':checked')) {
        e.append("ARCHIVO_RNSDD", document.getElementById("fileArchivoChk1").files[0]);
    } else {
        e.append("ARCHIVO_RNSDD", null);
    }

    if ($("#customCheckbox2").is(':checked')) {
        e.append("ARCHIVO_REDAM", document.getElementById("fileArchivoChk2").files[0]);
    } else {
        e.append("ARCHIVO_REDAM", null);
    }

    if ($("#customCheckbox3").is(':checked')) {
        e.append("ARCHIVO_REDERECI", document.getElementById("fileArchivoChk3").files[0]);
    } else {
        e.append("ARCHIVO_REDERECI", null);
    }
    
    if ($("#customCheckbox4").is(':checked')) {
        e.append("ARCHIVO_AIRSHP", document.getElementById("fileArchivoChk4").files[0]);
    } else {
        e.append("ARCHIVO_AIRSHP", null);
    }
    /*
    if ($("#customCheckbox5").is(':checked')) {
        e.append("ARCHIVO_OTROS", document.getElementById("fileArchivoChk5").files[0]);
    } else {
        e.append("ARCHIVO_OTROS", null);
    }*/
    

    e.append("ID_CONTRATO", 0);
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("ID_ARCHIVO", $("#ID").val());
    e.append("NUM_CONTRATO", num_contrato);
    e.append("FLG_HISTORICO", flagHist);
    e.append("FECHA_LIM_REN", fecha_renovacion);

    e.append("ACCION", Accion);
    debugger;
    $.ajax({
        url: baseUrl + 'Solicitudes/Solicitudes/InsContratoSolicitud',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                jQuery("#modalObservacion").html('');
                $('#modalObservacion').modal('hide');
                CargarGrillaSolicitudes();
                $("#myModalCargando").css("display", "none");
                
            }
            else {
                if (p.extra == "ALERT") {
                    DESARROLLO.PROCESO_ALERT(p.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(p.extra);
                }
               
                $("#myModalCargando").css("display", "none");

            }
        }
    });

}
/****** INICIO PROCESO SOLICITUD DE PAGO ****/
function CrearGrillaPeriodoEntidad() {
    DESARROLLO.configurarGrilla();
    TableSolicitudPago_ = $("#TableSolicitudPago").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_PAGO", "data": "ID_PAGO", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVOS", "data": "", "class": "text-center", "width": "3%" },
            { "title": "ENTIDAD", "data": "ENTIDAD", "class": "text-left", },
            { "title": "DNI/CE", "data": "NUM_DOCUMENTO", "class": "text-center", },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left", "width": "1%" },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-left", },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left", },
            { "title": "PERIODO DE PAGO", "data": "DES_MES", "class": "text-center", },
            { "title": "FECHA INICIO", "data": "FECHA_INICIO", "class": "text-center", },
            { "title": "FECHA FIN", "data": "FECHA_FIN", "class": "text-center", },
            { "title": "IMPORTE S/", "data": "IMPORTE_COMPROBANTE", "class": "text-center", },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "N° CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_RECIBO", "data": "ID_ARCHIVO_RECIBO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_CPE", "data": "ID_ARCHIVO_CPE", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ID_CONFORMIDAD", "data": "ID_CONFORMIDAD", "class": "text-center", "visible": false, },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_CONFORMIDAD", "data": "ID_ARCHIVO_CONFORMIDAD", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
            { "title": "CANT_OBSERVACIONES", "data": "CANT_OBSERVACIONES", "class": "text-center", "visible": false, },

            
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.IDEDOCODIGO == "013") {
                        html = '<button  class="btn btn-xs"  onClick="fn_GenerarPago(' + row.ID_SOLICITUD + ',' + row.ID_CONFORMIDAD+',' + row.NUM_MES + ');" type="button" title="Aprobar solicitud de pago"><i class="fas fa-user-check" style="color:#0093F8;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_VerobservacionPeriodo(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ',' + row.ID_CONFORMIDAD + ');" type="button" title="Observar solicitud de pago"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';

                    }

                    if (row.IDEDOCODIGO == "015") {
                     
                        html += '<button  class="btn btn-xs"  onClick="fn_VerservacionPeriodo(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:#0097d9;font-size:18px"></i></button>';
                   
                    }
                    if (row.IDEDOCODIGO == "014") {

                        html += '<button  class="btn btn-xs"  type="button" title="Solicitud aprobada"><i class="far fa-check-circle" style="color:#0093f8;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                  /*  var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_RECIBO + ');" type="button" title="Descargar Recibo por Honorario"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                    html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CPE + ');" type="button" title="Descargar Comprobante de Pago"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                    html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONFORMIDAD + ');" type="button" title="Descargar Conformidad"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                    */
                   /* var html = ' <select id="cboDescarga" onchange="fn_DescargarArchivo(this.value)" class="form-control">';
                    html += '<option value= "0">SELECCIONE</option>';
                    html += '<option value= "' + row.ID_ARCHIVO_RECIBO + ' ">Descargar Recibo por Honorario</option>';
                    html += '<option value= "' + row.ID_ARCHIVO_CPE +' ">Descargar Comprobante de Pago</option>';
                    html += '<option value= "' + row.ID_ARCHIVO_CONFORMIDAD + ' ">Descargar Conformidad</option>';
                    html += '<option value= "' + row.ARCHIVO_TDR + ' ">Descargar Término de Referencia</option>';
                    html  += '</select > ';*/

                    var html = '<div class="btn-group show"><button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false"></button><ul class="dropdown-menu show" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 37px, 0px);" x-placement="bottom-start">';
                    html += '<li><a class="dropdown-item" href="fn_DescargarArchivo(' + row.ID_ARCHIVO_RECIBO + ')">Descargar Recibo por Honorario</a></li>';
                    html += '<li><a class="dropdown-item" href="fn_DescargarArchivo(' + row.ID_ARCHIVO_CPE + ')">Descargar Comprobante de Pago</a></li>';
                    html += '<li><a class="dropdown-item" href="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONFORMIDAD + ')">Descargar Conformidad</a></li>';
                    html += '<li><a class="dropdown-item" href="fn_DescargarArchivo(' + row.ARCHIVO_TDR + ')">Descargar Término de Referencia</a></li>';

                    html += '</ul></div>';

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
        TIPO_PROCESO: $('#DdlProcesoPago').val(),
        ANIO_PROCESO: $('#txtAnioPago').val(),
        NUM_MES: $('#DdlMesPago').val(),
        ID_ENTIDAD: $('#DcboEntidadesPago').val(),
        IDEDOCODIGO: $('#DdlEstadoPago').val(),

    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaSolicitudPagoEntidadUTP';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitudPago_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableSolicitudPago_.rows.add(respuesta).draw();
    }
}
function fn_InsReevaluacionPeriodo() {
    item = {
        ID_SOLICITUD: $("#ID_SOLICITUD").val(),
        DESCRIPCION: $("#TxtObservaciones").val(),
        CONSULTOR: $("#inputHddUserNombreCompleto").val(),
        TIPO_PROCESO: "UP",
        ID_CONFORMIDAD: $("#ID_CONFORMIDAD").val(),
        NR_MES: $("#NR_MES").val(),        
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/UpdateReevaluacionPago';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CerrarModalObservacionPeriodo();
            $("#myModalCargando").css("display", "none");
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaPeriodoEntidad();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
            $("#myModalCargando").css("display", "none");
        }

    }
}
function CerrarModalObservacionPeriodo() {
    jQuery("#modalObservacion").html('');
    $('#modalObservacion').modal('hide');
}
function fn_VerobservacionPeriodo(ID_SOLICITUD, NUM_MES, ID_CONFORMIDAD) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Solicitudes/Solicitudes/ObservacionPeriodoPagoUTP?ID_SOLICITUD=" + ID_SOLICITUD + "&ID_CONFORMIDAD=" + ID_CONFORMIDAD + "&NR_MES=" + NUM_MES  +"&TIPO=UP", function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_VerservacionPeriodo(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Solicitudes/Solicitudes/VerObservacionUTP?ID_SOLICITUD=" + ID + "&TIPO=UP", function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_GenerarPago(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Aprobar</b> la solicitud de pago? </br>", function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_UpdateEnvioPagoUTP_I(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES);
            });

        }
    })

}
function fn_UpdateEnvioPagoUTP_I(ID_SOLICITUD, ID_CONFORMIDAD, NR_MES) {
    item = {
        ID_CONFORMIDAD: ID_CONFORMIDAD,
        NUM_MES: NR_MES,
        ID_SOLICITUD: ID_SOLICITUD,
        IDEDOCODIGO: "014",
        ID_ENTIDAD: 0,
        ACCION: "E_UTP_A",
        TIPO_PROCESO: 0,
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/Update_Conformidad_Pago';
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
/****** FIN PROCESO SOLICITUD DE PAGO  ****/
/****** INICIO PROCESO SOLICITUD DE ADENDA ****/
function CrearGrillaAdenda() {
    DESARROLLO.configurarGrilla();
    TableSolicitudAdenda_ = $("#TableSolicitudAdenda").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_CONTRATO_DET", "data": "ID_CONTRATO_DET", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ENTIDAD", "data": "DESC_ENTIDAD", "class": "text-left", },
            { "title": "DNI/CE", "data": "DOCUMENTO_CONSULTOR", "class": "text-left", },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left", },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-left", },
            { "title": "CARGO", "data": "NOMBRE_PUESTO", "class": "text-left", },
            { "title": "HONORARIOS", "data": "MONTO", "class": "text-left", },
            { "title": "H. R. ASIGNADA", "data": "NR_TRAMITE", "class": "text-center", },
            { "title": "N° DE ADENDA", "data": "CODIGO_ADENDA", "class": "text-left", },
            { "title": "FECHA INICIO", "data": "FECHA_INI", "class": "text-center", },
            { "title": "FECHA FIN", "data": "FECHA_FIN", "class": "text-center", },
            
            { "title": "NUM_PROCESO", "data": "NUM_PROCESO", "class": "text-center", "visible": false, },
            { "title": "ANIO_PROCESO", "data": "ANIO_PROCESO", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },  
            { "title": "ID_ARCHIVO_SUSTENTO", "data": "ID_ARCHIVO_SUSTENTO", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_CONTRATO", "data": "ID_ARCHIVO_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ID_TRAMITE", "data": "ID_TRAMITE", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.IDEDOCODIGO == "018") {
                        if (row.TIPO_PROCESO =="F") {
                            html += '<button  class="btn btn-xs"  onClick="Ver_R_Adenda_F(' + row.ID_CONTRATO_DET + ');" type="button" title="Aprobar solicitud de adenda"><i class="fas fa-user-check" style="color:#0093F8;font-size:18px"></i></button>';

                        } else {
                            html += '<button  class="btn btn-xs"  onClick="Ver_R_Adenda_P(' + row.ID_CONTRATO_DET + ');" type="button" title="Aprobar solicitud de adenda"><i class="fas fa-user-check" style="color:#0093F8;font-size:18px"></i></button>';

                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_VerObservarAdenda(' + row.ID_CONTRATO_DET +  ');" type="button" title="Observar solicitud de adenda"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_VerRechazarAdenda(' + row.ID_CONTRATO_DET + ');" type="button" title="Rechazar solicitud de adenda"><i class="fas fa-times-circle" style="color:#e30613;font-size:18px"></i></button>';

                    }
                    if (row.IDEDOCODIGO == "021") {
                        html += '<button  class="btn btn-xs" type="button" title="Solicitud Rechazada"><i class="fas fa-user-alt-slash" style="color:#e30613;font-size:18px"></i></button>';
                    }
                    if (row.IDEDOCODIGO == "020") {
                        html += '<button  class="btn btn-xs"  onClick="fn_VerObservacionesAdenda(' + row.ID_CONTRATO_DET + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:#606060;font-size:18px"></i></button>';
                    }
                    if (row.IDEDOCODIGO == "019") {
                        html += '<button  class="btn btn-xs" type="button" title="Solicitud aprobada"><i class="far fa-check-circle" style="color:#0093F8;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                   
                    html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_SUSTENTO + ');" type="button" title="Descargar archivo de sustento"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    if (row.TIPO_PROCESO == "P") {
                        html += '<button  class="btn btn-xs"  onClick="fn_Formato_Adenda_Pac(' + row.ID_CONTRATO_DET + ');" type="button" title="Ver formato de adenda"><i class="fas fa-file-signature" style="color:#606060;font-size:18px"></i></button>';
                    }
                    if (row.ID_ARCHIVO_CONTRATO > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONTRATO + ');" type="button" title="Descargar contrato firmado"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';

                    }
                
                    return html;
                }
            },
            {
                "targets": [9],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                'targets': 12,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INI);;
                    return html;
                }
            },
            {
                'targets': 13,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);;
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaAdenda() {
    var item =
    {
        ID_CONTRATO_DET: 0,
        ID_CONTRATO: 0,
        ID_ENTIDAD: $('#DcboEntidadesAdenda').val(),
        TIPO_PROCESO: $('#DdlProcesoContratoAdenda').val(),
        IDEDOCODIGO: $('#DdlEstadoAdenda').val(),
        NUM_PROCESO: $('#txtNrContratoAdenda').val(),
        ANIO_PROCESO: $('#txtAnioAdenda').val(),
        DOCUMENTO_CONSULTOR: "",

    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaDetalleContratos';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitudAdenda_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableSolicitudAdenda_.rows.add(respuesta).draw();
    }
}
function fn_InsReevaluacionAdenda() {
    item = {
        ID_CONTRATO_DET: $("#ID_CONTRATO_DET").val(),
        TIPO: "O",
        DES_REEVALUACION: $("#TxtObservaciones").val(),
        NOMBRE_COMPLETO: $("#inputHddUserNombreCompleto").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/InsReevaluacionAdenda';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            fn_CerrarObservacionAdenda();
            CargarGrillaAdenda();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
function fn_InsDeleteSolicitudAdenda() {
    item = {
        ID_CONTRATO_DET: $("#ID_CONTRATO_DET").val(),
        TIPO: "D",
        DES_REEVALUACION: $("#TxtObservaciones").val(),
        NOMBRE_COMPLETO: $("#inputHddUserNombreCompleto").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/InsReevaluacionAdenda';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            fn_CerrarObservacionAdenda();
            CargarGrillaAdenda();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
function fn_VerObservarAdenda(ID) {
    jQuery("#modalObservacionAdenda").html('');
    jQuery("#modalObservacionAdenda").load(baseUrl + "Solicitudes/Solicitudes/ObservacionADENDA?ID_CONTRATO_DET=" + ID + "&TIPO=O", function (responseText, textStatus, request) {
        $("#modalObservacionAdenda").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_VerRechazarAdenda(ID) {
    jQuery("#modalObservacionAdenda").html('');
    jQuery("#modalObservacionAdenda").load(baseUrl + "Solicitudes/Solicitudes/ObservacionADENDA?ID_CONTRATO_DET=" + ID + "&TIPO=D", function (responseText, textStatus, request) {
        $("#modalObservacionAdenda").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_Formato_Adenda_Pac(id) {
    jQuery("#modalAdendaFormato").html('');
    jQuery("#modalAdendaFormato").load(baseUrl + "Solicitudes/Solicitudes/Ver_Adenda?ID=" + id , function (responseText, textStatus, request) {
        $("#modalAdendaFormato").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarAdenda() {
    jQuery("#modalAdendaFormato").html('');
    $('#modalAdendaFormato').modal('hide');
    $("#modalAdendaFormato").css("display", "none");
}
function fn_VerObservacionesAdenda(ID) {
    jQuery("#modalVerObservacion").html('');
    jQuery("#modalVerObservacion").load(baseUrl + "Solicitudes/Solicitudes/VerObservacionADENDA?ID_CONTRATO_DET=" + ID , function (responseText, textStatus, request) {
        $("#modalVerObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_CerrarVerObservacion() {
    jQuery("#modalVerObservacion").html('');
    $('#modalVerObservacion').modal('hide');
    $("#modalVerObservacion").css("display", "none");
}
function fn_CerrarObservacionAdenda() {
    jQuery("#modalObservacionAdenda").html('');
    $('#modalObservacionAdenda').modal('hide');
    $("#modalObservacionAdenda").css("display", "none");
}
/////notificar
function Ver_R_Adenda_F(ID) {
    jQuery("#modalContrato").html('');
    jQuery("#modalContrato").load(baseUrl + "Solicitudes/Solicitudes/VerRegistroAdenda?ID_CONTRATO_DET=" + ID + "&TIPO=F", function (responseText, textStatus, request) {
        $("#modalContrato").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function Ver_R_Adenda_P(ID) {
    jQuery("#modalContrato").html('');
    jQuery("#modalContrato").load(baseUrl + "Solicitudes/Solicitudes/VerRegistroAdenda?ID_CONTRATO_DET=" + ID + "&TIPO=P", function (responseText, textStatus, request) {
        $("#modalContrato").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_ApruebaAdendaUTP(ID, ID_TRAMITE) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b> Aprobar</b> la solicitud de Adenda.? </br >", function (r) {
        $("#myModalCargando").fadeIn(100, function () {
            fn_NotificarCoordinador(ID);
        });
    });
}
function fn_NotificarCoordinador(ID) {
    var e = new FormData;
    if ($("#ACCION").val() == "P") {
        var doc = document.getElementById("FileArchivo").files.length;
        if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
    }

    if ($("#customCheckbox1").is(':checked')) {
        e.append("ARCHIVO_RNSDD", document.getElementById("fileArchivoChk1").files[0]);
    } else {
        e.append("ARCHIVO_RNSDD", null);
    }

    if ($("#customCheckbox2").is(':checked')) {
        e.append("ARCHIVO_REDAM", document.getElementById("fileArchivoChk2").files[0]);
    } else {
        e.append("ARCHIVO_REDAM", null);
    }

    if ($("#customCheckbox3").is(':checked')) {
        e.append("ARCHIVO_REDERECI", document.getElementById("fileArchivoChk3").files[0]);
    } else {
        e.append("ARCHIVO_REDERECI", null);
    }

    if ($("#customCheckbox4").is(':checked')) {
        e.append("ARCHIVO_AIRSHP", document.getElementById("fileArchivoChk4").files[0]);
    } else {
        e.append("ARCHIVO_AIRSHP", null);
    }

    if ($("#customCheckbox5").is(':checked')) {
        e.append("ARCHIVO_OTROS", document.getElementById("fileArchivoChk5").files[0]);
    } else {
        e.append("ARCHIVO_OTROS", null);
    }

    e.append("ID_CONTRATO_DET", $("#ID_CONTRATO_DET").val());
    e.append("IDEDOCODIGO", '019');
    e.append("ID_ARCHIVO", $("#ID").val());
    e.append("ACCION", $("#ACCION").val());
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("TIPO", $("#TIPO").val());
    e.append("DESC_ENTIDAD", $("#DESCRIPCION").val());
    
    $.ajax({
        url: baseUrl + 'Solicitudes/Solicitudes/UpdEstadoAdenda',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                jQuery("#modalContrato").html('');
                $('#modalContrato').modal('hide');
                CargarGrillaAdenda();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function fn_CerrarContratoAdenda() {
    jQuery("#modalContrato").html('');
    $('#modalContrato').modal('hide');
    $("#modalContrato").css("display", "none");
}
/****** FIN PROCESO SOLICITUD DE ADENDA  ****/

/****** INICIO PROCESO SOLICITUD DE SUSPENSION ****/
function CrearGrillaSolicitudSuspension() {
    DESARROLLO.configurarGrilla();
    TableSolicitudSuspension_ = $("#TableSolicitudSuspension").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_SUSPENSION", "data": "ID_SUSPENSION", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ENTIDAD", "data": "DESC_ENTIDAD", "class": "text-left", "width": "1%" },
            { "title": "DNI/CE", "data": "NUM_DOCUMENTO", "class": "text-center" },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left" },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left" },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-center" },

            { "title": "FECHA DE INICIO", "data": "FECHA_INICIO_CONTRATO", "class": "text-center" },
            { "title": "FECHA DE FIN", "data": "FECHA_FIN_CONTRATO", "class": "text-center" },

            { "title": "H.R. ASIGNADA", "data": "NR_TRAMITE", "class": "text-center" },

            { "title": "FECHA DE INICIO", "data": "FECHA_INICIO", "class": "text-center" },
            { "title": "FECHA DE FIN", "data": "FECHA_FIN", "class": "text-center", "width": "1%" },

         
            { "title": "DÍAS OTORGADOS", "data": "DIAS_LIBRE", "class": "text-center"},
   
            { "title": "ID_ARCHIVO_U", "data": "ID_ARCHIVO_U", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_C", "data": "ID_ARCHIVO_C", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.IDEDOCODIGO == "028") {
                        html += '<button  class="btn btn-xs"  onClick="fn_ApruebaSolicitudUTP(' + row.ID_SUSPENSION + ');" type="button" title="Aprobar solicitud de suspensión"><i class="fas fa-user-check" style="color:#0093F8;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_ObservarSuspension(' + row.ID_SUSPENSION + ');" type="button" title="Observar solicitud de suspensión"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';

                    }
                    if (row.IDEDOCODIGO == "030") {
                        html = '<button  class="btn btn-xs"  onClick="fn_VerListaObservacionesC(' + row.ID_SUSPENSION + ');" type="button" title="Lista de observaciones"><i class="fas fa-list-ol" style="color:#575756;font-size:18px"></i></button>';

                    }
                    if (row.IDEDOCODIGO == "029") {
                        html += '<button  class="btn btn-xs" type="button" title="Solicitud aprobada"><i class="far fa-check-circle" style="color:#0093F8;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.ID_ARCHIVO_C > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_C + ');" type="button" title="Descargar archivo de sustento"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';

                    }
                    return html;

                }
            },
            //{
            //    'targets': 9,
            //    'render': function (data, type, row, meta) {
            //        var html = '';
            //        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO_CONTRATO);
            //        return html;
            //    }
            //},
            //{
            //    'targets': 10,
            //    'render': function (data, type, row, meta) {
            //        var html = '';
            //        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN_CONTRATO);
            //        return html;
            //    }
            //},
            {
                'targets': 12,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO);
                    return html;
                }
            },
            {
                'targets': 13,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaSolicitudSuspension() {

    var item =
    {
        ID_CONTRATO: 0,
        ID_SUSPENSION: 0,
        TIPO_PROCESO: $("#DdlProcesoContratoSuspension").val(),
        ANIO: $("#txtAnioContratoS").val(),
        NUM_CONTRATO: $("#txtNrContratoS").val(),
        IDEDOCODIGO: $("#DdlEstadoSuspension").val(),
        ID_ENTIDAD: $("#DcboEntidadesSuspension").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaSolicitud_Suspension';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitudSuspension_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableSolicitudSuspension_.rows.add(respuesta).draw();
    }
}
function fn_ObservarSuspension(ID) {
    jQuery("#modalVerObservacionSuspension").html('');
    jQuery("#modalVerObservacionSuspension").load(baseUrl + "Solicitudes/Solicitudes/ObservacionSUSPENSION?ID_SUSPENSION=" + ID , function (responseText, textStatus, request) {
        $("#modalVerObservacionSuspension").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarVerObservacionSuspension() {
    jQuery("#modalVerObservacionSuspension").html('');
    $('#modalVerObservacionSuspension').modal('hide');
    $("#modalVerObservacionSuspension").css("display", "none");
}
function fn_InsReevaluacionSuspension() {
    item = {
        ID_SUSPENSION: $("#ID_SUSPENSION").val(),
        DES_REEVALUACION: $("#TxtObservaciones").val(),
        TIPO: "U",
        NOMBRE_COMPLETO: $("#inputHddUserNombreCompleto").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/UpdateReevaluacionSuspension';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            fn_CerrarVerObservacionSuspension();
            $("#myModalCargando").css("display", "none");
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaSolicitudSuspension();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
            $("#myModalCargando").css("display", "none");
        }

    }
}
function fn_VerListaObservacionesC(ID) {
    jQuery("#modalVerObservacionSuspension").html('');
    jQuery("#modalVerObservacionSuspension").load(baseUrl + "Solicitudes/Solicitudes/VerObservacionSUSPENSION?ID_SUSPENSION=" + ID , function (responseText, textStatus, request) {
        $("#modalVerObservacionSuspension").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_ApruebaSolicitudUTP(ID_SUSPENSION) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Aprobar</b> la solicitud.? </br>", function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_UpdateAprobarSolicitudSuspension(ID_SUSPENSION);
            });

        }
    })

}
function fn_UpdateAprobarSolicitudSuspension(ID_SUSPENSION) {
    item = {
        ID_SUSPENSION: ID_SUSPENSION,
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/UpdEnvioSuspensionAprueba';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaSolicitudSuspension();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
/****** FIN PROCESO SOLICITUD DE SUSPENSION  ****/


/****** INICIO PROCESO SOLICITUD DE COVID ****/
function CrearGrillaSolicitudCovid() {
    DESARROLLO.configurarGrilla();
    TableSolicitudCovid_ = $("#TableSolicitudCovid").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_COVID", "data": "ID_COVID", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ENTIDAD", "data": "DESC_ENTIDAD", "class": "text-left", "width": "1%" },
            { "title": "DNI/CE", "data": "NUM_DOCUMENTO", "class": "text-center" },
            { "title": "APELLIDOS NOMBRES", "data": "CONSULTOR", "class": "text-left" },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-center" },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left" },
            { "title": "FECHA DE INICIO", "data": "FECHA_INICIO_CONTRATO", "class": "text-center", },
            { "title": "FECHA DE FIN", "data": "FECHA_FIN_CONTRATO", "class": "text-center", "width": "1%" },
            { "title": "H.R. ASIGNADA", "data": "NR_TRAMITE", "class": "text-center", "width": "1%" },
            { "title": "FECHA DE INICIO", "data": "FECHA_INICIO", "class": "text-center", },
            { "title": "FECHA DE FIN", "data": "FECHA_FIN", "class": "text-center", "width": "1%" },
            { "title": "DÍAS OTORGADOS", "data": "NUM_DIAS", "class": "text-center", "width": "1%" },
            { "title": "ID_ARCHIVO_U", "data": "ID_ARCHIVO_U", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_C", "data": "ID_ARCHIVO_C", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.IDEDOCODIGO == "028") {
                        html += '<button  class="btn btn-xs"  onClick="fn_ApruebaSolicitudUTPCovid(' + row.ID_COVID + ');" type="button" title="Aprobar solicitud de suspensión"><i class="fas fa-user-check" style="color:#0093F8;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_ObservarCovid(' + row.ID_COVID + ');" type="button" title="Observar solicitud de suspensión"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';

                    }
                    if (row.IDEDOCODIGO == "030") {
                        html = '<button  class="btn btn-xs"  onClick="fn_VerListaObservacionesCovid(' + row.ID_COVID + ');" type="button" title="Lista de observaciones"><i class="fas fa-list-ol" style="color:#575756;font-size:18px"></i></button>';

                    }
                    if (row.IDEDOCODIGO == "029") {
                        html += '<button  class="btn btn-xs" type="button" title="Solicitud aprobada"><i class="far fa-check-circle" style="color:#0093F8;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.ID_ARCHIVO_C > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_C + ');" type="button" title="Descargar archivo de sustento"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';

                    }
                    return html;

                }
            },
          {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO_CONTRATO);
                    return html;
                }
            },
           /*   {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN_CONTRATO);
                    return html;
                }
            },*/
            {
                'targets': 12,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO);
                    return html;
                }
            },
            /*{
                'targets': 13,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);
                    return html;
                }
            },*/
        ],

    });
}
function CargarGrillaSolicitudCovid() {

    var item =
    {
        ID_CONTRATO: 0,
        ID_COVID: 0,
        TIPO_PROCESO: $("#DdlProcesoContratoCovid").val(),
        ANIO: $("#txtAnioContratoCovid").val(),
        NUM_CONTRATO: $("#txtNrContratoCovid").val(),
        IDEDOCODIGO: $("#DdlEstadoCovid").val(),
        ID_ENTIDAD: $("#DcboEntidadesCovid").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaSolicitud_Covid';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitudCovid_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableSolicitudCovid_.rows.add(respuesta).draw();
    }
}
function fn_ObservarCovid(ID) {
    jQuery("#modalVerObservacionCovid").html('');
    jQuery("#modalVerObservacionCovid").load(baseUrl + "Solicitudes/Solicitudes/ObservacionCOVID?ID_COVID=" + ID, function (responseText, textStatus, request) {
        $("#modalVerObservacionCovid").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarVerObservacionCovid() {
    jQuery("#modalVerObservacionCovid").html('');
    $('#modalVerObservacionCovid').modal('hide');
    $("#modalVerObservacionCovid").css("display", "none");
}
function fn_InsReevaluacionCovid() {
    item = {
        ID_COVID: $("#ID_COVID").val(),
        DES_REEVALUACION: $("#TxtObservaciones").val(),
        TIPO: "U",
        NOMBRE_COMPLETO: $("#inputHddUserNombreCompleto").val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/UpdateReevaluacionCovid';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            fn_CerrarVerObservacionCovid();
            $("#myModalCargando").css("display", "none");
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaSolicitudCovid();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
            $("#myModalCargando").css("display", "none");
        }

    }
}
function fn_VerListaObservacionesCovid(ID) {
    jQuery("#modalVerObservacionCovid").html('');
    jQuery("#modalVerObservacionCovid").load(baseUrl + "Solicitudes/Solicitudes/VerObservacionCOVID?ID_COVID=" + ID, function (responseText, textStatus, request) {
        $("#modalVerObservacionCovid").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_ApruebaSolicitudUTPCovid(ID) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Aprobar</b> la solicitud.? </br>", function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_UpdateAprobarSolicitudCovid(ID);
            });

        }
    })

}
function fn_UpdateAprobarSolicitudCovid(ID) {
    item = {
        ID_COVID: ID,
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/UpdEnvioCovidAprueba';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaSolicitudCovid();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
/****** FIN PROCESO SOLICITUD DE COVID  ****/
