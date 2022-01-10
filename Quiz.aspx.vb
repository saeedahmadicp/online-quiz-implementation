Imports System.DateTime
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Drawing


Partial Class Quiz
    Inherits System.Web.UI.Page
    'Dim rdButtonChoice As New ArrayList()
    'Dim lblQuestion As New ArrayList()
    Private connectionString As String = WebConfigurationManager.ConnectionStrings("QUIZ_DB").ConnectionString
    Dim Marks As Integer = 0
    Dim rdButtonChoice(100) As RadioButtonList
    Dim correctChoice(100) As String
    Dim arrMark(100) As Integer
    Dim lblQuestion(100) As Label
    Dim txtboxAnswers(100) As TextBox
    Dim isDescriptive(100) As Boolean
    Dim questionsAtPage() As Label
    Dim count As Integer
    Dim toShuffle As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ''''''''''''''''''''''''Reading the Quiz setting'''''''''''''''''''''''
        If Session("UserRole") = "Student" Then
            Dim connection As SqlConnection = New SqlConnection()
            connection.ConnectionString = connectionString
            connection.Open()
            Dim adp As String = "select * from Quiz_Setting;"
            Dim cmdDatabase As SqlCommand = New SqlCommand(adp, connection)
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            Dim dr As SqlDataReader = cmdDatabase.ExecuteReader()
            While dr.Read
                Session("time") = DateTime.Now.AddMinutes(CType(dr("timeLimit"), Double))
                Session("numOfQuestions") = CType(dr("NoOfQuestions"), Byte)
                toShuffle = CType(dr("isShuffled"), Integer) * -1
                Dim Label12 As New Label
                Label12.Text = toShuffle
                showQuestions.Controls.Add(Label12)
            End While
            connection.Close()
        Else
            Response.Redirect("AccessDenied.aspx")
        End If

        Dim connection2 As SqlConnection = New SqlConnection()

        'WRITING ALL THE QUERIES'
        connection2.ConnectionString = connectionString
        connection2.Open()
        Dim adp2 As String
        adp2 = " Select Question.QuestionNumber, QuestionStatement,OptionA, OptionB, OptionC, OptionD,CorrectOption, Marks from Question inner join  MCQ_Question on Question.QuestionNumber= MCQ_Question.QuestionNumber;"

        Dim adp3 As String
        adp3 = " Select Question.QuestionNumber, QuestionStatement,CorrectOption, Marks from Question inner join  TF_Question on Question.QuestionNumber= TF_Question.QuestionNumber;"

        Dim adp4 As String
        adp4 = " Select Question.QuestionNumber, QuestionStatement, QuestionType, Marks from Question inner join  DESC_Question on Question.QuestionNumber= DESC_Question.QuestionNumber;"

        Dim cmdDatabase1 As SqlCommand = New SqlCommand(adp2, connection2)


        'OPENING THE CONNECTION IN CASE IT WASN'T ALREADY OPENED
        If connection2.State <> ConnectionState.Open Then
            connection2.Open()
        End If

        Dim dr2 As SqlDataReader = cmdDatabase1.ExecuteReader()




        '''''''''''''READING ALL THE MCQS''''''''''
        While dr2.Read()
            count = count + 1
            Dim correctChoice1 As String = dr2.GetString(6)
            Dim i As Integer = dr2.GetInt32(0)
            Dim lblQuestion1 As New Label
            Dim arrMark1 As Integer
            lblQuestion1.Text = "<b>" & dr2.GetInt32(0) & "</b>" & ". "
            lblQuestion1.Text &= "<b>" & dr2.GetString(1) & "</b>" & "<br />"
            Dim rdButtonChoice1 As New RadioButtonList
            rdButtonChoice1.Items.Add(dr2.GetString(2))
            rdButtonChoice1.Items.Add(dr2.GetString(3))
            rdButtonChoice1.Items.Add(dr2.GetString(4))
            rdButtonChoice1.Items.Add(dr2.GetString(5))
            arrMark1 = dr2.GetInt32(7)
            arrMark(i) = arrMark1
            rdButtonChoice1.Attributes.Add("runat", "server")
            rdButtonChoice1.Attributes.Add("AutoPostBack", "True")
            rdButtonChoice1.ID = dr2.GetInt32(0)
            lblQuestion(i) = lblQuestion1
            rdButtonChoice(i) = rdButtonChoice1
            correctChoice(i) = correctChoice1
            isDescriptive(i) = False
        End While
        connection2.Close()
        connection2.Open()



        '''''''READING ALL THE TRUE FALSE'''''''''''
        Dim cmdDatabase2 As SqlCommand = New SqlCommand(adp3, connection2)
        Dim dr3 As SqlDataReader = cmdDatabase2.ExecuteReader()
        While dr3.Read()
            'count = count + 1
            Dim correctChoice1 As String = dr3.GetString(2)
            Dim i As Integer = dr3.GetInt32(0)
            Dim arrMark1 As Integer
            Dim lblQuestion1 As New Label
            lblQuestion1.Text = "<b>" & dr3.GetInt32(0) & "</b>" & ". "
            lblQuestion1.Text &= "<b>" & dr3.GetString(1) & "</b>" & "<br />"
            Dim rdButtonChoice1 As New RadioButtonList
            rdButtonChoice1.Items.Add("True")
            rdButtonChoice1.Items.Add("False")
            arrMark1 = dr3.GetInt32(3)
            arrMark(i) = arrMark1
            rdButtonChoice1.Attributes.Add("runat", "server")
            rdButtonChoice1.Attributes.Add("AutoPostBack", "True")
            rdButtonChoice1.ID = dr3.GetInt32(0)
            lblQuestion(i) = lblQuestion1
            rdButtonChoice(i) = rdButtonChoice1
            correctChoice(i) = correctChoice1
            isDescriptive(i) = False

        End While
        connection2.Close()
        connection2.Open()


        Dim cmdDatabase3 As SqlCommand = New SqlCommand(adp4, connection2)
        Dim dr4 As SqlDataReader = cmdDatabase3.ExecuteReader()




        '''''''READING ALL THE DESCRIPTIVE ANSWERS'''''''''''
        While dr4.Read()
            'count = count + 1
            Dim txtboxAnswers1 As New TextBox
            Dim i As Integer = dr4.GetInt32(0)
            Dim arrMark1 As Integer
            Dim lblQuestion1 As New Label
            lblQuestion1.Text = "<b>" & dr4.GetInt32(0) & "</b>" & ". "
            lblQuestion1.Text &= "<b>" & dr4.GetString(1) & "</b>" & "<br />"
            Dim rdButtonChoice1 As New RadioButtonList
            arrMark1 = dr4.GetInt32(3)
            arrMark(i) = arrMark1
            rdButtonChoice1.ID = dr4.GetInt32(0)
            lblQuestion(i) = lblQuestion1
            isDescriptive(i) = True
            txtboxAnswers(i) = txtboxAnswers1
        End While


        'Dim Label12 As New Label
        'Label12.Text = count
        'showQuestions.Controls.Add(Label12)


        'CHECKING IF SHUFFLE EQUALS TO ONE, THEN SHUFFLE!!!
        If toShuffle = 1 Then
            shuffleQuestions()
        End If


        ''''''''ADDING ALL THE CONTROLS DYNAMICALLY''''''''
        For i = 0 To 100
            If lblQuestion(i) IsNot Nothing Then
                showQuestions.Controls.Add(lblQuestion(i))
            End If

            If isDescriptive(i) = True Then
                showQuestions.Controls.Add(txtboxAnswers(i))
            ElseIf isDescriptive(i) = False And rdButtonChoice(i) IsNot Nothing Then
                showQuestions.Controls.Add(rdButtonChoice(i))
            End If
        Next



        ''FINALLY CLOSING THE CONNECTION 2 WE OPENED
        connection2.Close()

    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick

        Dim time1 As TimeSpan = New TimeSpan(0, 0, 0)
        time1 = CType(Session("time"), DateTime) - DateTime.Now
        If time1.Seconds <= 0 And time1.Minutes <= 0 And time1.Hours <= 0 Then

            Label1.Text = "TimeOut!"


        Else

            Label1.Text = "The Remaining time is: " & time1.Hours.ToString() +
                " h:" + time1.Minutes.ToString() + " m:" + time1.Seconds.ToString() + " s"

        End If
    End Sub

    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Dim numberOfQuestions As Integer = Session("noOfQuestions")
        Dim connection3 As SqlConnection = New SqlConnection()
        connection3.ConnectionString = connectionString
        connection3.Open()

        Dim studentID1 As Integer = Session("StudentID")
        For i = 1 To 100
            If isDescriptive(i) = False And rdButtonChoice(i) IsNot Nothing Then
                If rdButtonChoice(i).SelectedValue = correctChoice(i) Then
                    Marks = Marks + arrMark(i)
                    Dim textbox1 As New Label
                    textbox1.Text = Marks

                    showQuestions.Controls.Add(textbox1)

                End If
            ElseIf isDescriptive(i) = True Then

                insertSetting(connection3.ConnectionString, i, 1, txtboxAnswers(i).Text)

            End If
        Next


        Session("Marks") = Marks
        Session("StudentMarks") = Marks
        updateMarks(Marks, Session("StudentID"))
        updateMarks(Marks, Session("StudentID"))
        Dim lblMarks As New Label
        lblMarks.Text = "Marks are: " & Marks
        'lblMarks.Text &= rdButtonChoice(1).SelectedValue
        lblMarks.Attributes.Add("runat", "server")
        showQuestions.Controls.Add(lblMarks)

        Response.Redirect("submittedForm.aspx")

    End Sub
    Protected Sub shuffleQuestions()
        Dim rndLoop As Integer = CInt(Int(10 * Rnd()) + 1)
        For i = 1 To rndLoop
            Dim rndInteger As Integer = CInt(Int(5 * Rnd()) + 1)
            Dim rndInteger2 As Integer = CInt(Int(5 * Rnd()) + 1)

            Dim lblTemp As New Label
            lblTemp = lblQuestion(rndInteger)
            lblQuestion(rndInteger) = lblQuestion(rndInteger2)
            lblQuestion(rndInteger2) = lblTemp

            Dim rdButtonTemp As New RadioButtonList
            rdButtonTemp = rdButtonChoice(rndInteger)
            rdButtonChoice(rndInteger) = rdButtonChoice(rndInteger2)
            rdButtonChoice(rndInteger2) = rdButtonTemp

            Dim arrMark1 As New Integer
            arrMark1 = arrMark(rndInteger)
            arrMark(rndInteger) = arrMark(rndInteger2)
            arrMark(rndInteger2) = arrMark1


        Next
    End Sub
    Private Sub insertSetting(connectionstring As String, QuestionNumber As Integer, StudentID As Integer, Answer As String)


        Dim insertSQL As String
        insertSQL = "insert into DESC_Question(QuestionNumber, StudentID, Answer)"
        insertSQL &= "VALUES ("
        insertSQL &= "@QuestionNumber, @StudentID, @Answer"
        insertSQL &= ")"



        Dim con As New SqlConnection(connectionstring)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@QuestionNumber", QuestionNumber)
        cmd.Parameters.AddWithValue("@StudentID", StudentID)
        cmd.Parameters.AddWithValue("@Answer", Answer)

        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            lbl_showErrorMessages.Text = "<b>Answer is Successfully inserted!</b>"

        Catch err As Exception
            lbl_showErrorMessages.Text = "<b>The Answer cannot be inserting at the time!</b> "
            lbl_showErrorMessages.Text &= err.Message
        Finally
            con.Close()
        End Try

    End Sub


    Private Sub updateMarks(Marks As Integer, StudentID As Integer)

        'update Quiz_Student set Marks = 2 where StudentID = 1

        Dim insertSQL As String
        insertSQL = "update Quiz_Student set Marks = @Marks where StudentID = @StudentID"




        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@Marks", Marks)
        cmd.Parameters.AddWithValue("@StudentID", StudentID)


        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()


        Catch err As Exception
            ' lbl_showErrorMessages.Text = "<b>The Answer cannot be inserting at the time!</b> "
            'lbl_showErrorMessages.Text &= err.Message
        Finally
            con.Close()
        End Try

    End Sub

    Protected Sub Next_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click

    End Sub

End Class



