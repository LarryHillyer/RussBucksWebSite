Public Class ChangeUserPick
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserHandle1.Focus()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Session("changeUser") = UserHandle1.Text
        Response.Redirect("~/LosersPool/Administration/ChangeUserPick2.aspx")
    End Sub
End Class