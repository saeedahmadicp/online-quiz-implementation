<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admin.aspx.vb" Inherits="admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
              <h1 id="title" style="text-align:center" runat="server">Welcome To Admin Interface</h1>
            <br />
            <div id="AdminProfile" runat="server"></div>
          <div align="center">
           <asp:Button  ID="btnChangeSetting" Text="Change Settings" runat="server" /> <br /> <br />
            <asp:Label ID="showErrorMessages" runat="server"></asp:Label>
          </div>
        </div>
    </form>
</body>
</html>
