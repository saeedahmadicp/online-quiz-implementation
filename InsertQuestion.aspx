<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InsertQuestion.aspx.vb" Inherits="InsertQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label id="QuestionsInfo" runat="server" align="center"></asp:Label>
           <h2>Insert a Question</h2>

            <br />
            <span>Confirm Question Type:</span>
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnDesc" Text="Descriptive" Checked="true" AutoPostBack="true" GroupName="QuestionType" runat="server" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnMCQs" Text="MCQs" AutoPostBack="true"   GroupName="QuestionType" runat="server" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnTFs" Text="True/False" AutoPostBack="true"  GroupName="QuestionType" runat="server" />

            <br /> <br />
            <span >Question Statement: </span> &nbsp &nbsp 
            <asp:TextBox ID="txtboxQuestionStatement"  TextMode="MultiLine" runat="server"> </asp:TextBox>
            <br /> <br />
            <span>Complexity Level</span>
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnEasy" Text="Easy" Checked="true" GroupName="ComplexityLevel" runat="server" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnMedium" Text="Medium" GroupName="ComplexityLevel" runat="server" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtnDifficult" Text="Difficult" GroupName="ComplexityLevel" runat="server" />
            <br /> <br />
            <span>Marks</span>
            <asp:TextBox ID="txtboxMarks" runat="server" CausesValidation="False" TextMode="Number"></asp:TextBox>

            <br id="spaceOptionA" visible="false" runat="server"/> 
            <span id="txtbox_OptionATitle" visible="false" runat="server">Enter Option A</span>
            <asp:TextBox ID="txtbox_OptionA" visible="false" runat="server"></asp:TextBox>

            <br  id="spaceOptionB" visible="false" runat="server"/> 
            <span id="txtbox_OptionBTitle" visible="false" runat="server">Enter Option B</span>
            <asp:TextBox ID="txtbox_OptionB" visible="false" runat="server"></asp:TextBox>

            <br  id="spaceOptionC" visible="false" runat="server"/> 
            <span id="txtbox_OptionCTitle" visible="false" runat="server">Enter Option C</span>
            <asp:TextBox ID="txtbox_OptionC" visible="false" runat="server"></asp:TextBox>

            <br  id="spaceOptionD" visible="false" runat="server"/> 
            <span id="txtbox_OptionDTitle" visible="false" runat="server">Enter Option D</span>
            <asp:TextBox ID="txtbox_OptionD" visible="false" runat="server"></asp:TextBox>

            <br  id="spaceCorrectOption" visible="false" runat="server"/> 
            <span id="txtbox_CorrectOptionTitle" visible="false" runat="server" >Enter the Correct Option</span>
            <asp:TextBox ID="txtbox_CorrectOption" visible="false" runat="server"></asp:TextBox>
           
            <br id="spaceTF" visible="false" runat="server" />
            <span  id="TFradiobtn_title" visible="false" runat="server" >Select Correct Option</span>
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtn_TF_True" visible="false" Text="True" Checked="true" GroupName="TFOptions" runat="server" />
            &nbsp &nbsp &nbsp
            <asp:RadioButton ID="radiobtn_TF_False" visible="false" Text="False" GroupName="TFOptions" runat="server" />


            <br /> <br />
            <asp:Button ID="btnInsertQuestion" runat="server" Text="Insert Question"/>
             &nbsp &nbsp
            <asp:Button ID="btnClear" runat="server" Text="Clear"/>

             <!-- Validation Summary -->
            <asp:ValidationSummary ID="ValidationSummary1"  runat="server" Style="color:red;text-align:left;" />
            <asp:Label id="lbl_showErrorMessages" runat="server"></asp:Label>

            
        </div>
    </form>
</body>
</html>
