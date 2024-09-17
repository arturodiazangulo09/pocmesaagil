/**  PROCESOS GLOBALES **/
var TableGenerales = null;

/**  FIN PROCESOS GLOBALES **/
function CrearGrillaGenerales() {
    DESARROLLO.configurarGrilla(true);
    TableGenerales = $("#TableGenerales").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": true,
        columns: [
            { "title": "ID_GENERAL", "data": "ID_GENERAL", "class": "text-center", "visible": false, },
            { "title": "Acciones", "data": "", "class": "text-center", "width": "10%" },
            { "title": "AÑO", "data": "ANIO", "class": "text-center", "width": "30%" },
            { "title": "PIA", "data": "PIA", "class": "text-center", "width": "30%" },
            { "title": "PIM", "data": "PIM", "class": "text-left", "width": "30%" },  
        ],
        "columnDefs": [
            {
                'targets': 1,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_NuevoEditar(' + row.ID_GENERAL + ');" type="button" title="Actualizar Registro"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                    return html;
                }
            }
        ],

    });
}
function CargarGrillaGenerales() {
    item = {}
    console.log("entro a CargarGrillaGenerales");
    var url = baseUrl + 'Administracion/Entidad/ListaGenerales';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    //TableGenerales.rows().remove();
    TableGenerales.rows().remove().draw();
    if (respuesta != null && respuesta != "") {
        TableGenerales.rows.add(respuesta).draw();
    }
}
function fn_NuevoEditar(id) {
    var ID_GENERAL = id;
    jQuery("#modalNuevoEdita").html('');
    jQuery("#modalNuevoEdita").load(baseUrl + "Administracion/Entidad/NuevoEditaGeneral?ID_GENERAL=" + ID_GENERAL, function (responseText, textStatus, request) {
        $("#modalNuevoEdita").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_MantenimientoGeneral(Accion) {
    console.log("Entró a fn_MantenimientoGeneral");
    console.log("Accion:" + Accion);

                item = {
                    ID_GENERAL: $("#ID_GENERAL").val(),
                    ANIO: $("#ANIO").val(),
                    PIA: $("#PIA").val(),
                    PIM: $("#PIM").val(),
                    ACCION: Accion,
                };
                var url = baseUrl + 'Administracion/Entidad/MantenimientoGenerales';
                var respuesta = DESARROLLO.Ajax(url, item, false);
                if (respuesta != null && respuesta != "") {
                    if (respuesta.success) {
                        CargarGrillaGenerales();
                        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                    } else {
                        DESARROLLO.PROCESO_ERROR(respuesta.extra);
                    }
                }
}
function fn_EliminarRegistro(id) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>Eliminar</b> registro.?", function (r) {
        if (r) {
            item = {
                ID_GENERAL: id,
                ACCION: "D",
            };
            var url = baseUrl + 'Administracion/Entidad/MantenimientoGenerales';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaGenerales();
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
            }
        }
    });
 
}
function ValidarDuplicidadGeneral() {
    console.log("entró a ValidarDuplicidadGeneral");
    var item =
    {
        ANIO: $("#ANIO").val()
    };
    var urls = baseUrl + 'Administracion/Entidad/ValidarDuplicidadGeneral';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}

function fn_CerrarGeneral() {
    jQuery("#modalNuevoEdita").html('');
    $('#modalNuevoEdita').modal('hide');
}


