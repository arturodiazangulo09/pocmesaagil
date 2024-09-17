/**  PROCESOS GLOBALES **/
var TableCoordinador_ = null;
/**  FIN PROCESOS GLOBALES **/
function CrearGrillaCoordinador() {
    DESARROLLO.configurarGrilla(true);
    TableCoordinador_ = $("#tabla_Coordinadores").DataTable({
        scrollX: true,
        paging: true,
        "autoWidth": true,
        "order": [[0, "desc"], [5, 'asc']],
        columns: [
            { "title": "ID_COORDINADOR", "data": "ID_COORDINADOR", "class": "text-center", "visible": false, 'orderable': false  },
            { "title": "USUARIO", "data": "", "class": "text-center",  'orderable': false  },
            { "title": "FORMATOS", "data": "", "class": "text-center", "width": "120px", 'orderable': false  },
            { "title": "DNI/CE", "data": "NUM_DOCUMENTO", "class": "text-center", "width": "100px", 'orderable': false },
            { "title": "APELLIDOS NOMBRES", "data": "NOMBRE_COMPLETO", "class": "text-left", "width": "250px", 'orderable': false  },
            { "title": "ENTIDAD", "data": "DESC_ENTIDAD", "class": "text-left", "width": "250px", 'orderable': true },
            { "title": "DEPENDENCIA", "data": "DEPENDENCIA", "class": "text-left", "width": "250px", 'orderable': false  },
            { "title": "CARGO", "data": "CARGO", "class": "text-left", "width": "200px", 'orderable': false  },
            { "title": "TIPO DE USUARIO", "data": "DESC_TIPO_USUSARIO", "class": "text-left", "width": "250px", 'orderable': false  },
            { "title": "FECHA DE REGISTRO", "data": "FEC_REGISTRO", "class": "text-center", 'orderable': true  },
            { "title": "FECHA DE EVALUACIÓN", "data": "FEC_EVALUACION", "class": "text-center", 'orderable': false },
            { "title": "ESTADO", "data": "", "class": "text-center", 'orderable': false },
            { "title": "V. LABORAL", "data": "DESC_VINCULO_LAB", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "FLG_SOLICITUD", "data": "FLG_SOLICITUD", "class": "text-center", "visible": false, 'orderable': false  },
            { "title": "NOMBRE_ARCHIVO_DNI", "data": "NOMBRE_ARCHIVO_DNI", "class": "text-center", "visible": false, 'orderable': false  },
            { "title": "ARCHIVO_SUSTENTO", "data": "ARCHIVO_SUSTENTO", "class": "text-center", "visible": false, 'orderable': false  },
            { "title": "ARCHIVO_DNI", "data": "ARCHIVO_DNI", "class": "text-center", "visible": false, 'orderable': false  },
            { "title": "OBSERVACION SOLICITUD", "data": "OBSERVACION_SOLICITUD", "class": "text-left", "width": "300px", 'orderable': false   },
            { "title": "FLG_ESTADO", "data": "FLG_ESTADO", "class": "text-center", "visible": false, 'orderable': false  },

        ],
        "columnDefs": [
            {
                'targets': 1,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO == "1") {
                        if (row.FLG_SOLICITUD == "1") {
                            html = '<button  class="btn btn-xs"  onClick="fn_DesactivarCoordinador(' + row.ID_COORDINADOR + ');" type="button" title="Desactivar usuario"><i class="fas fa-user-check" style="color:blue;font-size:18px"></i></button>';
                        } else {
                            if (row.FLG_SOLICITUD == "2") {
                                html = '<button  class="btn btn-xs" type="button" title="Usuario observado"><i class="fas fa-user-alt-slash" style="color:#4a4a49;font-size:18px"></i></button>';
                            } else {
                                html = '<button  class="btn btn-xs" type="button" title="Usuario pendiente de evaluar"><i class="fas fa-user-clock" style="color:#4a4a49;font-size:18px"></i></button>';

                            }
                    }
                    } else {
                        html = '<button  class="btn btn-xs" type="button" title="Usuario desactivado"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_MostrarSolicitud(' + row.ID_COORDINADOR + ',' + row.FLG_SOLICITUD +');" type="button" title="Ver Solicitud"><i class="fas fa-file-medical-alt" style="color:#0093F8;font-size:18px"></i></button>';
                    if (row.ARCHIVO_SUSTENTO > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivoDocumento(' + row.ARCHIVO_SUSTENTO + ');" type="button" title="Descargar documento de sustento"><i class="far fa-file-pdf" style="color:#e30613;font-size:18px"></i></button>';
                    }
                    if (row.ARCHIVO_DNI > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivoDocumento(' + row.ARCHIVO_DNI + ');" type="button" title="Descargar dni"><i class="fas fa-address-card" style="color:#6ABEB0;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = row.FEC_REGISTRO
                    return html;
                }
            },
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (row.FEC_EVALUACION!=null) {
                        html =  row.FEC_EVALUACION
                    }
                    
                    return html;
                }
            },
            {
                'targets': 11,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (row.FLG_ESTADO == "1") {
                        if (row.FLG_SOLICITUD == "1") {
                            html = 'APROBADO';
                        }
                        else {
                            if (row.FLG_SOLICITUD == "2") {
                                html = 'OBSERVADO';
                            } else {
                                html = 'PENDIENTE';
                            }
                        }
                    } else {
                        html = "USUARIO</br>DESACTIVADO";

                    }
                    return html;
                }
            },
            {
                'targets': 17,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (row.OBSERVACION_SOLICITUD != null) {
                        html = row.OBSERVACION_SOLICITUD;
                    }

                    return html;
                }
            },
           
        ],

    });
}
function CargarGrillaCoordinador() {
    var item =
    {
        FLG_SOLICITUD: $("#vId_Estado").val(),
        ID_ENTIDAD: $("#DcboEntidades").val(),
    };
    var url = baseUrl + 'Administracion/Coordinadores/ListaCoordinadores';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableCoordinador_.rows().remove().draw();
    if (respuesta != null && respuesta != "") {
        TableCoordinador_.rows.add(respuesta).draw();
    }
}
function Descargar_Archivo(ID_COORDINADOR, TIPO) {
    var proceso = '';
    if (TIPO == "1") {
        proceso = "DS";
    } else {
        proceso = "DNI";
    }
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Administracion/Entidad/VerArchivo?id=" + ID_COORDINADOR + "&proceso=" + proceso, function (responseText, textStatus, request) {
    });
}
function fn_MostrarSolicitud(id,estado) {
    jQuery("#modalSolicitud").html('');
    jQuery("#modalSolicitud").load(baseUrl + "Administracion/Coordinadores/Ver_Solicitud?ID=" + id + "&FLG_SOLICITUD=" + estado, function (responseText, textStatus, request) {
        $("#modalSolicitud").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_MantenimientoAccionesCoordinador(Accion,Obs) {
    item = {
        ID_COORDINADOR: $("#H_ID_COORDINADOR").val(),
        OBSERVACION_SOLICITUD: Obs,
        ACCION: Accion,
    };
    var url = baseUrl + 'Administracion/Coordinadores/MantenimientoAccionesCoordinador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            if (Accion == "A") { fn_CerrarSolicitud(); } else { fn_CerrarSolicitud(); $(".modal-backdrop").css("display", "none");};
            CargarGrillaCoordinador();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
    
    $("#myModalCargando").css("display", "none");
}
function fn_CerrarSolicitud() {
    jQuery("#modalSolicitud").html('');
    $('#modalSolicitud').modal('hide');
}
function fn_CerrarObservado() {
    jQuery("#modalObservado").html('');
    $('#modalObservado').modal('hide');
}
function fn_DesactivarCoordinador(ID) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>DESACTIVAR</b> al coordinador, recuerde que una vez desactivado no podrá volver a activarlo.? </br>", function (r) {
        if (r) {
    item = {
        ID_COORDINADOR: ID,
        OBSERVACION_SOLICITUD: "",
        ACCION: "DESAC",
    };
    var url = baseUrl + 'Administracion/Coordinadores/MantenimientoAccionesCoordinador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaCoordinador();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }

        }
    });

}
///*DESCARGAR ARCHIVO INI        
function fn_DescargarArchivoDocumento(ID) {
    DESARROLLO.DESCARGAR_DOCUMENTO_LF(ID);
}
///*FIN DESCARGAR ARHIVO