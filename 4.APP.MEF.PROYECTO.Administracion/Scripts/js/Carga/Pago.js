$(function () {
    /*$(document).unbind("keydown");*/
    LoadTipoCarga();
    ComboAno();
    $("#btnProcesarPago").hide();
    $("#btnQuitarArchivoPago").hide();
    $("#DdlTreporte").select2();
    DropZoneInit();
})

function LoadTipoCarga() {
    let data = {};
    EnviarCombo(data, baseUrl + "Carga/Carga/GetTipoFormatoPago", "#DdlTreporte");

    $("#span_titulo").html($("#DdlTreporte option:selected").text());
    
    var valFormato = $("#DdlTreporte").val();
    if (valFormato == null) valFormato = 'FAG';
    //tableBody("/Carga/Carga/LoadCamposFormatos?tipoDocumento=" + valFormato, '#tblFormatoPagoBody');
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
            for (var i = 0; i < data.length; i++) {
                html += "<option value='" + data[i].Id + "'>" + data[i].Descripcion + "</option>";
            }
            $(id).html(html);
        }
    });
};
var OpenModal = function () {
    jQuery("#modalFormatoPago").html('');
    jQuery("#modalFormatoPago").load(baseUrl + "Carga/Carga/ProcesoPago", function (responseText, textStatus, request) {
        if (textStatus === "success") {
            $("#modalFormatoPago").modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header", cursor: "move"
            })
        }
    });

    //$("#modalFormatoPago").modal('show');
};
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
function ComboAno() {
    var n = (new Date()).getFullYear()
    var select = document.getElementById("DdlPeriodoPago");
    for (var i = n; i >= 2000; i--)select.options.add(new Option(i, i));
};

var loadingOpen = function () {
    $(".container_spinner").removeClass("hide");
    $(".container_spinner").addClass("show");
};
var loadingClose = function () {
    $(".container_spinner").removeClass("show");
    $(".container_spinner").addClass("hide");
}
var DropZoneInit = function () {
    Dropzone.autoDiscover = false;
    var myDropzone = new Dropzone("#dropzonePagoDiv", {
        paramName: "file", // Nombre del parámetro que contiene el archivo en tu controlador
        maxFilesize: 10, // Tamaño máximo del archivo en MB
        acceptedFiles: ".xls,.xlsx", //Formatos permitidos
        init: function () {
            this.on("success", function (file, response) {
                // Manejar la respuesta del servidor después de cargar el archivo
                console.log(response);
            });
        }
    });
}
var SendFile = function () {
    var file = $("#fileArchivoPago")[0].files[0];
    var formData = new FormData();
    formData.append("file", file);

    loadingOpen();

    $.ajax({
        url: "/Carga/Carga/CargarArchivo?tipoDocumento=" + $("#DdlTreporte").val() + "&periodo=" + $("#DdlPeriodoPago").val(),
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (datos) {
            let data = JSON.parse(datos);
            console.log(data);
            console.log(data.ListaErrores);

            if (data.Status) {
                DESARROLLO.PROCESO_CORRECTO("Carga Correcta");
            } else {
                DESARROLLO.PROCESO_ALERT("Carga Incorrecta. Verifique excel de errores");

                var url = baseUrl + 'Carga/Carga/ExportarExcelErrores' + "?errores=" + JSON.stringify(data.ListaErrores);
                var iframe = $("<iframe/>").hide().appendTo("body");
                iframe.attr("src", url);
            }

            EnviarCombo({}, baseUrl + 'Carga/Carga/GetCargas', '#DcboCargas');
            $("#DcboCargas").select2();

            loadingClose();

            $("#btnQuitarArchivoPago").click();
        }
    });
};
$("#icoExcelPago").click(function () {
    $("#fileArchivoPago").click();
});
$("#btnQuitarArchivoPago").click(function () {
    $("#fileArchivoPago").val("");
    $("#lblArchivoCargadoPago").html("Click o arrastra tu archivo al ícono de Excel. <br />Ningún Archivo Cargado");
    $("#btnProcesarPago").hide();
    $("#btnQuitarArchivoPago").hide();
});
$("#btnVerFormatoPago").click(function () {
    OpenModal();    
});
$("#btnSalirModal").click(function () {
    $("#modalFormatoPago").modal('hide');
});
$("#fileArchivoPago").change(function (e) {
    var val = $(this).val();
    var file = this.files[0];
    var fileName = e.target.files[0].name;;

    if (file.size > 11534300) {
        $(this).val('');
        DESARROLLO.PROCESO_ALERT('Alerta', "El archivo que está subiendo pesa más de 10Mb");
        return false
    }

    if (val.substring(val.lastIndexOf('.') + 1).toLowerCase() == 'xlsx' || val.substring(val.lastIndexOf('.') + 1).toLowerCase() == 'xls') {
        $('#lblArchivoCargadoPago').html("<b>" + fileName + "</b>");
        $("#btnProcesarPago").show();
        $("#btnQuitarArchivoPago").show();
    } else {
        DESARROLLO.PROCESO_ALERT('Alerta', "Formato de archivo no válido, solo se permite archivos EXCEL");
        $("#btnQuitarArchivoPago").click();
    }
});
$("#btnProcesarPago").click(function () {
    SendFile();
});