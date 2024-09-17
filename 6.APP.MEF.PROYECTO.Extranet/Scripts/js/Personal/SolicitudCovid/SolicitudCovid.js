var TableSolicitudCovid_;

function fn_CerrarCovid() {
    jQuery("#modalSolicitudDescanso").html('');
    $('#modalSolicitudDescanso').modal('hide');
}
function fn_CerrarSolicitudCovid() {
    jQuery("#modalCrearSolicitudCovid").html('');
    $('#modalCrearSolicitudCovid').modal('hide');
}
function fn_CrearSolicitudCovid(id) {
    jQuery("#modalCrearSolicitudCovid").html('');
    jQuery("#modalCrearSolicitudCovid").load(baseUrl + "Usuario/SolicitudCovid/CrearSolicitudCovid?ID_COVID=" + id, function (responseText, textStatus, request) {
        $("#modalCrearSolicitudCovid").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_MantenimientoSolicitudCovid() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_COVID", $("#ID_COVID").val());
    e.append("ID_CONTRATO", $("#ID_CONTRATO").val());
    e.append("FECHA_INICIO", $("#FEC_INICIO").val());
    e.append("FECHA_FIN", $("#FEC_FIN").val());
    e.append("ID_ARCHIVO_U", $("#ID_ARCHIVO_U").val());
    e.append("ID_ARCHIVO_CM", $("#ID_ARCHIVO_CM").val());

    e.append("ACCION", $("#ACCION").val());
    $.ajax({
        url: baseUrl + "Usuario/SolicitudCovid/MentenimientoSolicitud_Covid",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarSolicitudCovid();
                CargarGrillaSolicitudCovid();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function CrearGrillaSolicitudCovid() {
    DESARROLLO.configurarGrilla();
    TableSolicitudCovid_ = $("#TableSolicitudCovid").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_COVID", "data": "ID_COVID", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "CODIGO CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "FECHA DE</br>SOLICITUD", "data": "FECHA_SOLICITUD", "class": "text-center", "width": "1%" },
            { "title": "FECHA DE</br>APROBACIÓN", "data": "FECHA_ENVIO_UTP", "class": "text-center", "width": "1%" },
            { "title": "FECHA DE INICIO DESCANSO</br>MÉDICO POR COVID 19", "data": "FECHA_INICIO", "class": "text-center", "width": "1%" },
            { "title": "FECHA DE TÉRMINO DESCANSO</br>MÉDICO POR COVID 19", "data": "FECHA_FIN", "class": "text-center", "width": "1%" },
            { "title": "CANTIDAD</br>DE DIAS", "data": "NUM_DIAS", "class": "text-center", "width": "1%" },           
            { "title": "ID_ARCHIVO_U", "data": "ID_ARCHIVO_U", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    
                    if (row.IDEDOCODIGO == "024" || row.IDEDOCODIGO == "027") {
                        html = '<button  class="btn btn-xs"  onClick="fn_CrearSolicitudCovid(' + row.ID_COVID + ');" type="button" title="Editar Estudio"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_EliminarSolicitudCovid(' + row.ID_COVID + ');" type="button" title="Eliminar Estudio"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        if (row.IDEDOCODIGO == "027") {
                            html += '<button  class="btn btn-xs"  onClick="fn_VerListaObservacionesC(' + row.ID_COVID + ');" type="button" title="Lista de observaciones"><i class="fas fa-list-ol" style="color:#575756;font-size:18px"></i></button>';
                        }
                        html += '<button  class="btn btn-xs"  onClick="fn_EnviarSolicitudCovid(' + row.ID_COVID + ',' + row.ID_CONTRATO + ');" type="button" title="Enviar solicitud a la entidad"><i class="far fa-envelope" style="color:#3ABEB0;font-size:18px"></i></button>';

                    }

                    if (row.IDEDOCODIGO == "025") {

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
                    if (DESARROLLO.FECHA_DATATABLE(row.FECHA_SOLICITUD) != "31/12/0") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_SOLICITUD);
                    }


                    return html;
                }
            },
            {
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (DESARROLLO.FECHA_DATATABLE(row.FECHA_ENVIO_UTP) != "31/12/0") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_ENVIO_UTP);
                    }
                
                    return html;
                }
            },
            {
                'targets': 7,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO);
                    return html;
                }
            },
            {
                'targets': 8,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaSolicitudCovid() {
    
    var item =
    {
        ID_CONTRATO: $("#ID_CONTRATO").val(),
        ID_COVID: 0,
    };
    var url = baseUrl + 'Usuario/SolicitudCovid/ListaSolicitud_Covid';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitudCovid_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableSolicitudCovid_.rows.add(respuesta).draw();
    }
}
function fn_EliminarSolicitudCovid(ID) {
    var e = new FormData;
    e.append("ID_COVID",ID);
    e.append("ACCION","D");
    $.ajax({
        url: baseUrl + "Usuario/SolicitudCovid/MentenimientoSolicitud_Covid",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarSolicitudCovid();
                CargarGrillaSolicitudCovid();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function fn_EnviarSolicitudCovid(ID_COVID, ID_CONTRATO) {
    DESARROLLO.PROCESO_CONFIRM("¿Está seguro que desea enviar su solicitud, una vez enviada la información no se podrá realizar modificaciones?", function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_EnvioSolicitudCovid_(ID_COVID, ID_CONTRATO);
            });
        }
    });
}
function fn_EnvioSolicitudCovid_(ID_COVID, ID_CONTRATO) {
    item = {
        ID_COVID: ID_COVID,
        ID_CONTRATO: ID_CONTRATO,
        IDEDOCODIGO: "025",
        CODIGO_CONTRATO: $("#CODIGO_CONTRATO").val(),
    };
    var url = baseUrl + 'Usuario/SolicitudCovid/UpdEstado_Covid';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaSolicitudCovid();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}
function fn_VerListaObservacionesC(id) {
    jQuery("#modalObservacionCovid").html('');
    jQuery("#modalObservacionCovid").load(baseUrl + "Usuario/SolicitudCovid/VerObservacionCovid?ID_COVID=" + id, function (responseText, textStatus, request) {
        $("#modalObservacionCovid").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarObservacionCovid() {
    jQuery("#modalObservacionCovid").html('');
    $('#modalObservacionCovid').modal('hide');
}


