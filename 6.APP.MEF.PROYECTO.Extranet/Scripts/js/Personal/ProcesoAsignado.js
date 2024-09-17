var TableProcesoAsignado_ = null;
function CrearGrillaProcesoAsignacion() {
    DESARROLLO.configurarGrilla(true);
    TableProcesoAsignado_ = $("#TableProcesoAsignado").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "ACCION", "data": "", "class": "text-center", },
            { "title": "TDR", "data": "", "class": "text-center", "width": "1%" },
            { "title": "MODALIDAD", "data": "", "class": "text-center", "width": "1%" },
            { "title": "N° SOLICITUD", "data": "NUM_PROCESO", "class": "text-center", "width": "1%" },
            { "title": "ESTADO", "data": "IDEDOCODIGO", "class": "text-left", },
            { "title": "ENTIDAD", "data": "ENTIDAD", "class": "text-left", },
            { "title": "HONORARIOS", "data": "MONTO_MENSUAL", "class": "text-center", },
            { "title": "INICIO", "data": "FECHA_INICIO", "class": "text-center", },
            { "title": "FIN", "data": "FECHA_FIN", "class": "text-center", "width": "1%" },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ENVIO_PROPUESTA != "1") {
                        if (row.TIPO_PROCESO == "F") {
                          //  html = '<button  class="btn btn-xs"  onClick="fn_Propuesta_Fag(' + row.ID_SOLICITUD + ');" type="button" title="Registrar información"><i class="fas fa-user-edit" style="color:#e30613;font-size:18px"></i></button>';
                        } else {
                            html = '<button  class="btn btn-xs"  onClick="fn_Propuesta_Pac(' + row.ID_SOLICITUD + ');" type="button" title="Registrar información"><i class="fas fa-user-edit" style="color:#e30613;font-size:18px"></i></button>';
                        }
                        if (row.FLG_ENVIO_PROPUESTA == "0") {

                            if (row.TIPO_PROCESO == "F") {
                                html += "<button class='btn bgr-salmon btn-block' onClick='fn_initWizard(" +
                                    row.ID_PERSONAL + ", " + row.ID_SOLICITUD + ")'>Participar <i class='fa fa-arrow-circle-right'></i></button>";
                            } else {
                                html += '<button  class="btn btn-xs"  onClick="fn_EnviarInformacionPac(' + row.ID_SOLICITUD + ',' + row.USER_COORDINADOR + ',' + row.ID_FORMATOB + ');" type="button" title="Enviar información"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';
                            }
                        }
                        if (row.CANT_OBSERVACION > 0) {
                            html += '<button  class="btn btn-xs"  onClick="fn_VerObservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observación"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';
                        }
                    }
                    if (row.IDEDOCODIGO == "007") {
                        if (row.TIPO_PROCESO == "F") {
                            html += '<button  class="btn btn-xs"  onClick="fn_GenerarSolicitudPagoFag(' + row.ID_SOLICITUD + ');" type="button" title="Generar solicitud de pago "><i class="fas fa-hand-holding-usd" style="color:#118C4F;font-size:18px"></i></button>';
                        } else {
                            html += '<button  class="btn btn-xs"  onClick="fn_GenerarSolicitudPagoPac(' + row.ID_SOLICITUD + ');" type="button" title="Generar solicitud de pago "><i class="fas fa-hand-holding-usd" style="color:#118C4F;font-size:18px"></i></button>';
                        }

                        html += '<button  class="btn btn-xs"  onClick="fn_GenerarSolicitudDescanso(' + row.ID_CONTRATO + ');" type="button" title="Generar solicitud de suspensión de prestación con contraprestación"><i class="fas fa-hand-holding" style="color:#606060;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_GenerarSolicitudCovid(' + row.ID_CONTRATO + ');" type="button" title="Generar solicitud descanso médico por Covid-19."><i class="fas fa-hand-holding-medical" style="color:#e30613;font-size:18px"></i></button>';


                    } else {
                        if (row.FLG_ENVIO_PROPUESTA == "1") {
                            html += '<button  class="btn btn-xs"  type="button" title="Información enviada"><i class="fas fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';
                        }
                    }

                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    var botones = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO_TDR + ');" type="button" title="Descargar TDR"><i class="fas fa-download" style="color:#84C157;font-size:18px"></i></button>';
                    if (row.IDEDOCODIGO == "007") {
                       
                        botones += ' <li  class=" mr-5 ml-5"> Descargar Documentos</li>';
                        botones += ' <li><button  class="btn" onClick="fn_DescargarArchivo(' + row.ARCHIVO_TDR + ');"><u>1. TDR</u></button></li>';
                        botones += ' <li><button  class="btn" onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONTRATO + ');"><u>2. Anexo 7</u></button></li>';
                        botones += ' <li class="text-center"><button class="btn btn-primary"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONTRATO + ');fn_DescargarArchivo(' + row.ARCHIVO_TDR + ');">Descargar Todo</button></li>';
                        html = '<div class="btn-group"><button type="button" class="btn btn-default btn-warning dropdown-toggle" data-toggle="dropdown"> <i class="fa fa-circle" style="color:"#F5BB47" aria-hidden="true"></i> Expediente </button> <ul class="dropdown-menu" role="menu">' + botones+' </ul></div>'
                       // html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONTRATO + ');" type="button" title="Descargar contrato"><i class="fas fa-download" style="color:#84C157;font-size:18px"></i></button>';

                    }


                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.TIPO_PROCESO == "F") {
                        html += '<p>FAG</p>';

                    } else
                    if (row.TIPO_PROCESO == "P") {
                        html += '<p>PAC</p>';
                    }
                    return html;
                }
            },
         
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO);;
                    return html;
                }
            },
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);;
                    return html;
                }
            },
            {
                "targets": [8],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
        ],

    });
}
function DescargarConsultaFag() {
    $.paramcustom = {
        url: baseUrl + 'Usuario/Personal/ExportarExcelSolicitudes',
        values: {
            TIPO_PROCESO: "F",
            IDEDOCODIGO: $("#DdlEstado").val(),
            NUM_PROCESO: $("#txtTdr").val(),
            DENOMINACION_PUESTO: $("#txtDesc").val(),
            FECHA_REGISTRO: $("#FechaCreacion").val(),
        }
    }
    $.redirect();
}
function fn_initWizard(consultor, solicitud) {
    jQuery("#modalProceso").html('');
    jQuery("#modalProceso").load(
        baseUrl + "Usuario/Personal/WizardContainer?consultorId="
        + consultor + "&solicitudId=" + solicitud,
        function (responseText, textStatus, request) {
            $("#modalProceso").modal('show');
            //$(".modal-content").draggable({
            //    handle: ".modal-header", cursor: "move"
            //});
        });
}

function CargarGrillaProcesoAsignacion() {
    
    var item =
    {
        ID_PERSONAL: $('#inputHddIdUsuarioSesion').val(),
        ID_SOLICITUD: 0,
        ID_ENTIDAD: $('#DcboEntidades').val(),
        NUM_PROCESO: $('#txtTdr').val(),
        ANIO_PROCESO: $('#txtAnio').val(),
        IDEDOCODIGO: $('#DdlEstado').val(),
    };
    var url = baseUrl + 'Usuario/Personal/ListasolicitudPersonal';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableProcesoAsignado_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableProcesoAsignado_.rows.add(respuesta).draw();
    }
}
function fn_Propuesta_Fag(id,tipo) {
        jQuery("#modalPropuesta").html('');
        jQuery("#modalPropuesta").load(baseUrl + "Usuario/Personal/PropuestaFag?ID_SOLICITUD=" + id, function (responseText, textStatus, request) {
            $("#modalPropuesta").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
}
function fn_Propuesta_Pac(id, tipo) {
        jQuery("#modalPropuesta").html('');
        jQuery("#modalPropuesta").load(baseUrl + "Usuario/Personal/PropuestaPac?ID_SOLICITUD=" + id, function (responseText, textStatus, request) {
            $("#modalPropuesta").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
}
function fn_CerrarPropuesta() {
    jQuery("#modalPropuesta").html('');
    $('#modalPropuesta').modal('hide');
}
function fn_finWizard() {
    jQuery("#modalProceso").html('');
    $("#modalProceso").modal('hide');

}
//function fn_DescargarArchivoTDR(ID) {
//    jQuery("#myModalDescarga").html('');
//    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=ATDR", function (responseText, textStatus, request) {
//    });

//}
function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });
}
function fn_UpdateDJ_Fag(id) {
    //var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if ($("#H_MONTO_RECIBO").val() > 0) {
        var A2 = document.getElementById("FileArchivoAnexo2").files.length;
        var A3 = document.getElementById("FileArchivoAnexo3").files.length;
        var A7 = document.getElementById("FileArchivoAnexo7").files.length;
        var A8 = document.getElementById("FileArchivoBancario").files.length;
        if (A2 > 0) for (var a, l = 0; A2 > l; l++) a = document.getElementById("FileArchivoAnexo2").files[l], e.append("FileArchivoAnexo2", a);
        if (A3 > 0) for (var a, l = 0; A3 > l; l++) a = document.getElementById("FileArchivoAnexo3").files[l], e.append("FileArchivoAnexo3", a);
        if (A7 > 0) for (var a, l = 0; A7 > l; l++) a = document.getElementById("FileArchivoAnexo7").files[l], e.append("FileArchivoAnexo7", a);
        if (A8 > 0) for (var a, l = 0; A8 > l; l++) a = document.getElementById("FileArchivoBancario").files[l], e.append("FileArchivoBancario", a);
    }
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("MONTO_RECIBO", $("#MONTO_RECIBO").val());
    e.append("FLG_CHECK_4", $("#FLG_SUSPENSION").val());
    e.append("FLG_PENSIONISTA_ESTADO", $("#FLG_PENSION_ESTADO").val());
    e.append("FLG_PENSIONISTA_POLICIA", $("#FLG_PENSION_POLICIAL").val());
    e.append("FLG_PENSIONES", $("#FLG_PENSION").val());
    e.append("ID_ANEXO2", $("#ID_ANEXO2").val());
    e.append("ID_ANEXO3", $("#ID_ANEXO3").val());
    e.append("ID_ANEXO7", $("#ID_ANEXO7").val());
    e.append("ENTIDAD", $("#ENTIDAD").val());
    e.append("ID_BANCO", $("#ID_BANCO").val());

    $.ajax({
        url: baseUrl + 'Usuario/Personal/UpdateDJ_Fag',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                CargarGrillaProcesoAsignacion();
                fn_CerrarPropuesta();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            DESARROLLO.CERRAR_MODAL('myModalCargando');

        }
    });
}


function fn_UpdateDJ_FagWid(id) {
    var result = true;
    //var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if ($("#H_MONTO_RECIBO").val() > 0) {
        var A2 = document.getElementById("FileArchivoAnexo2").files.length;
        var A3 = document.getElementById("FileArchivoAnexo3").files.length;
        var A7 = document.getElementById("FileArchivoAnexo7").files.length;
        var A8 = document.getElementById("FileArchivoBancario").files.length;
        if (A2 > 0) for (var a, l = 0; A2 > l; l++) a = document.getElementById("FileArchivoAnexo2").files[l], e.append("FileArchivoAnexo2", a);
        if (A3 > 0) for (var a, l = 0; A3 > l; l++) a = document.getElementById("FileArchivoAnexo3").files[l], e.append("FileArchivoAnexo3", a);
        if (A7 > 0) for (var a, l = 0; A7 > l; l++) a = document.getElementById("FileArchivoAnexo7").files[l], e.append("FileArchivoAnexo7", a);
        if (A8 > 0) for (var a, l = 0; A8 > l; l++) a = document.getElementById("FileArchivoBancario").files[l], e.append("FileArchivoBancario", a);
    }
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("MONTO_RECIBO", $("#MONTO_RECIBO").val());
    e.append("FLG_CHECK_4", $("#FLG_SUSPENSION").val());
    e.append("FLG_PENSIONISTA_ESTADO", $("#FLG_PENSION_ESTADO").val());
    e.append("FLG_PENSIONISTA_POLICIA", $("#FLG_PENSION_POLICIAL").val());
    e.append("FLG_PENSIONES", $("#FLG_PENSION").val());
    e.append("ID_ANEXO2", $("#ID_ANEXO2").val());
    e.append("ID_ANEXO3", $("#ID_ANEXO3").val());
    e.append("ID_ANEXO7", $("#ID_ANEXO7").val());
    e.append("ENTIDAD", $("#ENTIDAD").val());
    e.append("ID_BANCO", $("#ID_BANCO").val());

    $.ajax({
        url: baseUrl + 'Usuario/Personal/UpdateDJ_Fag',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                /*CargarGrillaProcesoAsignacion();
                fn_CerrarPropuesta();*/
                result = true;
                DESARROLLO.PROCESO_CORRECTO(p.message);
               
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
                result = false;
            }
        }
    });
    return result;
}


function Ver_CV() {
    var id = $('#inputHddIdUsuarioSesion').val();
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + id + "&proceso=RFICHACV", function (responseText, textStatus, request) {
    });
}
function Ver_CV_Pac() {
    var id = $('#inputHddIdUsuarioSesion').val();
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + id + "&proceso=RFICHACVPAC", function (responseText, textStatus, request) {
    });
}
function Ver_CV_Fag() {
    var id = $('#inputHddIdUsuarioSesion').val();
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + id + "&proceso=RFICHACVFAG", function (responseText, textStatus, request) {
    });
}
function Ver_Anexo02(id) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + id + "&proceso=RANEXO02", function (responseText, textStatus, request) {
    });
}
function Ver_Anexo03(id) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + id + "&proceso=RANEXO03", function (responseText, textStatus, request) {
    });
}
function fn_UpdateDJ_Pac(id) {
    var e = new FormData;
    if ($("#H_MONTO_RECIBO").val() > 0) {
        var A2 = document.getElementById("FileArchivoFORMATOB").files.length;
        var A3 = document.getElementById("FileArchivoFORMATOC").files.length;
        var A7 = document.getElementById("FileArchivoFORMATOCV").files.length;
        if (A2 > 0) for (var a, l = 0; A2 > l; l++) a = document.getElementById("FileArchivoFORMATOB").files[l], e.append("FileArchivoFORMATOB", a);
        if (A3 > 0) for (var a, l = 0; A3 > l; l++) a = document.getElementById("FileArchivoFORMATOC").files[l], e.append("FileArchivoFORMATOC", a);
        if (A7 > 0) for (var a, l = 0; A7 > l; l++) a = document.getElementById("FileArchivoFORMATOCV").files[l], e.append("FileArchivoFORMATOCV", a);

    }
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("MONTO_RECIBO", $("#MONTO_RECIBO").val());
    e.append("FLG_CHECK_4", $("#FLG_SUSPENSION").val());
    e.append("ID_FORMATOB", $("#ID_FORMATOB").val());
    e.append("ID_FORMATOC", $("#ID_FORMATOC").val());
    e.append("ID_PAC_ANEXO2", $("#ID_PAC_ANEXO2").val());
    e.append("ENTIDAD", $("#ENTIDAD").val());
    $.ajax({
        url: baseUrl + 'Usuario/Personal/UpdateDJ_Pac',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                CargarGrillaProcesoAsignacion();
                fn_CerrarPropuesta();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            DESARROLLO.CERRAR_MODAL('myModalCargando');
        }
    });
}
function Ver_FORMATOB(id) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + id + "&proceso=RFORMATOB", function (responseText, textStatus, request) {
    });
}
function Ver_FORMATOC(id) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + id + "&proceso=RFORMATOC", function (responseText, textStatus, request) {
    });
}
function fn_EnviarInformacion(id, NUM_DOCUMENTO , VALIDAR) {
    if (VALIDAR>0) {
        DESARROLLO.PROCESO_CONFIRM("¿Está seguro que desea enviar su información, una vez enviada la información no se podrá realizar modificaciones?", function (r) {
            if (r) {
                $("#myModalCargando").fadeIn(100, function () {
                    fn_UpdateEnvioInformacion(id, NUM_DOCUMENTO);
                });
            }
        });
    } else {
        DESARROLLO.PROCESO_ALERT("Para poder enviar la información al coordinador de la entidad es necesario que adjunte los documentos firmados.</br>" + "<button class='btn btn-xs' onclick='fn_Propuesta_Fag(" + id +");' type='button' title='Registrar información'><i class='fas fa-user-edit' style='color:#e30613;font-size:40px'></i></button>");
    }
      
}

function fn_EnviarInformacionFagWid(id, NUM_DOCUMENTO, VALIDAR) {
    if (VALIDAR > 0) {
        DESARROLLO.PROCESO_CONFIRM("¿Está seguro que desea enviar su información, una vez enviada la información no se podrá realizar modificaciones?", function (r) {
            if (r) {
                $("#myModalCargando").fadeIn(100, function () {
                    fn_UpdateEnvioInformacionFagWid(id, NUM_DOCUMENTO);
                    DESARROLLO.CERRAR_MODAL('modalProceso');
                });
            }
        });
    } else {
        DESARROLLO.PROCESO_ALERT("Para poder enviar la información al coordinador de la entidad es necesario que adjunte los documentos firmados.</br>" + "<button class='btn btn-xs' onclick='fn_Propuesta_Fag(" + id + ");' type='button' title='Registrar información'><i class='fas fa-user-edit' style='color:#e30613;font-size:40px'></i></button>");
    }

}
function fn_EnviarInformacionPac(id, NUM_DOCUMENTO, VALIDAR) {
    if (VALIDAR > 0) {
        DESARROLLO.PROCESO_CONFIRM("¿Está seguro que desea enviar su información, una vez enviada la información no se podrá realizar modificaciones?", function (r) {
            if (r) {
                $("#myModalCargando").fadeIn(100, function () {
                    fn_UpdateEnvioInformacion(id, NUM_DOCUMENTO);
                });
            }
        });
    } else {
        DESARROLLO.PROCESO_ALERT("Para poder enviar la información al coordinador de la entidad es necesario que adjunte los documentos firmados.</br>" + "<button class='btn btn-xs' onclick='fn_Propuesta_Pac(" + id + ");' type='button' title='Registrar información'><i class='fas fa-user-edit' style='color:#e30613;font-size:40px'></i></button>");
    }

}
function fn_UpdateEnvioInformacion(id, NUM_DOCUMENTO) {
    item = {
        ID_SOLICITUD: id,
        USER_COORDINADOR: NUM_DOCUMENTO,
    };
    var url = baseUrl + 'Usuario/Personal/UpdatePropuesta_Envio';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaProcesoAsignacion();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
      //  fn_finWizard();
    }
}

function fn_UpdateEnvioInformacionFagWid(id, NUM_DOCUMENTO) {
    item = {
        ID_SOLICITUD: id,
        USER_COORDINADOR: NUM_DOCUMENTO,
    };
    var url = baseUrl + 'Usuario/Personal/UpdatePropuesta_Envio';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
        var urlinicio = baseUrl + 'Usuario/Personal/Inicio';
        $.ajax({
            url: urlinicio,
            dataType: 'html',
            type: 'get',
            contentType: 'text/xml; charset=utf-8',
            success: function (result) {
                $('#divProceso').html("");
                $('#divProceso').html(result);
            }
        });
    }
}
function Ver_CV_Post_Pac(ID) {

    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=RFICHACVPAC_PER", function (responseText, textStatus, request) {
    });
}
function Ver_CV_Post_Fag(ID) {

    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=RFICHACVFAG_PER", function (responseText, textStatus, request) {
    });
}
function fn_VerObservacion(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Coordinador/Solicitud/VerObservacionProfesional?ID_SOLICITUD=" + ID, function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}
function fn_GenerarSolicitudPagoFag(id) {
    jQuery("#modalSolicitudPago").html('');
    jQuery("#modalSolicitudPago").load(baseUrl + "Usuario/SolicitudPago/GenerarSolicitudPago_Fag?ID_SOLICITUD=" + id, function (responseText, textStatus, request) {
        $("#modalSolicitudPago").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_GenerarSolicitudPagoPac(id) {
    jQuery("#modalSolicitudPago").html('');
    jQuery("#modalSolicitudPago").load(baseUrl + "Usuario/SolicitudPago/GenerarSolicitudPago_Pac?ID_SOLICITUD=" + id, function (responseText, textStatus, request) {
        $("#modalSolicitudPago").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_Cerrarobservacion() {
    jQuery("#modalObservacion").html('');
    $('#modalObservacion').modal('hide');
}
///////////*******PROCESO DE DESCANSO *********//////////////
function fn_GenerarSolicitudDescanso(id) {
    jQuery("#modalSolicitudDescanso").html('');
    jQuery("#modalSolicitudDescanso").load(baseUrl + "Usuario/SolicitudDescanso/GenerarSolicitudDescanso?ID_CONTRATO=" + id, function (responseText, textStatus, request) {
        $("#modalSolicitudDescanso").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_GenerarSolicitudCovid(id) {
    jQuery("#modalSolicitudDescanso").html('');
    jQuery("#modalSolicitudDescanso").load(baseUrl + "Usuario/SolicitudCovid/GenerarSolicitudCovid?ID_CONTRATO=" + id, function (responseText, textStatus, request) {
        $("#modalSolicitudDescanso").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
/////////******************* PROPUESTA FAG INICIO ******************/////////
function fn_RegistroFagSolicitud() {

    var text = "</br>¿Desea <b> Registrar</b> los datos.? </br >";
    if (ValidarFag()) {
        if ($("#H_MONTO_RECIBO").val() > 0) {
            if (ValidateArchivoFAG()) {
                DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                    if (r) {
                        $("#myModalCargando").fadeIn(100, function () {
                            fn_UpdateDJ_Fag();
                        });
                    }
                });
            }
        } else {
            DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                if (r) {
                    $("#myModalCargando").fadeIn(100, function () {
                        fn_UpdateDJ_Fag();
                    });
                }
            });
        }

    }
}

function fn_RegistroFagSolicitudfwiz() {
    var result = true;
    console.log("fn_RegistroFagSolicitudfwiz");
    var text = "</br>¿Desea <b> Registrar</b> los datos.? </br >";

        if ($("#H_MONTO_RECIBO").val() > 0) {
            if (ValidateArchivoFAG()) {
                DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                    if (r) {
                        result= fn_UpdateDJ_FagWid();
                         
                    }
                });
            }
        } else {
            DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                if (r) {
                    result =   fn_UpdateDJ_FagWid();
                }
            });
    }

    return result;

}


function ValidarFag() {
    var result = true;
    if ($("#FLG_PENSION").val() == "") { $("#FLG_PENSION").addClass('is-invalid'); result = false; } else { $("#FLG_PENSION").removeClass('is-invalid'); }
    if ($("#FLG_PENSION").val() == "1") {
        if ($("#FLG_PENSION_ESTADO").val() == "") { $("#FLG_PENSION_ESTADO").addClass('is-invalid'); result = false; } else { $("#FLG_PENSION_ESTADO").removeClass('is-invalid'); }
        if ($("#FLG_PENSION_POLICIAL").val() == "") { $("#FLG_PENSION_POLICIAL").addClass('is-invalid'); result = false; } else { $("#FLG_PENSION_POLICIAL").removeClass('is-invalid'); }
    } else {
        $("#FLG_PENSION_ESTADO").removeClass('is-invalid');
        $("#FLG_PENSION_POLICIAL").removeClass('is-invalid');
    }
    if ($("#MONTO_RECIBO").val() == "") { $("#MONTO_RECIBO").addClass('is-invalid'); result = false; } else { $("#MONTO_RECIBO").removeClass('is-invalid'); }
    if ($("#FLG_SUSPENSION").val() == "") { $("#FLG_SUSPENSION").addClass('is-invalid'); result = false; } else { $("#FLG_SUSPENSION").removeClass('is-invalid'); }
    if (document.getElementById('CheckCondiciones').checked) {
        $("#CheckCondiciones").removeClass('is-invalid');
    } else {
        $("#CheckCondiciones").addClass('is-invalid'); result = false;
    }
    return result;
}
function ValidarPen() {
    if ($("#FLG_PENSION").val() == "1") {
        $(".PEN").prop("disabled", false);
    } else {
        $(".PEN").val("");
        $(".PEN").prop("disabled", true);
    }
}
function fn_EliminarArchivo(ID) {
    switch (ID) {
        case 2:
            $("#DivM_Anexo2").css("display", "none");
            $("#DivO_Anexo2").css("display", "block");
            $("#inputAnexo2Delete").val("1");
            break;
        case 3:
            $("#DivM_Anexo3").css("display", "none");
            $("#DivO_Anexo3").css("display", "block");
            $("#inputAnexo3Delete").val("1");
            break;
        case 6:
            $("#DivM_Anexo7").css("display", "none");
            $("#DivO_Anexo7").css("display", "block");
            $("#inputAnexo7Delete").val("1");
            break;
        case 7:
            $("#DivM_BC").css("display", "none");
            $("#DivO_BC").css("display", "block");
            $("#inputBCDelete").val("1");
            break;
        default:
    }
}
function ValidateArchivoFAG() {
    var result = true;
    if ($('#ID_ANEXO2').val() > 0) {
        if ($('#inputAnexo2Delete').val() == "1") {
            if ($('#FileArchivoAnexo2')[0].files.length === 0) { $("#LblfileArchivoAnexo2").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoAnexo2").text(""); }
        }
    } else {
        if ($('#FileArchivoAnexo2')[0].files.length === 0) { $("#LblfileArchivoAnexo2").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoAnexo2").text(""); }
    }
    if ($('#ID_ANEXO3').val() > 0) {
        if ($('#inputAnexo3Delete').val() == "1") {
            if ($('#FileArchivoAnexo3')[0].files.length === 0) { $("#LblfileArchivoAnexo3").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoAnexo3").text(""); }
        }
    } else {
        if ($('#FileArchivoAnexo3')[0].files.length === 0) { $("#LblfileArchivoAnexo3").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoAnexo3").text(""); }
    }


    if ($('#ID_ANEXO7').val() > 0) {
        if ($('#inputAnexo7Delete').val() == "1") {
            if ($('#FileArchivoAnexo7')[0].files.length === 0) { $("#LblfileArchivoAnexo7").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoAnexo7").text(""); }
        }
    } else {
        if ($('#FileArchivoAnexo7')[0].files.length === 0) { $("#LblfileArchivoAnexo7").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoAnexo7").text(""); }
    }
    if ($('#ID_BANCO').val() > 0) {
        if ($('#inputBCDelete').val() == "1") {
            if ($('#FileArchivoBancario')[0].files.length === 0) { $("#LblfileArchivoBancario").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoBancario").text(""); }
        }
    } else {
        if ($('#FileArchivoBancario')[0].files.length === 0) { $("#LblfileArchivoBancario").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoBancario").text(""); }
    }

    return result;
}
/////////******************* PROPUESTA FAG FIN ******************/////////

/////////******************* PROPUESTA PAC INICIO ******************/////////
function fn_RegistroPacSolicitud() {
    var text = "</br>¿Desea <b> Registrar</b> los datos.? </br >";
    if (ValidarPac()) {
        if ($("#H_MONTO_RECIBO").val() > 0) {
            if (ValidateArchivoPAC()) {
                DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                    if (r) {
                        $("#myModalCargando").fadeIn(100, function () {
                            fn_UpdateDJ_Pac();
                        });
                    }
                });
            }
        } else {
            DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                if (r) {
                    $("#myModalCargando").fadeIn(100, function () {
                        fn_UpdateDJ_Pac();
                    });
                }
            });
        }

    }
}
function ValidarPac() {
    var result = true;
    if ($("#MONTO_RECIBO").val() == "") { $("#MONTO_RECIBO").addClass('is-invalid'); result = false; } else { $("#MONTO_RECIBO").removeClass('is-invalid'); }
    if ($("#FLG_SUSPENSION").val() == "") { $("#FLG_SUSPENSION").addClass('is-invalid'); result = false; } else { $("#FLG_SUSPENSION").removeClass('is-invalid'); }
    if (document.getElementById('CheckCondiciones').checked) {
        $("#CheckCondiciones").removeClass('is-invalid');
    } else {
        $("#CheckCondiciones").addClass('is-invalid'); result = false;
    }
    return result;
}
function fn_EliminarArchivoPac(ID) {
    switch (ID) {
        case 13:
            $("#DivM_FORMATOB").css("display", "none");
            $("#DivO_FORMATOB").css("display", "block");
            $("#inputFORMATOBDelete").val("1");
            break;
        case 14:
            $("#DivM_FORMATOC").css("display", "none");
            $("#DivO_FORMATOC").css("display", "block");
            $("#inputFORMATOCDelete").val("1");
            break;
        case 18:
            $("#DivM_CV").css("display", "none");
            $("#DivO_CV").css("display", "block");
            $("#inputFORMATOCVDelete").val("1");
            break;
        default:
    }
}
function ValidateArchivoPAC() {
    var result = true;
    if ($('#ID_FORMATOB').val() > 0) {
        if ($('#inputFORMATOBDelete').val() == "1") {
            if ($('#FileArchivoFORMATOB')[0].files.length === 0) { $("#LblfileArchivoFORMATOB").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoFORMATOB").text(""); }
        }
    } else {
        if ($('#FileArchivoFORMATOB')[0].files.length === 0) { $("#LblfileArchivoFORMATOB").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoFORMATOB").text(""); }
    }

    if ($('#ID_FORMATOC').val() > 0) {
        if ($('#inputFORMATOCVDelete').val() == "1") {
            if ($('#FileArchivoFORMATOC')[0].files.length === 0) { $("#LblfileArchivoFORMATOC").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoFORMATOC").text(""); }
        }
    } else {
        if ($('#FileArchivoFORMATOC')[0].files.length === 0) { $("#LblfileArchivoFORMATOC").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoFORMATOC").text(""); }
    }

    if ($('#ID_PAC_ANEXO2').val() > 0) {
        if ($('#inputFORMATOCVDelete').val() == "1") {
            if ($('#FileArchivoFORMATOCV')[0].files.length === 0) { $("#LblfileArchivoFORMATOCV").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoFORMATOCV").text(""); }
        }
    } else {
        if ($('#FileArchivoFORMATOCV')[0].files.length === 0) { $("#LblfileArchivoFORMATOCV").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivoFORMATOCV").text(""); }
    }
    return result;
}
/////////******************* PROPUESTA PAC FIN ******************/////////
function fn_DesArchRepo(ID) {
    DESARROLLO.DESCARGAR_PERSONAL_DOCUMENTO_LF(ID);
}