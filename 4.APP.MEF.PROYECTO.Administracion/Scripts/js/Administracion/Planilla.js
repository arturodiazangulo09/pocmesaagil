var TablePlanillaPendiente_ = null;
var TablePlanillaGenerada_ = null;
function CrearGrillaPlanillaPendiente() {
    DESARROLLO.configurarGrilla();
    TablePlanillaPendiente_ = $("#TablePlanillaPendiente").DataTable({
        ordering: false,
        "paging": true,
        autoWidth: false,
        columns: [
            { "title": "DNI / CE", "data": "NUM_DOCUMENTO", "class": "text-center", "width": "110px"},
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left", "width": "200px"},
            { "title": "ENTIDAD", "data": "ENTIDAD", "class": "text-left", "width": "250px"},
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left", "width": "250px"},
            { "title": "MARCO NORMATIVO", "data": "DESC_PROCESO", "class": "text-center", "width": "150px"},
            { "title": "PERIODO DE PAGO", "data": "DES_MES", "class": "text-center", "width": "150px"},
            { "title": "N° SERIE RECIBO  </br> HONORARIOS </br> ELECTRÓNICO", "data": "SERIE_COMPROBANTE", "class": "text-center", "width": "100px" },
            { "title": "N° RECIBO POR </br> HONORARIOS </br> ELECTRÓNICO", "data": "NR_COMPROBANTE", "class": "text-center", "width": "100px" },
            { "title": "FECHA DE PAGO", "data": "FECHA_PAGO", "class": "text-center", "width": "100px"},
            { "title": "N° DE PLANILLA </br> DE LOCACIÓN </br> DE SERVICIOS", "data": "NR_PLANILLA", "class": "text-center", "width": "100px"},
            
        ],
        "columnDefs": [
            {
                'targets': 8,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_PAGO);
                    if (html == "31/12/0") {
                        html = '-';
                    }
                    return html;
                }
            },
        ],
   
    });
}
function CargarGrillaPendiente() {
    var item =
    {
        ID_ENTIDAD: $("#DcboEntidadesPago").val(),
        ANIO: $("#txtAnioPago").val(),
        NUM_MES: $("#DdlMesPago").val(),
        NR_PLANILLA:"T",
    };
    var url = baseUrl + 'AltasBajas/Planilla/ListaPeriodoPlanilla';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TablePlanillaPendiente_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TablePlanillaPendiente_.rows.add(respuesta).draw();
    }
}
function fn_CerrarPlanilla() {
    jQuery("#modalPlanilla").html('');
    $('#modalPlanilla').modal('hide');
}
function fn_GenerarPlanilla(Accion, Obs) {
    item = {
        NR_PLANILLA: $("#txtCodigoPlanilla").val(),
        MES_PLANILLA: $("#DdlMesPlanilla").val(),
        FECHA_PAGO: $("#FechaPagoPlanilla").val(),
        ANIO: $("#txtAnioPago").val(),
        NR_MES: $("#DdlMesPago").val(),
        ID_ENTIDAD: $("#DcboEntidades").val(),
    };
    var url = baseUrl + 'AltasBajas/Planilla/UpdAsignarPlanilla';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    if (respuesta != null && respuesta != "") {
        if (respuesta.success) {
            DESARROLLO.PROCESO_CORRECTO(respuesta.message);
            fn_CerrarPlanilla();
            CargarGrillaPendiente();
        } else {
            DESARROLLO.PROCESO_ERROR(respuesta.extra);
        }
    }
    $("#myModalCargando").css("display", "none");
}
function CrearGrillaPlanillaGenerada() {
    DESARROLLO.configurarGrilla();
    TablePlanillaGenerada_ = $("#TablePlanillaGenerada").DataTable({
        ordering: false,
        "paging": true,
        autoWidth: false,
        columns: [
            { "title": "DNI / CE", "data": "NUM_DOCUMENTO", "class": "text-center", "width": "110px" },
            { "title": "APELLIDOS Y NOMBRES", "data": "CONSULTOR", "class": "text-left", "width": "200px" },
            { "title": "ENTIDAD", "data": "ENTIDAD", "class": "text-left", "width": "250px" },
            { "title": "CARGO", "data": "DENOMINACION_PUESTO", "class": "text-left", "width": "250px" },
            { "title": "MARCO NORMATIVO", "data": "DESC_PROCESO", "class": "text-center", "width": "150px" },
            { "title": "N° DE CONTRATO", "data": "CODIGO_CONTRATO", "class": "text-center", "width": "150px" },
            { "title": "PERIODO </br> DE PAGO", "data": "DES_MES", "class": "text-center", "width": "100px" },
            { "title": "N° SERIE RECIBO  </br> HONORARIOS </br> ELECTRÓNICO", "data": "SERIE_COMPROBANTE", "class": "text-center", "width": "100px" },
            { "title": "N° RECIBO POR </br> HONORARIOS </br> ELECTRÓNICO", "data": "NR_COMPROBANTE", "class": "text-center", "width": "100px" },
            { "title": "N° DE PLANILLA", "data": "NR_PLANILLA", "class": "text-center", "width": "100px" },
            { "title": "FECHA DE PAGO", "data": "FECHA_PAGO", "class": "text-center", "width": "100px" },
         
        ],
        "columnDefs": [
            {
                'targets': 10,
                'render': function (data, type, row, meta) {
                    var html = '';
                    html = DESARROLLO.FECHA_DATATABLE(row.FECHA_PAGO);
                    if (html == "31/12/0") {
                        html = '-';
                    }
                    return html;
                }
            },
            
        ],

    });
}
function CargarGrillaGenerada() {
    var item =
    {
        ANIO: $("#txtAnioPlanillaPago").val(),
        MES_PLANILLA: $("#DdlMesPlanillaPago").val(),
        NR_PLANILLA: $("#txtCodigoPlanillaPago").val(),
    };
    var url = baseUrl + 'AltasBajas/Planilla/ListaPeriodoPlanilla';
    var respuesta = DESARROLLO.Ajax(url, item, false);
    TablePlanillaGenerada_.clear().draw();
    if (respuesta != null && respuesta != "") {
        TablePlanillaGenerada_.rows.add(respuesta).draw();
    }
}
function fn_DescargarPlanilla(MES_PLANILLA,ANIO_PLANILLA,CODIGO_PLANILLA) {
    window.open(baseUrl+'Reportes/DescargaPlanilla.aspx?PERIODO_PLANILLA=' + MES_PLANILLA + "&ANIO_PLANILLA=" + ANIO_PLANILLA + "&CODIGO_PLANILLA=" + CODIGO_PLANILLA, '_blank');

}
