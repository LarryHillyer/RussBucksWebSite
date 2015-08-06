Imports System.Data

Imports System
Imports System.Collections.Specialized
Imports System.Collections
Imports System.ComponentModel
Imports System.Security.Permissions
Imports System.Web

Imports System.Web.UI
Imports System.Web.UI.WebControls


Imports RussBucksPools
Imports RussBucksPools.LosersPool.Models
Imports RussBucksPools.JoinPools.Models


Public Class CompletedTimePeriodResults
    Inherits System.Web.UI.Page

    Private GameUpdateCollection As New Dictionary(Of String, GameUpdate)
    Private GameUpdateCollectionSorted As New Dictionary(Of String, GameUpdate)

    ' Keeps track of if users are winning, losing, of if the game they picked is tied
    Private UserStatusCollection As New Dictionary(Of String, UserStatus)

    Private WinningTeamForeColor As System.Drawing.Color = Drawing.Color.Green
    Private WinningTeamBackColor As System.Drawing.Color = Drawing.Color.LightGreen

    Private LosingTeamForeColor As System.Drawing.Color = Drawing.Color.Red
    Private LosingTeamBackColor As System.Drawing.Color = Drawing.Color.LightSalmon

    Private userColor As New System.Drawing.Color

    Private UserForeColor As System.Drawing.Color = Drawing.Color.Green
    Private UserBackColor As System.Drawing.Color = Drawing.Color.LightGreen


    Private AvailableForeColor As System.Drawing.Color = Drawing.Color.Green
    Private AvailableBackColor As System.Drawing.Color = Drawing.Color.LightGreen

    Private NotAvailableForeColor As System.Drawing.Color = Drawing.Color.Red
    Private NotAvailableBackColor As System.Drawing.Color = Drawing.Color.LightSalmon

    Private AdminBackColor As System.Drawing.Color = Drawing.Color.Yellow

    Private TeamBackColor As System.Drawing.Color = Drawing.Color.White

    Private LoserCollectionSorted As New Dictionary(Of String, Loser)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        If Not Page.IsPostBack Then

            Dim _dbLoserPool As New LosersPoolContext
            Dim _dbPools As New PoolDbContext
            Dim _dbApp As New ApplicationDbContext
            Try
                Using (_dbLoserPool)
                    Using (_dbPools)
                        Using (_dbApp)

                            Dim poolAlias = CStr(Session("poolAlias"))
                            Dim staleTimePeriod = CStr(Session("TimePeriod"))

                            Dim cronJob = (From cJN1 In _dbApp.CronJobPools
                                            Where cJN1.CronJobPoolAlias = poolAlias).Single

                            Dim cronJobName = cronJob.CronJobName

                            Dim queryPoolParam = (From qPP1 In _dbPools.PoolParameters
                                                  Where qPP1.CronJob = cronJobName And qPP1.poolAlias = poolAlias).Single

                            Dim thisTimePeriod1 = queryPoolParam.TimePeriod
                            Dim sport = queryPoolParam.Sport
                            Dim poolState = queryPoolParam.poolState

                            If thisTimePeriod1 <> staleTimePeriod And poolState <> "Scoring Update" Then
                                Response.Redirect("~/LosersPool/LoserPoolHome.aspx")
                            End If

                            Dim thisTimePeriod = CStr(Session("completedTimePeriod"))

                            Dim queryLeagueSize = (From sport1 In _dbPools.Sports
                                              Where sport1.SportName = sport).ToList

                            Dim leagueSize = queryLeagueSize(0).LeagueSize

                            Dim queryContenders = (From contender1 In _dbLoserPool.UserChoicesList
                                               Where contender1.TimePeriod = thisTimePeriod And contender1.Contender = True And contender1.PoolAlias = poolAlias And contender1.CronJob = cronJobName).ToList

                            Dim teams1 = (From teams2 In _dbPools.Teams
                                          Where teams2.Sport = sport And teams2.TeamName <> "dummy").ToList

                            Dim queryTimePeriods = (From timePeriod1 In _dbLoserPool.ScheduleTimePeriods
                                               Where CInt(Mid(timePeriod1.TimePeriod, Len(timePeriod1.TimePeriod), 3)) <= CInt(Mid(thisTimePeriod, Len(thisTimePeriod), 3)) And timePeriod1.CronJob = cronJobName
                                               Order By timePeriod1.TimePeriod Descending).ToList

                            CreateScoringUpdateTable(thisTimePeriod, poolAlias, queryContenders, teams1, GameUpdateCollection, GameUpdateCollectionSorted, UserStatusCollection, cronJobName)

                            CreateContendersTable(thisTimePeriod, poolAlias, teams1, leagueSize, cronJobName)

                            CreateLoserTable(thisTimePeriod, poolAlias, queryTimePeriods, LoserCollectionSorted, cronJobName)

                            ViewState("TimePeriod") = thisTimePeriod

                        End Using
                    End Using
                End Using
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub CreateScoringUpdateTable(thisTimePeriod As String, poolAlias As String, queryContenders As List(Of UserChoices), teams1 As List(Of Team), _
                                         GameUpdateCollection As Dictionary(Of String, GameUpdate), GameUpdateCollectionSorted As Dictionary(Of String, GameUpdate), _
                                         UserStatusCollection As Dictionary(Of String, UserStatus), cronJobName As String)

        GameUpdateCollection = GetGameUpdateCollection(thisTimePeriod, queryContenders, teams1, GameUpdateCollection, cronJobName)

        GetScoringUpdateTableHeader(GameUpdateCollection, teams1)

        UserStatusCollection = GetUserStatusCollection(UserStatusCollection, thisTimePeriod, poolAlias, cronJobName)

        GameUpdateCollectionSorted = SortGameUpdateCollection(GameUpdateCollection, GameUpdateCollectionSorted)

        AddUserStatusToScoringUpdateTable(GameUpdateCollectionSorted, UserStatusCollection, thisTimePeriod, poolAlias, cronJobName)

    End Sub

    Private Sub CreateContendersTable(thisTimePeriod As String, poolAlias As String, teams1 As List(Of Team), leagueSize As Int32, cronJobName As String)
        GetTableSize(leagueSize)

        GetContendersTableHeader(teams1)

        AddUserStatusToContenderTable(thisTimePeriod, poolAlias, teams1, leagueSize, cronJobName)

    End Sub

    Private Sub CreateLoserTable(thisTimePeriod As String, poolAlias As String, queryTimePeriods As List(Of ScheduleTimePeriod), LoserCollectionSorted As Dictionary(Of String, Loser), cronJobName As String)
        SortLosers(thisTimePeriod, poolAlias, queryTimePeriods, cronJobName)

        AddLosersToLoserTable(LoserCollectionSorted)
    End Sub

    Private Shared Function GetGameUpdateCollection(thisTimePeriod As String, queryContenders As List(Of UserChoices), _
                                                    teams1 As List(Of Team), GameUpdateCollection As Dictionary(Of String, GameUpdate), _
                                                    cronJobName As String) As Dictionary(Of String, GameUpdate)

        Dim _dbLoserPool1 = New LosersPoolContext


        Try
            Using (_dbLoserPool1)

                Dim timePeriodGames = (From scheduleEntity1 In _dbLoserPool1.ScheduleEntities
                        Where scheduleEntity1.TimePeriod = thisTimePeriod And scheduleEntity1.CronJob = cronJobName
                        Select scheduleEntity1).ToList

                Dim cnt1 = 0
                For Each game1 In timePeriodGames

                    cnt1 = cnt1 + 1
                    Dim gameupdate1 As New GameUpdate

                    gameupdate1.GameId = "game" + CStr(cnt1)
                    gameupdate1.HomeTeam = game1.HomeTeam
                    gameupdate1.AwayTeam = game1.AwayTeam
                    gameupdate1.GameCode = game1.GameCode
                    gameupdate1.Status = game1.Status
                    gameupdate1.DisplayStatus1 = game1.DisplayStatus1
                    gameupdate1.DisplayStatus2 = game1.DisplayStatus2
                    gameupdate1.HomeScore = game1.HomeScore
                    gameupdate1.AwayScore = game1.AwayScore
                    gameupdate1.GameTime = game1.GameTime

                    For Each user1 In queryContenders

                        gameupdate1.UserHandles.Add(user1.UserName)

                        Dim teamAvailability = False
                        Dim homeAvailabilitySet = False
                        Dim awayAvailabilitySet = False

                        For Each team1 In teams1

                            If team1.TeamName = game1.HomeTeam Then
                                If team1.TeamName = teams1(0).TeamName Then
                                    teamAvailability = user1.Team1Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(1).TeamName Then
                                    teamAvailability = user1.Team2Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(2).TeamName Then
                                    teamAvailability = user1.Team3Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(3).TeamName Then
                                    teamAvailability = user1.Team4Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(4).TeamName Then
                                    teamAvailability = user1.Team5Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(5).TeamName Then
                                    teamAvailability = user1.Team6Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(6).TeamName Then
                                    teamAvailability = user1.Team7Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(7).TeamName Then
                                    teamAvailability = user1.Team8Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(8).TeamName Then
                                    teamAvailability = user1.Team9Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(9).TeamName Then
                                    teamAvailability = user1.Team10Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(10).TeamName Then
                                    teamAvailability = user1.Team11Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(11).TeamName Then
                                    teamAvailability = user1.Team12Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(12).TeamName Then
                                    teamAvailability = user1.Team13Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(13).TeamName Then
                                    teamAvailability = user1.Team14Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)

                                ElseIf team1.TeamName = teams1(14).TeamName Then
                                    teamAvailability = user1.Team15Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(15).TeamName Then
                                    teamAvailability = user1.Team16Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(16).TeamName Then
                                    teamAvailability = user1.Team17Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(17).TeamName Then
                                    teamAvailability = user1.Team18Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(18).TeamName Then
                                    teamAvailability = user1.Team19Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)

                                ElseIf team1.TeamName = teams1(19).TeamName Then
                                    teamAvailability = user1.Team20Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(20).TeamName Then
                                    teamAvailability = user1.Team21Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(21).TeamName Then
                                    teamAvailability = user1.Team22Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(22).TeamName Then
                                    teamAvailability = user1.Team23Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(23).TeamName Then
                                    teamAvailability = user1.Team24Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(24).TeamName Then
                                    teamAvailability = user1.Team25Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(25).TeamName Then
                                    teamAvailability = user1.Team26Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(26).TeamName Then
                                    teamAvailability = user1.Team27Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(27).TeamName Then
                                    teamAvailability = user1.Team28Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(28).TeamName Then
                                    teamAvailability = user1.Team29Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(29).TeamName Then
                                    teamAvailability = user1.Team30Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(30).TeamName Then
                                    teamAvailability = user1.Team31Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(31).TeamName Then
                                    teamAvailability = user1.Team32Available
                                    gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    homeAvailabilitySet = True

                                End If

                            ElseIf team1.TeamName = game1.AwayTeam Then
                                If team1.TeamName = teams1(0).TeamName Then
                                    teamAvailability = user1.Team1Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(1).TeamName Then
                                    teamAvailability = user1.Team2Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(2).TeamName Then
                                    teamAvailability = user1.Team3Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(3).TeamName Then
                                    teamAvailability = user1.Team4Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(4).TeamName Then
                                    teamAvailability = user1.Team5Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True

                                ElseIf team1.TeamName = teams1(5).TeamName Then
                                    teamAvailability = user1.Team6Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(6).TeamName Then
                                    teamAvailability = user1.Team7Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(7).TeamName Then
                                    teamAvailability = user1.Team8Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(8).TeamName Then
                                    teamAvailability = user1.Team9Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(9).TeamName Then
                                    teamAvailability = user1.Team10Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(10).TeamName Then
                                    teamAvailability = user1.Team11Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(11).TeamName Then
                                    teamAvailability = user1.Team12Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(12).TeamName Then
                                    teamAvailability = user1.Team13Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(13).TeamName Then
                                    teamAvailability = user1.Team14Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(14).TeamName Then
                                    teamAvailability = user1.Team15Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(15).TeamName Then
                                    teamAvailability = user1.Team16Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(16).TeamName Then
                                    teamAvailability = user1.Team17Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(17).TeamName Then
                                    teamAvailability = user1.Team18Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(18).TeamName Then
                                    teamAvailability = user1.Team19Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(19).TeamName Then
                                    teamAvailability = user1.Team20Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(20).TeamName Then
                                    teamAvailability = user1.Team21Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(21).TeamName Then
                                    teamAvailability = user1.Team22Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(22).TeamName Then
                                    teamAvailability = user1.Team23Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(23).TeamName Then
                                    teamAvailability = user1.Team24Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(24).TeamName Then
                                    teamAvailability = user1.Team25Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(25).TeamName Then
                                    teamAvailability = user1.Team26Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(26).TeamName Then
                                    teamAvailability = user1.Team27Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(27).TeamName Then
                                    teamAvailability = user1.Team28Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(28).TeamName Then
                                    teamAvailability = user1.Team29Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(29).TeamName Then
                                    teamAvailability = user1.Team30Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(30).TeamName Then
                                    teamAvailability = user1.Team31Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                ElseIf team1.TeamName = teams1(31).TeamName Then
                                    teamAvailability = user1.Team32Available
                                    gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    awayAvailabilitySet = True
                                End If
                            End If
                            If homeAvailabilitySet = True And awayAvailabilitySet = True Then
                                Exit For
                            End If
                        Next
                    Next
                    GameUpdateCollection.Add(gameupdate1.GameId, gameupdate1)
                Next

                Return GameUpdateCollection

            End Using


        Catch ex As Exception

        End Try

        Return Nothing
    End Function

    Private Shared Function SetHomeTeamAvailabilityState(gameUpdate1 As GameUpdate, team As String, teamAvailability As Boolean, user1 As UserChoices) As GameUpdate
        Dim _dbLoserPool4 As New LosersPoolContext
        Try
            Using (_dbLoserPool4)
                Dim queryUserPick = (From qUP1 In _dbLoserPool4.UserChoicesList
                                     Where qUP1.CronJob = user1.CronJob And qUP1.PoolAlias = user1.PoolAlias And _
                                     qUP1.PickedGameCode = gameUpdate1.GameCode And qUP1.UserID = user1.UserID).SingleOrDefault

                If gameUpdate1.HomeTeam = team And Not (queryUserPick Is Nothing) Then
                    If queryUserPick.UserPick = team And queryUserPick.UserPickPostponed = False Then
                        gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "L")
                    ElseIf queryUserPick.UserPick = team And queryUserPick.UserPickPostponed = True Then
                        gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "P")
                    ElseIf teamAvailability = True Then
                        gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "A")
                    ElseIf teamAvailability = False Then
                        gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "NA")
                    End If
                ElseIf gameUpdate1.HomeTeam = team And teamAvailability = True Then
                    gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "A")
                ElseIf gameUpdate1.HomeTeam = team And teamAvailability = False Then
                    gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "NA")
                End If

            End Using
        Catch ex As Exception

        End Try

        Return gameUpdate1
    End Function

    Private Shared Function SetAwayTeamAvailabilityState(gameUpdate1 As GameUpdate, team As String, teamAvailability As Boolean, user1 As UserChoices) As GameUpdate

        Dim _dbLoserPool4 As New LosersPoolContext

        Try
            Using (_dbLoserPool4)

                Dim queryUserPick = (From qUP1 In _dbLoserPool4.UserChoicesList
                                     Where qUP1.CronJob = user1.CronJob And qUP1.PoolAlias = user1.PoolAlias And _
                                     qUP1.PickedGameCode = gameUpdate1.GameCode And qUP1.UserID = user1.UserID).SingleOrDefault

                If gameUpdate1.AwayTeam = team And Not (queryUserPick Is Nothing) Then
                    If queryUserPick.UserPick = team And queryUserPick.UserPickPostponed = False Then
                        gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "L")
                    ElseIf queryUserPick.UserPick = team And queryUserPick.UserPickPostponed = True Then
                        gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "P")
                    ElseIf teamAvailability = True Then
                        gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "A")
                    ElseIf teamAvailability = False Then
                        gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "NA")
                    End If
                ElseIf gameUpdate1.AwayTeam = team And teamAvailability = True Then
                    gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "A")
                ElseIf gameUpdate1.AwayTeam = team And teamAvailability = False Then
                    gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "NA")

                End If

            End Using
        Catch ex As Exception

        End Try


        Return gameUpdate1
    End Function

    Private Sub GetScoringUpdateTableHeader(GameUpdateCollection As Dictionary(Of String, GameUpdate), teams1 As List(Of Team))

        If Not Page.IsPostBack Then

            Dim queryGameUpdateCollection = (From gameUpdateCollection1 In GameUpdateCollection
                                             Order By gameUpdateCollection1.Key Ascending).ToList

            For Each game In queryGameUpdateCollection

                Dim HomeTeamImage As String = ""
                Dim AwayTeamImage As String = ""
                Dim HomeTeamIcon As String = ""
                Dim AwayTeamIcon As String = ""

                For Each team1 In teams1
                    If GameUpdateCollection(game.Key).HomeTeam = team1.TeamName Then
                        HomeTeamImage = team1.TeamLabel
                        HomeTeamIcon = team1.TeamIcon
                    ElseIf GameUpdateCollection(game.Key).AwayTeam = team1.TeamName Then
                        AwayTeamImage = team1.TeamLabel
                        AwayTeamIcon = team1.TeamIcon
                    End If
                Next

                If game.Key = "game1" Then
                    GameNumber1.Text = game.Key
                    HomeTeam1Image1.ImageUrl = HomeTeamImage
                    AwayTeam1Image1.ImageUrl = AwayTeamImage
                    HomeTeam1Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam1Icon1.ImageUrl = AwayTeamIcon

                    HomeScore1.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore1.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber1Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore1.Text) > CInt(AwayScore1.Text) Then
                        HomeScore1.ForeColor = WinningTeamForeColor
                        AwayScore1.ForeColor = LosingTeamForeColor

                        HomeTeam1Image.BackColor = WinningTeamBackColor
                        AwayTeam1Image.BackColor = LosingTeamBackColor
                        HomeScore1.BackColor = WinningTeamBackColor
                        AwayScore1.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore1.Text) > CInt(HomeScore1.Text) Then
                        HomeScore1.ForeColor = LosingTeamForeColor
                        AwayScore1.ForeColor = WinningTeamForeColor

                        HomeTeam1Image.BackColor = LosingTeamBackColor
                        AwayTeam1Image.BackColor = WinningTeamBackColor
                        HomeScore1.BackColor = LosingTeamBackColor
                        AwayScore1.BackColor = WinningTeamBackColor

                    End If

                ElseIf game.Key = "game2" Then
                    GameNumber2.Text = game.Key
                    HomeTeam2Image1.ImageUrl = HomeTeamImage
                    AwayTeam2Image1.ImageUrl = AwayTeamImage
                    HomeTeam2Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam2Icon1.ImageUrl = AwayTeamIcon

                    HomeScore2.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore2.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber2Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore2.Text) > CInt(AwayScore2.Text) Then
                        HomeScore2.ForeColor = WinningTeamForeColor
                        AwayScore2.ForeColor = LosingTeamForeColor

                        HomeTeam2Image.BackColor = WinningTeamBackColor
                        AwayTeam2Image.BackColor = LosingTeamBackColor
                        HomeScore2.BackColor = WinningTeamBackColor
                        AwayScore2.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore2.Text) > CInt(HomeScore2.Text) Then
                        HomeScore2.ForeColor = LosingTeamForeColor
                        AwayScore2.ForeColor = WinningTeamForeColor

                        HomeTeam2Image.BackColor = LosingTeamBackColor
                        AwayTeam2Image.BackColor = WinningTeamBackColor
                        HomeScore2.BackColor = LosingTeamBackColor
                        AwayScore2.BackColor = WinningTeamBackColor

                    End If


                ElseIf game.Key = "game3" Then
                    GameNumber3.Text = game.Key
                    HomeTeam3Image1.ImageUrl = HomeTeamImage
                    AwayTeam3Image1.ImageUrl = AwayTeamImage
                    HomeTeam3Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam3Icon1.ImageUrl = AwayTeamIcon
                    HomeScore3.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore3.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber3Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore3.Text) > CInt(AwayScore3.Text) Then
                        HomeScore3.ForeColor = WinningTeamForeColor
                        AwayScore3.ForeColor = LosingTeamForeColor

                        HomeTeam3Image.BackColor = WinningTeamBackColor
                        AwayTeam3Image.BackColor = LosingTeamBackColor
                        HomeScore3.BackColor = WinningTeamBackColor
                        AwayScore3.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore3.Text) > CInt(HomeScore3.Text) Then
                        HomeScore3.ForeColor = LosingTeamForeColor
                        AwayScore3.ForeColor = WinningTeamForeColor

                        HomeTeam3Image.BackColor = LosingTeamBackColor
                        AwayTeam3Image.BackColor = WinningTeamBackColor
                        HomeScore3.BackColor = LosingTeamBackColor
                        AwayScore3.BackColor = WinningTeamBackColor

                    End If


                ElseIf game.Key = "game4" Then
                    GameNumber4.Text = game.Key
                    HomeTeam4Image1.ImageUrl = HomeTeamImage
                    AwayTeam4Image1.ImageUrl = AwayTeamImage
                    HomeTeam4Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam4Icon1.ImageUrl = AwayTeamIcon
                    HomeScore4.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore4.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber4Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore4.Text) > CInt(AwayScore4.Text) Then
                        HomeScore4.ForeColor = WinningTeamForeColor
                        AwayScore4.ForeColor = LosingTeamForeColor

                        HomeTeam4Image.BackColor = WinningTeamBackColor
                        AwayTeam4Image.BackColor = LosingTeamBackColor
                        HomeScore4.BackColor = WinningTeamBackColor
                        AwayScore4.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore4.Text) > CInt(HomeScore4.Text) Then
                        HomeScore4.ForeColor = LosingTeamForeColor
                        AwayScore4.ForeColor = WinningTeamForeColor

                        HomeTeam4Image.BackColor = LosingTeamBackColor
                        AwayTeam4Image.BackColor = WinningTeamBackColor
                        HomeScore4.BackColor = LosingTeamBackColor
                        AwayScore4.BackColor = WinningTeamBackColor

                    End If
                ElseIf game.Key = "game5" Then
                    GameNumber5.Text = game.Key
                    HomeTeam5Image1.ImageUrl = HomeTeamImage
                    AwayTeam5Image1.ImageUrl = AwayTeamImage
                    HomeTeam5Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam5Icon1.ImageUrl = AwayTeamIcon
                    HomeScore5.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore5.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber5Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore5.Text) > CInt(AwayScore5.Text) Then
                        HomeScore5.ForeColor = WinningTeamForeColor
                        AwayScore5.ForeColor = LosingTeamForeColor

                        HomeTeam5Image.BackColor = WinningTeamBackColor
                        AwayTeam5Image.BackColor = LosingTeamBackColor
                        HomeScore5.BackColor = WinningTeamBackColor
                        AwayScore5.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore5.Text) > CInt(HomeScore5.Text) Then
                        HomeScore5.ForeColor = LosingTeamForeColor
                        AwayScore5.ForeColor = WinningTeamForeColor

                        HomeTeam5Image.BackColor = LosingTeamBackColor
                        AwayTeam5Image.BackColor = WinningTeamBackColor
                        HomeScore5.BackColor = LosingTeamBackColor
                        AwayScore5.BackColor = WinningTeamBackColor

                    End If

                ElseIf game.Key = "game6" Then
                    GameNumber6.Text = game.Key
                    HomeTeam6Image1.ImageUrl = HomeTeamImage
                    AwayTeam6Image1.ImageUrl = AwayTeamImage
                    HomeTeam6Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam6Icon1.ImageUrl = AwayTeamIcon
                    HomeScore6.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore6.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber6Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore6.Text) > CInt(AwayScore6.Text) Then
                        HomeScore6.ForeColor = WinningTeamForeColor
                        AwayScore6.ForeColor = LosingTeamForeColor

                        HomeTeam6Image.BackColor = WinningTeamBackColor
                        AwayTeam6Image.BackColor = LosingTeamBackColor
                        HomeScore6.BackColor = WinningTeamBackColor
                        AwayScore6.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore6.Text) > CInt(HomeScore6.Text) Then
                        HomeScore6.ForeColor = LosingTeamForeColor
                        AwayScore6.ForeColor = WinningTeamForeColor

                        HomeTeam6Image.BackColor = LosingTeamBackColor
                        AwayTeam6Image.BackColor = WinningTeamBackColor
                        HomeScore6.BackColor = LosingTeamBackColor
                        AwayScore6.BackColor = WinningTeamBackColor

                    End If

                ElseIf game.Key = "game7" Then
                    GameNumber7.Text = game.Key
                    HomeTeam7Image1.ImageUrl = HomeTeamImage
                    AwayTeam7Image1.ImageUrl = AwayTeamImage
                    HomeTeam7Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam7Icon1.ImageUrl = AwayTeamIcon
                    HomeScore7.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore7.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber7Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore7.Text) > CInt(AwayScore7.Text) Then
                        HomeScore7.ForeColor = WinningTeamForeColor
                        AwayScore7.ForeColor = LosingTeamForeColor

                        HomeTeam7Image.BackColor = WinningTeamBackColor
                        AwayTeam7Image.BackColor = LosingTeamBackColor
                        HomeScore7.BackColor = WinningTeamBackColor
                        AwayScore7.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore7.Text) > CInt(HomeScore7.Text) Then
                        HomeScore7.ForeColor = LosingTeamForeColor
                        AwayScore7.ForeColor = WinningTeamForeColor

                        HomeTeam7Image.BackColor = LosingTeamBackColor
                        AwayTeam7Image.BackColor = WinningTeamBackColor
                        HomeScore7.BackColor = LosingTeamBackColor
                        AwayScore7.BackColor = WinningTeamBackColor

                    End If


                ElseIf game.Key = "game8" Then
                    GameNumber8.Text = game.Key
                    HomeTeam8Image1.ImageUrl = HomeTeamImage
                    AwayTeam8Image1.ImageUrl = AwayTeamImage
                    HomeTeam8Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam8Icon1.ImageUrl = AwayTeamIcon
                    HomeScore8.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore8.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber8Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore8.Text) > CInt(AwayScore8.Text) Then
                        HomeScore8.ForeColor = WinningTeamForeColor
                        AwayScore8.ForeColor = LosingTeamForeColor

                        HomeTeam8Image.BackColor = WinningTeamBackColor
                        AwayTeam8Image.BackColor = LosingTeamBackColor
                        HomeScore8.BackColor = WinningTeamBackColor
                        AwayScore8.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore8.Text) > CInt(HomeScore8.Text) Then
                        HomeScore8.ForeColor = LosingTeamForeColor
                        AwayScore8.ForeColor = WinningTeamForeColor

                        HomeTeam8Image.BackColor = LosingTeamBackColor
                        AwayTeam8Image.BackColor = WinningTeamBackColor
                        HomeScore8.BackColor = LosingTeamBackColor
                        AwayScore8.BackColor = WinningTeamBackColor

                    End If
                ElseIf game.Key = "game9" Then
                    GameNumber9.Text = game.Key
                    HomeTeam9Image1.ImageUrl = HomeTeamImage
                    AwayTeam9Image1.ImageUrl = AwayTeamImage
                    HomeTeam9Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam9Icon1.ImageUrl = AwayTeamIcon
                    HomeScore9.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore9.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber9Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore9.Text) > CInt(AwayScore9.Text) Then
                        HomeScore9.ForeColor = WinningTeamForeColor
                        AwayScore9.ForeColor = LosingTeamForeColor

                        HomeTeam9Image.BackColor = WinningTeamBackColor
                        AwayTeam9Image.BackColor = LosingTeamBackColor
                        HomeScore9.BackColor = WinningTeamBackColor
                        AwayScore9.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore9.Text) > CInt(HomeScore9.Text) Then
                        HomeScore9.ForeColor = LosingTeamForeColor
                        AwayScore9.ForeColor = WinningTeamForeColor

                        HomeTeam9Image.BackColor = LosingTeamBackColor
                        AwayTeam9Image.BackColor = WinningTeamBackColor
                        HomeScore9.BackColor = LosingTeamBackColor
                        AwayScore9.BackColor = WinningTeamBackColor

                    End If

                ElseIf game.Key = "game10" Then
                    GameNumber10.Text = game.Key
                    HomeTeam10Image1.ImageUrl = HomeTeamImage
                    AwayTeam10Image1.ImageUrl = AwayTeamImage
                    HomeTeam10Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam10Icon1.ImageUrl = AwayTeamIcon
                    HomeScore10.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore10.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber10Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore10.Text) > CInt(AwayScore10.Text) Then
                        HomeScore10.ForeColor = WinningTeamForeColor
                        AwayScore10.ForeColor = LosingTeamForeColor

                        HomeTeam10Image.BackColor = WinningTeamBackColor
                        AwayTeam10Image.BackColor = LosingTeamBackColor
                        HomeScore10.BackColor = WinningTeamBackColor
                        AwayScore10.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore10.Text) > CInt(HomeScore10.Text) Then
                        HomeScore10.ForeColor = LosingTeamForeColor
                        AwayScore10.ForeColor = WinningTeamForeColor

                        HomeTeam10Image.BackColor = LosingTeamBackColor
                        AwayTeam10Image.BackColor = WinningTeamBackColor
                        HomeScore10.BackColor = LosingTeamBackColor
                        AwayScore10.BackColor = WinningTeamBackColor

                    End If


                ElseIf game.Key = "game11" Then
                    GameNumber11.Text = game.Key
                    HomeTeam11Image1.ImageUrl = HomeTeamImage
                    AwayTeam11Image1.ImageUrl = AwayTeamImage
                    HomeTeam11Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam11Icon1.ImageUrl = AwayTeamIcon
                    HomeScore11.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore11.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber11Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore11.Text) > CInt(AwayScore11.Text) Then
                        HomeScore11.ForeColor = WinningTeamForeColor
                        AwayScore11.ForeColor = LosingTeamForeColor

                        HomeTeam11Image.BackColor = WinningTeamBackColor
                        AwayTeam11Image.BackColor = LosingTeamBackColor
                        HomeScore11.BackColor = WinningTeamBackColor
                        AwayScore11.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore11.Text) > CInt(HomeScore11.Text) Then
                        HomeScore11.ForeColor = LosingTeamForeColor
                        AwayScore11.ForeColor = WinningTeamForeColor

                        HomeTeam11Image.BackColor = LosingTeamBackColor
                        AwayTeam11Image.BackColor = WinningTeamBackColor
                        HomeScore11.BackColor = LosingTeamBackColor
                        AwayScore11.BackColor = WinningTeamBackColor

                    End If


                ElseIf game.Key = "game12" Then
                    GameNumber12.Text = game.Key
                    HomeTeam12Image1.ImageUrl = HomeTeamImage
                    AwayTeam12Image1.ImageUrl = AwayTeamImage
                    HomeTeam12Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam12Icon1.ImageUrl = AwayTeamIcon
                    HomeScore12.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore12.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber12Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore12.Text) > CInt(AwayScore12.Text) Then
                        HomeScore12.ForeColor = WinningTeamForeColor
                        AwayScore12.ForeColor = LosingTeamForeColor

                        HomeTeam12Image.BackColor = WinningTeamBackColor
                        AwayTeam12Image.BackColor = LosingTeamBackColor
                        HomeScore12.BackColor = WinningTeamBackColor
                        AwayScore12.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore12.Text) > CInt(HomeScore12.Text) Then
                        HomeScore12.ForeColor = LosingTeamForeColor
                        AwayScore12.ForeColor = WinningTeamForeColor

                        HomeTeam12Image.BackColor = LosingTeamBackColor
                        AwayTeam12Image.BackColor = WinningTeamBackColor
                        HomeScore12.BackColor = LosingTeamBackColor
                        AwayScore12.BackColor = WinningTeamBackColor

                    End If
                ElseIf game.Key = "game13" Then
                    GameNumber13.Text = game.Key
                    HomeTeam13Image1.ImageUrl = HomeTeamImage
                    AwayTeam13Image1.ImageUrl = AwayTeamImage
                    HomeTeam13Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam13Icon1.ImageUrl = AwayTeamIcon
                    HomeScore13.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore13.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber13Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore13.Text) > CInt(AwayScore13.Text) Then
                        HomeScore13.ForeColor = WinningTeamForeColor
                        AwayScore13.ForeColor = LosingTeamForeColor

                        HomeTeam13Image.BackColor = WinningTeamBackColor
                        AwayTeam13Image.BackColor = LosingTeamBackColor
                        HomeScore13.BackColor = WinningTeamBackColor
                        AwayScore13.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore13.Text) > CInt(HomeScore13.Text) Then
                        HomeScore13.ForeColor = LosingTeamForeColor
                        AwayScore13.ForeColor = WinningTeamForeColor

                        HomeTeam13Image.BackColor = LosingTeamBackColor
                        AwayTeam13Image.BackColor = WinningTeamBackColor
                        HomeScore13.BackColor = LosingTeamBackColor
                        AwayScore13.BackColor = WinningTeamBackColor

                    End If

                ElseIf game.Key = "game14" Then
                    GameNumber14.Text = game.Key
                    HomeTeam14Image1.ImageUrl = HomeTeamImage
                    AwayTeam14Image1.ImageUrl = AwayTeamImage
                    HomeTeam14Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam14Icon1.ImageUrl = AwayTeamIcon
                    HomeScore14.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore14.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber14Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore14.Text) > CInt(AwayScore14.Text) Then
                        HomeScore14.ForeColor = WinningTeamForeColor
                        AwayScore14.ForeColor = LosingTeamForeColor

                        HomeTeam14Image.BackColor = WinningTeamBackColor
                        AwayTeam14Image.BackColor = LosingTeamBackColor
                        HomeScore14.BackColor = WinningTeamBackColor
                        AwayScore14.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore14.Text) > CInt(HomeScore14.Text) Then
                        HomeScore14.ForeColor = LosingTeamForeColor
                        AwayScore14.ForeColor = WinningTeamForeColor

                        HomeTeam14Image.BackColor = LosingTeamBackColor
                        AwayTeam14Image.BackColor = WinningTeamBackColor
                        HomeScore14.BackColor = LosingTeamBackColor
                        AwayScore14.BackColor = WinningTeamBackColor

                    End If


                ElseIf game.Key = "game15" Then
                    GameNumber15.Text = game.Key
                    HomeTeam15Image1.ImageUrl = HomeTeamImage
                    AwayTeam15Image1.ImageUrl = AwayTeamImage
                    HomeTeam15Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam15Icon1.ImageUrl = AwayTeamIcon
                    HomeScore15.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore15.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber15Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore15.Text) > CInt(AwayScore15.Text) Then
                        HomeScore15.ForeColor = WinningTeamForeColor
                        AwayScore15.ForeColor = LosingTeamForeColor

                        HomeTeam15Image.BackColor = WinningTeamBackColor
                        AwayTeam15Image.BackColor = LosingTeamBackColor
                        HomeScore15.BackColor = WinningTeamBackColor
                        AwayScore15.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore15.Text) > CInt(HomeScore15.Text) Then
                        HomeScore15.ForeColor = LosingTeamForeColor
                        AwayScore15.ForeColor = WinningTeamForeColor

                        HomeTeam15Image.BackColor = LosingTeamBackColor
                        AwayTeam15Image.BackColor = WinningTeamBackColor
                        HomeScore15.BackColor = LosingTeamBackColor
                        AwayScore15.BackColor = WinningTeamBackColor

                    End If

                ElseIf game.Key = "game16" Then
                    GameNumber16.Text = game.Key
                    HomeTeam16Image1.ImageUrl = HomeTeamImage
                    AwayTeam16Image1.ImageUrl = AwayTeamImage
                    HomeTeam16Icon1.ImageUrl = HomeTeamIcon
                    AwayTeam16Icon1.ImageUrl = AwayTeamIcon
                    HomeScore16.Text = GameUpdateCollection(game.Key).HomeScore
                    AwayScore16.Text = GameUpdateCollection(game.Key).AwayScore
                    GameNumber16Status.Text = GameUpdateCollection(game.Key).Status

                    If CInt(HomeScore16.Text) > CInt(AwayScore16.Text) Then
                        HomeScore16.ForeColor = WinningTeamForeColor
                        AwayScore16.ForeColor = LosingTeamForeColor

                        HomeTeam16Image.BackColor = WinningTeamBackColor
                        AwayTeam16Image.BackColor = LosingTeamBackColor
                        HomeScore16.BackColor = WinningTeamBackColor
                        AwayScore16.BackColor = LosingTeamBackColor
                    ElseIf CInt(AwayScore16.Text) > CInt(HomeScore16.Text) Then
                        HomeScore16.ForeColor = LosingTeamForeColor
                        AwayScore16.ForeColor = WinningTeamForeColor

                        HomeTeam16Image.BackColor = LosingTeamBackColor
                        AwayTeam16Image.BackColor = WinningTeamBackColor
                        HomeScore16.BackColor = LosingTeamBackColor
                        AwayScore16.BackColor = WinningTeamBackColor

                    End If

                End If
            Next
        End If
    End Sub

    Private Function GetUserStatusCollection(UserStatusCollection As Dictionary(Of String, UserStatus), thisTimePeriod As String, poolAlias As String, cronJobName As String) As Dictionary(Of String, UserStatus)

        Dim _dbLoserPool5 As New LosersPoolContext

        Try
            Using (_dbLoserPool5)

                Dim queryUserChoices = (From qUC1 In _dbLoserPool5.UserChoicesList
                                        Where qUC1.CronJob = cronJobName And qUC1.PoolAlias = poolAlias And _
                                        qUC1.TimePeriod = thisTimePeriod And qUC1.Contender = True).ToList

                For Each user1 In queryUserChoices


                    Dim user1status As New UserStatus
                    user1status.UserName = user1.UserName
                    user1status.IsUserWinning = user1.UserIsWinning
                    user1status.IsUserTied = user1.UserIsTied

                    If user1.UserIsTied = True Then
                        user1status.UserColor = Drawing.Color.Black
                    ElseIf user1.UserIsWinning = True Then
                        user1status.UserColor = WinningTeamForeColor
                    ElseIf user1.UserIsWinning = False Then
                        user1status.UserColor = LosingTeamForeColor
                    End If

                    UserStatusCollection.Add(user1.UserName, user1status)

                Next


            End Using
        Catch ex As Exception

        End Try


        Return UserStatusCollection

    End Function

    Private Function SortGameUpdateCollection(GameUpdateCollection As Dictionary(Of String, GameUpdate), GameUpdateCollectionSorted As Dictionary(Of String, GameUpdate)) As Dictionary(Of String, GameUpdate)

        For Each game1 In GameUpdateCollection
            Dim game2 As New GameUpdate
            game2.GameId = game1.Value.GameId
            game2.GameTime = game1.Value.GameTime
            game2.HomeTeam = game1.Value.HomeTeam
            game2.HomeScore = game1.Value.HomeScore
            game2.HomeTeamAvailability = game1.Value.HomeTeamAvailability
            game2.AwayTeam = game1.Value.AwayTeam
            game2.AwayScore = game1.Value.AwayScore
            game2.AwayTeamAvailability = game1.Value.AwayTeamAvailability
            game2.UserHandles.Clear()
            If game1.Key = "game1" Then
                For Each user1Status In UserStatusCollection
                    If user1Status.Value.IsUserWinning = True And user1Status.Value.IsUserTied = False Then
                        game2.UserHandles.Add(user1Status.Key)
                    End If
                Next
                For Each user1Status In UserStatusCollection
                    If user1Status.Value.IsUserTied = True And user1Status.Value.IsUserWinning = False Then
                        game2.UserHandles.Add(user1Status.Key)
                    End If
                Next
                For Each user1Status In UserStatusCollection
                    If user1Status.Value.IsUserWinning = False And user1Status.Value.IsUserTied = False Then
                        game2.UserHandles.Add(user1Status.Key)
                    End If
                Next
            End If
            GameUpdateCollectionSorted.Add(game2.GameId, game2)
        Next

        Return GameUpdateCollectionSorted

    End Function

    Private Sub AddUserStatusToScoringUpdateTable(GameUpdateCollectionSorted As Dictionary(Of String, GameUpdate), UserStatusCollection As Dictionary(Of String, UserStatus), thisTimePeriod As String, poolAlias As String, cronJobName As String)

        If Not Page.IsPostBack Then

            Dim _dbLoserPool2 As New LosersPoolContext

            Try
                Using (_dbLoserPool2)
                    Dim cnt = 0
                    For Each user1 In GameUpdateCollectionSorted("game1").UserHandles

                        Dim IsAdminPick = (From user1a In _dbLoserPool2.UserChoicesList
                                           Where user1a.UserName = user1 And user1a.TimePeriod = thisTimePeriod And user1a.PoolAlias = poolAlias
                                           Select user1a.AdministrationPick).Single

                        If GameUpdateCollectionSorted.Count <= 1 Then
                            GameNumber2.Visible = False
                            GameNumber2Status.Visible = False
                            HomeTeam2Image.Visible = False
                            HomeTeam2Icon.Visible = False
                            AwayTeam2Image.Visible = False
                            AwayTeam2Icon.Visible = False
                            HomeScore2.Visible = False
                            AwayScore2.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 2 Then
                            GameNumber3.Visible = False
                            GameNumber3Status.Visible = False
                            HomeTeam3Image.Visible = False
                            HomeTeam3Icon.Visible = False
                            AwayTeam3Image.Visible = False
                            AwayTeam3Icon.Visible = False
                            HomeScore3.Visible = False
                            AwayScore3.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 3 Then
                            GameNumber4.Visible = False
                            GameNumber4Status.Visible = False
                            HomeTeam4Image.Visible = False
                            HomeTeam4Icon.Visible = False
                            AwayTeam4Image.Visible = False
                            AwayTeam4Icon.Visible = False
                            HomeScore4.Visible = False
                            AwayScore4.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 4 Then
                            GameNumber5.Visible = False
                            GameNumber5Status.Visible = False
                            HomeTeam5Image.Visible = False
                            HomeTeam5Icon.Visible = False
                            AwayTeam5Image.Visible = False
                            AwayTeam5Icon.Visible = False
                            HomeScore5.Visible = False
                            AwayScore5.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 5 Then
                            GameNumber6.Visible = False
                            GameNumber6Status.Visible = False
                            HomeTeam6Image.Visible = False
                            HomeTeam6Icon.Visible = False
                            AwayTeam6Image.Visible = False
                            AwayTeam6Icon.Visible = False
                            HomeScore6.Visible = False
                            AwayScore6.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 6 Then
                            GameNumber7.Visible = False
                            GameNumber7Status.Visible = False
                            HomeTeam7Image.Visible = False
                            HomeTeam7Icon.Visible = False
                            AwayTeam7Image.Visible = False
                            AwayTeam7Icon.Visible = False
                            HomeScore7.Visible = False
                            AwayScore7.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 7 Then
                            GameNumber8.Visible = False
                            GameNumber8Status.Visible = False
                            HomeTeam8Image.Visible = False
                            HomeTeam8Icon.Visible = False
                            AwayTeam8Image.Visible = False
                            AwayTeam8Icon.Visible = False
                            HomeScore8.Visible = False
                            AwayScore8.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 8 Then
                            GameNumber9.Visible = False
                            GameNumber9Status.Visible = False
                            HomeTeam9Image.Visible = False
                            HomeTeam9Icon.Visible = False
                            AwayTeam9Image.Visible = False
                            AwayTeam9Icon.Visible = False
                            HomeScore9.Visible = False
                            AwayScore9.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 9 Then
                            GameNumber10.Visible = False
                            GameNumber10Status.Visible = False
                            HomeTeam10Image.Visible = False
                            HomeTeam10Icon.Visible = False
                            AwayTeam10Image.Visible = False
                            AwayTeam10Icon.Visible = False
                            HomeScore10.Visible = False
                            AwayScore10.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 10 Then
                            GameNumber11.Visible = False
                            GameNumber11Status.Visible = False
                            HomeTeam11Image.Visible = False
                            HomeTeam11Icon.Visible = False
                            AwayTeam11Image.Visible = False
                            AwayTeam11Icon.Visible = False
                            HomeScore11.Visible = False
                            AwayScore11.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 11 Then
                            GameNumber12.Visible = False
                            GameNumber12Status.Visible = False
                            HomeTeam12Image.Visible = False
                            HomeTeam12Icon.Visible = False
                            AwayTeam12Image.Visible = False
                            AwayTeam12Icon.Visible = False
                            HomeScore12.Visible = False
                            AwayScore12.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 12 Then
                            GameNumber13.Visible = False
                            GameNumber13Status.Visible = False
                            HomeTeam13Image.Visible = False
                            HomeTeam13Icon.Visible = False
                            AwayTeam13Image.Visible = False
                            AwayTeam13Icon.Visible = False
                            HomeScore13.Visible = False
                            AwayScore13.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 13 Then
                            GameNumber14.Visible = False
                            GameNumber14Status.Visible = False
                            HomeTeam14Image.Visible = False
                            HomeTeam14Icon.Visible = False
                            AwayTeam14Image.Visible = False
                            AwayTeam14Icon.Visible = False
                            HomeScore14.Visible = False
                            AwayScore14.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 14 Then
                            GameNumber15.Visible = False
                            GameNumber15Status.Visible = False
                            HomeTeam15Image.Visible = False
                            HomeTeam15Icon.Visible = False
                            AwayTeam15Image.Visible = False
                            AwayTeam15Icon.Visible = False
                            HomeScore15.Visible = False
                            AwayScore15.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 15 Then
                            GameNumber16.Visible = False
                            GameNumber16Status.Visible = False
                            HomeTeam16Image.Visible = False
                            HomeTeam16Icon.Visible = False
                            AwayTeam16Image.Visible = False
                            AwayTeam16Icon.Visible = False
                            HomeScore16.Visible = False
                            AwayScore16.Visible = False
                        End If

                        Dim dRow As New TableRow

                        If GameUpdateCollectionSorted.Count >= 1 Then

                            Dim dCell1 As New TableCell
                            dCell1.Text = GameUpdateCollectionSorted("game1").UserHandles(cnt)
                            dCell1.ForeColor = UserStatusCollection(user1).UserColor
                            dCell1.Font.Bold = True
                            dCell1.CssClass = "UserName1"

                            If IsAdminPick Then
                                dCell1.BackColor = AdminBackColor
                            End If

                            dRow.Cells.Add(dCell1)


                            Dim dCell2 As New TableCell
                            dCell2.Text = GameUpdateCollectionSorted("game1").HomeTeamAvailability(user1)

                            If dCell2.Text = "L" Or dCell2.Text = "P" Then
                                dCell2.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell2.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell2.BackColor = LosingTeamBackColor
                                End If

                                dCell2.Font.Bold = True

                                If IsAdminPick Then
                                    dCell2.BackColor = AdminBackColor
                                End If

                            End If

                            dRow.Cells.Add(dCell2)

                            Dim dCell3 As New TableCell
                            dCell3.Text = GameUpdateCollectionSorted("game1").AwayTeamAvailability(user1)

                            If dCell3.Text = "L" Or dCell3.Text = "P" Then
                                dCell3.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell3.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell3.BackColor = LosingTeamBackColor
                                End If

                                dCell3.Font.Bold = True

                                If IsAdminPick Then
                                    dCell3.BackColor = AdminBackColor
                                End If

                            End If

                            dRow.Cells.Add(dCell3)

                        End If

                        If GameUpdateCollectionSorted.Count >= 2 Then
                            Dim dCell4 As New TableCell
                            dCell4.Text = GameUpdateCollectionSorted("game2").HomeTeamAvailability(user1)

                            If dCell4.Text = "L" Or dCell4.Text = "P" Then
                                dCell4.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell4.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell4.BackColor = LosingTeamBackColor
                                End If

                                dCell4.Font.Bold = True

                                If IsAdminPick Then
                                    dCell4.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell4)

                            Dim dCell5 As New TableCell
                            dCell5.Text = GameUpdateCollectionSorted("game2").AwayTeamAvailability(user1)

                            If dCell5.Text = "L" Or dCell5.Text = "P" Then
                                dCell5.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell5.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell5.BackColor = LosingTeamBackColor
                                End If

                                dCell5.Font.Bold = True

                                If IsAdminPick Then
                                    dCell5.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell5)

                        End If

                        If GameUpdateCollectionSorted.Count >= 3 Then

                            Dim dCell6 As New TableCell
                            dCell6.Text = GameUpdateCollectionSorted("game3").HomeTeamAvailability(user1)

                            If dCell6.Text = "L" Or dCell6.Text = "P" Then
                                dCell6.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell6.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell6.BackColor = LosingTeamBackColor
                                End If

                                dCell6.Font.Bold = True

                                If IsAdminPick Then
                                    dCell6.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell6)

                            Dim dCell7 As New TableCell
                            dCell7.Text = GameUpdateCollectionSorted("game3").AwayTeamAvailability(user1)

                            If dCell7.Text = "L" Or dCell7.Text = "P" Then
                                dCell7.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell7.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell7.BackColor = LosingTeamBackColor
                                End If

                                dCell7.Font.Bold = True

                                If IsAdminPick Then
                                    dCell7.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell7)

                        End If

                        If GameUpdateCollectionSorted.Count >= 4 Then
                            Dim dCell8 As New TableCell
                            dCell8.Text = GameUpdateCollectionSorted("game4").HomeTeamAvailability(user1)

                            If dCell8.Text = "L" Or dCell8.Text = "P" Then
                                dCell8.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell8.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell8.BackColor = LosingTeamBackColor
                                End If

                                dCell8.Font.Bold = True

                                If IsAdminPick Then
                                    dCell8.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell8)

                            Dim dCell9 As New TableCell
                            dCell9.Text = GameUpdateCollectionSorted("game4").AwayTeamAvailability(user1)

                            If dCell9.Text = "L" Or dCell9.Text = "P" Then
                                dCell9.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell9.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell9.BackColor = LosingTeamBackColor
                                End If

                                dCell9.Font.Bold = True

                                If IsAdminPick Then
                                    dCell9.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell9)

                        End If

                        If GameUpdateCollectionSorted.Count >= 5 Then


                            Dim dCell10 As New TableCell
                            dCell10.Text = GameUpdateCollectionSorted("game5").HomeTeamAvailability(user1)

                            If dCell10.Text = "L" Or dCell10.Text = "P" Then
                                dCell10.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell10.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell10.BackColor = LosingTeamBackColor
                                End If

                                dCell10.Font.Bold = True

                                If IsAdminPick Then
                                    dCell10.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell10)

                            Dim dCell11 As New TableCell
                            dCell11.Text = GameUpdateCollectionSorted("game5").AwayTeamAvailability(user1)

                            If dCell11.Text = "L" Or dCell11.Text = "P" Then
                                dCell11.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell11.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell11.BackColor = LosingTeamBackColor
                                End If

                                dCell11.Font.Bold = True

                                If IsAdminPick Then
                                    dCell11.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell11)

                        End If

                        If GameUpdateCollectionSorted.Count >= 6 Then
                            Dim dCell12 As New TableCell
                            dCell12.Text = GameUpdateCollectionSorted("game6").HomeTeamAvailability(user1)

                            If dCell12.Text = "L" Or dCell12.Text = "P" Then
                                dCell12.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell12.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell12.BackColor = LosingTeamBackColor
                                End If

                                dCell12.Font.Bold = True

                                If IsAdminPick Then
                                    dCell12.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell12)

                            Dim dCell13 As New TableCell
                            dCell13.Text = GameUpdateCollectionSorted("game6").AwayTeamAvailability(user1)

                            If dCell13.Text = "L" Or dCell13.Text = "P" Then
                                dCell13.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell13.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell13.BackColor = LosingTeamBackColor
                                End If

                                dCell13.Font.Bold = True

                                If IsAdminPick Then
                                    dCell13.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell13)

                        End If

                        If GameUpdateCollectionSorted.Count >= 7 Then

                            Dim dCell14 As New TableCell
                            dCell14.Text = GameUpdateCollectionSorted("game7").HomeTeamAvailability(user1)
                            If dCell14.Text = "L" Or dCell14.Text = "P" Then
                                dCell14.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell14.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell14.BackColor = LosingTeamBackColor
                                End If

                                dCell14.Font.Bold = True

                                If IsAdminPick Then
                                    dCell14.BackColor = AdminBackColor
                                End If

                            End If

                            dRow.Cells.Add(dCell14)

                            Dim dCell15 As New TableCell
                            dCell15.Text = GameUpdateCollectionSorted("game7").AwayTeamAvailability(user1)

                            If dCell15.Text = "L" Or dCell15.Text = "P" Then
                                dCell15.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell15.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell15.BackColor = LosingTeamBackColor
                                End If

                                dCell15.Font.Bold = True

                                If IsAdminPick Then
                                    dCell15.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell15)

                        End If

                        If GameUpdateCollectionSorted.Count >= 8 Then
                            Dim dCell16 As New TableCell
                            dCell16.Text = GameUpdateCollectionSorted("game8").HomeTeamAvailability(user1)

                            If dCell16.Text = "L" Or dCell16.Text = "P" Then
                                dCell16.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell16.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell16.BackColor = LosingTeamBackColor
                                End If

                                dCell16.Font.Bold = True

                                If IsAdminPick Then
                                    dCell16.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell16)

                            Dim dCell17 As New TableCell
                            dCell17.Text = GameUpdateCollectionSorted("game8").AwayTeamAvailability(user1)

                            If dCell17.Text = "L" Or dCell17.Text = "P" Then
                                dCell17.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell17.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell17.BackColor = LosingTeamBackColor
                                End If

                                dCell17.Font.Bold = True

                                If IsAdminPick Then
                                    dCell17.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell17)

                        End If

                        If GameUpdateCollectionSorted.Count >= 9 Then

                            Dim dCell18 As New TableCell
                            dCell18.Text = GameUpdateCollectionSorted("game9").HomeTeamAvailability(user1)

                            If dCell18.Text = "L" Or dCell18.Text = "P" Then
                                dCell18.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell18.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell18.BackColor = LosingTeamBackColor
                                End If

                                dCell18.Font.Bold = True

                                If IsAdminPick Then
                                    dCell18.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell18)

                            Dim dCell19 As New TableCell
                            dCell19.Text = GameUpdateCollectionSorted("game9").AwayTeamAvailability(user1)

                            If dCell19.Text = "L" Or dCell19.Text = "P" Then
                                dCell19.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell19.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell19.BackColor = LosingTeamBackColor
                                End If

                                dCell19.Font.Bold = True
                                If IsAdminPick Then
                                    dCell19.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell19)
                        End If

                        If GameUpdateCollectionSorted.Count >= 10 Then
                            Dim dCell20 As New TableCell
                            dCell20.Text = GameUpdateCollectionSorted("game10").HomeTeamAvailability(user1)

                            If dCell20.Text = "L" Or dCell20.Text = "P" Then
                                dCell20.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell20.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell20.BackColor = LosingTeamBackColor
                                End If

                                dCell20.Font.Bold = True

                                If IsAdminPick Then
                                    dCell20.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell20)

                            Dim dCell21 As New TableCell
                            dCell21.Text = GameUpdateCollectionSorted("game10").AwayTeamAvailability(user1)

                            If dCell21.Text = "L" Or dCell21.Text = "P" Then
                                dCell21.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell21.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell21.BackColor = LosingTeamBackColor
                                End If

                                dCell21.Font.Bold = True

                                If IsAdminPick Then
                                    dCell21.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell21)
                        End If

                        If GameUpdateCollectionSorted.Count >= 11 Then

                            Dim dCell22 As New TableCell
                            dCell22.Text = GameUpdateCollectionSorted("game11").HomeTeamAvailability(user1)

                            If dCell22.Text = "L" Or dCell22.Text = "P" Then
                                dCell22.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell22.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell22.BackColor = LosingTeamBackColor
                                End If

                                dCell22.Font.Bold = True

                                If IsAdminPick Then
                                    dCell22.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell22)

                            Dim dCell23 As New TableCell
                            dCell23.Text = GameUpdateCollectionSorted("game11").AwayTeamAvailability(user1)

                            If dCell23.Text = "L" Or dCell23.Text = "P" Then
                                dCell23.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell23.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell23.BackColor = LosingTeamBackColor
                                End If

                                dCell23.Font.Bold = True

                                If IsAdminPick Then
                                    dCell23.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell23)

                        End If

                        If GameUpdateCollectionSorted.Count >= 12 Then
                            Dim dCell24 As New TableCell
                            dCell24.Text = GameUpdateCollectionSorted("game12").HomeTeamAvailability(user1)

                            If dCell24.Text = "L" Or dCell24.Text = "P" Then
                                dCell24.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell24.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell24.BackColor = LosingTeamBackColor
                                End If

                                dCell24.Font.Bold = True

                                If IsAdminPick Then
                                    dCell24.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell24)

                            Dim dCell25 As New TableCell
                            dCell25.Text = GameUpdateCollectionSorted("game12").AwayTeamAvailability(user1)

                            If dCell25.Text = "L" Or dCell25.Text = "P" Then
                                dCell25.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell25.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell25.BackColor = LosingTeamBackColor
                                End If

                                dCell25.Font.Bold = True

                                If IsAdminPick Then
                                    dCell25.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell25)

                        End If

                        If GameUpdateCollectionSorted.Count >= 13 Then

                            Dim dCell26 As New TableCell
                            dCell26.Text = GameUpdateCollectionSorted("game13").HomeTeamAvailability(user1)

                            If dCell26.Text = "L" Or dCell26.Text = "P" Then
                                dCell26.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell26.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell26.BackColor = LosingTeamBackColor
                                End If

                                dCell26.Font.Bold = True

                                If IsAdminPick Then
                                    dCell26.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell26)

                            Dim dCell27 As New TableCell
                            dCell27.Text = GameUpdateCollectionSorted("game13").AwayTeamAvailability(user1)

                            If dCell27.Text = "L" Or dCell27.Text = "P" Then
                                dCell27.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell27.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell27.BackColor = LosingTeamBackColor
                                End If

                                dCell27.Font.Bold = True
                                If IsAdminPick Then
                                    dCell27.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell27)

                        End If

                        If GameUpdateCollectionSorted.Count >= 14 Then
                            Dim dCell28 As New TableCell
                            dCell28.Text = GameUpdateCollectionSorted("game14").HomeTeamAvailability(user1)

                            If dCell28.Text = "L" Or dCell28.Text = "P" Then
                                dCell28.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell28.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell28.BackColor = LosingTeamBackColor
                                End If

                                dCell28.Font.Bold = True

                                If IsAdminPick Then
                                    dCell28.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell28)

                            Dim dCell29 As New TableCell
                            dCell29.Text = GameUpdateCollectionSorted("game14").AwayTeamAvailability(user1)

                            If dCell29.Text = "L" Or dCell29.Text = "P" Then
                                dCell29.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell29.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell29.BackColor = LosingTeamBackColor
                                End If

                                dCell29.Font.Bold = True

                                If IsAdminPick Then
                                    dCell29.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell29)

                        End If

                        If GameUpdateCollectionSorted.Count >= 15 Then

                            Dim dCell30 As New TableCell
                            dCell30.Text = GameUpdateCollectionSorted("game15").HomeTeamAvailability(user1)

                            If dCell30.Text = "L" Or dCell30.Text = "P" Then
                                dCell30.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell30.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell30.BackColor = LosingTeamBackColor
                                End If

                                dCell30.Font.Bold = True

                                If IsAdminPick Then
                                    dCell30.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell30)

                            Dim dCell31 As New TableCell
                            dCell31.Text = GameUpdateCollectionSorted("game15").AwayTeamAvailability(user1)

                            If dCell31.Text = "L" Or dCell31.Text = "P" Then
                                dCell31.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell31.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell31.BackColor = LosingTeamBackColor
                                End If

                                dCell31.Font.Bold = True

                                If IsAdminPick Then
                                    dCell31.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell31)

                        End If

                        If GameUpdateCollectionSorted.Count >= 16 Then

                            Dim dCell32 As New TableCell
                            dCell32.Text = GameUpdateCollectionSorted("game16").HomeTeamAvailability(user1)

                            If dCell32.Text = "L" Or dCell32.Text = "P" Then
                                dCell32.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell32.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell32.BackColor = LosingTeamBackColor
                                End If

                                dCell32.Font.Bold = True

                                If IsAdminPick Then
                                    dCell32.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell32)

                            Dim dCell33 As New TableCell
                            dCell33.Text = GameUpdateCollectionSorted("game16").AwayTeamAvailability(user1)

                            If dCell33.Text = "L" Or dCell33.Text = "P" Then
                                dCell33.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell33.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell33.BackColor = LosingTeamBackColor
                                End If

                                dCell33.Font.Bold = True

                                If IsAdminPick Then
                                    dCell33.BackColor = AdminBackColor
                                End If

                            End If
                            dRow.Cells.Add(dCell33)

                        End If

                        dRow.CssClass = "dRow1"
                        If GameUpdateCollectionSorted.Count >= 1 Then
                            TeamsByGameTable1.Rows.Add(dRow)
                        End If

                        cnt = cnt + 1
                    Next

                    If GameUpdateCollectionSorted.Count >= 1 Then
                        TeamsByGameTable1.DataBind()
                    End If

                End Using
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub GetTableSize(leagueSize As String)

        If leagueSize <= 1 Then
            Team2Label.Visible = False
            Team2Icon.Visible = False
        End If

        If leagueSize <= 2 Then
            Team3Label.Visible = False
            Team3Icon.Visible = False
        End If

        If leagueSize <= 3 Then
            Team4Label.Visible = False
            Team4Icon.Visible = False
        End If

        If leagueSize <= 4 Then
            Team5Label.Visible = False
            Team5Icon.Visible = False
        End If

        If leagueSize <= 5 Then
            Team6Label.Visible = False
            Team6Icon.Visible = False
        End If

        If leagueSize <= 6 Then
            Team7Label.Visible = False
            Team7Icon.Visible = False
        End If

        If leagueSize <= 7 Then
            Team8Label.Visible = False
            Team8Icon.Visible = False
        End If

        If leagueSize <= 8 Then
            Team9Label.Visible = False
            Team9Icon.Visible = False
        End If

        If leagueSize <= 9 Then
            Team10Label.Visible = False
            Team10Icon.Visible = False
        End If

        If leagueSize <= 10 Then
            Team11Label.Visible = False
            Team11Icon.Visible = False
        End If

        If leagueSize <= 11 Then
            Team12Label.Visible = False
            Team12Icon.Visible = False
        End If

        If leagueSize <= 12 Then
            Team13Label.Visible = False
            Team13Icon.Visible = False
        End If

        If leagueSize <= 13 Then
            Team14Label.Visible = False
            Team14Icon.Visible = False
        End If

        If leagueSize <= 14 Then
            Team15Label.Visible = False
            Team15Icon.Visible = False
        End If

        If leagueSize <= 15 Then
            Team16Label.Visible = False
            Team16Icon.Visible = False
        End If

        If leagueSize <= 16 Then
            Team17Label.Visible = False
            Team17Icon.Visible = False
        End If

        If leagueSize <= 17 Then
            Team18Label.Visible = False
            Team18Icon.Visible = False
        End If

        If leagueSize <= 18 Then
            Team19Label.Visible = False
            Team19Icon.Visible = False
        End If

        If leagueSize <= 19 Then
            Team20Label.Visible = False
            Team20Icon.Visible = False
        End If

        If leagueSize <= 20 Then
            Team21Label.Visible = False
            Team21Icon.Visible = False
        End If

        If leagueSize <= 21 Then
            Team22Label.Visible = False
            Team22Icon.Visible = False
        End If

        If leagueSize <= 22 Then
            Team23Label.Visible = False
            Team23Icon.Visible = False
        End If

        If leagueSize <= 23 Then
            Team24Label.Visible = False
            Team24Icon.Visible = False
        End If

        If leagueSize <= 24 Then
            Team25Label.Visible = False
            Team25Icon.Visible = False
        End If

        If leagueSize <= 25 Then
            Team26Label.Visible = False
            Team26Icon.Visible = False
        End If

        If leagueSize <= 26 Then
            Team27Label.Visible = False
            Team27Icon.Visible = False
        End If

        If leagueSize <= 27 Then
            Team28Label.Visible = False
            Team28Icon.Visible = False
        End If

        If leagueSize <= 28 Then
            Team29Label.Visible = False
            Team29Icon.Visible = False
        End If
        If leagueSize <= 29 Then
            Team30Label.Visible = False
            Team30Icon.Visible = False
        End If

        If leagueSize <= 30 Then
            Team31Label.Visible = False
            Team31Icon.Visible = False
        End If

        If leagueSize <= 31 Then
            Team32Label.Visible = False
            Team32Icon.Visible = False
        End If

    End Sub

    Private Sub GetContendersTableHeader(teams1 As List(Of Team))
        ' Display Contenders Table

        If teams1.Count >= 1 Then
            Team1Label2.ImageUrl = teams1(0).TeamLabel
            Team1Icon2.ImageUrl = teams1(0).TeamIcon
            Team1Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 2 Then
            Team2Label2.ImageUrl = teams1(1).TeamLabel
            Team2Icon2.ImageUrl = teams1(1).TeamIcon
            Team2Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 3 Then
            Team3Label2.ImageUrl = teams1(2).TeamLabel
            Team3Icon2.ImageUrl = teams1(2).TeamIcon
            Team3Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 4 Then
            Team4Label2.ImageUrl = teams1(3).TeamLabel
            Team4Icon2.ImageUrl = teams1(3).TeamIcon
            Team4Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 5 Then
            Team5Label2.ImageUrl = teams1(4).TeamLabel
            Team5Icon2.ImageUrl = teams1(4).TeamIcon
            Team5Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 6 Then
            Team6Label2.ImageUrl = teams1(5).TeamLabel
            Team6Icon2.ImageUrl = teams1(5).TeamIcon
            Team6Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 7 Then
            Team7Label2.ImageUrl = teams1(6).TeamLabel
            Team7Icon2.ImageUrl = teams1(6).TeamIcon
            Team7Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 8 Then
            Team8Label2.ImageUrl = teams1(7).TeamLabel
            Team8Icon2.ImageUrl = teams1(7).TeamIcon
            Team8Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 9 Then
            Team9Label2.ImageUrl = teams1(8).TeamLabel
            Team9Icon2.ImageUrl = teams1(8).TeamIcon
            Team9Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 10 Then
            Team10Label2.ImageUrl = teams1(9).TeamLabel
            Team10Icon2.ImageUrl = teams1(9).TeamIcon
            Team10Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 11 Then
            Team11Label2.ImageUrl = teams1(10).TeamLabel
            Team11Icon2.ImageUrl = teams1(10).TeamIcon
            Team11Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 12 Then
            Team12Label2.ImageUrl = teams1(11).TeamLabel
            Team12Icon2.ImageUrl = teams1(11).TeamIcon
            Team12Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 13 Then
            Team13Label2.ImageUrl = teams1(12).TeamLabel
            Team13Icon2.ImageUrl = teams1(12).TeamIcon
            Team13Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 14 Then
            Team14Label2.ImageUrl = teams1(13).TeamLabel
            Team14Icon2.ImageUrl = teams1(13).TeamIcon
            Team14Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 15 Then
            Team15Label2.ImageUrl = teams1(14).TeamLabel
            Team15Icon2.ImageUrl = teams1(14).TeamIcon
            Team15Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 16 Then
            Team16Label2.ImageUrl = teams1(15).TeamLabel
            Team16Icon2.ImageUrl = teams1(15).TeamIcon
            Team16Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 17 Then
            Team17Label2.ImageUrl = teams1(16).TeamLabel
            Team17Icon2.ImageUrl = teams1(16).TeamIcon
            Team17Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 18 Then
            Team18Label2.ImageUrl = teams1(17).TeamLabel
            Team18Icon2.ImageUrl = teams1(17).TeamIcon
            Team18Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 19 Then
            Team19Label2.ImageUrl = teams1(18).TeamLabel
            Team19Icon2.ImageUrl = teams1(18).TeamIcon
            Team19Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 20 Then
            Team20Label2.ImageUrl = teams1(19).TeamLabel
            Team20Icon2.ImageUrl = teams1(19).TeamIcon
            Team20Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 21 Then
            Team21Label2.ImageUrl = teams1(20).TeamLabel
            Team21Icon2.ImageUrl = teams1(20).TeamIcon
            Team21Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 22 Then
            Team22Label2.ImageUrl = teams1(21).TeamLabel
            Team22Icon2.ImageUrl = teams1(21).TeamIcon
            Team22Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 23 Then
            Team23Label2.ImageUrl = teams1(22).TeamLabel
            Team23Icon2.ImageUrl = teams1(22).TeamIcon
            Team23Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 24 Then
            Team24Label2.ImageUrl = teams1(23).TeamLabel
            Team24Icon2.ImageUrl = teams1(23).TeamIcon
            Team24Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 25 Then
            Team25Label2.ImageUrl = teams1(24).TeamLabel
            Team25Icon2.ImageUrl = teams1(24).TeamIcon
            Team25Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 26 Then
            Team26Label2.ImageUrl = teams1(25).TeamLabel
            Team26Icon2.ImageUrl = teams1(25).TeamIcon
            Team26Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 27 Then
            Team27Label2.ImageUrl = teams1(26).TeamLabel
            Team27Icon2.ImageUrl = teams1(26).TeamIcon
            Team27Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 28 Then
            Team28Label2.ImageUrl = teams1(27).TeamLabel
            Team28Icon2.ImageUrl = teams1(27).TeamIcon
            Team28Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 29 Then
            Team29Label2.ImageUrl = teams1(28).TeamLabel
            Team29Icon2.ImageUrl = teams1(28).TeamIcon
            Team29Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 30 Then
            Team30Label2.ImageUrl = teams1(29).TeamLabel
            Team30Icon2.ImageUrl = teams1(29).TeamIcon
            Team30Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 31 Then
            Team31Label2.ImageUrl = teams1(30).TeamLabel
            Team31Icon2.ImageUrl = teams1(30).TeamIcon
            Team31Label2.BackColor = TeamBackColor
        End If

        If teams1.Count >= 32 Then
            Team32Label2.ImageUrl = teams1(31).TeamLabel
            Team32Icon2.ImageUrl = teams1(31).TeamIcon
            Team32Label2.BackColor = TeamBackColor
        End If


    End Sub

    Private Sub AddUserStatusToContenderTable(thisTimePeriod As String, poolAlias As String, teams1 As List(Of Team), leagueSize As Int32, cronJobName As String)

        If Not Page.IsPostBack Then

            Dim _dbLoserPool3 As New LosersPoolContext

            Try
                Using (_dbLoserPool3)
                    Dim timeState = CStr(Session("timeState"))

                    Dim dailyUserChoices = (From user1 In _dbLoserPool3.UserChoicesList
                                            Where user1.Contender = True And user1.TimePeriod = thisTimePeriod And user1.PoolAlias = poolAlias And user1.CronJob = cronJobName
                                            Select user1).ToList()

                    If dailyUserChoices.Count = 0 Then
                        Contenders4.Visible = False
                        Exit Sub
                    End If

                    For Each user1 In dailyUserChoices


                        Dim dRow As New TableRow

                        Dim dCell1 As New TableCell
                        dCell1.Text = user1.UserName
                        dCell1.ForeColor = UserForeColor
                        dCell1.BackColor = UserBackColor
                        dCell1.CssClass = "UserName1"
                        dRow.Cells.Add(dCell1)


                        If leagueSize >= 1 Then
                            Dim dCell2 As New TableCell
                            If user1.Team1Available = True Or (user1.UserPick = teams1(0).TeamName And timeState = "Enter Picks") Then
                                dCell2.Text = "A"
                                dCell2.ForeColor = AvailableForeColor
                                dCell2.BackColor = AvailableBackColor
                            Else
                                dCell2.Text = "na"
                                dCell2.ForeColor = NotAvailableForeColor
                                dCell2.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell2)

                        End If

                        If leagueSize >= 2 Then
                            Dim dCell3 As New TableCell
                            If user1.Team2Available = True Or (user1.UserPick = teams1(1).TeamName And timeState = "Enter Picks") Then
                                dCell3.Text = "A"
                                dCell3.ForeColor = AvailableForeColor
                                dCell3.BackColor = AvailableBackColor

                            Else
                                dCell3.Text = "na"
                                dCell3.ForeColor = NotAvailableForeColor
                                dCell3.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell3)

                        End If

                        If leagueSize >= 3 Then
                            Dim dCell4 As New TableCell
                            If user1.Team3Available = True Or (user1.UserPick = teams1(2).TeamName And timeState = "Enter Picks") Then
                                dCell4.Text = "A"
                                dCell4.ForeColor = AvailableForeColor
                                dCell4.BackColor = AvailableBackColor

                            Else
                                dCell4.Text = "na"
                                dCell4.ForeColor = NotAvailableForeColor
                                dCell4.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell4)

                        End If

                        If leagueSize >= 4 Then
                            Dim dCell5 As New TableCell
                            If user1.Team4Available = True Or (user1.UserPick = teams1(3).TeamName And timeState = "Enter Picks") Then
                                dCell5.Text = "A"
                                dCell5.ForeColor = AvailableForeColor
                                dCell5.BackColor = AvailableBackColor

                            Else
                                dCell5.Text = "na"
                                dCell5.ForeColor = NotAvailableForeColor
                                dCell5.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell5)

                        End If

                        If leagueSize >= 5 Then
                            Dim dCell6 As New TableCell
                            If user1.Team5Available = True Or (user1.UserPick = teams1(4).TeamName And timeState = "Enter Picks") Then
                                dCell6.Text = "A"
                                dCell6.ForeColor = AvailableForeColor
                                dCell6.BackColor = AvailableBackColor

                            Else
                                dCell6.Text = "na"
                                dCell6.ForeColor = NotAvailableForeColor
                                dCell6.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell6)

                        End If

                        If leagueSize >= 6 Then
                            Dim dCell7 As New TableCell
                            If user1.Team6Available = True Or (user1.UserPick = teams1(5).TeamName And timeState = "Enter Picks") Then
                                dCell7.Text = "A"
                                dCell7.ForeColor = AvailableForeColor
                                dCell7.BackColor = AvailableBackColor

                            Else
                                dCell7.Text = "na"
                                dCell7.ForeColor = NotAvailableForeColor
                                dCell7.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell7)

                        End If

                        If leagueSize >= 7 Then
                            Dim dCell8 As New TableCell
                            If user1.Team7Available = True Or (user1.UserPick = teams1(6).TeamName And timeState = "Enter Picks") Then
                                dCell8.Text = "A"
                                dCell8.ForeColor = AvailableForeColor
                                dCell8.BackColor = AvailableBackColor

                            Else
                                dCell8.Text = "na"
                                dCell8.ForeColor = NotAvailableForeColor
                                dCell8.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell8)

                        End If

                        If leagueSize >= 8 Then
                            Dim dCell9 As New TableCell
                            If user1.Team8Available = True Or (user1.UserPick = teams1(7).TeamName And timeState = "Enter Picks") Then
                                dCell9.Text = "A"
                                dCell9.ForeColor = AvailableForeColor
                                dCell9.BackColor = AvailableBackColor

                            Else
                                dCell9.Text = "na"
                                dCell9.ForeColor = NotAvailableForeColor
                                dCell9.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell9)

                        End If

                        If leagueSize >= 9 Then
                            Dim dCell10 As New TableCell
                            If user1.Team9Available = True Or (user1.UserPick = teams1(8).TeamName And timeState = "Enter Picks") Then
                                dCell10.Text = "A"
                                dCell10.ForeColor = AvailableForeColor
                                dCell10.BackColor = AvailableBackColor

                            Else
                                dCell10.Text = "na"
                                dCell10.ForeColor = NotAvailableForeColor
                                dCell10.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell10)

                        End If

                        If leagueSize >= 10 Then
                            Dim dCell11 As New TableCell
                            If user1.Team10Available = True Or (user1.UserPick = teams1(9).TeamName And timeState = "Enter Picks") Then
                                dCell11.Text = "A"
                                dCell11.ForeColor = AvailableForeColor
                                dCell11.BackColor = AvailableBackColor

                            Else
                                dCell11.Text = "na"
                                dCell11.ForeColor = NotAvailableForeColor
                                dCell11.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell11)

                        End If

                        If leagueSize >= 11 Then
                            Dim dCell12 As New TableCell
                            If user1.Team11Available = True Or (user1.UserPick = teams1(10).TeamName And timeState = "Enter Picks") Then
                                dCell12.Text = "A"
                                dCell12.ForeColor = AvailableForeColor
                                dCell12.BackColor = AvailableBackColor

                            Else
                                dCell12.Text = "na"
                                dCell12.ForeColor = NotAvailableForeColor
                                dCell12.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell12)

                        End If

                        If leagueSize >= 12 Then
                            Dim dCell13 As New TableCell
                            If user1.Team12Available = True Or (user1.UserPick = teams1(11).TeamName And timeState = "Enter Picks") Then
                                dCell13.Text = "A"
                                dCell13.ForeColor = AvailableForeColor
                                dCell13.BackColor = AvailableBackColor

                            Else
                                dCell13.Text = "na"
                                dCell13.ForeColor = NotAvailableForeColor
                                dCell13.BackColor = NotAvailableBackColor
                            End If
                            dRow.Cells.Add(dCell13)

                        End If

                        If leagueSize >= 13 Then
                            Dim dCell14 As New TableCell
                            If user1.Team13Available = True Or (user1.UserPick = teams1(12).TeamName And timeState = "Enter Picks") Then
                                dCell14.Text = "A"
                                dCell14.ForeColor = AvailableForeColor
                                dCell14.BackColor = AvailableBackColor

                            Else
                                dCell14.Text = "na"
                                dCell14.ForeColor = NotAvailableForeColor
                                dCell14.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell14)

                        End If

                        If leagueSize >= 14 Then
                            Dim dCell15 As New TableCell
                            If user1.Team14Available = True Or (user1.UserPick = teams1(13).TeamName And timeState = "Enter Picks") Then
                                dCell15.Text = "A"
                                dCell15.ForeColor = AvailableForeColor
                                dCell15.BackColor = AvailableBackColor
                            Else
                                dCell15.Text = "na"
                                dCell15.ForeColor = NotAvailableForeColor
                                dCell15.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell15)

                        End If

                        If leagueSize >= 15 Then
                            Dim dCell16 As New TableCell
                            If user1.Team15Available = True Or (user1.UserPick = teams1(14).TeamName And timeState = "Enter Picks") Then
                                dCell16.Text = "A"
                                dCell16.ForeColor = AvailableForeColor
                                dCell16.BackColor = AvailableBackColor

                            Else
                                dCell16.Text = "na"
                                dCell16.ForeColor = NotAvailableForeColor
                                dCell16.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell16)

                        End If

                        If leagueSize >= 16 Then
                            Dim dCell17 As New TableCell
                            If user1.Team16Available = True Or (user1.UserPick = teams1(15).TeamName And timeState = "Enter Picks") Then
                                dCell17.Text = "A"
                                dCell17.ForeColor = AvailableForeColor
                                dCell17.BackColor = AvailableBackColor

                            Else
                                dCell17.Text = "na"
                                dCell17.ForeColor = NotAvailableForeColor
                                dCell17.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell17)

                        End If

                        If leagueSize >= 17 Then
                            Dim dCell18 As New TableCell
                            If user1.Team17Available = True Or (user1.UserPick = teams1(16).TeamName And timeState = "Enter Picks") Then
                                dCell18.Text = "A"
                                dCell18.ForeColor = AvailableForeColor
                                dCell18.BackColor = AvailableBackColor

                            Else
                                dCell18.Text = "na"
                                dCell18.ForeColor = NotAvailableForeColor
                                dCell18.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell18)

                        End If

                        If leagueSize >= 18 Then
                            Dim dCell19 As New TableCell
                            If user1.Team18Available = True Or (user1.UserPick = teams1(17).TeamName And timeState = "Enter Picks") Then
                                dCell19.Text = "A"
                                dCell19.ForeColor = AvailableForeColor
                                dCell19.BackColor = AvailableBackColor

                            Else
                                dCell19.Text = "na"
                                dCell19.ForeColor = NotAvailableForeColor
                                dCell19.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell19)

                        End If

                        If leagueSize >= 19 Then
                            Dim dCell20 As New TableCell
                            If user1.Team19Available = True Or (user1.UserPick = teams1(18).TeamName And timeState = "Enter Picks") Then
                                dCell20.Text = "A"
                                dCell20.ForeColor = AvailableForeColor
                                dCell20.BackColor = AvailableBackColor

                            Else
                                dCell20.Text = "na"
                                dCell20.ForeColor = NotAvailableForeColor
                                dCell20.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell20)

                        End If

                        If leagueSize >= 20 Then
                            Dim dCell21 As New TableCell
                            If user1.Team20Available = True Or (user1.UserPick = teams1(19).TeamName And timeState = "Enter Picks") Then
                                dCell21.Text = "A"
                                dCell21.ForeColor = AvailableForeColor
                                dCell21.BackColor = AvailableBackColor

                            Else
                                dCell21.Text = "na"
                                dCell21.ForeColor = NotAvailableForeColor
                                dCell21.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell21)

                        End If

                        If leagueSize >= 21 Then
                            Dim dCell22 As New TableCell
                            If user1.Team21Available = True Or (user1.UserPick = teams1(20).TeamName And timeState = "Enter Picks") Then
                                dCell22.Text = "A"
                                dCell22.ForeColor = AvailableForeColor
                                dCell22.BackColor = AvailableBackColor

                            Else
                                dCell22.Text = "na"
                                dCell22.ForeColor = NotAvailableForeColor
                                dCell22.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell22)

                        End If

                        If leagueSize >= 22 Then
                            Dim dCell23 As New TableCell
                            If user1.Team22Available = True Or (user1.UserPick = teams1(21).TeamName And timeState = "Enter Picks") Then
                                dCell23.Text = "A"
                                dCell23.ForeColor = AvailableForeColor
                                dCell23.BackColor = AvailableBackColor

                            Else
                                dCell23.Text = "na"
                                dCell23.ForeColor = NotAvailableForeColor
                                dCell23.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell23)

                        End If

                        If leagueSize >= 23 Then
                            Dim dCell24 As New TableCell
                            If user1.Team23Available = True Or (user1.UserPick = teams1(22).TeamName And timeState = "Enter Picks") Then
                                dCell24.Text = "A"
                                dCell24.ForeColor = AvailableForeColor
                                dCell24.BackColor = AvailableBackColor

                            Else
                                dCell24.Text = "na"
                                dCell24.ForeColor = NotAvailableForeColor
                                dCell24.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell24)

                        End If

                        If leagueSize >= 24 Then
                            Dim dCell25 As New TableCell
                            If user1.Team24Available = True Or (user1.UserPick = teams1(23).TeamName And timeState = "Enter Picks") Then
                                dCell25.Text = "A"
                                dCell25.ForeColor = AvailableForeColor
                                dCell25.BackColor = AvailableBackColor

                            Else
                                dCell25.Text = "na"
                                dCell25.ForeColor = NotAvailableForeColor
                                dCell25.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell25)

                        End If

                        If leagueSize >= 25 Then
                            Dim dCell26 As New TableCell
                            If user1.Team25Available = True Or (user1.UserPick = teams1(24).TeamName And timeState = "Enter Picks") Then
                                dCell26.Text = "A"
                                dCell26.ForeColor = AvailableForeColor
                                dCell26.BackColor = AvailableBackColor

                            Else
                                dCell26.Text = "na"
                                dCell26.ForeColor = NotAvailableForeColor
                                dCell26.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell26)

                        End If

                        If leagueSize >= 26 Then
                            Dim dCell27 As New TableCell
                            If user1.Team26Available = True Or (user1.UserPick = teams1(25).TeamName And timeState = "Enter Picks") Then
                                dCell27.Text = "A"
                                dCell27.ForeColor = AvailableForeColor
                                dCell27.BackColor = AvailableBackColor

                            Else
                                dCell27.Text = "na"
                                dCell27.ForeColor = NotAvailableForeColor
                                dCell27.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell27)

                        End If

                        If leagueSize >= 27 Then
                            Dim dCell28 As New TableCell
                            If user1.Team27Available = True Or (user1.UserPick = teams1(26).TeamName And timeState = "Enter Picks") Then
                                dCell28.Text = "A"
                                dCell28.ForeColor = AvailableForeColor
                                dCell28.BackColor = AvailableBackColor

                            Else
                                dCell28.Text = "na"
                                dCell28.ForeColor = NotAvailableForeColor
                                dCell28.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell28)

                        End If

                        If leagueSize >= 28 Then
                            Dim dCell29 As New TableCell
                            If user1.Team28Available = True Or (user1.UserPick = teams1(27).TeamName And timeState = "Enter Picks") Then
                                dCell29.Text = "A"
                                dCell29.ForeColor = AvailableForeColor
                                dCell29.BackColor = AvailableBackColor

                            Else
                                dCell29.Text = "na"
                                dCell29.ForeColor = NotAvailableForeColor
                                dCell29.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell29)

                        End If

                        If leagueSize >= 29 Then
                            Dim dCell30 As New TableCell
                            If user1.Team29Available = True Or (user1.UserPick = teams1(28).TeamName And timeState = "Enter Picks") Then
                                dCell30.Text = "A"
                                dCell30.ForeColor = AvailableForeColor
                                dCell30.BackColor = AvailableBackColor

                            Else
                                dCell30.Text = "na"
                                dCell30.ForeColor = NotAvailableForeColor
                                dCell30.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell30)

                        End If

                        If leagueSize >= 30 Then
                            Dim dCell31 As New TableCell
                            If user1.Team30Available = True Or (user1.UserPick = teams1(29).TeamName And timeState = "Enter Picks") Then
                                dCell31.Text = "A"
                                dCell31.ForeColor = AvailableForeColor
                                dCell31.BackColor = AvailableBackColor

                            Else
                                dCell31.Text = "na"
                                dCell31.ForeColor = NotAvailableForeColor
                                dCell31.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell31)

                        End If

                        If leagueSize >= 31 Then
                            Dim dCell32 As New TableCell
                            If user1.Team31Available = True Or (user1.UserPick = teams1(30).TeamName And timeState = "Enter Picks") Then
                                dCell32.Text = "A"
                                dCell32.ForeColor = AvailableForeColor
                                dCell32.BackColor = AvailableBackColor

                            Else
                                dCell32.Text = "na"
                                dCell32.ForeColor = NotAvailableForeColor
                                dCell32.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell32)

                        End If

                        If leagueSize >= 32 Then
                            Dim dCell33 As New TableCell
                            If user1.Team32Available = True Or (user1.UserPick = teams1(31).TeamName And timeState = "Enter Picks") Then
                                dCell33.Text = "A"
                                dCell33.ForeColor = AvailableForeColor
                                dCell33.BackColor = AvailableBackColor

                            Else
                                dCell33.Text = "na"
                                dCell33.ForeColor = NotAvailableForeColor
                                dCell33.BackColor = NotAvailableBackColor

                            End If
                            dRow.Cells.Add(dCell33)

                        End If

                        Contenders4.Rows.Add(dRow)
                    Next

                    Contenders4.DataBind()

                End Using
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub SortLosers(thisTimePeriod As String, poolAlias As String, queryTimePs As List(Of ScheduleTimePeriod), cronJobName As String)

        Dim _dbLoserPool3 As New LosersPoolContext

        Try
            Using (_dbLoserPool3)
                For Each timeP1 In queryTimePs

                    Dim queryDayLosers = (From loser1 In _dbLoserPool3.LoserList
                                          Where loser1.TimePeriod = timeP1.TimePeriod And loser1.PoolAlias = poolAlias And loser1.CronJob = cronJobName).ToList

                    For Each loser1 In queryDayLosers

                        If loser1.LosingPick <> "Not Made" Then
                            Dim loser2 = New Loser
                            loser2.UserName = loser1.UserName
                            loser2.TimePeriod = loser1.TimePeriod
                            loser2.LosingPick = loser1.LosingPick
                            LoserCollectionSorted.Add(loser2.UserName, loser2)
                        End If
                    Next
                    For Each loser1 In queryDayLosers
                        If loser1.LosingPick = "Not Made" Then
                            Dim loser2 = New Loser
                            loser2.UserName = loser1.UserName
                            loser2.TimePeriod = loser1.TimePeriod
                            loser2.LosingPick = loser1.LosingPick
                            LoserCollectionSorted.Add(loser2.UserName, loser2)
                        End If
                    Next
                Next

            End Using
        Catch ex As Exception

        End Try


    End Sub

    Private Sub AddLosersToLoserTable(LoserCollectionSorted As Dictionary(Of String, Loser))

        If Not Page.IsPostBack Then

            If LoserCollectionSorted.Count = 0 Then
                LoserTable1.Visible = False
                Exit Sub
            End If


            For Each loser1 In LoserCollectionSorted

                Dim dRow As New TableRow

                Dim dCell1 As New TableCell
                dCell1.Text = loser1.Value.UserName
                dCell1.ForeColor = Drawing.Color.Red
                dCell1.BackColor = Drawing.Color.LightSalmon
                dRow.Cells.Add(dCell1)

                Dim dCell2 As New TableCell
                dCell2.Text = loser1.Value.TimePeriod
                dCell2.ForeColor = Drawing.Color.Red
                dCell2.BackColor = Drawing.Color.LightSalmon
                dRow.Cells.Add(dCell2)

                Dim dCell3 As New TableCell
                dCell3.Text = loser1.Value.LosingPick
                dCell3.ForeColor = Drawing.Color.Red
                dCell3.BackColor = Drawing.Color.LightSalmon
                dRow.Cells.Add(dCell3)

                LoserTable1.Rows.Add(dRow)
            Next

            LoserTable1.DataBind()

        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Response.Redirect("~/LosersPool/LoserPoolHome.aspx")
    End Sub


End Class