/**  PROCESOS GLOBALES **/
var TableEntidades = null;
var TablePeriodoEntidades = null;
var TableProcesoFag = null;
var TableProcesoPac = null;
var TablePuestosPac = null;
var TableEntidadesOrgano = null;
var tabla_periodo_Organo_F = null;
/**  FIN PROCESOS GLOBALES **/
function CrearGrillaEntidades() {
    DESARROLLO.configurarGrilla(true);
    TableEntidades = $("#tabla_Entidades").DataTable({
        "order": [[4, 'asc']],
        //ordering: false,
        "paging": true,
        "autoWidth": true,
        columns: [
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "Acciones", "data": "", "class": "text-center", "width": "10%" },
            { "title": "FAG", "data": "", "class": "text-center", "width": "10%" },
            { "title": "PAC", "data": "", "class": "text-center", "width": "12%" },
            { "title": "Entidad", "data": "DESC_ENTIDAD", "class": "text-left", },
            { "title": "DESC_UNIDAD", "data": "DESC_UNIDAD", "class": "text-left", "visible": false, },
            { "title": "FLG_ESTADO", "data": "FLG_ESTADO", "class": "text-left", "visible": false, },
            { "title": "FLG_FAG", "data": "FLG_FAG", "class": "text-left", "visible": false, },
            { "title": "FLG_PAC", "data": "FLG_PAC", "class": "text-left", "visible": false, },
            { "title": "CANT_DEPENDENCIA", "data": "CANT_DEPENDENCIA", "class": "text-left", "visible": false, },
            
        ],
        "columnDefs": [
            {
                'targets': 1,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO=="1") {
                        html = '<button  class="btn btn-xs"  onClick="fn_NuevoEditar(' + row.ID_ENTIDAD + ');" type="button" title="Actualizar Entidad"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_EliminarEntidad(' + row.ID_ENTIDAD + ');" type="button" title="Eliminar Entidad"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                    }
                    else {
                        html = '<button  class="btn btn-xs"  onClick="fn_HabilitarEntidad(' + row.ID_ENTIDAD + ');" type="button" title="Activar Entidad"><i class="fas fa-undo-alt" style="color:#0097d9;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO == "1") {
                        if (row.FLG_FAG == "1") {
                            html = '<button  class="btn btn-xs"  onClick="fn_ProcesoFag(' + row.ID_ENTIDAD + ',0);" type="button" title="Desactivar Fag"><i class="fas fa-toggle-on" style="color:#0097d9;font-size:18px"></i></button>';

                            if (row.CANT_DEPENDENCIA == "0") {
                                html += '<button  class="btn btn-xs"  onClick="fn_ProcesoModalFag(' + row.ID_ENTIDAD + ',1);" type="button" title="Periodo Fag"><i class="fas fa-plus-circle" style="color:#6ABEB0;font-size:18px"></i></button>';

                            } else {
                                html += '<button  class="btn btn-xs"  onClick="fn_ProcesoModalFagDependencia(' + row.ID_ENTIDAD + ',1);" type="button" title="Periodo Fag"><i class="fas fa-plus-circle" style="color:#6ABEB0;font-size:18px"></i></button>';
                            }

                        }
                        else {
                            html = '<button  class="btn btn-xs"  onClick="fn_ProcesoFag(' + row.ID_ENTIDAD + ',1);" type="button" title="Activar Fag"><i class="fas fa-toggle-off" style="color:#6c757d;font-size:18px"></i></button>';

                        }
                    }
              
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO == "1") {
                        if (row.FLG_PAC == "1") {
                            html = '<button  class="btn btn-xs"  onClick="fn_ProcesoPac(' + row.ID_ENTIDAD + ',0);" type="button" title="Desactivar Pac"><i class="fas fa-toggle-on" style="color:#0097d9;font-size:18px"></i></button>';
                            if (row.CANT_DEPENDENCIA == "0") {
                                html += '<button  class="btn btn-xs"  onClick="fn_ProcesoModalFag(' + row.ID_ENTIDAD + ',2);" type="button" title="Periodo Pac"><i class="fas fa-plus-circle" style="color:#6ABEB0;font-size:18px"></i></button>';

                            } else {
                                html += '<button  class="btn btn-xs"  onClick="fn_ProcesoModalFagDependencia(' + row.ID_ENTIDAD + ',2);" type="button" title="Periodo Fag"><i class="fas fa-plus-circle" style="color:#6ABEB0;font-size:18px"></i></button>';
                            }
                            html += '<button  class="btn btn-xs"  onClick="fn_ProcesoModalPuestosPac(' + row.ID_ENTIDAD + ');" type="button" title="Puestos Entidad"><i class="fas fa-user-tie" style="font-size:18px"></i></button>';


                        }
                        else {
                            html = '<button  class="btn btn-xs"  onClick="fn_ProcesoPac(' + row.ID_ENTIDAD + ',1);" type="button" title="Activar Pac"><i class="fas fa-toggle-off" style="color:#6c757d;font-size:18px"></i></button>';

                        }
                    }
               
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = row.DESC_ENTIDAD;
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaEntidades() {
    var item =
    {
        FLG_ESTADO: $("#vId_Estado").val(),
        DESCRIPCION: $("#txtEntidad").val(),
    };
    var url = baseUrl + 'Administracion/Entidad/ListaEntidades';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    //TableEntidades.rows().remove();
    TableEntidades.rows().remove().draw();
    if (respuesta != null && respuesta != "") {
        TableEntidades.rows.add(respuesta).draw();
    }
}
function fn_NuevoEditar(id) {
    var ID_ENTIDAD = id;
    jQuery("#modalNuevoEdita").html('');
    jQuery("#modalNuevoEdita").load(baseUrl + "Administracion/Entidad/NuevoEditaEntidad?ID_ENTIDAD=" + ID_ENTIDAD, function (responseText, textStatus, request) {
        $("#modalNuevoEdita").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_MantenimientoEntidad(Accion) {
                item = {
                    ID_ENTIDAD: $("#ID_ENTIDAD").val(),
                    DESC_ENTIDAD: $("#DESC_ENTIDAD").val(),
                    DESC_UNIDAD: $("#DESC_UNIDAD").val(),
                    ACRONIMO: $("#ACRONIMO").val(),
                    ACCION: Accion,
                };
                var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
                var respuesta = DESARROLLO.Ajax(url, item, false);
                if (respuesta != null && respuesta != "") {
                    if (respuesta.success) {
                        CargarGrillaEntidades();
                        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                    } else {
                        DESARROLLO.PROCESO_ERROR(respuesta.extra);
                    }
                }
}
function fn_EliminarEntidad(id) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Eliminar</b> la entidad.?", function (r) {
        if (r) {
            item = {
                ID_ENTIDAD: id,
                DESC_ENTIDAD: "",
                ACCION: "D",
            };
            var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaEntidades();
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
            }
        }
    });
 
}
function fn_HabilitarEntidad(id) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Activar</b> la entidad.?", function (r) {
        if (r) {
            item = {
                ID_ENTIDAD: id,
                DESC_ENTIDAD: "",
                ACCION: "H",
            };
            var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaEntidades();
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
            }
        }
    });
 
}
function fn_ValidarDuplicado() {
    var item =
    {
        DESC_ENTIDAD: $("#DESC_ENTIDAD").val(),
        DESC_UNIDAD: $("#DESC_UNIDAD").val(),
    };
    var urls = baseUrl + 'Administracion/Entidad/ValidarDuplicidadEntidad';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}
function fn_CerrarEntidad() {
    jQuery("#modalNuevoEdita").html('');
    $('#modalNuevoEdita').modal('hide');
}
function fn_ProcesoFag(id, accion) {
    var flg = "";
    
    accion == "1" ? flg = "AF" : flg = "DF";
    item = {
        ID_ENTIDAD: id,
        DESC_ENTIDAD: "",
        ACCION: flg,
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEntidades();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_ProcesoPac(id, accion) {
    var pac = "";
    accion == "1" ? pac = "AP" : pac = "DP";
    item = {
        ID_ENTIDAD: id,
        DESC_ENTIDAD: "",
        ACCION: pac,
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEntidades();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_ProcesoModalFag(id,proceso) {
    var ID_ENTIDAD = id;
    if (proceso == '1') {
        jQuery("#modalProcesoFag").html('');
        jQuery("#modalProcesoFag").load(baseUrl + "Administracion/Entidad/ProcesoFag?ID_ENTIDAD=" + ID_ENTIDAD  +"&FLG_PROCESO=F", function (responseText, textStatus, request) {
            $("#modalProcesoFag").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
    } else {
         jQuery("#modalProcesoPac").html('');
        jQuery("#modalProcesoPac").load(baseUrl + "Administracion/Entidad/ProcesoFag?ID_ENTIDAD=" + ID_ENTIDAD + "&FLG_PROCESO=P", function (responseText, textStatus, request) {
            $("#modalProcesoPac").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
    }
 
}
function fn_ProcesoModalFagDependencia(id,proceso) {
    var ID_ENTIDAD = id;
    if (proceso == '1') {
        jQuery("#modalProcesoFag").html('');
        jQuery("#modalProcesoFag").load(baseUrl + "Administracion/Entidad/GenerarPeriodoEntidadMaestra?ID_ENTIDAD=" + ID_ENTIDAD + "&FLG_PROCESO=F",  function (responseText, textStatus, request) {
            $("#modalProcesoFag").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
    } else {
        jQuery("#modalProcesoPac").html('');
        jQuery("#modalProcesoPac").load(baseUrl + "Administracion/Entidad/GenerarPeriodoEntidadMaestra?ID_ENTIDAD=" + ID_ENTIDAD + "&FLG_PROCESO=P", function (responseText, textStatus, request) {
            $("#modalProcesoPac").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
    }

}
//function fn_ProcesoModalPac(id) {
//    var ID_ENTIDAD = id;
//    jQuery("#modalProcesoPac").html('');
//    jQuery("#modalProcesoPac").load(baseUrl + "Administracion/Entidad/ProcesoPac?ID_ENTIDAD=" + ID_ENTIDAD, function (responseText, textStatus, request) {
//        $("#modalProcesoPac").modal('show');
//    });
//}
function fn_PeriodoFag() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_PERIODO", $("#G_ID_PERIODO").val());
    e.append("ID_ENTIDAD", $("#G_ID_ENTIDAD").val());
    e.append("TIPO_PROCESO", $('#FLG_PROCESO').val());
    e.append("ANIO_PERIODO", $("#txtFacanio").val());
    e.append("FEC_PERIODO_INI", $("#FechaFinicio").val());
    e.append("FEC_PERIODO_FIN", $("#FechaFfin").val());
    e.append("MONTO_MENSUAL", $("#txtFmonto").val());
    
    $.ajax({
        url: baseUrl + "Administracion/Entidad/MantenimientoPeriodoEntidad",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                CargarGrillaPeriodoEntidadesFAG($("#txtFanio").val());
                fn_CerrarGenerarPeriodo();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function fn_UpdatePeriodoFag() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_PERIODO", $("#G_ID_PERIODO").val());
    e.append("ID_ENTIDAD", $("#G_ID_ENTIDAD").val());
    e.append("TIPO_PROCESO", $('#FLG_PROCESO').val());
    e.append("ANIO_PERIODO", $("#txtFacanio").val());
    e.append("FEC_PERIODO_INI", $("#FechaFinicio").val());
    e.append("FEC_PERIODO_FIN", $("#FechaFfin").val());
    e.append("MONTO_MENSUAL", $("#txtFmonto").val());
    
    $.ajax({
        url: baseUrl + "Administracion/Entidad/UpdatePeriodoEntidad",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                CargarGrillaPeriodoEntidadesFAG($("#txtFanio").val());
                fn_CerrarGenerarPeriodo();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
//function fn_PeriodoPac() {
//    var doc = document.getElementById("fileArchivo_P").files.length;
//    var e = new FormData;
//    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo_P").files[l], e.append("fileArchivo_P", a);
//    e.append("ID_PERIODO", $("#P_ID_PERIODO").text());
//    e.append("ID_ENTIDAD", $("#P_ID_ENTIDAD").val());
//    e.append("TIPO_PROCESO", "P");
//    e.append("ANIO_PERIODO", $("#txtanio").val());
//    e.append("FEC_PERIODO_INI", $("#FechaPinicio").val());
//    e.append("FEC_PERIODO_FIN", $("#FechaPfin").val());
//    e.append("MONTO_MENSUAL", $("#txtmonto").val());
//    
//    $.ajax({
//        url: baseUrl + "Administracion/Entidad/MantenimientoPeriodoEntidad",
//        type: "POST",
//        data: e,
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        contentType: !1,
//        processData: !1,
//        success: function (p) {
//            if (p.success) {
//                DESARROLLO.PROCESO_CORRECTO(p.message);
//            }
//            else {
//                DESARROLLO.PROCESO_ERROR(p.extra);
//            }
//        }
//    });
//    //item = {
//    //    ID_PERIODO: $("#P_ID_PERIODO").text(),
//    //    ID_ENTIDAD: $("#P_ID_ENTIDAD").val(),
//    //    TIPO_PROCESO: "P",
//    //    ANIO_PERIODO: $("#txtanio").val(),
//    //    FEC_PERIODO_INI: $("#FechaPinicio").val(),
//    //    FEC_PERIODO_FIN: $("#FechaPfin").val(),
//    //    MONTO_MENSUAL: $("#txtmonto").val(),
//    //};
//    //var url = baseUrl + 'Administracion/Entidad/MantenimientoPeriodoEntidad';
//    //var respuesta = DESARROLLO.Ajax(url, item, false);
//    //if (respuesta != null && respuesta != "") {
//    //    if (respuesta.success) {
//    //        CargarGrillaEntidades();
//    //        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
//    //    } else {
//    //        DESARROLLO.PROCESO_ERROR(respuesta.extra);
//    //    }
//    //}
//}
function CrearGrillaPeriodoDetalleFag() {
    DESARROLLO.configurarGrilla();
    TableProcesoFag = $("#tabla_proceso_F").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_PERIODO", "data": "ID_PERIODO", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "Detalle", "data": "", "class": "text-center", "width": "2%" },
            { "title": "NUM_MES", "data": "NUM_MES", "class": "text-center", "width": "10%", "visible": false,  },
            { "title": "Mes", "data": "DES_MES", "class": "text-center", "width": "5%" },
            { "title": "Monto", "data": "MONTO_MENSUAL", "class": "text-center", "width": "5%" },
            { "title": "FLG_ESTADO", "data": "FLG_ESTADO", "class": "text-left", "visible": false, },
            { "title": "Sustento", "data": "Sustento", "class": "text-center", "width": "2%" },
            { "title": "NOMBRE_ARCHIVO", "data": "NOMBRE_ARCHIVO", "class": "text-left", "visible": false, },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-left", "visible": false, },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO == "1") {
                        html = '<button  class="btn btn-xs"  onClick="fn_NuevoEditarMontoF(' + row.ID_PERIODO + ',' + row.ID_ENTIDAD + ',' + row.NUM_MES +');" type="button" title="Actualizar Monto"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        //html += '<button  class="btn btn-xs"  onClick="-------(' + row.ID_PERIODO + ');" type="button" title="Detalle de mes"><i class="fas fa-layer-group" style="color:rgb(20, 110, 190);font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                "targets": [5],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                'targets': 7,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.ARCHIVO >0) {
                        html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-file-pdf" style="color:red;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaPeriodoDetalleFag(id) {
    
    var item =
    {
        ID_PERIODO : id
    };
    var url = baseUrl + 'Administracion/Entidad/ListaPeriodoDetalleEntidades';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableProcesoFag.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableProcesoFag.rows.add(respuesta).draw();
    }
}
//function CrearGrillaPeriodoDetallePac() {
//    DESARROLLO.configurarGrilla();
//    TableProcesoPac = $("#tabla_proceso_P").DataTable({
//        ordering: false,
//        "paging": false,
//        "autoWidth": false,
//        "bInfo": false,
//        columns: [
//            { "title": "ID_PERIODO", "data": "ID_PERIODO", "class": "text-center", "visible": false, },
//            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
//            { "title": "Detalle", "data": "", "class": "text-center", "width": "2%" },
//            { "title": "NUM_MES", "data": "NUM_MES", "class": "text-center", "width": "10%", "visible": false, },
//            { "title": "Mes", "data": "DES_MES", "class": "text-center", "width": "5%" },
//            { "title": "Monto", "data": "MONTO_MENSUAL", "class": "text-center", "width": "5%" },
//            { "title": "FLG_ESTADO", "data": "FLG_ESTADO", "class": "text-left", "visible": false, },
//            { "title": "Sustento", "data": "Sustento", "class": "text-center", "width": "2%" },
//            { "title": "NOMBRE_ARCHIVO", "data": "NOMBRE_ARCHIVO", "class": "text-left", "visible": false, },
//        ],
//        "columnDefs": [
//            {
//                'targets': 2,
//                'render': function (data, type, row, meta) {
//                    var html = '';
//                    if (row.FLG_ESTADO == "1") {
//                        html = '<button  class="btn btn-xs"  onClick="fn_NuevoEditarMontoP(' + row.ID_PERIODO + ',' + row.ID_ENTIDAD + ',' + row.NUM_MES +');" type="button" title="Actualizar Monto"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
//                        html += '<button  class="btn btn-xs"  onClick="-(' + row.ID_PERIODO + ');" type="button" title="Detalle de mes"><i class="fas fa-layer-group" style="color:rgb(20, 110, 190);font-size:18px"></i></button>';
//                    }
//                    return html;
//                }
//            },
//            {
//                'targets': 7,
//                'render': function (data, type, row, meta) {
//                    var html = '';
//                    if (row.NOMBRE_ARCHIVO != null) {
//                        html = '<button  class="btn btn-xs"  onClick="DescargarArchivoSustentoPer(' + row.ID_PERIODO + ',' + row.NUM_MES + ');" type="button" title="Actualizar Monto"><i class="fas fa-file-pdf" style="color:red;font-size:18px"></i></button>';
//                    }
//                    return html;
//                }
//            },
//        ],

//    });
//}
//function CargarGrillaPeriodoDetallePac(id) {
//    var item =
//    {
//        ID_PERIODO: id
//    };
//    var url = baseUrl + 'Administracion/Entidad/ListaPeriodoDetalleEntidades';
//    var respuesta = DESARROLLO.Ajax(url, item, false);
//    TableProcesoPac.clear().draw();
//    if (respuesta != null && respuesta != "") {
//        TableProcesoPac.rows.add(respuesta).draw();
//    }
//}
function fn_ProcesoModalPuestosPac(id) {
    var ID_ENTIDAD = id;
    jQuery("#modalProcesoPuestoPac").html('');
    jQuery("#modalProcesoPuestoPac").load(baseUrl + "Administracion/Entidad/PuestosPac?ID_ENTIDAD=" + ID_ENTIDAD, function (responseText, textStatus, request) {
        $("#modalProcesoPuestoPac").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_NuevoPuestoEditar(ID_PUESTO,ID_ENTIDAD) {

    jQuery("#modalNuevoEditaPuesto").html('');
    jQuery("#modalNuevoEditaPuesto").load(baseUrl + "Administracion/Entidad/NuevoEditaPuesto?ID_PUESTO=" + ID_PUESTO + "&ID_ENTIDAD=" +ID_ENTIDAD, function (responseText, textStatus, request) {
        $("#modalNuevoEditaPuesto").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarEditarPuesto() {
    jQuery("#modalNuevoEditaPuesto").html('');
    $('#modalNuevoEditaPuesto').modal('hide');
}
function fn_MantenimientoPuesto(Accion) {
    item = {
        ID_PUESTO: $("#H_ID_PUESTO").val(),
        ID_ENTIDAD: $("#H_ID_ENTIDAD").val(),
        DES_PUESTO: $("#DES_PUESTO").val(),
        ACCION: Accion,
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoPuesto';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaPuestoPac($("#PUESTO_ID_ENTIDAD").val());
            fn_CerrarEditarPuesto();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function CrearGrillaPuestoPac() {
    DESARROLLO.configurarGrilla();
    TablePuestosPac = $("#tabla_puestos_pac").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_PUESTO", "data": "ID_PUESTO", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "PUESTO", "data": "DES_PUESTO", "class": "text-left",  },
            { "title": "FLG_ESTADO", "data": "FLG_ESTADO", "class": "text-left", "visible": false, },
            { "title": "ARCHIVO_PUESTO", "data": "ARCHIVO_PUESTO", "class": "text-left", "visible": false, },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO_PUESTO + ');" type="button" title="Descargar Archivo"><i class="fas fa-file-download" style="color:#6ABEB0;font-size:18px"></i></button>';

                    //if (row.FLG_ESTADO == "1") {
                    //    html = '<button  class="btn btn-xs"  onClick="fn_NuevoPuestoEditar(' + row.ID_PUESTO + ',' + row.ID_ENTIDAD   + ');" type="button" title="Editar Puesto"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                    //    html += '<button  class="btn btn-xs"  onClick="fn_EliminarPuesto(' + row.ID_PUESTO + ',' + row.ID_ENTIDAD + ',0' + ');" type="button" title="Eliminar Puesto"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';

                    //} else {
                    //    html = '<button  class="btn btn-xs"  onClick="fn_EliminarPuesto(' + row.ID_PUESTO + ',' + row.ID_ENTIDAD + ',1' +  ');" type="button" title="Activar Puesto"><i class="fas fa-undo-alt" style="color:#0097d9;font-size:18px"></i></button>';

                    //}
                    return html;
                }
            },
        ],

    });
}
function fn_DescargarPuesto(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Administracion/Entidad/VerArchivo?id=" + ID + "&proceso=PU", function (responseText, textStatus, request) {
    });
}
function CargarGrillaPuestoPac(id) {
    var item =
    {
        ID_ENTIDAD: id,
         DES_PUESTO: $('#txtpuesto').val(),
    };
    var url = baseUrl + 'Administracion/Entidad/ListaPuestos';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TablePuestosPac.rows().remove();
    if (respuesta != null && respuesta != "") {
        TablePuestosPac.rows.add(respuesta).draw();
    }
}
function fn_ValidarDuplicadoPuesto() {
    var item =
    {
        DES_PUESTO: $("#DES_PUESTO").val(),
    };
    var urls = baseUrl + 'Administracion/Entidad/ValidarDuplicidadPuesto';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}
function fn_EliminarPuesto(ID_PUESTO, ID_ENTIDAD, ACCION) {
    var flg = "";
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Eliminar</b> el puesto.? </br>", function (r) {
        if (r) {
            ACCION == 0 ? flg = "D" : flg = "H";
            item = {
                ID_PUESTO: ID_PUESTO,
                ID_ENTIDAD: ID_ENTIDAD,
                DES_PUESTO: "",
                ACCION: flg,
            };
            var url = baseUrl + 'Administracion/Entidad/MantenimientoPuesto';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaPuestoPac($("#PUESTO_ID_ENTIDAD").val());
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
            }
        }
    });
}
function fn_NuevoEditarMontoF(ID_PERIODO, ID_ENTIDAD,MES) {
    jQuery("#modalNuevoEditaMontoFac").html('');
    jQuery("#modalNuevoEditaMontoFac").load(baseUrl + "Administracion/Entidad/NuevoEditaMontoMensual?ID_PERIODO=" + ID_PERIODO + "&ID_ENTIDAD=" + ID_ENTIDAD + "&MES=" + MES + "&TIPO=F", function (responseText, textStatus, request) {
        $("#modalNuevoEditaMontoFac").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_NuevoEditarMontoP(ID_PERIODO, ID_ENTIDAD,MES) {
    jQuery("#modalNuevoEditaMontoPac").html('');
    jQuery("#modalNuevoEditaMontoPac").load(baseUrl + "Administracion/Entidad/NuevoEditaMontoMensual?ID_PERIODO=" + ID_PERIODO + "&ID_ENTIDAD=" + ID_ENTIDAD + "&MES=" + MES + "&TIPO=P", function (responseText, textStatus, request) {
        $("#modalNuevoEditaMontoPac").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarEditarMontoMensual() {
    jQuery("#modalNuevoEditaMontoFac").html('');
    $('#modalNuevoEditaMontoFac').modal('hide');
    jQuery("#modalNuevoEditaMontoPac").html('');
    $('#modalNuevoEditaMontoPac').modal('hide');
}
function fn_UpdateMensualPeriodoEntidad() {
    var periodo = $("#M_ID_PERIODO").val();
    //if ($('#fileArchivo')[0].files.length === 0) { $("#id_filearchivo").text("Es necesario que adjunte el archivo."); result = false; } else { $("#id_filearchivo").text(""); }


    var doc = document.getElementById("fileArchivoMonto").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivoMonto").files[l], e.append("fileArchivoMonto", a);
    e.append("ID_PERIODO", periodo);
    e.append("ID_ENTIDAD", $("#M_ID_ENTIDAD").val());
    e.append("NUM_MES", $("#M_NUM_MES").val());
    e.append("MONTO_MENSUAL", $("#MONTO_MENSUAL").val());
    e.append("ANIO_PERIODO", $("#txtFanio").val());
    
    
    $.ajax({
        url: baseUrl + "Administracion/Entidad/UpdateMensualPeriodoEntidad",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
            DESARROLLO.PROCESO_CORRECTO(p.message);
            if ($("#M_ACCION").val() == "F") {
                CargarGrillaPeriodoDetalleFag(periodo);
            } else {
                CargarGrillaPeriodoDetallePac(periodo);
            }  
                fn_CerrarEditarMontoMensual();
            
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
    //var periodo = $("#M_ID_PERIODO").val();
    //item = {
    //    ID_PERIODO: $("#M_ID_PERIODO").val(),
    //    ID_ENTIDAD: $("#M_ID_ENTIDAD").val(),
    //    NUM_MES: $("#M_NUM_MES").val(),
    //    MONTO_MENSUAL: $("#MONTO_MENSUAL").val(),
    //};
    //var url = baseUrl + 'Administracion/Entidad/UpdateMensualPeriodoEntidad';
    //var respuesta = DESARROLLO.Ajax(url, item, false);
    //if (respuesta != null && respuesta != "") {
    //    if (respuesta.success) { 
    //        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
    //        if ($("#M_ACCION").val() == "F") {
    //            CargarGrillaPeriodoDetalleFag(periodo);
    //        } else {
    //            CargarGrillaPeriodoDetallePac(periodo);
    //        }  
    //        fn_CerrarEditarMontoMensual();
    //    } else {
    //        DESARROLLO.PROCESO_ERROR(respuesta.extra);
    //    }
    //}
}
function DescargarArchivoSustentoPer(periodo,mes) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Administracion/Entidad/VerArchivo?id=" + periodo + "&proceso=" + mes, function (responseText, textStatus, request) {
    });
}
function CrearGrillaEntidadesOrgano() {
    DESARROLLO.configurarGrilla();
    TableEntidadesOrgano = $("#tabla_organo").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "Acciones", "data": "", "class": "text-center", "width": "2%" },
            { "title": "Órgano", "data": "DESC_ENTIDAD", "class": "text-center", "width": "10%" },
            { "title": "FLG_ESTADO", "data": "FLG_ESTADO", "class": "text-left", "visible": false, },

        ],
        "columnDefs": [
            {
                'targets': 1,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO == "1") {
                        html = '<button  class="btn btn-xs"  onClick="fn_NuevoEditarOrgano(' + row.ID_ENTIDAD + ');" type="button" title="Actualizar Organo"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_EliminarOrgano(' + row.ID_ENTIDAD + ');" type="button" title="Eliminar Entidad"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                    }
                    else {
                        html = '<button  class="btn btn-xs"  onClick="fn_HabilitarEntidadOrgano(' + row.ID_ENTIDAD + ');" type="button" title="Activar Entidad"><i class="fas fa-undo-alt" style="color:#0097d9;font-size:18px"></i></button>';

                    }
                    return html;
                },
            },
        ],

    });
}
function CargarGrillaEntidadesOrgano() {
    var item =
    {
    };
    var url = baseUrl + 'Administracion/Entidad/ListaEntidades_Organo?id=' + $('#ID_ENTIDAD').val();
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableEntidadesOrgano.rows().remove();
    if (respuesta != null && respuesta != "") {
        TableEntidadesOrgano.rows.add(respuesta).draw();
    }
}
function fn_InsertarOrganoEntidad() {
    item = {
        ID_ENTIDAD: $("#ID_ENTIDAD").val(),
        DESC_ENTIDAD: $("#txtOrgano").val(),
        DESC_UNIDAD: "",
        ACCION: "IO",
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEntidadesOrgano();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            CargarGrillaEntidades();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_ValidarDuplicadoOrgano() {
    var item =
    {
        ID_ENTIDAD: $("#ID_ENTIDAD").val(),
        DESC_ENTIDAD: $("#txtOrgano").val(),
    };
    var urls = baseUrl + 'Administracion/Entidad/ValidarDuplicidadEntidadOgano';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}
function fn_HabilitarEntidadOrgano(id) {
    item = {
        ID_ENTIDAD: id,
        DESC_ENTIDAD: "",
        ACCION: "H",
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEntidadesOrgano();
            DESARROLLO.PROCESO_CORRECTO("El órgano se habilito correctamente.");
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_EliminarOrgano(id) {
    item = {
        ID_ENTIDAD: id,
        DESC_ENTIDAD: "",
        ACCION: "D",
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEntidadesOrgano();
            DESARROLLO.PROCESO_CORRECTO("El órgano se elimino correctamente.");
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_NuevoEditarOrgano(id) {
    var ID_ENTIDAD = id;
    jQuery("#modalOrgano").html('');
    jQuery("#modalOrgano").load(baseUrl + "Administracion/Entidad/NuevoEditaOrgano?ID_ENTIDAD=" + ID_ENTIDAD, function (responseText, textStatus, request) {
        $("#modalOrgano").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarOrgano() {
    jQuery("#modalOrgano").html('');
    $('#modalOrgano').modal('hide');
}
function fn_MantenimientoOrgano(Accion) {
    item = {
        ID_ENTIDAD: $("#OR_ID_ENTIDAD").val(),
        DESC_ENTIDAD: $("#txtOrgano_").val(),
        DESC_UNIDAD: $("#DESC_UNIDAD").val(),
        ACCION: Accion,
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEntidades';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEntidadesOrgano();
            DESARROLLO.PROCESO_CORRECTO("El órgano se  actualizo correctamente.");
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_ValidarDuplicadoOrgano_() {
    var item =
    {
        ID_ENTIDAD: $("#ID_ENTIDAD").val(),
        DESC_ENTIDAD: $("#txtOrgano_").val(),
    };
    var urls = baseUrl + 'Administracion/Entidad/ValidarDuplicidadEntidadOgano';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}
function CrearGrillaPeriodoEntidadesFAG() {
    DESARROLLO.configurarGrilla();
    TablePeriodoEntidades = $("#tabla_periodo_F").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_PERIODO", "data": "ID_PERIODO", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "width": "10%", "visible": false, },

            { "title": "Editar", "data": "", "class": "text-center", "width": "10%"},
            { "title": "Año", "data": "ANIO_PERIODO", "class": "text-center", "width": "12%" ,},
            { "title": "Inicio", "data": "FEC_PERIODO_INI", "class": "text-center", },
            { "title": "Fin", "data": "FEC_PERIODO_FIN", "class": "text-center",  },
            { "title": "Monto", "data": "MONTO_MENSUAL", "class": "text-center", },
            { "title": "NOMBRE_ARCHIVO", "data": "NOMBRE_ARCHIVO", "class": "text-left", "visible": false, },
            { "title": "Ar.", "data": "", "class": "text-center", },
            { "title": "CANT_DEPENDENCIA", "data": "CANT_DEPENDENCIA", "class": "text-center", "visible": false, },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-center", "visible": false, },

            
        ],
        "columnDefs": [
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_NuevoPeriodoFag('+row.ID_ENTIDAD+',' + row.ID_PERIODO + ');" type="button" title="Editar Asignación"><i class="fas fa-pen-square" style="color:red;font-size:18px"></i></button>';
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = DESARROLLO.FECHA_DATATABLE(row.FEC_PERIODO_INI);
                    return html;
                }
            },
            {
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = DESARROLLO.FECHA_DATATABLE(row.FEC_PERIODO_FIN);
                    return html;
                }
            },
            {
                "targets": [7],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html ='';
                    if (row.ARCHIVO > 0) {
                        html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-file-pdf" style="color:red;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
        ],

    });
    $('#tabla_periodo_F tbody').on('click', 'tr', function () {
        var data = TablePeriodoEntidades.row(this).data();
        if (data != null) {
            if (data.CANT_DEPENDENCIA=="0") {
                CargarGrillaPeriodoDetalleFag(data.ID_PERIODO);
            } 
          
        }
    
    });
}
function CargarGrillaPeriodoEntidadesFAG(anio) {
    var item =
    {
    };
    var url = baseUrl + "Administracion/Entidad/ListaPeriodoEntidades?anio=" + anio + "&id_entidad=" + $("#F_ID_ENTIDAD").val() + "&tipo_proceso=" + $('#FLG_PROCESO').val();
    var respuesta = DESARROLLO.Ajax(url, item, false);
    
    TablePeriodoEntidades.rows().remove();
    if (respuesta != null && respuesta != "") {
        TablePeriodoEntidades.rows.add(respuesta).draw();
    }
}
function fn_NuevoPeriodoFag(id,periodo) {
    var ID_ENTIDAD = id;
    jQuery("#modalPeriodoFag").html('');
    jQuery("#modalPeriodoFag").load(baseUrl + "Administracion/Entidad/GenerarPeriodo?ID_ENTIDAD=" + ID_ENTIDAD  + "&ID_PERIODO=" + periodo, function (responseText, textStatus, request) {
        $("#modalPeriodoFag").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarGenerarPeriodo() {
    jQuery("#modalPeriodoFag").html('');
    $('#modalPeriodoFag').modal('hide');
}
function VerArchivoFag(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Administracion/Entidad/VerArchivo?id=" + ID + "&proceso=P", function (responseText, textStatus, request) {
    });

}
function fn_ValidarPeriodoFag() {
    var item =
    {
        ID_ENTIDAD: $("#G_ID_ENTIDAD").val(),
        ANIO_PERIODO: $("#txtFacanio").val(),
        FEC_PERIODO_INI: $("#FechaFinicio").val(),
        FEC_PERIODO_FIN: $("#FechaFfin").val(),
        TIPO_PROCESO: $("#FLG_PROCESO").val(),
        ID_PERIODO: $("#G_ID_PERIODO").val(),
    };
    console.log("item");
    console.log(item);

    var urls = baseUrl + 'Administracion/Entidad/ValidarDetallePeriodo';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}
function callPeriodo() {
    $.getJSON(baseUrl + "Administracion/Entidad/ListarPeriodoCbo",
        {
            ANIO_PERIODO: $("#txtFanio").val(),
            ID_ENTIDAD: $("#F_ID_ENTIDAD").val(),
            TIPO_PROCESO: $("#FLG_PROCESO").val(),
        }, function (e) {
            $("#COD_PERIODO").empty();
            $.each(e, function (e, o) {
                $("#COD_PERIODO").append($("<option></option>").val(o.ID_PERIODO).html(o.DESCRIPCION));
            })
        });
}
function CrearGrillaPeriodoEntidadesOrganoFAG() {
    DESARROLLO.configurarGrilla();
    tabla_periodo_Organo_F = $("#tabla_periodo_Organo_F").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_PERIODO", "data": "ID_PERIODO", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "width": "10%", "visible": false, },
            { "title": "Año", "data": "ANIO_PERIODO", "class": "text-center", "width": "12%", },
            { "title": "Inicio", "data": "FEC_PERIODO_INI", "class": "text-center", },
            { "title": "Fin", "data": "FEC_PERIODO_FIN", "class": "text-center", },
            { "title": "Monto", "data": "MONTO_MENSUAL", "class": "text-center", },
            { "title": "NOMBRE_ARCHIVO", "data": "NOMBRE_ARCHIVO", "class": "text-left", "visible": false, },
            { "title": "Archivo", "data": "", "class": "text-center", },
            { "title": "ARCHIVO", "data": "ARCHIVO", "class": "text-left", "visible": false, },
        ],
        "columnDefs": [
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = DESARROLLO.FECHA_DATATABLE(row.FEC_PERIODO_INI);
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = DESARROLLO.FECHA_DATATABLE(row.FEC_PERIODO_FIN);
                    return html;
                }
            },
            {
                "targets": [6],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                'targets': 8,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.ARCHIVO > 0) {
                        html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO + ');" type="button" title="Descargar Archivo"><i class="fas fa-file-pdf" style="color:red;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
        ],

    });
    $('#tabla_periodo_Organo_F tbody').on('click', 'tr', function () {
        var data = tabla_periodo_Organo_F.row(this).data();
        if (data != null) {
            CargarGrillaPeriodoDetalleFag(data.ID_PERIODO);
        }

    });
}
function CargarGrillaPeriodoEntidadesOrganoFAG(id,anio) {
    var item =
    {
    };
    var url = baseUrl + "Administracion/Entidad/ListaPeriodoEntidades?anio=" + anio + "&id_entidad=" + id + "&tipo_proceso=" + $('#FLG_PROCESO').val() ;
    var respuesta = DESARROLLO.Ajax(url, item, false);
    
    tabla_periodo_Organo_F.rows().remove();
    if (respuesta != null && respuesta != "") {
        tabla_periodo_Organo_F.rows.add(respuesta).draw();
    }
}
function fn_NuevoPeriodoOrganoFag(id, periodo) {
    jQuery("#modalPeriodoOrganoFag").html('');
    jQuery("#modalPeriodoOrganoFag").load(baseUrl + "Administracion/Entidad/GenerarPeriodoOrgano?ID_ENTIDAD=" + id + "&ID_PERIODO=" + periodo, function (responseText, textStatus, request) {
        $("#modalPeriodoOrganoFag").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarPeriodoOrgano() {
    jQuery("#modalPeriodoOrganoFag").html('');
    $('#modalPeriodoOrganoFag').modal('hide');
}
function fn_PeriodoOrganoFag() {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_PERIODO", $("#G_ID_PERIODO").val());
    e.append("ID_ENTIDAD", $("#G_ID_ENTIDAD").val());
    e.append("TIPO_PROCESO", $('#FLG_PROCESO').val());
    e.append("ANIO_PERIODO", $("#txtFacanio").val());
    e.append("FEC_PERIODO_INI", $("#FechaFinicio").val());
    e.append("FEC_PERIODO_FIN", $("#FechaFfin").val());
    e.append("MONTO_MENSUAL", $("#txtFmonto").val());
    
    $.ajax({
        url: baseUrl + "Administracion/Entidad/MantenimientoPeriodoEntidad",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                CargarGrillaPeriodoEntidadesOrganoFAG($("#CboOrgano").val(), $("#txtFanio").val());
                fn_CerrarPeriodoOrgano();
                callPeriodo();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
        }
    });
}
function fn_CerrarPrincipalEntidad() {
    if ($('#FLG_PROCESO').val() == 'F') {
        jQuery("#modalProcesoFag").html('');
        $('#modalProcesoFag').modal('hide');
    } else {
        jQuery("#modalProcesoPac").html('');
        $('#modalProcesoPac').modal('hide');
    }
}
///*DESCARGAR ARCHIVO INI        
function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Administracion/Entidad/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}
///*FIN DESCARGAR ARHIVO