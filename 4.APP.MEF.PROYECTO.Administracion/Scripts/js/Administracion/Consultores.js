var TableConsultores_ = null;
var TableInformacion_ = null;
function CrearGrillaSolicitudes() {
    DESARROLLO.configurarGrilla();
    TableConsultores_ = $("#TableConsultores").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": true,
        columns: [
            
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "2%" },
            { "title": "N. DOCUMENTO", "data": "NUM_DOCUMENTO", "class": "text-center", "width": "10%" },
            { "title": "RUC", "data": "RUC", "class": "text-center", "width": "12%" },
            { "title": "APELLIDOS NOMBRES", "data": "APELLIDO_NOMBRES", "class": "text-left", },
            { "title": "F. NACIMIENTO", "data": "FECHA_NACIMIENTO", "class": "text-center", },
            { "title": "NACIONALIDAD", "data": "DESC_NACIONALIDAD", "class": "text-center", },
            { "title": "CELULAR", "data": "CELULAR", "class": "text-center", },
            { "title": "TELEFONO", "data": "TELEFONO", "class": "text-center", },
            { "title": "EMAIL", "data": "EMAIL", "class": "text-left", },


        ],
        "columnDefs": [
            {
                'targets': 1,
                'render': function (data, type, row, meta) {
                    var html = '';
                
                    html = '<button  class="btn btn-xs"  onClick="fn_NuevoEditarComentarios(' + row.ID_PERSONAL + ' );" type="button" title="Ingresar Información"><i class="fas fa-comment-medical" style="color:#e30613;font-size:18px"></i></button>';
           
                    return html;
                }
            },
            {
                'targets': 5,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (DESARROLLO.FECHA_DATATABLE(row.FECHA_NACIMIENTO) !="31/12/0") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_NACIMIENTO);
                    }
         
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaSolicitudes() {
    var item =
    {
        APELLIDO_NOMBRES: $("#txtApellidoNom").val(),
        NUM_DOCUMENTO: $("#txtDni").val(),
    };
    var url = baseUrl + 'Solicitudes/Consultores/ListaPersonalAdministrador';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    //TableSolicitud_.rows().remove();
    TableConsultores_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableConsultores_.rows.add(respuesta).draw();
    }
}
function fn_NuevoEditarComentarios(id) {
    jQuery("#modalComentarios").html('');
    jQuery("#modalComentarios").load(baseUrl + "Solicitudes/Consultores/PersonalComentarios?ID_PERSONAL=" + id, function (responseText, textStatus, request) {
        $("#modalComentarios").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarInformacion() {
    jQuery("#modalComentarios").html('');
    $('#modalComentarios').modal('hide');
}
function fn_NuevoInformacionEditar(id) {
    jQuery("#modalNuevoEditInfo").html('');
    jQuery("#modalNuevoEditInfo").load(baseUrl + "Solicitudes/Consultores/EditarPersonalInformacion?ID_INFORMACION=" + id, function (responseText, textStatus, request) {
        $("#modalNuevoEditInfo").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarPersonalInformacion() {
    jQuery("#modalNuevoEditInfo").html('');
    $('#modalNuevoEditInfo').modal('hide');
}
function fn_MantenimientoInformacion() {
    var e = new FormData;
    e.append("ID_INFORMACION", $("#ID_INFORMACION").val());
    e.append("ID_PERSONAL", $("#ID_PERSONAL").val());
    e.append("DES_INFORMACION", $("#txtInformacion").val());
    e.append("ENTIDAD", $("#txtentidad").val());
    e.append("PERSONA", $("#txtContacto").val());
    e.append("CELULAR", $("#txtCelular").val());
    e.append("ID_INFORMACION", $("#ID_INFORMACION").val());
    e.append("ACCION", $("#ACCION").val());
    $.ajax({
        url: baseUrl + "Solicitudes/Consultores/MantenimientoInformacion",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                CargarGrillaInformacionPersonal();
                fn_CerrarPersonalInformacion();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function CrearGrillaInformacionPersonal() {
    DESARROLLO.configurarGrilla();
    TableInformacion_ = $("#TableInformacion").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [

            { "title": "ID_INFORMACION", "data": "ID_INFORMACION", "class": "text-center", "visible": false, },
            { "title": "ID_PERSONAL", "data": "ID_PERSONAL", "class": "text-center", "visible": false, },

            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "DETALLE", "data": "DES_INFORMACION", "class": "text-left", "width": "50%" },
            { "title": "ENTIDAD", "data": "ENTIDAD", "class": "text-left", "width": "10%" },
            { "title": "CONTACTO", "data": "PERSONA", "class": "text-left", "width": "10%"},
            { "title": "CELULAR", "data": "CELULAR", "class": "text-left", "width": "10%"},
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_NuevoInformacionEditar(' + row.ID_INFORMACION + ');" type="button" title="Editar Información"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                    html += '<button  class="btn btn-xs"  onClick="fn_DeleteInformacion(' + row.ID_INFORMACION + ');" type="button" title="Eliminar Información"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                    return html;
                }
            },

        ],

    });
}
function CargarGrillaInformacionPersonal() {
    var item =
    {
        ID_INFORMACION: 0,
        ID_PERSONAL: $("#ID_PERSONAL").val(),
        DES_INFORMACION: $("#txtinformacion").val(),
    };
    var url = baseUrl + 'Solicitudes/Consultores/ListaInformacionPersonal';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableInformacion_.rows().remove().draw();
    //TableInformacion_.rows().remove();
    if (respuesta != null && respuesta != "") {
        TableInformacion_.rows.add(respuesta).draw();
    }
}
function fn_DeleteInformacion(ID) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b>" + "ELIMINAR" + "</b> la informacion.? </br>", function (r) {
        if (r) {
            var e = new FormData;
            e.append("ID_INFORMACION", ID);
            e.append("ID_PERSONAL", $("#ID_PERSONAL").val());
            e.append("DES_INFORMACION", $("#txtInformacion").val());
            e.append("ENTIDAD", $("#txtentidad").val());
            e.append("PERSONA", $("#txtContacto").val());
            e.append("CELULAR", $("#txtCelular").val());
            e.append("ID_INFORMACION", $("#ID_INFORMACION").val());
            e.append("ACCION", "D");
            $.ajax({
                url: baseUrl + "Solicitudes/Consultores/MantenimientoInformacion",
                type: "POST",
                data: e,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                contentType: !1,
                processData: !1,
                success: function (p) {
                    if (p.success) {
                        DESARROLLO.PROCESO_CORRECTO(p.message);
                        CargarGrillaInformacionPersonal();
                        fn_CerrarPersonalInformacion();
                    }
                    else {
                        DESARROLLO.PROCESO_ERROR(p.extra);
                    }

                }
            });
        }
    });
 
}