Public Class Site_Mobile
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _dbMembership As New ApplicationDbContext

        administration1.Visible = False
        Try
            Using (_dbMembership)
                If Session("userId") Is Nothing Then
                Else
                    Dim queryMembership = (From member1 In _dbMembership.SuperUsers).ToList
                    For Each member1 In queryMembership
                        If CStr(Session("userId")) = member1.SuperUserName Then
                            administration1.Visible = True
                            Exit For
                        End If
                    Next
                End If
            End Using

        Catch ex As Exception

        End Try
    End Sub

End Class