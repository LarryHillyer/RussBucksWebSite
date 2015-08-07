
Imports System.Linq

Imports RussBucksPools.LosersPool.Models
Imports RussBucksPools.JoinPools.Models

Public Class ChangeUserPick2
    Inherits System.Web.UI.Page

    Private TeamList As New Dictionary(Of String, Team)
    Private ByeList As New Dictionary(Of String, String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim _dbLosersPool As New LosersPoolContext
        Dim _dbPools As New PoolDbContext

        Try
            Using (_dbLosersPool)
                Using (_dbPools)

                    If Not Page.IsPostBack Then

                        Dim poolAlias = CStr(Session("poolAlias"))
                        Dim changeUser = CStr(Session("changeUser"))
                        Dim sport = CStr(Session("Sport"))
                        Dim thisTimePeriod = CStr(Session("TimePeriod"))
                        Dim cronJobName = CStr(Session("cronJobName"))

                        UserHandle1.Text = changeUser

                        Dim TimePeriod = CStr(Session("TimePeriod"))

                        Dim queryEName = (From user1 In _dbPools.BarPoolList
                                           Where user1.UserName = changeUser And user1.PoolAlias = poolAlias
                                           Select user1.UserId).Single

                        Dim teams1 = (From teams2 In _dbPools.Teams
                                      Where teams2.Sport = sport And teams2.TeamName <> "dummy").ToList

                        Dim byeTeams1 = (From byeteam1 In _dbLosersPool.ByeTeamsList
                                         Where byeteam1.TimePeriod = thisTimePeriod And byeteam1.CronJob = cronJobName And byeteam1.TeamName <> "dummy").ToList

                        For Each byeTeam2 In byeTeams1
                            ByeList.Add(byeTeam2.TeamName, byeTeam2.TeamName)
                        Next

                        Dim cUPickList = New MyPickList(queryEName, TimePeriod, poolAlias, teams1, ByeList, TeamList)


                        Dim queryUserChoice = (From user1 In _dbLosersPool.UserChoicesList
                                              Where user1.UserName = changeUser And user1.TimePeriod = TimePeriod And user1.PoolAlias = poolAlias
                                              Select user1.UserPick).SingleOrDefault

                        If queryUserChoice Is Nothing Then
                            Exit Sub
                        End If


                        Dim cnt = 0
                        If cUPickList.MyPicks.PossibleUserPicks Is Nothing Then
                        Else
                            For Each pick1 In cUPickList.MyPicks.PossibleUserPicks
                                If pick1.Key = queryUserChoice Then
                                    Exit For
                                End If
                                cnt = cnt + 1
                            Next
                        End If

                        Dim PossibleUserPicks1 As New List(Of String)

                        Dim queryMyPicks = (From qMP In _dbLosersPool.MyPicks).ToList

                        If queryMyPicks.Count > 0 Then
                            For Each pick1 In queryMyPicks
                                _dbLosersPool.MyPicks.Remove(pick1)
                            Next
                        End If

                        For Each pick1 In cUPickList.MyPicks.PossibleUserPicks

                            Dim querySchedule = (From qS In _dbLosersPool.ScheduleEntities
                                                 Where qS.GameCode = pick1.Value.gameCode And qS.CronJob = cronJobName).SingleOrDefault
                            If querySchedule Is Nothing Then
                            Else
                                If querySchedule.MultipleGamesAreScheduled = False Then
                                    PossibleUserPicks1.Add(pick1.Key)
                                Else
                                    PossibleUserPicks1.Add(pick1.Key + " game" + querySchedule.MultipleGameNumber)
                                End If

                                Dim myPick1 As New MyPick
                                myPick1.PossibleTeam = pick1.Key
                                myPick1.PossibleTeamGameCode = pick1.Value.gameCode

                                _dbLosersPool.MyPicks.Add(myPick1)
                            End If
                        Next

                        _dbLosersPool.SaveChanges()

                        ListBox1.SelectedIndex = cnt

                        ListBox1.DataSource = PossibleUserPicks1
                        ListBox1.DataBind()

                    End If

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim _dbLosersPool As New LosersPoolContext
        Dim _dbPools As New PoolDbContext
        Dim _dbApp As New ApplicationDbContext

        Dim newPick2 = ListBox1.SelectedItem.Text
        Dim nP = newPick2.Split("game")
        Dim newPick = nP(0)

        Dim changeUser = CStr(Session("changeUser"))
        Dim thisTimePeriod = CStr(Session("TimePeriod"))
        Dim poolAlias = CStr(Session("poolAlias"))
        Dim cronJobName = CStr(Session("cronJobName"))

        Try
            Using (_dbLosersPool)
                Using (_dbPools)
                    Using (_dbApp)

                        Dim queryLoser = (From loser1 In _dbLosersPool.LoserList
                                            Where loser1.UserName = changeUser And loser1.PoolAlias = poolAlias).ToList

                        For Each loser1 In queryLoser
                            _dbLosersPool.LoserList.Remove(loser1)
                            _dbLosersPool.SaveChanges()
                        Next

                        Dim teams1 = (From teams2 In _dbPools.Teams
                                    Where teams2.TeamName <> "dummy").ToList

                        Dim queryMyPicks = (From qMP In _dbLosersPool.MyPicks
                                            Where qMP.PossibleTeam = newPick).SingleOrDefault


                        Dim queryUserChoice = (From user1 In _dbLosersPool.UserChoicesList
                                             Where user1.UserName = changeUser And user1.TimePeriod = thisTimePeriod _
                                             And user1.PoolAlias = poolAlias And user1.CronJob = cronJobName).Single

                        Dim queryUserPicks1 = (From user1 In _dbLosersPool.UserPicks
                                                Where user1.PoolAlias = poolAlias And user1.CronJobName = cronJobName _
                                                And user1.UserID = queryUserChoice.UserID And user1.TimePeriod = thisTimePeriod And user1.UserPickPostponed = False).SingleOrDefault

                        Dim querySchedule = (From game1 In _dbLosersPool.ScheduleEntities
                                             Where game1.CronJob = cronJobName And game1.GameCode = queryMyPicks.PossibleTeamGameCode And game1.TimePeriod = thisTimePeriod).Single

                        If queryUserPicks1 Is Nothing Then
                        Else
                            If queryUserPicks1 Is Nothing Then
                            Else
                                _dbLosersPool.UserPicks.Remove(queryUserPicks1)
                                _dbLosersPool.SaveChanges()
                            End If

                        End If

                        Dim userPick1 As New UserPick
                        userPick1.UserID = queryUserChoice.UserID
                        userPick1.TimePeriod = thisTimePeriod
                        userPick1.PoolAlias = poolAlias
                        userPick1.CronJobName = cronJobName
                        userPick1.UserPick1 = newPick
                        userPick1.GameCode = queryMyPicks.PossibleTeamGameCode

                        If CInt(querySchedule.HomeScore) > CInt(querySchedule.AwayScore) Then
                            userPick1.PickIsTied = False
                            If userPick1.UserPick1 = querySchedule.HomeTeam Then
                                userPick1.PickIsWinning = False
                            ElseIf userPick1.UserPick1 = querySchedule.AwayTeam Then
                                userPick1.PickIsWinning = True
                            End If
                        ElseIf CInt(querySchedule.AwayScore) > CInt(querySchedule.HomeScore) Then
                            userPick1.PickIsTied = False
                            If userPick1.UserPick1 = querySchedule.HomeTeam Then
                                userPick1.PickIsWinning = True
                            ElseIf userPick1.UserPick1 = querySchedule.AwayTeam Then
                                userPick1.PickIsWinning = False
                            End If
                        Else
                            userPick1.PickIsTied = True
                            userPick1.PickIsWinning = False
                        End If

                        userPick1.UserPickPostponed = False

                        _dbLosersPool.UserPicks.Add(userPick1)

                        Dim queryUserPicks2 = (From qUP2 In _dbLosersPool.UserPicks
                                               Where qUP2.CronJobName = cronJobName And qUP2.PoolAlias = poolAlias And _
                                               qUP2.UserID = queryUserChoice.UserID).ToList

                        Dim userIsWinning = False
                        Dim userIsTied = True

                        For Each pick1 In queryUserPicks2
                            If pick1.PickIsTied = True Then
                            ElseIf pick1.PickIsWinning = True Then
                                userIsTied = False
                                userIsWinning = True
                            ElseIf pick1.PickIsWinning = False Then
                                userIsTied = False
                                userIsWinning = False
                                Exit For
                            End If
                        Next

                        queryUserChoice.UserIsWinning = userIsWinning
                        queryUserChoice.UserIsTied = userIsTied

                        Dim oldPick = queryUserChoice.UserPick
                        queryUserChoice.UserPick = newPick
                        queryUserChoice.PickedGameCode = queryMyPicks.PossibleTeamGameCode
                        queryUserChoice.AdministrationPick = True
                        queryUserChoice.Contender = True

                        If oldPick = teams1(0).TeamName Then
                            queryUserChoice.Team1Available = True
                        ElseIf oldPick = teams1(1).TeamName Then
                            queryUserChoice.Team2Available = True
                        ElseIf oldPick = teams1(2).TeamName Then
                            queryUserChoice.Team3Available = True
                        ElseIf oldPick = teams1(3).TeamName Then
                            queryUserChoice.Team4Available = True
                        ElseIf oldPick = teams1(4).TeamName Then
                            queryUserChoice.Team5Available = True
                        ElseIf oldPick = teams1(5).TeamName Then
                            queryUserChoice.Team6Available = True
                        ElseIf oldPick = teams1(6).TeamName Then
                            queryUserChoice.Team7Available = True
                        ElseIf oldPick = teams1(7).TeamName Then
                            queryUserChoice.Team8Available = True
                        ElseIf oldPick = teams1(8).TeamName Then
                            queryUserChoice.Team9Available = True
                        ElseIf oldPick = teams1(9).TeamName Then
                            queryUserChoice.Team10Available = True
                        ElseIf oldPick = teams1(10).TeamName Then
                            queryUserChoice.Team11Available = True
                        ElseIf oldPick = teams1(11).TeamName Then
                            queryUserChoice.Team12Available = True
                        ElseIf oldPick = teams1(12).TeamName Then
                            queryUserChoice.Team13Available = True
                        ElseIf oldPick = teams1(13).TeamName Then
                            queryUserChoice.Team14Available = True
                        ElseIf oldPick = teams1(14).TeamName Then
                            queryUserChoice.Team15Available = True
                        ElseIf oldPick = teams1(15).TeamName Then
                            queryUserChoice.Team16Available = True
                        ElseIf oldPick = teams1(16).TeamName Then
                            queryUserChoice.Team17Available = True
                        ElseIf oldPick = teams1(17).TeamName Then
                            queryUserChoice.Team18Available = True
                        ElseIf oldPick = teams1(18).TeamName Then
                            queryUserChoice.Team19Available = True
                        ElseIf oldPick = teams1(19).TeamName Then
                            queryUserChoice.Team20Available = True
                        ElseIf oldPick = teams1(20).TeamName Then
                            queryUserChoice.Team21Available = True
                        ElseIf oldPick = teams1(21).TeamName Then
                            queryUserChoice.Team22Available = True
                        ElseIf oldPick = teams1(22).TeamName Then
                            queryUserChoice.Team23Available = True
                        ElseIf oldPick = teams1(23).TeamName Then
                            queryUserChoice.Team24Available = True
                        ElseIf oldPick = teams1(24).TeamName Then
                            queryUserChoice.Team25Available = True
                        ElseIf oldPick = teams1(25).TeamName Then
                            queryUserChoice.Team26Available = True
                        ElseIf oldPick = teams1(26).TeamName Then
                            queryUserChoice.Team27Available = True
                        ElseIf oldPick = teams1(27).TeamName Then
                            queryUserChoice.Team28Available = True
                        ElseIf oldPick = teams1(28).TeamName Then
                            queryUserChoice.Team29Available = True
                        ElseIf oldPick = teams1(29).TeamName Then
                            queryUserChoice.Team30Available = True
                        ElseIf oldPick = teams1(30).TeamName Then
                            queryUserChoice.Team31Available = True
                        ElseIf oldPick = teams1(31).TeamName Then
                            queryUserChoice.Team32Available = True

                        End If

                        If newPick = teams1(0).TeamName Then
                            queryUserChoice.Team1Available = False
                        ElseIf newPick = teams1(1).TeamName Then
                            queryUserChoice.Team2Available = False
                        ElseIf newPick = teams1(2).TeamName Then
                            queryUserChoice.Team3Available = False
                        ElseIf newPick = teams1(3).TeamName Then
                            queryUserChoice.Team4Available = False
                        ElseIf newPick = teams1(4).TeamName Then
                            queryUserChoice.Team5Available = False
                        ElseIf newPick = teams1(5).TeamName Then
                            queryUserChoice.Team6Available = False
                        ElseIf newPick = teams1(6).TeamName Then
                            queryUserChoice.Team7Available = False
                        ElseIf newPick = teams1(7).TeamName Then
                            queryUserChoice.Team8Available = False
                        ElseIf newPick = teams1(8).TeamName Then
                            queryUserChoice.Team9Available = False
                        ElseIf newPick = teams1(9).TeamName Then
                            queryUserChoice.Team10Available = False
                        ElseIf newPick = teams1(10).TeamName Then
                            queryUserChoice.Team11Available = False
                        ElseIf newPick = teams1(11).TeamName Then
                            queryUserChoice.Team12Available = False
                        ElseIf newPick = teams1(12).TeamName Then
                            queryUserChoice.Team13Available = False
                        ElseIf newPick = teams1(13).TeamName Then
                            queryUserChoice.Team14Available = False
                        ElseIf newPick = teams1(14).TeamName Then
                            queryUserChoice.Team15Available = False
                        ElseIf newPick = teams1(15).TeamName Then
                            queryUserChoice.Team16Available = False
                        ElseIf newPick = teams1(16).TeamName Then
                            queryUserChoice.Team17Available = False
                        ElseIf newPick = teams1(17).TeamName Then
                            queryUserChoice.Team18Available = False
                        ElseIf newPick = teams1(18).TeamName Then
                            queryUserChoice.Team19Available = False
                        ElseIf newPick = teams1(19).TeamName Then
                            queryUserChoice.Team20Available = False
                        ElseIf newPick = teams1(20).TeamName Then
                            queryUserChoice.Team21Available = False
                        ElseIf newPick = teams1(21).TeamName Then
                            queryUserChoice.Team22Available = False
                        ElseIf newPick = teams1(22).TeamName Then
                            queryUserChoice.Team23Available = False
                        ElseIf newPick = teams1(23).TeamName Then
                            queryUserChoice.Team24Available = False
                        ElseIf newPick = teams1(24).TeamName Then
                            queryUserChoice.Team25Available = False
                        ElseIf newPick = teams1(25).TeamName Then
                            queryUserChoice.Team26Available = False
                        ElseIf newPick = teams1(26).TeamName Then
                            queryUserChoice.Team27Available = False
                        ElseIf newPick = teams1(27).TeamName Then
                            queryUserChoice.Team28Available = False
                        ElseIf newPick = teams1(28).TeamName Then
                            queryUserChoice.Team29Available = False
                        ElseIf newPick = teams1(29).TeamName Then
                            queryUserChoice.Team30Available = False
                        ElseIf newPick = teams1(30).TeamName Then
                            queryUserChoice.Team31Available = False
                        ElseIf newPick = teams1(31).TeamName Then
                            queryUserChoice.Team32Available = False

                        End If

                        _dbLosersPool.SaveChanges()

                        Response.Redirect("~/LosersPool/LoserPoolHome.aspx")

                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class