DESARROLLO = {
    PROCESO_CONFIRM: function (elemento, callback) {
        var result = false;
        Swal.fire({
            title: elemento,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#D80612',
            cancelButtonColor: '#575756',
            cancelButtonText: 'CANCELAR',
            confirmButtonText: 'ACEPTAR'
        }).then((result) => {
            if (result.isConfirmed) {
                if (callback) callback(true);
            }
        })
    },
    PROCESO_CORRECTO: function (elemento) {
        Swal.fire({
            icon: 'success',
            title: 'Proceso Correcto',
            text: elemento,
            footer: '',
            confirmButtonColor: '#c82333',
        })
    },
    PROCESO_ERROR: function (elemento) {
        Swal.fire({
            icon: 'error',
            title: 'Error en el proceso',
            text: elemento,
            footer: '',
            confirmButtonColor: '#c82333',
        })
    },
    PROCESO_ALERT: function (titulo, texto) {
            Swal.fire({
                title: titulo,
                html: texto,
                confirmButtonColor: '#c82333',
            })
    },
    Ajax: function (url, parameters, async, funcionSuccess) {
        var rsp;
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: JSON.stringify(parameters),
            success: function (response) {
                rsp = response;
                if (typeof (funcionSuccess) == 'function') {
                    funcionSuccess(response);
                }
            },
            failure: function (msg) {
                alert(msg);
                rsp = msg;
            },
            error: function (xhr, status, error) {
              //  alert(msg);
                rsp = error;
            }
        });

        return rsp;
    },
    LoadDropDownListItems: function (name, url, parameters, selected) {
        var combo = document.getElementById(name);
        combo.options.length = 0;
        combo.options[0] = new Option("");
        combo.selectedIndex = 0;
        combo.disabled = true;
        var items = "";
        var resultado = DESARROLLO.Ajax(url, parameters, false);
        resultado = resultado.items == undefined ? resultado : resultado.items;
        //console.log(resultado);
        if (resultado != null || resultado != undefined) {
            $.each(resultado, function (index, item) {
                if (item != null)
                    combo.options[index] = new Option(item.DESCRIPCION.trim().length > 80 ? item.DESCRIPCION.trim().substring(0, 77) + "..." : item.DESCRIPCION.trim(), item.ID);
            });
        }
        else {
            items += '<option value = "0">--Sin Datos--</option>';
            $('#' + name).html(items);
        }
        combo.disabled = false;
        if (selected == undefined) selected = 0;
        $('#' + name).val(selected);
    },
    configurarGrilla: function (searching) {
        // Setting datatable defaults
        if (searching == null || searching == "" || searching == undefined) { searching = false };
        //if (pageLength == null || pageLength == "" || pageLength == undefined) { pageLength = 60 };

        $.extend($.fn.dataTable.defaults,
            {
                responsive: true,
                searching: searching,
                autoWidth: false,
                bAutoWidth: false,
                select: true,
                "bLengthChange": true,//esto muestra oculta eñ combo por encima del grid
                "bInfo": true,//esto oculta muestra el texto de la paginacion
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible",
                    "sInfo": "Mostrando registro(s) del _START_ al _END_ de un total de _TOTAL_ registro(s)",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    },
                    "select": {
                        "rows": {
                            "_": "",
                            "1": "",
                            "0": "",
                        }
                    },

                }

            });

    },
    selectrowGrilla: function (value) {
        var table = $('#' + value).DataTable();
        $('#' + value +' tbody').on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });
    },
    FECHA_DATATABLE: function (value) {
        if (value === null) return "";
        var pattern = /Date\(([^)]+)\)/; //date format from server side
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        if (dt.getFullYear() === 9999) return ""; //Control para MaxValue
        return ('0' + dt.getDate()).slice(-2) + "/" + ('0' + (dt.getMonth() + 1)).slice(-2) + "/" + dt.getFullYear();
    },
    ///PROCESO DE VALIDACIÓN INPUT
    VALIDAR_FECHAS_I_F: function (fechaInicial, fechaFinal) {
        var strFechainicial = fechaInicial;
        var strFechafinal = fechaFinal;
        valuesStart = strFechainicial.split("/");
        valuesEnd = strFechafinal.split("/");
        // Verificamos que la fecha no sea posterior a la actual
        var dateStart = new Date(valuesStart[2], (valuesStart[1] - 1), valuesStart[0]);
        var dateEnd = new Date(valuesEnd[2], (valuesEnd[1] - 1), valuesEnd[0]);
        if (dateStart > dateEnd) {
            DESARROLLO.PROCESO_ALERT("Atención", "La fecha de inicio, no puede ser mayor que la fecha final.");
            return false;
        }
    return true;
    },
    SOLONUMEROS: function (e) {
        var val = (document.all);
        var key = val ? e.keyCode : e.which;
        if (key > 31 && (key < 48 || key > 57)) {
            if (val)
                window.event.keyCode = 0;
            else {
                e.stopPropagation();
                e.preventDefault();
            }
        }

    },
    GENERARCAPCHALOGIN: function (id) {
        var url = baseUrl + 'Captcha/VisorLogin?id=' + id;
        return url;
    },
    DESCARGAR_DOCUMENTO_LF: function (ID_LASERFICHE) {
        window.location = baseUrl + 'Laserfiche/ExportarDocRepositorio?ID_LASERFICHE=' + ID_LASERFICHE;
    },
}
  ///PROCESO DE VALIDACIÓN INPUT
function VALIDARSOLONUMEROS(e) {
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
        (e.keyCode == 65 && e.ctrlKey === true) ||
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        return;
    }

    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}
function VALIDARSOLODECIMALES(event) {
    var $this = $(this);
    if ((event.which != 46 || $this.val().indexOf('.') != -1) && ((event.which < 48 || event.which > 57) && (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    var text = $(this).val();
    if (text.length === 18) {
        $(this).val(text + ".")
    }
    if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {
            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
            }
        }, 1);
    }
    if ((text.indexOf('.') == 18 && text.substring(text.indexOf('.')).length > 2)) {
        event.preventDefault();
    }
    if (((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 2) && (event.which != 0 && event.which != 8) && ($(this)[0].selectionStart >= text.length - 2))) {
        event.preventDefault();
    }
}
