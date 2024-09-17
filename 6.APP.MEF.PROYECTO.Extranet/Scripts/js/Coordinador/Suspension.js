var TableSolicitud_ = null;
function CrearGrillaSolicitud() {
    DESARROLLO.configurarGrilla();
    TableSolicitud_ = $("#TableSolicitud").DataTable({
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
            { "title": "DNI / CE", "data": "NUM_DOCUMENTO", "class": "text-center" },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left" },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-center" },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left" },
            { "title": "FECHA DE INICIO", "data": "FECHA_INICIO_CONTRATO", "class": "text-center", },
            { "title": "FECHA DE FIN", "data": "FECHA_FIN_CONTRATO", "class": "text-center", "width": "1%" },
            { "title": "H.R. ASIGNADA", "data": "NR_TRAMITE", "class": "text-center" },
            { "title": "FECHA DE INICIO", "data": "FECHA_INICIO_SUSPENSION", "class": "text-center"},
            { "title": "FECHA DE FIN", "data": "FECHA_FIN_SUSPENSION", "class": "text-center"},
            { "title": "DÍAS </br> OTORGADOS", "data": "DIAS_LIBRE", "class": "text-center", },
            { "title": "ID_ARCHIVO_U", "data": "ID_ARCHIVO_U", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_C", "data": "ID_ARCHIVO_C", "class": "text-center", "visible": false, },
            { "title": "ID_TRAMITE", "data": "ID_TRAMITE", "class": "text-center", "visible": false, },

            
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';


                    if (row.IDEDOCODIGO == "025") {

                        html = '<button  class="btn btn-xs"  onClick="fn_VerSuspension(' + row.ID_SUSPENSION + ');" type="button" title="Verificar solicitud"><i class="fas fa-user-edit" style="color:#575756;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SUSPENSION  + ');" type="button" title="Devolver solicitud al personal calificado"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';
                        if (row.ID_ARCHIVO_C > 0) {
                            html += '<button  class="btn btn-xs"  onClick="fn_EnviarSolicitudUTP(' + row.ID_SUSPENSION + ',' + row.ID_TRAMITE + ');" type="button" title="Enviar a UTP para la revisión para la aprobación de la solicitud"><i class="far fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';

                        }

                    }
                    if (row.IDEDOCODIGO == "027") {
                        html = '<button  class="btn btn-xs"  onClick="fn_VerListaObservacionesC(' + row.ID_SUSPENSION + ');" type="button" title="Lista de observaciones"><i class="fas fa-list-ol" style="color:#575756;font-size:18px"></i></button>';
                       }
                    if (row.IDEDOCODIGO == "028") {
                        html = '<button  class="btn btn-xs" type="button" title="Se realizo el envio a la Oficina de UTP del Ministerio Económica y Finanzas."><i class="fas fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';
                    }

                    if (row.IDEDOCODIGO == "030") {

                        html = '<button  class="btn btn-xs"  onClick="fn_VerSuspension(' + row.ID_SUSPENSION + ');" type="button" title="Verificar solicitud"><i class="fas fa-user-edit" style="color:#575756;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SUSPENSION + ');" type="button" title="Devolver solicitud al personal calificado"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_VerListaObservacionesU(' + row.ID_SUSPENSION + ');" type="button" title="Lista de observaciones"><i class="fas fa-list-ol" style="color:#575756;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_EnviarSolicitudUTP(' + row.ID_SUSPENSION + ',' + row.ID_TRAMITE +');" type="button" title="Enviar a UTP para la revisión para la aprobación de la solicitud"><i class="far fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';

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
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_U + ');" type="button" title="Descargar archivo cargado por el personal calificado"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    if (row.ID_ARCHIVO_C>0) {
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
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO_SUSPENSION);
                    return html;
                }
            },
            {
                'targets':13,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN_SUSPENSION);
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaSolicitud() {
    
    var item =
    {
        ID_CONTRATO:0,
        ID_SUSPENSION: 0,
        TIPO_PROCESO: $("#DdlTipoC").val(),
        ANIO: $("#txtAnio").val(),
        NUM_CONTRATO: $("#txtTdr").val(),
        IDEDOCODIGO: $("#DdlEstado").val(),
        ID_ENTIDAD: $("#inputHddIdEntidad").val(),
    };
    var url = baseUrl + 'Coordinador/Suspension/ListaSolicitud_Suspension';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitud_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableSolicitud_.rows.add(respuesta).draw();
    }
}
function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}
function fn_InsReevaluacion() {
    item = {
        ID_SUSPENSION: $("#ID_SUSPENSION").val(),
        DES_REEVALUACION: $("#TxtObservaciones").val(),
        TIPO: "C",
    };
    var url = baseUrl + 'Coordinador/Suspension/UpdateReevaluacionSuspension';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            fn_Cerrarobservacion();
            $("#myModalCargando").css("display", "none");
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaSolicitud();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
            $("#myModalCargando").css("display", "none");
        }

    }
}
function fn_Verobservacion(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Coordinador/Suspension/ObservacionProfesional?ID_SUSPENSION=" + ID , function (responseText, textStatus, request) {
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
function fn_VerListaObservacionesC(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Coordinador/Suspension/VerObservacionSuspension?ID_SUSPENSION=" + ID +"&TIPO=C", function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_VerListaObservacionesU(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Coordinador/Suspension/VerObservacionSuspension?ID_SUSPENSION=" + ID + "&TIPO=U", function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_VerSuspension(ID) {
    jQuery("#modalSuspension").html('');
    jQuery("#modalSuspension").load(baseUrl + "Coordinador/Suspension/VerSuspensionArchivo?ID_SUSPENSION=" + ID + "&TIPO=C", function (responseText, textStatus, request) {
        $("#modalSuspension").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_CerrarSuspension() {
    jQuery("#modalSuspension").html('');
    $('#modalSuspension').modal('hide');
}
function fn_MantenimientoSolicitud() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_SUSPENSION", $("#ID_SUSPENSION").val());
    e.append("ID_ARCHIVO_C", $("#ID_ARCHIVO_C").val());
    e.append("NUM_DOCUMENTO", $("#NUM_DOCUMENTO").val());
    e.append("ID_CONTRATO", $("#ID_CONTRATO").val());
    $.ajax({
        url: baseUrl + "Coordinador/Suspension/UpdArchivoSuspension",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarSuspension();
                CargarGrillaSolicitud();
                DESARROLLO.PROCESO_CORRECTO("El documento se registro correctamente");

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function fn_CerrarEnvioUTPDocumentos() {
    jQuery("#modalObservacion").html('');
    $('#modalObservacion').modal('hide');
}

function fn_EnviarSolicitudUTP(ID_SUSPENSION, ID_TRAMITE) {
    if (ID_TRAMITE>0) {
        DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Enviar</b> la solicitud a la Oficina de UTP del Ministerio Económica y Finanzas.? </br>", function (r) {
            if (r) {
                $("#myModalCargando").fadeIn(100, function () {
                    fn_UpdateEnvioSolicitudUTP_I(ID_SUSPENSION);
                });

            }
        })
    } else {
        jQuery("#modalObservacion").html('');
        jQuery("#modalObservacion").load(baseUrl + "Coordinador/Suspension/Ver_Integrar_STD?ID_SUSPENSION=" + ID_SUSPENSION, function (responseText, textStatus, request) {
            $("#modalObservacion").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });

    }


}

function fn_UpdateEnvioSolicitudUTP_ARCHIVO(ID_SUSPENSION) {
    var doc = document.getElementById("FileArchivoOficio").files.length;
    var doc_ = document.getElementById("FileArchivoResumen").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoOficio").files[l], e.append("FileArchivoOficio", a);
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoResumen").files[l], e.append("FileArchivoResumen", a);

    e.append("NR_OFICIO", $("#txtOficio").val());
    e.append("NR_FOLIOS", $("#txtFolios").val());
    e.append("ASUNTO", $("#txtAsunto").val());
    e.append("ID_SUSPENSION", $("#ID_SUSPENSION").val());
    e.append("NR_TRAMITE", $("#txtNTramite").val());
    e.append("FLG_CON_HR", $("#Ddlflgtramite").val());
    $.ajax({
        url: baseUrl + 'Coordinador/Suspension/UpdEnvioSuspension',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                fn_CerrarEnvioUTPDocumentos();
                CargarGrillaSolicitud();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });



    //item = {
    //    ID_SUSPENSION: ID_SUSPENSION,
    //};
    //var url = baseUrl + 'Coordinador/Suspension/UpdEnvioSuspension';
    //var respuesta = DESARROLLO.Ajax(url, item, false);
    //if (respuesta != null && respuesta != "") {
    //    if (respuesta.success) {
    //        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
    //        CargarGrillaSolicitud();
    //    } else {
    //        DESARROLLO.PROCESO_ERROR(respuesta.extra);
    //    }
    //    $("#myModalCargando").css("display", "none");
    //}
}

function fn_UpdateEnvioSolicitudUTP_I(ID_SUSPENSION) {
    item = {
        ID_SUSPENSION: ID_SUSPENSION,
    };
    var url = baseUrl + 'Coordinador/Suspension/UpdEnvioSuspension';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaSolicitud();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}