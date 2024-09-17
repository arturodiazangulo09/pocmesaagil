<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RevisaLog.aspx.cs" Inherits="APP.MEF.EXTRANET.FAG.PAG.RevisaLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Listado de Archivos Log del Sistema</h1>
        </div>
        <div>
            <asp:GridView ID="gvwListaLog" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="Archivo">
                    <ItemStyle Width="500px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Ver">
                        <ItemTemplate>
                            <asp:ImageButton ID="imbVer" runat="server" ImageUrl="~/assets/images/Zoom.png" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
