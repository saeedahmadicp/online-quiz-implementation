Imports System.Web.UI.Control
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class QuizSignUp
    Inherits System.Web.UI.Page
    Public connectionString As String = WebConfigurationManager.ConnectionStrings("QUIZ_DB").ConnectionString



    Private Sub QuizSignUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            insertValidator("*Access Code", "Access Code is mandatory", "txtboxAccessCode")
        End If
    End Sub

    Private Sub radiobtnAdmin_CheckedChanged(sender As Object, e As EventArgs) Handles radiobtnAdmin.CheckedChanged

        space11.Visible = True
        space21.Visible = False
        space31.Visible = False
        space41.Visible = False




        AccessCodeTitle.Visible = True
        txtboxAccessCode.Visible = True
        departmentNameTitle.Visible = False
        txtboxDepartmentName.Visible = False
        semesterNumberTitle.Visible = False
        txtboxSemesterNumber.Visible = False
        gradeScaleTitle.Visible = False
        txtBoxGradeScale.Visible = False
        insertValidator("*Access Code", "Access Code is mandatory", "txtboxAccessCode")
    End Sub

    Private Sub radiobtnInstructor_CheckedChanged(sender As Object, e As EventArgs) Handles radiobtnInstructor.CheckedChanged

        space11.Visible = True
        space21.Visible = True
        space31.Visible = False
        space41.Visible = True





        AccessCodeTitle.Visible = True
        txtboxAccessCode.Visible = True
        departmentNameTitle.Visible = True
        txtboxDepartmentName.Visible = True
        semesterNumberTitle.Visible = False
        txtboxSemesterNumber.Visible = False
        gradeScaleTitle.Visible = True
        txtBoxGradeScale.Visible = True




        insertValidator("*Access Code", "Access Code is mandatory", "txtboxAccessCode")
        insertValidator("*Grade Scale", "Grade Scale is mandatory", "txtBoxGradeScale")
        insertValidator("*Department Name", "Department Name is mandatory", "txtboxDepartmentName")

    End Sub

    Private Sub radiobtnStudent_CheckedChanged(sender As Object, e As EventArgs) Handles radiobtnStudent.CheckedChanged

        space11.Visible = False
        space21.Visible = True
        space31.Visible = True
        space41.Visible = False

        AccessCodeTitle.Visible = False
        txtboxAccessCode.Visible = False
        departmentNameTitle.Visible = True
        txtboxDepartmentName.Visible = True
        semesterNumberTitle.Visible = True
        txtboxSemesterNumber.Visible = True
        gradeScaleTitle.Visible = False
        txtBoxGradeScale.Visible = False

        insertRangeValidator(0, 9, "*Invalid Semester", "Semester No. should be between 0  and 9", "txtboxSemesterNumber")
        insertValidator("*Department Name", "Department Name is mandatory", "txtboxDepartmentName")

    End Sub

    Private Sub insertTextBox(txtboxText As String, txtboxID As String)

        Dim txtboxLabel As New Label
        txtboxLabel.Text = txtboxText
        txtboxLabel.Text &= "&nbsp &nbsp"
        addElements.Controls.Add(txtboxLabel)
        Dim txtbox As New TextBox()
        txtbox.ID = txtboxID
        txtbox.EnableViewState = True
        If (txtboxID = "txtboxAccessCode") Then
            txtbox.TextMode = TextBoxMode.Password
        End If


        addElements.Controls.Add(txtbox)
        Dim newLine As New Label
        newLine.Text = "<br/><br/>"
        addElements.Controls.Add(newLine)
    End Sub


    Private Sub insertValidator(validationText As String, ErrorMessage As String, txtboxID As String)
        Dim rqfValidator As RequiredFieldValidator = New RequiredFieldValidator
        rqfValidator.Display = ValidatorDisplay.Dynamic
        rqfValidator.Text = validationText
        rqfValidator.Attributes.Add("runat", "server")
        rqfValidator.ErrorMessage = ErrorMessage
        rqfValidator.ControlToValidate = txtboxID
        Me.Form.Controls.Add(rqfValidator)
    End Sub

    Private Sub insertRangeValidator(minValue As Integer, maxValue As Integer, RangeValidationText As String, ErrorMessage As String, txtboxID As String)
        Dim rangeValidator As RangeValidator = New RangeValidator
        rangeValidator.MinimumValue = minValue.ToString()
        rangeValidator.MaximumValue = maxValue.ToString()
        rangeValidator.Type = ValidationDataType.Double
        rangeValidator.ErrorMessage = ErrorMessage
        rangeValidator.Text = RangeValidationText
        rangeValidator.Style.Add("position", "absolute")
        rangeValidator.Attributes.Add("runat", "server")
        rangeValidator.ControlToValidate = txtboxID
        rangeValidator.Attributes.Add("runat", "server")
        Me.Form.Controls.Add(rangeValidator)
    End Sub


    Private Sub btnSignUp_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click
        If Page.IsValid Then
            Dim UserRole As String
            If radiobtnAdmin.Checked Then
                UserRole = radiobtnAdmin.Text
            ElseIf radiobtnInstructor.Checked Then
                UserRole = radiobtnInstructor.Text
            ElseIf radiobtnStudent.Checked Then
                UserRole = radiobtnStudent.Text
            End If

            If UserRole <> "Student" Then

                If AccessCodeCheck(UserRole, txtboxAccessCode.Text) Then
                    InsertUser(txtboxFullName.Text, txtboxAddress.Text, txtboxEmail.Text, txtboxPassword1.Text,
                    txtboxPhoneNumber.Text, UserRole)
                End If
            Else
                InsertUser(txtboxFullName.Text, txtboxAddress.Text, txtboxEmail.Text, txtboxPassword1.Text,
                       txtboxPhoneNumber.Text, UserRole)

            End If


            If UserRole = "Student" Then
                If GetCurrentUserID() <> -1 Then
                    insertStudent(GetCurrentUserID(), Integer.Parse(txtboxSemesterNumber.Text), txtboxDepartmentName.Text)
                End If
            ElseIf UserRole = "Admin" Then
                If GetCurrentUserID() <> -1 Then
                    insertAdmin(GetCurrentUserID())
                End If

            ElseIf UserRole = "Examiner" Then
                If GetCurrentUserID() <> -1 Then
                    insertExaminer(GetCurrentUserID(), txtBoxGradeScale.Text, txtboxDepartmentName.Text)
                End If


            End If

        Else
            ' showErrorMessages.Text = "The "
        End If

    End Sub

    'HASHBYTES('SHA2_512', @Password ),
    Private Sub InsertUser(FullName As String, Address As String, Email As String, Password As String,
                           PhoneNumber As String, UserRole As String)
        Dim insertSQL As String
        insertSQL = "INSERT INTO Quiz_User ("
        insertSQL &= "FullName, Address, "
        insertSQL &= "Email, Password, PhoneNumber, UserRole) "
        insertSQL &= "VALUES ("
        insertSQL &= "@FullName, @Address, @Email,"
        insertSQL &= "@Password ,"
        insertSQL &= "@PhoneNumber, @UserRole"
        insertSQL &= ")"



        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@FullName", FullName)
        cmd.Parameters.AddWithValue("@Address", Address)
        cmd.Parameters.AddWithValue("@Email", Email)
        cmd.Parameters.AddWithValue("@Password", Password)
        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber)
        cmd.Parameters.AddWithValue("@UserRole", UserRole)
        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            showErrorMessages.Text = "<b>You Account is Successfully Created!</b>"

        Catch err As Exception
            showErrorMessages.Text = "<b>The Email you Entered is already Registered!</b> "
            'showErrorMessages.Text = err.Message
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub insertStudent(UserID As Integer, Semester As Integer, Department As String)
        Dim insertSQL As String
        insertSQL = "INSERT INTO Quiz_Student ("
        insertSQL &= "UserID, Semester, "
        insertSQL &= "Department) "
        insertSQL &= "VALUES ("
        insertSQL &= "@UserID, @Semester, @Department )"



        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@UserID", UserID)
        cmd.Parameters.AddWithValue("@Semester", Semester)
        cmd.Parameters.AddWithValue("@Department", Department)


        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            'showErrorMessages.Text = "<b>You Account is Successfully Created!</b>"

        Catch err As Exception
            ' showErrorMessages.Text = "<b>The Email you Entered is already Registered!</b> "
            ' showErrorMessages.Text &= err.Message
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub insertAdmin(UserID As Integer)
        Dim insertSQL As String
        insertSQL = "INSERT INTO Quiz_Admin ("
        insertSQL &= "UserID ) "
        insertSQL &= "VALUES ("
        insertSQL &= "@UserID )"



        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@UserID", UserID)

        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            'showErrorMessages.Text = "<b>You Account is Successfully Created!</b>"

        Catch err As Exception
            ' showErrorMessages.Text = "<b>The Email you Entered is already Registered!</b> "
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub insertExaminer(UserId As Integer, GradeScale As String, Deparment As String)
        Dim insertSQL As String
        insertSQL = "INSERT INTO Quiz_Examiner ("
        insertSQL &= "UserID, GradeScale, "
        insertSQL &= "Department) "
        insertSQL &= "VALUES ("
        insertSQL &= "@UserID, @GradeScale, @Department )"



        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(insertSQL, con)

        'Add the parameters 
        cmd.Parameters.AddWithValue("@UserID", UserId)
        cmd.Parameters.AddWithValue("@GradeScale", GradeScale)
        cmd.Parameters.AddWithValue("@Department", Deparment)

        ' Try to open the database and execute the update.

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            'showErrorMessages.Text = "<b>You Account is Successfully Created!</b>"

        Catch err As Exception
            ' showErrorMessages.Text = "<b>The Email you Entered is already Registered!</b> "
            ' showErrorMessages.Text = err.Message
        Finally
            con.Close()
        End Try

    End Sub


    Public Function AccessCodeCheck(UserRole As String, AccessCode As String) As Boolean
        Dim hashesCheckSQL As String
        Dim IsAccessCodeCorrect As Integer
        hashesCheckSQL = "Select CASE WHEN HASHBYTES('SHA2_512', '" & AccessCode.ToString() & "') = "
        hashesCheckSQL &= "(Select AccessCodeHashe from Quiz_AccessCodes where UserRole= '" & UserRole.ToString & "')"
        hashesCheckSQL &= "THEN '1' ELSE '0' END as condition"

        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(hashesCheckSQL, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "condition")

            IsAccessCodeCorrect = Integer.Parse(dsQuizDB.Tables("Condition").Rows.Item(0).Field(Of String)("condition"))

        Catch err As Exception
            showErrorMessages.Text = "The access Code you entered is invalid!"
            Return False
        Finally
            con.Close()
        End Try

        If IsAccessCodeCorrect = 1 Then
            Return True
        Else
            Return False
        End If

    End Function


    Public Function GetCurrentUserID() As Integer
        Dim hashesCheckSQL As String
        Dim CurrentUserID As Integer
        hashesCheckSQL = "SELECT Top 1  USERID from Quiz_User order by USERID DESC"

        Dim con As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand(hashesCheckSQL, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dsQuizDB As New DataSet()
        ' Try to open database and read information.
        Try
            con.Open()
            adapter.Fill(dsQuizDB, "USERID_T")

            CurrentUserID = dsQuizDB.Tables("USERID_T").Rows.Item(0).Field(Of Integer)("USERID")

        Catch err As Exception
            showErrorMessages.Text = "The access Code you entered is invalid!"
            showErrorMessages.Text &= err.Message
            Return -1
        Finally
            con.Close()
        End Try

        If CurrentUserID Then
            Return CurrentUserID
        Else
            Return -1
        End If

    End Function



    Protected Sub txtboxSemesterNumber_TextChanged(sender As Object, e As EventArgs) Handles txtboxSemesterNumber.TextChanged

    End Sub

End Class
