var tabla_periodo_ = null;
var f = new Date();
var x = 0;
var ModelMeses = ["ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO",
    "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"];
/*** PROCESO FAG */
function CrearGrillaPeriodoPago_Fag() {
    DESARROLLO.configurarGrilla();
    tabla_periodo_ = $("#tabla_periodo").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_PAGO", "data": "ID_PAGO", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVOS", "data": "", "class": "text-center", "width": "1%" },
            { "title": "AÑO", "data": "ANIO", "class": "text-center", },
            { "title": "ID_MES", "data": "NUM_MES", "class": "text-center", "visible": false,},
            { "title": "MES", "data": "DES_MES", "class": "text-center", },
            { "title": "HONORARIO", "data": "MONTO", "class": "text-center", },
            { "title": "N° COMPROBANTE", "data": "NR_COMPROBANTE", "class": "text-center", },
            { "title": "GENERADO", "data": "FECHA_GENERACION", "class": "text-center", },
            { "title": "IMPORTE", "data": "IMPORTE_COMPROBANTE", "class": "text-center", },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "visible": false, },

            { "title": "ID_ARCHIVO_RECIBO", "data": "ID_ARCHIVO_RECIBO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_CPE", "data": "ID_ARCHIVO_CPE", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
            { "title": "ID_CONFORMIDAD", "data": "ID_CONFORMIDAD", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_INFORME", "data": "ID_ARCHIVO_INFORME", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    //if (row.ID_ARCHIVO_RECIBO == 0) {
                       // if (x == 0) {

                            if (row.IDEDOCODIGO == "0") {
                                html = '<button  class="btn btn-xs"  onClick="fn_RegistroPago_Fag(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ');" type="button" title="Generar solicitud de Pago"><i class="fas fa-user-edit" style="color:#606060;font-size:18px"></i></button>';
                               // x = 1;
                            }
                      //  }
                   // }
                   // else {
                      //  x == 0;
                  //  }
                    if (row.IDEDOCODIGO == "010" || row.IDEDOCODIGO == "016") {
                        html = '<button  class="btn btn-xs"  onClick="fn_RegistroPago_Fag(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ');" type="button" title="Generar solicitud de Pago"><i class="fas fa-user-edit" style="color:#606060;font-size:18px"></i></button>';
                        if (row.IDEDOCODIGO == "016") {
                            html += '<button  class="btn btn-xs"  onClick="fn_VerObservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observación"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';
                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_EnviarSolicitudPago(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ',1);" type="button" title="Enviar solicitud de pago"><i class="far fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';
                    } else {
                        if (row.IDEDOCODIGO != "0") {
                            if (row.IDEDOCODIGO != "014") {
                                html = '<button  class="btn btn-xs" type="button" title="Se realizo el envio de la solicitud"><i class="fas fa-envelope"" style="color:#3ABEB0;font-size:18px"></i></button>';

                            } else {
                                html = '<button  class="btn btn-xs" type="button" title="Se aprueba la solicitud de pago"><i class="far fa-check-circle" style="color:#0093f8;font-size:18px"></i></button>';

                            }
                        }
                    }

                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.ID_ARCHIVO_RECIBO > 0) {
                        html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_RECIBO + ');" type="button" title="Descargar Recibo por Honorario"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                        if (row.ID_ARCHIVO_CPE > 0) {
                            html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CPE + ');" type="button" title="Descargar Constancia de validez de Comprobante de Pago"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_INFORME + ');" type="button" title="Descargar Informe de Actividades"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';

                    }
                    //html = '<button  class="btn btn-xs"  onClick="fn_DescargarFormacion(' + row.ID_FORMAC_ACADEMICA + ');" type="button" title="Descargar Archivo"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    return html;
                }
            },
            {
                "targets": [8],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (DESARROLLO.FECHA_DATATABLE(row.FECHA_GENERACION) != "31/12/0") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_GENERACION);
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
function CargarGrillaPeriodoPago_Fag() {
    
    var item =
    {
        ID_SOLICITUD: $('#ID_SOLICITUD').val()
    };
    var url = baseUrl + 'Usuario/SolicitudPago/ListaPeriodoPagoSolicitud';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    tabla_periodo_.clear().draw();
    if (respuesta != null && respuesta != "") {

        tabla_periodo_.rows.add(respuesta).draw();
    }
}
function fn_RegistroPago_Fag(id, id_mes) {
    jQuery("#modalPago").html('');
    jQuery("#modalPago").load(baseUrl + "Usuario/SolicitudPago/RegistroPago_Fag?ID_SOLICITUD=" + id + "&ID_MES=" + id_mes, function (responseText, textStatus, request) {
        $("#modalPago").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
/*** FIN PROCESO FAG */
/*** PROCESO PAC */
function CrearGrillaPeriodoPago_Pac() {
    DESARROLLO.configurarGrilla();
    tabla_periodo_ = $("#tabla_periodo").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_PAGO", "data": "ID_PAGO", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVOS", "data": "", "class": "text-center", "width": "1%" },
            { "title": "AÑO", "data": "ANIO", "class": "text-center", },
            { "title": "ID_MES", "data": "NUM_MES", "class": "text-center", "visible": false, },
            { "title": "MES", "data": "DES_MES", "class": "text-center", },
            { "title": "HONORARIO", "data": "MONTO", "class": "text-center", },
            { "title": "N° COMPROBANTE", "data": "NR_COMPROBANTE", "class": "text-center", },
            { "title": "GENERADO", "data": "FECHA_GENERACION", "class": "text-center", },
            { "title": "IMPORTE", "data": "IMPORTE_COMPROBANTE", "class": "text-center", },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "visible": false, },

            { "title": "ID_ARCHIVO_RECIBO", "data": "ID_ARCHIVO_RECIBO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_CPE", "data": "ID_ARCHIVO_CPE", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_INFORME", "data": "ID_ARCHIVO_INFORME", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
          
                    if (row.ID_ARCHIVO_RECIBO == 0) {
                   
                        if (x == 0) {
                            if (row.IDEDOCODIGO == "0") {
                       
                                html = '<button  class="btn btn-xs"  onClick="fn_RegistroPago_Fag(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ');" type="button" title="Generar solicitud de Pago"><i class="fas fa-user-edit" style="color:#606060;font-size:18px"></i></button>';
                                x = 1;
                            }
                        }
                    }
                    else {
                        x == 0;
                    }


                    if (row.IDEDOCODIGO == "010" || row.IDEDOCODIGO == "016") {
                        html = '<button  class="btn btn-xs"  onClick="fn_RegistroPago_Fag(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ');" type="button" title="Generar solicitud de Pago"><i class="fas fa-user-edit" style="color:#606060;font-size:18px"></i></button>';
                        if (row.IDEDOCODIGO == "016") {
                            html += '<button  class="btn btn-xs"  onClick="fn_VerObservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observación"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';
                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_EnviarSolicitudPago(' + row.ID_SOLICITUD + ',' + row.NUM_MES + ',1);" type="button" title="Enviar solicitud de pago"><i class="far fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';
                    } else {
                        if (row.IDEDOCODIGO != "0") {
                            if (row.IDEDOCODIGO != "014") {
                                html = '<button  class="btn btn-xs" type="button" title="Se realizo el envio de la solicitud"><i class="fas fa-envelope"" style="color:#3ABEB0;font-size:18px"></i></button>';

                            } else {
                                html = '<button  class="btn btn-xs" type="button" title="Se aprueba la solicitud de pago"><i class="far fa-check-circle" style="color:#0093f8;font-size:18px"></i></button>';

                            }
                        }
                    }
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.ID_ARCHIVO_RECIBO > 0) {
                        html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_RECIBO + ');" type="button" title="Descargar Recibo por Honorario"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                        if (row.ID_ARCHIVO_CPE>0) {
                            html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CPE + ');" type="button" title="Descargar Comprobante de Pago"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_INFORME + ');" type="button" title="Descargar Informe de Actividades"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                "targets": [8],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (DESARROLLO.FECHA_DATATABLE(row.FECHA_GENERACION) !="31/12/0") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_GENERACION);
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
function CargarGrillaPeriodoPago_Pac() {
    
    var item =
    {
        ID_SOLICITUD: $('#ID_SOLICITUD').val()
    };
    var url = baseUrl + 'Usuario/SolicitudPago/ListaPeriodoPagoSolicitud';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    tabla_periodo_.clear().draw();
    if (respuesta != null && respuesta != "") {

        tabla_periodo_.rows.add(respuesta).draw();
    }
}
function fn_RegistroPago_Pac(id,id_mes) {
    jQuery("#modalPago").html('');
    jQuery("#modalPago").load(baseUrl + "Usuario/SolicitudPago/RegistroPago_Pac?ID_SOLICITUD=" + id + "&ID_MES=" + id_mes, function (responseText, textStatus, request) {
        $("#modalPago").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
/*** FIN PROCESO PAC */
function fn_Cerrar() {
    jQuery("#modalSolicitudPago").html('');
    $('#modalSolicitudPago').modal('hide');   
}
function fn_CerrarPago() {
    jQuery("#modalPago").html('');
    $('#modalPago').modal('hide');
}
function fn_UpdateReciboHonorario() {
    var A1 = document.getElementById("FileArchivoRXH").files.length;
    var A2 = 0;//document.getElementById("FileArchivoCP").files.length;
    var A3 = document.getElementById("FileArchivoINFORME").files.length;
    var e = new FormData;
    if (A1 > 0) for (var a, l = 0; A1 > l; l++) a = document.getElementById("FileArchivoRXH").files[l], e.append("FileArchivoRXH", a);
    if (A2 > 0) for (var a, l = 0; A2 > l; l++) a = document.getElementById("FileArchivoCP").files[l], e.append("FileArchivoCP", a);
    if (A3 > 0) for (var a, l = 0; A3 > l; l++) a = document.getElementById("FileArchivoINFORME").files[l], e.append("FileArchivoINFORME", a);

    e.append("ID_SOLICITUD", $("#H_ID_SOLICITUD").val());
    e.append("NUM_MES", $("#H_NUM_MES").val());
    e.append("FECHA_GENERACION", $("#FECHA_GENERACION").val());
    e.append("DESC_SERVICIO", $("#DESC_SERVICIO").val());
    e.append("NR_COMPROBANTE", $("#NR_COMPROBANTE").val());
    e.append("IMPORTE_COMPROBANTE", $("#IMPORTE_COMPROBANTE").val());
    e.append("SERIE_COMPROBANTE", $("#SERIE_COMPROBANTE").val());
    e.append("FLG_CUARTA_CATEGORIA", $("#DdlSuspension").val());
    $.ajax({
        url: baseUrl + "Usuario/SolicitudPago/UpdateReciboHonorario",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                var tipo = $("#inputTIPO").val();
                DESARROLLO.PROCESO_CORRECTO(p.message);
                fn_CerrarPago();
                if (tipo == "F") {
                    CargarGrillaPeriodoPago_Fag();
                } else {
                    CargarGrillaPeriodoPago_Pac();
                }
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            jQuery("#myModalCargando").html('');
            $('#myModalCargando').modal('hide');
        }
    });
}
function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}
/*** INICIO SOLICITUD PAGO */
function fn_EnviarSolicitudPago(ID_SOLICITUD, NUM_MES, TIPO) {

    DESARROLLO.PROCESO_CONFIRM("¿Está seguro que desea enviar su solicitud de pago, una vez enviada la información no se podrá realizar modificaciones?", function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_EnvioSolicitudDePago(ID_SOLICITUD, NUM_MES,TIPO);
            });
        }
    });
}
function fn_EnvioSolicitudDePago(ID_SOLICITUD, NUM_MES, TIPO) {
    item = {
        ID_SOLICITUD: ID_SOLICITUD,
        NUM_MES: NUM_MES,
        IDEDOCODIGO: "011",
        DES_MES: ModelMeses[(parseInt(NUM_MES)-1)],
    };
    var url = baseUrl + 'Usuario/SolicitudPago/UpdateReciboEstado';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            if (TIPO == 1) {
                CargarGrillaPeriodoPago_Fag();
            } else {
                CargarGrillaPeriodoPago_Pac();
            }
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
/*** FIN SOLICITUD PAGO */
function fn_VerObservacion(ID) {
    jQuery("#modalObservacion_").html('');
    jQuery("#modalObservacion_").load(baseUrl + "Coordinador/Solicitud/VerObservacionProfesional?ID_SOLICITUD=" + ID +"&TIPO=016", function (responseText, textStatus, request) {
        $("#modalObservacion_").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}

function fn_Cerrarobservacion() {
    jQuery("#modalObservacion_").html('');
    $('#modalObservacion_').modal('hide');
}