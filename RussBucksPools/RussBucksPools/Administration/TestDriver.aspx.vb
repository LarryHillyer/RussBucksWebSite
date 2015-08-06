Imports System
Imports System.Data
Imports System.Linq
Imports System.Xml.Linq
Imports System.Globalization

Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.IO
Imports System.IO.Path

Imports System.Threading
Imports System.Threading.Tasks

Imports RussBucksPools.LosersPool.Models

Public Class TestDriver
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim _dbLoserPool = New LosersPoolContext
        Try
            Using (_dbLoserPool)

                Dim queryUsersChoices = (From user1 In _dbLoserPool.UserChoicesList).ToList

                If queryUsersChoices.Count > 0 Then
                    For Each user1 In queryUsersChoices
                        _dbLoserPool.UserChoicesList.Remove(user1)
                    Next
                End If

                Dim queryTimePeriods = (From user1 In _dbLoserPool.ScheduleTimePeriods).ToList

                If queryTimePeriods.Count > 0 Then
                    For Each timeperiod1 In queryTimePeriods
                        _dbLoserPool.ScheduleTimePeriods.Remove(timeperiod1)
                    Next
                End If

                Dim querySchedule = (From game1 In _dbLoserPool.ScheduleEntities).ToList

                If querySchedule.Count > 0 Then
                    For Each game1 In querySchedule
                        _dbLoserPool.ScheduleEntities.Remove(game1)
                    Next
                End If

                Dim queryLosers = (From game1 In _dbLoserPool.LoserList).ToList

                If queryLosers.Count > 0 Then
                    For Each loser1 In queryLosers
                        _dbLoserPool.LoserList.Remove(loser1)
                    Next
                End If

                Dim queryByeTeams = (From game1 In _dbLoserPool.ByeTeamsList).ToList

                If queryByeTeams.Count > 0 Then
                    For Each byeteam1 In queryByeTeams
                        _dbLoserPool.ByeTeamsList.Remove(byeteam1)
                    Next
                End If

                Dim queryCurrentScoringUpdate = (From game1 In _dbLoserPool.CurrentScoringUpdates).ToList

                If queryCurrentScoringUpdate.Count > 0 Then
                    For Each cSU1 In queryCurrentScoringUpdate
                        _dbLoserPool.CurrentScoringUpdates.Remove(cSU1)
                    Next
                End If

                Dim queryScoringUpdate = (From game1 In _dbLoserPool.ScoringUpdates).ToList

                If queryScoringUpdate.Count > 0 Then
                    For Each sU1 In queryScoringUpdate
                        _dbLoserPool.ScoringUpdates.Remove(sU1)
                    Next
                End If


                _dbLoserPool.SaveChanges()


                _dbLoserPool.SaveChanges()

                Response.Redirect("~/LoserPoolHome.aspx")

            End Using
        Catch ex As Exception

        End Try

    End Sub

End Class