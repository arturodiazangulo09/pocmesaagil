$(function () {
    $("#span_titulo").html($("#DdlTreporte option:selected").text());

    var valFormato = $("#DdlTreporte").val();
    if (valFormato == null) valFormato = 'FAG';
    tableBody(baseUrl + '/Carga/Carga/LoadCamposFormatos?tipoDocumento=' + valFormato, '#tblFormatoPagoBody');
})
function tableBody(ruta, id) {
    $.ajax({
        type: 'POST',
        url: ruta,
        data: null,
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            let html = "";
            let data = JSON.parse(result);
            for (var i = 0; i < data.length; i++) {
                html += '<tr>';
                html += '<td> ' + (i + 1) + '</td>';
                html += '<td>' + data[i].Descripcion + '</td>';
                html += '<td>' + (data[i].Obligatorio ? 'SI' : 'NO') + '</td>';
                html += '<td>' + data[i].Ejemplo + '</td>';
                html += '</tr>';

            }

            $(id).html(html);

            $('#tblCamposPago').dataTable({
                retrieve: true,
                autoWidth: true,
                searching: false,
                responsive: true,
                ordering: false,
                paging: false,
                scrollY: 320,
                Processing: "Cargando...",
                info: false,
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
                },
            });
        }
    });
}

$("#btnSalirModal").click(function () {
    $("#modalFormatoPago").modal('hide');
})
$("#btnDescargarFormato").click(function () {
    var valFormato = $("#DdlTreporte").val();
    var url = baseUrl + 'Carga/Carga/DescargaFormato' + "?tipoFormato=" + valFormato;
    var iframe = $("<iframe/>").hide().appendTo("body");
    iframe.attr("src", url);
});