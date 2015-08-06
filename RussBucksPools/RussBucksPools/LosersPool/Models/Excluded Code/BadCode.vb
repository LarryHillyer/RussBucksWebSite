Public Class BadCode
                    For Each user1 In GameUpdateCollection("game1").UserHandles


                    For Each game In GameUpdateCollection

    Dim queryUserChoices1 = (From qUC1 In _dbLoserPool5.UserChoicesList
    Where qUC1.CronJob = cronJobName And qUC1.PoolAlias = poolAlias And _
    qUC1.TimePeriod = thisTimePeriod And qUC1.UserName = user1 And qUC1.Contender = True).Single


    Dim queryUserPicks = (From qUP1 In _dbLoserPool5.UserPicks
                          Where qUP1.CronJobName = cronJobName And qUP1.PoolAlias = poolAlias And qUP1.UserID = queryUserChoices1.UserID).ToList

    Dim IsGamePresent = False

                        For Each pick1 In queryUserPicks
                            If pick1.GameCode = game.Value.GameCode Then
                                IsGamePresent = True
                            End If
                        Next

                        If IsGamePresent = False Then
                            Continue For
                        End If

                        If GameUpdateCollection.Count >= 1 Then

                            If GameUpdateCollection("game1").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game1").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game1").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore1.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore1.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If

                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If

                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game1").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game1").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game1").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore1.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore1.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 2 Then

                            If GameUpdateCollection("game2").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game2").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game2").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore2.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore2.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game2").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game2").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game2").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore2.ForeColor = WinningTeamForeColor Then
    Dim userStatus1 As New UserStatus
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore2.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 3 Then
                            If GameUpdateCollection("game3").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game3").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game3").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore3.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore3.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game3").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game3").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game3").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore3.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore3.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 4 Then
                            If GameUpdateCollection("game4").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game4").HomeTeamAvailability(user1) = "L" Or _
                                   GameUpdateCollection("game4").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore4.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore4.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game4").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game4").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game4").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore4.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore4.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 5 Then
                            If GameUpdateCollection("game5").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game5").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game5").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore5.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore5.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                            End If

                            If GameUpdateCollection("game5").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game5").AwayTeamAvailability(user1) = "L" Or _
                                   GameUpdateCollection("game5").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore5.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore5.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                            End If
                        End If

                        If GameUpdateCollection.Count >= 6 Then
                            If GameUpdateCollection("game6").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game6").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game6").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore6.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore6.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                            End If

                            If GameUpdateCollection("game6").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game6").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game6").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore6.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore6.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 7 Then
                            If GameUpdateCollection("game7").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game7").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game7").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore7.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore7.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game7").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game7").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game7").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore7.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore7.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 8 Then
                            If GameUpdateCollection("game8").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game8").HomeTeamAvailability(user1) = "L" Or _
                                   GameUpdateCollection("game8").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore8.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore8.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game8").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game8").AwayTeamAvailability(user1) = "L" Or _
                                   GameUpdateCollection("game8").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore8.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore8.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 9 Then
                            If GameUpdateCollection("game9").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game9").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game9").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore9.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore9.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game9").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game9").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game9").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore9.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore9.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 10 Then
                            If GameUpdateCollection("game10").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game10").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game10").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore10.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore10.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game10").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game10").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game10").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore10.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore10.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 11 Then
                            If GameUpdateCollection("game11").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game11").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game11").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore11.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore11.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game11").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game11").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game11").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore11.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore11.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 12 Then
                            If GameUpdateCollection("game12").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game12").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game12").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore12.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore12.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game12").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game12").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game12").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore12.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore12.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game12").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game12").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game12").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore12.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore12.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 13 Then
                            If GameUpdateCollection("game13").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game13").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game13").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore13.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore13.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game13").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game13").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game13").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore13.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore13.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 14 Then
                            If GameUpdateCollection("game14").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game14").HomeTeamAvailability(user1) = "L" Or _
                                   GameUpdateCollection("game14").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore14.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore14.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game14").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game14").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game14").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore14.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore14.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 15 Then
                            If GameUpdateCollection("game15").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game15").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game15").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore15.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore15.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For

                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game15").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game15").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game15").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore15.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore15.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                        If GameUpdateCollection.Count >= 16 Then
                            If GameUpdateCollection("game16").HomeTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game16").HomeTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game16").HomeTeamAvailability(user1) = "P" Then
    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore16.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore16.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For

                                    End If
                                End If
                            End If

                            If GameUpdateCollection("game16").AwayTeamAvailability.ContainsKey(user1) Then
                                If GameUpdateCollection("game16").AwayTeamAvailability(user1) = "L" Or _
                                    GameUpdateCollection("game16").AwayTeamAvailability(user1) = "P" Then
    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore16.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore16.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next

End Class
