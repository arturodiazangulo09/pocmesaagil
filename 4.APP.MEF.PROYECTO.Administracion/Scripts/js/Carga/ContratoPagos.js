var tablaPagos_;

var ContratoPagos = function () {
    var Iniciar = function (codContrato) {
        $("#txhCodContrato").val(codContrato);
        loadPagos(codContrato);
        listarPagos();
    }

    function loadingOpen(mensaje) {
        $(".container_spinner").removeClass("hide");
        $("#mensaje_spinner").text(mensaje);
        $(".container_spinner").addClass("show");
    };
    function loadingClose() {
        $(".container_spinner").removeClass("show");
        $(".container_spinner").addClass("hide");
    };

    var loadPagos = function (codContrato, pagina = 1, nregistros = 10) {
        DESARROLLO.configurarGrilla();

        tablaPagos_ = $("#tblCargaPagosModal").DataTable({
            //data: result.Data,
            retrieve: true,
            searching: false,
            ordering: false,
            paging: false,
            autoWidth: false,
            bInfo: false,
            columns: [
                { "title": "ACCIONES", "data": "", "class": "text-center", "visible": true, },
                { "title": "ENTIDAD", "data": "DESC_ENTIDAD", "class": "text-center", "visible": true, },
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

        /*var periodo = $("#DdlPeriodoPago").val();
         * 
        var numDocumento = $("#txt_documento").val();
        var rucCas = $("#txt_ruc").val();
        var idEntidad = $("#DdlDependencia").val();

        $.ajax({
            type: 'POST',
            url: baseUrl + 'Carga/Contrato/ListaPagos?codContrato=' + codContrato + '&periodo=' + periodo + '&numDocumento=' + numDocumento + '&rucCas=' + rucCas + '&idEntidad=' + idEntidad +'&campos=&valores=&pagina=' + pagina + '&nregistros=' + nregistros,
            data: null,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (!result.Error) {
                    $("#tblCargaPagos").DataTable({
                        data: result.Data,
                        retrieve: true,
                        searching: true,
                        ordering: false,
                        paging: false,
                        autoWidth: false,
                        bInfo: false,
                        columns: [
                            { "title": "ACCIONES", "data": "", "class": "text-center", "visible": true, },
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
                }
            }
        });*/
    };

    var eliminarPago = function (idRegistro) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea eliminar el pago seleccionado?", (rpta) => {
            loadingOpen();
            if (rpta) {

                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Contrato/EliminarPago?idPago=" + idRegistro,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result.Data) {
                            DESARROLLO.PROCESO_CORRECTO("Registro eliminado correctamente");
                            ContratoPagos.Iniciar();
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
        jQuery("#modalCargaPagoModal").load(baseUrl + "Carga/Contrato/MantenimientoPagos", function (responseText, textStatus, request) {
            if (textStatus == 'success') {
                EnviarCombo({}, baseUrl + 'Carga/Contrato/GetEntidades', '#DdlEntidadModal');
                ObtenerCargaPago(idPago);
                ComboAnio("DdlPeriodoPagoModal");
                $("#modalCargaPagoModal").modal("show");
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
                    $("#txhParentModal").val("CONTRATO");
                    $("#txtIdRegistro").val(result.ID_REGISTRO);
                    $("#TXT_COD_CONTRATO").val(result.COD_CONTRATO);
                    $("#DdlPeriodoPagoModal").val(result.PERIODO);
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

    function listarPagos() {
        var codContrato = $("#txhCodContrato").val();
        loadingOpen();
        var item =
        {
            pagina: '1',
            nregistros: '10',
            codContrato: codContrato,
            periodo: "0",
            numDocumento: "",
            rucCas: "",
            idEntidad: 0
        };

        var url = baseUrl + 'Carga/Contrato/ListaPagos';

        var respuesta = DESARROLLO.Ajax(url, item, false);
        tablaPagos_.clear().draw();
        if (respuesta != null && respuesta != "") {
            tablaPagos_.rows.add(respuesta.Data).draw();
        }

        loadingClose();
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
        Iniciar: function (codContrato) { Iniciar(codContrato); },
        OpenModalCargaPago: function (idPago) { openModalCargaPago(idPago); },
        EditarPago: function (idRegistro) { editarPago(idRegistro); },
        EliminarPago: function (idRegistro) { eliminarPago(idRegistro); },
        ObtenerCargaPago: function (idPago) { ObtenerCargaPago(idPago); },
        ListarPagos: function () { listarPagos(); },
    }
}();