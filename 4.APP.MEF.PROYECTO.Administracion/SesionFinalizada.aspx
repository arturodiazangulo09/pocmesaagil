<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SesionFinalizada.aspx.cs" Inherits="APP.ADMINISTRAR.FAG.PAG.SesionFinalizada" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script type="text/javascript">
        function redireccionar() {
            var url = document.getElementById("divUrl").innerHTML;
            window.locationf = url;
        }
        setTimeout("redireccionar()", 5000); //tiempo expresado en milisegundos
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <h1 class="error">Sistema</h1>
        <hr />
        <hr />
        <h2 class="error">
            <asp:Literal ID="ltrmensaje" runat="server"></asp:Literal>
        </h2>
        <asp:Literal ID="ltrurl" runat="server" ></asp:Literal>
    </form>
</body>
</html>
