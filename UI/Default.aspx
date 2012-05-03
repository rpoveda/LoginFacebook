<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UI.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:RadioButtonList runat="server" ID="rbTipoRetorno">
        <asp:ListItem Text="Json" Value="Json"/>
        <asp:ListItem Text="Objeto" Value="Objeto"/>
    </asp:RadioButtonList>
    <div>
        <asp:LinkButton runat="server" ID="hlLoginFacebook" Text="Login Facebook" OnClick="hlLoginFacebook_OnClick" />
    </div>
    <hr/>
    <div>
        <span>Dados de retorno</span><br/>
        <asp:Literal runat="server" ID="ltlRetorno"/>
    </div>
    </form>
</body>
</html>
