var TableSolicitud_ = null;
function fn_CerrarDescanso() {
    jQuery("#modalSolicitudDescanso").html('');
    $('#modalSolicitudDescanso').modal('hide');
}
function fn_CrearSolicitud(id) {
    jQuery("#modalCrearSolicitudDescanso").html('');
    jQuery("#modalCrearSolicitudDescanso").load(baseUrl + "Usuario/SolicitudDescanso/CrearSolicitudDescanso?ID_SUSPENSION=" + id, function (responseText, textStatus, request) {
        $("#modalCrearSolicitudDescanso").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarSolicitud() {
    jQuery("#modalCrearSolicitudDescanso").html('');
    $('#modalCrearSolicitudDescanso').modal('hide');
}
function fn_MantenimientoSolicitud() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_SUSPENSION", $("#ID_SUSPENSION").val());
    e.append("ID_CONTRATO", $("#ID_CONTRATO").val());
    e.append("FECHA_INICIO", $("#FEC_INICIO").val());
    e.append("FECHA_FIN", $("#FEC_FIN").val());
    e.append("FECHA_PERIODO_INICIO", $("#FEC_INICIO_PERIODO").val());
    e.append("FECHA_PERIODO_FIN", $("#FEC_FIN_PERIODO").val());
    e.append("DIAS_LIBRE", $("#DIAS_LIBRE").val());
    e.append("FLG_DESCUENTO", "");
    e.append("DIAS_DESCUENTO", 0);
    e.append("ID_ARCHIVO_U", $("#ID_ARCHIVO_U").val());

    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("TIPO_PROCESO", $("#TIPO_PROCESO").val());
    e.append("ACCION", $("#ACCION").val());
    $.ajax({
        url: baseUrl + "Usuario/SolicitudDescanso/MentenimientoSolicitud_Suspension",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarSolicitud();
                CargarGrillaSolicitud();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function CrearGrillaSolicitud() {
    DESARROLLO.configurarGrilla();
    TableSolicitud_ = $("#TableSolicitud").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_SUSPENSION", "data": "ID_SUSPENSION", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "CODIGO CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "FECHA DE </br>SOLICITUD", "data": "FECHA_SOLICITUD", "class": "text-center", },
            { "title": "FECHA DE </br>APROBACIÓN", "data": "FECHA_APRUEBA", "class": "text-center", },
            { "title": "FECHA DE INICIO </br> DE SUSPENSIÓN", "data": "FECHA_PERIODO_INICIO", "class": "text-center", "visible": false,  },
            { "title": "FECHA DE TÉRMINO </br> DE SUSPENSIÓN", "data": "FECHA_PERIODO_FIN", "class": "text-center", "visible": false, },



            { "title": "FECHA DE INICIO </br> DE SUSPENSIÓN", "data": "FECHA_INICIO", "class": "text-center",   },
            { "title": "FECHA DE TÉRMINO </br> DE SUSPENSIÓN", "data": "FECHA_FIN", "class": "text-center", "width": "1%",  },
            { "title": "CANTIDAD </br>DE DIAS", "data": "DIAS_LIBRE", "class": "text-center", },
     
            { "title": "ID_ARCHIVO_U", "data": "ID_ARCHIVO_U", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    
                    if (row.IDEDOCODIGO == "024" || row.IDEDOCODIGO == "027"  ) {
                        html = '<button  class="btn btn-xs"  onClick="fn_CrearSolicitud(' + row.ID_SUSPENSION + ');" type="button" title="Editar Estudio"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_EliminacionSolicitud(' + row.ID_SUSPENSION + ');" type="button" title="Eliminar Estudio"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        if (row.IDEDOCODIGO == "027" ) {
                            html += '<button  class="btn btn-xs"  onClick="fn_VerListaObservacionesC(' + row.ID_SUSPENSION + ');" type="button" title="Lista de observaciones"><i class="fas fa-list-ol" style="color:#575756;font-size:18px"></i></button>';
                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_EnviarSolicitud(' + row.ID_SUSPENSION + ',' + row.ID_CONTRATO + ');" type="button" title="Enviar solicitud a la entidad"><i class="far fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';

                    }

                    if (row.IDEDOCODIGO =="025") {

                        html = '<button  class="btn btn-xs" type="button" title="Se realizo el envio de la solicitud"><i class="fas fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';

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
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_U + ');" type="button" title="Descargar Archivo"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (DESARROLLO.FECHA_DATATABLE(row.FECHA_SOLICITUD) !="31/12/0") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_SOLICITUD);
                    }
              
                    return html;
                }
            },
            {
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (DESARROLLO.FECHA_DATATABLE(row.FECHA_APRUEBA) != "31/12/0") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_APRUEBA);
                    }
       
                    return html;
                }
            },
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO);
                    return html;
                }
            },
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaSolicitud() {
    
    var item =
    {
        ID_CONTRATO: $("#ID_CONTRATO").val(),
        ID_SUSPENSION:0,
    };
    var url = baseUrl + 'Usuario/SolicitudDescanso/ListaSolicitud_Suspension';
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
function fn_EliminacionSolicitud(ID) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b> Eliminar</b> la solicitud.? </br >", function (r) {
        if (r) {
            var e = new FormData;
            e.append("ID_SUSPENSION", ID);
            e.append("ID_CONTRATO", $("#ID_CONTRATO").val());
            e.append("FECHA_INICIO", $("#FEC_INICIO").val());
            e.append("FECHA_FIN", $("#FEC_FIN").val());
            e.append("FECHA_PERIODO_INICIO", $("#FEC_INICIO_PERIODO").val());
            e.append("FECHA_PERIODO_FIN", $("#FEC_FIN_PERIODO").val());
            e.append("DIAS_LIBRE", $("#DIAS_LIBRE").val());
            e.append("FLG_DESCUENTO", "");
            e.append("DIAS_DESCUENTO", 0);
            e.append("ID_ARCHIVO_U", $("#ID_ARCHIVO_U").val());

            e.append("ACCION", "D");
            $.ajax({
                url: baseUrl + "Usuario/SolicitudDescanso/MentenimientoSolicitud_Suspension",
                type: "POST",
                data: e,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                contentType: !1,
                processData: !1,
                success: function (p) {
                    if (p.success) {
                        fn_CerrarSolicitud();
                        CargarGrillaSolicitud();
                        DESARROLLO.PROCESO_CORRECTO(p.message);

                    }
                    else {
                        DESARROLLO.PROCESO_ERROR(p.extra);
                    }

                }
            });
        }
    });
}
function fn_EnviarSolicitud(ID_SUSPENSION, ID_CONTRATO) {
    DESARROLLO.PROCESO_CONFIRM("¿Está seguro que desea enviar su solicitud, una vez enviada la información no se podrá realizar modificaciones?", function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_EnvioSolicitudSuspension(ID_SUSPENSION, ID_CONTRATO);
            });
        }
    });
}
function fn_EnvioSolicitudSuspension(ID_SUSPENSION, ID_CONTRATO) {
    item = {
        ID_SUSPENSION: ID_SUSPENSION,
        ID_CONTRATO: ID_CONTRATO,
        IDEDOCODIGO: "025",
        CODIGO_CONTRATO: $("#CODIGO_CONTRATO").val(),
    };
    var url = baseUrl + 'Usuario/SolicitudDescanso/UpdEstado_Suspension';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaSolicitud();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
function fn_VerListaObservacionesC(id) {
    jQuery("#modalObservacionDescanso").html('');
    jQuery("#modalObservacionDescanso").load(baseUrl + "Usuario/SolicitudDescanso/VerObservacionSuspension?ID_SUSPENSION=" + id, function (responseText, textStatus, request) {
        $("#modalObservacionDescanso").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarObservacionDescanso() {
    jQuery("#modalObservacionDescanso").html('');
    $('#modalObservacionDescanso').modal('hide');
}
