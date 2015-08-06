Public Class DeleteAppFolders
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        Dim _dbApp As New ApplicationDbContext

        Try
            Using (_dbApp)
                Dim queryFolders = (From folder1 In _dbApp.AppFolders).ToList

                If queryFolders.Count > 0 Then
                    For Each folder1 In queryFolders
                        _dbApp.AppFolders.Remove(folder1)
                    Next

                    _dbApp.SaveChanges()

                End If

                Response.Redirect("~/Administration/TestDriver.aspx")

            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class