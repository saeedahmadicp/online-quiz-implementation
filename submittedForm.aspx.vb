
Partial Class submittedForm
    Inherits System.Web.UI.Page

    Private Sub submittedForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Marks As Integer
        Marks = Session("Marks")
        showMarks.Text = "Your marks are: " & Marks.ToString

    End Sub
End Class
