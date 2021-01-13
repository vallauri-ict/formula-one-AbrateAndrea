<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FormulaOneWebForm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>   
            <asp:Label id ="lblMessaggio" runat="server" Text ="Seleziona una tabella: "> </asp:Label>
            <br /> 
            <asp:DropDownList id="DropDownList" runat="server" OnSelectedIndexChanged="Selection_Change" AutoPostBack="True"> </asp:DropDownList>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
