Public Class CreateAppFolders
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RootFolder1.Focus()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim _dbApp As New ApplicationDbContext
        Try
            Using (_dbApp)
                Dim appFolder1 As New AppFolder
                appFolder1.rootFolder = RootFolder1.Text
                appFolder1.driverRootFolder = TestDriverRootFolder1.Text

                _dbApp.AppFolders.Add(appFolder1)
                _dbApp.SaveChanges()
            End Using
            Response.Redirect("~/Administration/TestDriver.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class