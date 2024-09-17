var TableFormacion_ = null;
var TableEspecializacion_ = null;
var TableCapacitacion_ = null;
var TableExperiencia_ = null;
/**
 *
 * param FORMACIÓN ACADEMICA
 */
function CrearGrillaFormacion() {
    DESARROLLO.configurarGrilla();
    TableFormacion_ = $("#TableFormacion").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": true,
        "bInfo": false,
        columns: [
            { "title": "ID_FORMAC_ACADEMICA", "data": "ID_FORMAC_ACADEMICA", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "VALIDADO <BR> ENTIDAD", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "SOLICITAR<BR>INFORMACIÓN", "data": "", "class": "text-center", "width": "1%" },
            { "title": "INFORMACIÓN<BR>VERIFICADA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "NIVEL ACADÉMICO", "data": "DESC_NIVEL_ACADEMICO", "class": "text-center", },
            { "title": "GRADO ACADÉMICO", "data": "NOMBRE_NIVEL", "class": "text-center", },
            { "title": "ESPECIALIDAD", "data": "DESC_ACADEMICA", "class": "text-left", "width": "1%" },
            { "title": "FECHA TÍTULO", "data": "FEC_EMISION_FORMAC_ACADEMICA", "class": "text-center", },
            { "title": "CENTRO DE ESTUDIOS", "data": "DESC_CENTRO_ESTUDIOS", "class": "text-left","width": "200px" },
            { "title": "CIUDAD/PAÍS", "data": "DESC_CIUDAD_PAIS", "class": "text-center", },
            { "title": "APLICA", "data": "APLICA", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "VERIFICAR_ENTIDAD", "data": "VERIFICAR_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-center", "visible": false, },
            { "title": "VERIFICACION_CORRECTA", "data": "VERIFICACION_CORRECTA", "class": "text-center", "visible": false, },

            
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.APLICA == "1") {
                        html = '<button  class="btn btn-xs"   type="button" title="Registro aplica requisito por el coordinador"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                    } else {
                        html = '<button  class="btn btn-xs"   type="button" title="Registro no aplica"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                    }
                  
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';

                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                  
                    if (row.VERIFICAR_ENTIDAD == "1") {
                        html = '<button  class="btn btn-xs"   type="button" title="Se solicitó información de la formación académica a la entidad correspondiente"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';
                    } else {
                        html = '<button  class="btn btn-xs"  onClick="fn_ValidarFormacion(' + row.ID_FORMAC_ACADEMICA + ',1);" type="button" title="Solicitar información de la formación académica a la entidad correspondiente"><i class="far fa-check-circle" style="color:#183153;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '';
                    
                    if (row.VERIFICAR_ENTIDAD == "1") {
                        if (row.VERIFICACION_CORRECTA=="1") {
                            html = '<button  class="btn btn-xs"   type="button" title="Se realizo la verificación correctamente"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="fn_ValidarFormacion(' + row.ID_FORMAC_ACADEMICA + ',2);" type="button" title="Solicitar información de la formación académica a la entidad correspondiente"><i class="far fa-check-circle" style="color:#183153;font-size:18px"></i></button>';

                        }
                    } 
                    return html;
                }
            },
            {
                'targets': 9,
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
        ID_PERSONAL: $('#ID_PERSONAL').val(),
        ID_SOLICITUD: $('#ID_SOLICITUD').val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaEstudios';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableFormacion_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableFormacion_.rows.add(respuesta).draw();
    }
}
function fn_ValidarFormacion(ID_FORMAC_ACADEMICA, TIPO) {
    var tipo_ = "C";
    if (TIPO == 1) {
        tipo_ = "S";
    }
    var item =
    {
        ID_FORMAC_ACADEMICA: ID_FORMAC_ACADEMICA,
        APLICA: tipo_,
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/MentenimientoEstudios_Verificar';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaFormacion();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }

    }
}
/**
 *
 * param ESPECIALIZACIÓN
 */
function CrearGrillaEspecializacion() {
    DESARROLLO.configurarGrilla();
    TableEspecializacion_ = $("#TableEspecializacion").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_ESPECIALIZACION", "data": "ID_ESPECIALIZACION", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "VALIDADO <BR> ENTIDAD", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "SOLICITAR<BR>INFORMACIÓN", "data": "", "class": "text-center", "width": "1%" },
            { "title": "INFORMACIÓN<BR>VERIFICADA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "TIPO", "data": "DESC_TIPO_ESPECIALIZACION", "class": "text-center", },
            { "title": "ESPECIALIDAD", "data": "NOMBRE_ESPECIALIZACION", "class": "text-left", },
            { "title": "F. INICIO", "data": "FEC_INICIO_ESPECIALIZACION", "class": "text-center", "width": "1%" },
            { "title": "F. TERMINO", "data": "FEC_FIN_ESPECIALIZACION", "class": "text-center", },
            { "title": "CENTRO DE ESTUDIOS", "data": "DESC_CENTRO_ESTUDIOS", "class": "text-left", },
            { "title": "CIUDAD/PAÍS", "data": "NOMBRE_CIUDAD_PAIS", "class": "text-center", },
            { "title": "APLICA", "data": "APLICA", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "VERIFICAR_ENTIDAD", "data": "VERIFICAR_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-center", "visible": false, },
            { "title": "VERIFICACION_CORRECTA", "data": "VERIFICACION_CORRECTA", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.APLICA == "1") {
                        html = '<button  class="btn btn-xs"   type="button" title="Registro aplica requisito por el coordinador"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                    } else {
                        html = '<button  class="btn btn-xs"   type="button" title="Registro no aplica"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                    }
                   
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';

                    if (row.VERIFICAR_ENTIDAD == "1") {
                        html = '<button  class="btn btn-xs"   type="button" title="Se solicitó información de cursos y/o estudios de especialización"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';
                    } else {
                        html = '<button  class="btn btn-xs"  onClick="fn_ValidarEspecializacion(' + row.ID_ESPECIALIZACION + ',1);" type="button" title="Se solicitó información de cursos y/o estudios de especialización"><i class="far fa-check-circle" style="color:#183153;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '';

                    if (row.VERIFICAR_ENTIDAD == "1") {
                        if (row.VERIFICACION_CORRECTA == "1") {
                            html = '<button  class="btn btn-xs"   type="button" title="Se realizo la verificación correctamente"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="fn_ValidarEspecializacion(' + row.ID_ESPECIALIZACION + ',2);" type="button" title="Se solicitó información de cursos y/o estudios de especialización"><i class="far fa-check-circle" style="color:#183153;font-size:18px"></i></button>';

                        }
                    }
                    return html;
                }
            },
            {
                'targets': 8,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_ESPECIALIZACION);;
                    return html;
                }
            },
            {
                'targets': 9,
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
        ID_PERSONAL: $('#ID_PERSONAL').val(),
        ID_SOLICITUD: $('#ID_SOLICITUD').val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaEspecializacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableEspecializacion_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableEspecializacion_.rows.add(respuesta).draw();
    }
}
function fn_ValidarEspecializacion(ID_FORMAC_ACADEMICA, TIPO) {
    var tipo_ = "C";
    if (TIPO == 1) {
        tipo_ = "S";
    }
    var item =
    {
        ID_ESPECIALIZACION: ID_FORMAC_ACADEMICA,
        APLICA: tipo_,
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/MentenimientoEspecializacion_Verificar';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaEspecializacion();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }

    }
}

/**
 *
 * param CAPACITACION
 */
function CrearGrillaCapacitacion() {
    DESARROLLO.configurarGrilla();
    TableCapacitacion_ = $("#TableCapacitacion").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_CAPACITACION", "data": "ID_CAPACITACION", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "VALIDADO <BR> ENTIDAD", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "SOLICITAR<BR>INFORMACIÓN", "data": "", "class": "text-center", "width": "1%" },
            { "title": "INFORMACIÓN<BR>VERIFICADA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ESPECIALIDAD", "data": "NOMBRE_CAPACITACION", "class": "text-center", },
            { "title": "F. INICIO", "data": "FEC_INICIO_CAPACITACION", "class": "text-center", "width": "1%" },
            { "title": "F. TERMINO", "data": "FEC_FIN_CAPACITACION", "class": "text-center", },
            { "title": "CENTRO DE ESTUDIOS", "data": "DESC_CENTRO_ESTUDIOS", "class": "text-left", },
            { "title": "CIUDAD/PAÍS", "data": "NOMBRE_CIUDAD_PAIS", "class": "text-center", },
            { "title": "APLICA", "data": "APLICA", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [

            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
            
                    if (row.APLICA == "1") {
                        html = '<button  class="btn btn-xs"   type="button" title="Registro aplica requisito por el coordinador"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                    } else {
                        html = '<button  class="btn btn-xs"   type="button" title="Registro no aplica"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';

                    if (row.VERIFICAR_ENTIDAD == "1") {
                        html = '<button  class="btn btn-xs"   type="button" title="Se solicitó información de capacitación"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';
                    } else {
                        html = '<button  class="btn btn-xs"  onClick="fn_ValidarCapacitacion(' + row.ID_CAPACITACION + ',1);" type="button" title="Solicitar información de capacitación"><i class="far fa-check-circle" style="color:#183153;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '';

                    if (row.VERIFICAR_ENTIDAD == "1") {
                        if (row.VERIFICACION_CORRECTA == "1") {
                            html = '<button  class="btn btn-xs"   type="button" title="Se realizo la verificación correctamente"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="fn_ValidarCapacitacion(' + row.ID_CAPACITACION + ',2);" type="button" title="Solicitar información de capacitación"><i class="far fa-check-circle" style="color:#183153;font-size:18px"></i></button>';

                        }
                    }
                    return html;
                }
            },
            {
                'targets': 7,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_CAPACITACION);;
                    return html;
                }
            },
            {
                'targets': 8,
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
        ID_PERSONAL: $('#ID_PERSONAL').val(),
        ID_SOLICITUD: $('#ID_SOLICITUD').val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaCapacitacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableCapacitacion_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableCapacitacion_.rows.add(respuesta).draw();
    }
}
function fn_ValidarCapacitacion(ID_FORMAC_ACADEMICA, TIPO) {
    var tipo_ = "C";
    if (TIPO == 1) {
        tipo_ = "S";
    }
    var item =
    {
        ID_CAPACITACION: ID_FORMAC_ACADEMICA,
        APLICA: tipo_,
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/MentenimientoCapacitacion_Verificar';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaCapacitacion();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }

    }
}
/**
 *
 * param EXPERIENCIA
 */
function CrearGrillaExperiencia() {
    DESARROLLO.configurarGrilla();
    TableExperiencia_ = $("#TableExperiencia").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_EXPERIENCIA", "data": "ID_EXPERIENCIA", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "VALIDADO <BR> ENTIDAD", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "SOLICITAR<BR>INFORMACIÓN", "data": "", "class": "text-center", "width": "1%" },
            { "title": "INFORMACIÓN<BR>VERIFICADA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "EXPERIENCIA", "data": "DESC_TIPO_EXPERIENCIA", "class": "text-center", },
            { "title": "ENTIDAD", "data": "NOMBRE_ENTIDAD", "class": "text-left", },
            { "title": "CARGO", "data": "CARGO_EMPRESA", "class": "text-left", },
            { "title": "F. INICIO", "data": "FEC_INICIO_EXPERIENCIA", "class": "text-center", "width": "1%" },
            { "title": "F. TERMINO", "data": "FEC_FIN_EXPERIENCIA", "class": "text-center", },
            { "title": "Año(s)", "data": "NUM_ANIOS", "class": "text-center", },
            { "title": "Mes(es)", "data": "NUM_MESES", "class": "text-center", },
            { "title": "Día(s)", "data": "NUM_DIAS", "class": "text-center", },
            { "title": "DESCRIPCIÓN DEL TRABAJO REALIZADO", "data": "FUNCIONES", "class": "text-left", },
            { "title": "APLICA", "data": "APLICA", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "VERIFICAR_ENTIDAD", "data": "VERIFICAR_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-center", "visible": false, },
            { "title": "VERIFICACION_CORRECTA", "data": "VERIFICACION_CORRECTA", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.APLICA == "1") {
                        html = '<button  class="btn btn-xs"   type="button" title="Registro aplica requisito por el coordinador"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                    } else {
                        html = '<button  class="btn btn-xs"   type="button" title="Registro no aplica"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO +  ');" type="button" title="Descargar Archivo"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';

                    if (row.VERIFICAR_ENTIDAD == "1") {
                        html = '<button  class="btn btn-xs"   type="button" title="Se solicitó información de la experiencia a la entidad correspondiente"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';
                    } else {
                        html = '<button  class="btn btn-xs"  onClick="fn_ValidarExperiencia(' + row.ID_EXPERIENCIA + ',1);" type="button" title="Solicitar información de la experiencia a la entidad correspondiente"><i class="far fa-check-circle" style="color:#183153;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '';

                    if (row.VERIFICAR_ENTIDAD == "1") {
                        if (row.VERIFICACION_CORRECTA == "1") {
                            html = '<button  class="btn btn-xs"   type="button" title="Se realizo la verificación correctamente"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="fn_ValidarExperiencia(' + row.ID_EXPERIENCIA + ',2);" type="button" title="Solicitar información de la experiencia a la entidad correspondiente"><i class="far fa-check-circle" style="color:#183153;font-size:18px"></i></button>';

                        }
                    }
                    return html;
                }
            },
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_EXPERIENCIA);;
                    return html;
                }
            },
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_FIN_EXPERIENCIA);;
                    return html;
                }
            },
        ],
        //"footerCallback": function (row, data, start, end, display) {
        //    var api = this.api(), data;

        //    // Remove the formatting to get integer data for summation
        //    var intVal = function (i) {
        //        return typeof i === 'string' ?
        //            i.replace(/[\$,]/g, '') * 1 :
        //            typeof i === 'number' ?
        //                i : 0;
        //    };

        //    // Total over all pages
        //    total = api
        //        .column(8)
        //        .data()
        //        .reduce(function (a, b) {
        //            return intVal(a) + intVal(b);
        //        }, 0);
        //    // Total over this page
        //    pageTotal = api
        //        .column(8, { page: 'current' })
        //        .data()
        //        .reduce(function (a, b) {
        //            return intVal(a) + intVal(b);
        //        }, 0);
        //    // Update footer
        //    $(api.column(8).footer()).html(
        //        '$' + pageTotal + ' ( $' + total + ' total)'
        //    );
        //}

    });
}
function CargarGrillaExperiencia() {
    var item =
    {
        ID_PERSONAL: $('#ID_PERSONAL').val(),
        ID_SOLICITUD: $('#ID_SOLICITUD').val(),
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/ListaExperiencia';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableExperiencia_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableExperiencia_.rows.add(respuesta).draw();
    }
}
function fn_ValidarExperiencia(ID_FORMAC_ACADEMICA, TIPO) {
    var tipo_ = "C";
    if (TIPO == 1) {
        tipo_ = "S";
    }
    var item =
    {
        ID_EXPERIENCIA: ID_FORMAC_ACADEMICA,
        APLICA: tipo_,
    };
    var url = baseUrl + 'Solicitudes/Solicitudes/MentenimientoExperiencia_Verificar';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaExperiencia();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }

    }
}

function fn_DescargarArchivo(ID) {
    DESARROLLO.DESCARGAR_DOCUMENTO_LF(ID);
    //jQuery("#myModalDescarga").html('');
    //jQuery("#myModalDescarga").load(baseUrl + "Administracion/Entidad/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    //});

}