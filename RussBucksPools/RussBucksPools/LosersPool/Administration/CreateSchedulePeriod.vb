Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Linq
Imports System.Data

Imports RussBucksPools.LosersPool.Models

Imports RussBucksPools.JoinPools
Imports RussBucksPools.JoinPools.Models

Public Class CreateSchedulePeriod

    Private _dbLoserPool As New LosersPoolContext

    Public Sub New(sport As String)

        If _dbLoserPool.ScheduleTimePeriods.Count >= 1 Then
            Exit Sub
        End If


        Try
            Using (_dbLoserPool)


                Dim games = (From schedule1 In _dbLoserPool.ScheduleEntities
                             Where schedule1.Sport = sport).ToList

                Dim TimePeriods = New Dictionary(Of String, String)
                For Each game In games

                    Dim timePeriod1 = game.TimePeriod
                    If Not (TimePeriods.ContainsKey(timePeriod1)) Then
                        TimePeriods.Add(timePeriod1, timePeriod1)
                    End If
                Next

                For Each timePeriod2 In TimePeriods

                    Dim TimePeriodGames = _dbLoserPool.ScheduleEntities.Where(Function(dg) dg.TimePeriod = timePeriod2.Key And dg.Sport = sport).ToList

                    Dim minStartDate As String
                    minStartDate = TimePeriodGames(0).StartDate
                    For gamenum = 1 To TimePeriodGames.Count - 1
                        If TimePeriodGames(gamenum).StartDate < minStartDate Then
                            minStartDate = TimePeriodGames(gamenum).StartDate
                        End If
                    Next

                    Dim TimePeriodGames1 = _dbLoserPool.ScheduleEntities.Where(Function(dg) dg.TimePeriod = timePeriod2.Key And dg.StartDate = minStartDate).ToList

                    Dim minStartTime As String
                    minStartTime = TimePeriodGames1(0).StartTime
                    For gamenum = 1 To TimePeriodGames1.Count - 1
                        If TimePeriodGames1(gamenum).StartTime < minStartTime Then
                            minStartTime = TimePeriodGames1(gamenum).StartTime
                        End If
                    Next

                    Dim scheduleTimePeriod = New ScheduleTimePeriod
                    scheduleTimePeriod.TimePeriod = timePeriod2.Key

                    If timePeriod2.Key = "day1" Then
                        scheduleTimePeriod.TimePeriodStartDate = "3/15/15"
                        scheduleTimePeriod.TimePeriodStartTime = "12:01 AM"
                    End If

                    scheduleTimePeriod.startGameDate = minStartTime
                    scheduleTimePeriod.startGameTime = minStartDate
                    scheduleTimePeriod.Sport = sport

                    _dbLoserPool.ScheduleTimePeriods.Add(scheduleTimePeriod)
                    _dbLoserPool.SaveChanges()

                    Dim dummy = "dummy"

                Next

            End Using
        Catch ex As Exception

        End Try


    End Sub

    Private Shared Function ClearExistingScheduleTimePeriodList(_dbLoserPool As LosersPoolContext) As LosersPoolContext

        Try

            Dim queryScheduleTimePeriod = (From game In _dbLoserPool.ScheduleTimePeriods).ToList

            For Each timePeriod In queryScheduleTimePeriod
                _dbLoserPool.ScheduleTimePeriods.Remove(timePeriod)
            Next

            _dbLoserPool.SaveChanges()
            Return _dbLoserPool


        Catch ex As Exception

        End Try

        Return Nothing

    End Function

End Class
