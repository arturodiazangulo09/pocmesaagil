var TableSolicitudAdenda_ = null;
var TableSolicitudAdendaModal_ = null;

var ModelMeses = ["ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO",
    "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"];
function fn_Adenda(id) {
    jQuery("#modalAdenda").html('');
    jQuery("#modalAdenda").load(baseUrl + "Coordinador/Adenda/NuevoEditAdendaSolicitud?ID_CONTRATO_DET=" + id, function (responseText, textStatus, request) {
        $("#modalAdenda").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarEditarSolicitudAdenda() {
    jQuery("#modalAdenda").html('');
    $('#modalAdenda').modal('hide');
}
function callContratos(TipoProceso) {
    var ID_CONTRATO = $("#ID_CONTRATO").val();
    $.getJSON(baseUrl + "Coordinador/Adenda/ListarContratoCbo",
        {
            TIPO_PROCESO: TipoProceso,
            ID_ENTIDAD: $("#inputHddIdEntidad").val()
        }, function (e) {
            $("#COD_CONTRATO").empty();
            $.each(e, function (e, o) {
                $("#COD_CONTRATO").append($("<option></option>").val(o.ID_CONTRATO).html(o.CODIGO_CONTRATO));
                if (ID_CONTRATO >0) {
                    $("#COD_CONTRATO").val(ID_CONTRATO);
                } else {
                    fn_ValidDetalleContrato(0);
                }
            })
    });

}
function fn_ValidDetalleContrato(ID_CONTRATO) {
    $.ajax({
        url: baseUrl + "Coordinador/Adenda/ListaContratoDetalle?ID_CONTRATO=" + ID_CONTRATO,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (p) {
            
            if (ID_CONTRATO >0) {
                $("#txtApellidoNombres").val(p.CONSULTOR);
                $("#txtPuesto").val(p.DENOMINACION_PUESTO);
                $("#txtRenumeracion").val("S/" + parseFloat(p.MONTO_MENSUAL).toFixed(2));
                $("#txtFechaI").val(p.FECHA_INICIO);
                $("#txtFechaF").val(p.FECHA_FIN);
                $("#inputMontoMensual").val(p.MONTO_MENSUAL);
                $("#inputIdSolicitud").val(p.ID_SOLICITUD);
                
                
            }
            else {
                $("#txtApellidoNombres").val("");
                $("#txtPuesto").val("");
                $("#txtRenumeracion").val("");
                $("#txtFechaI").val("");
                $("#txtFechaF").val("");
                $("#inputMontoMensual").val(0);
                $("#inputIdSolicitud").val(0);
            }
        }
    });
}
function fn_GenerarPeriodoRenumeracion() {
    var i = $('#txtFechaIAdenda').val();
    var f = $('#txtFechaFAdenda').val();
    var Remuneracion = $('#inputMontoMensual').val();
    var lista = new Array();
    var Rows = "";
    var fec_inicio = new Date(i.split('/')[2], i.split('/')[1] - 1, i.split('/')[0]);
    var fec_fin = new Date(f.split('/')[2], f.split('/')[1] - 1, f.split('/')[0]);

    if (fec_inicio > fec_fin)
        return DESARROLLO.PROCESO_ALERT("Atención", "La fecha inicial no puede ser mayor a la fecha final");

    if (fec_inicio.getFullYear() != fec_fin.getFullYear())
        return DESARROLLO.PROCESO_ALERT("Atención", "El periodo inicial no puede ser diferente al periodo final");
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
            //rf_103 KCR
            var tipo_proceso = $("#DdlTipoProceso").val();
            if (tipo_proceso == 'P') {
                _disabled = "disabled";
            }
            Rows += "<tr>"
                + "<td>" + Anio + "</td>"
                + "<td>" + (i + 1) + "</td>"
                + "<td>" + ModelMeses[i] + "</td>"
                + "<td> <input type=\"text\" " + _disabled + " value=" + Remuneracion + " /></td>"
                + "</tr>"
        }
    } else {
        if ((DiasMontFin - DiasMontInit) == 30 && MontInit != 2) {
            _disabled = "disabled";
        }
        Rows += "<tr>"
            + "<td>" + Anio + "</td>"
            + "<td>" + (MontInit + 1) + "</td>"
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
function fn_verifica() {
    var result = true;
    if ($("#txtFechaIAdenda").val() == "") {
        $("#txtFechaIAdenda").addClass('is-invalid'); result = false;
    } else { $("#txtFechaIAdenda").removeClass('is-invalid'); }

    if ($("#txtFechaFAdenda").val() == "") {
        $("#txtFechaFAdenda").addClass('is-invalid'); result = false;
    } else { $("#txtFechaFAdenda").removeClass('is-invalid'); }

    if ($("#inputVerificaDelete").val() == "1") {
        if ($('#FileArchivo')[0].files.length === 0) { $("#LblfileArchivo").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivo").text(""); }
    }
    else {
        if ($("#ID_CONTRATO_DET").val() == 0) {
            if ($('#FileArchivo')[0].files.length === 0) { $("#LblfileArchivo").text("Es necesario que adjunte el archivo."); result = false; } else { $("#LblfileArchivo").text(""); }
        }
    }
    return result;
}
function ValidateArchivos() {
    var result = true;
    if ($("#inputVerificaDelete").val() == "1") {
        if ($('#FileArchivo')[0].files.length === 0) { $("#LblfileArchivo").text("Es necesario que adjunte el documento."); result = false; } else { $("#LblfileArchivo").text(""); }
    } else {
        if ($("#ID_CONTRATO_DET").val() == "0") {
            if ($('#FileArchivo')[0].files.length === 0) { $("#LblfileArchivo").text("Es necesario que adjunte el documento."); result = false; } else { $("#LblfileArchivo").text(""); }

        }
    }
    return result;
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
            ID_ENTIDAD: $("#DcboEntidades").val(),
            TIPO_PROCESO: $("#DdlTipoProceso").val(),
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
                Test = true;
            }

        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
    return Test;
}
function Valid_Fechas() {
    
    var inicio_f = $("#txtFechaF").val();
    var ini = $("#txtFechaIAdenda").val();
    var fini = $("#txtFechaFAdenda").val();
    if (ini != '') {
        if ($("#ID_CONTRATO_DET").val() > 0 || $("#inputIdSolicitud").val() > 0) {
            if (Valid_Fechas_Contrato(inicio_f, ini)) {
                if (ini != '' && fini != '') {
                    if (DESARROLLO.VALIDAR_FECHAS_I_F(ini, fini) == false) {
                        $("#txtFechaIAdenda").val('');
                        $("#txtFechaFAdenda").val('');
                    }
                }
            } else {
                DESARROLLO.PROCESO_ALERT('Alerta', 'La fecha de inicio debe debe ser mayor a la finalización de contrato')
                $("#txtFechaIAdenda").val('');
                $("#txtFechaFAdenda").val('');
            }
        }
        else {
            DESARROLLO.PROCESO_ALERT('Alerta', 'Seleccione un contrato para realizar el nuevo periodo')
            $("#txtFechaIAdenda").val('');
            $("#txtFechaFAdenda").val('');
        }

    }

}
function Valid_Fechas_Contrato(fechaInicial, fechaFinal) {
    var strFechainicial = fechaInicial;
    var strFechafinal = fechaFinal;
    valuesStart = strFechainicial.split("/");
    valuesEnd = strFechafinal.split("/");
    // Verificamos que la fecha no sea posterior a la actual
    var dateStart = new Date(valuesStart[2], (valuesStart[1] - 1), valuesStart[0]);
    var dateEnd = new Date(valuesEnd[2], (valuesEnd[1] - 1), valuesEnd[0]);
    if (dateStart > dateEnd) {
        DESARROLLO.PROCESO_ALERT('Alerta', 'Seleccione un contrato para realizar el nuevo periodo')
        $("#txtFechaIAdenda").val('');
        $("#txtFechaFAdenda").val('');
        return false;
    }
    return true;
}
function fn_EliminarArchivo() {
    $("#DivMuestraAr").css("display", "none");
    $("#DivAdjuntarar").css("display", "block");
    $("#inputVerificaDelete").val("1");
}
function fn_MantenimientoSolicitudAdenda() {
    var doc = document.getElementById("FileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
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
            TIPO_PROCESO: $("#DdlTipoProceso").val(),
            ID_SOLICITUD: $("#inputIdSolicitud").val(),
            
        };
        Lista_Periodo_pago.push(item);
    });
    e.append("ID_CONTRATO_DET", $("#ID_CONTRATO_DET").val());
    e.append("ID_CONTRATO", $("#COD_CONTRATO").val());
    e.append("ID_ENTIDAD", $("#DcboEntidades").val());
    e.append("TIPO_PROCESO", $("#DdlTipoProceso").val());
    e.append("FECHA_INI", $("#txtFechaIAdenda").val());
    e.append("FECHA_FIN", $("#txtFechaFAdenda").val());
    e.append("MONTO", $("#inputMontoMensual").val());
    e.append("NOMBRE_PUESTO", $("#txtPuesto").val());
    e.append("TIPO", $("#ACCION").val());
    e.append("ID_ARCHIVO_SUSTENTO", $("#ID_ARCHIVO_SUSTENTO").val());
    e.append("ID_SOLICITUD", $("#inputIdSolicitud").val());
    
    e.append("listaRenumeracion", JSON.stringify(Lista_Periodo_pago));
    $.ajax({
        url: baseUrl + "Coordinador/Adenda/MantenimientoSolicitudAdenda",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarEditarSolicitudAdenda();
                CargarGrillaAdenda();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
}
function CrearGrillaAdenda() {
    DESARROLLO.configurarGrilla();
    TableSolicitudAdenda_ = $("#TableSolicitudAdenda").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": false,
        "bInfo": true,
        columns: [
            { "title": "ID_CONTRATO_DET", "data": "ID_CONTRATO_DET", "class": "text-center", "visible": false, },
            { "title": "ID_CONTRATO", "data": "ID_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ACCIONES", "data": "", "class": "text-center", "width": "3%" },
            { "title": "ARCHIVO", "data": "", "class": "text-center", "width": "1%" },
            { "title": "FECHA DE REGISTRO", "data": "FEC_INGRESO", "class": "text-center", "width": "1%" },
            { "title": "H.R. ASIGNADA", "data": "NR_TRAMITE", "class": "text-center", },
            { "title": "N° DE SOLICITUD", "data": "CODIGO_ADENDA", "class": "text-left", },
            { "title": "DNI / CE", "data": "DOCUMENTO_CONSULTOR", "class": "text-left", },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left", },
            { "title": "ENTIDAD", "data": "DESC_ENTIDAD", "class": "text-left", },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-left", },
            { "title": "CARGO", "data": "NOMBRE_PUESTO", "class": "text-left", },
            { "title": "FECHA INICIO", "data": "FECHA_INI", "class": "text-center", },
            { "title": "FECHA FIN", "data": "FECHA_FIN", "class": "text-center", },
            { "title": "HONORARIOS S/ ", "data": "MONTO", "class": "text-center", },


            { "title": "NUM_PROCESO", "data": "NUM_PROCESO", "class": "text-center", "visible": false, },
            { "title": "ANIO_PROCESO", "data": "ANIO_PROCESO", "class": "text-center", "visible": false,  },
            { "title": "ID_ENTIDAD", "data": "ID_ENTIDAD", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_SUSTENTO", "data": "ID_ARCHIVO_SUSTENTO", "class": "text-center", "visible": false, },
            { "title": "IDEDOCODIGO", "data": "IDEDOCODIGO", "class": "text-center", "visible": false, },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "visible": false, },
            { "title": "ID_ARCHIVO_CONTRATO", "data": "ID_ARCHIVO_CONTRATO", "class": "text-center", "visible": false, },
            { "title": "ID_TRAMITE", "data": "ID_TRAMITE", "class": "text-center", "visible": false, },
            { "title": "ID_SOLICITUD", "data": "ID_SOLICITUD", "class": "text-center", "visible": false, },
            
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                   

                    if (row.IDEDOCODIGO == "017" || row.IDEDOCODIGO == "020") {
                        if (row.TIPO_PROCESO == "F") {
                            if (row.ID_ARCHIVO_CONTRATO > 0) {
                                html += '<input  type="checkbox"  id="' + row.ID_CONTRATO_DET + '" value="' + row.ID_TRAMITE + '" >';
                             //   html += '&nbsp;&nbsp;<label for="cbxSeleccionarTodo" class="custom-control-label"></label > class="custom-control-input custom-control-input-danger" '
                            }
                        } else {
                             html += '<input  type="checkbox"  id="' + row.ID_CONTRATO_DET + '" value="' + row.ID_TRAMITE + '" >';
                        }
                    }

                    if (row.IDEDOCODIGO=="017") {
                        html += '<button  class="btn btn-xs"  onClick="fn_Adenda(' + row.ID_CONTRATO_DET + ');" type="button" title="Editar solicitud de adenda"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';

                     
                        html += '<button  class="btn btn-xs"  onClick="fn_EliminarSolicitud(' + row.ID_CONTRATO_DET + ');" type="button" title="Eliminar solicitud de adenda"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        if (row.TIPO_PROCESO == "F") {
                            html += '<button  class="btn btn-xs"  onClick="fn_Formato_Adenda_Fag(' + row.ID_CONTRATO_DET + ',' + row.ID_SOLICITUD+ ');" type="button" title="Ver formato de adenda"><i class="fas fa-file-signature" style="color:#606060;font-size:18px"></i></button>';
                            if (row.ID_ARCHIVO_CONTRATO > 0) {
                                html += '<button  class="btn btn-xs"  onClick="fn_EnviarUTP(' + row.ID_CONTRATO_DET + ',' + row.ID_TRAMITE +');" type="button" title="Enviar a UTP para registro de adenda"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';

                            }
                        } else {
                            html += '<button  class="btn btn-xs"  onClick="fn_Formato_Adenda_Pac(' + row.ID_CONTRATO_DET + ');" type="button" title="Ver formato de adenda"><i class="fas fa-file-signature" style="color:#606060;font-size:18px"></i></button>';
                            html += '<button  class="btn btn-xs"  onClick="fn_EnviarUTP(' + row.ID_CONTRATO_DET + ',' + row.ID_TRAMITE+');" type="button" title="Enviar a UTP para generar adenda"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';

                        }
                        html += '<button class="btn btn-xs" onClick="subirAdendaFirmadaFrm(' + row.ID_CONTRATO_DET + ', \'' + row.TIPO_PROCESO + '\')" type="button" title="Adenda Firmada"><i class="fa fa-upload" style="color: #606060; font-size: 18px;"></i></button>';
                      
                    }
                    if (row.IDEDOCODIGO == "018") {
                        html += '<button  class="btn btn-xs"  type="button" title="Documento enviado a UTP"><i class="fas fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';

                    }
                    if (row.IDEDOCODIGO == "021") {
                        html += '<button  class="btn btn-xs" type="button" title="Solicitud Rechazada"><i class="fas fa-user-alt-slash" style="color:#e30613;font-size:18px"></i></button>';
                    }
                    if (row.IDEDOCODIGO == "020") {
                        html = '<button  class="btn btn-xs"  onClick="fn_Adenda(' + row.ID_CONTRATO_DET + ');" type="button" title="Editar solicitud de adenda"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="fn_VerObservacionesAdenda(' + row.ID_CONTRATO_DET + ');" type="button" title="Ver observaciones"><i class="far fa-comment-dots" style="color:#606060;font-size:18px"></i></button>';

                        // html += '<button  class="btn btn-xs"  onClick="fn_EliminarSolicitud(' + row.ID_SOLICITUD + ');" type="button" title="Eliminar solicitud de adenda"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        if (row.TIPO_PROCESO == "F") {
                            html += '<button  class="btn btn-xs"  onClick="fn_Formato_Adenda_Fag(' + row.ID_CONTRATO_DET + ');" type="button" title="Ver formato de adenda"><i class="fas fa-file-signature" style="color:#606060;font-size:18px"></i></button>';
                            if (row.ID_ARCHIVO_CONTRATO > 0) {
                                html += '<button  class="btn btn-xs"  onClick="fn_EnviarUTP(' + row.ID_CONTRATO_DET + ',' + row.ID_TRAMITE + ');" type="button" title="Enviar a UTP para registro de adenda"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';

                            }
                        } else {
                            html += '<button  class="btn btn-xs"  onClick="fn_Formato_Adenda_Pac(' + row.ID_CONTRATO_DET + ');" type="button" title="Ver formato de adenda"><i class="fas fa-file-signature" style="color:#606060;font-size:18px"></i></button>';
                            html += '<button  class="btn btn-xs"  onClick="fn_EnviarUTP(' + row.ID_CONTRATO_DET + ',' + row.ID_TRAMITE + ');" type="button" title="Enviar a UTP para generar adenda"><i class="far fa-envelope" style="color:#6ABEB0;font-size:18px"></i></button>';

                        }
                    }
                    if (row.IDEDOCODIGO == "019") {
                        html += '<button  class="btn btn-xs" type="button" title="Solicitud aprobada"><i class="far fa-check-circle" style="color:#0093F8;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
            {
                'targets': 3,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_SUSTENTO +');" type="button" title="Descargar archivo de sustento"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    if (row.ID_ARCHIVO_CONTRATO > 0 ) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONTRATO + ');" type="button" title="Descargar contrato firmado"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';

                    }
                    return html;
                }
            },
            {
                'targets': 4,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FEC_INGRESO);;
                    return html;
                }
            },
            {
                'targets': 12,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_INI);;
                    return html;
                }
            },
            {
                'targets': 13,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_FIN);;
                    return html;
                }
            },
            {
                "targets": [14],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
        ],

    });
}
function CargarGrillaAdenda() {
    var item =
    {
        ID_CONTRATO_DET: 0,
        ID_CONTRATO:0,
        ID_ENTIDAD: $('#inputHddIdEntidad').val(),
        TIPO_PROCESO: $('#DdlTipoC').val(),
        IDEDOCODIGO: $('#DdlEstado').val(),
        NUM_PROCESO: $('#txtContrato').val(),
        ANIO_PROCESO: $('#txtAnio').val(),
        DOCUMENTO_CONSULTOR: $('#txtDocumento').val(),  
    };
    var url = baseUrl + 'Coordinador/Adenda/ListaDetalleContratos';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitudAdenda_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableSolicitudAdenda_.rows.add(respuesta).draw();
    }
}
function CrearGrillaSolicitudAdenda() {
    DESARROLLO.configurarGrilla();
    TableSolicitudAdendaModal_ = $("#TableSolicitudAdendaModal").DataTable({
        ordering: false,
        "paging": false,
        "autoWidth": false,
        "bInfo": false,
        columns: [
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-center", "visible": true, },
            { "title": "CONTRATO N°", "data": "CODIGO_CONTRATO", "class": "text-center", "visible": true, },
            { "title": "ARCHIVO ADENDA", "data": "", "class": "text-center", "width": "1%" },
        ],
        "columnDefs": [
            {
                'targets': 2,
                'render': function (data, type, row, meta) {
                    var html = '';
                    if (row.ID_ARCHIVO_CONTRATO > 0) {
                        html += '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' + row.ID_ARCHIVO_CONTRATO + ');" type="button" title="Descargar contrato firmado"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    }
                    return html;
                }
            },
           
        ],

    });
}

function CargarGrillaSolicitudAdenda(codigos_adendas) {
    console.log("codigos_adendas");
    console.log(codigos_adendas);
    var item =
    {
        TIPO_PROCESO: $('#DdlTipoC').val(),
        CODIGOS_CONTRATO_DET: codigos_adendas,

    };
    var url = baseUrl + 'Coordinador/Adenda/ListaDetalleAdendas';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TableSolicitudAdendaModal_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableSolicitudAdendaModal_.rows.add(respuesta).draw();
    }
}

function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Coordinador/ProcesoPac/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}
function fn_Formato_Adenda_Fag(id, id_solicitud) {
    jQuery("#modalAdendaFormato").html('');
    jQuery("#modalAdendaFormato").load(baseUrl + "Coordinador/Adenda/Ver_Adenda?ID=" + id + "&FLG_TIPO=F" , function (responseText, textStatus, request) {
        $("#modalAdendaFormato").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_Formato_Adenda_Pac(id) {
    jQuery("#modalAdendaFormato").html('');
    jQuery("#modalAdendaFormato").load(baseUrl + "Coordinador/Adenda/Ver_Adenda?ID=" + id + "&FLG_TIPO=P", function (responseText, textStatus, request) {
        $("#modalAdendaFormato").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}




function fn_CerrarAdenda() {
    jQuery("#modalAdendaFormato").html('');
    $('#modalAdendaFormato').modal('hide');
}
function ValidateArchivosAdenda() {
    var result = true;
    if ($("#inputVerificaDelete").val() == "1") {
        if ($('#FileArchivo')[0].files.length === 0) { $("#LblfileArchivo").text("Es necesario que adjunte el documento."); result = false; } else { $("#LblfileArchivo").text(""); }
    } else {
        if ($("#ID_ARCHIVO").val() == "0") {
            if ($('#FileArchivo')[0].files.length === 0) { $("#LblfileArchivo").text("Es necesario que adjunte el documento."); result = false; } else { $("#LblfileArchivo").text(""); }

        }
    }
    return result;
}
function fn_UpdArchivoAdenda() {
    var doc = document.getElementById("FileArchivo").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivo").files[l], e.append("FileArchivo", a);
    e.append("ID_CONTRATO_DET", $("#ID").val());
    e.append("ID_ARCHIVO", $("#ID_ARCHIVO").val());
    e.append("ID_SOLICITUD", $("#ID_SOLICITUD").val());
    e.append("TIPO_PROCESO", $("#FLG_TIPO").val());
    
    $.ajax({
        url: baseUrl + "Coordinador/Adenda/UpdArchivoAdenda",
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarAdenda();
                CargarGrillaAdenda();
                DESARROLLO.PROCESO_CORRECTO(p.message);

            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }

        }
    });
}
function fn_EnviarUTP(ID, ID_TRAMITE) {
    if (ID_TRAMITE==0) {
        jQuery("#modalVerObservacionAdenda").html('');
        jQuery("#modalVerObservacionAdenda").load(baseUrl + "Coordinador/Adenda/Ver_Integrar_STD?ID_TRAMITE=" + ID_TRAMITE + "&ID_CONTRATO_DET=" + ID, function (responseText, textStatus, request) {
            $("#modalVerObservacionAdenda").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
    }
    else {
        DESARROLLO.PROCESO_CONFIRM("</br>¿Desea <b> Notificar</b> a la oficina de UTP.? </br >", function (r) {
            $("#myModalCargando").fadeIn(100, function () {
                fn_NotificarUTP(ID, ID_TRAMITE);
            });
        });
    }

}

function culminar() {
    $("#myModalCargando").css("display", "none");
    fn_CerrarEnvioUTPDocumentos();
    CargarGrillaAdenda();
    DESARROLLO.PROCESO_CORRECTO("Proceso Finalizado Correctamente");
}

function fn_Formato_Adenda_Masiva() {
    var tipoProceso = $("#DdlTipoProceso").val();

    jQuery("#modalAdendaFormato").html('');
    jQuery("#modalAdendaFormato").load(baseUrl + "Coordinador/Adenda/Ver_AdendaMasiva?jsondata=" + id + "&FLG_TIPO=" + tipoProceso, function (responseText, textStatus, request) {
        $("#modalAdendaFormato").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}

function fn_NotificarUTP_Archivo(ID, ID_TRAMITE) {
    var doc = document.getElementById("FileArchivoOficio").files.length;
    var doc_ = document.getElementById("FileArchivoResumen").files.length;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoOficio").files[l], e.append("FileArchivoOficio", a);
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoResumen").files[l], e.append("FileArchivoResumen", a);
    e.append("NR_OFICIO", $("#txtOficio").val());
    e.append("NR_FOLIOS", $("#txtFolios").val());
    e.append("ASUNTO", $("#txtAsunto").val());
    e.append("ID_CONTRATO_DET", ID);
    e.append("IDEDOCODIGO", '018');
    e.append("ID_TRAMITE", ID_TRAMITE);
    e.append("NR_TRAMITE", $("#txtNTramite").val());
    e.append("FLG_CON_HR", $("#Ddlflgtramite").val());
    $.ajax({
        url: baseUrl + 'Coordinador/Adenda/UpdEstadoAdenda',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            if (p.success) {
                fn_CerrarEnvioUTPDocumentos();
                CargarGrillaAdenda();
                DESARROLLO.PROCESO_CORRECTO(p.message);
                if (p.extra2 == "1") {
                    DESARROLLO.PROCESO_ALERT(p.message2);
                }
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
            }
            $("#myModalCargando").css("display", "none");
        }
    });
    //var item =
    //{
    //    ID_CONTRATO_DET: ID,
    //    IDEDOCODIGO: '018',
    //    ID_TRAMITE: ID_TRAMITE
    //};
    //var urls = baseUrl + 'Coordinador/Adenda/UpdEstadoAdenda';
    //var respuesta = DESARROLLO.Ajax(urls, item, false);
    //if (respuesta != null && respuesta != "") {
    //    if (respuesta.success) {
    //        CargarGrillaAdenda();
    //        DESARROLLO.PROCESO_CORRECTO(respuesta.message);
    //        if (respuesta.extra2 == "1") {
    //            DESARROLLO.PROCESO_ALERT(respuesta.message2);
    //        }
    //    } else {
    //        DESARROLLO.PROCESO_ERROR(respuesta.extra);
    //    }
    //    $("#myModalCargando").css("display", "none");
    //}
}


function fn_NotificarUTP_Archivo_Masivo(ID, ID_TRAMITE, oficio, folio, nroTramite, flagTramite,asunto) {

    // var doc = document.getElementById("FileArchivoOficioM").files.length;
    //var doc_ = document.getElementById("FileArchivoResumenM").files.length;
    var doc = 0;
    var doc_ = 0;
    var e = new FormData;
    if (doc > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoOficioM").files[l], e.append("FileArchivoOficio ", a);
    if (doc_ > 0) for (var a, l = 0; doc > l; l++) a = document.getElementById("FileArchivoResumenM").files[l], e.append("FileArchivoResumen", a);
    e.append("NR_OFICIO", oficio);
    e.append("NR_FOLIOS", folio);
    e.append("ASUNTO", asunto);
    e.append("ID_CONTRATO_DET", ID);
    e.append("IDEDOCODIGO", '018');
    e.append("ID_TRAMITE", ID_TRAMITE);
    e.append("NR_TRAMITE", nroTramite);
    e.append("FLG_CON_HR", flagTramite);
    $.ajax({
        url: baseUrl + 'Coordinador/Adenda/UpdEstadoAdenda',
        type: "POST",
        data: e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        contentType: !1,
        processData: !1,
        success: function (p) {
            console.log(p);
            if (p.success) {

                if (p.extra2 == "1") {
                    DESARROLLO.PROCESO_ALERT(p.message2);
                    return;
                }
            }
            else {
                DESARROLLO.PROCESO_ERROR(p.extra);
                return;
            }

        }
    });
}
function fn_NotificarUTP(ID, ID_TRAMITE) {
    var item =
    {
        ID_CONTRATO_DET: ID,
        IDEDOCODIGO: '018',
        ID_TRAMITE: ID_TRAMITE
    };
    var urls = baseUrl + 'Coordinador/Adenda/UpdEstadoAdenda';
    var respuesta = DESARROLLO.Ajax(urls, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            CargarGrillaAdenda();
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            if (respuesta.extra2 == "1") {
                DESARROLLO.PROCESO_ALERT(respuesta.message2);
            }
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
        $("#myModalCargando").css("display", "none");
    }
}

function fn_EliminarSolicitud(ID) {
    DESARROLLO.PROCESO_CONFIRM("¿Desea <b>Eliminar</b> la adenda registrada?", function (r) {
        if (r) {
            var item =
            {
                ID_CONTRATO_DET: ID,
                IDEDOCODIGO: '017',
                ID_TRAMITE: 0
            };
            var urls = baseUrl + 'Coordinador/Adenda/UpdEstadoAdenda_Delete';
            var respuesta = DESARROLLO.Ajax(urls, item, false);
            if (respuesta != null && respuesta != "") {
                if (respuesta.success) {
                    CargarGrillaAdenda();
                    DESARROLLO.PROCESO_CORRECTO(respuesta.message);
                } else {
                    DESARROLLO.PROCESO_ERROR(respuesta.extra);
                }
            }

        }
    });

}
function fn_VerObservacionesAdenda(id) {
    jQuery("#modalVerObservacionAdenda").html('');
    jQuery("#modalVerObservacionAdenda").load(baseUrl + "Coordinador/Adenda/VerObservacionADENDA?ID_CONTRATO_DET=" + id , function (responseText, textStatus, request) {
        $("#modalVerObservacionAdenda").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
function fn_CerrarVerObservacion() {
    jQuery("#modalVerObservacionAdenda").html('');
    $('#modalVerObservacionAdenda').modal('hide');
}
function fn_CerrarEnvioUTPDocumentos() {
    jQuery("#modalVerObservacionAdenda").html('');
    $('#modalVerObservacionAdenda').modal('hide');
}

function subirAdendaFirmadaFrm(id, tipo) {
    jQuery("#modalSubirAdendaFirmada").html('');
    jQuery("#modalSubirAdendaFirmada").load(baseUrl + "Coordinador/Adenda/AdendaFirmadaFrm?ID_CONTRATO_DET=" + id + "&TIPO=" + tipo, function (responseText, textStatus, request) {
        $("#modalSubirAdendaFirmada").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    });
}
