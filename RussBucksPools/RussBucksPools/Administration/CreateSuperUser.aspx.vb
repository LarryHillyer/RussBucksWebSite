Public Class CreateSuperUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim _dbMembership As New ApplicationDbContext

        Try
            Using (_dbMembership)
                If SuperUser1.Text Is Nothing Or SuperUser1.Text = "" Then
                    Label1.Text = "Invalid Email Address"
                    Label1.ForeColor = Drawing.Color.Red
                Else

                    Dim queryMember = (From member1 In _dbMembership.Users
                                       Where member1.UserName = SuperUser1.Text).SingleOrDefault

                    If queryMember Is Nothing Or queryMember.UserName = "" Then
                    Else
                        Dim userName1 As String
                        Dim user1 As String = CStr(queryMember.UserName)
                        Dim Ai As Int16 = InStr(user1, "@")
                        Dim Pi As Int16 = InStr(user1, ".")

                        userName1 = Left(user1, Ai - 1) + Mid(user1, (Ai + 1), Pi - Ai - 1) + Right(user1, Len(user1) - Pi)

                        Dim sU1 As New SuperUser
                        sU1.SuperUserName = userName1
                        _dbMembership.SuperUsers.Add(sU1)
                        _dbMembership.SaveChanges()

                        SuperUser1.Text = ""
                        Response.Redirect("~/Administration/TestDriver")
                    End If

                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class