var TableSolicitud_ = null;
var ModelMeses = ["ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO",
    "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"];

function CrearGrillaSolicitudes() {
    DESARROLLO.configurarGrilla();
    TableSolicitud_ = $("#TableSolicitud").DataTable({
        ordering: false,
        "paging": true,
        "autoWidth": true,
        columns: [
            { "title": "ID_PAGO", "data": "ID_PAGO", "class": "text-center", "visible": false, },
            { "title": "SERIE", "data": "SERIE_COMPROBANTE", "class": "text-center", "width": "10%", "visible": false, },
            { "title": "N° RECIBO POR </BR>HONORARIOS E.", "data": "NR_COMPROBANTE", "class": "text-center", "width": "12%" },
            { "title": "IMPORTE DE </BR> HONRARIOS", "data": "IMPORTE_COMPROBANTE", "class": "text-left", },
            { "title": "FECHA DE EMISIÓN", "data": "FECHA_EMISION", "class": "text-left", },
            { "title": "FECHA PAGO", "data": "FECHA_PAGO", "class": "text-left", },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left", },
            { "title": "DNI / CE", "data": "NUM_DOCUMENTO", "class": "text-left", },
            { "title": "ENTIDAD", "data": "ENTIDAD", "class": "text-left", },
            { "title": "MES PAGO", "data": "NUM_MES", "class": "text-center", },
            { "title": "N° DE PLANILLA DE </BR> LOCACIÓN DE SERVICIOS", "data": "NR_PLANILLA", "class": "text-center", },
        ],
        "columnDefs": [
            {
                "targets": [3],
                render: $.fn.dataTable.render.number(',', '.', 2, 'S/')
            },
            {
                'targets': 9,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = ModelMeses[(row.NUM_MES-1)];
                    return html;
                }
            },
        ],

    });
}
function CargarGrillaSolicitudes() {
    var item =
    {
        TIPO_PROCESO: $("#DdlProcesoConstancia").val(),
        ANIO: $("#anio_txt").val(),
        MES: $("#DdlMes").val(),
    };
    var url = baseUrl + 'AltasBajas/Pdt/ListaPlanillaPDT';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    //TableSolicitud_.rows().remove();
    TableSolicitud_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TableSolicitud_.rows.add(respuesta).draw();
    }
}

