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


Imports RussBucksPools.LosersPool.Models
Imports RussBucksPools.JoinPools.Models


Public Class WeeklyLoserPoolUserEntry
    Inherits System.Web.UI.Page

    Private GameScheduleCollection As New Dictionary(Of String, TimePeriodSchedule)
    Private RadioButtonCollection As New Dictionary(Of String, RadioButton)
    Private ImageButtonCollection As New Dictionary(Of String, ImageButton)

    Private SelectedImageButton As String

    Private TeamBackColor As System.Drawing.Color = Drawing.Color.White

    Private TeamList As New Dictionary(Of String, Team)
    Private ByeList As New Dictionary(Of String, String)

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If


        Dim _dbLoserPool As New LosersPoolContext
        Dim _dbPools As New PoolDbContext
        Dim _dbApp As New ApplicationDbContext
        Try
            Using (_dbLoserPool)
                Using (_dbPools)
                    Using (_dbApp)

                        Dim EName = CStr(Session("userId"))
                        Dim poolAlias = CStr(Session("poolAlias"))
                        Dim staleTimePeriod = CStr(Session("TimePeriod"))

                        Dim cronJob = (From cJN1 In _dbApp.CronJobPools
                                        Where cJN1.CronJobPoolAlias = poolAlias).Single

                        Dim cronJobName = cronJob.CronJobName

                        Dim queryPoolParam = (From qPP1 In _dbPools.PoolParameters
                                              Where qPP1.CronJob = cronJobName And qPP1.poolAlias = poolAlias).Single

                        Dim thisTimePeriod = queryPoolParam.TimePeriod
                        Dim sport = queryPoolParam.Sport
                        Dim poolState = queryPoolParam.poolState

                        If thisTimePeriod = staleTimePeriod And poolState = "Scoring Update" Then
                            Response.Redirect("~/LosersPool/WeeklyScoringUpdate.aspx")
                        ElseIf thisTimePeriod <> staleTimePeriod And poolState <> "Enter Picks" Then
                            Response.Redirect("~/LosersPool/LoserPoolHome.aspx")
                        End If

                        Dim currentDateTime = Date.Now

                        Dim queryScheduleTimePeriods = (From period1 In _dbLoserPool.ScheduleTimePeriods
                                Where period1.CronJob = cronJobName And period1.TimePeriod = thisTimePeriod).Single

                        Dim gameStartDateTime = DateTime.Parse(CDate(queryScheduleTimePeriods.startGameDate + " " + queryScheduleTimePeriods.startGameTime))

                        If currentDateTime > gameStartDateTime And currentDateTime < gameStartDateTime.AddSeconds(60) Then
                            Response.Redirect("~/LosersPool/UpdateNotReady.aspx")
                        ElseIf currentDateTime > gameStartDateTime And currentDateTime > gameStartDateTime.AddSeconds(60) Then
                            Response.Redirect("~/JoinPool/MyPools.aspx")
                        End If

                        Dim teams1 = (From teams2 In _dbPools.Teams
                                      Where teams2.Sport = sport And teams2.TeamName <> "dummy").ToList


                        Dim byeTeams1 = (From byeTeams2 In _dbLoserPool.ByeTeamsList
                                         Where byeTeams2.CronJob = cronJobName And byeTeams2.TimePeriod = thisTimePeriod And byeTeams2.TeamName <> "dummy").ToList

                        For Each byeTeam2 In byeTeams1
                            ByeList.Add(byeTeam2.TeamName, byeTeam2.TeamName)
                        Next


                        Dim timePeriodGameSchedule = (From game1 In _dbLoserPool.ScheduleEntities
                                               Where game1.TimePeriod = thisTimePeriod And game1.CronJob = cronJobName).ToList

                        Dim mpl = MyPickList.UserPickList(EName, thisTimePeriod, poolAlias, teams1, ByeList, TeamList)

                        GetMyPicks(mpl, thisTimePeriod, teams1)

                        Dim queryMyPicks1 = (From pick1 In _dbLoserPool.MyPicks).ToList

                        GetPickTableHeader(timePeriodGameSchedule, GameScheduleCollection, teams1)

                        GetTables(GameScheduleCollection)

                        GetRadioButtons(RadioButtonCollection, queryMyPicks1, thisTimePeriod, cronJobName, ImageButtonCollection)

                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try




    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub


    Private Sub GetMyPicks(mpl As UserChoices, thisTimePeriod As String, teams1 As List(Of Team))

        Dim _dbLoserPool4 As New LosersPoolContext

        Try
            Using (_dbLoserPool4)
                If mpl Is Nothing Then

                    loser1.Visible = True
                    GameTable1.Visible = False
                    GameTable2.Visible = False
                    GameTable3.Visible = False
                    GameTable4.Visible = False
                    Button1.Visible = False
                    Button2.Visible = True
                    'Button3.Visible = False
                    'Button4.Visible = True
                    Exit Sub
                Else
                    Button1.Visible = True
                    Button2.Visible = False
                    'Button3.Visible = True
                    'Button4.Visible = False
                    loser1.Visible = False

                    For Each team1 In teams1

                        Dim TeamAvailable = "team1"
                        If team1.TeamId = "team1" Then
                            TeamAvailable = mpl.Team1Available
                        ElseIf team1.TeamId = "team2" Then
                            TeamAvailable = mpl.Team2Available
                        ElseIf team1.TeamId = "team3" Then
                            TeamAvailable = mpl.Team3Available
                        ElseIf team1.TeamId = "team4" Then
                            TeamAvailable = mpl.Team4Available
                        ElseIf team1.TeamId = "team5" Then
                            TeamAvailable = mpl.Team5Available
                        ElseIf team1.TeamId = "team6" Then
                            TeamAvailable = mpl.Team6Available
                        ElseIf team1.TeamId = "team7" Then
                            TeamAvailable = mpl.Team7Available
                        ElseIf team1.TeamId = "team8" Then
                            TeamAvailable = mpl.Team8Available
                        ElseIf team1.TeamId = "team9" Then
                            TeamAvailable = mpl.Team9Available
                        ElseIf team1.TeamId = "team10" Then
                            TeamAvailable = mpl.Team10Available
                        ElseIf team1.TeamId = "team11" Then
                            TeamAvailable = mpl.Team11Available
                        ElseIf team1.TeamId = "team12" Then
                            TeamAvailable = mpl.Team12Available
                        ElseIf team1.TeamId = "team13" Then
                            TeamAvailable = mpl.Team13Available
                        ElseIf team1.TeamId = "team14" Then
                            TeamAvailable = mpl.Team14Available
                        ElseIf team1.TeamId = "team15" Then
                            TeamAvailable = mpl.Team15Available
                        ElseIf team1.TeamId = "team16" Then
                            TeamAvailable = mpl.Team16Available
                        ElseIf team1.TeamId = "team17" Then
                            TeamAvailable = mpl.Team17Available
                        ElseIf team1.TeamId = "team18" Then
                            TeamAvailable = mpl.Team18Available
                        ElseIf team1.TeamId = "team19" Then
                            TeamAvailable = mpl.Team19Available
                        ElseIf team1.TeamId = "team20" Then
                            TeamAvailable = mpl.Team20Available
                        ElseIf team1.TeamId = "team21" Then
                            TeamAvailable = mpl.Team21Available
                        ElseIf team1.TeamId = "team22" Then
                            TeamAvailable = mpl.Team22Available
                        ElseIf team1.TeamId = "team23" Then
                            TeamAvailable = mpl.Team23Available
                        ElseIf team1.TeamId = "team24" Then
                            TeamAvailable = mpl.Team24Available
                        ElseIf team1.TeamId = "team25" Then
                            TeamAvailable = mpl.Team25Available
                        ElseIf team1.TeamId = "team26" Then
                            TeamAvailable = mpl.Team26Available
                        ElseIf team1.TeamId = "team27" Then
                            TeamAvailable = mpl.Team27Available
                        ElseIf team1.TeamId = "team28" Then
                            TeamAvailable = mpl.Team28Available
                        ElseIf team1.TeamId = "team29" Then
                            TeamAvailable = mpl.Team29Available
                        ElseIf team1.TeamId = "team30" Then
                            TeamAvailable = mpl.Team30Available
                        ElseIf team1.TeamId = "team31" Then
                            TeamAvailable = mpl.Team31Available
                        ElseIf team1.TeamId = "team32" Then
                            TeamAvailable = mpl.Team32Available
                        End If

                        If team1.TeamName <> "dummy" Then
                            If TeamAvailable = True Or mpl.PossibleUserPicks.ContainsKey(team1.TeamName) Then
                                Dim MyPick1 As New MyPick
                                MyPick1.ListId = _dbLoserPool4.MyPicks.Count + 1
                                MyPick1.PossibleTeam = team1.TeamName
                                _dbLoserPool4.MyPicks.Add(MyPick1)
                            End If
                        End If
                    Next

                    _dbLoserPool4.SaveChanges()

                End If
            End Using
        Catch ex As Exception

        End Try


    End Sub

    Private Sub GetPickTableHeader(TimePGameSchedule As List(Of ScheduleEntity), GameScheduleCollection As Dictionary(Of String, TimePeriodSchedule), teams1 As List(Of Team))
        For Each game1 In TimePGameSchedule

            Dim timePSchedule1 = New TimePeriodSchedule
            timePSchedule1.GameId = game1.GameId
            timePSchedule1.TimePeriod = game1.TimePeriod
            timePSchedule1.StartDate = game1.StartDate
            timePSchedule1.StartTime = game1.StartTime
            timePSchedule1.HomeTeam = game1.HomeTeam
            timePSchedule1.AwayTeam = game1.AwayTeam
            timePSchedule1.GameCode = game1.GameCode

            For Each team1 In teams1
                If game1.HomeTeam = team1.TeamName Then
                    timePSchedule1.HomeTeamImage = team1.TeamLabel
                    timePSchedule1.HomeTeamIcon = team1.TeamIcon
                ElseIf game1.AwayTeam = team1.TeamName Then
                    timePSchedule1.AwayTeamImage = team1.TeamLabel
                    timePSchedule1.AwayTeamIcon = team1.TeamIcon
                End If
            Next

            GameScheduleCollection.Add(timePSchedule1.GameId, timePSchedule1)
        Next

    End Sub

    Private Sub GetTables(GameScheduleCollection As Dictionary(Of String, TimePeriodSchedule))
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

    End Sub

    Private Sub GetRadioButtons(RadioButtonCollection As Dictionary(Of String, RadioButton), queryMyPicks1 As List(Of MyPick), thisTimePeriod As String, cronJobName As String, ImageButtonCollection As Dictionary(Of String, ImageButton))

        Dim _dbLoserPool5 As New LosersPoolContext

        Try
            Using (_dbLoserPool5)
                For Each pick1 In queryMyPicks1

                    Dim queryPickedGame = (From game1 In _dbLoserPool5.ScheduleEntities
                                             Where game1.TimePeriod = thisTimePeriod And (game1.HomeTeam = pick1.PossibleTeam Or game1.AwayTeam = pick1.PossibleTeam) And game1.CronJob = cronJobName
                                             Order By game1.GameId).ToList

                    If queryPickedGame.Count > 0 Then

                        If queryPickedGame(0).GameId = "game1" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home1.Visible = True
                                RadioButtonCollection.Add(Home1.ID, Game1HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away1.Visible = True
                                RadioButtonCollection.Add(Away1.ID, Game1AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game2" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home2.Visible = True
                                RadioButtonCollection.Add(Home2.ID, Game2HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away2.Visible = True
                                RadioButtonCollection.Add(Away2.ID, Game2AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game3" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home3.Visible = True
                                RadioButtonCollection.Add(Home3.ID, Game3HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away3.Visible = True
                                RadioButtonCollection.Add(Away3.ID, Game3AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game4" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home4.Visible = True
                                RadioButtonCollection.Add(Home4.ID, Game4HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away4.Visible = True
                                RadioButtonCollection.Add(Away4.ID, Game4AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game5" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home5.Visible = True
                                RadioButtonCollection.Add(Home5.ID, Game5HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away5.Visible = True
                                RadioButtonCollection.Add(Away5.ID, Game5AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game6" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home6.Visible = True
                                RadioButtonCollection.Add(Home6.ID, Game6HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away6.Visible = True
                                RadioButtonCollection.Add(Away6.ID, Game6AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game7" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home7.Visible = True
                                RadioButtonCollection.Add(Home7.ID, Game7HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away7.Visible = True
                                RadioButtonCollection.Add(Away7.ID, Game7AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game8" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home8.Visible = True
                                RadioButtonCollection.Add(Home8.ID, Game8HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away8.Visible = True
                                RadioButtonCollection.Add(Away8.ID, Game8AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game9" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home9.Visible = True
                                RadioButtonCollection.Add(Home9.ID, Game9HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away9.Visible = True
                                RadioButtonCollection.Add(Away9.ID, Game9AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game10" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home10.Visible = True
                                RadioButtonCollection.Add(Home10.ID, Game10HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away10.Visible = True
                                RadioButtonCollection.Add(Away10.ID, Game10AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game11" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home11.Visible = True
                                RadioButtonCollection.Add(Home11.ID, Game11HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away11.Visible = True
                                RadioButtonCollection.Add(Away11.ID, Game11AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game12" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home12.Visible = True
                                RadioButtonCollection.Add(Home12.ID, Game12HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away12.Visible = True
                                RadioButtonCollection.Add(Away12.ID, Game12AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game13" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home13.Visible = True
                                RadioButtonCollection.Add(Home13.ID, Game13HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away13.Visible = True
                                RadioButtonCollection.Add(Away13.ID, Game13AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game14" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home14.Visible = True
                                RadioButtonCollection.Add(Home14.ID, Game14HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away14.Visible = True
                                RadioButtonCollection.Add(Away14.ID, Game14AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game15" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home15.Visible = True
                                RadioButtonCollection.Add(Home15.ID, Game15HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away15.Visible = True
                                RadioButtonCollection.Add(Away15.ID, Game15AwayRadio)
                            End If
                        End If

                        If queryPickedGame(0).GameId = "game16" Then
                            If queryPickedGame(0).HomeTeam = pick1.PossibleTeam Then
                                Home16.Visible = True
                                RadioButtonCollection.Add(Home16.ID, Game16HomeRadio)
                            ElseIf queryPickedGame(0).AwayTeam = pick1.PossibleTeam Then
                                Away16.Visible = True
                                RadioButtonCollection.Add(Away16.ID, Game16AwayRadio)
                            End If
                        End If

                    End If

                Next

            End Using
        Catch ex As Exception

        End Try


    End Sub

    Private Sub CreateTable1(qGSC As List(Of KeyValuePair(Of String, TimePeriodSchedule)))

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
        End If

        If qGSC.Count <= 2 Then

            GameNumber3.Visible = False
            GameNumber3Status.Visible = False
            HomeTeam3Image.Visible = False
            HomeTeam3Icon.Visible = False
            AwayTeam3Image.Visible = False
            AwayTeam3Icon.Visible = False
            HomePick3.Visible = False
            AwayPick3.Visible = False

        End If

        If qGSC.Count <= 3 Then
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

    Private Sub CreateTable2(qGSC As List(Of KeyValuePair(Of String, TimePeriodSchedule)))

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
        End If

        If qGSC.Count <= 2 Then

            GameNumber7.Visible = False
            GameNumber7Status.Visible = False
            HomeTeam7Image.Visible = False
            HomeTeam7Icon.Visible = False
            AwayTeam7Image.Visible = False
            AwayTeam7Icon.Visible = False
            HomePick7.Visible = False
            AwayPick7.Visible = False
        End If

        If qGSC.Count <= 3 Then

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

    Private Sub CreateTable3(qGSC As List(Of KeyValuePair(Of String, TimePeriodSchedule)))

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


        If qGSC.Count <= 1 Then

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

    Private Sub CreateTable4(qGSC As List(Of KeyValuePair(Of String, TimePeriodSchedule)))

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

            ElseIf game.Key = "game16" And qGSC.Count = 4 Then
                GameNumber16.Text = game.Key
                HomeTeam16Image1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamImage
                AwayTeam16Image1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamImage
                HomeTeam16Icon1.ImageUrl = GameScheduleCollection(game.Key).HomeTeamIcon
                AwayTeam16Icon1.ImageUrl = GameScheduleCollection(game.Key).AwayTeamIcon

                GameNumber16Status.Text = GameScheduleCollection(game.Key).StartDate + " " + GameScheduleCollection(game.Key).StartTime
                HomeTeam16Image1.BackColor = TeamBackColor
                AwayTeam16Image1.BackColor = TeamBackColor

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

        If qGSC.Count <= 3 Then

            GameNumber16.Visible = False
            GameNumber16Status.Visible = False
            HomeTeam16Image.Visible = False
            HomeTeam16Icon.Visible = False
            AwayTeam16Image.Visible = False
            AwayTeam16Icon.Visible = False
            HomePick16.Visible = False
            AwayPick16.Visible = False

        End If

        If GameScheduleCollection.Count >= 13 Then
            GameTable4.DataBind()
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim _dbPools6 As New PoolDbContext
        Dim _dbApp6 As New ApplicationDbContext

        Try
            Using (_dbPools6)
                Using (_dbApp6)

                    Dim poolAlias = CStr(Session("poolAlias"))
                    Dim EName = CStr(Session("userId"))

                    Dim cronJob = (From cJN1 In _dbApp6.CronJobPools
                                Where cJN1.CronJobPoolAlias = poolAlias).Single

                    Dim cronJobName = cronJob.CronJobName

                    Dim queryPoolParam6 = (From qPP6 In _dbPools6.PoolParameters
                                           Where qPP6.CronJob = cronJobName And qPP6.poolAlias = poolAlias).Single


                    Dim thisTimePeriod = queryPoolParam6.TimePeriod
                    Dim sport = queryPoolParam6.Sport
                    Dim timePeriodName = queryPoolParam6.timePeriodName
                    Dim timePeriodIncrement = CInt(queryPoolParam6.timePeriodIncrement)

                    Dim lastTimePeriod = timePeriodName + CStr(CInt(Mid(thisTimePeriod, Len(thisTimePeriod))) - timePeriodIncrement)
                    If lastTimePeriod = timePeriodName + "-1" Then
                        lastTimePeriod = timePeriodName + "0"
                    End If


                    Dim teams1 = (From teams2 In _dbPools6.Teams
                                  Where teams2.Sport = sport).ToList

                    ResetUserChoices(thisTimePeriod, EName, lastTimePeriod, teams1, poolAlias)


                    Dim userPick1 = New UserPick
                    userPick1 = GetPick(GameScheduleCollection, RadioButtonCollection, userPick1)

                    Dim userChoice1 = UpdateUserChoices(userPick1, teams1, EName, thisTimePeriod, poolAlias)

                    Session("confirmationNumber") = userChoice1.ListId

                End Using
            End Using
        Catch ex As Exception

        End Try


        Response.Redirect("~/LosersPool/ContenderConfirmation.aspx")

    End Sub

    Private Sub ResetUserChoices(thisTimePeriod As String, EName As String, lastTimePeriod As String, teams1 As List(Of Team), poolAlias As String)

        Dim _dbLoserPool1 As New LosersPoolContext
        Dim _dbPools1 As New PoolDbContext

        Try
            Using (_dbLoserPool1)
                Using (_dbPools1)

                    Dim queryPoolParam = (From pool1 In _dbPools1.PoolParameters
                                          Where pool1.poolAlias = poolAlias).Single
                    Dim preTimePeriod = queryPoolParam.timePeriodName + "0"

                    Dim lastTimePeriodChoice As New UserChoices
                    If lastTimePeriod = preTimePeriod Then
                        Dim queryUser = (From user1 In _dbLoserPool1.UserChoicesList
                                         Where user1.PoolAlias = poolAlias And user1.UserID = EName And user1.TimePeriod = thisTimePeriod).Single
                        lastTimePeriodChoice.AdministrationPick = False
                        lastTimePeriodChoice.ConfirmationNumber = 0
                        lastTimePeriodChoice.Contender = True
                        lastTimePeriodChoice.CronJob = queryPoolParam.CronJob
                        lastTimePeriodChoice.PickedGameCode = ""
                        lastTimePeriodChoice.PoolAlias = queryUser.PoolAlias
                        lastTimePeriodChoice.Sport = queryUser.Sport
                        lastTimePeriodChoice.Team1Available = True
                        lastTimePeriodChoice.Team2Available = True
                        lastTimePeriodChoice.Team3Available = True
                        lastTimePeriodChoice.Team4Available = True
                        lastTimePeriodChoice.Team5Available = True
                        lastTimePeriodChoice.Team6Available = True
                        lastTimePeriodChoice.Team7Available = True
                        lastTimePeriodChoice.Team8Available = True
                        lastTimePeriodChoice.Team9Available = True
                        lastTimePeriodChoice.Team10Available = True
                        lastTimePeriodChoice.Team11Available = True
                        lastTimePeriodChoice.Team12Available = True
                        lastTimePeriodChoice.Team13Available = True
                        lastTimePeriodChoice.Team14Available = True
                        lastTimePeriodChoice.Team15Available = True
                        lastTimePeriodChoice.Team16Available = True
                        lastTimePeriodChoice.Team17Available = True
                        lastTimePeriodChoice.Team18Available = True
                        lastTimePeriodChoice.Team19Available = True
                        lastTimePeriodChoice.Team20Available = True
                        lastTimePeriodChoice.Team21Available = True
                        lastTimePeriodChoice.Team22Available = True
                        lastTimePeriodChoice.Team23Available = True
                        lastTimePeriodChoice.Team24Available = True
                        lastTimePeriodChoice.Team25Available = True
                        lastTimePeriodChoice.Team26Available = True
                        lastTimePeriodChoice.Team27Available = True
                        lastTimePeriodChoice.Team28Available = True
                        lastTimePeriodChoice.Team29Available = True
                        lastTimePeriodChoice.Team30Available = True
                        lastTimePeriodChoice.Team31Available = True
                        lastTimePeriodChoice.Team32Available = True
                        lastTimePeriodChoice.TimePeriod = queryUser.TimePeriod
                        lastTimePeriodChoice.UserID = queryUser.UserID
                        lastTimePeriodChoice.UserName = queryUser.UserName
                        lastTimePeriodChoice.UserPick = ""
                        lastTimePeriodChoice.UserPickPostponed = False
                    Else
                        lastTimePeriodChoice = _dbLoserPool1.UserChoicesList.SingleOrDefault(Function(lWC) lWC.UserID = EName And lWC.TimePeriod = lastTimePeriod And lWC.PoolAlias = poolAlias)
                        lastTimePeriodChoice = SetContendersPickToFalse(lastTimePeriodChoice, teams1)
                        lastTimePeriodChoice.TimePeriod = thisTimePeriod
                        lastTimePeriodChoice.UserPick = ""

                    End If

                    Dim userChoice1 = New UserChoices
                    userChoice1 = _dbLoserPool1.UserChoicesList.SingleOrDefault(Function(uC1) uC1.UserID = EName And uC1.TimePeriod = thisTimePeriod And uC1.PoolAlias = poolAlias)
                    _dbLoserPool1.UserChoicesList.Remove(userChoice1)
                    _dbLoserPool1.UserChoicesList.Add(lastTimePeriodChoice)

                    _dbLoserPool1.SaveChanges()

                End Using
            End Using
        Catch ex As Exception

        End Try


    End Sub

    Private Function GetPick(GameScheduleCollection As Dictionary(Of String, TimePeriodSchedule), RadioButtonCollection As Dictionary(Of String, RadioButton), userPick1 As UserPick) As UserPick



        For Each radio1 In RadioButtonCollection

            If radio1.Key = Home1.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game1").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game1").GameCode
                End If
            End If

            If radio1.Key = Away1.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game1").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game1").GameCode
                End If
            End If

            If radio1.Key = Home2.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game2").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game2").GameCode
                End If
            End If

            If radio1.Key = Away2.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game2").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game2").GameCode
                End If
            End If

            If radio1.Key = Home3.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game3").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game3").GameCode
                End If
            End If

            If radio1.Key = Away3.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game3").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game3").GameCode
                End If
            End If

            If radio1.Key = Home4.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game4").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game4").GameCode
                End If
            End If

            If radio1.Key = Away4.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game4").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game4").GameCode
                End If
            End If

            If radio1.Key = Home5.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game5").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game5").GameCode
                End If
            End If

            If radio1.Key = Away5.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game5").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game5").GameCode
                End If
            End If

            If radio1.Key = Home6.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game6").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game6").GameCode
                End If
            End If

            If radio1.Key = Away6.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game6").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game6").GameCode
                End If
            End If

            If radio1.Key = Home7.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game7").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game7").GameCode
                End If
            End If

            If radio1.Key = Away7.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game7").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game7").GameCode
                End If
            End If

            If radio1.Key = Home8.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game8").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game8").GameCode
                End If
            End If

            If radio1.Key = Away8.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game8").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game8").GameCode
                End If
            End If

            If radio1.Key = Home9.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game9").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game9").GameCode
                End If
            End If

            If radio1.Key = Away9.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game9").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game9").GameCode
                End If
            End If

            If radio1.Key = Home10.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game10").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game10").GameCode
                End If
            End If

            If radio1.Key = Away10.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game10").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game10").GameCode
                End If
            End If

            If radio1.Key = Home11.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game11").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game11").GameCode
                End If
            End If

            If radio1.Key = Away11.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game11").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game11").GameCode
                End If
            End If

            If radio1.Key = Home12.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game12").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game12").GameCode
                End If
            End If

            If radio1.Key = Away12.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game12").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game12").GameCode
                End If
            End If

            If radio1.Key = Home13.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game13").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game13").GameCode
                End If
            End If

            If radio1.Key = Away13.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game13").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game13").GameCode
                End If
            End If

            If radio1.Key = Home14.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game14").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game14").GameCode
                End If
            End If

            If radio1.Key = Away14.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game14").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game14").GameCode
                End If
            End If

            If radio1.Key = Home15.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game15").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game15").GameCode
                End If
            End If

            If radio1.Key = Away15.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game15").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game15").GameCode
                End If
            End If

            If radio1.Key = Home16.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game16").HomeTeam
                    userPick1.GameCode = GameScheduleCollection("game16").GameCode
                End If
            End If

            If radio1.Key = Away16.ID Then
                If radio1.Value.Checked = True Then
                    userPick1.UserPick1 = GameScheduleCollection("game16").AwayTeam
                    userPick1.GameCode = GameScheduleCollection("game16").GameCode
                End If
            End If

        Next
        Return userPick1
    End Function

    Private Function UpdateUserChoices(userPick1 As UserPick, teams1 As List(Of Team), EName As String, thisTimePeriod As String, poolAlias As String) As UserChoices

        Dim _dbLoserPool2 As New LosersPoolContext

        Try
            Using (_dbLoserPool2)

                Dim userChoice1 = _dbLoserPool2.UserChoicesList.Single(Function(uC1) uC1.UserID = EName And uC1.TimePeriod = thisTimePeriod And uC1.PoolAlias = poolAlias)

                If userPick1.UserPick1 <> "" Then

                    If userPick1.UserPick1 = teams1(0).TeamName Then
                        userChoice1.Team1Available = False
                        userChoice1.UserPick = teams1(0).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(1).TeamName Then
                        userChoice1.Team2Available = False
                        userChoice1.UserPick = teams1(1).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(2).TeamName Then
                        userChoice1.Team3Available = False
                        userChoice1.UserPick = teams1(2).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(3).TeamName Then
                        userChoice1.Team4Available = False
                        userChoice1.UserPick = teams1(3).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(4).TeamName Then
                        userChoice1.Team5Available = False
                        userChoice1.UserPick = teams1(4).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(5).TeamName Then
                        userChoice1.Team6Available = False
                        userChoice1.UserPick = teams1(5).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(6).TeamName Then
                        userChoice1.Team7Available = False
                        userChoice1.UserPick = teams1(6).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(7).TeamName Then
                        userChoice1.Team8Available = False
                        userChoice1.UserPick = teams1(7).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(8).TeamName Then
                        userChoice1.Team9Available = False
                        userChoice1.UserPick = teams1(8).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(9).TeamName Then
                        userChoice1.Team10Available = False
                        userChoice1.UserPick = teams1(9).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(10).TeamName Then
                        userChoice1.Team11Available = False
                        userChoice1.UserPick = teams1(10).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(11).TeamName Then
                        userChoice1.Team12Available = False
                        userChoice1.UserPick = teams1(11).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(12).TeamName Then
                        userChoice1.Team13Available = False
                        userChoice1.UserPick = teams1(12).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(13).TeamName Then
                        userChoice1.Team14Available = False
                        userChoice1.UserPick = teams1(13).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(14).TeamName Then
                        userChoice1.Team15Available = False
                        userChoice1.UserPick = teams1(14).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(15).TeamName Then
                        userChoice1.Team16Available = False
                        userChoice1.UserPick = teams1(15).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(16).TeamName Then
                        userChoice1.Team17Available = False
                        userChoice1.UserPick = teams1(16).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(17).TeamName Then
                        userChoice1.Team18Available = False
                        userChoice1.UserPick = teams1(17).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(18).TeamName Then
                        userChoice1.Team19Available = False
                        userChoice1.UserPick = teams1(18).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(19).TeamName Then
                        userChoice1.Team20Available = False
                        userChoice1.UserPick = teams1(19).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(20).TeamName Then
                        userChoice1.Team21Available = False
                        userChoice1.UserPick = teams1(20).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(21).TeamName Then
                        userChoice1.Team22Available = False
                        userChoice1.UserPick = teams1(21).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(22).TeamName Then
                        userChoice1.Team23Available = False
                        userChoice1.UserPick = teams1(22).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(23).TeamName Then
                        userChoice1.Team24Available = False
                        userChoice1.UserPick = teams1(23).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(24).TeamName Then
                        userChoice1.Team25Available = False
                        userChoice1.UserPick = teams1(24).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(25).TeamName Then
                        userChoice1.Team26Available = False
                        userChoice1.UserPick = teams1(25).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(26).TeamName Then
                        userChoice1.Team27Available = False
                        userChoice1.UserPick = teams1(26).TeamName
                    End If


                    If userPick1.UserPick1 = teams1(27).TeamName Then
                        userChoice1.Team28Available = False
                        userChoice1.UserPick = teams1(27).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(28).TeamName Then
                        userChoice1.Team29Available = False
                        userChoice1.UserPick = teams1(28).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(29).TeamName Then
                        userChoice1.Team30Available = False
                        userChoice1.UserPick = teams1(29).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(30).TeamName Then
                        userChoice1.Team31Available = False
                        userChoice1.UserPick = teams1(30).TeamName
                    End If

                    If userPick1.UserPick1 = teams1(31).TeamName Then
                        userChoice1.Team32Available = False
                        userChoice1.UserPick = teams1(31).TeamName
                    End If

                    userChoice1.AdministrationPick = False
                    userChoice1.ConfirmationNumber = userChoice1.ListId
                    userChoice1.UserPickPostponed = False

                    userChoice1.PickedGameCode = userPick1.GameCode

                    Dim queryUserPicks = (From pick1 In _dbLoserPool2.UserPicks
                                          Where pick1.UserPickPostponed = False And pick1.UserID = EName And pick1.PoolAlias = poolAlias).SingleOrDefault

                    If queryUserPicks Is Nothing Then
                    Else
                        _dbLoserPool2.UserPicks.Remove(queryUserPicks)
                    End If


                    userPick1.CronJobName = userChoice1.CronJob
                    userPick1.GameCode = userChoice1.PickedGameCode
                    userPick1.PoolAlias = userChoice1.PoolAlias
                    userPick1.TimePeriod = userChoice1.TimePeriod
                    userPick1.UserID = userChoice1.UserID
                    userPick1.UserPick1 = userChoice1.UserPick
                    userPick1.UserPickPostponed = False
                    userPick1.PickIsTied = True
                    userPick1.PickIsWinning = False

                    _dbLoserPool2.UserPicks.Add(userPick1)

                    _dbLoserPool2.SaveChanges()

                    Return userChoice1

                End If

            End Using
        Catch ex As Exception

        End Try

        Return Nothing

    End Function

    Private Shared Function SetContendersPickToFalse(user1 As UserChoices, teams1 As List(Of Team)) As UserChoices


        If user1.UserPick = teams1(0).TeamName Then
            user1.Team1Available = False
        ElseIf user1.UserPick = teams1(1).TeamName Then
            user1.Team2Available = False
        ElseIf user1.UserPick = teams1(2).TeamName Then
            user1.Team3Available = False
        ElseIf user1.UserPick = teams1(3).TeamName Then
            user1.Team4Available = False
        ElseIf user1.UserPick = teams1(4).TeamName Then
            user1.Team5Available = False
        ElseIf user1.UserPick = teams1(5).TeamName Then
            user1.Team6Available = False
        ElseIf user1.UserPick = teams1(6).TeamName Then
            user1.Team7Available = False
        ElseIf user1.UserPick = teams1(7).TeamName Then
            user1.Team8Available = False
        ElseIf user1.UserPick = teams1(8).TeamName Then
            user1.Team9Available = False
        ElseIf user1.UserPick = teams1(9).TeamName Then
            user1.Team10Available = False
        ElseIf user1.UserPick = teams1(10).TeamName Then
            user1.Team11Available = False
        ElseIf user1.UserPick = teams1(11).TeamName Then
            user1.Team12Available = False
        ElseIf user1.UserPick = teams1(12).TeamName Then
            user1.Team13Available = False
        ElseIf user1.UserPick = teams1(13).TeamName Then
            user1.Team14Available = False
        ElseIf user1.UserPick = teams1(14).TeamName Then
            user1.Team15Available = False
        ElseIf user1.UserPick = teams1(15).TeamName Then
            user1.Team16Available = False
        ElseIf user1.UserPick = teams1(16).TeamName Then
            user1.Team17Available = False
        ElseIf user1.UserPick = teams1(17).TeamName Then
            user1.Team18Available = False
        ElseIf user1.UserPick = teams1(18).TeamName Then
            user1.Team19Available = False
        ElseIf user1.UserPick = teams1(19).TeamName Then
            user1.Team20Available = False
        ElseIf user1.UserPick = teams1(20).TeamName Then
            user1.Team21Available = False
        ElseIf user1.UserPick = teams1(21).TeamName Then
            user1.Team22Available = False
        ElseIf user1.UserPick = teams1(22).TeamName Then
            user1.Team23Available = False
        ElseIf user1.UserPick = teams1(23).TeamName Then
            user1.Team24Available = False
        ElseIf user1.UserPick = teams1(24).TeamName Then
            user1.Team25Available = False
        ElseIf user1.UserPick = teams1(25).TeamName Then
            user1.Team26Available = False
        ElseIf user1.UserPick = teams1(26).TeamName Then
            user1.Team27Available = False
        ElseIf user1.UserPick = teams1(27).TeamName Then
            user1.Team28Available = False
        ElseIf user1.UserPick = teams1(28).TeamName Then
            user1.Team29Available = False
        ElseIf user1.UserPick = teams1(29).TeamName Then
            user1.Team30Available = False
        ElseIf user1.UserPick = teams1(30).TeamName Then
            user1.Team31Available = False
        ElseIf user1.UserPick = teams1(31).TeamName Then
            user1.Team32Available = False

        End If
        Return user1
    End Function


    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/LosersPool/LoserPoolHome.aspx")
    End Sub

    Protected Sub ImageButton_Click(sender As Object, e As ImageClickEventArgs)

        ImageButtonCollection.Clear()

        ImageButtonCollection.Add(Home1.ID, Home1)
        ImageButtonCollection.Add(Away1.ID, Away1)
        ImageButtonCollection.Add(Home2.ID, Home2)
        ImageButtonCollection.Add(Away2.ID, Away2)
        ImageButtonCollection.Add(Home3.ID, Home3)
        ImageButtonCollection.Add(Away3.ID, Away3)
        ImageButtonCollection.Add(Home4.ID, Home4)
        ImageButtonCollection.Add(Away4.ID, Away4)
        ImageButtonCollection.Add(Home5.ID, Home5)
        ImageButtonCollection.Add(Away5.ID, Away5)
        ImageButtonCollection.Add(Home6.ID, Home6)
        ImageButtonCollection.Add(Away6.ID, Away6)
        ImageButtonCollection.Add(Home7.ID, Home7)
        ImageButtonCollection.Add(Away7.ID, Away7)
        ImageButtonCollection.Add(Home8.ID, Home8)
        ImageButtonCollection.Add(Away8.ID, Away8)
        ImageButtonCollection.Add(Home9.ID, Home9)
        ImageButtonCollection.Add(Away9.ID, Away9)
        ImageButtonCollection.Add(Home10.ID, Home10)
        ImageButtonCollection.Add(Away10.ID, Away10)
        ImageButtonCollection.Add(Home11.ID, Home11)
        ImageButtonCollection.Add(Away11.ID, Away11)
        ImageButtonCollection.Add(Home12.ID, Home12)
        ImageButtonCollection.Add(Away12.ID, Away12)
        ImageButtonCollection.Add(Home13.ID, Home13)
        ImageButtonCollection.Add(Away13.ID, Away13)
        ImageButtonCollection.Add(Home14.ID, Home14)
        ImageButtonCollection.Add(Away14.ID, Away14)
        ImageButtonCollection.Add(Home15.ID, Home15)
        ImageButtonCollection.Add(Away15.ID, Away15)
        ImageButtonCollection.Add(Home16.ID, Home16)
        ImageButtonCollection.Add(Away16.ID, Away16)

        For Each image1 In ImageButtonCollection
            ImageButtonCollection(image1.Key).ImageUrl = "~/Models/RadioButtonImages/Unchecked.png"
        Next

        ImageButtonCollection(sender.id).ImageUrl = "~/Models/RadioButtonImages/Checked.png"

        RadioButtonCollection(sender.id).Checked = True

    End Sub
End Class

Public Class MyPickList
    Public Property MyPicks As UserChoices

    Public Sub New(Ename As String, TimePeriod As String, poolAlias As String, teams1 As List(Of Team), byeTeams1 As Dictionary(Of String, String), TeamList As Dictionary(Of String, Team))

        Dim user1 As UserChoices = UserPickList(Ename, TimePeriod, poolAlias, teams1, byeTeams1, TeamList)
        Me.MyPicks = user1

    End Sub

    Public Shared Function UserPickList(EName As String, TimePeriod As String, poolAlias As String, teams1 As List(Of Team), byeTeams1 As Dictionary(Of String, String), TeamList As Dictionary(Of String, Team)) As UserChoices
        Dim _dbLoserPool3 = New LosersPoolContext
        Dim _dbApp2 = New ApplicationDbContext

        Try
            Using (_dbLoserPool3)
                Using (_dbApp2)

                    ' Clear Previous Picks
                    Dim users1 = New List(Of MyPick)
                    users1 = _dbLoserPool3.MyPicks.ToList

                    For Each user2 In users1
                        _dbLoserPool3.MyPicks.Remove(user2)
                    Next

                    _dbLoserPool3.SaveChanges()

                    Dim queryCronJobPool = (From qCJP1 In _dbApp2.CronJobPools
                                            Where qCJP1.CronJobPoolAlias = poolAlias).Single


                    Dim user1 = (From u1 In _dbLoserPool3.UserChoicesList
                                 Where u1.UserID = EName And u1.TimePeriod = TimePeriod And u1.PoolAlias = poolAlias And u1.CronJob = queryCronJobPool.CronJobName).Single

                    UpdateUserChoices(user1, teams1, byeTeams1, TeamList)

                    Return user1
                End Using
            End Using
        Catch ex As Exception

        End Try

        Return Nothing
    End Function

    Private Shared Sub UpdateUserChoices(user1 As UserChoices, teams1 As List(Of Team), byeTeams1 As Dictionary(Of String, String), TeamList As Dictionary(Of String, Team))

        Dim _dbLoserPool10 As New LosersPoolContext

        Try
            Using (_dbLoserPool10)

                For Each team1 In teams1
                    If byeTeams1.ContainsKey(team1.TeamName) Then
                    Else
                        TeamList.Add(team1.TeamName, team1)
                    End If
                Next

                If user1.PossibleUserPicks.Count > 0 Then
                    For Each pick1 In user1.PossibleUserPicks
                        user1.PossibleUserPicks.Remove(pick1.Key)
                    Next
                End If

                Dim querySchedule = (From qS In _dbLoserPool10.ScheduleEntities
                                     Where qS.TimePeriod = user1.TimePeriod And qS.CronJob = user1.CronJob).ToList

                For Each game In querySchedule

                    If user1 Is Nothing Then
                    Else
                        If teams1(0).TeamName = game.HomeTeam Or teams1(0).TeamName = game.AwayTeam Then
                            If TeamList.ContainsKey(teams1(0).TeamName) Then
                                If user1.Team1Available = True Or user1.UserPick = teams1(0).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(0).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(0).TeamName, possiblePick1)
                                End If
                            End If
                        End If


                        If TeamList.ContainsKey(teams1(1).TeamName) Then
                            If teams1(1).TeamName = game.HomeTeam Or teams1(1).TeamName = game.AwayTeam Then
                                If user1.Team2Available = True Or user1.UserPick = teams1(1).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(1).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(1).TeamName, possiblePick1)
                                End If
                            End If

                        End If


                        If TeamList.ContainsKey(teams1(2).TeamName) Then
                            If teams1(2).TeamName = game.HomeTeam Or teams1(2).TeamName = game.AwayTeam Then
                                If user1.Team3Available = True Or user1.UserPick = teams1(2).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(2).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(2).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(3).TeamName) Then
                            If teams1(3).TeamName = game.HomeTeam Or teams1(3).TeamName = game.AwayTeam Then
                                If user1.Team4Available = True Or user1.UserPick = teams1(3).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(3).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(3).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(4).TeamName) Then
                            If teams1(4).TeamName = game.HomeTeam Or teams1(4).TeamName = game.AwayTeam Then
                                If user1.Team5Available = True Or user1.UserPick = teams1(4).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(4).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(4).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(5).TeamName) Then
                            If teams1(5).TeamName = game.HomeTeam Or teams1(5).TeamName = game.AwayTeam Then
                                If user1.Team6Available = True Or user1.UserPick = teams1(5).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(5).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(5).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(6).TeamName) Then
                            If teams1(6).TeamName = game.HomeTeam Or teams1(6).TeamName = game.AwayTeam Then
                                If user1.Team7Available = True Or user1.UserPick = teams1(6).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(6).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(6).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(7).TeamName) Then
                            If teams1(7).TeamName = game.HomeTeam Or teams1(7).TeamName = game.AwayTeam Then
                                If user1.Team8Available = True Or user1.UserPick = teams1(7).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(7).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(7).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(8).TeamName) Then
                            If teams1(8).TeamName = game.HomeTeam Or teams1(8).TeamName = game.AwayTeam Then
                                If user1.Team9Available = True Or user1.UserPick = teams1(8).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(8).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(8).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(9).TeamName) Then
                            If teams1(9).TeamName = game.HomeTeam Or teams1(9).TeamName = game.AwayTeam Then
                                If user1.Team10Available = True Or user1.UserPick = teams1(9).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(9).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(9).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(10).TeamName) Then
                            If teams1(10).TeamName = game.HomeTeam Or teams1(10).TeamName = game.AwayTeam Then
                                If user1.Team11Available = True Or user1.UserPick = teams1(10).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(10).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(10).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(11).TeamName) Then
                            If teams1(11).TeamName = game.HomeTeam Or teams1(11).TeamName = game.AwayTeam Then
                                If user1.Team12Available = True Or user1.UserPick = teams1(11).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(11).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(11).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(12).TeamName) Then
                            If teams1(12).TeamName = game.HomeTeam Or teams1(12).TeamName = game.AwayTeam Then
                                If user1.Team13Available = True Or user1.UserPick = teams1(12).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(12).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(12).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(13).TeamName) Then
                            If teams1(13).TeamName = game.HomeTeam Or teams1(13).TeamName = game.AwayTeam Then
                                If user1.Team14Available = True Or user1.UserPick = teams1(13).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(13).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(13).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(14).TeamName) Then
                            If teams1(14).TeamName = game.HomeTeam Or teams1(14).TeamName = game.AwayTeam Then
                                If user1.Team15Available = True Or user1.UserPick = teams1(14).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(14).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(14).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(15).TeamName) Then
                            If teams1(15).TeamName = game.HomeTeam Or teams1(15).TeamName = game.AwayTeam Then
                                If user1.Team16Available = True Or user1.UserPick = teams1(15).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(15).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(15).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(16).TeamName) Then
                            If teams1(16).TeamName = game.HomeTeam Or teams1(16).TeamName = game.AwayTeam Then
                                If user1.Team17Available = True Or user1.UserPick = teams1(16).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(16).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(16).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(17).TeamName) Then
                            If teams1(17).TeamName = game.HomeTeam Or teams1(17).TeamName = game.AwayTeam Then
                                If user1.Team18Available = True Or user1.UserPick = teams1(17).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(17).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(17).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(18).TeamName) Then
                            If teams1(18).TeamName = game.HomeTeam Or teams1(18).TeamName = game.AwayTeam Then
                                If user1.Team19Available = True Or user1.UserPick = teams1(18).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(18).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(18).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(19).TeamName) Then
                            If teams1(19).TeamName = game.HomeTeam Or teams1(19).TeamName = game.AwayTeam Then
                                If user1.Team20Available = True Or user1.UserPick = teams1(19).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(19).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(19).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(20).TeamName) Then
                            If teams1(20).TeamName = game.HomeTeam Or teams1(20).TeamName = game.AwayTeam Then
                                If user1.Team21Available = True Or user1.UserPick = teams1(20).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(20).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(20).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(21).TeamName) Then
                            If teams1(21).TeamName = game.HomeTeam Or teams1(21).TeamName = game.AwayTeam Then
                                If user1.Team22Available = True Or user1.UserPick = teams1(21).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(21).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(21).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(22).TeamName) Then
                            If teams1(22).TeamName = game.HomeTeam Or teams1(22).TeamName = game.AwayTeam Then
                                If user1.Team23Available = True Or user1.UserPick = teams1(22).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(22).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(22).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(23).TeamName) Then
                            If teams1(23).TeamName = game.HomeTeam Or teams1(23).TeamName = game.AwayTeam Then
                                If user1.Team24Available = True Or user1.UserPick = teams1(23).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(23).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(23).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(24).TeamName) Then
                            If teams1(24).TeamName = game.HomeTeam Or teams1(24).TeamName = game.AwayTeam Then
                                If user1.Team25Available = True Or user1.UserPick = teams1(24).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(24).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(24).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(25).TeamName) Then
                            If teams1(25).TeamName = game.HomeTeam Or teams1(25).TeamName = game.AwayTeam Then
                                If user1.Team26Available = True Or user1.UserPick = teams1(25).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(25).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(25).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(26).TeamName) Then
                            If teams1(26).TeamName = game.HomeTeam Or teams1(26).TeamName = game.AwayTeam Then
                                If user1.Team27Available = True Or user1.UserPick = teams1(26).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(26).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(26).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(27).TeamName) Then
                            If teams1(27).TeamName = game.HomeTeam Or teams1(27).TeamName = game.AwayTeam Then
                                If user1.Team28Available = True Or user1.UserPick = teams1(27).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(27).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(27).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(28).TeamName) Then
                            If teams1(28).TeamName = game.HomeTeam Or teams1(28).TeamName = game.AwayTeam Then
                                If user1.Team29Available = True Or user1.UserPick = teams1(28).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(28).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(28).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If TeamList.ContainsKey(teams1(29).TeamName) Then
                            If teams1(29).TeamName = game.HomeTeam Or teams1(29).TeamName = game.AwayTeam Then
                                If user1.Team30Available = True Or user1.UserPick = teams1(29).TeamName Then
                                    Dim possiblePick1 As New PossiblePick
                                    possiblePick1.pickedTeam = teams1(29).TeamName
                                    possiblePick1.gameCode = game.GameCode
                                    user1.PossibleUserPicks.Add(teams1(29).TeamName, possiblePick1)
                                End If
                            End If
                        End If

                        If teams1.Count > 30 Then
                            If teams1(30).TeamName = game.HomeTeam Or teams1(30).TeamName = game.AwayTeam Then
                                If TeamList.ContainsKey(teams1(30).TeamName) Then
                                    If user1.Team31Available = True Or user1.UserPick = teams1(30).TeamName Then
                                        Dim possiblePick1 As New PossiblePick
                                        possiblePick1.pickedTeam = teams1(30).TeamName
                                        possiblePick1.gameCode = game.GameCode
                                        user1.PossibleUserPicks.Add(teams1(30).TeamName, possiblePick1)
                                    End If
                                End If
                            End If
                        End If

                        If teams1.Count > 31 Then
                            If teams1(31).TeamName = game.HomeTeam Or teams1(31).TeamName = game.AwayTeam Then
                                If TeamList.ContainsKey(teams1(31).TeamName) Then
                                    If user1.Team32Available = True Or user1.UserPick = teams1(31).TeamName Then
                                        Dim possiblePick1 As New PossiblePick
                                        possiblePick1.pickedTeam = teams1(31).TeamName
                                        possiblePick1.gameCode = game.GameCode
                                        user1.PossibleUserPicks.Add(teams1(31).TeamName, possiblePick1)
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

            End Using

        Catch ex As Exception

        End Try



    End Sub

End Class
