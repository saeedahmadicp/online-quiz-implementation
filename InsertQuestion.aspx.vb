Imports System.Web.UI.Control
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration


Partial Class InsertQuestion
    Inherits System.Web.UI.Page
    Public connectionString As String = WebConfigurationManager.ConnectionStrings("QUIZ_DB").ConnectionString

    Private Sub InsertQuestion_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserRole") <> "Examiner" Then
                Response.Redirect("AccessDenied.aspx")
            End If
        End If


        insertValidator("*Question Statement", "Question Statement is mandatory", "txtboxQuestionStatement")
        insertValidator("*Marks", "Marks is mandatory", "txtboxMarks")
        showQuestionsInfo()
        'lbl_showErrorMessages.Text = GetQuestionNumberID().ToString()



    End Sub

    Private Sub btnInsertQuestion_Click(sender As Object, e As EventArgs) Handles btnInsertQuestion.Click

        Dim QuestionType As String
        Dim TF_isTrue As String
        Dim ComplexityLevel As String

        If radiobtnDesc.Checked Then
            QuestionType = "DESC"
        ElseIf radiobtnMCQs.Checked Then
            QuestionType = "MCQ"
        Else
            QuestionType = "TF"
            If radiobtn_TF_True.Checked Then
                TF_isTrue = "True"
            Else
                TF_isTrue = "False"
            End If
        End If

        If radiobtnEasy.Checked Then
            ComplexityLevel = radiobtnEasy.Text

        ElseIf radiobtnMedium.Checked Then
            ComplexityLevel = radiobtnMedium.Text

        ElseIf radiobtnDifficult.Checked Then
            ComplexityLevel = radiobtnDifficult.Text
        End If

        If Page.IsValid Then
            txtboxMarks.Controls.Clear()
            txtboxQuestionStatement.Controls.Clear()
            txtboxMarks.Controls.Clear()

            If QuestionType = "MCQ" Then
                txtbox_OptionA.Controls.Clear()
                txtbox_OptionB.Controls.Clear()
                txtbox_OptionC.Controls.Clear()
                txtbox_OptionD.Controls.Clear()
                txtbox_CorrectOption.Controls.Clear()

            End If

            If (GetTotalNumberOfQuestionsToBeInserted() - GetNumberOfQuestionsInserted()) Then
                If InsertQuestion(txtboxQuestionStatement.Text, QuestionType, ComplexityLevel, Integer.Parse(txtboxMarks.Text)) Then
                    If QuestionType = "MCQ" Then
                        InsertMCQsQuestion(GetQuestionNumberID(), txtbox_OptionA.Text, txtbox_OptionB.Text,
                                       txtbox_OptionC.Text, txtbox_OptionD.Text, txtbox_CorrectOption.Text)

                    ElseIf QuestionType = "TF" Then
                        InsertTFQuestion(GetQuestionNumberID(), TF_isTrue)

                    End If
                End If
            End If
            showQuestionsInfo()
        End If

    End Sub



    Private Sub showQuestionsInfo()
        QuestionsInfo.Text = "<b>The Total Question Already Inserted: &nbsp &nbsp </b>"
        QuestionsInfo.Text &= "<b>" & GetNumberOfQuestionsInserted.ToString() & "</b><br/>"
        QuestionsInfo.Text &= "<b>The Total Question To Be Inserted: &nbsp &nbsp </b>"
        QuestionsInfo.Text &= "<b>" & (GetTotalNumberOfQuestionsToBeInserted() - GetNumberOfQuestionsInserted()).ToString() & "</b><br/><br/>"

    End Sub


    Private Sub radiobtnMCQs_CheckedChanged(sender As Object, e As EventArgs) Handles radiobtnMCQs.CheckedChanged
        spaceOptionA.Visible = True
        txtbox_OptionATitle.Visible = True
        txtbox_OptionA.Visible = True

        spaceOptionB.Visible = True
        txtbox_OptionBTitle.Visible = True
        txtbox_OptionB.Visible = True

        spaceOptionC.Visible = True
        txtbox_OptionCTitle.Visible = True
        txtbox_OptionC.Visible = True

        spaceOptionD.Visible = True
        txtbox_OptionDTitle.Visible = True
        txtbox_OptionD.Visible = True

        spaceCorrectOption.Visible = True
        txtbox_CorrectOptionTitle.Visible = True
        txtbox_CorrectOption.Visible = True

        spaceTF.Visible = False
        TFradiobtn_title.Visible = False
        radiobtn_TF_True.Visible = False
        radiobtn_TF_False.Visible = False

        insertValidator("*Option A", "Option A is mandatory", "txtbox_OptionA")
        insertValidator("*Option B", "Option B is mandatory", "txtbox_OptionB")
        insertValidator("*Option C", "Option C is mandatory", "txtbox_OptionC")
        insertValidator("*Option D", "Option D is mandatory", "txtbox_OptionD")
        insertValidator("*Correct Option", "Correct Option is mandatory", "txtbox_CorrectOption")

    End Sub

    Private Sub radiobtnTFs_CheckedChanged(sender As Object, e As EventArgs) Handles radiobtnTFs.CheckedChanged
        spaceOptionA.Visible = False
        txtbox_OptionATitle.Visible = False
        txtbox_OptionA.Visible = False

        spaceOptionB.Visible = False
        txtbox_OptionBTitle.Visible = False
        txtbox_OptionB.Visible = False

        spaceOptionC.Visible = False
        txtbox_OptionCTitle.Visible = False
        txtbox_OptionC.Visible = False

        spaceOptionD.Visible = False
        txtbox_OptionDTitle.Visible = False
        txtbox_OptionD.Visible = False

        spaceCorrectOption.Visible = False
        txtbox_CorrectOptionTitle.Visible = False
        txtbox_CorrectOption.Visible = False

        spaceTF.Visible = True
        TFradiobtn_title.Visible = True
        radiobtn_TF_True.Visible = True
        radiobtn_TF_False.Visible = True

    End Sub

    Private Sub radiobtnDesc_CheckedChanged(sender As Object, e As EventArgs) Handles radiobtnDesc.CheckedChanged
        spaceOptionA.Visible = False
        txtbox_OptionATitle.Visible = False
        txtbox_OptionA.Visible = False

        spaceOptionB.Visible = False
        txtbox_OptionBTitle.Visible = False
        txtbox_OptionB.Visible = False

        spaceOptionC.Visible = False
        txtbox_OptionCTitle.Visible = False
        txtbox_OptionC.Visible = False

        spaceOptionD.Visible = False
        txtbox_OptionDTitle.Visible = False
        txtbox_OptionD.Visible = False

        spaceCorrectOption.Visible = False
        txtbox_CorrectOptionTitle.Visible = False
        txtbox_CorrectOption.Visible = False

        spaceTF.Visible = False
        TFradiobtn_title.Visible = False
        radiobtn_TF_True.Visible = False
        radiobtn_TF_False.Visible = False


    End Sub

    Private Sub insertValidator(validationText As String, ErrorMessage As String, txtboxID As String)
        Dim rqfValidator As RequiredFieldValidator = New RequiredFieldValidator
        rqfValidator.Display = ValidatorDisplay.Dynamic
        rqfValidator.Text = validationText
        rqfValidator.Attributes.Add("runat", "server")
        rqfValidator.ErrorMessage = ErrorMessage
        rqfValidator.Display = ValidatorDisplay.None
        rqfValidator.ControlToValidate = txtboxID
        Me.Form.Controls.Add(rqfValidator)
    End Sub



    'function for Returning the Total number of questions to be inserted 
    Private Function GetTotalNumberOfQuestionsToBeInserted() As Integer
        Dim SQLStatement As String
        Dim NumberOfQuestionsToBeInserted As Integer
        SQLStatement = "SELECT NoOfQuestions  As NoOfQuestions from Quiz_Setting WHERE QuizSettingID = 1"


        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(SQLStatement, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "QuizSetting")

            NumberOfQuestionsToBeInserted = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of Integer)("NoOfQuestions")

        Catch err As Exception
            lbl_showErrorMessages.Text = err.Message
            Return -1
        Finally
            con.Close()
        End Try

        Return NumberOfQuestionsToBeInserted

    End Function




    'function for Returning the Total number of questions to be inserted 
    Private Function GetNumberOfQuestionsInserted() As Integer
        Dim SQLStatement As String
        Dim NumberOfQuestionsInserted As Integer
        SQLStatement = "SELECT Count(*) as NoOfQuestionsAlreadyInserted from Question"


        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(SQLStatement, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "QuizSetting")

            NumberOfQuestionsInserted = dsQuizDB.Tables("QuizSetting").Rows.Item(0).Field(Of Integer)("NoOfQuestionsAlreadyInserted")

        Catch err As Exception
            lbl_showErrorMessages.Text = err.Message
            Return -1
        Finally
            con.Close()
        End Try

        Return NumberOfQuestionsInserted

    End Function


    Private Function InsertQuestion(QuestionStatement As String, QuestionType As String, ComplexityLevel As String,
                               Marks As Integer) As Boolean

        Dim insertSQL As String
        insertSQL = "Insert into Question(QuestionStatement, QuestionType, ComplexityLevel, Marks) "
        insertSQL &= "VALUES ("
        insertSQL &= "@QuestionStatement, @QuestionType, @ComplexityLevel,"
        insertSQL &= "@Marks"
        insertSQL &= ")"


        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@QuestionStatement", QuestionStatement)
        cmd.Parameters.AddWithValue("@QuestionType", QuestionType)
        cmd.Parameters.AddWithValue("@ComplexityLevel", ComplexityLevel)
        cmd.Parameters.AddWithValue("@Marks", Marks)
        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            lbl_showErrorMessages.Text = "<b>Question is Successfully Inserted</b>"

            Return True

        Catch err As Exception
            lbl_showErrorMessages.Text = "<b>The Quiz Setting cannot be inserting at the time!</b> "
            lbl_showErrorMessages.Text &= err.Message
            Return False
        Finally
            con.Close()
        End Try

    End Function

    Private Sub InsertMCQsQuestion(QuestionNumber As Integer, OptionA As String, OptionB As String,
                                   OptionC As String, OptionD As String, CorrectOption As String)



        Dim insertSQL As String
        insertSQL = "Insert into MCQ_Question(QuestionNumber, OptionA, OptionB, OptionC, OptionD, CorrectOption) "
        insertSQL &= "VALUES ("
        insertSQL &= "@QuestionNumber, @OptionA, @OptionB,"
        insertSQL &= "@OptionC , @OptionD , @CorrectOption"
        insertSQL &= ")"


        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@QuestionNumber", QuestionNumber)
        cmd.Parameters.AddWithValue("@OptionA", OptionA)
        cmd.Parameters.AddWithValue("@OptionB", OptionB)
        cmd.Parameters.AddWithValue("@OptionC", OptionC)
        cmd.Parameters.AddWithValue("@OptionD", OptionD)
        cmd.Parameters.AddWithValue("@CorrectOption", CorrectOption)
        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            lbl_showErrorMessages.Text = "<b>MCQs Question Successfully Inserted</b>"


        Catch err As Exception
            ' lbl_showErrorMessages.Text = "<b>The Quiz Setting cannot be inserting at the time!</b> "
            lbl_showErrorMessages.Text &= err.Message
        Finally
            con.Close()
        End Try


    End Sub

    Private Sub InsertTFQuestion(QuestionNumber As Integer, CorrectOption As String)

        ' insert into TF_Question(QuestionNumber, CorrectOption)
        'Values(1, 'True')

        Dim insertSQL As String
        insertSQL = "insert into TF_Question(QuestionNumber, CorrectOption)"
        insertSQL &= "VALUES ("
        insertSQL &= "@QuestionNumber, @CorrectOption"
        insertSQL &= ")"


        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@QuestionNumber", QuestionNumber)
        cmd.Parameters.AddWithValue("@CorrectOption", CorrectOption)
        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            lbl_showErrorMessages.Text = "<b>TF Question Successfully Inserted</b>"


        Catch err As Exception
            ' lbl_showErrorMessages.Text = "<b>The Quiz Setting cannot be inserting at the time!</b> "
            lbl_showErrorMessages.Text &= err.Message
        Finally
            con.Close()
        End Try

    End Sub


    'function for Returning the Total number of questions to be inserted 
    Private Function GetQuestionNumberID() As Integer
        Dim SQLStatement As String
        Dim QuestionNumberID As Integer
        SQLStatement = "SELECT TOP 1 QuestionNumber FROM Question order by QuestionNumber Desc "


        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(SQLStatement, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "QuestionID")

            QuestionNumberID = dsQuizDB.Tables("QuestionID").Rows.Item(0).Field(Of Integer)("QuestionNumber")

        Catch err As Exception
            lbl_showErrorMessages.Text = err.Message
            Return -1
        Finally
            con.Close()
        End Try

        Return QuestionNumberID

    End Function


End Class
