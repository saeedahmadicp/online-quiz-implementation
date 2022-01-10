<%@ Page Language="VB" AutoEventWireup="false" CodeFile="student.aspx.vb" Inherits="student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <h1 id="title" style="text-align:center" runat="server">Welcome To Student Interface</h1>
            <br />
            <div id="StudentProfile" runat="server"></div>
          <div align="center">
           <asp:Button  ID="btnStartQuiz" Text="Start Quiz" runat="server" /> <br /> <br />
           <asp:Button  style="text-align:center" ID="btnShowResult" Text="Show Result" runat="server" />
           <asp:Label ID="showErrorMessages" runat="server"></asp:Label>
          </div>

        </div>
    </form>
</body>
</html>
