Imports System
Imports System.Data
Imports System.Data.Entity

Imports RussBucksPools.JoinPools.Models

Public Class PoolParameters
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Sport1.Focus()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        Dim _dbPools As New PoolDbContext
        Try
            Using (_dbPools)
                Dim poolParameter1 As New PoolParameter
                poolParameter1.Sport = Sport1.Text
                poolParameter1.timePeriodName = TimePeriodName1.Text
                poolParameter1.timePeriodIncrement = TimeIncrement1.Text
                poolParameter1.seasonStartDate = SeasonStartDate1.Text
                poolParameter1.seasonStartTime = SeasonStartTime1.Text
                poolParameter1.seasonEndDate = SeasonEndDate1.Text

                _dbPools.PoolParameters.Add(poolParameter1)
                _dbPools.SaveChanges()

                Response.Redirect("~/Administration/TestDriver.aspx")
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class