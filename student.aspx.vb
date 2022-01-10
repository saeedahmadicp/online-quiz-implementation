Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class student
    Inherits System.Web.UI.Page

    'connection String 
    Private connectionString As String = WebConfigurationManager.ConnectionStrings("Quiz_DB").ConnectionString
    Public Shared UserID As Integer

    'loading all the user data
    Private Sub student_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserRole") = "Student" Then

                'take the user data from the from database And update the userprofile accordingly
                UserID = Session("UserID")
                Dim studentProfilelabel As New Label
                StudentProfile.Controls.Clear()
                studentProfilelabel.ID = "txtLabel_instructorProfile"
                update_UserProfile(studentProfilelabel)
                StudentProfile.Controls.Add(studentProfilelabel)
            Else
                Response.Redirect("AccessDenied.aspx")

            End If
        End If
    End Sub

    'Function for updating the userProfile from database 
    Private Sub update_UserProfile(label_UserProfile As Label)
        label_UserProfile.Controls.Clear()
        ' Define ADO.NET objects.
        Dim selectSQL As String
        'general user data
        selectSQL = "SELECT UserID, FullName, Address, Email, PhoneNumber FROM Quiz_User Where userID = " & UserID
        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(selectSQL, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "Quiz_Users")

            'specific user data
            cmd.CommandText = "SELECT StudentID, UserID, Marks, Semester, Department FROM Quiz_Student Where userID = " & UserID
            adapter.Fill(dsQuizDB, "Quiz_students")

        Catch err As Exception
            label_UserProfile.Text = "Error 404! The User Account Doesn't found "
            'label_UserProfile.Text &= err.Message
        Finally
            con.Close()
        End Try


        Try
            'The first augument, defines the unique name for the relation, while the 2nd and 3rd shows 
            ' the unique parent and child relation in the tables respectivley
            'relation between quiz user and quiz students 
            Dim QuizUsers_QuizStudents As New DataRelation("QuizUsers_QuizStudents",
            dsQuizDB.Tables("Quiz_Users").Columns("UserID"),
            dsQuizDB.Tables("Quiz_Students").Columns("UserID"))

            'add relations
            dsQuizDB.Relations.Add(QuizUsers_QuizStudents)

            Dim userRow As DataRow
            Dim studentRow As DataRow
            studentRow = dsQuizDB.Tables("Quiz_Students").Rows.Item(0)
            userRow = studentRow.GetParentRow(QuizUsers_QuizStudents)



            InsertLabel("Student Name: ", userRow("FullName"), "txtlabel_studentfullName")
            InsertLabel("Student Address: ", userRow("Address"), "txtlabel_studentAdress")
            InsertLabel("Student Email: ", userRow("Email"), "txtlabel_studentEmail")
            InsertLabel("Student Phone Number: ", userRow("PhoneNumber"), "txtlabel_studentPhone")

            InsertLabel("Student ID: ", studentRow("StudentID"), "txtlabel_studentID")
            InsertLabel("Semester : ", studentRow("Semester"), "txtlabel_studentsemester")
            InsertLabel("Department : ", studentRow("Department"), "txtlabel_studentdepartment")


            'Update the students marks here
            Session("StudentMarks") = studentRow("Marks")
            Session("StudentID") = studentRow("StudentID")

        Catch ex As Exception
            showErrorMessages.Text = "<b>Sorry For inconvenience! There are issues with the Student Account!</b>"
            btnShowResult.Visible = False
            btnStartQuiz.Visible = False
            title.Visible = False

        End Try



    End Sub


    'Functions for dynamically inserting the label
    Private Sub InsertLabel(labelText As String, labelValue As String, labelId As String)
        Dim txtLabel As New Label
        txtLabel.ID = labelId
        txtLabel.Text = "<br/>" & labelText & "&nbsp &nbsp" & labelValue & "<br/>"
        StudentProfile.Controls.Add(txtLabel)
    End Sub

    Private Sub btnShowResult_Click(sender As Object, e As EventArgs) Handles btnShowResult.Click

        Dim studentMarks As Integer = Session("StudentMarks")
        If studentMarks.ToString IsNot Nothing Then
            InsertLabel("Your best Score is: ", studentMarks.ToString(), "txtlabel_studentmarks")
        Else
            InsertLabel("You havn't take any Quiz Yet!", "", "txtlabelmarks")
        End If
    End Sub

    Private Sub btnStartQuiz_Click(sender As Object, e As EventArgs) Handles btnStartQuiz.Click


        If GetTotalNumberOfQuestionsToBeInserted().ToString IsNot Nothing And GetNumberOfQuestionsInserted().ToString IsNot Nothing Then

            If (GetTotalNumberOfQuestionsToBeInserted() - GetNumberOfQuestionsInserted()) = 0 Then
                Dim url As String
                url = "Quiz.aspx"
                Response.Redirect(url)

            Else
                Response.Redirect("AccessDenied.aspx")

            End If

        Else
            Response.Redirect("AccessDenied.aspx")
        End If

    End Sub







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
            showErrorMessages.Text = err.Message
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
            showErrorMessages.Text = err.Message
            Return -1
        Finally
            con.Close()
        End Try

        Return NumberOfQuestionsInserted

    End Function

End Class
