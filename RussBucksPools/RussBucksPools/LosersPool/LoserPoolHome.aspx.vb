Imports System
Imports System.Data
Imports System.Linq
Imports System.Xml.Linq
Imports System.Globalization
Imports System.Threading

Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.ModelBinding


Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models


Public Class LoserPoolHome
    Inherits System.Web.UI.Page

    Private MyOptions As List(Of MyOption)

    Private rootFolder As String

    Private TeamNameCollection As New Dictionary(Of String, String)
    Private TeamNameCollection2 As New Dictionary(Of String, String)

    Private TimePeriodCompletedCollection As New Dictionary(Of String, TimePeriodCompleted)
    Private TimePeriodCompletedList As New List(Of TimePeriodCompleted)

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        Dim _dBLoserPool As New LosersPoolContext
        Dim _dbPools As New PoolDbContext
        Dim _dbApp As New ApplicationDbContext
        Try
            Using (_dBLoserPool)
                Using (_dbPools)


                    Dim poolAlias = CStr(Session("poolAlias"))
                    Dim cronJobName = CStr(Session("cronJobName"))

                    Dim poolParams1 = (From poolParam1 In _dbPools.PoolParameters
                                          Where poolParam1.poolAlias = poolAlias).Single

                    Session("TimePeriod") = poolParams1.TimePeriod

                    If poolParams1.poolState = "Enter Picks" Then

                        Session("timeState") = "Enter Picks"

                        Dim myOption1 As New MyOption
                        myOption1.TimePeriod = "Enter Picks"
                        myOption1.PageURL = "WeeklyLoserPoolUserEntry.aspx"

                        MyOptions = New List(Of MyOption)
                        MyOptions.Add(myOption1)

                    ElseIf poolParams1.poolState = "Scoring Update" Then

                        Session("timeState") = "Scoring Update"

                        Dim myOption1 As New MyOption
                        myOption1.TimePeriod = "Scoring Update"
                        myOption1.PageURL = "WeeklyScoringUpdate.aspx"

                        MyOptions = New List(Of MyOption)
                        MyOptions.Add(myOption1)

                    ElseIf poolParams1.poolState = "Season End" Then

                        Session("timeState") = "Season End"

                        Dim myOption2 As New MyOption
                        myOption2.TimePeriod = "Season End"
                        myOption2.PageURL = "SeasonEnd.aspx"

                        MyOptions = New List(Of MyOption)
                        MyOptions.Add(myOption2)

                    End If

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function GetMyOptions() As List(Of MyOption)

        Dim _dbLoserPool As New LosersPoolContext
        Dim _dbPools As New PoolDbContext

        Dim EName As String = CStr(Session("userId"))
        Dim poolAlias = CStr(Session("poolAlias"))
        Try
            Using (_dbPools)

                Dim queryAdmin = (From user1 In _dbPools.PoolParameters
                                 Where user1.poolAdministrator = EName).ToList

                If queryAdmin.Count = 0 Then
                Else
                    If MyOptions(0).TimePeriod = "Enter Picks" Then
                    ElseIf MyOptions(0).TimePeriod = "Scoring Update" Then
                        Administration1.Visible = True
                        ChangeUserPick1.Visible = True
                    End If

                End If

            End Using
        Catch ex As Exception

        End Try

        Return MyOptions
    End Function

    Public Function completedTimePeriods2_GetData(<QueryString("Id")> TimePeriod As String) As List(Of TimePeriodCompleted)

        If Not Page.IsPostBack Then
            Dim _dbLosersPool As New LosersPoolContext
            Using (_dbLosersPool)
                Try
                    Dim sport = CStr(Session("Sport"))
                    Dim cronJobName = CStr(Session("cronJobName"))

                    Dim queryCompletedTimePeriods = (From timePeriod1 In _dbLosersPool.ScheduleTimePeriods
                                                    Where Not timePeriod1.TimePeriodEndDate Is Nothing And timePeriod1.CronJob = cronJobName).ToList

                    For Each timePeriod1 In queryCompletedTimePeriods
                        If Not TimePeriodCompletedCollection.ContainsKey(timePeriod1.TimePeriod) Then
                            Dim timePeriodCompleted1 As New TimePeriodCompleted
                            timePeriodCompleted1.TimePeriod = timePeriod1.TimePeriod
                            TimePeriodCompletedCollection.Add(timePeriod1.TimePeriod, timePeriodCompleted1)
                        End If
                    Next

                    For Each timeP1 In TimePeriodCompletedCollection
                        Dim queryTimePeriod = (From timeperiod1 In _dbLosersPool.ScheduleTimePeriods
                                               Where timeperiod1.TimePeriod = timeP1.Key).ToList
                        TimePeriodCompletedCollection(timeP1.Key).TimePeriodDate = queryTimePeriod(0).TimePeriodEndDate
                        TimePeriodCompletedCollection(timeP1.Key).TimePeriodTime = queryTimePeriod(0).TimePeriodEndTime
                    Next

                    For Each timeP1 In TimePeriodCompletedCollection
                        Dim timePeriodCompleted1 As New TimePeriodCompleted
                        timePeriodCompleted1.TimePeriod = timeP1.Value.TimePeriod
                        timePeriodCompleted1.TimePeriodDate = timeP1.Value.TimePeriodDate
                        timePeriodCompleted1.TimePeriodTime = timeP1.Value.TimePeriodTime
                        TimePeriodCompletedList.Add(timePeriodCompleted1)
                    Next

                    If Not (TimePeriod Is Nothing) Then
                        Session("completedTimePeriod") = TimePeriod
                        Response.Redirect("~/LosersPool/CompletedDayResults.aspx")
                    End If

                Catch ex As Exception
                    Return Nothing
                End Try
            End Using

        End If

        Return TimePeriodCompletedList
    End Function

End Class
Public Class MyOption
    Public Property TimePeriod As String
    Public Property PageURL As String
End Class