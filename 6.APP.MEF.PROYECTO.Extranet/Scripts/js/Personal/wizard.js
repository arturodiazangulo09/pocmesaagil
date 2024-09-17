
$(document).ready(function () {
    showSpinner(3000);
    $('#step1-content').load(baseUrl + 'Usuario/Personal/MisDatos', { ID_PERSONAL: $('#consultorId').val() });
    $('#step2-content').load(baseUrl +'Usuario/Personal/MiCv');
    $('#step3-content').load(baseUrl + 'Usuario/Personal/PropuestaFagWiz', { ID_SOLICITUD: $('#solicitudId').val() });
    /*$('#step4-content').load(baseUrl + 'Usuario/Personal/FormatoWizFag', {
        ID_SOLICITUD: $('#solicitudId').val(),
        FLG_PENSIONISTA_POLICIA:'',
        FLG_PENSIONISTA_ESTADO:'',
        FLG_PENSIONES: '',
        FLG_CHECK_4: '',
        FLG_TERMINOS: '',
    });
    $('#step5-content').load(baseUrl + 'Usuario/Personal/NotificarWizFag', { ID_SOLICITUD: $('#solicitudId').val() });
    */

    //Wizard
    $(".next-step").click(function (e) {
        console.log($(this).attr('id'));
        showSpinner(3000);
        let formIsValid = true;
        if ($(this).attr('id') == 'bntProces2') {
            formIsValid = validateCvForm();
        }
        
        if (!formIsValid) {
            DESARROLLO.PROCESO_ALERT('Completar CV', 'Debe completar este formulario para poder continuar con el siguiente.');
            return;
        }

        var active = $('.wizard .nav-tabs li.active');
        active.next().removeClass('disabled');
        active.addClass('disabled');
        nextTab(active);
    });

    $(".prev-step").click(function (e) {
        var active = $('.wizard .nav-tabs li.active');
        active.prev().removeClass('disabled');
        active.addClass('disabled');
        prevTab(active);
    });
});

function nextTab(elem) {
    $(elem).next().addClass('active').find('a[data-toggle="tab"]').click();
    $(elem).removeClass('active');
}
function prevTab(elem) {
    $(elem).prev().addClass('active').find('a[data-toggle="tab"]').click();
    $(elem).removeClass('active');
}

function validateCvForm() {
    let rowCount = 0;

    rowCount = $('#TableFormacion > tbody > tr > td').length;
    console.log('TableFormacion = ' + rowCount);
    if (rowCount <= 1) { return false; }

    rowCount = $('#TableEspecializacion > tbody > tr > td').length;
    console.log('TableEspecializacion = ' + rowCount);
    if (rowCount <= 1) { return false; }

    rowCount = $('#TableCapacitacion > tbody > tr > td').length;
    console.log('TableCapacitacion = ' + rowCount);
    if (rowCount <= 1) { return false; }

    rowCount = $('#TableExperiencia > tbody > tr > td').length;
    console.log('TableExperiencia = ' + rowCount);
    if (rowCount <= 1) { return false; }

    return true;
}
