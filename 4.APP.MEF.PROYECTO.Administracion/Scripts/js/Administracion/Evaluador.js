var tabla_Evaluadores_ = null;
var tabla_EntidadesAsignadas_ = null;
function fn_NuevoEditar(id) {
    jQuery("#modalEvaluador").html('');
    jQuery("#modalEvaluador").load(baseUrl + "Administracion/Entidad/NuevoEditEvaluador?ID_EVALUADOR=" + id, function (responseText, textStatus, request) {
        $("#modalEvaluador").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_NuevoAsignarEntidad(id) {
    jQuery("#modalAsignarEntidad").html('');
    jQuery("#modalAsignarEntidad").load(baseUrl + "Administracion/Entidad/NuevoEntidadAsignar?ID_EVALUADOR=" + id, function (responseText, textStatus, request) {
        $("#modalAsignarEntidad").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarEvaluador() {
    jQuery("#modalEvaluador").html('');
    $('#modalEvaluador').modal('hide');
}
function fn_CerrarAsignarEntidad() {
    jQuery("#modalAsignarEntidad").html('');
    $('#modalAsignarEntidad').modal('hide');
}
function fn_ValidarDuplicadEvaluador() {
    var item =
    {
        DESC_EVALUADOR: $("#DESC_EVALUADOR").val(),
    };
    var urls = baseUrl + 'Administracion/Entidad/ValidarDuplicidadEvaluador';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}
function fn_MantenimientoEvaluador(Accion) {
    item = {
        ID_EVALUADOR: $("#ID_EVALUADOR").val(),
        DESC_EVALUADOR: $("#DESC_EVALUADOR").val(),
        ACCION: Accion,
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEvaluador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            fn_CerrarEvaluador();
            CargarGrillaEvaluador();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_EliminarEvaluador(Accion) {
    item = {
        ID_EVALUADOR: Accion,
        DESC_EVALUADOR: $("#DESC_EVALUADOR").val(),
        ACCION: "D",
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEvaluador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEvaluador();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_HabilitarEvaluador(Accion) {
    item = {
        ID_EVALUADOR: Accion,
        DESC_EVALUADOR: $("#DESC_EVALUADOR").val(),
        ACCION: "H",
    };
    var url = baseUrl + 'Administracion/Entidad/MantenimientoEvaluador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEvaluador();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function CrearGrillaEvaluador() {
    DESARROLLO.configurarGrilla(true);
    tabla_Evaluadores_ = $("#tabla_Evaluadores").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": true,
        columns: [
            { "title": "ID_EVALUADOR", "data": "ID_EVALUADOR", "class": "text-center", "visible": false, },
            { "title": "Acciones", "data": "", "class": "text-center", "width": "1%" },
            { "title": "Entidades", "data": "", "class": "text-center", "width": "10%" },
            { "title": "Evaluador", "data": "DESC_EVALUADOR", "class": "text-left", },
            { "title": "FLG_ESTADO", "data": "FLG_ESTADO", "class": "text-left", "visible": false, },


        ],
        "columnDefs": [
            {
                'targets': 1,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO == "1") {
                        html = '<button  class="btn btn-xs"  onClick="fn_NuevoEditar(' + row.ID_EVALUADOR + ');" type="button" title="Actualizar Evaluador"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_EliminarEvaluador(' + row.ID_EVALUADOR + ');" type="button" title="Eliminar Evaluador"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                    }
                    else {
                        html = '<button  class="btn btn-xs"  onClick="fn_HabilitarEvaluador(' + row.ID_EVALUADOR + ');" type="button" title="Activar Evaluador"><i class="fas fa-undo-alt" style="color:#0097d9;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO == "1") {
                        html = '<button  class="btn btn-xs"  onClick="fn_NuevoAsignarEntidad(' + row.ID_EVALUADOR + ');" type="button" title="Lista de entidades asignadas"><i class="fas fa-list-ol" style="color:#606060;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaEvaluador() {
    var item =
    {
        DESC_EVALUADOR: $("#txtEvaluador").val(),
    };
    var url = baseUrl + 'Administracion/Entidad/ListaEvaluador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    tabla_Evaluadores_.rows().remove().draw();
    if (respuesta != null && respuesta != "") {
        tabla_Evaluadores_.rows.add(respuesta).draw();
    }
}
function CrearGrillaEvaluadorEntidas() {
    DESARROLLO.configurarGrilla();
    tabla_EntidadesAsignadas_ = $("#tabla_EntidadesAsignadas").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ENTIDAD", "data": "DESC_UNIDAD", "class": "text-left", },
        ],
        "columnDefs": [
            {
                'targets': 1,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html += '<button  class="btn btn-xs"  onClick="fn_RetirarEntidad(' + row.ID_ENTIDAD + ');" type="button" title="Eliminar asignación de la entidad"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaEvaluadorEntidad() {
    var item =
    {
        EVALUADOR: $("#H_DESC_EVALUADOR").val(),
    };
    var url = baseUrl + 'Administracion/Entidad/ListaEntidadEvaluador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    tabla_EntidadesAsignadas_.rows().remove().draw();
    if (respuesta != null && respuesta != "") {
        tabla_EntidadesAsignadas_.rows.add(respuesta).draw();
    }
}
function fn_AsginarEntidad(ID) {
    item = {
        ID_ENTIDAD: ID,
        EVALUADOR: $("#H_DESC_EVALUADOR").val(),
        ACCION: 'I',
    };
    var url = baseUrl + 'Administracion/Entidad/UDP_EvaluadorEntidad';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaEvaluadorEntidad();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            callEntidades();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
}
function fn_RetirarEntidad(ID) {

    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Retirar</b> la entidad asignada al evaluador.? </br>", function (r) {
        if (r) {
            item = {
                ID_ENTIDAD: ID,
                EVALUADOR: '',
                ACCION: 'D',
            };
            var url = baseUrl + 'Administracion/Entidad/UDP_EvaluadorEntidad';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaEvaluadorEntidad();
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                    callEntidades();
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
            }
        }
    });

}
function callEntidades() {
    
    $.getJSON(baseUrl + "Administracion/Entidad/ListarEntidadesAsignadas",
        { EVALUADOR: $("#H_DESC_EVALUADOR").val() }, function (e) {
            $("#CBO_ENTIDAD").empty();
            $.each(e, function (e, o) {
                $("#CBO_ENTIDAD").append($("<option></option>").val(o.ID_ENTIDAD).html(o.DESC_UNIDAD));
            })
        });
}