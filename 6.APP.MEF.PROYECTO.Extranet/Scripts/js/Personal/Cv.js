/**  PROCESOS GLOBALES **/
var TableFormacion_ = null;
var TableEspecializacion_ = null;
var TableCapacitacion_ = null;
var TableExperiencia_ = null;
/**  FIN PROCESOS GLOBALES **/
/**
 * 
 * param FORMACIÓN ACADEMICA
 */
function fn_Formacion(id) {
    jQuery("#modalFormacion").html('');
    jQuery("#modalFormacion").load(baseUrl + "Usuario/Personal/MantenimientoEstudios?ESTUDIOS="+id, function (responseText, textStatus, request) {
        $("#modalFormacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_MantenimientoFormacion() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_FORMAC_ACADEMICA", $("#ID_FORMAC_ACADEMICA").val());
    e.append("FEC_EMISION_FORMAC_ACADEMICA", $("#FEC_EMISION").val());
    e.append("DESC_ACADEMICA", $("#DESC_ACADEMICA").val());
    e.append("ID_NIVEL_ACADEMICO", $("#ID_NIVEL_ACADEMICO").val());
    e.append("ID_NIVEL_GRADO", $("#ID_NIVEL_GRADO").val());
    e.append("DESC_CENTRO_ESTUDIOS", $("#DESC_CENTRO_ESTUDIOS").val());
    e.append("DESC_CIUDAD_PAIS", $("#DESC_CIUDAD_PAIS").val());
    e.append("ACCION", $("#ACCION").val());
    e.append("ARCHIVO", $("#ARCHIVO").val());
    
    $.ajax({
        url: baseUrl + "Usuario/Personal/DML_MantenimientoEstudio",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarFormacion();
               CargarGrillaFormacion();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function fn_CerrarFormacion() {
    jQuery("#modalFormacion").html('');
    $('#modalFormacion').modal('hide');
}
function CrearGrillaFormacion() {
    DESARROLLO.configurarGrilla();
    TableFormacion_ = $("#TableFormacion").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": true,
        "bLengthChange": false,
        "bInfo": false,
        columns: [
            { "title": "ID_FORMAC_ACADEMICA", "data": "ID_FORMAC_ACADEMICA", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center acticons", "width": "1%" },
            { "title": "FORMACIÓN", "data": "DESC_NIVEL_ACADEMICO", "class": "text-center", },
            { "title": "TITULO O</br>GRADO ACADÉMICO", "data": "NOMBRE_NIVEL", "class": "text-center", },
            { "title": "ESPECIALIDAD", "data": "DESC_ACADEMICA", "class": "text-left", "width": "1%" },
            { "title": "FECHA DE<br/>EXPEDICIÓN", "data": "FEC_EMISION_FORMAC_ACADEMICA", "class": "text-center", },
            { "title": "CENTRO DE ESTUDIOS", "data": "DESC_CENTRO_ESTUDIOS", "class": "text-left",  },
            { "title": "CIUDAD/PAÍS", "data": "DESC_CIUDAD_PAIS", "class": "text-center", },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-left", "visible": false, },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs idownload" onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-download"></i></button>';
                    html += '<button  class="btn btn-xs iedit" onClick="fn_Formacion(' + row.ID_FORMAC_ACADEMICA + ');" type="button" title="Editar Estudio"><i class="fas fa-pen-square"></i></button>';
                    html += '<button  class="btn btn-xs iremove" onClick="fn_DeleteFormacion(' + row.ID_FORMAC_ACADEMICA + ');" type="button" title="Eliminar Estudio"><i class="fas fa-trash"></i></button>';
                    return html;
                }
            },
            {
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_EMISION_FORMAC_ACADEMICA);;
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaFormacion() {
    
    var item =
    {
        ID_PERSONAL: $('#inputHddIdUsuarioSesion').val()
    };
    var url = baseUrl + 'Usuario/Personal/ListaEstudios';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableFormacion_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableFormacion_.rows.add(respuesta).draw();
    }
}
function fn_Eliminararchivoformacion(id) {
    $("#DivMuestraAr").css("display", "none");
    $("#DivAdjuntarar").css("display", "block");
    $("#inputVerificaDelete").val("1");
}
//function fn_DescargarFormacion(ID) {
//    jQuery("#myModalDescarga").html('');
//    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=CV_F", function (responseText, textStatus, request) {
//    });
//}
function fn_DeleteFormacion(id) {
    var e = new FormData;
    e.append("ID_FORMAC_ACADEMICA", id);
    e.append("FEC_EMISION_FORMAC_ACADEMICA", $("#FEC_EMISION").val());
    e.append("DESC_ACADEMICA", $("#DESC_ACADEMICA").val());
    e.append("ID_NIVEL_ACADEMICO", $("#ID_NIVEL_ACADEMICO").val());
    e.append("ID_NIVEL_GRADO", $("#ID_NIVEL_GRADO").val());
    e.append("DESC_CENTRO_ESTUDIOS", $("#DESC_CENTRO_ESTUDIOS").val());
    e.append("DESC_CIUDAD_PAIS", $("#DESC_CIUDAD_PAIS").val());
    e.append("ACCION", "D");

    var text = "</br>¿Desea <b> Elimina</b> el estudio.? </br >";
    DESARROLLO.PROCESO_CONFIRM(text, function (r) {
        if (r) {
            $.ajax({
                url: baseUrl + "Usuario/Personal/DML_MantenimientoEstudio",
                type: "POST",
                data: e,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                contentType: !1,
                processData: !1,
                success: function (p) {
                    if (p.success) {
                        CargarGrillaFormacion();
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
/**
 *
 * param ESPECIALIZACIÓN
 */
function fn_Especializacion(id) {
    jQuery("#modalEspecializacion").html('');
    jQuery("#modalEspecializacion").load(baseUrl + "Usuario/Personal/MantenimientoEspecializacon?ESPECIALIZACION=" + id, function (responseText, textStatus, request) {
        $("#modalEspecializacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_MantenimientoEspecializacion() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_ESPECIALIZACION", $("#ID_ESPECIALIZACION").val());
    e.append("FEC_INICIO_ESPECIALIZACION", $("#FEC_INICIO").val());
    e.append("FEC_FIN_ESPECIALIZACION", $("#FEC_FIN").val());
    e.append("DESC_CENTRO_ESTUDIOS", $("#DESC_CENTRO_ESTUDIOS").val());
    e.append("ID_TIPO_ESPECIALIZACION", $("#ID_TIPO_ESPECIALIZACION").val());
    e.append("NOMBRE_ESPECIALIZACION", $("#NOMBRE_ESPECIALIZACION").val());
    e.append("NOMBRE_CIUDAD_PAIS", $("#NOMBRE_CIUDAD_PAIS").val());
    e.append("HORAS", $("#HORAS").val()); 
    e.append("ACCION", $("#ACCION").val());
    e.append("ARCHIVO", $("#ARCHIVO").val());
    $.ajax({
        url: baseUrl + "Usuario/Personal/DML_MantenimientoEspecializacion",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarEspecializacion();
                CargarGrillaEspecializacion();
                DESARROLLO.PROCESO_CORRECTO(p.message);
                DESARROLLO.CERRAR_MODAL('myModalCargando');
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function fn_CerrarEspecializacion() {
    jQuery("#modalEspecializacion").html('');
    $('#modalEspecializacion').modal('hide');
}
function CrearGrillaEspecializacion() {
    DESARROLLO.configurarGrilla();
    TableEspecializacion_ = $("#TableEspecializacion").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bInfo": false,
        columns: [
            { "title": "ID_ESPECIALIZACION", "data": "ID_ESPECIALIZACION", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center acticons", "width": "1%" },
            { "title": "TIPO", "data": "DESC_TIPO_ESPECIALIZACION", "class": "text-center", },
            { "title": "ESPECIALIDAD", "data": "NOMBRE_ESPECIALIZACION", "class": "text-left", },
            { "title": "INICIO", "data": "FEC_INICIO_ESPECIALIZACION", "class": "text-center", "width": "1%" },
            { "title": "FIN", "data": "FEC_FIN_ESPECIALIZACION", "class": "text-center", },
            { "title": "HORAS", "data": "HORAS", "class": "text-center" , "visible": false,},
            { "title": "INSTITUCIÓN", "data": "DESC_CENTRO_ESTUDIOS", "class": "text-left", },
            { "title": "CIUDAD/PAÍS", "data": "NOMBRE_CIUDAD_PAIS", "class": "text-center", },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-left", "visible": false,  },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs idownload"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-download"></i></button>';
                    html += '<button  class="btn btn-xs iedit"  onClick="fn_Especializacion(' + row.ID_ESPECIALIZACION + ');" type="button" title="Editar Estudio"><i class="fas fa-pen-square"></i></button>';
                    html += '<button  class="btn btn-xs iremove"  onClick="fn_DeleteEspecializacion(' + row.ID_ESPECIALIZACION + ');" type="button" title="Eliminar Estudio"><i class="fas fa-trash"></i></button>';
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_ESPECIALIZACION);;
                    return html;
                }
            },
            {
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_FIN_ESPECIALIZACION);;
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaEspecializacion() {
    
    var item =
    {
        ID_PERSONAL: $('#inputHddIdUsuarioSesion').val()
    };
    var url = baseUrl + 'Usuario/Personal/ListaEspecializacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableEspecializacion_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableEspecializacion_.rows.add(respuesta).draw();
    }
}
//function fn_DescargarEspecializacion(ID) {
//    jQuery("#myModalDescarga").html('');
//    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=CV_E", function (responseText, textStatus, request) {
//    });
//}
function fn_Eliminararchivoespecializacion(id) {
    $("#DivMuestraAr").css("display", "none");
    $("#DivAdjuntarar").css("display", "block");
    $("#inputVerificaDelete").val("1");
}
function fn_DeleteEspecializacion(id) {
    var e = new FormData;
    e.append("ID_ESPECIALIZACION", id);
    e.append("FEC_INICIO_ESPECIALIZACION", $("#FEC_INICIO").val());
    e.append("FEC_FIN_ESPECIALIZACION", $("#FEC_FIN").val());
    e.append("DESC_CENTRO_ESTUDIOS", $("#DESC_CENTRO_ESTUDIOS").val());
    e.append("ID_TIPO_ESPECIALIZACION", $("#ID_TIPO_ESPECIALIZACION").val());
    e.append("NOMBRE_ESPECIALIZACION", $("#NOMBRE_ESPECIALIZACION").val());
    e.append("NOMBRE_CIUDAD_PAIS", $("#NOMBRE_CIUDAD_PAIS").val());
    e.append("ACCION", "D");
    var text = "</br>¿Desea <b> Elimina</b> la especialización.? </br >";
    DESARROLLO.PROCESO_CONFIRM(text, function (r) {
        if (r) {
            $.ajax({
                url: baseUrl + "Usuario/Personal/DML_MantenimientoEspecializacion",
                type: "POST",
                data: e,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                contentType: !1,
                processData: !1,
                success: function (p) {
                    if (p.success) {
                        CargarGrillaEspecializacion();
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
/**
 *
 * param CAPACITACION
 */
function fn_Capacitacion(id) {
    jQuery("#modalCapacitacion").html('');
    jQuery("#modalCapacitacion").load(baseUrl + "Usuario/Personal/MantenimientoCapacitacion?ID_CAPACITACION=" + id, function (responseText, textStatus, request) {
        $("#modalCapacitacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarCapacitacion() {
    jQuery("#modalCapacitacion").html('');
    $('#modalCapacitacion').modal('hide');
}
//function fn_DescargarCapacitacion(ID) {
//    jQuery("#myModalDescarga").html('');
//    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=CV_C", function (responseText, textStatus, request) {
//    });
//}
function fn_EliminararchivoCapacitacion(id) {
    $("#DivMuestraAr").css("display", "none");
    $("#DivAdjuntarar").css("display", "block");
    $("#inputVerificaDelete").val("1");
}
function fn_MantenimientoCapacitacion() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_CAPACITACION", $("#ID_CAPACITACION").val());
    e.append("FEC_INICIO_CAPACITACION", $("#FEC_INICIO").val());
    e.append("FEC_FIN_CAPACITACION", $("#FEC_FIN").val());
    e.append("DESC_CENTRO_ESTUDIOS", $("#DESC_CENTRO_ESTUDIOS").val());
    e.append("NOMBRE_CAPACITACION", $("#NOMBRE_CAPACITACION").val());
    e.append("NOMBRE_CIUDAD_PAIS", $("#NOMBRE_CIUDAD_PAIS").val());
    e.append("ACCION", $("#ACCION").val());
    e.append("ARCHIVO", $("#ARCHIVO").val());
    $.ajax({
        url: baseUrl + "Usuario/Personal/DML_MantenimientoCapacitacion",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarCapacitacion();
                CargarGrillaCapacitacion();
                DESARROLLO.PROCESO_CORRECTO(p.message);
                DESARROLLO.CERRAR_MODAL('myModalCargando');
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function CrearGrillaCapacitacion() {
    DESARROLLO.configurarGrilla();
    TableCapacitacion_ = $("#TableCapacitacion").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bInfo": false,
        columns: [
            { "title": "ID_CAPACITACION", "data": "ID_CAPACITACION", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center acticons", "width": "1%" },
            { "title": "NOMBRE", "data": "NOMBRE_CAPACITACION", "class": "text-left", },
            { "title": "INICIO", "data": "FEC_INICIO_CAPACITACION", "class": "text-center", "width": "1%" },
            { "title": "FIN", "data": "FEC_FIN_CAPACITACION", "class": "text-center", },
            { "title": "INSTITUCIÓN", "data": "DESC_CENTRO_ESTUDIOS", "class": "text-left", },
            { "title": "CIUDAD/PAÍS", "data": "NOMBRE_CIUDAD_PAIS", "class": "text-center", },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-left", "visible": false,  },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs idownload"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-download"></i></button>';
                    html += '<button  class="btn btn-xs iedit"  onClick="fn_Capacitacion(' + row.ID_CAPACITACION + ');" type="button" title="Editar Estudio"><i class="fas fa-pen-square"></i></button>';
                    html += '<button  class="btn btn-xs iremove"  onClick="fn_DeleteCapacitacion(' + row.ID_CAPACITACION + ');" type="button" title="Eliminar Estudio"><i class="fas fa-trash"></i></button>';
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_CAPACITACION);;
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_FIN_CAPACITACION);;
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaCapacitacion() {
    
    var item =
    {
        ID_PERSONAL: $('#inputHddIdUsuarioSesion').val()
    };
    var url = baseUrl + 'Usuario/Personal/ListaCapacitacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableCapacitacion_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableCapacitacion_.rows.add(respuesta).draw();
    }
}
function fn_DeleteCapacitacion(id) {
    var e = new FormData;
    e.append("ID_CAPACITACION", id);
    e.append("FEC_INICIO_CAPACITACION", $("#FEC_INICIO").val());
    e.append("FEC_FIN_CAPACITACION", $("#FEC_FIN").val());
    e.append("DESC_CENTRO_ESTUDIOS", $("#DESC_CENTRO_ESTUDIOS").val());
    e.append("NOMBRE_CAPACITACION", $("#NOMBRE_CAPACITACION").val());
    e.append("NOMBRE_CIUDAD_PAIS", $("#NOMBRE_CIUDAD_PAIS").val());
    e.append("ACCION", "D");
    var text = "</br>¿Desea <b> Elimina</b> la capacitación.? </br >";
    DESARROLLO.PROCESO_CONFIRM(text, function (r) {
        if (r) {
            $.ajax({
                url: baseUrl + "Usuario/Personal/DML_MantenimientoCapacitacion",
                type: "POST",
                data: e,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                contentType: !1,
                processData: !1,
                success: function (p) {
                    if (p.success) {
                        CargarGrillaCapacitacion();
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
/**
 *
 * param EXPERIENCIA
 */
function fn_Experiencia(id) {
    jQuery("#modalExperiencia").html('');
    jQuery("#modalExperiencia").load(baseUrl + "Usuario/Personal/MantenimientoExperiencia?ID_EXPERIENCIA=" + id, function (responseText, textStatus, request) {
        $("#modalExperiencia").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarExperiencia() {
    jQuery("#modalExperiencia").html('');
    $('#modalExperiencia').modal('hide');
}
//function fn_DescargarExperiencia(ID) {
//    jQuery("#myModalDescarga").html('');
//    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=CV_EX", function (responseText, textStatus, request) {
//    });
//}
function fn_EliminararchivoExperiencia(id) {
    $("#DivMuestraAr").css("display", "none");
    $("#DivAdjuntarar").css("display", "block");
    $("#inputVerificaDelete").val("1");
}
function fn_MantenimientoExperiencia() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_EXPERIENCIA", $("#ID_EXPERIENCIA").val());
    e.append("ID_TIPO_EXPERIENCIA", $("#ID_TIPO_EXPERIENCIA").val());
    e.append("NOMBRE_ENTIDAD", $("#NOMBRE_ENTIDAD").val());
    e.append("CARGO_EMPRESA", $("#CARGO_EMPRESA").val());
    e.append("FEC_INICIO_EXPERIENCIA", $("#FEC_INICIO").val());
    e.append("FEC_FIN_EXPERIENCIA", $("#FEC_FIN").val());
    e.append("NUM_ANIOS", $("#NUM_ANIOS").val());
    e.append("NUM_MESES", $("#NUM_MESES").val());
    e.append("NUM_DIAS", $("#NUM_DIAS").val());
    e.append("FUNCIONES", $("#FUNCIONES").val());
    e.append("ACCION", $("#ACCION").val());
    e.append("ARCHIVO", $("#ARCHIVO").val());
    $.ajax({
        url: baseUrl + "Usuario/Personal/DML_MantenimientoExperiencia",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarExperiencia();
                CargarGrillaExperiencia();
                DESARROLLO.PROCESO_CORRECTO(p.message);
                DESARROLLO.CERRAR_MODAL('myModalCargando');
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function CrearGrillaExperiencia() {
    DESARROLLO.configurarGrilla();
    TableExperiencia_ = $("#TableExperiencia").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bInfo": false,
        columns: [
            { "title": "ID_EXPERIENCIA", "data": "ID_EXPERIENCIA", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center acticons", "width": "1%" },
            { "title": "TIPO", "data": "DESC_TIPO_EXPERIENCIA", "class": "text-center", },
            { "title": "CENTRO LABORAL", "data": "NOMBRE_ENTIDAD", "class": "text-left", },
            { "title": "CARGO", "data": "CARGO_EMPRESA", "class": "text-left", },
            { "title": "INICIO", "data": "FEC_INICIO_EXPERIENCIA", "class": "text-center", "width": "1%" },
            { "title": "FIN", "data": "FEC_FIN_EXPERIENCIA", "class": "text-center", },
            { "title": "Año(s)", "data": "NUM_ANIOS_TOTAL", "class": "text-center",  },
            { "title": "Mes(es)", "data": "NUM_MESES_TOTAL", "class": "text-center", },
            { "title": "Día(s)", "data": "NUM_DIAS_TOTAL", "class": "text-center", },
            { "title": "DESCRIPCIÓN DEL TRABAJO REALIZADO", "data": "FUNCIONES", "class": "text-left", },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-left", "visible": false,  },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs idownload"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-download"></i></button>';
                    html += '<button  class="btn btn-xs iedit"  onClick="fn_Experiencia(' + row.ID_EXPERIENCIA + ');" type="button" title="Editar Experiencia"><i class="fas fa-pen-square"></i></button>';
                    html += '<button  class="btn btn-xs iremove"  onClick="fn_DeleteExperiencia(' + row.ID_EXPERIENCIA + ');" type="button" title="Eliminar Experiencia"><i class="fas fa-trash"></i></button>';
                    return html;
                }
            },
            {
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_EXPERIENCIA);;
                    return html;
                }
            },
            {
                'targets': 7,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_FIN_EXPERIENCIA);;
                    return html;
                }
            },
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
                 };
                 var xdias = 0;
                 var xmeses = 0;
                 var xanios = 0;
                 //DIAS
                 xdias = api
                     .column(10)
                     .data()
                     .reduce(function (a, b) {
                         return intVal(a) + intVal(b);
                     }, 0);
                 //MESES
                 xmeses = api
                     .column(9)
                     .data()
                     .reduce(function (a, b) {
                         return intVal(a) + intVal(b);
                     }, 0);
                 //ANIOS
                 xanios = api
                     .column(8)
                     .data()
                     .reduce(function (a, b) {
                         return intVal(a) + intVal(b);
                     }, 0);
                 if (xdias >= 30) {
                     dias = xdias % 30;
                     if ((xmeses + Math.floor(xdias / 30)) > 12) {
                         meses = (xmeses + Math.floor(xdias / 30)) % 12;
                     }
                     else {
                         if ((xmeses + Math.floor(xdias / 30)) == 12) {
                             meses = 0;
                         }
                         else {
                             meses = xmeses + Math.floor(xdias / 30);
                         }
                     }
                 }
                 else {
                     dias = xdias;
                     if ((xmeses + Math.floor(xdias / 30)) > 12) {
                         meses = (xmeses + Math.floor(xdias / 30)) % 12;
                     }
                     else {
                         if ((xmeses + Math.floor(xdias / 30)) == 12) {
                             meses = 0;
                             xmeses = 0;
                             xanios += 1;
                         }
                         else {
                             meses = xmeses;
                         }
                     }
                 }
                 anios = xanios + Math.floor((xmeses + Math.floor(xdias / 30)) / 12)
                 $(api.column(6).footer()).html('<p class="text-primary text-center text-bold" style="margin:0px">Tiempo Total de Servicio</span>');
                 $(api.column(8).footer()).html('<p class="text-muted text-center text-bold" style="margin:0px">' + anios+ '</span>');
                 $(api.column(9).footer()).html('<p class="text-muted text-center text-bold" style="margin:0px">' + meses+'</span>');
                 $(api.column(10).footer()).html('<p class="text-muted text-center text-bold" style="margin:0px">' + dias+'</span>');
        }
    });
}
function CargarGrillaExperiencia() {
    
    var item =
    {
        ID_PERSONAL: $('#inputHddIdUsuarioSesion').val()
    };
    var url = baseUrl + 'Usuario/Personal/ListaExperiencia';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableExperiencia_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableExperiencia_.rows.add(respuesta).draw();
    }
}
function fn_DeleteExperiencia(id) {
    var e = new FormData;
    e.append("ID_EXPERIENCIA", id);
    e.append("ID_TIPO_EXPERIENCIA", $("#ID_TIPO_EXPERIENCIA").val());
    e.append("NOMBRE_ENTIDAD", $("#NOMBRE_ENTIDAD").val());
    e.append("CARGO_EMPRESA", $("#CARGO_EMPRESA").val());
    e.append("FEC_INICIO_EXPERIENCIA", $("#FEC_INICIO").val());
    e.append("FEC_FIN_EXPERIENCIA", $("#FEC_FIN").val());
    e.append("NUM_ANIOS", $("#NUM_ANIOS").val());
    e.append("NUM_MESES", $("#NUM_MESES").val());
    e.append("NUM_DIAS", $("#NUM_DIAS").val());
    e.append("FUNCIONES", $("#FUNCIONES").val());
    e.append("ACCION", "D");
    var text = "</br>¿Desea <b> Eliminar</b> la experiencia.? </br >";
    DESARROLLO.PROCESO_CONFIRM(text, function (r) {
        if (r) {
            $.ajax({
                url: baseUrl + "Usuario/Personal/DML_MantenimientoExperiencia",
                type: "POST",
                data: e,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                contentType: !1,
                processData: !1,
                success: function (p) {
                    if (p.success) {
                        CargarGrillaExperiencia();
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
function validate_fecha(fecha) {
    var patron = new RegExp("^([0-9]{1,2})([/])([0-9]{1,2})([/])(19|20)+([0-9]{2})$");

    if (fecha.search(patron) == 0) {
        var values = fecha.split("/");
        if (isValidDate(values[0], values[1], values[2])) {
            return true;
        }
    }
    return false;
}
function isValidDate(day, month, year) {
    var dteDate;
    month = month - 1;
    dteDate = new Date(year, month, day);
    return ((day == dteDate.getDate()) && (month == dteDate.getMonth()) && (year == dteDate.getFullYear()));

}
function fn_CalculaTiempoServicio() {
    var fechaInicio = document.getElementById("FEC_INICIO").value;
    var fechaFin = document.getElementById("FEC_FIN").value;
    validate_fecha(fechaInicio)
    validate_fecha(fechaFin)
            item = {
                FEC_INICIO_EXPERIENCIA: fechaInicio,//$("#FEC_INICIO_VALIDADO").val(),
                FEC_FIN_EXPERIENCIA: fechaFin //$("#FEC_FIN_VALIDADO").val(),
            }
            var url = baseUrl + 'Usuario/Personal/CalcularExperiencia';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                $("#NUM_ANIOS").val(respuesta.NUM_ANIOS);
                $("#NUM_MESES").val(respuesta.NUM_MESES);
                $("#NUM_DIAS").val(respuesta.NUM_DIAS);
            }
}
/**
 *
 * param FICHA CV
 */
function ProfessionalFichaCV() {
    var id = $('#inputHddIdUsuarioSesion').val();
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + id + "&proceso=RFICHACV", function (responseText, textStatus, request) {
    });
}
function fn_DescargarArchivo(ID) {
    DESARROLLO.DESCARGAR_PERSONAL_DOCUMENTO_LF(ID);
    //jQuery("#myModalDescarga").html('');
    //jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    //});
}
