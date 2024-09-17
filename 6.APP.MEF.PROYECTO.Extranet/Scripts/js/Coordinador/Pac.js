/**  PROCESOS GLOBALES **/
var TablePuestosPac_ = null;
var TableSolicitudPac_ = null;
var ModelMeses = ["ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO",
    "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"];
var Ruc_Entidad;
/**  FIN PROCESOS GLOBALES **/
function fn_NuevoEditar(ID_PUESTO) {
    jQuery("#modalNuevoEditaPuesto").html('');
    jQuery("#modalNuevoEditaPuesto").load(baseUrl + "Coordinador/ProcesoPac/NuevoEditaPuesto?ID_PUESTO=" + ID_PUESTO + "&ID_ENTIDAD=" + $('#inputHddIdEntidad').val(), function (responseText, textStatus, request) {
        $("#modalNuevoEditaPuesto").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_NuevaFirmaTDR(ID_SOLICITUD) {

    jQuery("#modalTDR").html('');
    jQuery("#modalTDR").load(baseUrl + "Coordinador/Solicitud/UpdateArchivoTdr?ID_SOLICITUD=" + ID_SOLICITUD , function (responseText, textStatus, request) {
        $("#modalTDR").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_NuevaFirmaTDR_SN(ID_SOLICITUD) {

    jQuery("#modalTDR").html('');
    jQuery("#modalTDR").load(baseUrl + "Coordinador/Solicitud/UpdateArchivoTdrSN?ID_SOLICITUD=" + ID_SOLICITUD, function (responseText, textStatus, request) {
        $("#modalTDR").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function CrearGrillaPuestoPac() {
    DESARROLLO.configurarGrilla();
    TablePuestosPac_ = $("#TablePuestosPac").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_PUESTO", "data": "ID_PUESTO", "class": "text-center", "visible": false, },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "CREACIÓN", "data": "FEC_INGRESO", "class": "text-center", "width": "1%"  },
            { "title": "PUESTO", "data": "DES_PUESTO", "class": "text-left", },
            { "title": "FICHA", "data": "DESC_TIPO_FICHA", "class": "text-left", },
            { "title": "MONTO", "data": "MONTO_PUESTO", "class": "text-center", "width": "1%" },
            { "title": "FLG_ESTADO", "data": "FLG_ESTADO", "class": "text-left", "visible": false, },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO_PUESTO", "data": "ARCHIVO_PUESTO", "class": "text-left", "visible": false, },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.FLG_ESTADO == "1") {
                        html = '<button  class="btn btn-xs"  onClick="fn_NuevoEditar(' + row.ID_PUESTO + ',' + row.ID_ENTIDAD   + ');" type="button" title="Editar Puesto"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_EliminarPuesto(' + row.ID_PUESTO + ',' + row.ID_ENTIDAD + ',0' + ');" type="button" title="Eliminar Puesto"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';

                    } else {
                        html = '<button  class="btn btn-xs"  onClick="fn_EliminarPuesto(' + row.ID_PUESTO + ',' + row.ID_ENTIDAD + ',1' +  ');" type="button" title="Activar Puesto"><i class="fas fa-undo-alt" style="color:#0097d9;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INGRESO);
                    return html;
                }
            },
            {
                'targets': 8,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO_PUESTO + ');" type="button" title="Descargar Archivo"><i class="fas fa-file-download" style="color:#6ABEB0;font-size:18px"></i></button>';
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaPuestoPac() {
    
    var item =
    {
        ID_PUESTO: 0,
        TIPO_FICHA: $('#vId_Ficha').val(),
        DES_PUESTO: $('#txtDescPuesto').val(),
        ID_ENTIDAD: $('#inputHddIdEntidad').val()
    };
    var url = baseUrl + 'Coordinador/ProcesoPac/ListaPuestos';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    $('#TablePuestosPac').DataTable().clear().draw();
    if (respuesta != null && respuesta != "") {
        $('#TablePuestosPac').DataTable().rows.add(respuesta).draw();
    }
}
function fn_ValidarTipoFicha() {
    if ($("#TIPO_FICHA").val() == "1") {
        $("#tipo_1").show();
        $("#tipo_2").hide();
        $("#P_I_1_1").val(0);
        $("#P_I_2_1").val(0);
        $("#P_I_3_1").val(0);
        $("#P_I_4_1").val(0);
    } else {
        if ($("#TIPO_FICHA").val() == "2") {
            $("#tipo_1").hide();
            $("#tipo_2").show();
            $("#P_H_1_1").val(0);
            $("#P_H_1_2").val(0);
            $("#P_H_1_3").val(0);
            $("#P_H_2_1").val(0);
            $("#P_H_2_2").val(0);
            $("#P_H_3_1").val(0);
            $("#P_H_3_2").val(0);
        } else {
            $("#tipo_1").hide();
            $("#tipo_2").hide();
            $("#P_H_1_1").val(0);
            $("#P_H_1_2").val(0);
            $("#P_H_1_3").val(0);
            $("#P_H_2_1").val(0);
            $("#P_H_2_2").val(0);
            $("#P_H_3_1").val(0);
            $("#P_H_3_2").val(0);
            $("#P_I_1_1").val(0);
            $("#P_I_2_1").val(0);
            $("#P_I_3_1").val(0);
            $("#P_I_4_1").val(0);
        }
    }
    fn_ValidarMontoAsignado();
}
function fn_ValidarMontoAsignado() {
    var P_H_1_1 = parseInt($("#P_H_1_1").val());
    var P_H_1_2 = parseInt($("#P_H_1_2").val());
    var P_H_1_3 = parseInt($("#P_H_1_3").val());
    var P_H_2_1 = parseInt($("#P_H_2_1").val());
    var P_H_2_2 = parseInt($("#P_H_2_2").val());
    var P_H_3_1 = parseInt($("#P_H_3_1").val());
    var P_H_3_2 = parseInt($("#P_H_3_2").val());


    var P_I_1_1 = parseInt($("#P_I_1_1").val());
    var P_I_2_1 = parseInt($("#P_I_2_1").val());
    var P_I_3_1 = parseInt($("#P_I_3_1").val());
    var P_I_4_1 = parseInt($("#P_I_4_1").val());

    var asignado = 0;
    var puntaje = 0;
    if ($("#TIPO_FICHA").val() == "1") {
        puntaje = (P_H_1_1 + P_H_1_2 + P_H_1_3 + P_H_2_1 + P_H_2_2 + P_H_3_1 + P_H_3_2)
    } else {
        if ($("#TIPO_FICHA").val() == "2") {
            puntaje = (P_I_1_1 + P_I_2_1 + P_I_3_1 + P_I_4_1)
        }
    }

    $("#P_TOTAL").val(puntaje);

    if (puntaje >= 80 && puntaje <= 83) {
        asignado = parseInt(16000);
    }
    if (puntaje >= 84 && puntaje <= 87) {
        asignado = parseInt(18250);
    }
    if (puntaje >= 88 && puntaje <= 91) {
        asignado = parseInt(20500);
    }
    if (puntaje >= 92 && puntaje <= 95) {
        asignado = parseInt(22750);
    }
    if (puntaje >= 96 && puntaje <= 100) {
        asignado = parseInt(25000);
    }

    $("#MONTO_PUESTO").val(asignado);
}
function fn_CerrarEditarPuesto() {
    jQuery("#modalNuevoEditaPuesto").html('');
    $('#modalNuevoEditaPuesto').modal('hide');
}
function fn_MantenimientoPuesto(Accion) {
    var doc = document.getElementById("fileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("fileArchivo").files[l], e.append("fileArchivo", a);
    e.append("ID_PUESTO", $("#ID_PUESTO").val());
    e.append("ID_ENTIDAD", $("#ID_ENTIDAD").val());
    e.append("DES_PUESTO", $('#DES_PUESTO').val().toUpperCase());
    e.append("ACCION", Accion);
    e.append("P_H_1_1", $('#P_H_1_1').val());
    e.append("P_H_1_2", $('#P_H_1_2').val());
    e.append("P_H_1_3", $('#P_H_1_3').val());
    e.append("P_H_2_1", $('#P_H_2_1').val());
    e.append("P_H_2_2", $('#P_H_2_2').val());
    e.append("P_H_3_1", $('#P_H_3_1').val());
    e.append("P_H_3_2", $('#P_H_3_2').val());
    e.append("P_I_1_1", $('#P_I_1_1').val());
    e.append("P_I_2_1", $('#P_I_2_1').val());
    e.append("P_I_3_1", $('#P_I_3_1').val());
    e.append("P_I_4_1", $('#P_I_4_1').val());
    e.append("TIPO_FICHA", $('#TIPO_FICHA').val());
    e.append("P_TOTAL", $('#P_TOTAL').val());
    e.append("MONTO_PUESTO", $('#MONTO_PUESTO').val());
    $.ajax({
        url: baseUrl + 'Coordinador/ProcesoPac/MantenimientoPuesto',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                fn_CerrarEditarPuesto();
                CargarGrillaPuestoPac();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
        }
    });

}
function fn_ValidarDuplicadoPuesto() {
    var item =
    {
        DES_PUESTO: $("#DES_PUESTO").val(),
        ID_ENTIDAD: $("#inputHddIdEntidad").val(),
    };
    var urls = baseUrl + 'Coordinador/ProcesoPac/ValidarDuplicidadPuesto';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    return respuesta.success;
}
function fn_DescargarPuesto(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=P", function (responseText, textStatus, request) {
    });

}
function fn_EliminarPuesto(ID_PUESTO, ID_ENTIDAD, ACCION) {
    
    var flg = "";
    var desc = "";
    ACCION == 0 ? desc = "</br>¿Desea <b>Eliminar</b> el puesto.? </br>" : desc = "</br>¿Desea <b>Habilitar</b> el puesto.? </br>" ;
    DESARROLLO.PROCESO_CONFIRM(desc, function (r) {
        if (r) {
            ACCION == 0 ? flg = "D" : flg = "H";
            item = {
                ID_PUESTO: ID_PUESTO,
                ID_ENTIDAD: ID_ENTIDAD,
                DES_PUESTO: "",
                ACCION: flg,
            };
            var url = baseUrl + 'Coordinador/ProcesoPac/MantenimientoPuesto';
            var respuesta = DESARROLLO.Ajax(url, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaPuestoPac();
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
            }
        }
    });
}
function fn_NuevaSolicitudEditar(ID_SOLICITUD, VERSION) {
 /*   VERSION == "2" ? text = "Coordinador/ProcesoPac/NuevoEditaSolicitud_v2?ID_SOLICITUD=" :*/ text = "Coordinador/Solicitud/NuevoEditaSolicitud?ID_SOLICITUD=";
    jQuery("#modalNuevoEditaSolicitud").html('');
    jQuery("#modalNuevoEditaSolicitud").load(baseUrl + text +  ID_SOLICITUD + "&PROCESO=P&ID_ENTIDAD=" + $('#inputHddIdEntidad').val(), function (responseText, textStatus, request) {
        $("#modalNuevoEditaSolicitud").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_ValidDetalleMonto(id) {
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/ListaPuestoDetalle?id=" + id,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (p) {
            
            if (p.MONTO_PUESTO > 0) {
                $("#txtMontoFijo").val(p.MONTO_PUESTO);
            }
        }
    });
}
function fn_ValidMontoPermitido(id) {
    var permitido = parseFloat($("#txtMontoFijo").val());
    var monto = parseFloat($("#MONTO_MENSUAL").val());
    if (monto > permitido) {
        DESARROLLO.PROCESO_ALERT("Alerta", "El monto ingresado supera lo permitido");
        $("#MONTO_MENSUAL").val(0);
    }
}
function fn_Aregar_Funcionalidad() {
    var cant = 0;
    if ($('#txtDescServicio').val().trim().length == 0) {
        $('#txtDescServicio').val("").focus();
        $("#txtDescServicio").addClass('is-invalid')
    } else {
        $("#txtDescServicio").removeClass('is-invalid');
        var cantidad = $('#tabla_funcionalidad >tbody >tr').length + 1;
        $("#tabla_funcionalidad tbody").append("<tr id='" + cantidad + "'> <td style='text-align:left'>" + cantidad + "</td> <td style='text-align:left'> " + $('#txtDescServicio').val() + " </td> <td style='text-align:center'> <a onclick='eliminar_funcionalidad(" + cantidad + ");' href='javascript:void(0)' title='Eliminar'><i class='fas fa-trash-alt' style='color:red'></i></a> </td > </tr > ");
        $('#txtDescServicio').val('').focus();
    }
}
function eliminar_funcionalidad(id) {
    DESARROLLO.PROCESO_CONFIRM("¿Esta seguro que desea eliminar el registro?<br>",function (r) {
        if (r == true) {
            $("#tabla_funcionalidad tbody tr[id=" + id + "]").remove();
        }
    });
}
function fn_Aregar_Formacion() {
    if (ValidFormacion()) {
        var cant = 0;
        if ($('#txtFormacion').val().trim().length == 0) {
            $('#txtFormacion').val("").focus();
            $("#txtFormacion").addClass('is-invalid')
        } else {
            $("#txtFormacion").removeClass('is-invalid');
            var cantidad = $('#tabla_formacion >tbody >tr').length + 1;
            $("#tabla_formacion tbody").append("<tr id='" + cantidad + "'> <td style='text-align:left'>" + cantidad + "</td> <td style='text-align:left'> " + $('#txtFormacion').val() + " </td> <td style='text-align:center'> <a onclick='eliminar_formacion(" + cantidad + ");' href='javascript:void(0)' title='Eliminar'><i class='fas fa-trash-alt' style='color:red'></i></a> </td > </tr > ");
            $('#txtFormacion').val('').focus();
        }
    }
    
}

function ValidFormacion() {
    var result = true;
    var serepite = true;
    serepite = ValidarRepeticionFormacion($('#txtFormacion').val())
    result = !serepite;
    if (serepite) { //se repite
        DESARROLLO.PROCESO_ALERT("Información académica ya existe");
        $("#txtFormacion").addClass('is-invalid');
    } else {
        $("#txtFormacion").removeClass('is-invalid');
    }
    return result;
}

function ValidarRepeticionFormacion(descripcion) {
    serepite = false;
    $("#frmNuevaSolicitudFag").find("#Tabla_Formacion" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var DESC_ACADEMICA = entorno.find("input.tdDESC_ACADEMICA", "id").val();
        if ((DESC_ACADEMICA) == (descripcion)) {
            serepite = true;
        }
    });
    return serepite;
}
function eliminar_formacion(id) {
    DESARROLLO.PROCESO_CONFIRM("¿Esta seguro que desea eliminar el registro?<br>", function (r) {
        if (r == true) {
            $("#tabla_formacion tbody tr[id=" + id + "]").remove();
        }
    });
}
function fn_Aregar_Conocimientos() {
    var cant = 0;
    if ($('#txtConocimientos').val().trim().length == 0) {
        $('#txtConocimientos').val("").focus();
        $("#txtConocimientos").addClass('is-invalid')
    } else {
        $("#txtConocimientos").removeClass('is-invalid');
        var cantidad = $('#tabla_conocimientos >tbody >tr').length + 1;
        $("#tabla_conocimientos tbody").append("<tr id='" + cantidad + "'> <td style='text-align:left'>" + cantidad + "</td> <td style='text-align:left'> " + $('#txtConocimientos').val() + " </td> <td style='text-align:center'> <a onclick='eliminar_conocimiento(" + cantidad + ");' href='javascript:void(0)' title='Eliminar'><i class='fas fa-trash-alt' style='color:red'></i></a> </td > </tr > ");
        $('#txtConocimientos').val('').focus();
    }
}
function eliminar_conocimiento(id) {
    DESARROLLO.PROCESO_CONFIRM("¿Esta seguro que desea eliminar el registro?<br>", function (r) {
        if (r == true) {
            $("#tabla_conocimientos tbody tr[id=" + id + "]").remove();
        }
    });
}
function fn_Aregar_Curso() {
    var cant = 0;
    if ($('#txtCurso').val().trim().length == 0) {
        $('#txtCurso').val("").focus();
        $("#txtCurso").addClass('is-invalid')
    } else {
        $("#txtCurso").removeClass('is-invalid');
        var cantidad = $('#tabla_curso >tbody >tr').length + 1;
        $("#tabla_curso tbody").append("<tr id='" + cantidad + "'> <td style='text-align:left'>" + cantidad + "</td> <td style='text-align:left'> " + $('#txtCurso').val() + " </td> <td style='text-align:center'> <a onclick='eliminar_curso(" + cantidad + ");' href='javascript:void(0)' title='Eliminar'><i class='fas fa-trash-alt' style='color:red'></i></a> </td > </tr > ");
        $('#txtCurso').val('').focus();
    }
}
function eliminar_curso(id) {
    DESARROLLO.PROCESO_CONFIRM("¿Esta seguro que desea eliminar el registro?<br>", function (r) {
        if (r == true) {
            $("#tabla_curso tbody tr[id=" + id + "]").remove();
        }
    });
}
function fn_Aregar_Experiencia() {
    var cant = 0;
    if ($('#txtExperiencia').val().trim().length == 0) {
        $('#txtExperiencia').val("").focus();
        $("#txtExperiencia").addClass('is-invalid')
    } else {
        $("#txtExperiencia").removeClass('is-invalid');
        var cantidad = $('#tabla_experiencia >tbody >tr').length + 1;
        $("#tabla_experiencia tbody").append("<tr id='" + cantidad + "'> <td style='text-align:left'>" + cantidad + "</td> <td style='text-align:left'> " + $('#txtExperiencia').val() + " </td> <td style='text-align:center'> <a onclick='eliminar_experiencia(" + cantidad + ");' href='javascript:void(0)' title='Eliminar'><i class='fas fa-trash-alt' style='color:red'></i></a> </td > </tr > ");
        $('#txtExperiencia').val('').focus();
    }
}
function eliminar_experiencia(id) {
    DESARROLLO.PROCESO_CONFIRM("¿Esta seguro que desea eliminar el registro?<br>", function (r) {
        if (r == true) {
            $("#tabla_experiencia tbody tr[id=" + id + "]").remove();
        }
    });
}
function fn_Aregar_Actividad() {
    var cant = 0;
    if ($('#txtDescActividad').val().trim().length == 0) {
        $('#txtDescActividad').val("").focus();
        $("#txtDescActividad").addClass('is-invalid')
    } else {
        $("#txtDescActividad").removeClass('is-invalid');
        var cantidad = $('#tabla_actividad >tbody >tr').length + 1;
        $("#tabla_actividad tbody").append("<tr id='" + cantidad + "'> <td style='text-align:left'>" + cantidad + "</td> <td style='text-align:left'> " + $('#txtDescActividad').val() + " </td> <td style='text-align:center'> <a onclick='eliminar_actividad(" + cantidad + ");' href='javascript:void(0)' title='Eliminar'><i class='fas fa-trash-alt' style='color:red'></i></a> </td > </tr > ");
        $('#txtDescActividad').val('').focus();
    }
}
function eliminar_actividad(id) {
    DESARROLLO.PROCESO_CONFIRM("¿Esta seguro que desea eliminar el registro?<br>", function (r) {
        if (r == true) {
            $("#tabla_actividad tbody tr[id=" + id + "]").remove();
        }
    });
}
function fn_Aregar_Otro() {
    var cant = 0;
    if ($('#txtDescOtro').val().trim().length == 0) {
        $('#txtDescOtro').val("").focus();
        $("#txtDescOtro").addClass('is-invalid')
    } else {
        $("#txtDescOtro").removeClass('is-invalid');
        var cantidad = $('#tabla_Otro >tbody >tr').length + 1;
        $("#tabla_Otro tbody").append("<tr id='" + cantidad + "'> <td style='text-align:left'>" + cantidad + "</td> <td style='text-align:left'> " + $('#txtDescOtro').val() + " </td> <td style='text-align:center'> <a onclick='eliminar_otro(" + cantidad + ");' href='javascript:void(0)' title='Eliminar'><i class='fas fa-trash-alt' style='color:red'></i></a> </td > </tr > ");
        $('#txtDescOtro').val('').focus();
    }
}
function eliminar_otro(id) {
    DESARROLLO.PROCESO_CONFIRM("¿Esta seguro que desea eliminar el registro?<br>", function (r) {
        if (r == true) {
            $("#tabla_Otro tbody tr[id=" + id + "]").remove();
        }
    });
}
function Fun_ValidarPersonaDni() {
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/ListaDetallePersona?dni=" + $("#NUM_DOCUMENTO_CONSULTOR").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (p) {
            if (p.ACCION == '1') {
                if (p.APELLIDO_PATERNO != "-") {
                    $("#APELLIDO_PAT_CONSULTOR").attr('disabled', 'disabled');
                    $("#APELLIDO_MAT_CONSULTOR").attr('disabled', 'disabled');
                    $("#NOMBRES_CONSULTOR").attr('disabled', 'disabled');

                    $("#APELLIDO_PAT_CONSULTOR").val(p.APELLIDO_PAT_CONSULTOR);
                    $("#APELLIDO_MAT_CONSULTOR").val(p.APELLIDO_MAT_CONSULTOR);
                    $("#NOMBRES_CONSULTOR").val(p.NOMBRES_CONSULTOR);
                } else {
                    $("#NUM_DOCUMENTO_CONSULTOR").val("");
                    $("#APELLIDO_PAT_CONSULTOR").val("");
                    $("#APELLIDO_MAT_CONSULTOR").val("");
                    $("#NOMBRES_CONSULTOR").val("");
                    DESARROLLO.PROCESO_ALERT('Atención', 'El número de documento no es válido.');
                }
                $("#myModalCargando").css("display", "none");
            } else {
                $("#APELLIDO_PAT_CONSULTOR").removeAttr('disabled');
                $("#APELLIDO_MAT_CONSULTOR").removeAttr('disabled');
                $("#NOMBRES_CONSULTOR").removeAttr('disabled');
                DESARROLLO.PROCESO_ALERT('Atención', 'Problemas al consultar el número de documento, ingrese su información manualmente.');
                console.log(p.DES_ERROR);
                $("#myModalCargando").css("display", "none");
            }
        }
    });

}
function Validar_Designado() {
    if ($("#FLG_CHECK_DESIGNADO").is(":checked")) {
        $('.DESG').show();

    } else { $('.DESG').hide(); }

}
function fn_CerrarEditarSolicitud() {
    jQuery("#modalNuevoEditaSolicitud").html('');
    $('#modalNuevoEditaSolicitud').modal('hide');
}
function fn_MantenimientoSolicitud() {
    var e = new FormData;
    if ($("#FLG_VERSION").val() == "1") {
        var doc = document.getElementById("FileArchivo").files.length;
        if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
    }

    var DESIG = "0";
    var NR_RESOLUCION_ = "";
    var FECHA_RESOLUCION_ = "";
    if ($("#FLG_CHECK_DESIGNADO").is(":checked")) {
        DESIG = "1";
        NR_RESOLUCION_ = $("#NR_RESOLUCION").val();
        FECHA_RESOLUCION_ = $("#FechaAsignacion").val();
    }
    var lista_formacion = new Array();
    if ($('#tabla_formacion >tbody >tr').length > 0) {
        $("#tabla_formacion tbody tr").each(function (index) {
            var codigo = "";
            var descrip = "";
            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 0:
                        codigo = ($(this).text());
                        break;
                    case 1:
                        descrip = ($(this).text());
                        break;

                }
            })

            var obj_filas = {
                DESC_ACADEMICA: (descrip.trim() == "" ? "-" : descrip),
            };
            lista_formacion.push(obj_filas);
        });
    }
    var lista_conocimientos = new Array();
    if ($('#tabla_conocimientos >tbody >tr').length > 0) {
        $("#tabla_conocimientos tbody tr").each(function (index) {
            var codigo = "";
            var descrip = "";
            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 0:
                        codigo = ($(this).text());
                        break;
                    case 1:
                        descrip = ($(this).text());
                        break;

                }
            })

            var obj_filas = {
                DESC_CONOCIMIENTO: (descrip.trim() == "" ? "-" : descrip),
            };
            lista_conocimientos.push(obj_filas);
        });
    }
    var lista_cursos = new Array();
    if ($('#tabla_curso >tbody >tr').length > 0) {
        $("#tabla_curso tbody tr").each(function (index) {
            var codigo = "";
            var descrip = "";
            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 0:
                        codigo = ($(this).text());
                        break;
                    case 1:
                        descrip = ($(this).text());
                        break;

                }
            })

            var obj_filas = {
                DESC_CURSO_PRO: (descrip.trim() == "" ? "-" : descrip),
            };
            lista_cursos.push(obj_filas);
        });
    }
    var lista_experiencia = new Array();
    if ($('#tabla_experiencia >tbody >tr').length > 0) {
        $("#tabla_experiencia tbody tr").each(function (index) {
            var codigo = "";
            var descrip = "";
            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 0:
                        codigo = ($(this).text());
                        break;
                    case 1:
                        descrip = ($(this).text());
                        break;

                }
            })

            var obj_filas = {
                DESC_EXPERIENCIA: (descrip.trim() == "" ? "-" : descrip),
            };
            lista_experiencia.push(obj_filas);
        });
    }
    var lista_funcionalidad = new Array();
    if ($('#tabla_funcionalidad >tbody >tr').length > 0) {
        $("#tabla_funcionalidad tbody tr").each(function (index) {
            var codigo = "";
            var descrip = "";
            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 0:
                        codigo = ($(this).text());
                        break;
                    case 1:
                        descrip = ($(this).text());
                        break;

                }
            })

            var obj_filas = {
                DESC_FUNCIONES: (descrip.trim() == "" ? "-" : descrip),
            };
            lista_funcionalidad.push(obj_filas);
        });
    }
    var lista_actividad = new Array();
    if ($('#tabla_actividad >tbody >tr').length > 0) {
        $("#tabla_actividad tbody tr").each(function (index) {
            var codigo = "";
            var descrip = "";
            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 0:
                        codigo = ($(this).text());
                        break;
                    case 1:
                        descrip = ($(this).text());
                        break;

                }
            })

            var obj_filas = {
                DESC_ACTIVIDAD: (descrip.trim() == "" ? "-" : descrip),
            };
            lista_actividad.push(obj_filas);
        });
    }
    var lista_otro = new Array();
    if ($('#tabla_Otro >tbody >tr').length > 0) {
        $("#tabla_Otro tbody tr").each(function (index) {
            var codigo = "";
            var descrip = "";
            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 0:
                        codigo = ($(this).text());
                        break;
                    case 1:
                        descrip = ($(this).text());
                        break;

                }
            })

            var obj_filas = {
                DESC_ACT_OTRO: (descrip.trim() == "" ? "-" : descrip),
            };
            lista_otro.push(obj_filas);
        });
    }
    var Lista_Periodo_pago = new Array();
    var Nr_Mes = 0;
    var Meses = "";
    var Monto = 0
    var Anio = 0;
    $("#TablaMeses tbody tr").each(function (index) {
        $(this).children("td").each(function (index2) {
            switch (index2) {
                case 0:
                    Anio = $(this).text();
                    break;
                case 1:
                    Nr_Mes = $(this).text();
                    break;
                case 2:
                    Meses = $(this).text();
                    break;
                case 3:
                    Monto = $(this).children('input').val();
                    break;
            }
        });
        var item = {

            DES_MES: Meses,
            ID_MES: Nr_Mes,
            MONTO: Monto,
            ANIO: Anio,
            ID_ENTIDAD: $("#inputHddIdEntidad").val(),
            TIPO_PROCESO: "P",
        };
        Lista_Periodo_pago.push(item);
    });
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("ID_ENTIDAD", $("#inputHddIdEntidad").val());
    e.append("DEPENDENCIA", $("#DEPENDENCIA").val());
    e.append("FECHA_INICIO", $("#FechaInicial").val());
    e.append("FECHA_FIN", $("#FechaFin").val());
    e.append("MONTO_MENSUAL", $("#MONTO_MENSUAL").val());
    e.append("ID_PUESTO", $("#DcboPuestos").val());
    e.append("CONFORMIDAD_SERVICIO", $("#CONFORMIDAD_SERVICIO").val());
    e.append("APE_NOMB_CERTIFICA", $("#APE_NOMB_CERTIFICA").val().toUpperCase());
    e.append("NUM_DOCUMENTO_CONSULTOR", $("#NUM_DOCUMENTO_CONSULTOR").val());
    e.append("APELLIDO_PAT_CONSULTOR", $("#APELLIDO_PAT_CONSULTOR").val());
    e.append("APELLIDO_MAT_CONSULTOR", $("#APELLIDO_MAT_CONSULTOR").val());
    e.append("NOMBRES_CONSULTOR", $("#NOMBRES_CONSULTOR").val());
    e.append("CORREO_CONSULTOR", $("#CORREO_CONSULTOR").val());
    e.append("FLG_PROCESO", $("#FLG_PROCESO").val());
    e.append("FLG_VERSION", $("#FLG_VERSION").val());
    e.append("OFICINA_CERTIFICA", $("#OFICINA_CERTIFICA").val().toUpperCase());
    e.append("FLG_DESIGNADO", DESIG);
    e.append("NR_RESOLUCION", NR_RESOLUCION_);
    e.append("DESC_ORGANO", $("#ID").val() == "0" ? $("#DESC_ORGANO").val() : "");
    e.append("FECHA_RESOLUCION", FECHA_RESOLUCION_);
    e.append("listaExperiencia", JSON.stringify(lista_experiencia));
    e.append("listaCurso", JSON.stringify(lista_cursos));
    e.append("listaAcademico", JSON.stringify(lista_formacion));
    e.append("listaConocimiento", JSON.stringify(lista_conocimientos));
    e.append("listaActividad", JSON.stringify(lista_actividad));
    e.append("listaAcividadOtro", JSON.stringify(lista_otro));
    e.append("listaFunciones", JSON.stringify(lista_funcionalidad));
    e.append("listaRenumeracion", JSON.stringify(Lista_Periodo_pago));
    e.append("listaDocumentos", JSON.stringify(GetData()));
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/MantenimientoSolicitudPac",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarEditarSolicitud();
                CargarGrillaSolicitudesPac();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                if (p.message2 == "0") {
                    fn_CerrarEditarSolicitudFag();
                    CargarGrillaSolicitudesFag();
                    DESARROLLO.PROCESO_ALERT("Atención", "Se registro la solicitud, Se generó inconvenientes al registrar los archivos de la solicitud, intente nuevamente en breves minutos.");
                } else {
                    DESARROLLO.PROCESO_ERROR(p.extra);
                }
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function CrearGrillaSolicitudesPac() {
    DESARROLLO.configurarGrilla(true);
    var mostrar = $("#DdlEstado").val() == "006" ? true : false;
    TableSolicitudPac_ = $("#TableSolicitudPac").DataTable({
        scrollX: true,
        paging: true,
        "autoWidth": true,
        "order": [[0, "ASC"]],
        columns: [
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%", 'orderable': false},
            { "title": "FORMATOS / ANEXOS", "data": "", "class": "text-center", "width": "1%", 'orderable': false},
            { "title": "ARCHIVOS", "data": "", "class": "text-center", "width": "1%", 'orderable': false},
            { "title": "FECHA DE REGISTRO", "data": "FECHA_REGISTRO", "class": "text-center", "width": "1%", 'orderable': true},
            { "title": "H.R. ASIGNADA", "data": "NR_TRAMITE", "class": "text-center", "width": "1%", 'orderable': false },
            { "title": "N° SOLICITUD", "data": "NUM_PROCESO", "class": "text-left", "width": "1%", 'orderable': true},
            { "title": "DNI / CE", "data": "NUM_DOCUMENTO_CONSULTOR", "class": "text-left", "width": "1%", 'orderable': false},
            { "title": "APELLIDOS Y NOMBRES", "data": "APELLIDO_PAT_CONSULTOR", "class": "text-left", "width": "1%", 'orderable': true },
            { "title": "N° DE CONTRATO", "data": "NUM_CONTRATO", "class": "text-left", "width": "1%", 'orderable': false },
            { "title": "AÑO", "data": "ANIO_PROCESO", "class": "text-center", "width": "1%", "visible": false, 'orderable': false},
            { "title": "FICHA", "data": "DESC_TIPO_FICHA", "class": "text-left", "width": "1%", 'orderable': false },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left", 'orderable': false},
            { "title": "HONORARIOS S/", "data": "MONTO_MENSUAL", "class": "text-center", 'orderable': false},
            { "title": "FECHA INICIO", "data": "FECHA_INICIO", "class": "text-center", "width": "1%", 'orderable': false },
            { "title": "FECHA FIN", "data": "FECHA_FIN", "class": "text-left", 'orderable': false },
            { "title": "ESTADO", "data": "DESC_SOLICITUD", "class": "text-left", 'orderable': false },
            { "title": "NOMBRE_ARCHIVO_TDR", "data": "NOMBRE_ARCHIVO_TDR", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "FLG_PROPUESTA_CONSULOR", "data": "FLG_PROPUESTA_CONSULOR", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "FLG_CUMPLE_REQUISITOS", "data": "FLG_CUMPLE_REQUISITOS", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "FLG_CALIFICA_REQUISITOS", "data": "FLG_CALIFICA_REQUISITOS", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "FLG_NOTIFICA_UTP", "data": "FLG_NOTIFICA_UTP", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "CANT_OBSERVACION", "data": "CANT_OBSERVACION", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "ID_ARCHIVO_CONTRATO", "data": "ID_ARCHIVO_CONTRATO", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "ARCHIVO_PUESTO_SUS_SOLICITUD", "data": "ARCHIVO_PUESTO_SUS_SOLICITUD", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "ARCHIVO_TDR", "data": "ARCHIVO_TDR", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "ID_TRAMITE", "data": "ID_TRAMITE", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "ID_INFORME_F", "data": "ID_INFORME_F", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "RUC_ENTIDAD", "data": "RUC_ENTIDAD", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "APELLIDO_MAT_CONSULTOR", "data": "APELLIDO_MAT_CONSULTOR", "class": "text-center", "visible": false, 'orderable': false},
            { "title": "NOMBRES_CONSULTOR", "data": "NOMBRES_CONSULTOR", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "CANT_DOC", "data": "CANT_DOC", "class": "text-center", "visible": false, 'orderable': false },
            { "title": "FLG_VERSION", "data": "FLG_VERSION", "class": "text-center", "visible": false, 'orderable': false },

        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    Ruc_Entidad = row.RUC_ENTIDAD;
                    if (row.FLG_ESTADO == "1") {
                        if (row.IDEDOCODIGO == "009") {
                            html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';
                        }
                        else {
                            if (row.ARCHIVO_TDR == '0') {
                                html = '<button  class="btn btn-xs"  onClick="fn_NuevaSolicitudEditar(' + row.ID_SOLICITUD + "," + row.FLG_VERSION+');" type="button" title="Editar Solicitud"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                                html += '<button  class="btn btn-xs"  onClick="fn_EliminarSolicitud(' + row.ID_SOLICITUD + ');" type="button" title="Eliminar Solicitud"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                            }
                            if (row.FLG_NOTIFICA_CONSULOR == '1') {
                                if (row.FLG_PROPUESTA_CONSULOR == '1') {
                                    if (row.FLG_CALIFICA_REQUISITOS == '1') {
                                        if (row.FLG_CUMPLE_REQUISITOS == '1') {
                                            html += '<button  class="btn btn-xs"  onClick="fn_NuevoCalificar(' + row.ID_SOLICITUD + ');" type="button" title="Profesional cumple con los requisitos"><i class="fas fa-user-check" style="color:#008db8;font-size:18px"></i></button>';

                                            if (row.FLG_NOTIFICA_UTP != '1') {
                                                if (row.CANT_OBSERVACION > 0) {
                                                    html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';
                                                }
                                                html += '<button  class="btn btn-xs"  onClick="fn_ver_Contrato(' + row.ID_PERSONAL + "," + row.ID_SOLICITUD + ');" type="button" title="Ver Contrato"><i class="fas fa-file-signature" style="color:#4a4a49;font-size:18px"></i></button>';
                                                html += '<button  class="btn btn-xs"  onClick="fn_AdjuntarUTPDocumentos(' + row.ID_SOLICITUD + ');" type="button" title="Adjuntar documentos para la contratación."><i class="fas fa-folder-plus" style="color:#e30613;font-size:18px"></i></button>';
                                                if (row.ID_INFORME_F > 0) {
                                                    if (row.ID_TRAMITE > 0) {
                                                        html += '<button  class="btn btn-xs"  onClick="fn_UpdateEnvio_SM_UTP(' + row.ID_SOLICITUD + ',' + row.ID_TRAMITE + ');" type="button" title="Enviar a UTP para la generación de contrato"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';

                                                    } else {
                                                            html += '<button  class="btn btn-xs"  onClick="fn_EnvioUTPDocumentos(' + row.ID_SOLICITUD + ',' + row.ID_TRAMITE + ');" type="button" title="Enviar a UTP para la generación de contrato"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';
                                                    }
                                                }
                                            

                                            }
                                        } else {
                                            html += '<button  class="btn btn-xs"  onClick="fn_NuevoCalificar(' + row.ID_SOLICITUD + ');" type="button" title="Profesional no cumple con los requisitos"><i class="fas fa-user-times" style="color:#e30613;font-size:18px"></i></button>';
                                            if (row.CANT_OBSERVACION > 0) {
                                                html += '<button  class="btn btn-xs"  onClick="fn_Verobservacion(' + row.ID_SOLICITUD + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:gray;font-size:18px"></i></button>';
                                            }
                                            html += '<button  class="btn btn-xs"  onClick="fn_NuevoSubsanacion(' + row.ID_SOLICITUD + ');" type="button" title="Enviar al profesional para que subsane los formatos"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';
                                        }

                                    } else {
                                        html += '<button  class="btn btn-xs"  onClick="fn_NuevoCalificar(' + row.ID_SOLICITUD + ');" type="button" title="Calificar al profesional"><i class="fas fa-user-edit" style="color:gray;font-size:18px"></i></button>';

                                    }
                                } else {
                                    html += '<button  class="btn btn-xs"  onClick="fn_NotificarPac(' + row.ID_SOLICITUD + ');" type="button" title="Volver a notificar usuario para propuesta"><i class="fas fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';
                                    html += '<button  class="btn btn-xs"  onClick="fn_NuevaFirmaTDR_SN(' + row.ID_SOLICITUD + ');" type="button" title="Actualizar archivo TDR"><i class="fas fa-file-upload" style="color:#6c757d;font-size:18px"></i></button>';

                                }
                            }
                            else {
                                html += '<button  class="btn btn-xs"  onClick="fn_NuevaFirmaTDR(' + row.ID_SOLICITUD + ');" type="button" title="Notificar usuario para propuesta"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';
                            }
                        }
                    
                    } 
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.IDEDOCODIGO =="007") {

                    } else {
                        html = '<button  class="btn btn-xs"  onClick="fn_MostrarFormatoPac(' + row.ID_SOLICITUD + ',1);" type="button" title="Ver TDR"><i class="far fa-file-pdf" style="color:#eb5767;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_MostrarFormatoPac(' + row.ID_SOLICITUD + ',2);" type="button" title="Ver Certificación"><i class="far fa-file-pdf" style="color:#eb5767;font-size:18px"></i></button>';

                    }
                
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.CANT_DOC > 0) {
                        html = '<button  class="btn btn-xs"  onClick="fn_VerDocumento(' + row.ID_SOLICITUD + ');" type="button" title="Archivos de sustento"><i class="fab fa-stack-overflow" style="color:#0097d9;font-size:18px;font-weight:bold"></i></button>';
                    } else {
                        if (row.ARCHIVO_PUESTO_SUS_SOLICITUD > 0) {
                            html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO_PUESTO_SUS_SOLICITUD + ',1);" type="button" title="Descargar documento sustentatorio"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                        }
                    }
                    if (row.ARCHIVO_TDR > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ARCHIVO_TDR + ');" type="button" title="Descargar TDR"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                    }
                    if (row.IDEDOCODIGO == '007') {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONTRATO + ',1);" type="button" title="Descargar Contrato"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            //{
            //    'targets': 5,
            //    'render': function (data, type, row, meta) {
            //        var html = '';
            //        html = row.FECHA_REGISTRO.substr(0, 10);
            //        return html;
            //    }
            //},
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = row.APELLIDO_PAT_CONSULTOR + ' ' + row.APELLIDO_MAT_CONSULTOR + ' ' + row.NOMBRES_CONSULTOR;
                    return html;
                }
            },
            {
                'targets': 15,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO);
                    return html;
                }
            },
            {
                'targets': 16,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaSolicitudesPac() {
    
    var item =
    {
        TIPO_PROCESO: "P",
        IDEDOCODIGO: $("#DdlEstado").val(),
        NUM_PROCESO: $("#txtTdr").val(),
        DENOMINACION_PUESTO: $("#txtDesc").val(),
        FECHA_REGISTRO: $("#FechaCreacion").val(),
    };
    var url = baseUrl + 'Coordinador/Solicitud/ListaSolicitudes';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    $('#TableSolicitudPac').DataTable().clear().draw();
    if (respuesta != null && respuesta != "") {

        $('#TableSolicitudPac').DataTable().rows.add(respuesta).draw();
    }
}
function DescargarConsultaPac() {
    $.paramcustom = {
        url: baseUrl + 'Coordinador/Solicitud/ExportarExcelSolicitudes',
        values: {
            TIPO_PROCESO: "P",
            IDEDOCODIGO: $("#DdlEstado").val(),
            NUM_PROCESO: $("#txtTdr").val(),
            DENOMINACION_PUESTO: $("#txtDesc").val(),
            FECHA_REGISTRO: $("#FechaCreacion").val(),
        }
    }
    $.redirect();
}
function fn_DescargarArchivoTDR(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=ATDR", function (responseText, textStatus, request) {
    });

}
function fn_EliminarSolicitud(id) {
    var DESIG = "0";
    var NR_RESOLUCION_ = "";
    var FECHA_RESOLUCION_ = "";
    var lista_formacion = new Array();
    var lista_conocimientos = new Array();
    var lista_cursos = new Array();
    var lista_experiencia = new Array();
    var lista_funcionalidad = new Array();
    var lista_actividad = new Array();
    var lista_otro = new Array();
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b> Eliminar</b> la solicitud.? </br >", function (r) {
        if (r) {
            var item =
            {
                ID_SOLICITUD: id,
                ID_ENTIDAD: $("#inputHddIdEntidad").val(),
                DEPENDENCIA: $("#DEPENDENCIA").val(),
                FECHA_INICIO: $("#FechaInicial").val(),
                FECHA_FIN: $("#FechaFin").val(),
                MONTO_MENSUAL: $("#MONTO_MENSUAL").val(),
                ID_PUESTO: $("#DcboPuestos").val(),
                CONFORMIDAD_SERVICIO: $("#CONFORMIDAD_SERVICIO").val(),
                APE_NOMB_CERTIFICA: $("#APE_NOMB_CERTIFICA").val(),
                NUM_DOCUMENTO_CONSULTOR: $("#NUM_DOCUMENTO_CONSULTOR").val(),
                APELLIDO_PAT_CONSULTOR: $("#APELLIDO_PAT_CONSULTOR").val(),
                APELLIDO_MAT_CONSULTOR: $("#APELLIDO_MAT_CONSULTOR").val(),
                NOMBRES_CONSULTOR: $("#NOMBRES_CONSULTOR").val(),
                CORREO_CONSULTOR: $("#CORREO_CONSULTOR").val(),
                FLG_PROCESO: "D",
                FLG_DESIGNADO: DESIG,
                NR_RESOLUCION: NR_RESOLUCION_,
                FECHA_RESOLUCION: FECHA_RESOLUCION_,
                listaFunciones: lista_funcionalidad,
                listaExperiencia: lista_experiencia,
                listaCurso: lista_cursos,
                listaAcademico: lista_formacion,
                listaConocimiento: lista_conocimientos,
                listaActividad: lista_actividad,
                listaAcividadOtro: lista_otro,
            };
            var urls = baseUrl + 'Coordinador/Solicitud/MantenimientoSolicitudPac';
            var respuesta = DESARROLLO.Ajax(urls, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaSolicitudesPac();
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
            }
        }
    });

}
function fn_MostrarFormatoPac(id, tipo) {
    switch (tipo) {
        case 1:
            window.location = baseUrl + "Coordinador/Solicitud/Ver_Formatos?ID=" + id + "&FLG_TIPO=S_T_P";
            break;
        case 2:
            window.location = baseUrl + "Coordinador/Solicitud/Ver_Formatos?ID=" + id + "&FLG_TIPO=S_C_P";
            break;
    }
}
function fn_NotificarPac(id) {
    DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b> Notificar</b> al participante.? </br >", function (r) {
        $("#myModalCargando").fadeIn(100, function () {
            fn_NotificarParticipantePac(id);
        });
    });

}
function fn_NotificarParticipantePac(id) {
        var item =
        {
            ID_SOLICITUD: id,
        };
        var urls = baseUrl + 'Coordinador/Solicitud/Enviar_Solicitud_Participante';
            var respuesta = DESARROLLO.Ajax(urls, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaSolicitudesPac();
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
                $("#myModalCargando").css("display", "none");
            }

}
function fn_DescargarArchivoSus(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AS", function (responseText, textStatus, request) {
    });

}
function fn_GenerarPeriodoRenumeracion() {
    var i = $('#FechaInicial').val();
    var f = $('#FechaFin').val();
    var Remuneracion = $('#MONTO_MENSUAL').val();
    var lista = new Array();
    var Rows = "";
    var fec_inicio = new Date(i.split('/')[2], i.split('/')[1] - 1, i.split('/')[0]);
    var fec_fin = new Date(f.split('/')[2], f.split('/')[1] - 1, f.split('/')[0]);

    if (fec_inicio > fec_fin)
        return DESARROLLO.PROCESO_ALERT("Atención", "La fecha inicial no puede ser mayor a la fecha final");

    if (fec_inicio.getFullYear() != fec_fin.getFullYear())
        return DESARROLLO.PROCESO_ALERT("Atención","El periodo inicial no puede ser diferente al periodo final");
    else
        Anio = fec_inicio.getFullYear();
    var MontInit = fec_inicio.getMonth();
    var MontFin = fec_fin.getMonth();
    var DiasMontInit = parseInt(i.split('/')[0]) - 1; // dias primer mes 
    var DiasMontFin = parseInt(f.split('/')[0]);// dias ultimo mes 

    var _disabled = "";
    if (MontInit != MontFin) {
        var idGrid = 0
        for (var i = MontInit; i <= MontFin; i++) {
            idGrid = idGrid + 1
            var _Dias = diasEnUnMes(i, Anio);
            var _Remu = Remuneracion;
            _disabled = "disabled";
            if (i == MontInit) {
                if (DiasMontInit != 0) {
                    _Dias = (_Dias - DiasMontInit);
                    _Remu = Number(((_Dias * _Remu) / 30)).toFixed(2);
                    _disabled = "";
                }
            }
            if (i == MontFin) {
                if (DiasMontFin != 30) {
                    _Dias = DiasMontFin;
                    _Remu = Number(((_Dias * _Remu) / 30)).toFixed(2);
                    _disabled = "";
                }
            }

            Rows += "<tr>"
                + "<td>" + Anio + "</td>"
                + "<td>" + (i + 1) + "</td>"
                + "<td>" + ModelMeses[i] + "</td>"
                + "<td> <input type=\"text\" " + _disabled + " value=" + Remuneracion + " /></td>"
                + "</tr>"
        }
    } else {
        if ((DiasMontFin - DiasMontInit) == 30 && MontInit !=2) {
                _disabled = "disabled";
         }
        
        Rows += "<tr>"
            + "<td>" + Anio + "</td>"
            + "<td>" + (MontInit + 1 )+ "</td>"
            + "<td>" + ModelMeses[MontInit] + "</td>"
            + "<td> <input type=\"text\"  " + _disabled + " value=" + Remuneracion + " /></td>"
            + "</tr>"
    }
    var Table = $('#TablaMeses tbody');
    Table[0].innerHTML = "";
    Table[0].innerHTML = Rows;
}
function diasEnUnMes(mes, año) {
    return new Date(año, mes + 1, 0).getDate();
}
function fn_ValidarPago() {
  var Test = false;
    var Lista = new Array();
    var Nr_Mes = 0;
    var Meses = "";
    var Monto = 0
    var Anio = 0;
    $("#TablaMeses tbody tr").each(function (index) {
        $(this).children("td").each(function (index2) {
            switch (index2) {
                case 0:
                    Anio = $(this).text();
                    break;
                case 1:
                    Nr_Mes = $(this).text();
                    break;
                case 2:
                    Meses = $(this).text();
                    break;
                case 3:
                    Monto = $(this).children('input').val();
                    break;
            }
        });

        var item = {
      
            DES_MES: Meses,
            ID_MES: Nr_Mes,
            MONTO: Monto,
            ANIO: Anio,
            ID_ENTIDAD: $("#inputHddIdEntidad").val(),
            TIPO_PROCESO: "P",
            ID_SOLICITUD: $("#ID_SOLICITUD").val(),
        };
        Lista.push(item);
    });
    var item =
    {
        Lista: Lista,
    };
    var urls = baseUrl + 'Coordinador/Solicitud/ValidarRemuneracion';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            if (respuesta.codigo == '0') {
                DESARROLLO.PROCESO_ALERT('Verificar Periodos', respuesta.message)
            } else {
                //DESARROLLO.PROCESO_CORRECTO(respuesta.message);
               Test = true;
            }
           
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
   return Test;
}
function fn_EliminarArchivoSus() {
    $("#DivMuestraAr").css("display", "none");
    $("#DivAdjuntarar").css("display", "block");
    $("#inputVerificaDelete").val("1");
}
function ValidateArchivos() {
    var result = true;
    if ($("#FLG_VERSION").val() == "1") {
        if ($("#inputVerificaDelete").val() == "1") {
            if ($('#FileArchivo')[0].files.length === 0) { $("#LblfileArchivo").text("Es necesario que adjunte el documento."); result = false; } else { $("#LblfileArchivo").text(""); }
        } else {
            if ($("#FLG_PROCESO").val() == "I") {
                if ($('#FileArchivo')[0].files.length === 0) { $("#LblfileArchivo").text("Es necesario que adjunte el documento."); result = false; } else { $("#LblfileArchivo").text(""); }
            }
        }
    }


    return result;
}
function fn_GuardarTdrFirmado(Accion) {
    var doc = document.getElementById("FileArchivo").files.length;
    var e = new FormData;
    var numid = $("#ID_SOLICITUD").val();
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("TIPO_PROCESO", "PAC");
    $.ajax({
        url: baseUrl + 'Coordinador/Solicitud/AccionUpdateArchivoTdr',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                fn_CerrarAchivoTdr();
                fn_NotificarParticipantePac(numid);
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
        }
    });

}
function fn_GuardarTdrFirmadoSN(Accion) {
    var doc = document.getElementById("FileArchivo").files.length;
    var e = new FormData;
    var numid = $("#ID_SOLICITUD").val();
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("TIPO_PROCESO", "PAC");
    $.ajax({
        url: baseUrl + 'Coordinador/Solicitud/AccionUpdateArchivoTdr',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                fn_CerrarAchivoTdr();
                CargarGrillaSolicitudesPac();
               // fn_NotificarParticipantePac(numid);
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
        }
    });

}
function fn_CerrarAchivoTdr() {
    jQuery("#modalTDR").html('');
    $('#modalTDR').modal('hide');
}
function fn_NuevoCalificar(ID_SOLICITUD) {

    jQuery("#modalEvaluarPro").html('');
    jQuery("#modalEvaluarPro").load(baseUrl + "Coordinador/ProcesoPac/NuevoCalificarPro?ID_SOLICITUD=" + ID_SOLICITUD , function (responseText, textStatus, request) {
        $("#modalEvaluarPro").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarCalificarPac() {
    jQuery("#modalEvaluarPro").html('');
    $('#modalEvaluarPro').modal('hide');
}
function fn_FormatoB(ID_PERSONAL, ID_SOLICITUD) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/Solicitud/VerFormatoSolicitud?ID_PERSONAL=" + ID_PERSONAL + "&ID_SOLICITUD=" + ID_SOLICITUD +"&TIPO=RFORMATOB", function (responseText, textStatus, request) {
    });
}
function fn_FORMATOC(ID_PERSONAL, ID_SOLICITUD) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/Solicitud/VerFormatoSolicitud?ID_PERSONAL=" + ID_PERSONAL + "&ID_SOLICITUD=" + ID_SOLICITUD +"&TIPO=RFORMATOC", function (responseText, textStatus, request) {
    });
}
function fn_FormatoCv(ID_PERSONAL, ID_SOLICITUD) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/Solicitud/VerFormatoSolicitud?ID_PERSONAL=" + ID_PERSONAL + "&ID_SOLICITUD=" + ID_SOLICITUD + "&TIPO=RFICHACVPAC_PER", function (responseText, textStatus, request) {
    });
}
function fn_VerDocumento(ID) {
    jQuery("#modalDoc").html('');
    jQuery("#modalDoc").load(baseUrl + "Coordinador/Solicitud/DocumentosSolicitud?ID_SOLICITUD=" + ID, function (responseText, textStatus, request) {
        $("#modalDoc").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
/*INICIO CORREO NOTIFICAR*/
function fn_UpdateEnvioUTP(ID_SOLICITUD, TIPO) {
    var doc = document.getElementById("FileArchivoOficio").files.length;
    var doc_ = document.getElementById("FileArchivoResumen").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoOficio").files[l], e.append("FileArchivoOficio", a);
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoResumen").files[l], e.append("FileArchivoResumen", a);

    e.append("ID_SOLICITUD", ID_SOLICITUD);
    e.append("ID_TRAMITE", ID_TRAMITE);
    e.append("NR_OFICIO", $("#txtOficio").val());
    e.append("NR_FOLIOS", $("#txtFolios").val());
    e.append("ASUNTO", $("#txtAsunto").val());
    e.append("TIPO_PROCESO", $("#TIPO_PROCESO").val());
    e.append("NR_TRAMITE", $("#txtNTramite").val());
    e.append("FLG_CON_HR", $("#Ddlflgtramite").val());
    $.ajax({
        url: baseUrl + 'Coordinador/ProcesoPac/UpdNotificaUTP',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                jQuery("#modalEnvioUtp").html('');
                $('#modalEnvioUtp').modal('hide');
                CargarGrillaSolicitudesPac();
                if (p.message2 == "0") { DESARROLLO.PROCESO_ALERT("NUMERO DE TRÁMITE", p.message2); }
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
    //item = {
    //    ID_SOLICITUD: ID_SOLICITUD,
    //    ACCION: TIPO,
    //};
    //var url = baseUrl + 'Coordinador/ProcesoPac/UpdNotificaUTP';
    //var respuesta = DESARROLLO.Ajax(url, item, false);
    //if (respuesta != null && respuesta != "") {
    //    if (respuesta.success) {
    //        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
    //        jQuery("#modalEnvioUtp").html('');
    //        $('#modalEnvioUtp').modal('hide');
    //        CargarGrillaSolicitudesPac();
    //        if (respuesta.message2 != "0") {
    //            DESARROLLO.PROCESO_ALERT("NUMERO DE TRÁMITE", respuesta.message2);
    //        }
         
    //    } else {
    //        DESARROLLO.PROCESO_ERROR(respuesta.extra);
    //    }
    //    $("#myModalCargando").css("display", "none");
    //}
}
function fn_UpdateEnvio_SM_UTP(ID_SOLICITUD, ID_TRAMITE) {
    text = "</br>¿Está seguro que desea <b>enviar</b> a la oficina de UTP del Ministerio economía y finanzas, recuerde que todos documentos observados deben estar correctamente subsanados.? </br>";
    DESARROLLO.PROCESO_CONFIRM(text, function (r) {
        if (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_Remitente_UpdateEnvio_SM_UTP(ID_SOLICITUD, ID_TRAMITE);
            });

        }
    });

}
function fn_Remitente_UpdateEnvio_SM_UTP(ID_SOLICITUD, ID_TRAMITE) {
    var e = new FormData;
    e.append("ID_SOLICITUD", ID_SOLICITUD);
    e.append("ID_TRAMITE", ID_TRAMITE);
    e.append("NR_OFICIO", $("#txtOficio").val());
    e.append("NR_FOLIOS", $("#txtFolios").val());
    e.append("ASUNTO", $("#txtAsunto").val());
    $.ajax({
        url: baseUrl + 'Coordinador/ProcesoPac/UpdNotificaUTP',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                CargarGrillaSolicitudesPac();
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
/*FIN CORREO NOTIFICAR*/
function fn_NuevoSubsanacion(ID_SOLICITUD) {

    jQuery("#modalSubsanacion").html('');
    jQuery("#modalSubsanacion").load(baseUrl + "Coordinador/Solicitud/ObservacionProfesional?ID_SOLICITUD=" + ID_SOLICITUD, function (responseText, textStatus, request) {
        $("#modalSubsanacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_Verobservacion(ID) {
    jQuery("#modalObservacion").html('');
    jQuery("#modalObservacion").load(baseUrl + "Coordinador/Solicitud/VerObservacionProfesional?ID_SOLICITUD=" + ID +"&TIPO=" + $("#DdlEstado").val() , function (responseText, textStatus, request) {
        $("#modalObservacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_InsReevaluacion() {
    item = {
        ID_SOLICITUD: $("#ID_SOLICITUD").val(),
        TIPO: "C",
        DES_REEVALUACION: $("#TxtObservaciones").val(),
    };
    var url = baseUrl + 'Coordinador/Solicitud/InsReevaluacion';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            $("#myModalCargando").css("display", "none");
            jQuery("#modalSubsanacion").html('');
            $('#modalSubsanacion').modal('hide');
            CargarGrillaSolicitudesPac();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
            $("#myModalCargando").css("display", "none");
        }

    }
}
function fn_AdjuntarUTPDocumentos(ID) {
    jQuery("#modalEnvioUtp").html('');
    jQuery("#modalEnvioUtp").load(baseUrl + "Coordinador/Solicitud/EnvioUtpDocumentos?ID_SOLICITUD=" + ID + "&TIPO=P", function (responseText, textStatus, request) {
        $("#modalEnvioUtp").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });

}
function fn_EnvioUTPDocumentos(ID, ESTADO) {
    if (Ruc_Entidad > 0) {
        jQuery("#modalEnvioUtp").html('');
        jQuery("#modalEnvioUtp").load(baseUrl + "Coordinador/ProcesoFag/Ver_Integrar_STD?ID_SOLICITUD=" + ID, function (responseText, textStatus, request) {
            $("#modalEnvioUtp").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
    } else {
        DESARROLLO.PROCESO_ALERT("Alerta", "Para realizar él envió a UTP debe actualizar los datos de la entidad");
    }
}
function fn_ver_Contrato(ID_PERSONAL, ID_SOLICITUD) {
    window.location = baseUrl + "Coordinador/ProcesoFag/Ver_Contrato?ID_PERSONAL=" + ID_PERSONAL + "&ID_SOLICITUD=" + ID_SOLICITUD + "&FLG_TIPO=CONTRATO_PAC";
    //jQuery("#modalVerContrato").html('');
    //jQuery("#modalVerContrato").load(baseUrl + "Coordinador/ProcesoFag/Ver_Contrato?ID_PERSONAL=" + ID_PERSONAL + "&ID_SOLICITUD=" + ID_SOLICITUD + "&FLG_TIPO=CONTRATO_PAC", function (responseText, textStatus, request) {
    //    $("#modalVerContrato").modal('show');
    //});
}
function fn_Archivossustentos() {
    var e = new FormData;
    var AIF = document.getElementById("FileArchivoIF").files.length;
    var ADS = document.getElementById("FileArchivoDS").files.length;
    var AA = document.getElementById("FileArchivoFORMATOA").files.length;
    var AB = document.getElementById("FileArchivoFORMATOB").files.length;
    var AC = document.getElementById("FileArchivoFORMATOC").files.length;
    var AD = document.getElementById("FileArchivoFORMATOD").files.length;
    var AE = document.getElementById("FileArchivoFORMATOE").files.length;
    var AHI = document.getElementById("FileArchivoFORMATOHI").files.length;
    var ACV = document.getElementById("FileArchivoFORMATOCV").files.length;


    if ($('#ID_INFORME_F').val() > 0 ) {
        if ($('#ID_H_PROFESIONAL').val() > 0) {
            if ($('#FileArchivoFORMATORH')[0].files.length > 0) {
                var ARH = document.getElementById("FileArchivoFORMATORH").files.length;
                if (ARH > 0) for (var a, l = 0; ARH > l; l++) a = document.getElementById("FileArchivoFORMATORH").files[l], e.append("FileArchivoFORMATORH", a);
            }
        }

        if ($('#ID_OTROS').val() > 0) {
            if ($('#FileArchivoOtros')[0].files.length > 0) {
                var AO = document.getElementById("FileArchivoOtros").files.length;
                if (AO > 0) for (var a, l = 0; AO > l; l++) a = document.getElementById("FileArchivoOtros").files[l], e.append("FileArchivoOtros", a);
            }
        }

    }
    else {
        if ($('#FileArchivoFORMATORH')[0].files.length > 0) {
            var ARH = document.getElementById("FileArchivoFORMATORH").files.length;
            if (ARH > 0) for (var a, l = 0; ARH > l; l++) a = document.getElementById("FileArchivoFORMATORH").files[l], e.append("FileArchivoFORMATORH", a);
        }
        if ($('#FileArchivoOtros')[0].files.length > 0) {
            var AO = document.getElementById("FileArchivoOtros").files.length;
            if (AO > 0) for (var a, l = 0; AO > l; l++) a = document.getElementById("FileArchivoOtros").files[l], e.append("FileArchivoOtros", a);
        }

    }




  


    if (AIF > 0) for (var a, l = 0; AIF > l; l++) a = document.getElementById("FileArchivoIF").files[l], e.append("FileArchivoIF", a);
    if (ADS > 0) for (var a, l = 0; ADS > l; l++) a = document.getElementById("FileArchivoDS").files[l], e.append("FileArchivoDS", a);
    if (AA > 0) for (var a, l = 0; AA > l; l++) a = document.getElementById("FileArchivoFORMATOA").files[l], e.append("FileArchivoFORMATOA", a);
    if (AB > 0) for (var a, l = 0; AB > l; l++) a = document.getElementById("FileArchivoFORMATOB").files[l], e.append("FileArchivoFORMATOB", a);
    if (AC > 0) for (var a, l = 0; AC > l; l++) a = document.getElementById("FileArchivoFORMATOC").files[l], e.append("FileArchivoFORMATOC", a);
    if (AD > 0) for (var a, l = 0; AD > l; l++) a = document.getElementById("FileArchivoFORMATOD").files[l], e.append("FileArchivoFORMATOD", a);
    if (AE > 0) for (var a, l = 0; AE > l; l++) a = document.getElementById("FileArchivoFORMATOE").files[l], e.append("FileArchivoFORMATOE", a);
    if (AHI > 0) for (var a, l = 0; AHI > l; l++) a = document.getElementById("FileArchivoFORMATOHI").files[l], e.append("FileArchivoFORMATOHI", a);
    if (ACV > 0) for (var a, l = 0; ACV > l; l++) a = document.getElementById("FileArchivoFORMATOCV").files[l], e.append("FileArchivoFORMATOCV", a);



    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/UpdArchivosPAC",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                DESARROLLO.PROCESO_CORRECTO(p.message);
                $("#myModalCargando").css("display", "none");
                jQuery("#modalEnvioUtp").html('');
                $('#modalEnvioUtp').modal('hide');
                CargarGrillaSolicitudesPac();
            }
            else {
                $("#myModalCargando").css("display", "none");
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
function fn_Cerrarobservacion() {
    jQuery("#modalObservacion").html('');
    $('#modalObservacion').modal('hide');
}
function fn_CerrarEnvioUTPDocumentos() {
    jQuery("#modalEnvioUtp").html('');
    $('#modalEnvioUtp').modal('hide');
}
/*INICIO PRINCIPAL*/
jQuery("#btnConsultarPac").click(function (e) {
    CargarGrillaSolicitudesPac();
});
jQuery("#btnConsultarPuesto").click(function (e) {
    CargarGrillaPuestoPac();
});
jQuery("#btnDescargaPac").click(function (e) {
    DescargarConsultaPac();
});
/*FIN PRINCIPAL*/
/*INICIO MODAL*/
$("#FLG_CHECK_DESIGNADO").change(function () {
    Validar_Designado();
});
$("#DcboPuestos").change(function (e) {
    fn_ValidDetalleMonto($("#DcboPuestos").val());
});
$("#frmNuevaSolicitud").validate({
    rules: {

        "OFICINA_CERTIFICA": {
            required: true,
        },
        "DEPENDENCIA": {
            required: true,
        },
        "FechaInicial": {
            required: true,
        },
        "FechaFin": {
            required: true,
        },
        "MONTO_MENSUAL": {
            required: true,
            min: 1,
        },
        "DcboPuestos": {
            required: true,
            min: 1,
        },
        "CONFORMIDAD_SERVICIO": {
            required: true,
        },
        "APE_NOMB_CERTIFICA": {
            required: true,
        },
        "NUM_DOCUMENTO_CONSULTOR": {
            required: true,
            minlength: 8,
            maxlength: 12,
        },

        "APELLIDO_PAT_CONSULTOR": {
            required: true,
        },
        "APELLIDO_MAT_CONSULTOR": {
            required: true,
        },
        "NOMBRES_CONSULTOR": {
            required: true,
        },
        "CORREO_CONSULTOR": {
            required: true,
            email: true,
        },
        "NR_RESOLUCION": {
            required: true
        },
        "FechaAsignacion": {
            required: true,
        },
    },
    errorElement: "input",
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }
});
jQuery("#btnGrabarSolicitud").click(function (e) {
    var text = $("#ID_SOLICITUD").val() == 0 ? "¿Desea <b> Registrar</b> la solicitud.? </br >" : "</br>¿Desea <b> Actualizar</b> la solicitud.?";
    var text_ = $("#ID_SOLICITUD").val() == 0 ? "Para proceder con el  <b> Registrar</b>  debe generar el periodo de remuneración. </br >" : "</br>Para proceder con el  <b> Actualizar</b>  debe generar el periodo de remuneración.";
    var text__ = $("#ID_SOLICITUD").val() == 0 ? "Para proceder con el  <b> Registro</b>  debe registrar como mínimo un archivo de sustento. </br >" : "</br>Para proceder con la  <b> Actualización</b>  debe registrar como mínimo un archivo de sustento.";

    if ($("#frmNuevaSolicitud").valid()) {
        if (ValidateArchivos()) {
            var cant = $('#TablaMeses >tbody >tr').length;
            var cant_ = $('#Tabla_Documento >tbody >tr').length;
            if ($("#FLG_VERSION").val() == "2") {
                if (DESARROLLO.V_EMAIL("CORREO_CONSULTOR")){
                    if (cant_ > 0) {
                        if (cant > 0) {
                            if (fn_ValidarPago()) {
                                DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                                    if (r) {
                                        $("#myModalCargando").fadeIn(100, function () {
                                            fn_MantenimientoSolicitud();
                                        });

                                    }
                                });
                            }
                        } else {
                            DESARROLLO.PROCESO_ALERT("Atención", text_);
                        }
                    } else {
                        DESARROLLO.PROCESO_ALERT("Atención", text__);
                    }
                } else {
                    DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente el correo del consultor.");
                }

                
            } else {
                if (DESARROLLO.V_EMAIL("CORREO_CONSULTOR")) {
                    if (cant > 0) {
                        if (fn_ValidarPago()) {
                            DESARROLLO.PROCESO_CONFIRM(text, function (r) {
                                if (r) {
                                    $("#myModalCargando").fadeIn(100, function () {
                                        fn_MantenimientoSolicitud();
                                    });

                                }
                            });
                        }
                    } else {
                        DESARROLLO.PROCESO_ALERT("Atención", text_)
                    }
                } else {
                    DESARROLLO.PROCESO_ALERT("Atención", "Ingrese correctamente el correo del consultor.");
                }
            }
          
        }
    }
});
$("#FileArchivo").change(function (e) {
    var val = $(this).val();
    var input = document.getElementById('FileArchivo');
    var file = input.files[0];
    var filename = e.target.files[0].name;
    if (file.size > 11534300) {
        $(this).val('');
        DESARROLLO.PROCESO_ALERT('Alerta', "El archivo que está subiendo pesa más de 10Mb");
        return false
    }
    switch (val.substring(val.lastIndexOf('.') + 1).toLowerCase()) {
        case 'pdf':
            if (DESARROLLO.VALIDAR_CONTEN_PDF("FileArchivo")) {
                $('#archivo_adjuntado').text(filename);
                return true;
            }
            break;
        default:
            $(this).val('');
            DESARROLLO.PROCESO_ALERT('Alerta', "formato de archivo no válido, solo se permite archivos .pdf");
            break;
    }
});
$("#FileArchivoSust").change(function (e) {
    var val = $(this).val();
    var input = document.getElementById('FileArchivoSust');
    var file = input.files[0];
    var filename = e.target.files[0].name;
    if (file.size > 11534300) {
        $(this).val('');
        DESARROLLO.PROCESO_ALERT('Alerta', "El archivo que está subiendo pesa más de 10Mb");
        return false
    }
    switch (val.substring(val.lastIndexOf('.') + 1).toLowerCase()) {
        case 'pdf':
            if (DESARROLLO.VALIDAR_CONTEN_PDF("FileArchivoSust")) {
                $('#archivo_sust').text(filename);
                return true;
            }
            break;
        default:
            $(this).val('');
            DESARROLLO.PROCESO_ALERT('Alerta', "formato de archivo no válido, solo se permite archivos .pdf");
            break;
    }
});
/*FIN MODAL*/
/*INICIO REGISTRO ARCHIVO MULTIPLE*/
function ValidArchivo() {
    var result = true;
    if ($('#FileArchivoSust')[0].files.length === 0) { $("#LblfileArchivoSust").text("Es necesario que adjunte el documento de sustento."); result = false; } else { $("#LblfileArchivoSust").text(""); }
    if ($('#txtDesArchivo').val().trim().length == 0) {
        $('#txtDesArchivo').val("").focus();
        $("#txtDesArchivo").addClass('is-invalid');
        result = false;
    } else {
        $("#txtDesArchivo").removeClass('is-invalid');
    }
    return result;
}
function fn_Aregar_Archivo() {
    if (ValidArchivo()) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea registrar el archivo de sustento.?", function (r) {
            if (r) {
                var doc = document.getElementById("FileArchivoSust").files.length;
                var e = new FormData;
                if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoSust").files[l], e.append("FileArchivoSust", a);
                var items = GetData();
                e.append("RUTA_ARCHIVO", "0");
                e.append("ID_DOCUMENTO", 0);
                e.append("FLG_TIPO", "S");
                e.append("ID_PROCESO", $('#ID_SOLICITUD').val());
                e.append("DES_DOCUMENTO", $('#txtDesArchivo').val());
                e.append("Items", JSON.stringify(items));
                $.ajax({
                    url: baseUrl + "Coordinador/Solicitud/GenerarHTML_Multiple",
                    type: "POST",
                    data: e,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    contentType: !1,
                    processData: !1,
                    success: function (p) {
                        if (p.success) {
                            $("#FileArchivoSust").val("");
                            $("#archivo_sust").text("0 Archivos");
                            $("#txtDesArchivo").val("");
                            $("#gridBody").html(p.extra);
                        }
                        else {
                            DESARROLLO.PROCESO_ERROR(p.message);
                        }
                    }
                });
            }
        })
    }
}
function GetData() {
    var dataRequest = new Array();
    $("#frmNuevaSolicitud").find("#Tabla_Documento" + " tbody > tr").each(function (index, item) {
        var entorno = $(this);
        var ID_DOCUMENTO = entorno.find(".tdID_DOCUMENTO", "id").html();
        var ID_PROCESO = entorno.find(".tdID_PROCESO", "id").html();
        var FLG_TIPO = entorno.find(".tdFLG_TIPO", "id").html();
        var ID_LF = entorno.find(".tdID_LF", "id").html();
        var NOM_DOCUMENTO = entorno.find(".tdNOM_DOCUMENTO", "id").html();
        var DES_DOCUMENTO = entorno.find("input.tdDES_DOCUMENTO", "id").val();
        dataRequest.push({
            ID_DOCUMENTO: ID_DOCUMENTO,
            ID_PROCESO: ID_PROCESO,
            FLG_TIPO: FLG_TIPO,
            ID_LF: ID_LF,
            NOM_DOCUMENTO: NOM_DOCUMENTO,
            DES_DOCUMENTO: DES_DOCUMENTO,
        });
    });
    return dataRequest;
}
function fn_MostrarArchivoInicial() {
    var e = new FormData;
    e.append("ID_DOCUMENTO", 1);
    e.append("FLG_TIPO", "S");
    e.append("ID_PROCESO", $('#ID_SOLICITUD').val());
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/GenerarHTML_Archivo",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                $("#gridBody").html(p.extra);
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.message);
            }
        }
    });
}
function fn_MostrarDocumentoInicial() {
    var e = new FormData;
    e.append("ID_DOCUMENTO", 2);
    e.append("FLG_TIPO", "S");
    e.append("ID_PROCESO", $('#ID_SOLICITUD').val());
    $.ajax({
        url: baseUrl + "Coordinador/Solicitud/GenerarHTML_Archivo",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                $("#gridBody").html(p.extra);
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.message);
            }
        }
    });
}
/*FIN REGISTRO ARCHIVO MULTIPLE*/