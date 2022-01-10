<%@ Page Language="VB" AutoEventWireup="false"  CodeFile="instructor.aspx.vb" Inherits="instructure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <h1 id="title" style="text-align:center" runat="server">Welcome To Instructor Interface</h1>
            <br />
            <div id="InstructorProfile" runat="server"></div>
          <div align="center">
           <asp:Button  ID="btnInsertQuestion" Text="Insert Question" runat="server" />
            <asp:Label ID="showErrorMessages" runat="server"></asp:Label>
          </div>
        </div>
    </form>
</body>
</html>
