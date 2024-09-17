var TableCargas_
var TableCargaCabDet_;
var TableCargaAdenda_;
var TableCargaPago_;
var TableCargaSalario_;


var Carga = function () {
    var cargaDetalleEnt;
    var cargaSalarioEnt;

    var Iniciar = function () {
        $("#btnProcesarCarga").hide();
        $("#btnQuitarArchivo").hide();
        $("#divPeriodo").hide();
        //$("#tblCabeceraCargas").DataTable();
        CrearGrillaCargaContrato();
        CrearGrillaAdenda();
        CrearGrillaSalario();
        CrearGrillaCargaHistorial();
        CargarGrillaCargaHistorico();
        Remove_RemoverClases("liCargaDatos");
        LoadTipoCarga();
        EnviarCombo({}, baseUrl + 'Carga/Carga/GetCargas', '#DcboCargas');
        $("#DcboCargas").select2();
        //DropZoneInit();
    };
    var LoadTipoCarga = function () {
        let data = {};
        EnviarCombo(data, baseUrl + "Carga/Carga/GetTipoFormato", "#DdlTreporte");
    };
    var LoadTipoCargaDescarga = function () {
        let data = {};
        EnviarCombo(data, baseUrl + "Carga/Carga/GetTipoFormato", "#lstFormatoDescarga");
    };
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
    var OpenModal = function () {
        jQuery("#modalFormatoPago").load(baseUrl + "Carga/Carga/ProcesoPago", function (responseText, textStatus, request) {

        });
    };

    var OpenModalAdenda = function (IdRegistro) {
        jQuery("#modalAdendas").load(baseUrl + "Carga/Carga/VerAdenda?IdRegistro=" + IdRegistro, function (responseText, textStatus, request) {

        });
    };

    var OpenModalCargaDetalle = function (idCargaDetalle) {
        jQuery("#modalCargaDetalle").load(baseUrl + "Carga/Carga/MantenimientoDetalle", function (responseText, textStatus, request) {
            if (textStatus == 'success') {
                ObtenerCargaDetalle(idCargaDetalle);
                $("#modalCargaDetalle").modal("show");
            }
        });
    };

    var OpenModalCargaSalario = function (idCargaSalario) {
        jQuery("#modalCargaSalario").load(baseUrl + "Carga/Carga/MantenimientoSAL", function (responseText, textStatus, request) {
            if (textStatus == 'success') {
                ComboAnio("DdlPeriodoPagoModal");
                EnviarCombo({}, baseUrl + 'Carga/Carga/GetEntidades', '#DdlEntidadModal');
                ObtenerCargaSalario(idCargaSalario);
                $("#modalCargaSalario").modal("show");
            }
        });
    };

    var EliminarCargaDetalle = function (idCargaDetalle) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea eliminar la carga seleccionada?", (rpta) => {

            if (rpta) {

                loadingOpen();
                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Carga/EliminarCargaDetalle?idCargaDetalle=" + idCargaDetalle,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {

                        if (result) {
                            DESARROLLO.PROCESO_CORRECTO("Adenda Eliminada Correctamente");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se eliminó la Adenda");
                        }

                        CargarGrillaCargaCabDet($("#DcboCargas").val(), 'DET');

                        loadingClose();
                    }
                });

            }
        });
    };
    var formatearFecha = function (fecha) {
        console.log(fecha);
        if (fecha != "" && fecha != null) {
            let f = fecha.split(' ')[0].split('/');
            return f[2] + "-" + f[1].toString().padStart(2, '0') + "-" + f[0].toString().padStart(2, '0');
        }
        return null;
    };
    var ObtenerCargaDetalle = function (idCargaDetalle) {
        loadingOpen();
        $.ajax({
            type: 'POST',
            url: baseUrl + "Carga/Carga/ObtenerCargaDetalle?idCargaDetalle=" + idCargaDetalle,
            data: null,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                console.log('entidad = ', result);
                cargaDetalleEnt = result;
                loadingClose();

                if (result.Result) {

                    $("#txtIdCargaDetalle").val(result.ID_CARGA_DETALLE);
                    $("#txtIdCarga").val(result.ID_CARGA);
                    $("#txtFechaSuscripcion").val(result.FEC_SUSCRIPCION);
                    $("#txtCodigoContrato").val(result.COD_CONTRATO);
                    $("#txtDocumentoSolicitud").val(result.DOC_SOLICITUD);
                    $("#txtHojaRuta").val(result.HOJA_RUTA);
                    $("#txtNumero").val(result.NUMERO);
                    $("#txtFechaInicio").val(formatearFecha(result.FEC_INICIO));
                    $("#txtFechaCulminacion").val(formatearFecha(result.FEC_FIN));
                    $("#txtFechaRecepcion").val(formatearFecha(result.FEC_RECEPCION));
                    $("#txtDocumento").val(result.DOCUMENTO);
                }


            }
        });
    }
    var EliminarCargaSalario = function (idCargaSalario) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea eliminar la carga seleccionada?", (rpta) => {

            if (rpta) {

                loadingOpen();
                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Carga/EliminarCargaSalario?idCargaSalario=" + idCargaSalario,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {

                        if (result) {
                            DESARROLLO.PROCESO_CORRECTO("Registro Eliminado Correctamente");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se eliminó el registro");
                        }

                        var text = $("#DcboCargas").find("option:selected").html();
                        const myArray = text.split("-");
                        var tipoDoc = myArray[1];
                        var idCarga = $("#DcboCargas").val();
                        CargarGrillaCargaCabDet(idCarga, tipoDoc);

                        loadingClose();
                    }
                });

            }
        });
    };
    var ObtenerCargaSalario = function (idCargaSalario) {
        loadingOpen();
        $.ajax({
            type: 'POST',
            url: baseUrl + "Carga/Carga/ObtenerCargaSalario?idCargaSalario=" + idCargaSalario,
            data: null,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                cargaSalarioEnt = result;
                loadingClose();
                if (result.Result) {
                    //$("#staticBackdropLabel").html("Mantenimiento de Salario (Basico: " + new Intl.NumberFormat().format(result.HONORARIO) + ")");
                    $("#leyendaSalario").html("Salario básico: " + new Intl.NumberFormat().format(result.HONORARIO));
                    $("#txtIdRegistro").val(result.ID_REGISTRO);
                    $("#DdlPeriodoPagoModal").val(result.PERIODO);
                    $("#TXT_COD_CONTRATO").val(result.COD_CONTRATO);
                    $("#TXT_NUM_DOCUMENTO").val(result.NUM_DOCUMENTO);
                    $("#TXT_RUC_CAS").val(result.RUC_CAS);
                    $("#DdlEntidadModal").val(result.NOM_DEPENDENCIA);
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
    var OpenModalMantenimiento = function (IdRegistro, TipoDoc) {
        console.log("entro a OpenModalMantenimiento");
        console.log("id registro" + IdRegistro);
        console.log("TipoDoc" + TipoDoc);

        if (TipoDoc == 'FAG') {
            jQuery("#MantenimientoFAG").load(baseUrl + "Carga/Carga/MantenimientoFAG?IdRegistro=" + IdRegistro, function (responseText, textStatus, request) {
                if (textStatus == 'success') {
                    EnviarCombo({}, baseUrl + 'Carga/Carga/GetEntidades', '#DdlEntidadModal');
                    EnviarCombo({}, baseUrl + 'Carga/Carga/GetEntidades', '#DdlEntidadModal2');
                    $("#MantenimientoFAG").modal('show');
                    $(".modal-content").draggable({
                        handle: ".modal-header", cursor: "move"
                    })
                }
            });
        } else {
            jQuery("#MantenimientoPAC").load(baseUrl + "Carga/Carga/MantenimientoPAC?IdRegistro=" + IdRegistro, function (responseText, textStatus, request) {
                if (textStatus == 'success') {
                    EnviarCombo({}, baseUrl + 'Carga/Carga/GetEntidades', '#DdlEntidadModal');
                    EnviarCombo({}, baseUrl + 'Carga/Carga/GetEntidades', '#DdlEntidadModal2');
                    $("#MantenimientoPAC").modal('show');
                    $(".modal-content").draggable({
                        handle: ".modal-header", cursor: "move"
                    })
                }
            });
        }

    };
    var DropZoneInit = function () {
        Dropzone.autoDiscover = false;
        var myDropzone = new Dropzone("#dropzoneDiv", {
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
    var loadingOpen = function (mensaje = 'Cargando...') {
        $(".container_spinner").removeClass("hide");
        $("#mensaje_spinner").text(mensaje);
        $(".container_spinner").addClass("show");
    };
    var loadingClose = function () {
        $(".container_spinner").removeClass("show");
        $(".container_spinner").addClass("hide");
    }
    var fn_NuevoEditar = function (idRegistro) {
        console.log("entró a fn_NuevoEditar");
        var text = $("#DcboCargas").find("option:selected").html();
        const myArray = text.split("-");
        var tipoDoc = myArray[1];
        console.log("idRegistro:" + idRegistro);
        console.log("tipoDoc:" + tipoDoc);

        Carga.OpenModalMantenimiento(idRegistro, tipoDoc);
        $("#modalMantenimiento").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    }

    var fn_VerAdenda = function (idRegistro) {
        console.log("entró a fn_VerAdenda");
        Carga.OpenModalAdenda(idRegistro);
        $("#modalAdendas").modal('show');
        $(".modal-content").draggable({
            handle: ".modal-header", cursor: "move"
        })
    }

    var fn_EliminarContrato = function (idRegistro) {
        var text = $("#DcboCargas").find("option:selected").html();
        const myArray = text.split("-");
        var tipoDoc = myArray[1];
        console.log("entró a fn_EliminarEntidad");
        DESARROLLO.PROCESO_CONFIRM("¿Desea eliminar contrato seleccionado?", (rpta) => {
            var idCarga = $("#DcboCargas").val();
            if (rpta) {

                loadingOpen();
                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Carga/EliminarContratos?idRegistro=" + idRegistro,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {

                        if (result) {
                            DESARROLLO.PROCESO_CORRECTO("Carga Eliminada Correctamente");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se eliminó la Carga");
                        }
                        CargarGrillaCargaCabDet(idCarga, tipoDoc);
                        loadingClose();
                    }
                });

            }
        });

    }

    var fn_CerrarMantenimiento = function () {
        var text = $("#DcboCargas").find("option:selected").html();
        var idCarga = $("#DcboCargas").val();
        const myArray = text.split("-");
        var tipoDoc = myArray[1];
        jQuery("#MantenimientoFAG").html('');
        $('#MantenimientoFAG').modal('hide');

        jQuery("#MantenimientoPAC").html('');
        $('#MantenimientoPAC').modal('hide');
        CargarGrillaCargaCabDet(idCarga, tipoDoc);
    }

    var SendFile = function () {
        var file = $("#fileArchivo")[0].files[0];
        var formData = new FormData();
        formData.append("file", file);

        loadingOpen('Procesando Archivo...');

        $.ajax({
             //url: "/Carga/Carga/CargarArchivo?tipoDocumento=" + $("#DdlTreporte").val(),
              url: baseUrl + "/Carga/Carga/CargarArchivo?tipoDocumento=" + $("#DdlTreporte").val() + "&periodo=" + $("#DdlPeriodoPago").val(),
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,

            success: function (datos) {
                let data = JSON.parse(datos);
                console.log(data);
                console.log(data.ListaErrores);

                if (data.Status) {
                    DESARROLLO.PROCESO_CORRECTO("Archivo Procesado Correctamente");
                } else {
                    if (data.ListaErrores.length > 1) {
                        DESARROLLO.PROCESO_ALERT("Carga Incorrecta", "Verifique excel de errores");


                        // Solution 1
                        //var myJsonString = JSON.stringify(data.ListaErrores);
                        //var blob = new Blob([myJsonString], {
                        //    type: "application/vnd.ms-excel;charset=utf-8"
                        //});
                        ///*! @source http://purl.eligrey.com/github/FileSaver.js/blob/master/FileSaver.js */
                        //var saveAs = saveAs || function (e) { "use strict"; if (typeof e === "undefined" || typeof navigator !== "undefined" && /MSIE [1-9]\./.test(navigator.userAgent)) { return } var t = e.document, n = function () { return e.URL || e.webkitURL || e }, r = t.createElementNS("http://www.w3.org/1999/xhtml", "a"), o = "download" in r, a = function (e) { var t = new MouseEvent("click"); e.dispatchEvent(t) }, i = /constructor/i.test(e.HTMLElement) || e.safari, f = /CriOS\/[\d]+/.test(navigator.userAgent), u = function (t) { (e.setImmediate || e.setTimeout)(function () { throw t }, 0) }, s = "application/octet-stream", d = 1e3 * 40, c = function (e) { var t = function () { if (typeof e === "string") { n().revokeObjectURL(e) } else { e.remove() } }; setTimeout(t, d) }, l = function (e, t, n) { t = [].concat(t); var r = t.length; while (r--) { var o = e["on" + t[r]]; if (typeof o === "function") { try { o.call(e, n || e) } catch (a) { u(a) } } } }, p = function (e) { if (/^\s*(?:text\/\S*|application\/xml|\S*\/\S*\+xml)\s*;.*charset\s*=\s*utf-8/i.test(e.type)) { return new Blob([String.fromCharCode(65279), e], { type: e.type }) } return e }, v = function (t, u, d) { if (!d) { t = p(t) } var v = this, w = t.type, m = w === s, y, h = function () { l(v, "writestart progress write writeend".split(" ")) }, S = function () { if ((f || m && i) && e.FileReader) { var r = new FileReader; r.onloadend = function () { var t = f ? r.result : r.result.replace(/^data:[^;]*;/, "data:attachment/file;"); var n = e.open(t, "_blank"); if (!n) e.location.href = t; t = undefined; v.readyState = v.DONE; h() }; r.readAsDataURL(t); v.readyState = v.INIT; return } if (!y) { y = n().createObjectURL(t) } if (m) { e.location.href = y } else { var o = e.open(y, "_blank"); if (!o) { e.location.href = y } } v.readyState = v.DONE; h(); c(y) }; v.readyState = v.INIT; if (o) { y = n().createObjectURL(t); setTimeout(function () { r.href = y; r.download = u; a(r); h(); c(y); v.readyState = v.DONE }); return } S() }, w = v.prototype, m = function (e, t, n) { return new v(e, t || e.name || "download", n) }; if (typeof navigator !== "undefined" && navigator.msSaveOrOpenBlob) { return function (e, t, n) { t = t || e.name || "download"; if (!n) { e = p(e) } return navigator.msSaveOrOpenBlob(e, t) } } w.abort = function () { }; w.readyState = w.INIT = 0; w.WRITING = 1; w.DONE = 2; w.error = w.onwritestart = w.onprogress = w.onwrite = w.onabort = w.onerror = w.onwriteend = null; return m }(typeof self !== "undefined" && self || typeof window !== "undefined" && window || this.content); if (typeof module !== "undefined" && module.exports) { module.exports.saveAs = saveAs } else if (typeof define !== "undefined" && define !== null && define.amd !== null) { define("FileSaver.js", function () { return saveAs }) }

                        //saveAs(blob, "ListaErrores.xls");

                        //Solucion 2
                        var CsvString = "";
                        var Results = data.ListaErrores;
                        //Cabecera
                        CsvString += "FILA,COLUMNA,MENSAJE ERROR,";
                        CsvString += "\r\n";
                        Results.forEach(function (RowItem, RowIndex) {
                         
                            CsvString += RowItem["NroItem"] + ',' + RowItem["Campo"] + ',' + RowItem["Valor"] + ":" + RowItem["Mensaje"] + ',';
                            CsvString += "\r\n";
                        });
                        var encodedUri = encodeURI(CsvString);
                        var link = document.createElement("a");
                        link.setAttribute("href", "data:text/html;charset=iso-8859-1," + escape(CsvString));
                        link.setAttribute("download", "ListaErrores_" + $("#DdlTreporte").val() +".csv");
                        //document.body.appendChild(link);
                        link.click();

                        //CsvString = "data:application/csv," + encodeURIComponent(CsvString);
                        //var x = document.createElement("A");
                        //x.setAttribute("href", CsvString);
                        //x.setAttribute("download", "ListaErrores_" + $("#DdlTreporte").val() +".csv");
                        //document.body.appendChild(x);
                        //x.click();

                        //var url = baseUrl + 'Carga/Carga/ExportarExcelErrores' + "?errores=" + JSON.stringify(data.ListaErrores);
                        //var iframe = $("<iframe/>").hide().appendTo("body");
                        //iframe.attr("src", url);
                    } else {
                        DESARROLLO.PROCESO_ALERT("Carga Incorrecta", JSON.stringify("FILA: " + data.ListaErrores[0].NroItem + '- CAMPO: ' + data.ListaErrores[0].Campo + '- MENSAJE : ' + data.ListaErrores[0].Mensaje));
                    }
                }

                EnviarCombo({}, baseUrl + 'Carga/Carga/GetCargas', '#DcboCargas');
                $("#DcboCargas").select2();

                loadingClose();

                $("#btnQuitarArchivo").click();

            },
            error: function (result, status, error) {
                loadingClose();
                DESARROLLO.PROCESO_ALERT("Error al Cargar", "Ocurrio un error al Cargar archivo " + error);
            }
        });
    };
    var EliminarProcesoCarga = function (data) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea eliminar la carga seleccionada?", (rpta) => {

            if (rpta) {

                loadingOpen();
                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Carga/EliminarCarga?idCarga=" + data,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {

                        if (result) {
                            DESARROLLO.PROCESO_CORRECTO("Carga Eliminada Correctamente");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se eliminó la Carga");
                        }
                        EnviarCombo(null, baseUrl + 'Carga/Carga/GetCargas', '#DcboCargas');
                        $("#DcboCargas").select2();
                        $("#DcboCargas").val("");
                        
                        CargarGrillaCargaCabDet(0, 'DET');
                        CargarGrillaCargaHistorico();
                        loadingClose();
                    }
                });

            }
        });

    };
    var DescargarCarga = function (data) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea descargar la carga? Dependiendo de la cantidad de registros, podría tardar un poco", (rpta) => {

            if (rpta) {
                loadingOpen();

                var url = baseUrl + 'Carga/Carga/DescargarCarga' + "?idCarga=" + data;
                var iframe = $("<iframe/>").hide().appendTo("body");
                console.log(iframe);
                iframe.attr("src", url);
                iframe.on('load', function () {
                    loadingClose();
                })
                loadingClose();
            }

        });
    }
    var ProcesarCargaPendiente = function (data) {
        DESARROLLO.PROCESO_CONFIRM("¿Desea procesar la carga? Dependiendo de la cantidad de registros, podría tardar un poco.", (rpta) => {

            if (rpta) {
                loadingOpen('Procesando Carga...');

                $.ajax({
                    type: 'POST',
                    url: baseUrl + "Carga/Carga/ProcesarCarga?idCarga=" + data,
                    data: null,
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result) {
                            DESARROLLO.PROCESO_CORRECTO("Carga Procesada Correctamente");
                        } else {
                            DESARROLLO.PROCESO_ERROR("No se pudo procesar la Carga");
                        }
                        EnviarCombo(null, baseUrl + 'Carga/Carga/GetCargas', '#DcboCargas');
                        $("#DcboCargas").select2();
                        $("#DcboCargas").val("");
                        CargarGrillaCargaCabDet(0, 'DET');
                        loadingClose();
                    }
                });
            }

        });
    }
    var ModalInit = function () {
        LoadTipoCargaDescarga();
        var valFormato = $("#lstFormatoDescarga").val();
        if (valFormato == null) valFormato = 'FAG';
        tableBody(baseUrl + '/Carga/Carga/LoadCamposFormatos?tipoDocumento=' + valFormato, '#tblFormatoBody')
    }
    var DownloadTemplate = function () {
        var valFormato = $("#lstFormatoDescarga").val();
        var url = baseUrl + 'Carga/Carga/DescargaFormato' + "?tipoFormato=" + valFormato;
        var iframe = $("<iframe/>").hide().appendTo("body");
        iframe.attr("src", url);
    }
    var tableBody = function (ruta, id) {
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

                $('#tblCampos').dataTable({
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
    var CrearGrillaCargaContrato = function () {
        console.log("entró a CrearGrillaCargaCabDet");
        DESARROLLO.configurarGrilla();
        TableCargaCabDet_ = $("#TableCargaContrato").DataTable({
            retrieve: true,
            ordering: false,
            "paging": false,
            "autoWidth": false,
            "bInfo": false,
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
                        html = '<button  class="btn btn-xs"  onClick="Carga.fn_NuevoEditar(' + row.ID_REGISTRO + ');" type="button" title="Actualizar registro"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button class="btn btn-xs" onClick="Carga.fn_VerAdenda(' + row.ID_REGISTRO + ');" type="button" title="Ver Adenda"><i class="fas fa-solid fa-list"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="Carga.fn_EliminarContrato(' + row.ID_REGISTRO + ');" type="button" title="Eliminar registro"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
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
    }
    var CrearGrillaAdenda = function () {
        DESARROLLO.configurarGrilla();
        TableCargaAdenda_ = $("#TableCargaAdendas").DataTable({
            retrieve: true,
            ordering: false,
            "paging": false,
            "autoWidth": false,
            "bInfo": false,
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
                        html = '<button  class="btn btn-xs"  onClick="Carga.OpenModalCargaDetalle(\'' + row.ID_CARGA_DETALLE + '\');" type="button" title="Actualizar Contrato"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="Carga.EliminarCargaDetalle(\'' + row.ID_CARGA_DETALLE + '\');" type="button" title="Eliminar Contrato"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        return html;
                    }
                }
            ],

        });
    }
    var CrearGrillaSalario = function () {
        DESARROLLO.configurarGrilla();
        TableCargaSalario_ = $("#TableCargaMantenimiento").DataTable({
            retrieve: true,
            ordering: false,
            "paging": false,
            "autoWidth": false,
            "bInfo": false,
            columns: [
                { "title": "ACCIONES", "data": "", "class": "text-center", "visible": true, },
                { "title": "PERIODO", "data": "PERIODO", "class": "text-center", "visible": true, },
                { "title": "COD CONTRATO", "data": "COD_CONTRATO", "class": "text-center", "visible": true, },
                { "title": "NUM DOCUMENTO", "data": "NUM_DOCUMENTO", "class": "text-center" },
                { "title": "RUC CAS", "data": "RUC_CAS", "class": "text-center", "visible": true, },
            ],
            "columnDefs": [
                {
                    'targets': 0,
                    'render': function (data, type, row, meta) {
                        var html = '';
                        html = '<button  class="btn btn-xs"  onClick="Carga.OpenModalCargaSalario(' + row.ID_REGISTRO + ');" type="button" title="Actualizar Contrato"><i class="fas fa-pen-square" style="color:#e30613;font-size:18px"></i></button>';
                        html += '<button  class="btn btn-xs"  onClick="Carga.EliminarCargaSalario(' + row.ID_REGISTRO + ');" type="button" title="Eliminar Contrato"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        return html;
                    }
                }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData['HONORARIO'] > 0) {
                    if (aData['HONORARIO'] < aData['ING_ENE'] ||
                        aData['HONORARIO'] < aData['ING_FEB'] ||
                        aData['HONORARIO'] < aData['ING_MAR'] ||
                        aData['HONORARIO'] < aData['ING_ABR'] ||
                        aData['HONORARIO'] < aData['ING_MAY'] ||
                        aData['HONORARIO'] < aData['ING_JUN'] ||
                        aData['HONORARIO'] < aData['ING_JUL'] ||
                        aData['HONORARIO'] < aData['ING_AGO'] ||
                        aData['HONORARIO'] < aData['ING_SET'] ||
                        aData['HONORARIO'] < aData['ING_OCT'] ||
                        aData['HONORARIO'] < aData['ING_NOV'] ||
                        aData['HONORARIO'] < aData['ING_DIC']
                    ) {
                        $('td', nRow).css('background-color', '#fff39b');
                    }
                }
            }
        });
    }
    var CrearGrillaCargaHistorial = function () {
        DESARROLLO.configurarGrilla();
        TableCargas_ = $("#TableCargaHistorico").DataTable({
            retrieve: true,
            ordering: false,
            "paging": true,
            "autoWidth": false,
            "bInfo": false,
            columns: [
                { "title": "ACCIONES", "data": "", "class": "text-center", "visible": true, },
                { "title": "COD CARGA", "data": "ID_CARGA", "class": "text-center", "visible": true, },
                { "title": "FORMATO", "data": "TIPO_DOC", "class": "text-center", "visible": true, },
                { "title": "FECHA", "data": "FECHA_REGISTRO", "class": "text-center", "visible": true, },
                { "title": "NRO REGISTROS", "data": "NRO_REGISTROS", "class": "text-center", "visible": true, },
                { "title": "ESTADO", "data": "ESTADO", "class": "text-center", "visible": true, },
            ],
            "columnDefs": [
                {
                    'targets': 0,
                    'render': function (data, type, row, meta) {
                        var html = '';
                        
                         html = '<button  class="btn btn-xs"  onClick="Carga.DescargarCarga(' + row.ID_CARGA + ');" type="button" title="Descargar Carga"><i class="far fa-file-excel" style="color:#009900;font-size:18px"></i></button>';
                        if (row.FLG_ESTADO == 1) {
                        html += '<button  class="btn btn-xs"  onClick="Carga.EliminarProcesoCarga(' + row.ID_CARGA + ');" type="button" title="Eliminar Carga"><i class="fas fa-times-circle" style="color:#575756;font-size:18px"></i></button>';
                        }
                      return html;
                    }
                }
            ],

        });
    }

    var DescargarErrores = function (data) {
        $.paramcustom = {
            url: baseUrl + "Carga/Carga/ExportarExcelErrores" + "?errores=" + JSON.stringify(data),
            values: {
                TIPO_PROCESO: "P",
                IDEDOCODIGO: $("#DdlEstado").val(),
                NUM_PROCESO: $("#txtTdr").val(),
                DENOMINACION_PUESTO: $("#txtDesc").val(),
                FECHA_REGISTRO: $("#FechaCreacion").val(),
            }
        }
        $.redirect();
    }

    var CargarGrillaCargaCabDet = function (idCarga, tipoDoc, pValidar = true) {
        var item =
        {
            ID_CARGA: idCarga,
            TIPO_DOC: tipoDoc
        };

        var url = baseUrl + 'Carga/Carga/ListaContratosCabDet';
        var respuesta = DESARROLLO.Ajax(url, item, false);
        TableCargaCabDet_.clear().draw();
        TableCargaAdenda_.clear().draw();
        TableCargaSalario_.clear().draw();


        console.log("tipo doc:" + tipoDoc);
        if (tipoDoc == "DET") {
            $('#tablaContratos').css("display", "none");
            $('#tablaMantenimiento').css("display", "none");
            $('#tablaAdendas').css("display", "block");
            if (respuesta != null && respuesta != "") {
                TableCargaAdenda_.rows.add(respuesta).draw();
            }
        } else if (tipoDoc == "SAL") {
            $('#tablaContratos').css("display", "none");
            $('#tablaAdendas').css("display", "none");
            $('#tablaMantenimiento').css("display", "block");         
            if (respuesta != null && respuesta != "") {
                //Validando el monto de Honorarios
                if (validarHonorario(respuesta) > 0 && pValidar) {
                    DESARROLLO.PROCESO_ALERT('Alerta', "Existen registros, donde el importe de Ingreso supera el importe de honorarios. Por favor verifique los registros de color amarillo.");
                }
                TableCargaSalario_.rows.add(respuesta).draw();
            }
        } else {

            $('#tablaContratos').css("display", "block");
            $('#tablaAdendas').css("display", "none");
            $('#tablaMantenimiento').css("display", "none");
            if (respuesta != null && respuesta != "") {
                TableCargaCabDet_.rows.add(respuesta).draw();
            }
        }


    }


    var CargarGrillaCargaHistorico = function () {
      
        var url = baseUrl + 'Carga/Carga/ListaCargaHistorica';
        var respuesta = DESARROLLO.Ajax(url, null, false);

        TableCargas_.clear().draw();
        if (respuesta != null && respuesta != "") {
            TableCargas_.rows.add(respuesta).draw();
        }
    


    }

    return {
        Iniciar: function () { Iniciar();
            },
        LoadTipoCarga: function () { return LoadTipoCarga(); },
        OpenModal: function () { OpenModal(); },
        OpenModalMantenimiento: function (idRegistro, tipoDoc) { OpenModalMantenimiento(idRegistro, tipoDoc); },
        OpenModalAdenda: function (idRegistro) { OpenModalAdenda(idRegistro); },

        OpenModalCargaDetalle: function (idCargaDetalle) { OpenModalCargaDetalle(idCargaDetalle); },
        EliminarCargaDetalle: function (idCargaDetalle) { EliminarCargaDetalle(idCargaDetalle); },
        ObtenerCargaDetalle: function (idCargaDetalle) { ObtenerCargaDetalle(idCargaDetalle); },
        ProcesarCargaPendiente: function (idCarga) { ProcesarCargaPendiente(idCarga); },
        EnviarArchivo: function () { SendFile(); },
        ModalIniciar: function () { ModalInit(); },
        DescargarFormato: function () { DownloadTemplate(); },
        TableBody: function (ruta, id) { tableBody(ruta, id); },
        EliminarProcesoCarga: function (data) { EliminarProcesoCarga(data); },
        DescargarCarga: function (data) { DescargarCarga(data); },
        CargarGrillaCargaCabDet: function (idCarga, tipoDoc, pValidar) { CargarGrillaCargaCabDet(idCarga, tipoDoc, pValidar); },
        CrearGrillaAdenda: function () { CrearGrillaAdenda(); },
        CrearGrillaSalario: function () { CrearGrillaSalario(); },
        CrearGrillaCargaContrato: function () { CrearGrillaCargaContrato(); },
        DescargarErrores: function (data) { DescargarErrores(data); },
        fn_NuevoEditar: function (idRegistro) { fn_NuevoEditar(idRegistro); },
        fn_VerAdenda: function (idRegistro) { fn_VerAdenda(idRegistro); },
        fn_CerrarMantenimiento: function () { fn_CerrarMantenimiento(); },
        fn_EliminarContrato: function (idRegistro) { fn_EliminarContrato(idRegistro); },
        OpenModalCargaSalario: function (idCargaSalario) { OpenModalCargaSalario(idCargaSalario); },
        EliminarCargaSalario: function (idCargaSalario) { EliminarCargaSalario(idCargaSalario); },
        ObtenerCargaSalario: function (idCargaSalario) { ObtenerCargaSalario(idCargaSalario); },
    }
}();

function ComboAnio(cboControl) {
    var n = (new Date()).getFullYear()
    var select = document.getElementById(cboControl);
    for (var i = n; i >= 2009; i--)select.options.add(new Option(i, i));
};

$("#btnEliminarCarga").click(function () {
    Carga.EliminarProcesoCarga($("#DcboCargas").val());
})
$("#btnVerFormato").click(function () {
    Carga.OpenModal();
    $("#modalFormatoPago").modal('show');
    $(".modal-content").draggable({
        handle: ".modal-header", cursor: "move"
    })
});

$("#btnProcesarCargaPendiente").click(function () {
    Carga.ProcesarCargaPendiente($("#DcboCargas").val());
});

$("#icoExcel").click(function () {
    $("#fileArchivo").click();
});

$("#btnQuitarArchivo").click(function () {
    $("#fileArchivo").val("");
    $("#lblArchivoCargado").html("Click o arrastra tu archivo al ícono de Excel. <br />Ningún Archivo Cargado");
    $("#btnProcesarCarga").hide();
    $("#btnQuitarArchivo").hide();
});

$("#lstFormatoDescarga").change(function () {
    var valFormato = $("#lstFormatoDescarga").val();
    if (valFormato == null) valFormato = 'FAG';
    Carga.TableBody("/Carga/Carga/LoadValoresdeContratos?tipoDocumento=" + valFormato, '#tblFormatoBody');
});

$("#DdlTreporte").change(function (e) {
    var idFormato = $(this).val();

    if (idFormato == "SAL") {
        ComboAnio("DdlPeriodoPago");
        $("#divPeriodo").show();
    } else {
        $("#divPeriodo").hide();
    }
});

$("#btnProcesarCarga").click(function () {
    if ($("#DdlTreporte").val() == "0" ) {
        DESARROLLO.PROCESO_ALERT('Alerta', "Debe seleccionar el tipo de Formato a cargar ");
    } else {
        Carga.EnviarArchivo();
    } 
});

$('#btnDescargarCarga').click(function () {
    Carga.DescargarCarga($('#DcboCargas').val());
})

$("#fileArchivo").change(function (e) {
    var val = $(this).val();
    var file = this.files[0];
    var fileName = e.target.files[0].name;

    if (file.size > 11534300) {
        $(this).val('');
        DESARROLLO.PROCESO_ALERT('Alerta', "El archivo que está subiendo pesa más de 10Mb");
        return false
    }

    if (val.substring(val.lastIndexOf('.') + 1).toLowerCase() == 'xlsx' || val.substring(val.lastIndexOf('.') + 1).toLowerCase() == 'xls') {
        $('#lblArchivoCargado').html("<b>" + fileName + "</b>");
        $("#btnProcesarCarga").show();
        $("#btnQuitarArchivo").show();
    } else {
        DESARROLLO.PROCESO_ALERT('Alerta', "Formato de archivo no válido, solo se permite archivos EXCEL");
        $("#btnQuitarArchivo").click();
    }
});

$("#DcboCargas").change(function (e) {
    var idCarga = $(this).val();
    console.log(idCarga);
    var text = $(this).find("option:selected").html();
    const myArray = text.split("-");
    var tipoDoc = myArray[1];

    Carga.CargarGrillaCargaCabDet(idCarga, tipoDoc);
});

$("#btnCloseModal").click(function () {
    $('#modalFormatoPago').modal('hide');
});

$("#btnSalirModal").click(function () {
    $("#modalFormatoPago").modal('hide');
})

$("#btnDescargarFormato").click(function () {
    Carga.DescargarFormato($("#DcboCargas").val());
});

function validarHonorario(data){
    var contError = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i].HONORARIO > 0) {
            if (data[i].HONORARIO < data[i].ING_ENE ||
                data[i].HONORARIO < data[i].ING_FEB ||
                data[i].HONORARIO < data[i].ING_MAR ||
                data[i].HONORARIO < data[i].ING_ABR ||
                data[i].HONORARIO < data[i].ING_MAY ||
                data[i].HONORARIO < data[i].ING_JUN ||
                data[i].HONORARIO < data[i].ING_JUL ||
                data[i].HONORARIO < data[i].ING_AGO ||
                data[i].HONORARIO < data[i].ING_SET ||
                data[i].HONORARIO < data[i].ING_OCT ||
                data[i].HONORARIO < data[i].ING_NOV ||
                data[i].HONORARIO < data[i].ING_DIC) {
                contError++;
            }
        }        
    }

    return contError;
}