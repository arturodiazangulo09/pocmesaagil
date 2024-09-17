var ContratoAdendas = function () {
    var Iniciar = function (codContrato) {
        
        loadAdendas(codContrato);
    }
    var loadAdendas = function (codContrato, pagina = 1, nregistros = 10) {
        DESARROLLO.configurarGrilla();

        $.ajax({
            type: 'POST',
            url: baseUrl + 'Carga/Contrato/ListaAdendas?codContrato=' + codContrato + '&campos=&valores=&pagina=' + pagina + '&nregistros=' + nregistros,
            data: null,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (!result.Error) {
                    $("#tblCargaAdendas").DataTable({
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
                            { "title": "DOC SOLICITUD", "data": "DOC_SOLICITUD", "class": "text-center" },
                            { "title": "HOJA SOLICITUD", "data": "HOJA_RUTA", "class": "text-center", "visible": true, },
                            { "title": "DOCUMENTO", "data": "DOCUMENTO", "class": "text-center", "visible": true, },
                        ],
                        "columnDefs": [
                            {
                                'targets': 0,
                                'render': function (data, type, row, meta) {
                                    var html = '';
                                    html = '<button  class="btn btn-xs"  onClick="ContratoAdendas.OpenMantenimiento(\'' + row.ID_CARGA_ADENDA + '\');" type="button" title="Actualizar Contrato"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                                    html += '<button  class="btn btn-xs"  onClick="ContratoAdendas.Eliminar(\'' + row.ID_CARGA_ADENDA + '\');" type="button" title="Eliminar Contrato"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                                    return html;
                                }
                            }
                        ],
                    });
                }
            }
        });
    };
    var OpenMantenimiento = function (idCargaAdenda) {
        jQuery("#modalMantenimientoAdendas").load(baseUrl + "Carga/Contrato/OpenMantenimientoAdendas?idCargaAdenda=" + idCargaAdenda, function (responseText, textStatus, request) {
            $("#modalMantenimientoAdendas").modal("show");
        });
    }
    var Eliminar = function (idCargaAdenda) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea eliminar la adenda seleccionada?", (rpta) => {

            if (rpta) {

                $.ajax({
                    type: 'POST',
                    url: baseUrl + 'Carga/Contrato/EliminarAdendas?idAdenda=' + idCargaAdenda,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result.Data) {
                            DESARROLLO.PROCESO_CORRECTO("Registro eliminado correctamente");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se eliminó el registro");
                        }
                    }
                })

            }

        })
    }
    
    return {
        Iniciar: function (codContrato) { Iniciar(codContrato); },
        OpenMantenimiento(idCargaAdenda) { OpenMantenimiento(idCargaAdenda) },
        Eliminar(idCargaAdenda) { Eliminar(idCargaAdenda) }
    }
}();
