Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.ModelBinding
Imports System.Data
Imports System.Threading

Imports System.Collections.Specialized
Imports System.Collections
Imports System.ComponentModel
Imports System.Security.Permissions

Imports RussBucksPools
Imports RussBucksPools.LosersPool.Models
Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool

Public Class WeeklyLoserPoolUserEntry
    Inherits System.Web.UI.Page

    Private GameScheduleCollection As New Dictionary(Of String, DaySchedule)
    Private RadioButtonCollection As New Dictionary(Of String, RadioButton)

    Private TeamBackColor As System.Drawing.Color = Drawing.Color.White

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        Dim rootFolder = CStr(Cache("rootFolder"))
        System.IO.Directory.SetCurrentDirectory(rootFolder)


        Dim _dbLoserPool As New LosersPoolContext

        Dim EName = CStr(Session("userId"))
        Dim dayNumber = CStr(Session("dayNumber"))

        Dim mpl = MyPickList.UserPickList(EName, dayNumber)

        Dim currentDateTime = Date.Now

        Dim minStartTime = currentDateTime.AddYears(1)

        Try

            Dim queryScheduleStartTime = (From game1 In _dbLoserPool.ScheduleEntities
                                          Where game1.DayId = dayNumber
                                          Select game1).ToList

            ' Find Minimum start time for week
            For Each game1 In queryScheduleStartTime

                Dim gameStartDateTime = DateTime.Parse(CDate(game1.StartDate + " " + game1.StartTime))
                If gameStartDateTime < minStartTime Then
                    minStartTime = gameStartDateTime
                End If

            Next

            ' Is current time < min Start Time

            If currentDateTime > minStartTime Then
                Response.Redirect("~/LosersPool/Default.aspx")
            End If

            If mpl Is Nothing Then

                loser1.Visible = True
                GameTable1.Visible = False
                GameTable2.Visible = False
                GameTable3.Visible = False
                GameTable4.Visible = False
                Button1.Visible = False
                Button2.Visible = True
                Button3.Visible = False
                Button4.Visible = True
                Exit Try
            Else
                Button1.Visible = True
                Button2.Visible = False
                Button3.Visible = True
                Button4.Visible = False
                loser1.Visible = False
                If mpl.Washington = True Or mpl.PossibleUserPicks.Contains("Washington") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Washington"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Miami = True Or mpl.PossibleUserPicks.Contains("Miami") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Miami"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Colorado = True Or mpl.PossibleUserPicks.Contains("Colorado") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Colorado"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Arizona = True Or mpl.PossibleUserPicks.Contains("Arizona") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Arizona"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.SanFrancisco = True Or mpl.PossibleUserPicks.Contains("San Francisco") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "San Francisco"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.SanDiego = True Or mpl.PossibleUserPicks.Contains("San Diego") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "San Diego"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Pittsburg = True Or mpl.PossibleUserPicks.Contains("Pittsburg") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Pittsburg"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Cincinnati = True Or mpl.PossibleUserPicks.Contains("Cincinnati") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Cincinnati"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Toronto = True Or mpl.PossibleUserPicks.Contains("Toronto") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Toronto"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.NYYankees = True Or mpl.PossibleUserPicks.Contains("NY Yankees") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "NY Yankees"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Boston = True Or mpl.PossibleUserPicks.Contains("Boston") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Boston"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.TampaBay = True Or mpl.PossibleUserPicks.Contains("Tampa Bay") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Tampa Bay"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Atlanta = True Or mpl.PossibleUserPicks.Contains("Atlanta") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Atlanta"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Philadelphia = True Or mpl.PossibleUserPicks.Contains("Philadelphia") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Philadelphia"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.ChicagoWhiteSox = True Or mpl.PossibleUserPicks.Contains("Chicago White Sox") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Chicago White Sox"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Detroit = True Or mpl.PossibleUserPicks.Contains("Detroit") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Detroit"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.KansasCity = True Or mpl.PossibleUserPicks.Contains("Kansas City") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Kansas City"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Cleveland = True Or mpl.PossibleUserPicks.Contains("Cleveland") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Cleveland"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Milwaukee = True Or mpl.PossibleUserPicks.Contains("Milwaukee") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Milwaukee"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.LADodgers = True Or mpl.PossibleUserPicks.Contains("LA Dodgers") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "LA Dodgers"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Minnesota = True Or mpl.PossibleUserPicks.Contains("Minnesota") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Minnesota"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Oakland = True Or mpl.PossibleUserPicks.Contains("Oakland") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Oakland"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Houston = True Or mpl.PossibleUserPicks.Contains("Houston") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Houston"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Texas = True Or mpl.PossibleUserPicks.Contains("Texas") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Texas"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.STLouis = True Or mpl.PossibleUserPicks.Contains("St. Louis") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "St. Louis"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.ChicagoCubs = True Or mpl.PossibleUserPicks.Contains("Chicago Cubs") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Chicago Cubs"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.LAAngels = True Or mpl.PossibleUserPicks.Contains("LA Angels") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "LA Angels"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Seattle = True Or mpl.PossibleUserPicks.Contains("Seattle") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Seattle"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.NYMets = True Or mpl.PossibleUserPicks.Contains("NY Mets") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "NY Mets"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                If mpl.Baltimore = True Or mpl.PossibleUserPicks.Contains("Baltimore") Then
                    Dim MyPick1 As New MyPick
                    MyPick1.ListId = _dbLoserPool.MyPicks.Count + 1
                    MyPick1.PossibleTeam = "Baltimore"
                    _dbLoserPool.MyPicks.Add(MyPick1)
                End If

                _dbLoserPool.SaveChanges()

            End If

            Dim queryByeTeams = (From team1 In _dbLoserPool.ByeTeamsList
                                 Where team1.DayId = dayNumber).ToList
            For Each team1 In queryByeTeams

                Dim queryMyPicks = (From pick1 In _dbLoserPool.MyPicks
                                    Where pick1.PossibleTeam = team1.TeamName).ToList

                If queryMyPicks.Count > 0 Then
                    _dbLoserPool.MyPicks.Remove(queryMyPicks(0))
                End If
            Next
            _dbLoserPool.SaveChanges()


            Dim dayGameSchedule = (From game1 In _dbLoserPool.ScheduleEntities
                                   Where game1.DayId = dayNumber).ToList

            Dim cnt = 1
            For Each game1 In dayGameSchedule

                Dim daySchedule1 = New DaySchedule
                daySchedule1.GameId = game1.GameId
                daySchedule1.DayId = game1.DayId
                daySchedule1.StartDate = game1.StartDate
                daySchedule1.StartTime = game1.StartTime
                daySchedule1.HomeTeam = game1.HomeTeam
                daySchedule1.AwayTeam = game1.AwayTeam

                If game1.HomeTeam = "Washington" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Washington Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Washington.png"

                ElseIf game1.HomeTeam = "Miami" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Miami Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Miami.png"
                ElseIf game1.HomeTeam = "Colorado" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Colorado Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Colorado.png"

                ElseIf game1.HomeTeam = "Arizona" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Arizona Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Arizona.png"

                ElseIf game1.HomeTeam = "San Francisco" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/San Francisco Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/San Francisco.png"

                ElseIf game1.HomeTeam = "San Diego" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/San Diego Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/San Diego.png"

                ElseIf game1.HomeTeam = "Pittsburg" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Pittsburg Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Pittsburg.png"

                ElseIf game1.HomeTeam = "Cincinnati" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Cincinnati Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Cincinnati.png"

                ElseIf game1.HomeTeam = "Toronto" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Toronto Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Toronto.png"

                ElseIf game1.HomeTeam = "NY Yankees" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/NY Yankees Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/NY Yankees.png"

                ElseIf game1.HomeTeam = "Boston" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Boston Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Boston.png"

                ElseIf game1.HomeTeam = "Tampa Bay" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Tampa Bay Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Tampa Bay.png"

                ElseIf game1.HomeTeam = "Atlanta" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Atlanta Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Atlanta.png"

                ElseIf game1.HomeTeam = "Philadelphia" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Philadelphia Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Philadelphia.png"

                ElseIf game1.HomeTeam = "Chicago White Sox" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Chicago White Sox Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Chicago White Sox.png"

                ElseIf game1.HomeTeam = "Detroit" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Detroit Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Detroit.png"

                ElseIf game1.HomeTeam = "Kansas City" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Kansas City Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Kansas City.png"

                ElseIf game1.HomeTeam = "Cleveland" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Cleveland Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Cleveland.png"

                ElseIf game1.HomeTeam = "Milwaukee" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Milwaukee Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Milwaukee.png"

                ElseIf game1.HomeTeam = "LA Dodgers" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/LA Dodgers Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/LA Dodgers.png"

                ElseIf game1.HomeTeam = "Minnesota" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Minnesota Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Minnesota.png"

                ElseIf game1.HomeTeam = "Oakland" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Oakland Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Oakland.png"

                ElseIf game1.HomeTeam = "Houston" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Houston Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Houston.png"

                ElseIf game1.HomeTeam = "Texas" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Texas Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Texas.png"

                ElseIf game1.HomeTeam = "St. Louis" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/St. Louis Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/St. Louis.png"

                ElseIf game1.HomeTeam = "Chicago Cubs" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Chicago Cubs Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Chicago Cubs.png"

                ElseIf game1.HomeTeam = "LA Angels" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/LA Angels Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/LA Angels.png"

                ElseIf game1.HomeTeam = "Seattle" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Seattle Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Seattle.png"

                ElseIf game1.HomeTeam = "NY Mets" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/NY Mets Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/NY Mets.png"

                ElseIf game1.HomeTeam = "Baltimore" Then
                    daySchedule1.HomeTeamImage = "~/MLB ICONS/Baltimore Label.png"
                    daySchedule1.HomeTeamIcon = "~/MLB ICONS/Baltimore.png"

                End If

                If game1.AwayTeam = "Washington" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Washington Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Washington.png"

                ElseIf game1.AwayTeam = "Miami" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Miami Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Miami.png"
                ElseIf game1.AwayTeam = "Colorado" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Colorado Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Colorado.png"

                ElseIf game1.AwayTeam = "Arizona" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Arizona Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Arizona.png"

                ElseIf game1.AwayTeam = "San Francisco" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/San Francisco Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/San Francisco.png"

                ElseIf game1.AwayTeam = "San Diego" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/San Diego Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/San Diego.png"

                ElseIf game1.AwayTeam = "Pittsburg" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Pittsburg Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Pittsburg.png"

                ElseIf game1.AwayTeam = "Cincinnati" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Cincinnati Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Cincinnati.png"

                ElseIf game1.AwayTeam = "Toronto" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Toronto Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Toronto.png"

                ElseIf game1.AwayTeam = "NY Yankees" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/NY Yankees Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/NY Yankees.png"

                ElseIf game1.AwayTeam = "Boston" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Boston Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Boston.png"

                ElseIf game1.AwayTeam = "Tampa Bay" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Tampa Bay Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Tampa Bay.png"

                ElseIf game1.AwayTeam = "Atlanta" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Atlanta Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Atlanta.png"

                ElseIf game1.AwayTeam = "Philadelphia" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Philadelphia Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Philadelphia.png"

                ElseIf game1.AwayTeam = "Chicago White Sox" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Chicago White Sox Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Chicago White Sox.png"

                ElseIf game1.AwayTeam = "Detroit" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Detroit Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Detroit.png"

                ElseIf game1.AwayTeam = "Kansas City" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Kansas City Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Kansas City.png"

                ElseIf game1.AwayTeam = "Cleveland" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Cleveland Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Cleveland.png"

                ElseIf game1.AwayTeam = "Milwaukee" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Milwaukee Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Milwaukee.png"

                ElseIf game1.AwayTeam = "LA Dodgers" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/LA Dodgers Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/LA Dodgers.png"

                ElseIf game1.AwayTeam = "Minnesota" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Minnesota Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Minnesota.png"

                ElseIf game1.AwayTeam = "Oakland" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Oakland Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Oakland.png"

                ElseIf game1.AwayTeam = "Houston" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Houston Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Houston.png"

                ElseIf game1.AwayTeam = "Texas" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Texas Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Texas.png"

                ElseIf game1.AwayTeam = "St. Louis" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/St. Louis Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/St. Louis.png"

                ElseIf game1.AwayTeam = "Chicago Cubs" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Chicago Cubs Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Chicago Cubs.png"

                ElseIf game1.AwayTeam = "LA Angels" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/LA Angels Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/LA Angels.png"

                ElseIf game1.AwayTeam = "Seattle" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Seattle Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Seattle.png"

                ElseIf game1.AwayTeam = "NY Mets" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/NY Mets Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/NY Mets.png"

                ElseIf game1.AwayTeam = "Baltimore" Then
                    daySchedule1.AwayTeamImage = "~/MLB ICONS/Baltimore Label.png"
                    daySchedule1.AwayTeamIcon = "~/MLB ICONS/Baltimore.png"

                End If


                GameScheduleCollection.Add(daySchedule1.GameId, daySchedule1)

            Next

            Dim queryGameScheduleCollection = (From GameScheduleCollection1 In GameScheduleCollection
                                       Where GameScheduleCollection1.Key = "game1" Or GameScheduleCollection1.Key = "game2" Or GameScheduleCollection1.Key = "game3" Or GameScheduleCollection1.Key = "game4").ToList

            If queryGameScheduleCollection.Count > 0 Then
                CreateTable1(queryGameScheduleCollection)
            ElseIf queryGameScheduleCollection.Count = 0 Then
                GameTable1.Visible = False
            End If

            queryGameScheduleCollection = (From GameScheduleCollection1 In GameScheduleCollection
                                       Where GameScheduleCollection1.Key = "game5" Or GameScheduleCollection1.Key = "game6" Or GameScheduleCollection1.Key = "game7" Or GameScheduleCollection1.Key = "game8").ToList

            If queryGameScheduleCollection.Count > 0 Then
                CreateTable2(queryGameScheduleCollection)
            ElseIf queryGameScheduleCollection.Count = 0 Then
                GameTable2.Visible = False
            End If

            queryGameScheduleCollection = (From GameScheduleCollection1 In GameScheduleCollection
                                       Where GameScheduleCollection1.Key = "game9" Or GameScheduleCollection1.Key = "game10" Or GameScheduleCollection1.Key = "game11" Or GameScheduleCollection1.Key = "game12").ToList

            If queryGameScheduleCollection.Count > 0 Then
                CreateTable3(queryGameScheduleCollection)
            ElseIf queryGameScheduleCollection.Count = 0 Then
                GameTable3.Visible = False
            End If


            queryGameScheduleCollection = (From GameScheduleCollection1 In GameScheduleCollection
                                       Where GameScheduleCollection1.Key = "game13" Or GameScheduleCollection1.Key = "game14" Or GameScheduleCollection1.Key = "game15").ToList

            If queryGameScheduleCollection.Count > 0 Then
                CreateTable4(queryGameScheduleCollection)
            ElseIf queryGameScheduleCollection.Count = 0 Then
                GameTable4.Visible = False
            End If


            Dim queryMyPicks1 = (From pick1 In _dbLoserPool.MyPicks).ToList


            For Each pick1 In queryMyPicks1

                Dim queryPickedGame = (From game1 In _dbLoserPool.ScheduleEntities
                                         Where game1.DayId = dayNumber And (game1.HomeTeam = pick1.PossibleTeam Or game1.AwayTeam = pick1.PossibleTeam)
                                         Order By game1.GameId).ToList


                If queryPickedGame.Count > 0 Then

                    If queryPickedGame(0).GameId = "game1" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game1HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game1HomeRadio.ID, Game1HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game1AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game1AwayRadio.ID, Game1AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game2" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game2HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game2HomeRadio.ID, Game2HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game2AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game2AwayRadio.ID, Game2AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game3" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game3HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game3HomeRadio.ID, Game3HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game3AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game3AwayRadio.ID, Game3AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game4" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game4HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game4HomeRadio.ID, Game4HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game4AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game4AwayRadio.ID, Game4AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game5" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game5HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game5HomeRadio.ID, Game5HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game5AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game5AwayRadio.ID, Game5AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game6" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game6HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game6HomeRadio.ID, Game6HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game6AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game6AwayRadio.ID, Game6AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game7" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game7HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game7HomeRadio.ID, Game7HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game7AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game7AwayRadio.ID, Game7AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game8" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game8HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game8HomeRadio.ID, Game8HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game8AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game8AwayRadio.ID, Game8AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game9" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game9HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game9HomeRadio.ID, Game9HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game9AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game9AwayRadio.ID, Game9AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game10" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game10HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game10HomeRadio.ID, Game10HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game10AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game10AwayRadio.ID, Game10AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game11" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game11HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game11HomeRadio.ID, Game11HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game11AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game11AwayRadio.ID, Game11AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game12" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game12HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game12HomeRadio.ID, Game12HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game12AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game12AwayRadio.ID, Game12AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game13" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game13HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game13HomeRadio.ID, Game13HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game13AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game13AwayRadio.ID, Game13AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game14" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game14HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game14HomeRadio.ID, Game14HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game14AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game14AwayRadio.ID, Game14AwayRadio)
                        End If
                    End If

                    If queryPickedGame(0).GameId = "game15" Then
                        If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                            Game15HomeRadio.Visible = True
                            RadioButtonCollection.Add(Game15HomeRadio.ID, Game15HomeRadio)
                        ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                            Game15AwayRadio.Visible = True
                            RadioButtonCollection.Add(Game15AwayRadio.ID, Game15AwayRadio)
                        End If
                    End If

                End If
                cnt = cnt + 1
            Next

        Catch ex As Exception

        End Try


    End Sub


    Private Sub CreateTable1(qGSC As List(Of KeyValuePair(Of String, DaySchedule)))

        For Each game In qGSC

            If game.Key = "game1" Then
                GameNumber1.Text = game.Key
                HomeTeam1Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam1Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam1Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam1Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon
                GameNumber1Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam1Image1.BackColor = TeamBackColor
                AwayTeam1Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game2" And qGSC.Count >= 2 Then
                GameNumber2.Text = game.Key
                HomeTeam2Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam2Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam2Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam2Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon
                GameNumber2Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam2Image1.BackColor = TeamBackColor
                AwayTeam2Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game3" And qGSC.Count >= 3 Then
                GameNumber3.Text = game.Key
                HomeTeam3Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam3Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam3Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam3Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon
                GameNumber3Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam3Image1.BackColor = TeamBackColor
                AwayTeam3Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game4" And qGSC.Count = 4 Then
                GameNumber4.Text = game.Key
                HomeTeam4Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam4Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam4Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam4Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon
                GameNumber4Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam4Image1.BackColor = TeamBackColor
                AwayTeam4Image1.BackColor = TeamBackColor
            End If
        Next

        If qGSC.Count <= 1 Then

            GameNumber2.Visible = False
            GameNumber2Status.Visible = False
            HomeTeam2Image.Visible = False
            HomeTeam2Icon.Visible = False
            AwayTeam2Image.Visible = False
            AwayTeam2Icon.Visible = False
            HomePick2.Visible = False
            AwayPick2.Visible = False

        ElseIf qGSC.Count <= 2 Then

            GameNumber3.Visible = False
            GameNumber3Status.Visible = False
            HomeTeam3Image.Visible = False
            HomeTeam3Icon.Visible = False
            AwayTeam3Image.Visible = False
            AwayTeam3Icon.Visible = False
            HomePick3.Visible = False
            AwayPick3.Visible = False



        ElseIf qGSC.Count <= 3 Then
            GameNumber4.Visible = False
            GameNumber4Status.Visible = False
            HomeTeam4Image.Visible = False
            HomeTeam4Icon.Visible = False
            AwayTeam4Image.Visible = False
            AwayTeam4Icon.Visible = False
            HomePick4.Visible = False
            AwayPick4.Visible = False

        End If


        If GameScheduleCollection.Count >= 1 Then
            GameTable1.DataBind()
        End If

    End Sub

    Private Sub CreateTable2(qGSC As List(Of KeyValuePair(Of String, DaySchedule)))

        For Each game In qGSC

            If game.Key = "game5" Then
                GameNumber5.Text = game.Key
                HomeTeam5Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam5Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam5Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam5Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon
                GameNumber5Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam5Image1.BackColor = TeamBackColor
                AwayTeam5Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game6" And qGSC.Count >= 2 Then
                GameNumber6.Text = game.Key
                HomeTeam6Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam6Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam6Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam6Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon
                GameNumber6Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam6Image1.BackColor = TeamBackColor
                AwayTeam6Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game7" And qGSC.Count >= 3 Then
                GameNumber7.Text = game.Key
                HomeTeam7Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam7Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam7Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam7Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon
                GameNumber7Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam7Image1.BackColor = TeamBackColor
                AwayTeam7Image1.BackColor = TeamBackColor
            ElseIf game.Key = "game8" And qGSC.Count = 4 Then
                GameNumber8.Text = game.Key
                HomeTeam8Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam8Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam8Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam8Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon
                GameNumber8Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam8Image1.BackColor = TeamBackColor
                AwayTeam8Image1.BackColor = TeamBackColor
            End If
        Next

        If qGSC.Count <= 1 Then

            GameNumber6.Visible = False
            GameNumber6Status.Visible = False
            HomeTeam6Image.Visible = False
            HomeTeam6Icon.Visible = False
            AwayTeam6Image.Visible = False
            AwayTeam6Icon.Visible = False
            HomePick6.Visible = False
            AwayPick6.Visible = False

        ElseIf qGSC.Count <= 2 Then

            GameNumber7.Visible = False
            GameNumber7Status.Visible = False
            HomeTeam7Image.Visible = False
            HomeTeam7Icon.Visible = False
            AwayTeam7Image.Visible = False
            AwayTeam7Icon.Visible = False
            HomePick7.Visible = False
            AwayPick7.Visible = False

        ElseIf qGSC.Count <= 3 Then

            GameNumber8.Visible = False
            GameNumber8Status.Visible = False
            HomeTeam8Image.Visible = False
            HomeTeam8Icon.Visible = False
            AwayTeam8Image.Visible = False
            AwayTeam8Icon.Visible = False
            HomePick8.Visible = False
            AwayPick8.Visible = False

        End If


        If GameScheduleCollection.Count >= 5 Then
            GameTable2.DataBind()
        End If

    End Sub

    Private Sub CreateTable3(qGSC As List(Of KeyValuePair(Of String, DaySchedule)))

        For Each game In qGSC
            If game.Key = "game9" Then
                GameNumber9.Text = game.Key
                HomeTeam9Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam9Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam9Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam9Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon

                GameNumber9Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam9Image1.BackColor = TeamBackColor
                AwayTeam9Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game10" And qGSC.Count >= 2 Then
                GameNumber10.Text = game.Key
                HomeTeam10Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam10Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam10Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam10Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon

                GameNumber10Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam10Image1.BackColor = TeamBackColor
                AwayTeam10Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game11" And qGSC.Count >= 3 Then
                GameNumber11.Text = game.Key
                HomeTeam11Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam11Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam11Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam11Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon

                GameNumber11Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam11Image1.BackColor = TeamBackColor
                AwayTeam11Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game12" And qGSC.Count = 4 Then
                GameNumber12.Text = game.Key
                HomeTeam12Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam12Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam12Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam12Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon

                GameNumber12Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam12Image1.BackColor = TeamBackColor
                AwayTeam12Image1.BackColor = TeamBackColor

            End If

        Next

        If qGSC.Count = 1 Then

            GameNumber10.Visible = False
            GameNumber10Status.Visible = False
            HomeTeam10Image.Visible = False
            HomeTeam10Icon.Visible = False
            AwayTeam10Image.Visible = False
            AwayTeam10Icon.Visible = False
            HomePick10.Visible = False
            AwayPick10.Visible = False
        End If

        If qGSC.Count <= 2 Then

            GameNumber11.Visible = False
            GameNumber11Status.Visible = False
            HomeTeam11Image.Visible = False
            HomeTeam11Icon.Visible = False
            AwayTeam11Image.Visible = False
            AwayTeam11Icon.Visible = False
            HomePick11.Visible = False
            AwayPick11.Visible = False
        End If

        If qGSC.Count <= 3 Then
            GameNumber12.Visible = False
            GameNumber12Status.Visible = False
            HomeTeam12Image.Visible = False
            HomeTeam12Icon.Visible = False
            AwayTeam12Image.Visible = False
            AwayTeam12Icon.Visible = False
            HomePick12.Visible = False
            AwayPick12.Visible = False

        End If

        If GameScheduleCollection.Count >= 9 Then
            GameTable3.DataBind()
        End If

    End Sub

    Private Sub CreateTable4(qGSC As List(Of KeyValuePair(Of String, DaySchedule)))

        For Each game In qGSC
            If game.Key = "game13" Then
                GameNumber13.Text = game.Key
                HomeTeam13Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam13Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam13Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam13Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon

                GameNumber13Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam13Image1.BackColor = TeamBackColor
                AwayTeam13Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game14" And qGSC.Count >= 2 Then
                GameNumber14.Text = game.Key
                HomeTeam14Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam14Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam14Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam14Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon

                GameNumber14Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam14Image1.BackColor = TeamBackColor
                AwayTeam14Image1.BackColor = TeamBackColor

            ElseIf game.Key = "game15" And qGSC.Count = 3 Then
                GameNumber15.Text = game.Key
                HomeTeam15Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam15Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam15Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam15Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon

                GameNumber15Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam15Image1.BackColor = TeamBackColor
                AwayTeam15Image1.BackColor = TeamBackColor

            End If

        Next

        If qGSC.Count <= 1 Then

            GameNumber14.Visible = False
            GameNumber14Status.Visible = False
            HomeTeam14Image.Visible = False
            HomeTeam14Icon.Visible = False
            AwayTeam14Image.Visible = False
            AwayTeam14Icon.Visible = False
            HomePick14.Visible = False
            AwayPick14.Visible = False
        End If

        If qGSC.Count <= 2 Then

            GameNumber15.Visible = False
            GameNumber15Status.Visible = False
            HomeTeam15Image.Visible = False
            HomeTeam15Icon.Visible = False
            AwayTeam15Image.Visible = False
            AwayTeam15Icon.Visible = False
            HomePick15.Visible = False
            AwayPick15.Visible = False

        End If


        If GameScheduleCollection.Count >= 13 Then
            GameTable4.DataBind()
        End If

    End Sub



    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim EName = CStr(Session("userId"))
        Dim dayNumber = CStr(Session("dayNumber"))
        Dim I2 As Integer

        'day increment = 2 due to betting every other day

        I2 = CInt(Cache("timeIncrement"))

        Dim lastDay = "day" + CStr(CInt(Mid(dayNumber, 4)) - I2)
        If lastDay = "day-1" Then
            lastDay = "day0"
        End If

        Dim _DbLoserPool As New LosersPoolContext
        Using (_DbLoserPool)
            Try

                Dim lastDayChoice As New UserChoices
                lastDayChoice = _DbLoserPool.UserChoicesList.SingleOrDefault(Function(lWC) lWC.UserID = EName And lWC.DayId = lastDay)
                lastDayChoice = SetContendersPickToFalse(lastDayChoice)
                lastDayChoice.DayId = dayNumber
                lastDayChoice.UserPick = ""

                Dim userChoice1 = New UserChoices
                userChoice1 = _DbLoserPool.UserChoicesList.SingleOrDefault(Function(uC1) uC1.UserID = EName And uC1.DayId = dayNumber)
                _DbLoserPool.UserChoicesList.Remove(userChoice1)
                _DbLoserPool.UserChoicesList.Add(lastDayChoice)

                _DbLoserPool.SaveChanges()

                userChoice1 = New UserChoices
                userChoice1 = _DbLoserPool.UserChoicesList.SingleOrDefault(Function(uC1) uC1.UserID = EName And uC1.DayId = dayNumber)

                Dim userPick1 = ""

                For Each radio1 In RadioButtonCollection

                    If radio1.Key = "Game1HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game1").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game1AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game1").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game2HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game2").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game2AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game2").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game3HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game3").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game3AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game3").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game4HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game4").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game4AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game4").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game5HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game5").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game5AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game5").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game6HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game6").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game6AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game6").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game7HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game7").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game7AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game7").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game8HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game8").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game8AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game8").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game9HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game9").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game9AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game9").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game14HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game14").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game14AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game14").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game11HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game11").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game11AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game11").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game12HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game12").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game12AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game12").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game13HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game13").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game13AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game13").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game14HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game14").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game14AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game14").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game15HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game15").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game15AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game15").AwayTeam
                        End If
                    End If

                    If radio1.Key = "Game16HomeRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game16").HomeTeam
                        End If
                    End If

                    If radio1.Key = "Game16AwayRadio" Then
                        If radio1.Value.Checked = True Then
                            userPick1 = GameScheduleCollection("game16").AwayTeam
                        End If
                    End If

                Next

                If userPick1 <> "" Then

                    If userPick1 = "Washington" Then
                        userChoice1.Washington = False
                        userChoice1.UserPick = "Washington"
                    End If

                    If userPick1 = "Miami" Then
                        userChoice1.Miami = False
                        userChoice1.UserPick = "Miami"
                    End If

                    If userPick1 = "Colorado" Then
                        userChoice1.Colorado = False
                        userChoice1.UserPick = "Colorado"
                    End If

                    If userPick1 = "Arizona" Then
                        userChoice1.Arizona = False
                        userChoice1.UserPick = "Arizona"
                    End If

                    If userPick1 = "San Francisco" Then
                        userChoice1.SanFrancisco = False
                        userChoice1.UserPick = "San Francisco"
                    End If

                    If userPick1 = "San Diego" Then
                        userChoice1.SanDiego = False
                        userChoice1.UserPick = "San Diego"
                    End If

                    If userPick1 = "Pittsburg" Then
                        userChoice1.Pittsburg = False
                        userChoice1.UserPick = "Pittsburg"
                    End If

                    If userPick1 = "Cincinnati" Then
                        userChoice1.Cincinnati = False
                        userChoice1.UserPick = "Cincinnati"
                    End If

                    If userPick1 = "Toronto" Then
                        userChoice1.Toronto = False
                        userChoice1.UserPick = "Toronto"
                    End If

                    If userPick1 = "NY Yankees" Then
                        userChoice1.NYYankees = False
                        userChoice1.UserPick = "NY Yankees"
                    End If

                    If userPick1 = "Boston" Then
                        userChoice1.Boston = False
                        userChoice1.UserPick = "Boston"
                    End If

                    If userPick1 = "Tampa Bay" Then
                        userChoice1.TampaBay = False
                        userChoice1.UserPick = "Tampa Bay"
                    End If

                    If userPick1 = "Atlanta" Then
                        userChoice1.Atlanta = False
                        userChoice1.UserPick = "Atlanta"
                    End If

                    If userPick1 = "Philadelphia" Then
                        userChoice1.Philadelphia = False
                        userChoice1.UserPick = "Philadelphia"
                    End If

                    If userPick1 = "Chicago White Sox" Then
                        userChoice1.ChicagoWhiteSox = False
                        userChoice1.UserPick = "Chicago White Sox"
                    End If

                    If userPick1 = "Detroit" Then
                        userChoice1.Detroit = False
                        userChoice1.UserPick = "Detroit"
                    End If

                    If userPick1 = "Kansas City" Then
                        userChoice1.KansasCity = False
                        userChoice1.UserPick = "Kansas City"
                    End If

                    If userPick1 = "Cleveland" Then
                        userChoice1.Cleveland = False
                        userChoice1.UserPick = "Cleveland"
                    End If

                    If userPick1 = "Milwaukee" Then
                        userChoice1.Milwaukee = False
                        userChoice1.UserPick = "Milwaukee"
                    End If

                    If userPick1 = "LA Dodgers" Then
                        userChoice1.LADodgers = False
                        userChoice1.UserPick = "LA Dodgers"
                    End If

                    If userPick1 = "Minnesota" Then
                        userChoice1.Minnesota = False
                        userChoice1.UserPick = "Minnesota"
                    End If

                    If userPick1 = "Oakland" Then
                        userChoice1.Oakland = False
                        userChoice1.UserPick = "Oakland"
                    End If

                    If userPick1 = "Houston" Then
                        userChoice1.Houston = False
                        userChoice1.UserPick = "Houston"
                    End If

                    If userPick1 = "Texas" Then
                        userChoice1.Texas = False
                        userChoice1.UserPick = "Texas"
                    End If

                    If userPick1 = "St. Louis" Then
                        userChoice1.STLouis = False
                        userChoice1.UserPick = "St. Louis"
                    End If

                    If userPick1 = "Chicago Cubs" Then
                        userChoice1.ChicagoCubs = False
                        userChoice1.UserPick = "Chicago Cubs"
                    End If

                    If userPick1 = "LA Angels" Then
                        userChoice1.LAAngels = False
                        userChoice1.UserPick = "LAAngels"
                    End If


                    If userPick1 = "Seattle" Then
                        userChoice1.Seattle = False
                        userChoice1.UserPick = "Seattle"
                    End If

                    If userPick1 = "NY Mets" Then
                        userChoice1.NYMets = False
                        userChoice1.UserPick = "NY Mets"
                    End If

                    If userPick1 = "Baltimore" Then
                        userChoice1.Baltimore = False
                        userChoice1.UserPick = "Baltimore"
                    End If

                    userChoice1.ConfirmationNumber = userChoice1.ListId

                    _DbLoserPool.SaveChanges()

                    Session("confirmationNumber") = userChoice1.ListId

                End If

            Catch ex As Exception

            End Try
        End Using

        Response.Redirect("~/LosersPool/ContenderConfirmation.aspx")

    End Sub

    Private Shared Function SetContendersPickToFalse(user1 As UserChoices) As UserChoices
        If user1.UserPick = "Washington" Then
            user1.Washington = False
        ElseIf user1.UserPick = "Miami" Then
            user1.Miami = False
        ElseIf user1.UserPick = "Colorado" Then
            user1.Colorado = False
        ElseIf user1.UserPick = "Arizona" Then
            user1.Arizona = False
        ElseIf user1.UserPick = "San Francisco" Then
            user1.SanFrancisco = False
        ElseIf user1.UserPick = "San Diego" Then
            user1.SanDiego = False
        ElseIf user1.UserPick = "Pittsburg" Then
            user1.Pittsburg = False
        ElseIf user1.UserPick = "Cincinnati" Then
            user1.Cincinnati = False
        ElseIf user1.UserPick = "Toronto" Then
            user1.Toronto = False
        ElseIf user1.UserPick = "NY Yankees" Then
            user1.NYYankees = False
        ElseIf user1.UserPick = "Boston" Then
            user1.Boston = False
        ElseIf user1.UserPick = "Tampa Bay" Then
            user1.TampaBay = False
        ElseIf user1.UserPick = "Atlanta" Then
            user1.Atlanta = False
        ElseIf user1.UserPick = "Philadelphia" Then
            user1.Philadelphia = False
        ElseIf user1.UserPick = "Chicago White Sox" Then
            user1.ChicagoWhiteSox = False
        ElseIf user1.UserPick = "Detroit" Then
            user1.Detroit = False
        ElseIf user1.UserPick = "KansasCity" Then
            user1.KansasCity = False
        ElseIf user1.UserPick = "Cleveland" Then
            user1.Cleveland = False
        ElseIf user1.UserPick = "Milwaukee" Then
            user1.Milwaukee = False
        ElseIf user1.UserPick = "LA Dodgers" Then
            user1.LADodgers = False
        ElseIf user1.UserPick = "Minnesota" Then
            user1.Minnesota = False
        ElseIf user1.UserPick = "Oakland" Then
            user1.Oakland = False
        ElseIf user1.UserPick = "Houston" Then
            user1.Houston = False
        ElseIf user1.UserPick = "Texas" Then
            user1.Texas = False
        ElseIf user1.UserPick = "St. Louis" Then
            user1.STLouis = False
        ElseIf user1.UserPick = "ChicagoCubs" Then
            user1.ChicagoCubs = False
        ElseIf user1.UserPick = "LA Angels" Then
            user1.LAAngels = False
        ElseIf user1.UserPick = "Seattle" Then
            user1.Seattle = False
        ElseIf user1.UserPick = "NY Mets" Then
            user1.NYMets = False
        ElseIf user1.UserPick = "Baltimore" Then
            user1.Baltimore = False

        End If
        Return user1
    End Function


    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/LosersPool/Default.aspx")
    End Sub
End Class

Public Class MyPickList
    Public Property MyPicks As UserChoices

    Public Sub New(Ename As String, dayNumber As String)

        Dim user1 As UserChoices = UserPickList(Ename, dayNumber)
        Me.MyPicks = user1

    End Sub
    Public Shared Function UserPickList(EName As String, dayNumber As String) As UserChoices
        Dim _DbLoserPool = New LosersPoolContext

        Try
            Using (_DbLoserPool)

                ' Clear Previous Picks
                Dim users1 = New List(Of MyPick)
                users1 = _DbLoserPool.MyPicks.ToList

                For Each user2 In users1
                    _DbLoserPool.MyPicks.Remove(user2)
                Next

                _DbLoserPool.SaveChanges()

                ' Get New User

                Dim user1 = New UserChoices
                user1 = _DbLoserPool.UserChoicesList.SingleOrDefault(Function(u1) u1.UserID = EName And u1.DayId = dayNumber)

                If user1 Is Nothing Then
                    Return Nothing
                Else
                    If user1.Washington = True Or user1.UserPick = "Washington" Then
                        user1.PossibleUserPicks.Add("Washington")
                    End If

                    If user1.Miami = True Or user1.UserPick = "Miami" Then
                        user1.PossibleUserPicks.Add("Miami")
                    End If

                    If user1.Colorado = True Or user1.UserPick = "Colorado" Then
                        user1.PossibleUserPicks.Add("Colorado")
                    End If

                    If user1.Arizona = True Or user1.UserPick = "Arizona" Then
                        user1.PossibleUserPicks.Add("Arizona")
                    End If

                    If user1.SanFrancisco = True Or user1.UserPick = "San Francisco" Then
                        user1.PossibleUserPicks.Add("San Francisco")
                    End If

                    If user1.SanDiego = True Or user1.UserPick = "San Diego" Then
                        user1.PossibleUserPicks.Add("San Diego")
                    End If

                    If user1.Pittsburg = True Or user1.UserPick = "Pittsburg" Then
                        user1.PossibleUserPicks.Add("Pittsburg")
                    End If

                    If user1.Cincinnati = True Or user1.UserPick = "Cincinnati" Then
                        user1.PossibleUserPicks.Add("Cincinnati")
                    End If

                    If user1.Toronto = True Or user1.UserPick = "Toronto" Then
                        user1.PossibleUserPicks.Add("Toronto")
                    End If

                    If user1.NYYankees = True Or user1.UserPick = "NY Yankees" Then
                        user1.PossibleUserPicks.Add("NY Yankees")
                    End If

                    If user1.Boston = True Or user1.UserPick = "Boston" Then
                        user1.PossibleUserPicks.Add("Boston")
                    End If

                    If user1.TampaBay = True Or user1.UserPick = "Tampa Bay" Then
                        user1.PossibleUserPicks.Add("Tampa Bay")
                    End If

                    If user1.Atlanta = True Or user1.UserPick = "Atlanta" Then
                        user1.PossibleUserPicks.Add("Atlanta")
                    End If

                    If user1.Philadelphia = True Or user1.UserPick = "Philadelphia" Then
                        user1.PossibleUserPicks.Add("Philadelphia")
                    End If

                    If user1.ChicagoWhiteSox = True Or user1.UserPick = "Chicago White Sox" Then
                        user1.PossibleUserPicks.Add("Chicago White Sox")
                    End If

                    If user1.Detroit = True Or user1.UserPick = "Detroit" Then
                        user1.PossibleUserPicks.Add("Detroit")
                    End If

                    If user1.KansasCity = True Or user1.UserPick = "Kansas City" Then
                        user1.PossibleUserPicks.Add("Kansas City")
                    End If

                    If user1.Cleveland = True Or user1.UserPick = "Cleveland" Then
                        user1.PossibleUserPicks.Add("Cleveland")
                    End If

                    If user1.Milwaukee = True Or user1.UserPick = "Milwaukee" Then
                        user1.PossibleUserPicks.Add("Milwaukee")
                    End If

                    If user1.LADodgers = True Or user1.UserPick = "LA Dodgers" Then
                        user1.PossibleUserPicks.Add("LA Dodgers")
                    End If

                    If user1.Minnesota = True Or user1.UserPick = "Minnesota " Then
                        user1.PossibleUserPicks.Add("Minnesota ")
                    End If

                    If user1.Oakland = True Or user1.UserPick = "Oakland" Then
                        user1.PossibleUserPicks.Add("Oakland")
                    End If

                    If user1.Houston = True Or user1.UserPick = "Houston" Then
                        user1.PossibleUserPicks.Add("Houston")
                    End If

                    If user1.Texas = True Or user1.UserPick = "Texas" Then
                        user1.PossibleUserPicks.Add("Texas")
                    End If

                    If user1.STLouis = True Or user1.UserPick = "St. Louis" Then
                        user1.PossibleUserPicks.Add("St. Louis")
                    End If

                    If user1.ChicagoCubs = True Or user1.UserPick = "Chicago Cubs" Then
                        user1.PossibleUserPicks.Add("Chicago Cubs")
                    End If

                    If user1.LAAngels = True Or user1.UserPick = "LA Angels" Then
                        user1.PossibleUserPicks.Add("LA Angels")
                    End If

                    If user1.Seattle = True Or user1.UserPick = "Seattle" Then
                        user1.PossibleUserPicks.Add("Seattle")
                    End If

                    If user1.NYMets = True Or user1.UserPick = "NY Mets" Then
                        user1.PossibleUserPicks.Add("NY Mets")
                    End If

                    If user1.Baltimore = True Or user1.UserPick = "Baltimore" Then
                        user1.PossibleUserPicks.Add("Baltimore")
                    End If

                    Return user1

                End If

            End Using
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

End Class
