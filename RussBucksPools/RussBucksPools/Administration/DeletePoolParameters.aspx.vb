Imports RussBucksPools.JoinPools.Models


Public Class DeletePoolParameters
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim _dbPools = New PoolDbContext

        Try
            Using (_dbPools)
                Dim queryParameters = (From param1 In _dbPools.PoolParameters).ToList

                If queryParameters.Count > 0 Then
                    For Each param1 In queryParameters
                        _dbPools.PoolParameters.Remove(param1)
                    Next
                End If
                _dbPools.SaveChanges()

                Response.Redirect("~/Administration/TestDriver.aspx")
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class