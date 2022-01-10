Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Imports System.DateTime


Partial Class QuizSetting
    Inherits System.Web.UI.Page
    Private connectionString As String = WebConfigurationManager.ConnectionStrings("QUIZ_DB").ConnectionString

    Private Sub cmd_SaveSetting_Click(sender As Object, e As EventArgs) Handles cmd_SaveSetting.Click
        If Page.IsValid Then
            Dim isQuestionShuffle As Boolean
            Dim isAnswerShuffle As Boolean
            Dim isNegativeMarking As Boolean

            If radiobtn_isQuestionShuffleTrue.Checked Then
                isQuestionShuffle = True
            Else
                isQuestionShuffle = False
            End If

            If radiobtn_isAnswerShuffleTrue.Checked Then
                isAnswerShuffle = True
            Else
                isAnswerShuffle = False
            End If


            If radiobtn_isNegativeMarkingTrue.Checked Then
                isNegativeMarking = True
            Else
                isNegativeMarking = False
            End If
            If IsSettingExisting() Then
                lbl_showErrorMessages.Text &= "Yes you can insert the Settings!"



                insertSetting(Integer.Parse(txtboxNumberOfQuestion.Text),
                              isQuestionShuffle, isAnswerShuffle, DateTime.Parse(txtbox_startingTime.Text),
                              Integer.Parse(txtbox_QuizDuration.Text), isNegativeMarking, Integer.Parse(txtbox_NoOfQuestionToBeShown.Text))


            Else
                updateSetting(Integer.Parse(txtboxNumberOfQuestion.Text),
                              isQuestionShuffle, isAnswerShuffle, DateTime.Parse(txtbox_startingTime.Text),
                              Integer.Parse(txtbox_QuizDuration.Text), isNegativeMarking, Integer.Parse(txtbox_NoOfQuestionToBeShown.Text))


            End If
        End If

    End Sub


    Private Sub insertSetting(numberOfQuestions As Integer, isQuestionShuffle As Boolean, isAnswerShuffle As Integer,
                              startingTime As DateTime, durationOfQuiz As Integer, isNegativeMarking As Boolean, noOfQuestionsToBeShown As Integer)


        Dim insertSQL As String
        insertSQL = "INSERT INTO Quiz_Setting(NoOfQuestions, isShuffled, isAnswerShuffled,"
        insertSQL &= " timeLimit, noOfQuestionsShown, isNegativeMarking, startTime)"
        insertSQL &= "VALUES ("
        insertSQL &= "@numberOfQuestions, @isQuestionShuffle, @isAnswerShuffle,"
        insertSQL &= "@durationOfQuiz, @noOfQuestionsToBeShown, @isNegativeMarking,"
        insertSQL &= "@startingTime"
        insertSQL &= ")"



        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@numberOfQuestions", numberOfQuestions)
        cmd.Parameters.AddWithValue("@isQuestionShuffle", isQuestionShuffle)
        cmd.Parameters.AddWithValue("@isAnswerShuffle", isAnswerShuffle)
        cmd.Parameters.AddWithValue("@durationOfQuiz", durationOfQuiz)
        cmd.Parameters.AddWithValue("@noOfQuestionsToBeShown", noOfQuestionsToBeShown)
        cmd.Parameters.AddWithValue("@isNegativeMarking", isNegativeMarking)
        cmd.Parameters.AddWithValue("@startingTime", startingTime)
        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            lbl_showErrorMessages.Text = "<b>Quiz Setting is Successfully inserted!</b>"

        Catch err As Exception
            lbl_showErrorMessages.Text = "<b>The Quiz Setting cannot be inserting at the time!</b> "
            'lbl_showErrorMessages.Text &= err.Message
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub updateSetting(numberOfQuestions As Integer, isQuestionShuffle As Boolean, isAnswerShuffle As Integer,
                              startingTime As DateTime, durationOfQuiz As Integer, isNegativeMarking As Boolean, noOfQuestionsToBeShown As Integer)



        Dim updateSQL As String
        updateSQL = "UPDATE Quiz_Setting Set NoOfQuestions = @numberOfQuestions, isShuffled = @isQuestionShuffle,"
        updateSQL &= " isAnswerShuffled = @isAnswerShuffle,  timeLimit = @durationOfQuiz, noOfQuestionsShown = @noOfQuestionsToBeShown,"
        updateSQL &= " isNegativeMarking = @isNegativeMarking, startTime = @startingTime "
        updateSQL &= "WHERE QuizSettingID = 1"




        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(updateSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@numberOfQuestions", numberOfQuestions)
        cmd.Parameters.AddWithValue("@isQuestionShuffle", isQuestionShuffle)
        cmd.Parameters.AddWithValue("@isAnswerShuffle", isAnswerShuffle)
        cmd.Parameters.AddWithValue("@durationOfQuiz", durationOfQuiz)
        cmd.Parameters.AddWithValue("@noOfQuestionsToBeShown", noOfQuestionsToBeShown)
        cmd.Parameters.AddWithValue("@isNegativeMarking", isNegativeMarking)
        cmd.Parameters.AddWithValue("@startingTime", startingTime)
        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            lbl_showErrorMessages.Text = "<b>Quiz Setting is Successfully Update, as it was already inserted!</b>"

        Catch err As Exception
            lbl_showErrorMessages.Text = "<b>The Quiz Setting cannot be update at the time!</b> "
            lbl_showErrorMessages.Text &= err.Message
        Finally
            con.Close()
        End Try

    End Sub
    Private Sub showSetting()
        Dim SQLStatement As String
        SQLStatement = "SELECT * From Quiz_Setting WHERE QuizSettingID = 1"


        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(SQLStatement, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "QuizSetting")

        Catch err As Exception
            'lbl_showErrorMessages.Text = "Due to some issues, you can't insert the Quiz Settings!"
            lbl_showErrorMessages.Text = err.Message
        Finally
            con.Close()
        End Try


        txtboxNumberOfQuestion.Text = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of Integer)("NoOfQuestions").ToString()
        radiobtn_isQuestionShuffleTrue.Checked = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of Boolean)("isShuffled")
        radiobtn_isAnswerShuffleTrue.Checked = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of Boolean)("isAnswerShuffled")
        txtbox_startingTime.Text = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of DateTime)("startTime")
        txtbox_QuizDuration.Text = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of Integer)("timeLimit")
        txtbox_NoOfQuestionToBeShown.Text = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of Integer)("noOfQuestionsShown")
        radiobtn_isNegativeMarkingTrue.Checked = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of Boolean)("isNegativeMarking")

    End Sub
    Private Function IsSettingExisting() As Boolean
        Dim SQLStatement As String
        Dim QuizSettingCheck As Integer
        SQLStatement = "SELECT Count(*) As records From Quiz_Setting"


        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(SQLStatement, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "QuizSettingCheck")

            QuizSettingCheck = dsQuizDB.Tables("QuizSettingCheck").Rows.Item(0).Field(Of Integer)("records")

        Catch err As Exception
            lbl_showErrorMessages.Text = "Due to some issues, you can't insert the Quiz Settings!"
            Return False
        Finally
            con.Close()
        End Try

        If QuizSettingCheck = 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub insertValidator(validationText As String, ErrorMessage As String, txtboxID As String)
        Dim rqfValidator As RequiredFieldValidator = New RequiredFieldValidator
        rqfValidator.Display = ValidatorDisplay.Dynamic
        rqfValidator.Text = validationText
        rqfValidator.Attributes.Add("runat", "server")
        rqfValidator.ErrorMessage = ErrorMessage
        rqfValidator.ControlToValidate = txtboxID
        rqfValidator.Display = ValidatorDisplay.None
        Me.Form.Controls.Add(rqfValidator)
    End Sub

    Private Sub QuizSetting_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserRole") <> "Admin" Then
                Response.Redirect("AccessDenied.aspx")
            Else
                If Not IsSettingExisting() Then
                    showSetting()
                End If
            End If

        End If

        'RegularExpressionValidator1.Display = ValidatorDisplay.None

        insertValidator("*Number of Questions<br/>", "Number of Question is mandatory", "txtboxNumberOfQuestion")
        insertValidator("*Quiz Duration<br/>", "Quiz Duration is mandatory", "txtbox_QuizDuration")
        insertValidator("*Starting time<br/>", "Starting Time is Mandatory", "txtbox_startingTime")
        insertValidator("*Number OF Question to be Shown<br/>", "Number of Question to be shown are mandatory", "txtbox_NoOfQuestionToBeShown")
    End Sub






End Class
