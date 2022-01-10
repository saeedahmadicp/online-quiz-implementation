<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QuizSetting.aspx.vb" Inherits="QuizSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
     <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtbox_startingTime.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%Y/%m/%d %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to the Quiz Setting Options</h1>
            <span>Enter the Number of Question:</span>
            <asp:TextBox ID="txtboxNumberOfQuestion" runat="server" TextMode="Number"></asp:TextBox>
            <br /><br/>
            <span>Enter the Option For Shuffling the Questions: </span>
            <asp:RadioButton ID="radiobtn_isQuestionShuffleTrue" Text="Yes" Checked="true" GroupName="IsQuestionShuffleOptions" runat="server"/>
            <asp:RadioButton ID="radiobtn_isQuestionShuffleFalse" Text="No" GroupName="IsQuestionShuffleOptions" runat="server"/>
            <br /><br/>
            <span>Enter the Option For Shuffling the Answers(i.e. Options): </span>
            <asp:RadioButton ID="radiobtn_isAnswerShuffleTrue" Text="Yes" Checked="true" GroupName="IsAnswerShuffleOptions" runat="server"/>
            <asp:RadioButton ID="radiobtn_isAnswerShuffleFalse" Text="No" GroupName="IsAnswerShuffleOptions" runat="server"/>
            <br /><br/>
            <span>Enter the starting time for the Quiz: </span>
            <asp:TextBox ID="txtbox_startingTime" runat="server" TextMode="DateTime"></asp:TextBox> <img src="calender.png" />
        
            
            
            <br/><br/>
            <span>Enter the Duration for the Quiz(in minutes): </span>
            <asp:TextBox ID="txtbox_QuizDuration" runat="server" TextMode="Number" ></asp:TextBox>
            <br/> <br/>
            <span>Enter the Number of Question to be shown: </span>
            <asp:TextBox ID="txtbox_NoOfQuestionToBeShown" runat="server" TextMode="Number" ></asp:TextBox>
            <br/> <br/>
            <span>Enter the Option for Negative Markings: </span>
             <asp:RadioButton ID="radiobtn_isNegativeMarkingTrue" Text="Yes" GroupName="IsNegativeMarkingOptions" runat="server"/>
            <asp:RadioButton ID="radiobtn_isNegativeMarkingFalse" Text="No"  Checked="true" GroupName="IsNegativeMarkingOptions" runat="server"/>
            <br /> <br />
            <asp:Button id="cmd_SaveSetting" Text="SaveSettings" runat="server"/>
            <asp:ValidationSummary ID="ValidationSummary1"  runat="server" Style="color:red;text-align:left;" />
            <asp:Label ID="lbl_showErrorMessages" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
