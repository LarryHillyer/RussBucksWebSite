Imports RussBucksPools.LosersPool.Models

Namespace LosersPool.Models
    Public Class ContenderStatus

        Public Shared Sub UpdateContenderStatus(TimePeriod As String, queryGame As List(Of ScoringUpdate), thisTimePeriod As String, timePeriodIncrement As Int32, poolAlias As String, teams1 As List(Of Team), sport As String, timePeriodName As String)
            Dim _dbLoserPool As New LosersPoolContext

            Try
                Using (_dbLoserPool)
                    Dim queryUserChoices1 = (From user2 In _dbLoserPool.UserChoicesList
                        Where user2.TimePeriod = TimePeriod And user2.Contender = True And user2.PoolAlias = poolAlias And user2.Sport = sport
                        Select user2).ToList

                    For Each user1 In queryUserChoices1

                        ' Make sure contender is not  already on the loser list
                        Dim queryLoser = (From loser1 In _dbLoserPool.LoserList
                                          Where loser1.UserName = user1.UserName And user1.PoolAlias = poolAlias And loser1.Sport = sport
                                          Select loser1).ToList

                        If queryLoser.Count = 0 Then

                            If user1.UserPick Is Nothing Or user1.UserPick = "" Then
                                ' user1 is a loser
                                user1.Contender = False

                                Dim loser1 = New Loser
                                loser1.ListId = _dbLoserPool.LoserList.Count + 1
                                loser1.UserId = user1.UserID
                                loser1.UserName = user1.UserName
                                loser1.TimePeriod = user1.TimePeriod
                                loser1.TimePeriodInt = CInt(Mid(user1.TimePeriod, Len(user1.TimePeriod)))
                                loser1.LosingPick = "Not Made"
                                loser1.Sport = sport
                                loser1.PoolAlias = poolAlias
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
                                        user2.TimePeriod = timePeriodName + CStr(CInt(Mid(thisTimePeriod, Len(thisTimePeriod))) + timePeriodIncrement)
                                        user2.Team1Available = user1.Team1Available
                                        user2.Team2Available = user1.Team2Available
                                        user2.Team3Available = user1.Team3Available
                                        user2.Team4Available = user1.Team4Available
                                        user2.Team5Available = user1.Team5Available
                                        user2.Team6Available = user1.Team6Available
                                        user2.Team7Available = user1.Team7Available
                                        user2.Team8Available = user1.Team8Available
                                        user2.Team9Available = user1.Team9Available
                                        user2.Team10Available = user1.Team10Available
                                        user2.Team11Available = user1.Team11Available
                                        user2.Team12Available = user1.Team12Available
                                        user2.Team13Available = user1.Team13Available
                                        user2.Team14Available = user1.Team14Available
                                        user2.Team15Available = user1.Team15Available
                                        user2.Team16Available = user1.Team16Available
                                        user2.Team17Available = user1.Team17Available
                                        user2.Team18Available = user1.Team18Available
                                        user2.Team19Available = user1.Team19Available
                                        user2.Team20Available = user1.Team20Available
                                        user2.Team21Available = user1.Team21Available
                                        user2.Team22Available = user1.Team22Available
                                        user2.Team23Available = user1.Team23Available
                                        user2.Team24Available = user1.Team24Available
                                        user2.Team25Available = user1.Team25Available
                                        user2.Team26Available = user1.Team26Available
                                        user2.Team27Available = user1.Team27Available
                                        user2.Team28Available = user1.Team28Available
                                        user2.Team29Available = user1.Team29Available
                                        user2.Team30Available = user1.Team30Available
                                        user2.Team31Available = user1.Team31Available
                                        user2.Team32Available = user1.Team32Available
                                        user2.PoolAlias = poolAlias
                                        user2.Sport = user1.Sport
                                        user2.Contender = True
                                        user2.UserPick = user1.UserPick
                                        user2 = SetContendersPickToFalse(user2, teams1)
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
                                        loser1.TimePeriod = user1.TimePeriod
                                        loser1.TimePeriodInt = CInt(Mid(user1.TimePeriod, Len(user1.TimePeriod)))
                                        loser1.LosingPick = user1.UserPick
                                        loser1.Sport = sport
                                        loser1.PoolAlias = poolAlias
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
                                        user2.TimePeriod = timePeriodName + CStr(CInt(Mid(thisTimePeriod, Len(thisTimePeriod))) + timePeriodIncrement)  'TimePeriod
                                        user2.Team1Available = user1.Team1Available
                                        user2.Team2Available = user1.Team2Available
                                        user2.Team3Available = user1.Team3Available
                                        user2.Team4Available = user1.Team4Available
                                        user2.Team5Available = user1.Team5Available
                                        user2.Team6Available = user1.Team6Available
                                        user2.Team7Available = user1.Team7Available
                                        user2.Team8Available = user1.Team8Available
                                        user2.Team9Available = user1.Team9Available
                                        user2.Team10Available = user1.Team10Available
                                        user2.Team11Available = user1.Team11Available
                                        user2.Team12Available = user1.Team12Available
                                        user2.Team13Available = user1.Team13Available
                                        user2.Team14Available = user1.Team14Available
                                        user2.Team15Available = user1.Team15Available
                                        user2.Team16Available = user1.Team16Available
                                        user2.Team17Available = user1.Team17Available
                                        user2.Team18Available = user1.Team18Available
                                        user2.Team19Available = user1.Team19Available
                                        user2.Team20Available = user1.Team20Available
                                        user2.Team21Available = user1.Team21Available
                                        user2.Team22Available = user1.Team22Available
                                        user2.Team23Available = user1.Team23Available
                                        user2.Team24Available = user1.Team24Available
                                        user2.Team25Available = user1.Team25Available
                                        user2.Team26Available = user1.Team26Available
                                        user2.Team27Available = user1.Team27Available
                                        user2.Team28Available = user1.Team28Available
                                        user2.Team29Available = user1.Team29Available
                                        user2.Team30Available = user1.Team30Available
                                        user2.Team31Available = user1.Team31Available
                                        user2.Team32Available = user1.Team32Available

                                        user2.PoolAlias = poolAlias
                                        user2.Sport = user1.Sport
                                        user2.Contender = True
                                        user2.UserPick = user1.UserPick
                                        user2 = SetContendersPickToFalse(user2, teams1)
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
                                        loser1.TimePeriod = user1.TimePeriod
                                        loser1.TimePeriodInt = CInt(Mid(user1.TimePeriod, Len(user1.TimePeriod)))
                                        loser1.LosingPick = user1.UserPick
                                        loser1.PoolAlias = poolAlias
                                        loser1.Sport = sport
                                        _dbLoserPool.LoserList.Add(loser1)
                                        _dbLoserPool.SaveChanges()
                                        Exit For
                                    End If

                                End If
                            End If
                        Next
                    Next

                    _dbLoserPool.SaveChanges()

                End Using
            Catch ex As Exception

            End Try
        End Sub

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

    End Class
End Namespace