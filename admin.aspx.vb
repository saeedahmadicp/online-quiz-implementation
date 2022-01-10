Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class admin
    Inherits System.Web.UI.Page

    Private connectionString As String = WebConfigurationManager.ConnectionStrings("Quiz_DB").ConnectionString
    Public Shared UserID As Integer
    Private Sub admin_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Session("UserRole") = "Admin" Then
                'take the user data from the from database And update the userprofile accordingly
                UserID = Session("UserID")
                Dim adminProfilelabel As New Label
                AdminProfile.Controls.Clear()
                adminProfilelabel.ID = "txtLabel_instructorProfile"
                update_UserProfile(adminProfilelabel)
                AdminProfile.Controls.Add(adminProfilelabel)
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
            cmd.CommandText = "SELECT UserID FROM Quiz_Admin Where userID = " & UserID
            adapter.Fill(dsQuizDB, "Quiz_admin")

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
            Dim QuizUsers_QuizAdmins As New DataRelation("QuizUsers_QuizAdmins",
        dsQuizDB.Tables("Quiz_Users").Columns("UserID"),
        dsQuizDB.Tables("Quiz_admin").Columns("UserID"))

            'add relations
            dsQuizDB.Relations.Add(QuizUsers_QuizAdmins)

            Dim userRow As DataRow
            Dim adminRow As DataRow
            adminRow = dsQuizDB.Tables("Quiz_Admin").Rows.Item(0)
            userRow = adminRow.GetParentRow(QuizUsers_QuizAdmins)


            InsertLabel("Admin Name: ", userRow("FullName"), "txtlabel_adminfullName")
            InsertLabel("Admin Address: ", userRow("Address"), "txtlabel_adminAdress")
            InsertLabel("Admin Email: ", userRow("Email"), "txtlabel_adminEmail")
            InsertLabel("Admin Phone Number: ", userRow("PhoneNumber"), "txtlabel_adminPhone")

            'InsertLabel("Student ID: ", studentRow("StudentID"), "txtlabel_studentID")
            'InsertLabel("Marks : ", studentRow("Marks"), "txtlabel_studentmarks")
            'InsertLabel("Semester : ", studentRow("Semester"), "txtlabel_studentsemester")
            'InsertLabel("Department : ", studentRow("Department"), "txtlabel_studentdepartment")
        Catch ex As Exception
            showErrorMessages.Text = "<b>Sorry For inconvenience! There are issues with the Admin Account!</b>"
            btnChangeSetting.Visible = False
            title.Visible = False
        End Try

    End Sub


    Private Sub InsertLabel(labelText As String, labelValue As String, labelId As String)
        Dim txtLabel As New Label
        txtLabel.ID = labelId
        txtLabel.Text = "<br/>" & labelText & "&nbsp &nbsp" & labelValue & "<br/>"
        AdminProfile.Controls.Add(txtLabel)
    End Sub

    Private Sub btnChangeSetting_Click(sender As Object, e As EventArgs) Handles btnChangeSetting.Click
        Dim url As String
        url = "QuizSetting.aspx"
        Response.Redirect(url)
    End Sub
End Class
