var TableExpedienteFigital_ = null;
function CrearGrillaDocumento() {
    DESARROLLO.configurarGrilla();
    TableExpedienteFigital_ = $("#TableExpedienteFigital").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": true,
        columns: [

            { "title": "NUMERAL", "data": "NUMERAL", "class": "text-center", "visible": false, },
            { "title": "TIPO_PROCESO", "data": "TIPO_PROCESO", "class": "text-center", "width": "10%", "visible": false, },
            { "title": "ANIO_CONTRATO", "data": "ANIO_CONTRATO", "class": "text-center", "width": "12%", "visible": false,  },
            { "title": "NUM_CONTRATO", "data": "NUM_CONTRATO", "class": "text-left",  "visible": false, },
            { "title": "PROCESO", "data": "ASUNTO", "class": "text-left", },
            { "title": "FORMATO", "data": "FORMATO", "class": "text-left", },
            { "title": "DOCUMENTO", "data": "", "class": "text-center", "width": "2%" },
            { "title": "ID_ARCHIVO", "data": "ID_ARCHIVO", "class": "text-center", "visible": false,},
        ],
        "columnDefs": [
            {
                'targets': 6,
                'render': function (data, type, row, meta) {
                    var html = '-';
                    if (row.ID_ARCHIVO > 0) {
                        html = '<button  class="btn btn-xs"  onClick="fn_DescargarArchivo(' +row.ID_ARCHIVO+' );" type="button" title="Descargar Archivo"><i class="fas fa-download" style="color:#0093F8;font-size:18px"></i></button>';
                    } 

                    return html;
                }
            },

        ],

    });
}
function CargarGrillaDocumento() {
    var item =
    {
        TIPO_PROCESO: $("#DdlProceso").val(),
        ID_ENTIDAD: $("#DcboEntidades").val(),
        NUM_CONTRATO: $("#txtContrato").val(),
        ANIO_CONTRATO: $("#txtAnio").val(),
    };
    var url = baseUrl + 'Solicitudes/ExpedienteDigital/ListaDocumentosContrato';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    //TableSolicitud_.rows().remove();
    TableExpedienteFigital_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableExpedienteFigital_.rows.add(respuesta).draw();
    }
} function fn_DescargarArchivo(ID) {
    jQuery("#myModalDescarga").html('');
    jQuery("#myModalDescarga").load(baseUrl + "Administracion/Entidad/VerArchivo?id=" + ID + "&proceso=AR_CONTRA", function (responseText, textStatus, request) {
    });

}