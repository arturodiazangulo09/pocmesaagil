var tablaContratos_;
var tablaPagos_;

var loadingOpen = function () {
    $(".container_spinner").removeClass("hide");
    $(".container_spinner").addClass("show");
};
var loadingClose = function () {
    $(".container_spinner").removeClass("show");
    $(".container_spinner").addClass("hide");
}

var Contratos = function () {
    var Iniciar = function () {
        loadContratos();
        loadPagos();
        ComboAnio("DdlPeriodoPago");
        EnviarCombo({}, baseUrl + 'Carga/Contrato/GetEntidades', '#DdlDependenciaContrato');
        EnviarCombo({}, baseUrl + 'Carga/Contrato/GetEntidades', '#DdlDependencia');
        Remove_RemoverClases("liContratos");
        loadFiltros();
        listarContratos();
        listarPagos();
    };
    var loadFiltros = function () {
        $.ajax({
            type: 'POST',
            url: baseUrl + 'Carga/Contrato/ListarFiltroContrato',
            data: null,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                let html = '<option value="">...Seleccionar...</option>';

                for (var i in result) {
                    html += '<option value="' + result[i]["ID"] + '">' + result[i]["DESCRIPCION"] + '</option>';
                }

                $("#lstAgregarFiltro").html(html);
                $("#lstAgregarFiltro").select2();
            }
        })
    };
    var añadirBusqueda = function (campo, valor) {

    };
    var loadContratos = function (pagina = 1, nregistros = 10) {
        DESARROLLO.configurarGrilla();

        tablaContratos_ = $("#tblCargaContratos").DataTable({
            //data: result.Data,
            retrieve: true,
            searching: false,
            ordering: false,
            paging: false,
            autoWidth: false,
            bInfo: false,
            columns: [
                { "title": "COD REGISTRO", "data": "ID_REGISTRO", "class": "text-center", "visible": false, },
                { "title": "ACCIONES", "data": "", "class": "text-center", "visible": true, },
                { "title": "COD CONTRATO", "data": "COD_CONTRATO", "class": "text-center", "visible": true, },
                { "title": "CARGO", "data": "CARGO_FUNCIONARIO_SUSCRIBE", "class": "text-left", "visible": true, },
                { "title": "NOMBRE COMPLETO", "data": "", "class": "text-left", "visible": true, },
                { "title": "SECTORISTA RESPONSABLE", "data": "SEC_RESPONSABLE", "class": "text-left", "visible": true, },
                { "title": "DNI", "data": "DNI", "class": "text-center" },
            ],
            "columnDefs": [
                {
                    'targets': 1,
                    'render': function (data, type, row, meta) {
                        var html = '';
                        html = '<button  class="btn btn-xs"  onClick="Contratos.OpenMantenimientoContrato(\'' + row.ID_REGISTRO + '\', \'' + row.ID_CARGA + '\', \'' + row.TIPO_DOC + '\');" type="button" title="Actualizar registro"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button class="btn btn-xs" onClick="Contratos.OpenAdendas(\'' + row.COD_CONTRATO + '\');" type="button" title="Ver Adenda"><i class="fas fa-solid fa-list"></i></button>';
                        html += '<button class="btn btn-xs" onClick="Contratos.OpenPagos(\'' + row.COD_CONTRATO + '\');" type="button" title="Ver Pagos"><i class="fa fa-money"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="Contratos.EliminarContrato(\'' + row.ID_REGISTRO + '\');" type="button" title="Eliminar registro"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        return html;
                    }
                },
                {
                    'targets': 4,
                    'render': function (data, type, row, meta) {
                        var html = '';
                        html = row.NOMBRES + ' ' + row.APELLIDO_PATERNO + ' ' + row.APELLIDO_MATERNO
                        return html;
                    }
                },
            ],
        });

        //$.ajax({
        //    type: 'POST',
        //    url: baseUrl + 'Carga/Contrato/ListaContratos?campos=&valores=&pagina=' + pagina + '&nregistros=' + nregistros,
        //    data: null,
        //    contentType: 'application/json; charset=utf-8',
        //    success: function (result) {
        //        if (!result.Error) {
        //            $("#tblCargaContratos").DataTable({
        //                data: result.Data,
        //                retrieve: true,
        //                searching: false,
        //                ordering: false,
        //                paging: false,
        //                autoWidth: false,
        //                bInfo: false,
        //                columns: [
        //                    { "title": "COD REGISTRO", "data": "ID_REGISTRO", "class": "text-center", "visible": false, },
        //                    { "title": "ACCIONES", "data": "", "class": "text-center", "visible": true, },
        //                    { "title": "COD CONTRATO", "data": "COD_CONTRATO", "class": "text-center", "visible": true, },
        //                    { "title": "CARGO", "data": "CARGO_FUNCIONARIO_SUSCRIBE", "class": "text-left", "visible": true, },
        //                    { "title": "NOMBRE COMPLETO", "data": "", "class": "text-left", "visible": true, },
        //                    { "title": "SECTORISTA RESPONSABLE", "data": "SEC_RESPONSABLE", "class": "text-left", "visible": true, },
        //                    { "title": "DNI", "data": "DNI", "class": "text-center" },

        //                ],
        //                "columnDefs": [
        //                    {
        //                        'targets': 1,
        //                        'render': function (data, type, row, meta) {
        //                            var html = '';
        //                            html = '<button  class="btn btn-xs"  onClick="Contratos.OpenMantenimientoContrato(\'' + row.ID_REGISTRO + '\', \'' + row.TIPO_DOC +'\');" type="button" title="Actualizar registro"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
        //                            html += '<button class="btn btn-xs" onClick="Contratos.OpenAdendas(\'' + row.COD_CONTRATO + '\');" type="button" title="Ver Adenda"><i class="fas fa-solid fa-list"></i></button>';
        //                            html += '<button class="btn btn-xs" onClick="Contratos.OpenPagos(\'' + row.COD_CONTRATO + '\');" type="button" title="Ver Pagos"><i class="fa fa-money"></i></button>';
        //                            html += '<button  class="btn btn-xs"  onClick="Contratos.EliminarContrato(\'' + row.ID_REGISTRO + '\');" type="button" title="Eliminar registro"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
        //                            return html;
        //                        }
        //                    },
        //                    {
        //                        'targets': 4,
        //                        'render': function (data, type, row, meta) {
        //                            var html = '';
        //                            html = row.NOMBRES + ' ' + row.APELLIDO_PATERNO + ' ' + row.APELLIDO_MATERNO
        //                            return html;
        //                        }
        //                    },
        //                ],
        //            });
        //        }
        //    }
        //});
    }
    var loadPagos = function (codContrato = "0", pagina = 1, nregistros = 10) {
        DESARROLLO.configurarGrilla();

        tablaPagos_ = $("#tblCargaPagos").DataTable({
            //data: result.Data,
            retrieve: true,
            searching: false,
            ordering: false,
            paging: false,
            autoWidth: false,
            bInfo: false,
            columns: [
                { "title": "ACCIONES", "data": "", "class": "text-center", "visible": true, },
                { "title": "DEPENDENCIA", "data": "DESC_ENTIDAD", "class": "text-center", "visible": true, },
                { "title": "COD CONTRATO", "data": "COD_CONTRATO", "class": "text-center", "visible": true, },
                { "title": "NUM DOCUMENTO", "data": "NUM_DOCUMENTO", "class": "text-center" },
                { "title": "RUC_CAS", "data": "RUC_CAS", "class": "text-center", "visible": true, }
            ],
            "columnDefs": [
                {
                    'targets': 0,
                    'render': function (data, type, row, meta) {
                        var html = '';
                        html = '<button  class="btn btn-xs"  onClick="ContratoPagos.OpenModalCargaPago(\'' + row.ID_REGISTRO + '\');" type="button" title="Actualizar Contrato"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="ContratoPagos.EliminarPago(\'' + row.ID_REGISTRO + '\');" type="button" title="Eliminar Contrato"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        return html;
                    }
                }
            ],
        });

        //$.ajax({
        //    type: 'POST',
        //    url: baseUrl + 'Carga/Contrato/ListaPagos?codContrato=' + codContrato + '&periodo=' + periodo + '&codTrabajador=' + codTrabajador + '&numDocumento=' + numDocumento + '&rucCas=' + rucCas + '&idEntidad=' + idEntidad + '&campos=&valores=&pagina=' + pagina + '&nregistros=' + nregistros,
        //    data: null,
        //    contentType: 'application/json; charset=utf-8',
        //    success: function (result) {

        //        if (!result.Error) {
        //            $("#tblCargaPagos").DataTable({
        //                data: result.Data,
        //                retrieve: true,
        //                //searching: true,
        //                ordering: false,
        //                paging: false,
        //                autoWidth: false,
        //                bInfo: false,
        //                columns: [
        //                    { "title": "ACCIONES", "data": "", "class": "text-center", "visible": true, },
        //                    { "title": "DEPENDENCIA", "data": "DESC_ENTIDAD", "class": "text-center", "visible": true, },
        //                    { "title": "COD CONTRATO", "data": "COD_CONTRATO", "class": "text-center", "visible": true, },
        //                    { "title": "COD TRABAJADOR", "data": "COD_TRABAJADOR", "class": "text-center", "visible": true, },
        //                    { "title": "NUM DOCUMENTO", "data": "NUM_DOCUMENTO", "class": "text-center" },
        //                    { "title": "RUC_CAS", "data": "RUC_CAS", "class": "text-center", "visible": true, }
        //                ],
        //                "columnDefs": [
        //                    {
        //                        'targets': 0,
        //                        'render': function (data, type, row, meta) {
        //                            var html = '';
        //                            html = '<button  class="btn btn-xs"  onClick="ContratoPagos.OpenModalCargaPago(\'' + row.ID_REGISTRO + '\');" type="button" title="Actualizar Contrato"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
        //                            html += '<button  class="btn btn-xs"  onClick="ContratoPagos.EliminarPago(\'' + row.ID_REGISTRO + '\');" type="button" title="Eliminar Contrato"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
        //                            return html;
        //                        }
        //                    }
        //                ],
        //            });
        //        }
        //    }
        //});
    };
    var openMantenimientoContrato = function (idRegistro, idCarga, TipoDoc) {
        //if (TipoDoc == 'FAG') {
        jQuery("#modalMantenimientoContratoFAG").load(baseUrl + "Carga/Contrato/OpenMantenimientoFag?idRegistro=" + idRegistro + "&idCarga=" + idCarga, function (responseText, textStatus, request) {
            $("#modalMantenimientoContratoFAG").modal("show");
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
        /*}
        if (TipoDoc == 'PAC') {
            jQuery("#modalMantenimientoContratoPAC").load(baseUrl + "Carga/Contrato/OpenMantenimientoPac?idRegistro=" + idRegistro, function (responseText, textStatus, request) {
                $("#modalMantenimientoContratoPAC").modal("show");
            });
        }*/
    };
    var openAdendas = function (codContrato) {
        jQuery("#modalAdendas").load(baseUrl + "Carga/Contrato/OpenAdendas?codContrato=" + codContrato, function (responseText, textStatus, request) {
            $("#modalAdendas").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
    };
    var openPagos = function (codContrato) {
        jQuery("#modalPagos").load(baseUrl + "Carga/Contrato/OpenPagos?codContrato=" + codContrato, function (responseText, textStatus, request) {
            $("#modalPagos").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        });
    };
    var eliminarContrato = function (idRegistro) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea eliminar el contrato seleccionado? Se eliminarán las adendas relacionadas al contrato", (rpta) => {
            if (rpta) {
                loadingOpen();
                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Contrato/EliminarContrato?idRegistro=" + idRegistro,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result.Data) {
                            DESARROLLO.PROCESO_CORRECTO("Registro eliminado correctamente");
                            listarContratos();
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se eliminó el registro");
                        }
                    },
                    complete: function () {
                        loadingClose();
                    }
                });
            }

        });
    }
    var EnviarCombo = function (data, ruta, id) {
        $.ajax({
            type: 'POST',
            url: ruta,
            data: data,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                let html = "";
                let data = JSON.parse(result);
                html += "<option value='0'> SELECCIONAR </option>";
                for (var i = 0; i < data.length; i++) {
                    html += "<option value='" + data[i].Id + "'>" + data[i].Descripcion + "</option>";
                }
                $(id).html(html);
            }
        });
    };

    function ComboAnio(cboControl) {
        var n = (new Date()).getFullYear()
        var select = document.getElementById(cboControl);

        select.options.add(new Option(" SELECCIONAR ", "0"));
        for (var i = n; i >= 2009; i--)select.options.add(new Option(i, i));
    };
    function listarContratos() {
        var pagina = 1, nregistros = 10;

        loadingOpen();

        var item =
        {
            pagina: '1',
            nregistros: '10',
            numDocumento: $("#txt_dni_cont").val(),
            cargo: $("#txt_cargo_cont").val(),
            idEntidad: ($("#DdlDependenciaContrato").val() == null ? 0 : $("#DdlDependenciaContrato").val())
        };
        var url = baseUrl + 'Carga/Contrato/ListaContratos';

        var respuesta = DESARROLLO.Ajax(url, item, false);

        tablaContratos_.clear().draw();
        if (respuesta != null && respuesta != "") {
            tablaContratos_.rows.add(respuesta.Data).draw();
        }

        loadingClose();
    }
    function listarPagos() {
        loadingOpen();
        var item =
        {
            pagina: '1',
            nregistros: '10',
            codContrato: "0",
            periodo: $("#DdlPeriodoPago").val(),
            numDocumento: $("#txt_documento").val(),
            rucCas: $("#txt_ruc").val(),
            idEntidad: ($("#DdlDependencia").val() == null ? 0 : $("#DdlDependencia").val())
        };

        var url = baseUrl + 'Carga/Contrato/ListaPagos';

        //var url = baseUrl + 'Carga/Carga/ListaContratosCabDet';
        var respuesta = DESARROLLO.Ajax(url, item, false);

        tablaPagos_.clear().draw();
        if (respuesta != null && respuesta != "") {
            tablaPagos_.rows.add(respuesta.Data).draw();
        }

        loadingClose();
    }

    $("#btnPagoSC").click(function () {
        openPagos(0);
    })
    $("#btnBuscarPago").click(function () {
        listarPagos();
    })
    $("#btnBuscarContrato").click(function () {
        listarContratos();
    })
    $("#btnLimpiaContrato").click(function () {
        $("#DdlDependenciaContrato").val("0");
        $("#txt_dni_cont").val("");
        $("#txt_cargo_cont").val("");
        $("#txt_dni_cont").focus();
    })
    $("#btnLimpiaPago").click(function () {
        $("#DdlDependencia").val("0");
        $("#DdlPeriodoPago").val("0");
        $("#txt_cod_trab").val("");
        $("#txt_documento").val("");
        $("#txt_ruc").val("");        
        $("#txt_cod_trab").focus();
    })

    return {
        Iniciar: function () { Iniciar(); },
        OpenMantenimientoContrato: function (idRegistro, idCarga, TipoDoc) { openMantenimientoContrato(idRegistro, idCarga, TipoDoc); },
        OpenAdendas: function (codContrato) { openAdendas(codContrato); },
        OpenPagos: function (codContrato) { openPagos(codContrato); },
        EliminarContrato: function (idRegistro) { eliminarContrato(idRegistro); },
        ListarPagos: function (idRegistro) { listarPagos(); },
        ListarContratos: function (idRegistro) { listarContratos(); },
    }
}();
var ContratoPagos = function () {
    var eliminarPago = function (idRegistro) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea eliminar el pago seleccionado?", (rpta) => {

            
            if (rpta) {
                loadingOpen();
                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Contrato/EliminarPago?idPago=" + idRegistro,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result.Data) {
                            DESARROLLO.PROCESO_CORRECTO("Registro eliminado correctamente");
                            Contratos.ListarPagos();

                        } else {
                            DESARROLLO.PROCESO_ERROR("No se eliminó el registro");
                        }
                    },
                    complete: function () {
                        loadingClose();
                    }
                });
            }

        });
    }

    var openModalCargaPago = function (idPago) {
        jQuery("#modalCargaPago").load(baseUrl + "Carga/Contrato/MantenimientoPagos", function (responseText, textStatus, request) {
            if (textStatus == 'success') {
                EnviarCombo({}, baseUrl + 'Carga/Contrato/GetEntidades', '#DdlEntidadModal');
                ObtenerCargaPago(idPago);
                ComboAnio("DdlPeriodoPagoModal");
                $("#modalCargaPago").modal("show");
            }
        });
    };

    var ObtenerCargaPago = function (idPago) {
        loadingOpen();
        $.ajax({
            type: 'POST',
            url: baseUrl + "Carga/Contrato/ObtenerPago?idPago=" + idPago,
            data: null,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                cargaSalarioEnt = result;
                loadingClose();
                if (result.Result) {
                    $("#txtIdRegistro").val(result.ID_REGISTRO);
                    $("#TXT_COD_CONTRATO").val(result.COD_CONTRATO);
                    $("#DdlPeriodoPagoModal").val(result.PERIODO);
                    $("#TXT_COD_TRABAJADOR").val(result.COD_TRABAJADOR);
                    $("#TXT_NUM_DOCUMENTO").val(result.NUM_DOCUMENTO);
                    $("#TXT_RUC_CAS").val(result.RUC_CAS);
                    $("#DdlEntidadModal").val(result.ID_ENTIDAD);
                    $("#TXT_ING_ENE").val(result.ING_ENE); if (result.HONORARIO > 0 && result.ING_ENE > result.HONORARIO) $("#TXT_ING_ENE").css('background-color', '#fff39b');
                    $('#TXT_EGR_ENE').val(result.EGR_ENE);
                    $('#TXT_EMP_ENE').val(result.EMP_ENE);
                    $('#TXT_NET_ENE').val(result.NET_ENE);
                    $("#TXT_ING_FEB").val(result.ING_FEB); if (result.HONORARIO > 0 && result.ING_FEB > result.HONORARIO) $("#TXT_ING_FEB").css('background-color', '#fff39b');
                    $('#TXT_EGR_FEB').val(result.EGR_FEB);
                    $('#TXT_EMP_FEB').val(result.EMP_FEB);
                    $('#TXT_NET_FEB').val(result.NET_FEB);
                    $("#TXT_ING_MAR").val(result.ING_MAR); if (result.HONORARIO > 0 && result.ING_MAR > result.HONORARIO) $("#TXT_ING_MAR").css('background-color', '#fff39b');
                    $('#TXT_EGR_MAR').val(result.EGR_MAR);
                    $('#TXT_EMP_MAR').val(result.EMP_MAR);
                    $('#TXT_NET_MAR').val(result.NET_MAR);
                    $("#TXT_ING_ABR").val(result.ING_ABR); if (result.HONORARIO > 0 && result.ING_ABR > result.HONORARIO) $("#TXT_ING_ABR").css('background-color', '#fff39b');
                    $('#TXT_EGR_ABR').val(result.EGR_ABR);
                    $('#TXT_EMP_ABR').val(result.EMP_ABR);
                    $('#TXT_NET_ABR').val(result.NET_ABR);
                    $("#TXT_ING_MAY").val(result.ING_MAY); if (result.HONORARIO > 0 && result.ING_MAY > result.HONORARIO) $("#TXT_ING_MAY").css('background-color', '#fff39b');
                    $('#TXT_EGR_MAY').val(result.EGR_MAY);
                    $('#TXT_EMP_MAY').val(result.EMP_MAY);
                    $('#TXT_NET_MAY').val(result.NET_MAY);
                    $("#TXT_ING_JUN").val(result.ING_JUN); if (result.HONORARIO > 0 && result.ING_JUN > result.HONORARIO) $("#TXT_ING_JUN").css('background-color', '#fff39b');
                    $('#TXT_EGR_JUN').val(result.EGR_JUN);
                    $('#TXT_EMP_JUN').val(result.EMP_JUN);
                    $('#TXT_NET_JUN').val(result.NET_JUN);
                    $("#TXT_ING_JUL").val(result.ING_JUL); if (result.HONORARIO > 0 && result.ING_JUL > result.HONORARIO) $("#TXT_ING_JUL").css('background-color', '#fff39b');
                    $('#TXT_EGR_JUL').val(result.EGR_JUL);
                    $('#TXT_EMP_JUL').val(result.EMP_JUL);
                    $('#TXT_NET_JUL').val(result.NET_JUL);
                    $("#TXT_ING_AGO").val(result.ING_AGO); if (result.HONORARIO > 0 && result.ING_AGO > result.HONORARIO) $("#TXT_ING_AGO").css('background-color', '#fff39b');
                    $('#TXT_EGR_AGO').val(result.EGR_AGO);
                    $('#TXT_EMP_AGO').val(result.EMP_AGO);
                    $('#TXT_NET_AGO').val(result.NET_AGO);
                    $("#TXT_ING_SET").val(result.ING_SET); if (result.HONORARIO > 0 && result.ING_SET > result.HONORARIO) $("#TXT_ING_SET").css('background-color', '#fff39b');
                    $('#TXT_EGR_SET').val(result.EGR_SET);
                    $('#TXT_EMP_SET').val(result.EMP_SET);
                    $('#TXT_NET_SET').val(result.NET_SET);
                    $("#TXT_ING_OCT").val(result.ING_OCT); if (result.HONORARIO > 0 && result.ING_OCT > result.HONORARIO) $("#TXT_ING_OCT").css('background-color', '#fff39b');
                    $('#TXT_EGR_OCT').val(result.EGR_OCT);
                    $('#TXT_EMP_OCT').val(result.EMP_OCT);
                    $('#TXT_NET_OCT').val(result.NET_OCT);
                    $("#TXT_ING_NOV").val(result.ING_NOV); if (result.HONORARIO > 0 && result.ING_NOV > result.HONORARIO) $("#TXT_ING_NOV").css('background-color', '#fff39b');
                    $('#TXT_EGR_NOV').val(result.EGR_NOV);
                    $('#TXT_EMP_NOV').val(result.EMP_NOV);
                    $('#TXT_NET_NOV').val(result.NET_NOV);
                    $("#TXT_ING_DIC").val(result.ING_DIC); if (result.HONORARIO > 0 && result.ING_DIC > result.HONORARIO) $("#TXT_ING_DIC").css('background-color', '#fff39b');
                    $('#TXT_EGR_DIC').val(result.EGR_DIC);
                    $('#TXT_EMP_DIC').val(result.EMP_DIC);
                    $('#TXT_NET_DIC').val(result.NET_DIC);
                    $("#TXT_ING_TOT").val(result.ING_TOT);
                    $('#TXT_EGR_TOT').val(result.EGR_TOT);
                    $('#TXT_EMP_TOT').val(result.EMP_TOT);
                    $('#TXT_NET_TOT').val(result.NET_TOT);
                }
            }
        });
    }

    var EnviarCombo = function (data, ruta, id) {
        $.ajax({
            type: 'POST',
            url: ruta,
            data: data,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                let html = "";
                let data = JSON.parse(result);
                html += "<option value='0'> SELECCIONAR </option>";
                for (var i = 0; i < data.length; i++) {
                    html += "<option value='" + data[i].Id + "'>" + data[i].Descripcion + "</option>";
                }
                $(id).html(html);
            }
        });
    };

    function ComboAnio(cboControl) {
        var n = (new Date()).getFullYear()
        var select = document.getElementById(cboControl);
        for (var i = n; i >= 2009; i--)select.options.add(new Option(i, i));
    };

    return {
        OpenModalCargaPago: function (idPago) { openModalCargaPago(idPago); },
        EliminarPago: function (idRegistro) { eliminarPago(idRegistro); },
        ObtenerCargaPago: function (idPago) { ObtenerCargaPago(idPago); },
    }
}();

