<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="QuizLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QuizLogin</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Log into Account</h1>
            <br /> <br />
             <span >Email: </span>
            <asp:TextBox ID="txtboxEmail"  runat="server" TextMode="Email"></asp:TextBox>
             <br /> <br />
             <span >Password</span> 
             <asp:TextBox ID="txtboxPassword"  runat="server" TextMode="Password"></asp:TextBox>
            <br /> <br />
            <span>Confirm Role:</span>
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnAdmin" Text="Admin" GroupName="UserRole" runat="server" Checked="True" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnExaminer" Text="Examiner" GroupName="UserRole" runat="server" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnStudent" Text="Student" GroupName="UserRole" runat="server" />
            <br /> <br />
            <asp:Button ID="btnlogin"  runat="server" Text="Login"/>
             &nbsp &nbsp
            <asp:Button ID="btnClear" runat="server" Text="Clear"/>
            <br /> <br /> 
            <asp:Label ID="lbl_showErrorMessages" runat="server"></asp:Label>
            <br />Don't Have Account <a href="QuizSignUp.aspx"> SignUp</a>
        </div>
    </form>
</body>
</html>
