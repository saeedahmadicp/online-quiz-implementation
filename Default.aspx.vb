Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class QuizLogin
    Inherits System.Web.UI.Page
    'connection string
    Private connectionString As String = WebConfigurationManager.ConnectionStrings("QUIZ_DB").ConnectionString

    'Global varialble Accessable to all the functions 
    Public Shared _UserID As Integer


    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        lbl_showErrorMessages.Controls.Clear()
        Dim UserRole, UserEmail, UserPassword As String
        If radiobtnAdmin.Checked Then
            UserRole = radiobtnAdmin.Text
        ElseIf radiobtnExaminer.Checked Then
            UserRole = radiobtnExaminer.Text
        ElseIf radiobtnStudent.Checked Then
            UserRole = radiobtnStudent.Text
        End If

        UserEmail = txtboxEmail.Text
        UserPassword = txtboxPassword.Text




        'Value the user
        If ValidateUser(UserEmail, UserRole, UserPassword) Then
            'cross-page-posting
            Dim url As String
            If String.Compare(UserRole, "Admin") = 0 Then
                Session("UserRole") = "Admin"
                url = "admin.aspx"
                Response.Redirect(url)
            ElseIf String.Compare(UserRole, "Examiner") = 0 Then
                Session("UserRole") = "Examiner"
                url = "instructor.aspx"
                Response.Redirect(url)
            ElseIf String.Compare(UserRole, "Student") = 0 Then
                Session("UserRole") = "Student"
                url = "student.aspx"
                Response.Redirect(url)
            End If
        End If

    End Sub

    Private Function ValidateUser(UserEmail As String, UserRole As String, UserPassword As String) As Boolean
        Dim selectSQL As String
        Dim IsPasswordCorrect As Boolean
        'general user data
        selectSQL = "SELECT email, userRole, UserID FROM Quiz_User Where email = '" & UserEmail.ToString() & " '"
        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(selectSQL, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "Quiz_User")

            'calculate and campare the hashes of the  password entered by the user
            cmd.CommandText = "Select CASE WHEN '" & UserPassword.ToString() & "' = "
            cmd.CommandText &= "(SELECT Password from Quiz_User where Email = '" & UserEmail.ToString & "') THEN '1' ELSE '0' END as condition"
            adapter.Fill(dsQuizDB, "Condition")

            'This will return 1 if the hashes matches otherwise 0
            IsPasswordCorrect = Integer.Parse(dsQuizDB.Tables("Condition").Rows.Item(0).Field(Of String)("condition"))

            'Checking whether, the Role and Password Entered by the user is valid 
            If UserRole = dsQuizDB.Tables("Quiz_User").Rows.Item(0).Field(Of String)("UserRole") Then
                If IsPasswordCorrect Then
                    _UserID = dsQuizDB.Tables("Quiz_User").Rows.Item(0)("userID")
                    Session("UserID") = _UserID
                    Return True
                Else
                    lbl_showErrorMessages.Text = "The Password You Entered is incorrect!"
                    Return False
                End If
            Else
                lbl_showErrorMessages.Text = "The User Role you Entered Doesn't Match!"
                Return False
            End If

        Catch err As Exception
            lbl_showErrorMessages.Text = "The Email address you entered is not registered!"
            Return False
            'label_UserProfile.Text &= err.Message
        Finally
            con.Close()
        End Try

    End Function


End Class
