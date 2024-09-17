var TableAltasBajas_ = null;
var TablePeriodoEntidad_ = null;

function CrearGrillaAltasBajas() {
    DESARROLLO.configurarGrilla();
    TableAltasBajas_ = $("#TableAltasBajas").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "1%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "N° DE SOLICITUD", "data": "NR_SOLICITUD_SIGLA", "class": "text-left", },
            { "title": "DNI / CE", "data": "NUM_DOCUMENTO", "class": "text-left", },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left", },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-left", },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left", },
            { "title": "VIGENCIA DE CONTRATO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "N° DE ADENDA ", "data": "COD_ADENDA", "class": "text-center", },
            { "title": "VIGENCIA DE ADENDA", "data": "", "class": "text-center", "width": "1%" },
            { "title": "HONORARIOS S/", "data": "MONTO_MENSUAL", "class": "text-center", },
            { "title": "FECHA DE TÉRMINO </br> DE CONTRATO", "data": "FECHA_BAJA", "class": "text-center", },
            { "title": "ESTADO", "data": "IDEDOCODIGO", "class": "text-left", },
            { "title": "ANIO", "data": "ANIO", "class": "text-center", "visible": false, },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "visible": false, },
            { "title": "SOLICITUD", "data": "FECHA_ENVIO_UTP", "class": "text-left", "visible": false,  },
            { "title": "TIPO", "data": "TIPO", "class": "text-center", "visible": false, },
            { "title": "CANT_PAGOS", "data": "CANT_PAGOS", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_A_B", "data": "ID_ARCHIVO_A_B", "class": "text-center", "visible": false, },
            { "title": "ANIO_FIN", "data": "ANIO_FIN", "class": "text-center", "visible": false, },
            { "title": "MES_FIN", "data": "MES_FIN", "class": "text-center", "visible": false, },
            { "title": "INICIO C.", "data": "FECHA_INICIO", "class": "text-center", "visible": false,},
            { "title": "FIN C.", "data": "FECHA_FIN", "class": "text-center", "visible": false,},

        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.IDEDOCODIGO == "A") {
                        if (row.TIPO == "C") {
                            if (row.CANT_PAGOS > 0) {
                                html = '<button  class="btn btn-xs"  onClick="fn_Observacion_C_B(' + row.ID_CONTRATO + ','+row.NUM_DOCUMENTO+ ');" type="button" title="Dar de baja a consultor"><i class="fas fa-user-minus" style="color:#e30613;font-size:18px"></i></button>';

                            }
                            else {
                                html = '<button  class="btn btn-xs"  onClick="fn_Observacion_C_A(' + row.ID_CONTRATO + ',' + row.NUM_DOCUMENTO +  ');" type="button" title="Anular Contrato"><i class="fas fa-user-slash" style="color:#e30613;font-size:18px"></i></button>';

                            }
                        } else {
                            if (row.CANT_PAGOS > 0) {
                                html = '<button  class="btn btn-xs"  onClick="fn_Observacion_A_B(' + row.ID_CONTRATO + ',' + row.NUM_DOCUMENTO +  ');" type="button" title="Dar de baja a consultor"><i class="fas fa-user-minus" style="color:#e30613;font-size:18px"></i></button>';

                            } else {
                                html = '<button  class="btn btn-xs"  onClick="fn_Observacion_A_A(' + row.ID_CONTRATO + ',' + row.NUM_DOCUMENTO +  ');" type="button" title="Anular Contrato"><i class="fas fa-user-slash" style="color:#e30613;font-size:18px"></i></button>';

                            }
                        }
                    } else {
                        html = '<button  class="btn btn-xs"  type="button" title="Proceso culminado"><i class="fas fa-user-alt-slash" style="color:#e30613;font-size:18px"></i></button>';
                    }

                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.ID_ARCHIVO_A_B > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_A_B + ');" type="button" title="Descargar documento de sustento"><i class="fas fa-download" style="color:#008db8;font-size:18px"></i></button>';

                    }

                    return html;
                }
            },
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (row.TIPO=="C") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO) + ' - ' + DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);
                    }
                    return html;
                }
            },
            {
                'targets': 11,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (row.TIPO == "A") {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INICIO) + ' - ' + DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);
                    }
                    return html;
                }
            },
            {
                "targets": [12],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                'targets': 13,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (DESARROLLO.FECHA_DATATABLE(row.FECHA_BAJA) == "31/12/0") {
                        html = '-';
                    } else {
                        html = DESARROLLO.FECHA_DATATABLE(row.FECHA_BAJA);
                    }

                    return html;
                }
            },
            {
                'targets': 14,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.IDEDOCODIGO == "A") {
                        html = 'ALTA';
                    } else {
                        html = 'BAJA';
                    }

                    return html;
                }
            },

        ],
        "rowCallback": function (row, data) {
            var date = new Date();
            var anio = date.getFullYear();
            var mes = (date.getMonth() + 1);
            var anio_ = data.ANIO_FIN;
            var mes_ = data.MES_FIN;
            if (data.IDEDOCODIGO == "A") {
          if (anio == anio_ && mes == mes_) {
              $(row).css('background-color', '#EFBFB5')
          }
            }
        
        },
    });
}
function CargarGrillaAltasBajas() {
    var item =
    {
        TIPO_PROCESO: $('#DdlProceso').val(),
        IDEDOCODIGO: $('#DdlEstado').val(),
        NUM_DOCUMENTO: $('#txtDni').val(),
        CONSULTOR: $('#txtApeNo').val(),
        ANIO: $('#txtAnio').val(),
        FECHA_ENVIO_UTP: $('#FechaSolicitud').val(),
    };
    var url = baseUrl + 'Coordinador/AltasBajas/ListaContraAdendaPersona';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableAltasBajas_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableAltasBajas_.rows.add(respuesta).draw();
    }
}
function fn_UpdateAltasBajas() {
    var doc = document.getElementById("FileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);

    e.append("ID_CONTRATO", $("#ID_CONTRATO").val());
    e.append("FECHA_BAJA", $("#FechaBaja").val());
    e.append("TIPO", $("#TIPO").val());
    e.append("DESCRIPCION", $("#TxtObservaciones").val());
    e.append("NUM_DOCUMENTO", $("#NUM_DOCUMENTO").val());
    
    $.ajax({
        url: baseUrl + "Coordinador/AltasBajas/UpdateBajaAnulacion",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarAnulacion();
                CargarGrillaAltasBajas();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function fn_CerrarAnulacion() {
    jQuery("#modalAnulacion").html('');
    $('#modalAnulacion').modal('hide');
}
function fn_Observacion_C_B(id,NUM_DOCUMENTO ) {
    jQuery("#modalAnulacion").html('');
    jQuery("#modalAnulacion").load(baseUrl + "Coordinador/AltasBajas/ObservacionAltaBaja?ID_CONTRATO=" + id + "&TIPO=C_B" + "&NUM_DOCUMENTO=" + NUM_DOCUMENTO, function (responseText, textStatus, request) {
        $("#modalAnulacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_Observacion_C_A(id, NUM_DOCUMENTO) {
    jQuery("#modalAnulacion").html('');
    jQuery("#modalAnulacion").load(baseUrl + "Coordinador/AltasBajas/ObservacionAltaBaja?ID_CONTRATO=" + id + "&TIPO=C_A" + "&NUM_DOCUMENTO=" + NUM_DOCUMENTO, function (responseText, textStatus, request) {
        $("#modalAnulacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_Observacion_A_B(id, NUM_DOCUMENTO) {
    jQuery("#modalAnulacion").html('');
    jQuery("#modalAnulacion").load(baseUrl + "Coordinador/AltasBajas/ObservacionAltaBaja?ID_CONTRATO=" + id + "&TIPO=A_B" + "&NUM_DOCUMENTO=" + NUM_DOCUMENTO, function (responseText, textStatus, request) {
        $("#modalAnulacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_Observacion_A_A(id, NUM_DOCUMENTO) {
    jQuery("#modalAnulacion").html('');
    jQuery("#modalAnulacion").load(baseUrl + "Coordinador/AltasBajas/ObservacionAltaBaja?ID_CONTRATO=" + id + "&TIPO=A_A" + "&NUM_DOCUMENTO=" + NUM_DOCUMENTO, function (responseText, textStatus, request) {
        $("#modalAnulacion").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}
function CrearGrillaPeriodoEntidad() {
    DESARROLLO.configurarGrilla();
    TablePeriodoEntidad_ = $("#TablePeriodoEntidad").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,

        columns: [
            { "title": "AÑO", "data": "ANIO_PERIODO", "class": "text-center", },
            { "title": "MES", "data": "DES_MES", "class": "text-center", },
            { "title": "ASIGNACIÓN</br>MENSUAL", "data": "MONTO", "class": "text-center", },
            { "title": "MARCO</br>NORMATIVO", "data": "DESC_PROCESO", "class": "text-center", },
            { "title": "TOTAL</br>HONORARIOS ", "data": "MONTO_CONSUMIDO", "class": "text-center", },
            { "title": "CANTIDAD DE</br>CONSULTORES", "data": "CANTIDAD_CONSULTORES", "class": "text-center", },
            { "title": "SALDO", "data": "MONTO_DISPONIBLE", "class": "text-center", },
        ],
        "columnDefs": [
            {
                "targets": [2],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                "targets": [4],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                "targets": [6],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
        ],
    });
}
function CargarGrillaPeriodoEntidad() {
    var item =
    {
        TIPO_PROCESO: $('#DdlTipo').val(),
        NUM_MES: $('#DdlMes').val(),
        ANIO_PERIODO: $('#txtAnio_').val(),
    };
    var url = baseUrl + 'Coordinador/AltasBajas/ListaPeriodoPagoEntidad';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TablePeriodoEntidad_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TablePeriodoEntidad_.rows.add(respuesta).draw();
    }
}