<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QuizSignUp.aspx.vb" Inherits="QuizSignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" lang="javascript">
    function clearTextBox() {
        document.getElementByID('txtboxFullName').value = "";
        document.getElementByID('txtboxEmail').value = "";
        document.getElementByID('txtboxAccessCode').value = "";
        document.getElementByID('txtboxDepartmentName').value = "";
        document.getElementByID('txtboxPhoneNumber').value = "";
        document.getElementByID('txtboxSemesterNumber').value = "";
        document.getElementByID('txtBoxGradeScale').value = "";
        }
        </script>
        
          <style>
        *{
    margin: 0;
    padding:0;
    box-sizing: border-box;
        }

        body {
    text-align: center;
    background-image: url("back.jpg");

         }

    form {
    display: inline-block;
    background-color: azure;
    border-radius: 10px;
     margin-top: 10px;
    background: rgba(240,255,255,0.9);

    }
    div.transbox {
  margin: 15px;

}

    .gray-btn {
    padding: 10px;
    background-color: white;
    color: black;
    text-align:center;
    font-size: medium;
    border: 2px solid #e7e7e7;
      text-align: center;
  display: inline-block;
}


.gray-btn:hover { background-color: darkturquoise; }
    </style>

</head>
<body>
    <form id="form1" runat="server" style="align-content:center;font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif" >
        <div class="transbox">

           <h1>Create Account</h1>
            <br /> <br />
             <span> Full Name 
            
            </span>
            <asp:TextBox ID="txtboxFullName"  runat="server" style="" Width="230px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqfFullName" runat="server" SetFocusOnError="true" Style=" color:red; width: 16px;" ErrorMessage="Full Name is   
        mandatory"  
            ControlToValidate="txtboxFullName">*</asp:RequiredFieldValidator> 
            
            <!-- Validator for Full Name -->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Style=" color:red;"
                  ControlToValidate="txtboxFullName" ErrorMessage="Please enter a valid name."
                  ValidationExpression="^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$">!</asp:RegularExpressionValidator>
             <br /> <br />
             <span>Address</span> 
            
             <asp:TextBox ID="txtboxAddress"  runat="server" style="" Width="230px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rqfAddress" runat="server" SetFocusOnError="true" Style=" position: absolute;color:red;" ErrorMessage="Address is   
        mandatory" ControlToValidate="txtboxAddress">*</asp:RequiredFieldValidator> 
            <br /> <br />
             <span>Phone Number</span>
            
             <asp:TextBox ID="txtboxPhoneNumber"  runat="server" TextMode="Phone" style="" Width="232px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rqfPhoneNumber" SetFocusOnError="true" runat="server" Style=" position: absolute;color:red;  width: 22px;" ErrorMessage="Phone Number is   
        mandatory"  
            ControlToValidate="txtboxPhoneNumber">*</asp:RequiredFieldValidator> 

              <!-- Validator for Phone Number -->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
    runat="server" ErrorMessage="Please enter a valid Phone Number" SetFocusOnError="true" ControlToValidate="txtboxPhoneNumber" ValidationExpression= "^([0-9\(\)\/\+ \-]*)$">!</asp:RegularExpressionValidator>
            <br /> <br />
             <span >Email Address</span>
             
             <asp:TextBox ID="txtboxEmail"  runat="server"  TextMode="Email" style="" Width="230px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqfEmail" SetFocusOnError="true" runat="server" Style=" position: absolute;color:red; width: 12px;" ErrorMessage="Email is   
        mandatory"  
            ControlToValidate="txtboxEmail">*</asp:RequiredFieldValidator> 

              <!--Validator for Email -->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" runat="server" Style=" position: absolute; height: 22px; width: 177px"   
        ErrorMessage="Please enter a valid Email." ControlToValidate="txtboxEmail"   
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator> 

            <br /> <br />
             <span >Password</span>
        
             <asp:TextBox ID="txtboxPassword1" runat="server" TextMode="Password"  style="" Width="230px" MinLength="8" MaxLength="100"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rqfPassword" runat="server" SetFocusOnError="true" Style=" position: absolute;color:red; " ErrorMessage="Password is mandatory"  
            ControlToValidate="txtboxPassword1">*</asp:RequiredFieldValidator>
             
            <br /> <br />
            <span>Confirm Password
            </span>
             &nbsp;<asp:TextBox ID="txtboxPassword2"  runat="server" TextMode="Password" style="" Width="230px" MinLength="8" MaxLength="100" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqfConfirmPassword" runat="server" Style=" position: absolute;color:red; width: 14px;" ErrorMessage="Please confirm your password."  
            ControlToValidate="txtboxPassword2"  >*</asp:RequiredFieldValidator>
             <br \ />             
            <asp:CompareValidator ID="cmpVPassword" runat="server" SetFocusOnError="true" Style="position: absolute;color:red" ErrorMessage="Passwords don't match."  
        ControlToValidate="txtboxPassword2" ControlToCompare="txtboxPassword1" Display="Dynamic"></asp:CompareValidator> 
            <br /> <br />
            <span>Confirm Role:</span>
            &nbsp &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnAdmin" Checked="true" AutoPostBack="true" Text="Admin" GroupName="Role" runat="server" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnInstructor" AutoPostBack="true" Text="Examiner" GroupName="Role" runat="server" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnStudent" AutoPostBack="true" Text="Student" GroupName="Role" runat="server" />

            <div id="addElements" runat="server"></div>
                        <br id="space11" runat="server"/> 
             <span id="AccessCodeTitle" Visible= "true" runat="server">Access Code:</span>
             
             <asp:TextBox ID="txtboxAccessCode"  runat="server"  Visible="true" TextMode="Password" style="" Width="230px"></asp:TextBox>
           
               <br id="space21"  Visible= "false" runat="server"/> 
             <span id="departmentNameTitle" Visible= "false" runat="server">Department Name:</span>
             
             <asp:TextBox ID="txtboxDepartmentName" Visible= "false" runat="server"  TextMode="SingleLine" style="" Width="230px"></asp:TextBox>
                        <br id="space31" Visible= "false" runat="server"/>
             <span ID="semesterNumberTitle" Visible= "false"  runat="server">Semester Number:</span>
             
             <asp:TextBox ID="txtboxSemesterNumber" Visible= "false" runat="server"  TextMode="Number" style="" Width="230px"></asp:TextBox>
                        <br id="space41" Visible= "false"  runat="server"/> 
             <span Id="gradeScaleTitle" Visible= "false"  runat="server">Enter Grade Scale</span>
             
             <asp:TextBox ID="txtBoxGradeScale"  Visible= "false" runat="server"  TextMode="SingleLine" style="" Width="230px"></asp:TextBox>
                        <br /> <br />
            <asp:Button ID="btnSignUp" runat="server" CssClass="gray-btn" Text="Create Account"/>
             &nbsp &nbsp
            <asp:Button ID="btnClear"  runat="server" AutoPostBack="false"  OnClientClick="clearTextBox()" CssClass="gray-btn" Text="Clear" CausesValidation="False"/>
            <br /><br />

            <asp:Label ID="showErrorMessages" runat="server"></asp:Label>
            <!-- Validation Summary -->
            <asp:ValidationSummary ID="ValidationSummary1"  runat="server" Style="color:red;text-align:left;" />
            <br />
            Already Have Account <a href="Default.aspx"> Sign In</a>
        </div>
    </form>
</body>
</html>

