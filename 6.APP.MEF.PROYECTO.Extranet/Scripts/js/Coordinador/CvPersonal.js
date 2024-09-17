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
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_FORMAC_ACADEMICA", "data": "ID_FORMAC_ACADEMICA", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "APLICA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "NIVEL ACADÉMICO", "data": "DESC_NIVEL_ACADEMICO", "class": "text-center", },
            { "title": "GRADO ACADÉMICO", "data": "NOMBRE_NIVEL", "class": "text-center", },
            { "title": "ESPECIALIDAD", "data": "DESC_ACADEMICA", "class": "text-left", "width": "1%" },
            { "title": "FECHA TÍTULO", "data": "FEC_EMISION_FORMAC_ACADEMICA", "class": "text-center", },
            { "title": "CENTRO DE ESTUDIOS", "data": "DESC_CENTRO_ESTUDIOS", "class": "text-left", },
            { "title": "CIUDAD/PAÍS", "data": "DESC_CIUDAD_PAIS", "class": "text-center", },
            { "title": "APLICA", "data": "APLICA", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-center", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if ($("#H_FLG_NOTIFICA_UTP").val() == "1") {
                        if (row.APLICA == "1") {
                            html = '<button  class="btn btn-xs"   type="button" title="Registro aplica requisito"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"   type="button" title="Registro no aplica"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                        }
                    } else {
                        if (row.APLICA == "1") {
                            html = '<button  class="btn btn-xs"  onClick="MantenimientoAplicaPersonal(' + row.ID_FORMAC_ACADEMICA + ',0,1);" type="button" title="Desea desactivar el registro"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="MantenimientoAplicaPersonal(' + row.ID_FORMAC_ACADEMICA + ',1,1);" type="button" title="Desea aplicar el registro"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                        }
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
                'targets': 7,
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
        ID_PERSONAL: $('#H_ID_PERSONAL').val(),
        ID_SOLICITUD: $('#H_ID_SOLICITUD').val(),
    };
    var url = baseUrl + 'Coordinador/Solicitud/ListaEstudios';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableFormacion_.clear().draw();
    if (respuesta != null && respuesta != "") {

        TableFormacion_.rows.add(respuesta).draw();
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
            { "title": "APLICA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "TIPO", "data": "DESC_TIPO_ESPECIALIZACION", "class": "text-center", },
            { "title": "ESPECIALIDAD", "data": "NOMBRE_ESPECIALIZACION", "class": "text-center", },
            { "title": "F. INICIO", "data": "FEC_INICIO_ESPECIALIZACION", "class": "text-center", "width": "1%" },
            { "title": "F. TERMINO", "data": "FEC_FIN_ESPECIALIZACION", "class": "text-center", },
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

                    
                    if ($("#H_FLG_NOTIFICA_UTP").val() == "1") {
                        if (row.APLICA == "1") {
                            html = '<button  class="btn btn-xs"   type="button" title="Registro aplica requisito"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"   type="button" title="Registro no aplica"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                        }
                    } else {
                        if (row.APLICA == "1") {
                            html = '<button  class="btn btn-xs"  onClick="MantenimientoAplicaPersonal(' + row.ID_ESPECIALIZACION + ',0,2);" type="button" title="Desea desactivar el registro"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="MantenimientoAplicaPersonal(' + row.ID_ESPECIALIZACION + ',1,2);" type="button" title="Desea aplicar el registro"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                        }
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
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_ESPECIALIZACION);;
                    return html;
                }
            },
            {
                'targets': 7,
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
        ID_PERSONAL: $('#H_ID_PERSONAL').val(),
        ID_SOLICITUD: $('#H_ID_SOLICITUD').val(),
    };
    var url = baseUrl + 'Coordinador/Solicitud/ListaEspecializacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableEspecializacion_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableEspecializacion_.rows.add(respuesta).draw();
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
            { "title": "APLICA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
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
            

                       
                    if ($("#H_FLG_NOTIFICA_UTP").val() == "1") {
                        if (row.APLICA == "1") {
                            html = '<button  class="btn btn-xs"   type="button" title="Registro aplica requisito"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"   type="button" title="Registro no aplica"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                        }
                    } else {
                        if (row.APLICA == "1") {
                            html = '<button  class="btn btn-xs"  onClick="MantenimientoAplicaPersonal(' + row.ID_CAPACITACION + ',0,3);" type="button" title="DDesea desactivar el registro"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="MantenimientoAplicaPersonal(' + row.ID_CAPACITACION + ',1,3);" type="button" title="Desea aplicar el registro"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                        }
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
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_CAPACITACION);;
                    return html;
                }
            },
            {
                'targets': 6,
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
        ID_PERSONAL: $('#H_ID_PERSONAL').val(),
        ID_SOLICITUD: $('#H_ID_SOLICITUD').val(),
    };
    var url = baseUrl + 'Coordinador/Solicitud/ListaCapacitacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableCapacitacion_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableCapacitacion_.rows.add(respuesta).draw();
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
        "responsive": false,
        columns: [
            { "title": "ID_EXPERIENCIA", "data": "ID_EXPERIENCIA", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "APLICA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "EXPERIENCIA", "data": "DESC_TIPO_EXPERIENCIA", "class": "text-center", },
            { "title": "ENTIDAD", "data": "NOMBRE_ENTIDAD", "class": "text-left", "width": "300px" },
            { "title": "CARGO", "data": "CARGO_EMPRESA", "class": "text-left", },
            { "title": "F. INICIO", "data": "FEC_INICIO_EXPERIENCIA", "class": "text-center", "width": "1%" },
            { "title": "F. TERMINO", "data": "FEC_FIN_EXPERIENCIA", "class": "text-center", },
            { "title": "Año(s)", "data": "NUM_ANIOS_TOTAL", "class": "text-center", },
            { "title": "Mes(es)", "data": "NUM_MESES_TOTAL", "class": "text-center", },
            { "title": "Día(s)", "data": "NUM_DIAS_TOTAL", "class": "text-center", },
            { "title": "DESCRIPCIÓN DEL TRABAJO REALIZADO", "data": "FUNCIONES", "class": "text-left", "width": "400px"  },
            { "title": "APLICA", "data": "APLICA", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-center", "visible": false, },
            { "title": "ID_TIPO_EXPERIENCIA", "data": "ID_TIPO_EXPERIENCIA", "visible": false, },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';

                    if ($("#H_FLG_NOTIFICA_UTP").val() == "1") {
                        if (row.APLICA == "1") {
                            html = '<button  class="btn btn-xs"   type="button" title="Registro aplica requisito"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"   type="button" title="Registro no aplica"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                        }
                    } else {
                        if (row.APLICA == "1") {
                            html = '<button  class="btn btn-xs"  onClick="MantenimientoAplicaPersonal(' + row.ID_EXPERIENCIA + ',0,4);" type="button" title="Desea desactivar el registro"><i class="fas fa-check-circle" style="color:#70AD47;font-size:25px"></i></button>';

                        } else {
                            html = '<button  class="btn btn-xs"  onClick="MantenimientoAplicaPersonal(' + row.ID_EXPERIENCIA + ',1,4);" type="button" title="Desea aplicar el registro"><i class="far fa-check-circle" style="color:#e30613;font-size:18px"></i></button>';
                        }
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
                'targets': 7,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INICIO_EXPERIENCIA);;
                    return html;
                }
            },
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = "<label style='font-weight:bold' class='form-control tdNUM_ANIOS_TOTAL' >" + row.NUM_ANIOS_TOTAL + "</label>";
                    return html;
                }
            },

            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = "<label style='font-weight:bold' class='form-control tdNUM_MESES_TOTAL' >" + row.NUM_MESES_TOTAL + "</label>";
                    return html;
                }
            },
            {
                'targets': 11,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = "<label style='font-weight:bold' class='form-control tdNUM_DIAS_TOTAL' >" + row.NUM_DIAS_TOTAL + "</label>";
                    html += "<label style='font-weight:bold;display:none' class='form-control tdID_TIPO_EXPERIENCIA'>" + row.ID_TIPO_EXPERIENCIA + "</label>";
                    return html;
                }
            },
            {
                'targets': 8,
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
            $(api.column(6).footer()).html('<p class="text-primary text-right text-bold" style="margin:0px">Tiempo Total Servicio:</span>');
            $(api.column(8).footer()).html('<p class="text-dark text-right text-bold" style="margin:0px">General:</br>Especifica:</span>');
        }

    });
}
function CargarGrillaExperiencia() {
    var item =
    {
        ID_PERSONAL: $('#H_ID_PERSONAL').val(),
        ID_SOLICITUD: $('#H_ID_SOLICITUD').val(),
    };
    var url = baseUrl + 'Coordinador/Solicitud/ListaExperiencia';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableExperiencia_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableExperiencia_.rows.add(respuesta).draw();
    }
}
function CalcularTotalPeriodo() {
    var A = 0;
    var M = 0;
    var D = 0;
    var xdias = 0;
    var xmeses = 0;
    var xanios = 0;
    var xdias_es = 0;
    var xmeses_es = 0;
    var xanios_es = 0;
    $("#frmCalificarFag").find("#TableExperiencia" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var AN = parseInt(entorno.find("label.tdNUM_ANIOS_TOTAL", "id").text());
        var ME = parseInt(entorno.find("label.tdNUM_MESES_TOTAL", "id").text());
        var DI = parseInt(entorno.find("label.tdNUM_DIAS_TOTAL", "id").text());
        var TE = parseInt(entorno.find("label.tdID_TIPO_EXPERIENCIA", "id").text());
        //DIAS
        xdias += DI;
        //MESES
        xmeses +=ME;
        //ANIOS
        xanios +=AN;
        ///////ESPECIFICA 
        if (TE==2) {
            xdias_es += DI;
            //MESES
            xmeses_es += ME;
            //ANIOS
            xanios_es += AN;
        }
    });
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
    anios = xanios + Math.floor((xmeses + Math.floor(xdias / 30)) / 12);

    if (xdias_es >= 30) {
        dias_es = xdias_es % 30;
        if ((xmeses + Math.floor(xdias_es / 30)) > 12) {
            meses_es = (xmeses_es + Math.floor(xdias_es / 30)) % 12;
        }
        else {
            if ((xmeses + Math.floor(xdias_es / 30)) == 12) {
                meses_es = 0;
            }
            else {
                meses_es = xmeses_es + Math.floor(xdias_es / 30);
            }
        }
    }
    else {
        dias_es = xdias_es;
        if ((xmeses_es + Math.floor(xdias_es / 30)) > 12) {
            meses_es = (xmeses_es + Math.floor(xdias_es / 30)) % 12;
        }
        else {
            if ((xmeses + Math.floor(xdias_es / 30)) == 12) {
                meses_es = 0;
                xmeses_es = 0;
                xanios_es += 1;
            }
            else {
                meses_es = xmeses_es;
            }
        }
    }
    anios_es = xanios_es + Math.floor((xmeses_es + Math.floor(xdias_es / 30)) / 12);
    $(".AN").html(anios + "</br>"+anios_es);
    $(".ME").html(meses + "</br>" + meses_es);
    $(".DI").html(dias + "</br>" + dias_es);
}
/**
 *
 * param APLICA REGISTROS
 */
function MantenimientoAplicaPersonal(ID, FLG_ESTADO, PROCESO) {
    var e = new FormData;
    switch (PROCESO) {
        case 1:
            PROCESO = "ACAD";
            break;
        case 2:
            PROCESO = "ESP";
            break;
        case 3:
            PROCESO = "CAPA";
            break;
        case 4:
            PROCESO = "EXPE";
            break;
    }

    e.append("ID_SOLICITUD", $("#H_ID_SOLICITUD").val());
    e.append("ID_PERSONAL", $("#H_ID_PERSONAL").val());
    e.append("ID", ID);
    e.append("FLG_ESTADO", FLG_ESTADO);
    e.append("ACCION", PROCESO);
    $.ajax({
        url: baseUrl + "Coordinador/ProcesoPac/MantenimientoAplicaPersonal",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                switch (PROCESO) {
                    case "ACAD":
                        CargarGrillaFormacion();
                        break;
                    case "ESP":
                        CargarGrillaEspecializacion();
                        break;
                    case "CAPA":
                        CargarGrillaCapacitacion();
                        break;
                    case "EXPE":
                        CargarGrillaExperiencia();
                        break;
}
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}