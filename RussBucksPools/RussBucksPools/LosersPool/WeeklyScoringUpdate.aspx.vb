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

Public Class WeeklyScoringUpdate
    Inherits System.Web.UI.Page

    Private GameUpdateCollection As New Dictionary(Of String, GameUpdate)
    Private GameUpdateCollectionSorted As New Dictionary(Of String, GameUpdate)

    'Private TeamNameCollection As New Dictionary(Of String, String)
    'Private TeamNameCollection2 As New Dictionary(Of String, String)



    ' Keeps track of if users are winning, losing, of if the game they picked is tied
    Private UserStatusCollection As New Dictionary(Of String, UserStatus)
    Private PickStatusCollection As New Dictionary(Of String, PickStatus)

    Private WinningTeamForeColor As System.Drawing.Color = Drawing.Color.Green
    Private WinningTeamBackColor As System.Drawing.Color = Drawing.Color.LightGreen

    Private LosingTeamForeColor As System.Drawing.Color = Drawing.Color.Red
    Private LosingTeamBackColor As System.Drawing.Color = Drawing.Color.LightSalmon

    Private AdminBackColor As System.Drawing.Color = Drawing.Color.Yellow

    Private userColor As New System.Drawing.Color

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

                            Dim thisTimePeriod = queryPoolParam.TimePeriod
                            Dim sport = queryPoolParam.Sport
                            Dim poolState = queryPoolParam.poolState

                            If thisTimePeriod <> staleTimePeriod And poolState <> "Scoring Update" Then
                                Response.Redirect("~/LosersPool/LoserPoolHome.aspx")
                            End If

                            Dim rootFolder = CStr(Session("rootFolder"))
                            System.IO.Directory.SetCurrentDirectory(rootFolder)

                            Dim teams1 = (From teams2 In _dbPools.Teams
                                           Where teams2.Sport = sport And teams2.TeamName <> "dummy").ToList


                            Dim queryContenders = (From contender1 In _dbLoserPool.UserChoicesList
                                               Where contender1.TimePeriod = thisTimePeriod And contender1.Contender = True And contender1.PoolAlias = poolAlias And contender1.CronJob = cronJobName).ToList

                            Dim queryUserPicks = (From qUP In _dbLoserPool.UserPicks
                                                  Where qUP.CronJobName = cronJobName And qUP.PoolAlias = poolAlias).ToList

                            CreateScoringUpdateTable(thisTimePeriod, poolAlias, queryContenders, teams1, GameUpdateCollection, GameUpdateCollectionSorted, UserStatusCollection, cronJobName, PickStatusCollection)

                        End Using
                    End Using
                End Using
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub CreateScoringUpdateTable(thisTimePeriod As String, poolAlias As String, queryContenders As List(Of UserChoices), teams1 As List(Of Team), _
                                         GameUpdateCollection As Dictionary(Of String, GameUpdate), GameUpdateCollectionSorted As Dictionary(Of String, GameUpdate), _
                                         UserStatusCollection As Dictionary(Of String, UserStatus), cronJobName As String, PickStatusCollection As Dictionary(Of String, PickStatus))

        GameUpdateCollection = GetGameUpdateCollection(thisTimePeriod, queryContenders, teams1, GameUpdateCollection, cronJobName)

        GetScoringUpdateTableHeader(GameUpdateCollection, teams1)

        UserStatusCollection = GetUserStatusCollection(UserStatusCollection, thisTimePeriod, poolAlias, cronJobName)

        PickStatusCollection = GetPickStatusCollection(PickStatusCollection, thisTimePeriod, poolAlias, cronJobName)

        GameUpdateCollectionSorted = SortGameUpdateCollection(GameUpdateCollection, GameUpdateCollectionSorted)

        AddUserStatusToScoringUpdateTable(GameUpdateCollectionSorted, UserStatusCollection, thisTimePeriod, poolAlias, cronJobName, PickStatusCollection)

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
                Dim queryUserPick = (From qUP1 In _dbLoserPool4.UserPicks
                                     Where qUP1.CronJobName = user1.CronJob And qUP1.PoolAlias = user1.PoolAlias And _
                                     qUP1.GameCode = gameUpdate1.GameCode And qUP1.UserID = user1.UserID).SingleOrDefault

                If gameUpdate1.HomeTeam = team And Not (queryUserPick Is Nothing) Then
                    If queryUserPick.UserPick1 = team And queryUserPick.UserPickPostponed = False Then
                        gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "L")
                    ElseIf queryUserPick.UserPick1 = team And queryUserPick.UserPickPostponed = True Then
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

                Dim queryUserPick = (From qUP1 In _dbLoserPool4.UserPicks
                                     Where qUP1.CronJobName = user1.CronJob And qUP1.PoolAlias = user1.PoolAlias And _
                                     qUP1.GameCode = gameUpdate1.GameCode And qUP1.UserID = user1.UserID).SingleOrDefault

                If gameUpdate1.AwayTeam = team And Not (queryUserPick Is Nothing) Then
                    If queryUserPick.UserPick1 = team And queryUserPick.UserPickPostponed = False Then
                        gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "L")
                    ElseIf queryUserPick.UserPick1 = team And queryUserPick.UserPickPostponed = True Then
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

            If GameUpdateCollection.ContainsKey("game1") = False Then
                GameNumber1.Visible = False
                GameNumber1Status.Visible = False
                HomeTeam1Image.Visible = False
                HomeTeam1Icon.Visible = False
                AwayTeam1Image.Visible = False
                AwayTeam1Icon.Visible = False
                HomeScore1.Visible = False
                AwayScore1.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game2") = False Then
                GameNumber2.Visible = False
                GameNumber2Status.Visible = False
                HomeTeam2Image.Visible = False
                HomeTeam2Icon.Visible = False
                AwayTeam2Image.Visible = False
                AwayTeam2Icon.Visible = False
                HomeScore2.Visible = False
                AwayScore2.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game3") = False Then
                GameNumber3.Visible = False
                GameNumber3Status.Visible = False
                HomeTeam3Image.Visible = False
                HomeTeam3Icon.Visible = False
                AwayTeam3Image.Visible = False
                AwayTeam3Icon.Visible = False
                HomeScore3.Visible = False
                AwayScore3.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game4") = False Then
                GameNumber4.Visible = False
                GameNumber4Status.Visible = False
                HomeTeam4Image.Visible = False
                HomeTeam4Icon.Visible = False
                AwayTeam4Image.Visible = False
                AwayTeam4Icon.Visible = False
                HomeScore4.Visible = False
                AwayScore4.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game5") = False Then
                GameNumber5.Visible = False
                GameNumber5Status.Visible = False
                HomeTeam5Image.Visible = False
                HomeTeam5Icon.Visible = False
                AwayTeam5Image.Visible = False
                AwayTeam5Icon.Visible = False
                HomeScore5.Visible = False
                AwayScore5.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game6") = False Then
                GameNumber6.Visible = False
                GameNumber6Status.Visible = False
                HomeTeam6Image.Visible = False
                HomeTeam6Icon.Visible = False
                AwayTeam6Image.Visible = False
                AwayTeam6Icon.Visible = False
                HomeScore6.Visible = False
                AwayScore6.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game7") = False Then
                GameNumber7.Visible = False
                GameNumber7Status.Visible = False
                HomeTeam7Image.Visible = False
                HomeTeam7Icon.Visible = False
                AwayTeam7Image.Visible = False
                AwayTeam7Icon.Visible = False
                HomeScore7.Visible = False
                AwayScore7.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game8") = False Then
                GameNumber8.Visible = False
                GameNumber8Status.Visible = False
                HomeTeam8Image.Visible = False
                HomeTeam8Icon.Visible = False
                AwayTeam8Image.Visible = False
                AwayTeam8Icon.Visible = False
                HomeScore8.Visible = False
                AwayScore8.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game9") = False Then
                GameNumber9.Visible = False
                GameNumber9Status.Visible = False
                HomeTeam9Image.Visible = False
                HomeTeam9Icon.Visible = False
                AwayTeam9Image.Visible = False
                AwayTeam9Icon.Visible = False
                HomeScore9.Visible = False
                AwayScore9.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game10") = False Then
                GameNumber10.Visible = False
                GameNumber10Status.Visible = False
                HomeTeam10Image.Visible = False
                HomeTeam10Icon.Visible = False
                AwayTeam10Image.Visible = False
                AwayTeam10Icon.Visible = False
                HomeScore10.Visible = False
                AwayScore10.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game11") = False Then
                GameNumber11.Visible = False
                GameNumber11Status.Visible = False
                HomeTeam11Image.Visible = False
                HomeTeam11Icon.Visible = False
                AwayTeam11Image.Visible = False
                AwayTeam11Icon.Visible = False
                HomeScore11.Visible = False
                AwayScore11.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game12") = False Then
                GameNumber12.Visible = False
                GameNumber12Status.Visible = False
                HomeTeam12Image.Visible = False
                HomeTeam12Icon.Visible = False
                AwayTeam12Image.Visible = False
                AwayTeam12Icon.Visible = False
                HomeScore12.Visible = False
                AwayScore12.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game13") = False Then
                GameNumber13.Visible = False
                GameNumber13Status.Visible = False
                HomeTeam13Image.Visible = False
                HomeTeam13Icon.Visible = False
                AwayTeam13Image.Visible = False
                AwayTeam13Icon.Visible = False
                HomeScore13.Visible = False
                AwayScore13.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game14") = False Then
                GameNumber14.Visible = False
                GameNumber14Status.Visible = False
                HomeTeam14Image.Visible = False
                HomeTeam14Icon.Visible = False
                AwayTeam14Image.Visible = False
                AwayTeam14Icon.Visible = False
                HomeScore14.Visible = False
                AwayScore14.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game15") = False Then
                GameNumber15.Visible = False
                GameNumber15Status.Visible = False
                HomeTeam15Image.Visible = False
                HomeTeam15Icon.Visible = False
                AwayTeam15Image.Visible = False
                AwayTeam15Icon.Visible = False
                HomeScore15.Visible = False
                AwayScore15.Visible = False
            End If

            If GameUpdateCollection.ContainsKey("game16") = False Then
                GameNumber16.Visible = False
                GameNumber16Status.Visible = False
                HomeTeam16Image.Visible = False
                HomeTeam16Icon.Visible = False
                AwayTeam16Image.Visible = False
                AwayTeam16Icon.Visible = False
                HomeScore16.Visible = False
                AwayScore16.Visible = False
            End If

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
                        user1status.BackColor = Drawing.Color.Empty
                    ElseIf user1.UserIsWinning = True Then
                        user1status.UserColor = WinningTeamForeColor
                        user1status.BackColor = WinningTeamBackColor
                    ElseIf user1.UserIsWinning = False Then
                        user1status.UserColor = LosingTeamForeColor
                        user1status.BackColor = LosingTeamBackColor
                    End If

                    UserStatusCollection.Add(user1.UserName, user1status)

                Next


            End Using
        Catch ex As Exception

        End Try


        Return UserStatusCollection

    End Function

    Private Function GetPickStatusCollection(PickStatusCollection As Dictionary(Of String, PickStatus), thisTimePeriod As String, poolAlias As String, cronJobName As String) As Dictionary(Of String, PickStatus)
        Dim _dbLoserPool6 As New LosersPoolContext

        Try
            Using (_dbLoserPool6)

                Dim queryUserChoices = (From qUC1 In _dbLoserPool6.UserChoicesList
                        Where qUC1.CronJob = cronJobName And qUC1.PoolAlias = poolAlias And _
                        qUC1.TimePeriod = thisTimePeriod And qUC1.Contender = True).ToList

                For Each user1 In queryUserChoices
                    Dim queryUserPicks = (From qUP1 In _dbLoserPool6.UserPicks
                      Where qUP1.UserID = user1.UserID And qUP1.CronJobName = cronJobName And _
                      qUP1.PoolAlias = poolAlias).ToList

                    For Each pick1 In queryUserPicks
                        Dim pick1Status As New PickStatus
                        pick1Status.UserId = user1.UserID
                        pick1Status.UserName = user1.UserName
                        pick1Status.GameCode = pick1.GameCode
                        pick1Status.IsPickWinning = pick1.PickIsWinning
                        pick1Status.IsPickTied = pick1.PickIsTied

                        If pick1.PickIsTied = True Then
                            pick1Status.PickColor = Drawing.Color.Black
                            pick1Status.BackColor = Drawing.Color.Empty
                        ElseIf pick1.PickIsWinning = True Then
                            pick1Status.PickColor = WinningTeamForeColor
                            pick1Status.BackColor = WinningTeamBackColor
                        ElseIf pick1.PickIsWinning = False Then
                            pick1Status.PickColor = LosingTeamForeColor
                            pick1Status.BackColor = LosingTeamBackColor
                        End If

                        Dim pickKey = pick1Status.UserName + pick1Status.GameCode
                        PickStatusCollection.Add(pickKey, pick1Status)
                    Next
                Next
            End Using

        Catch ex As Exception

        End Try

        Return PickStatusCollection
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
            game2.GameCode = game1.Value.GameCode
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

    Private Sub AddUserStatusToScoringUpdateTable(GameUpdateCollectionSorted As Dictionary(Of String, GameUpdate), UserStatusCollection As Dictionary(Of String, UserStatus), thisTimePeriod As String, poolAlias As String, cronJobName As String, PickStatusCollection As Dictionary(Of String, PickStatus))

        If Not Page.IsPostBack Then

            Dim _dbLoserPool2 As New LosersPoolContext

            Try
                Using (_dbLoserPool2)
                    Dim cnt = 0
                    For Each user1 In GameUpdateCollectionSorted("game1").UserHandles

                        Dim IsAdminPick = (From user1a In _dbLoserPool2.UserChoicesList
                                           Where user1a.UserName = user1 And user1a.TimePeriod = thisTimePeriod And user1a.PoolAlias = poolAlias
                                           Select user1a.AdministrationPick).Single


                        Dim dRow As New TableRow

                        If GameUpdateCollectionSorted.Count >= 1 Then

                            Dim dCell1 As New TableCell
                            dCell1.Text = GameUpdateCollectionSorted("game1").UserHandles(cnt)
                            dCell1.ForeColor = UserStatusCollection(user1).UserColor
                            If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                dCell1.BackColor = WinningTeamBackColor
                            ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                dCell1.BackColor = LosingTeamBackColor
                            End If
                            dCell1.Font.Bold = True
                            dCell1.CssClass = "UserName1"
                            If IsAdminPick Then
                                dCell1.BackColor = AdminBackColor
                            End If
                            dRow.Cells.Add(dCell1)


                            Dim dCell2 As New TableCell
                            dCell2.Text = GameUpdateCollectionSorted("game1").HomeTeamAvailability(user1)
                            If dCell2.Text = "L" Or dCell2.Text = "P" Then

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game1").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell2.ForeColor = queryPickStatus.Value.PickColor
                                If dCell2.ForeColor = WinningTeamForeColor Then
                                    dCell2.BackColor = WinningTeamBackColor
                                ElseIf dCell2.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game1").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell3.ForeColor = queryPickStatus.Value.PickColor
                                If dCell3.ForeColor = WinningTeamForeColor Then
                                    dCell3.BackColor = WinningTeamBackColor
                                ElseIf dCell3.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game2").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell4.ForeColor = queryPickStatus.Value.PickColor
                                If dCell4.ForeColor = WinningTeamForeColor Then
                                    dCell4.BackColor = WinningTeamBackColor
                                ElseIf dCell4.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game2").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell5.ForeColor = queryPickStatus.Value.PickColor
                                If dCell5.ForeColor = WinningTeamForeColor Then
                                    dCell5.BackColor = WinningTeamBackColor
                                ElseIf dCell5.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game3").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell6.ForeColor = queryPickStatus.Value.PickColor
                                If dCell6.ForeColor = WinningTeamForeColor Then
                                    dCell6.BackColor = WinningTeamBackColor
                                ElseIf dCell6.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game3").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell7.ForeColor = queryPickStatus.Value.PickColor
                                If dCell7.ForeColor = WinningTeamForeColor Then
                                    dCell7.BackColor = WinningTeamBackColor
                                ElseIf dCell7.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game4").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell8.ForeColor = queryPickStatus.Value.PickColor
                                If dCell8.ForeColor = WinningTeamForeColor Then
                                    dCell8.BackColor = WinningTeamBackColor
                                ElseIf dCell8.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game4").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell9.ForeColor = queryPickStatus.Value.PickColor
                                If dCell9.ForeColor = WinningTeamForeColor Then
                                    dCell9.BackColor = WinningTeamBackColor
                                ElseIf dCell9.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game5").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell10.ForeColor = queryPickStatus.Value.PickColor
                                If dCell10.ForeColor = WinningTeamForeColor Then
                                    dCell10.BackColor = WinningTeamBackColor
                                ElseIf dCell10.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game5").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell11.ForeColor = queryPickStatus.Value.PickColor
                                If dCell11.ForeColor = WinningTeamForeColor Then
                                    dCell11.BackColor = WinningTeamBackColor
                                ElseIf dCell11.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game6").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell12.ForeColor = queryPickStatus.Value.PickColor
                                If dCell12.ForeColor = WinningTeamForeColor Then
                                    dCell12.BackColor = WinningTeamBackColor
                                ElseIf dCell12.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game6").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell13.ForeColor = queryPickStatus.Value.PickColor
                                If dCell13.ForeColor = WinningTeamForeColor Then
                                    dCell13.BackColor = WinningTeamBackColor
                                ElseIf dCell13.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game7").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell14.ForeColor = queryPickStatus.Value.PickColor
                                If dCell14.ForeColor = WinningTeamForeColor Then
                                    dCell14.BackColor = WinningTeamBackColor
                                ElseIf dCell14.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game7").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell15.ForeColor = queryPickStatus.Value.PickColor
                                If dCell15.ForeColor = WinningTeamForeColor Then
                                    dCell15.BackColor = WinningTeamBackColor
                                ElseIf dCell15.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game8").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell16.ForeColor = queryPickStatus.Value.PickColor
                                If dCell16.ForeColor = WinningTeamForeColor Then
                                    dCell16.BackColor = WinningTeamBackColor
                                ElseIf dCell16.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game8").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell17.ForeColor = queryPickStatus.Value.PickColor
                                If dCell17.ForeColor = WinningTeamForeColor Then
                                    dCell17.BackColor = WinningTeamBackColor
                                ElseIf dCell17.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game9").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell18.ForeColor = queryPickStatus.Value.PickColor
                                If dCell18.ForeColor = WinningTeamForeColor Then
                                    dCell18.BackColor = WinningTeamBackColor
                                ElseIf dCell18.ForeColor = LosingTeamForeColor Then
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

                                Dim pickKey = user1 + GameUpdateCollectionSorted("game9").GameCode

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell19.ForeColor = queryPickStatus.Value.PickColor
                                If dCell19.ForeColor = WinningTeamForeColor Then
                                    dCell19.BackColor = WinningTeamBackColor
                                ElseIf dCell19.ForeColor = LosingTeamForeColor Then
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

                            Dim pickKey = user1 + GameUpdateCollectionSorted("game10").GameCode

                            Dim dCell20 As New TableCell
                            dCell20.Text = GameUpdateCollectionSorted("game10").HomeTeamAvailability(user1)
                            If dCell20.Text = "L" Or dCell20.Text = "P" Then

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                       Where qPS1.Key = pickKey).Single

                                dCell20.ForeColor = queryPickStatus.Value.PickColor
                                If dCell20.ForeColor = WinningTeamForeColor Then
                                    dCell20.BackColor = WinningTeamBackColor
                                ElseIf dCell20.ForeColor = LosingTeamForeColor Then
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

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                        Where qPS1.Key = pickKey).Single

                                dCell21.ForeColor = queryPickStatus.Value.PickColor
                                If dCell21.ForeColor = WinningTeamForeColor Then
                                    dCell21.BackColor = WinningTeamBackColor
                                ElseIf dCell21.ForeColor = LosingTeamForeColor Then
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

                            Dim pickKey = user1 + GameUpdateCollectionSorted("game11").GameCode


                            Dim dCell22 As New TableCell
                            dCell22.Text = GameUpdateCollectionSorted("game11").HomeTeamAvailability(user1)
                            If dCell22.Text = "L" Or dCell22.Text = "P" Then

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                        Where qPS1.Key = pickKey).Single

                                dCell22.ForeColor = queryPickStatus.Value.PickColor
                                If dCell22.ForeColor = WinningTeamForeColor Then
                                    dCell22.BackColor = WinningTeamBackColor
                                ElseIf dCell22.ForeColor = LosingTeamForeColor Then
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

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                        Where qPS1.Key = pickKey).Single

                                dCell23.ForeColor = queryPickStatus.Value.PickColor
                                If dCell23.ForeColor = WinningTeamForeColor Then
                                    dCell23.BackColor = WinningTeamBackColor
                                ElseIf dCell23.ForeColor = LosingTeamForeColor Then
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

                            Dim pickKey = user1 + GameUpdateCollectionSorted("game12").GameCode


                            Dim dCell24 As New TableCell
                            dCell24.Text = GameUpdateCollectionSorted("game12").HomeTeamAvailability(user1)
                            If dCell24.Text = "L" Or dCell24.Text = "P" Then

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                        Where qPS1.Key = pickKey).Single

                                dCell24.ForeColor = queryPickStatus.Value.PickColor
                                If dCell24.ForeColor = WinningTeamForeColor Then
                                    dCell24.BackColor = WinningTeamBackColor
                                ElseIf dCell24.ForeColor = LosingTeamForeColor Then
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

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                         Where qPS1.Key = pickKey).Single

                                dCell25.ForeColor = queryPickStatus.Value.PickColor
                                If dCell25.ForeColor = WinningTeamForeColor Then
                                    dCell25.BackColor = WinningTeamBackColor
                                ElseIf dCell25.ForeColor = LosingTeamForeColor Then
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

                            Dim pickKey = user1 + GameUpdateCollectionSorted("game13").GameCode


                            Dim dCell26 As New TableCell
                            dCell26.Text = GameUpdateCollectionSorted("game13").HomeTeamAvailability(user1)
                            If dCell26.Text = "L" Or dCell26.Text = "P" Then

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                         Where qPS1.Key = pickKey).Single

                                dCell26.ForeColor = queryPickStatus.Value.PickColor
                                If dCell26.ForeColor = WinningTeamForeColor Then
                                    dCell26.BackColor = WinningTeamBackColor
                                ElseIf dCell26.ForeColor = LosingTeamForeColor Then
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

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                          Where qPS1.Key = pickKey).Single

                                dCell27.ForeColor = queryPickStatus.Value.PickColor
                                If dCell27.ForeColor = WinningTeamForeColor Then
                                    dCell27.BackColor = WinningTeamBackColor
                                ElseIf dCell27.ForeColor = LosingTeamForeColor Then
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

                            Dim pickKey = user1 + GameUpdateCollectionSorted("game14").GameCode


                            Dim dCell28 As New TableCell
                            dCell28.Text = GameUpdateCollectionSorted("game14").HomeTeamAvailability(user1)
                            If dCell28.Text = "L" Or dCell28.Text = "P" Then

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                         Where qPS1.Key = pickKey).Single

                                dCell28.ForeColor = queryPickStatus.Value.PickColor
                                If dCell28.ForeColor = WinningTeamForeColor Then
                                    dCell28.BackColor = WinningTeamBackColor
                                ElseIf dCell28.ForeColor = LosingTeamForeColor Then
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

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                        Where qPS1.Key = pickKey).Single

                                dCell29.ForeColor = queryPickStatus.Value.PickColor
                                If dCell29.ForeColor = WinningTeamForeColor Then
                                    dCell29.BackColor = WinningTeamBackColor
                                ElseIf dCell29.ForeColor = LosingTeamForeColor Then
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

                            Dim pickKey = user1 + GameUpdateCollectionSorted("game15").GameCode


                            Dim dCell30 As New TableCell
                            dCell30.Text = GameUpdateCollectionSorted("game15").HomeTeamAvailability(user1)
                            If dCell30.Text = "L" Or dCell30.Text = "P" Then

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                        Where qPS1.Key = pickKey).Single

                                dCell30.ForeColor = queryPickStatus.Value.PickColor
                                If dCell30.ForeColor = WinningTeamForeColor Then
                                    dCell30.BackColor = WinningTeamBackColor
                                ElseIf dCell30.ForeColor = LosingTeamForeColor Then
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

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                        Where qPS1.Key = pickKey).Single

                                dCell31.ForeColor = queryPickStatus.Value.PickColor
                                If dCell31.ForeColor = WinningTeamForeColor Then
                                    dCell31.BackColor = WinningTeamBackColor
                                ElseIf dCell31.ForeColor = LosingTeamForeColor Then
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

                            Dim pickKey = user1 + GameUpdateCollectionSorted("game16").GameCode


                            Dim dCell32 As New TableCell
                            dCell32.Text = GameUpdateCollectionSorted("game16").HomeTeamAvailability(user1)
                            If dCell32.Text = "L" Or dCell32.Text = "P" Then

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                         Where qPS1.Key = pickKey).Single

                                dCell32.ForeColor = queryPickStatus.Value.PickColor
                                If dCell32.ForeColor = WinningTeamForeColor Then
                                    dCell32.BackColor = WinningTeamBackColor
                                ElseIf dCell32.ForeColor = LosingTeamForeColor Then
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

                                Dim queryPickStatus = (From qPS1 In PickStatusCollection
                                                        Where qPS1.Key = pickKey).Single

                                dCell33.ForeColor = queryPickStatus.Value.PickColor
                                If dCell33.ForeColor = WinningTeamForeColor Then
                                    dCell33.BackColor = WinningTeamBackColor
                                ElseIf dCell33.ForeColor = LosingTeamForeColor Then
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Response.Redirect("~/LosersPool/LoserPoolHome.aspx")

        Catch ex As Exception

        End Try

    End Sub

End Class