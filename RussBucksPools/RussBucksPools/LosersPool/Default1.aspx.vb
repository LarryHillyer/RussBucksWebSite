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

Public Class _Default2
    Inherits System.Web.UI.Page

    Private I2 As New Integer
    Private rootFolder As String


    Private DayCompletedCollection As New Dictionary(Of String, DayCompleted)
    Private DayCompletedList As New List(Of DayCompleted)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        If Not Page.IsPostBack Then

            Dim rootFolder = CStr(Cache("rootFolder"))
            System.IO.Directory.SetCurrentDirectory(rootFolder)

            'First two are need for to use test driver
            'read UserList XML and save to empty Users Table of LoserPool2 database
            'Dim userList1 = New UserList(".\TestFiles\UserList.xml")

            ' read userChoices XML and save to empty userChoices Table of LoserPool2 database
            'Dim userChoices1 = New UserChoiceList(".\TestFiles\UserChoicesList.xml", "day1", rootFolder)

            'If reading only one week of schedule
            'to read a weekly XML schedule file uncomment the following statement
            'Dim schedule = New ReadScheduleFile("onefile")

            'to read all weekly XML schedule files
            Dim schedule1 = New ReadScheduleFile("manyfiles", rootFolder)

            Dim _dBLoserPool As New LosersPoolContext

            Try
                Using (_dBLoserPool)

                    Dim queryGameSchedule = (From game1 In _dBLoserPool.ScheduleEntities).ToList

                    Dim maxDayNumber = 1
                    For Each game1 In queryGameSchedule

                        Dim dayNumber = CInt(Mid(game1.DayId, 4))

                        If dayNumber > maxDayNumber Then
                            maxDayNumber = dayNumber
                        End If

                    Next

                    Cache("maxDayNumber") = CStr(maxDayNumber)


                    I2 = CInt(Cache("timeIncrement"))



                End Using
            Catch ex As Exception

            End Try

            ' query the schedule table in LoserPool2 to add minstart times and dates to the ScheduleTimePeriod Database
            'Dim scheduleTimePeriod1 = New CreateSchedulePeriod

            'Need if reading scheduleTimePeriods from File (unlikely)
            ' read the schedultTimePeriod XML file and save to ScheduleTimePeriod  Table of LoserPool2 Database
            'Dim scheduleTimePeriod2 = New ReadScheduleTimePeriodXMLFile(".\TestFiles\scheduleTimePeriod.xml")

            Dim dummy = "dummy"

        End If

    End Sub

    Public Function GetMyOptions() As IQueryable(Of User)

        If Not Page.IsPostBack Then
            Dim EName As String = CStr(Session("userId"))

            Dim rootFolder = CStr(Cache("rootFolder"))
            System.IO.Directory.SetCurrentDirectory(rootFolder)

            Dim maxDayNumber = CInt(Cache("maxDayNumber"))

            Dim mlpo As New MyOptionsList(EName, I2, rootFolder, maxDayNumber)

            Session("dayNumber") = mlpo.dayNumber

            Dim _dBLoserPool As New LosersPoolContext

            Try
                'needs to be set to number of days in the season + I2

                Dim endSeason = "day" + CStr(CInt(Cache("maxDayNumber")) + (I2))

                If mlpo.dayNumber = endSeason Then
                    Response.Redirect("~/LosersPool/Season End.aspx")
                End If


                Dim query1 = From user1 In _dBLoserPool.Users
                           Where user1.UserId = EName
                           Select user1

                For Each user1 In query1

                    Session("optionState") = user1.OptionState

                    If user1.OptionState = "Scoring Update" Then
                        enterUserData.Visible = False
                    ElseIf user1.OptionState = "Enter Picks" Then
                        scoringUpdate.Visible = False
                    ElseIf user1.OptionState = "SeasonEnd" Then
                        Response.Redirect("~/LosersPool/Season End.aspx")
                    ElseIf user1.OptionState = "ScoringUpdateNotReady" Then
                        Response.Redirect("~/LosersPool/UpdateNotReady.aspx")
                    End If

                Next

                _dBLoserPool.SaveChanges()

                Return query1


            Catch ex As Exception

            End Try

        End If

        Return Nothing
    End Function

    Public Function completedDays1_GetData(<QueryString("Id")> dayId As String) As List(Of DayCompleted)


        Dim _dbLosersPool As New LosersPoolContext
        Using (_dbLosersPool)
            Try
                Dim queryCompletedTimePeriods = (From timePeriod1 In _dbLosersPool.ScheduleTimePeriods
                                                Where Not timePeriod1.endDayDate Is Nothing).ToList

                For Each timePeriod1 In queryCompletedTimePeriods
                    If Not DayCompletedCollection.ContainsKey(timePeriod1.dayID) Then
                        Dim dayCompleted1 As New DayCompleted
                        dayCompleted1.DayId = timePeriod1.dayID
                        DayCompletedCollection.Add(timePeriod1.dayID, dayCompleted1)
                    End If
                Next

                For Each day1 In DayCompletedCollection
                    Dim queryTimePeriod = (From timeperiod1 In _dbLosersPool.ScheduleTimePeriods
                                           Where timeperiod1.dayID = day1.Key).ToList
                    DayCompletedCollection(day1.Key).DayDate = queryTimePeriod(0).endDayDate
                    DayCompletedCollection(day1.Key).DayTime = queryTimePeriod(0).endDayTime
                Next

                For Each day1 In DayCompletedCollection
                    Dim dayCompleted1 As New DayCompleted
                    dayCompleted1.DayId = day1.Value.DayId
                    dayCompleted1.DayDate = day1.Value.DayDate
                    dayCompleted1.DayTime = day1.Value.DayTime
                    DayCompletedList.Add(dayCompleted1)
                Next

                If Not (dayId Is Nothing) Then
                    Session("completedDayId") = dayId
                    Response.Redirect("~/LosersPool/CompletedDayResults.aspx")
                End If

            Catch ex As Exception

            End Try
        End Using


        Return DayCompletedList
    End Function

End Class

Public Class MyOptionsList
    Public Property optionsList As MyPool
    Public Property dayNumber As String

    Public Sub New(Ename, I2, rootFolder, maxDayNumber)
        Me.optionsList = MyOptionsList1(Me, Ename, I2, rootFolder, maxDayNumber)
    End Sub


    Public Shared Function MyOptionsList1(mlpo As MyOptionsList, Ename As String, I2 As Int32, rootFolder As String, maxdayNumber As Int32) As MyPool

        Dim myPool1 As New MyPool

        System.IO.Directory.SetCurrentDirectory(rootFolder)

        Dim finalScoringUpdateFile = "scoringUpdateFinalDay"

        Dim _dbLoserPool As New LosersPoolContext

        Try
            Using (_dbLoserPool)

                Dim currentDateTime As DateTime = Date.Now

                Dim myUpdate = XDocument.Load(".\TestFiles\scoringUpdate.xml")

                ' Get update file time and date
                Dim queryTime = (From score In myUpdate.Descendants("scores")
                                 Select New ScoreUpdateXML With {.filetime = score.Attribute("filetime").Value,
                                                                 .filedate = score.Attribute("filedate").Value,
                                                                 .daynumber = score.Attribute("daynumber").Value}).ToList

                ' Make sure user option state is initialized
                If queryTime(0).daynumber = "day0" Then
                    Dim queryUser = (From user1 In _dbLoserPool.Users
                                    Where user1.UserName = Ename).ToList
                    For Each user1 In queryUser
                        user1.OptionState = "Enter Picks"
                    Next
                    _dbLoserPool.SaveChanges()
                End If

                Dim fileTime = DateTime.Parse(CDate(queryTime(0).filedate) + " " + CDate(queryTime(0).filetime))
                Dim filedayNumber = queryTime(0).daynumber

                '  Get games in update file
                Dim queryGame = (From game In myUpdate.Descendants("scores").Descendants("game")
                                 Select New GameUpdateXML With {.hometeam = game.Attribute("hometeam").Value,
                                                               .homescore = CInt(game.Elements("homescore").Value),
                                                               .awayteam = game.Attribute("awayteam").Value,
                                                               .awayscore = CInt(game.Elements("awayscore").Value),
                                                               .gametime = game.Elements("gametime").Value}).ToList

                ' get  games from schedule which are not final
                Dim queryGameSchedule As New List(Of ScheduleEntity)
                queryGameSchedule = (From game1 In _dbLoserPool.ScheduleEntities
                                     Where game1.GameTime <> "final").ToList

                ' find earlist day number where games are not final
                Dim minDayNumber = maxdayNumber
                For Each game1 In queryGameSchedule

                    Dim dayNumber = CInt(Mid(game1.DayId, 4))

                    If dayNumber < minDayNumber Then
                        minDayNumber = dayNumber
                    End If

                Next

                ' Find which day we are in
                Dim thisDay = "day" + CStr(minDayNumber)
                Dim lastDay = "day" + CStr(minDayNumber - I2)

                If lastDay = "day-1" Then
                    lastDay = "day0"
                End If

                Dim queryTimePeriod As New List(Of ScheduleTimePeriod)
                queryTimePeriod = (From timePeriod In _dbLoserPool.ScheduleTimePeriods
                                   Where timePeriod.dayID = lastDay).ToList

                If lastDay <> "day0" Then
                    If queryTimePeriod(0).endDayDate Is Nothing Or queryTimePeriod(0).endDayTime Is Nothing Then
                        thisDay = lastDay
                    End If
                End If

                Dim fDN = CInt(filedayNumber.Substring(3))
                Dim tD = CInt(thisDay.Substring(3))

                Dim scoringUpdateFinalFile = ".\TestFiles\" + finalScoringUpdateFile + CStr(fDN) + ".xml"
                Dim sUFF = XDocument.Load(scoringUpdateFinalFile)

                Dim queryFinalTime = (From score In sUFF.Descendants("scores")
                             Select New ScoreUpdateXML With {.filetime = score.Attribute("filetime").Value,
                                                             .filedate = score.Attribute("filedate").Value,
                                                             .daynumber = score.Attribute("daynumber").Value}).ToList

                Dim fileFinalTime = DateTime.Parse(CDate(queryFinalTime(0).filedate) + " " + CDate(queryFinalTime(0).filetime))
                Dim fileFinalDayNumber = queryFinalTime(0).daynumber

                Dim DayIsFinished = False
                If fileTime = fileFinalTime And filedayNumber = fileFinalDayNumber Then
                    DayIsFinished = True
                End If


                Dim finishedDays = fDN - I2
                If DayIsFinished = True Then
                    finishedDays = fDN
                End If

                ' Databases needs to be updated due to inactivity

                If fDN > tD Then
                    For I = tD To finishedDays Step I2
                        Dim scoringUpdateFile = ".\TestFiles\" + finalScoringUpdateFile + CStr(I) + ".xml"
                        Dim sUF = XDocument.Load(scoringUpdateFile)

                        queryTime = (From score In sUF.Descendants("scores")
                                         Select New ScoreUpdateXML With {.filetime = score.Attribute("filetime").Value,
                                                                         .filedate = score.Attribute("filedate").Value,
                                                                         .daynumber = score.Attribute("daynumber").Value}).ToList

                        queryGame = (From game In sUF.Descendants("scores").Descendants("game")
                                         Select New GameUpdateXML With {.hometeam = game.Attribute("hometeam").Value,
                                                                       .homescore = CInt(game.Elements("homescore").Value),
                                                                       .awayteam = game.Attribute("awayteam").Value,
                                                                       .awayscore = CInt(game.Elements("awayscore").Value),
                                                                       .gametime = game.Elements("gametime").Value}).ToList


                        Dim I1 = I

                        queryTimePeriod = (From dayNum In _dbLoserPool.ScheduleTimePeriods
                                            Where dayNum.dayID = "day" + CStr(I1) Or dayNum.dayID = "day" + CStr(I1 + I2)
                                            Order By dayNum.dayID).ToList

                        queryTimePeriod(0).endDayDate = queryTime(0).filedate
                        queryTimePeriod(0).endDayTime = queryTime(0).filetime

                        If queryTimePeriod.Count > 1 Then
                            queryTimePeriod(1).startDayDate = queryTime(0).filedate
                            queryTimePeriod(1).startDayTime = queryTime(0).filetime
                        End If

                        _dbLoserPool.SaveChanges()

                        mlpo.dayNumber = "day" + CStr(I + I2)

                        queryGameSchedule = (From game1 In _dbLoserPool.ScheduleEntities
                                     Where game1.DayId = "day" + CStr(I1)).ToList

                        For Each game1 In queryGameSchedule

                            Dim queryUpdateGame1 = (From game In sUF.Descendants("scores").Descendants("game")
                                                    Where game.Attribute("hometeam").Value = game1.HomeTeam Or game.Attribute("awayteam").Value = game1.AwayTeam)

                            If queryUpdateGame1.Count = 0 Then
                                Continue For
                            End If

                            Dim queryUpdateGame As New List(Of GameUpdateXML)

                            queryUpdateGame = (From game In sUF.Descendants("scores").Descendants("game")
                                                  Where game.Attribute("hometeam").Value = game1.HomeTeam And game.Attribute("awayteam").Value = game1.AwayTeam
                                                  Select New GameUpdateXML With {.hometeam = game.Attribute("hometeam").Value,
                                                                           .homescore = game.Elements("homescore").Value,
                                                                           .awayteam = game.Attribute("awayteam").Value,
                                                                           .awayscore = game.Elements("awayscore").Value,
                                                                           .gametime = game.Elements("gametime").Value}).ToList

                            game1.HomeScore = queryUpdateGame(0).homescore
                            game1.AwayScore = queryUpdateGame(0).awayscore
                            game1.GameTime = queryUpdateGame(0).gametime

                            _dbLoserPool.SaveChanges()
                        Next

                        Dim myUser1 As New LosersPool.Models.User
                        myUser1 = _dbLoserPool.Users.SingleOrDefault(Function(mU) mU.UserId = Ename)
                        myUser1.OptionState = "Enter Picks"

                        ' Get all contenders
                        Dim queryUserChoices1 = (From user2 In _dbLoserPool.UserChoicesList
                                                Where user2.DayId = "day" + CStr(I1) And user2.Contender = True
                                                Select user2).ToList

                        For Each user1 In queryUserChoices1

                            ' Make sure contender is not  already on the loser list
                            Dim queryLoser = (From loser1 In _dbLoserPool.LoserList
                                              Where loser1.UserName = user1.UserName
                                              Select loser1).ToList

                            If queryLoser.Count = 0 Then

                                If user1.UserPick Is Nothing Or user1.UserPick = "" Then
                                    ' user1 is a loser
                                    user1.Contender = False

                                    Dim loser1 = New Loser
                                    loser1.ListId = _dbLoserPool.LoserList.Count + 1
                                    loser1.UserId = user1.UserID
                                    loser1.UserName = user1.UserName
                                    loser1.DayId = user1.DayId
                                    loser1.DayIdInt = CInt(Mid(user1.DayId, 4))
                                    loser1.LosingPick = "Not Made"
                                    _dbLoserPool.LoserList.Add(loser1)
                                    _dbLoserPool.SaveChanges()
                                    Continue For

                                End If
                            End If

                            ' Finalize scores in schedule and determine if user is a contender or a loser
                            For Each game In queryGame
                                If game.hometeam = user1.UserPick Or game.awayteam = user1.UserPick Then
                                    If game.hometeam = user1.UserPick Then
                                        If game.homescore < game.awayscore Then
                                            'user1 is still a contender
                                            ' set user1 pick team to false
                                            Dim user2 = New UserChoices

                                            user2.ListId = _dbLoserPool.UserChoicesList.Count + 1
                                            user2.UserID = user1.UserID
                                            user2.UserName = user1.UserName
                                            user2.DayId = "day" + CStr(CInt(Mid(thisDay, 4)) + I2)  'dayId
                                            user2.Washington = user1.Washington
                                            user2.Miami = user1.Miami
                                            user2.Colorado = user1.Colorado
                                            user2.Arizona = user1.Arizona
                                            user2.SanFrancisco = user1.SanFrancisco
                                            user2.SanDiego = user1.SanDiego
                                            user2.Pittsburg = user1.Pittsburg
                                            user2.Cincinnati = user1.Cincinnati
                                            user2.Toronto = user1.Toronto
                                            user2.NYYankees = user1.NYYankees
                                            user2.Boston = user1.Boston
                                            user2.TampaBay = user1.TampaBay
                                            user2.Atlanta = user1.Atlanta
                                            user2.Philadelphia = user1.Philadelphia
                                            user2.ChicagoWhiteSox = user1.ChicagoWhiteSox
                                            user2.Detroit = user1.Detroit
                                            user2.KansasCity = user1.KansasCity
                                            user2.Cleveland = user1.Cleveland
                                            user2.Milwaukee = user1.Milwaukee
                                            user2.LADodgers = user1.LADodgers
                                            user2.Minnesota = user1.Minnesota
                                            user2.Oakland = user1.Oakland
                                            user2.Houston = user1.Houston
                                            user2.Texas = user1.Texas
                                            user2.STLouis = user1.STLouis
                                            user2.ChicagoCubs = user1.ChicagoCubs
                                            user2.LAAngels = user1.LAAngels
                                            user2.Seattle = user1.Seattle
                                            user2.NYMets = user1.NYMets
                                            user2.Baltimore = user1.Baltimore
                                            user2.Contender = True
                                            user2.UserPick = user1.UserPick
                                            user2 = SetContendersPickToFalse(user2)
                                            user2.UserPick = ""
                                            _dbLoserPool.UserChoicesList.Add(user2)

                                            _dbLoserPool.SaveChanges()
                                            Exit For
                                        Else
                                            'user1 is a loser
                                            'set user1 contender to false
                                            user1.Contender = False
                                            'add  user1 to loser list
                                            Dim loser1 = New Loser
                                            loser1.ListId = _dbLoserPool.LoserList.Count + 1
                                            loser1.UserId = user1.UserID
                                            loser1.UserName = user1.UserName
                                            loser1.DayId = user1.DayId
                                            loser1.DayIdInt = CInt(Mid(user1.DayId, 4))
                                            loser1.LosingPick = user1.UserPick
                                            _dbLoserPool.LoserList.Add(loser1)
                                            _dbLoserPool.SaveChanges()
                                            Exit For
                                        End If
                                    Else
                                        If game.awayscore < game.homescore Then
                                            'user1 is still a contender
                                            ' set user1 pick team to false

                                            Dim user2 = New UserChoices
                                            user2.ListId = _dbLoserPool.UserChoicesList.Count + 1
                                            user2.UserID = user1.UserID
                                            user2.UserName = user1.UserName
                                            user2.DayId = "day" + CStr(CInt(Mid(thisDay, 4)) + I2)  'dayId
                                            user2.Washington = user1.Washington
                                            user2.Miami = user1.Miami
                                            user2.Colorado = user1.Colorado
                                            user2.Arizona = user1.Arizona
                                            user2.SanFrancisco = user1.SanFrancisco
                                            user2.SanDiego = user1.SanDiego
                                            user2.Pittsburg = user1.Pittsburg
                                            user2.Cincinnati = user1.Cincinnati
                                            user2.Toronto = user1.Toronto
                                            user2.NYYankees = user1.NYYankees
                                            user2.Boston = user1.Boston
                                            user2.TampaBay = user1.TampaBay
                                            user2.Atlanta = user1.Atlanta
                                            user2.Philadelphia = user1.Philadelphia
                                            user2.ChicagoWhiteSox = user1.ChicagoWhiteSox
                                            user2.Detroit = user1.Detroit
                                            user2.KansasCity = user1.KansasCity
                                            user2.Cleveland = user1.Cleveland
                                            user2.Milwaukee = user1.Milwaukee
                                            user2.LADodgers = user1.LADodgers
                                            user2.Minnesota = user1.Minnesota
                                            user2.Oakland = user1.Oakland
                                            user2.Houston = user1.Houston
                                            user2.Texas = user1.Texas
                                            user2.STLouis = user1.STLouis
                                            user2.ChicagoCubs = user1.ChicagoCubs
                                            user2.LAAngels = user1.LAAngels
                                            user2.Seattle = user1.Seattle
                                            user2.NYMets = user1.NYMets
                                            user2.Baltimore = user1.Baltimore
                                            user2.Contender = True
                                            user2.UserPick = user1.UserPick
                                            user2 = SetContendersPickToFalse(user2)
                                            user2.UserPick = ""
                                            _dbLoserPool.UserChoicesList.Add(user2)

                                            _dbLoserPool.SaveChanges()
                                            Exit For
                                        Else
                                            'user1 is a loser
                                            'set user1 contender to false
                                            user1.Contender = False
                                            'add  user1 to loser list
                                            Dim loser1 = New Loser
                                            loser1.ListId = _dbLoserPool.LoserList.Count + 1
                                            loser1.UserId = user1.UserID
                                            loser1.UserName = user1.UserName
                                            loser1.DayId = user1.DayId
                                            loser1.DayIdInt = CInt(Mid(user1.DayId, 4))
                                            loser1.LosingPick = user1.UserPick
                                            _dbLoserPool.LoserList.Add(loser1)
                                            _dbLoserPool.SaveChanges()
                                            Exit For
                                        End If

                                    End If
                                End If
                            Next
                        Next

                        thisDay = "day" + CStr(I + I2)
                        _dbLoserPool.SaveChanges()

                    Next
                End If

                ' Update scores for  current games on the schedule
                queryGameSchedule = New List(Of ScheduleEntity)
                queryGameSchedule = (From game1 In _dbLoserPool.ScheduleEntities
                                     Where game1.DayId = thisDay).ToList

                'Need to make sure to include new version of If statement in football, queryTime(0).daynumber = thisDay
                If queryTime(0).daynumber <> "day0" And queryTime(0).daynumber = thisDay Then
                    For Each game1 In queryGameSchedule

                        Dim queryUpdateGame1 = (From game In myUpdate.Descendants("scores").Descendants("game")
                                                Where game.Attribute("hometeam").Value = game1.HomeTeam Or game.Attribute("awayteam").Value = game1.AwayTeam)

                        If queryUpdateGame1.Count = 0 Then
                            Continue For
                        End If

                        Dim queryUpdateGame As New List(Of GameUpdateXML)

                        queryUpdateGame = (From game In myUpdate.Descendants("scores").Descendants("game")
                                              Where game.Attribute("hometeam").Value = game1.HomeTeam And game.Attribute("awayteam").Value = game1.AwayTeam
                                              Select New GameUpdateXML With {.hometeam = game.Attribute("hometeam").Value,
                                                                       .homescore = game.Elements("homescore").Value,
                                                                       .awayteam = game.Attribute("awayteam").Value,
                                                                       .awayscore = game.Elements("awayscore").Value,
                                                                       .gametime = game.Elements("gametime").Value}).ToList

                        game1.HomeScore = queryUpdateGame(0).homescore
                        game1.AwayScore = queryUpdateGame(0).awayscore
                        game1.GameTime = queryUpdateGame(0).gametime


                        _dbLoserPool.SaveChanges()

                    Next

                End If

                'Determine if games are finished from updated Schedule
                DayIsFinished = True
                For Each game In queryGameSchedule
                    If Not (game.GameTime = "final") Then
                        DayIsFinished = False
                        Exit For
                    End If
                Next

                ' Update all necessary lists and route user by updating the options state
                ' The user can being in the "Enter Picks" state, the "Scoring Update" state or the "SeasonEnd" state
                'The lists updated are The ScheduleTimePeriods list,
                queryTimePeriod = (From timeperiod1 In _dbLoserPool.ScheduleTimePeriods
                                  Where timeperiod1.dayID = thisDay).ToList

                If DayIsFinished = True Then

                    For Each timePeriod In queryTimePeriod

                        ' Convert dates and times to datetimes
                        Dim startDayTime As DateTime?
                        startDayTime = Nothing
                        If Not (timePeriod.startDayDate Is Nothing) And Not (timePeriod.startDayTime Is Nothing) Then
                            startDayTime = DateTime.Parse(timePeriod.startDayDate + " " + timePeriod.startDayTime)
                        End If

                        Dim startGameTime As DateTime?
                        startGameTime = Nothing
                        If Not (timePeriod.startGameDate Is Nothing) And Not (timePeriod.startGameTime Is Nothing) Then
                            startGameTime = DateTime.Parse(timePeriod.startGameDate + " " + timePeriod.startGameTime)
                        End If

                        Dim endDayTime As DateTime?
                        endDayTime = Nothing
                        If Not (timePeriod.endDayDate Is Nothing) And Not (timePeriod.endDayTime Is Nothing) Then
                            endDayTime = DateTime.Parse(timePeriod.endDayDate + " " + timePeriod.endDayTime)
                        End If

                        ' Find the time period which the user is in
                        If (startGameTime < currentDateTime) And (endDayTime Is Nothing) And fileTime >= startGameTime Then

                            'all games in scoring update file are final but Schedule Update Database needs to be updated
                            'user can enter picks for new week
                            ' my loser pool options (mlpo) list week is updated
                            ' users table is updated
                            ' losers table is updated
                            ' user choices table is updated

                            timePeriod.endDayDate = queryTime(0).filedate
                            timePeriod.endDayTime = queryTime(0).filetime

                            mlpo.dayNumber = "day" + CStr(CInt(Mid(thisDay, 4)) + I2)

                            'All games are final Schedule Time Periods table will be updated
                            Dim queryTimePeriod1 As New ScheduleTimePeriod

                            queryTimePeriod1 = _dbLoserPool.ScheduleTimePeriods.SingleOrDefault(Function(qTP) qTP.dayID = mlpo.dayNumber)
                            If queryTimePeriod1 Is Nothing Then
                            Else
                                queryTimePeriod1.startDayDate = queryTime(0).filedate
                                queryTimePeriod1.startDayTime = queryTime(0).filetime
                            End If

                            Dim myUser1 As New LosersPool.Models.User
                            myUser1 = _dbLoserPool.Users.SingleOrDefault(Function(mU) mU.UserId = Ename)
                            myUser1.OptionState = "Enter Picks"

                            ' Get all contenders
                            Dim queryUserChoices1 = (From user2 In _dbLoserPool.UserChoicesList
                                                    Where user2.DayId = timePeriod.dayID And user2.Contender = True
                                                    Select user2).ToList

                            For Each user1 In queryUserChoices1

                                ' Make sure contender is not  already on the loser list
                                Dim queryLoser = (From loser1 In _dbLoserPool.LoserList
                                                  Where loser1.UserName = user1.UserName
                                                  Select loser1).ToList

                                If queryLoser.Count = 0 Then

                                    If user1.UserPick Is Nothing Or user1.UserPick = "" Then
                                        ' user1 is a loser
                                        user1.Contender = False

                                        Dim loser1 = New Loser
                                        loser1.ListId = _dbLoserPool.LoserList.Count + 1
                                        loser1.UserId = user1.UserID
                                        loser1.UserName = user1.UserName
                                        loser1.DayId = user1.DayId
                                        loser1.DayIdInt = CInt(Mid(user1.DayId, 4))
                                        loser1.LosingPick = user1.UserPick
                                        _dbLoserPool.LoserList.Add(loser1)
                                        _dbLoserPool.SaveChanges()
                                        Continue For

                                    End If
                                End If

                                ' Finalize scores in schedule and determine if user is a contender or a loser
                                For Each game In queryGame
                                    If game.hometeam = user1.UserPick Or game.awayteam = user1.UserPick Then
                                        If game.hometeam = user1.UserPick Then
                                            If game.homescore < game.awayscore Then
                                                'user1 is still a contender
                                                ' set user1 pick team to false
                                                Dim user2 = New UserChoices

                                                user2.ListId = _dbLoserPool.UserChoicesList.Count + 1
                                                user2.UserID = user1.UserID
                                                user2.UserName = user1.UserName
                                                user2.DayId = "day" + CStr(CInt(Mid(thisDay, 4)) + I2)  'weekId
                                                user2.Washington = user1.Washington
                                                user2.Miami = user1.Miami
                                                user2.Colorado = user1.Colorado
                                                user2.Arizona = user1.Arizona
                                                user2.SanFrancisco = user1.SanFrancisco
                                                user2.SanDiego = user1.SanDiego
                                                user2.Pittsburg = user1.Pittsburg
                                                user2.Cincinnati = user1.Cincinnati
                                                user2.Toronto = user1.Toronto
                                                user2.NYYankees = user1.NYYankees
                                                user2.Boston = user1.Boston
                                                user2.TampaBay = user1.TampaBay
                                                user2.Atlanta = user1.Atlanta
                                                user2.Philadelphia = user1.Philadelphia
                                                user2.ChicagoWhiteSox = user1.ChicagoWhiteSox
                                                user2.Detroit = user1.Detroit
                                                user2.KansasCity = user1.KansasCity
                                                user2.Cleveland = user1.Cleveland
                                                user2.Milwaukee = user1.Milwaukee
                                                user2.LADodgers = user1.LADodgers
                                                user2.Minnesota = user1.Minnesota
                                                user2.Oakland = user1.Oakland
                                                user2.Houston = user1.Houston
                                                user2.Texas = user1.Texas
                                                user2.STLouis = user1.STLouis
                                                user2.ChicagoCubs = user1.ChicagoCubs
                                                user2.LAAngels = user1.LAAngels
                                                user2.Seattle = user1.Seattle
                                                user2.NYMets = user1.NYMets
                                                user2.Baltimore = user1.Baltimore

                                                user2.Contender = True
                                                user2.UserPick = user1.UserPick
                                                user2 = SetContendersPickToFalse(user2)
                                                user2.UserPick = ""
                                                _dbLoserPool.UserChoicesList.Add(user2)

                                                _dbLoserPool.SaveChanges()
                                                Exit For
                                            Else
                                                'user1 is a loser
                                                'set user1 contender to false
                                                user1.Contender = False
                                                'add  user1 to loser list
                                                Dim loser1 = New Loser
                                                loser1.ListId = _dbLoserPool.LoserList.Count + 1
                                                loser1.UserId = user1.UserID
                                                loser1.UserName = user1.UserName
                                                loser1.DayId = user1.DayId
                                                loser1.DayIdInt = CInt(Mid(user1.DayId, 4))
                                                loser1.LosingPick = user1.UserPick
                                                _dbLoserPool.LoserList.Add(loser1)
                                                _dbLoserPool.SaveChanges()
                                                Exit For
                                            End If
                                        Else
                                            If game.awayscore < game.homescore Then
                                                'user1 is still a contender
                                                ' set user1 pick team to false

                                                Dim user2 = New UserChoices
                                                user2.ListId = _dbLoserPool.UserChoicesList.Count + 1
                                                user2.UserID = user1.UserID
                                                user2.UserName = user1.UserName
                                                user2.DayId = "day" + CStr(CInt(Mid(thisDay, 4)) + I2)  'dayId
                                                user2.Washington = user1.Washington
                                                user2.Miami = user1.Miami
                                                user2.Colorado = user1.Colorado
                                                user2.Arizona = user1.Arizona
                                                user2.SanFrancisco = user1.SanFrancisco
                                                user2.SanDiego = user1.SanDiego
                                                user2.Pittsburg = user1.Pittsburg
                                                user2.Cincinnati = user1.Cincinnati
                                                user2.Toronto = user1.Toronto
                                                user2.NYYankees = user1.NYYankees
                                                user2.Boston = user1.Boston
                                                user2.TampaBay = user1.TampaBay
                                                user2.Atlanta = user1.Atlanta
                                                user2.Philadelphia = user1.Philadelphia
                                                user2.ChicagoWhiteSox = user1.ChicagoWhiteSox
                                                user2.Detroit = user1.Detroit
                                                user2.KansasCity = user1.KansasCity
                                                user2.Cleveland = user1.Cleveland
                                                user2.Milwaukee = user1.Milwaukee
                                                user2.LADodgers = user1.LADodgers
                                                user2.Minnesota = user1.Minnesota
                                                user2.Oakland = user1.Oakland
                                                user2.Houston = user1.Houston
                                                user2.Texas = user1.Texas
                                                user2.STLouis = user1.STLouis
                                                user2.ChicagoCubs = user1.ChicagoCubs
                                                user2.LAAngels = user1.LAAngels
                                                user2.Seattle = user1.Seattle
                                                user2.NYMets = user1.NYMets
                                                user2.Baltimore = user1.Baltimore

                                                user2.Contender = True
                                                user2.UserPick = user1.UserPick
                                                user2 = SetContendersPickToFalse(user2)
                                                user2.UserPick = ""
                                                _dbLoserPool.UserChoicesList.Add(user2)

                                                _dbLoserPool.SaveChanges()
                                                Exit For
                                            Else
                                                'user1 is a loser
                                                'set user1 contender to false
                                                user1.Contender = False
                                                'add  user1 to loser list
                                                Dim loser1 = New Loser
                                                loser1.ListId = _dbLoserPool.LoserList.Count + 1
                                                loser1.UserId = user1.UserID
                                                loser1.UserName = user1.UserName
                                                loser1.DayId = user1.DayId
                                                loser1.DayIdInt = CInt(Mid(user1.DayId, 4))
                                                loser1.LosingPick = user1.UserPick
                                                _dbLoserPool.LoserList.Add(loser1)
                                                _dbLoserPool.SaveChanges()
                                                Exit For
                                            End If

                                        End If
                                    End If
                                Next
                            Next

                            _dbLoserPool.SaveChanges()

                            ' myPool1 is updated
                            myPool1.EName = Ename
                            myPool1.LoserPoolOptions.Add("Enter Picks")
                            Return myPool1

                        ElseIf startGameTime < currentDateTime And endDayTime > currentDateTime And Not (endDayTime Is Nothing) Then

                            ' Scoring Update File is current but Schedule Period database was already updated - user is looked out of user entry but can see scoring updates
                            ' users table is updated
                            ' myPool1 is updated
                            Dim myUser1 As New LosersPool.Models.User
                            myUser1 = _dbLoserPool.Users.SingleOrDefault(Function(mU) mU.UserId = Ename)
                            myUser1.OptionState = "Scoring Update"
                            _dbLoserPool.SaveChanges()

                            mlpo.dayNumber = timePeriod.dayID
                            myPool1.EName = Ename
                            myPool1.LoserPoolOptions.Add("Scoring Update")
                            Return myPool1

                        ElseIf startDayTime < currentDateTime And startGameTime > currentDateTime And startDayTime >= fileTime Then

                            'Scoring Updates file is old file  and will not be used to update Schedule Period Database but user is in data entry time period
                            ' users table is updated
                            ' myPool1 is updated
                            Dim myUser1 As New LosersPool.Models.User
                            myUser1 = _dbLoserPool.Users.SingleOrDefault(Function(mU) mU.UserId = Ename)
                            myUser1.OptionState = "Enter Picks"
                            _dbLoserPool.SaveChanges()

                            mlpo.dayNumber = timePeriod.dayID
                            myPool1.EName = Ename
                            myPool1.LoserPoolOptions.Add("Enter Picks")
                            Return myPool1
                        End If
                    Next

                    ' Not all the games on the schedule are finished
                ElseIf DayIsFinished = False Then

                    For Each timePeriod In queryTimePeriod

                        Dim startDayTime As DateTime?
                        startDayTime = Nothing
                        If Not (timePeriod.startDayDate Is Nothing) And Not (timePeriod.startDayTime Is Nothing) Then
                            startDayTime = DateTime.Parse(timePeriod.startDayDate + " " + timePeriod.startDayTime)
                        End If

                        Dim startGameTime As DateTime?
                        startGameTime = Nothing
                        If Not (timePeriod.startGameDate Is Nothing) And Not (timePeriod.startGameTime Is Nothing) Then
                            startGameTime = DateTime.Parse(timePeriod.startGameDate + " " + timePeriod.startGameTime)
                        End If

                        Dim endDayTime As DateTime?
                        endDayTime = Nothing
                        If Not (timePeriod.endDayDate Is Nothing) And Not (timePeriod.endDayTime Is Nothing) Then
                            endDayTime = DateTime.Parse(timePeriod.endDayDate + " " + timePeriod.endDayTime)
                        End If

                        Dim queryUserChoices1 = (From user2 In _dbLoserPool.UserChoicesList
                                                Where user2.DayId = timePeriod.dayID And user2.Contender = True
                                                Select user2).ToList

                        For Each user1 In queryUserChoices1
                            If startDayTime Is Nothing Then
                            Else
                                If endDayTime Is Nothing And currentDateTime > startGameTime And (user1.UserPick Is Nothing Or user1.UserPick = "") Then
                                    'user1 is a loser because user did not enter data
                                    'set user1 contender to false
                                    user1.Contender = False
                                    'add  user1 to loser list

                                    Dim queryLoser = (From loser2 In _dbLoserPool.LoserList
                                                      Where loser2.UserName = user1.UserName
                                                      Select loser2).ToList

                                    If queryLoser.Count = 0 Then
                                        Dim loser1 = New Loser
                                        loser1.ListId = _dbLoserPool.LoserList.Count + 1
                                        loser1.UserId = user1.UserID
                                        loser1.UserName = user1.UserName
                                        loser1.DayId = user1.DayId
                                        loser1.DayIdInt = CInt(Mid(user1.DayId, 4))
                                        loser1.LosingPick = "Not Made"
                                        _dbLoserPool.LoserList.Add(loser1)
                                        _dbLoserPool.SaveChanges()
                                        Continue For
                                    End If
                                End If
                            End If

                        Next

                        ' Route contenders by forming myoptions list
                        If startDayTime Is Nothing Then

                        Else
                            If endDayTime Is Nothing And startGameTime <= fileTime Then

                                'Scoring update file is current but Games are not finished user can see scoring updates
                                ' users table is updated
                                ' myPool1 is updated
                                Dim myUser1 As New LosersPool.Models.User
                                myUser1 = _dbLoserPool.Users.SingleOrDefault(Function(mU) mU.UserId = Ename)
                                myUser1.OptionState = "Scoring Update"
                                _dbLoserPool.SaveChanges()

                                mlpo.dayNumber = timePeriod.dayID
                                myPool1.EName = Ename
                                myPool1.LoserPoolOptions.Add("Scoring Update")
                                Return myPool1

                            ElseIf endDayTime Is Nothing And (startDayTime < currentDateTime And startGameTime > currentDateTime) Then

                                'Games for week haven't started
                                ' users table is updated
                                ' myPool1 is updated
                                Dim myUser1 As New LosersPool.Models.User
                                myUser1 = _dbLoserPool.Users.SingleOrDefault(Function(mU) mU.UserId = Ename)
                                myUser1.OptionState = "Enter Picks"
                                _dbLoserPool.SaveChanges()

                                mlpo.dayNumber = timePeriod.dayID
                                myPool1.EName = Ename
                                myPool1.LoserPoolOptions.Add("Enter Picks")
                                Return myPool1

                            ElseIf endDayTime Is Nothing And startGameTime < currentDateTime And fileTime < startGameTime Then

                                'Games have started but Scoring Update file is not current
                                'users table is updated
                                'mypool1 is updated
                                Dim myUser1 As New LosersPool.Models.User
                                myUser1 = _dbLoserPool.Users.SingleOrDefault(Function(mU) mU.UserId = Ename)
                                myUser1.OptionState = "ScoringUpdateNotReady"
                                _dbLoserPool.SaveChanges()

                                mlpo.dayNumber = timePeriod.dayID
                                myPool1.EName = Ename
                                myPool1.LoserPoolOptions.Add("Scoring Update")
                                Return myPool1


                            End If
                        End If
                    Next
                End If
            End Using
        Catch ex As Exception

        End Try

        'Post Season Return
        Return myPool1

    End Function

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
        ElseIf user1.UserPick = "Kansas City" Then
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
        ElseIf user1.UserPick = "Chicago Cubs" Then
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

End Class