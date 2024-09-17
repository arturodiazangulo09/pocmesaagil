var Tamanio_Valido = 20971520;
function Remove_RemoverClases(li_seleccionado) {
    
    var input_li = document.getElementById('navbarCollapse').getElementsByTagName('li');
    if (input_li.length > 0) {
        $.each(input_li, function (i, v) {
            if (v.id != "") {
                //$("#" + v.id + " ul").css("display", "none");//QUEDA
                $("#" + v.id).removeClass("menu-open");
                $("#" + v.id + " > a").removeClass("active");
                $("#" + v.id).removeClass("active");
            }
        });
    }
    var Mi_li = $("#" + li_seleccionado);
    if (Mi_li.length > 0) {
         $("#" + Mi_li[0].id).addClass("active");
        var Texto_Padre = "";
        var dsa = Mi_li[0].parentNode.parentNode.id;
        if (Mi_li[0].parentNode.parentNode.id != "navbarCollapse") {
            Texto_Padre = Mi_li[0].parentNode.parentNode.outerText;
            $("#" + Mi_li[0].parentNode.parentNode.id).addClass("menu-open");
            $("#" + Mi_li[0].parentNode.parentNode.id + " > a").addClass("active");
            $("#" + li_seleccionado + " a").addClass("active");
        } else {

        }
        $("#lbNombreMenuPrincipal").text(Texto_Padre);
        $(".lbNombreMenuSecundario").text(Mi_li[0].outerText);

    }
}