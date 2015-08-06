Imports System
Imports System.Linq
Imports System.Xml.Linq
Imports System.Globalization
Imports System.Threading

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models


Public Class Season_End
    Inherits System.Web.UI.Page

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
                        Using (_dbPools)

                            Dim poolAlias = CStr(Session("poolAlias"))
                            Dim EName = CStr(Session("userId"))
                            Dim staleTimePeriod = CStr(Session("TimePeriod"))

                            Dim cronJob = (From cJN1 In _dbApp.CronJobPools
                                            Where cJN1.CronJobPoolAlias = poolAlias).Single

                            Dim cronJobName = cronJob.CronJobName

                            Dim queryPoolParam1 = (From poolParam1 In _dbPools.PoolParameters
                                                   Where poolParam1.poolAlias = poolAlias And poolParam1.CronJob = cronJobName).Single

                            Dim timePeriodName = queryPoolParam1.timePeriodName
                            Dim sport = queryPoolParam1.Sport

                            Dim thisTimePeriod = timePeriodName + CStr(queryPoolParam1.maxTimePeriod)

                            Dim queryLeagueSize = (From sport1 In _dbPools.Sports
                                              Where sport1.SportName = sport).ToList

                            Dim leagueSize = queryLeagueSize(0).LeagueSize


                            Dim queryTimePeriods = (From timePeriod1 In _dbLoserPool.ScheduleTimePeriods
                                               Where CInt(Mid(timePeriod1.TimePeriod, Len(timePeriod1.TimePeriod), 3)) <= CInt(Mid(thisTimePeriod, Len(thisTimePeriod), 3)) And timePeriod1.CronJob = cronJobName
                                               Order By timePeriod1.TimePeriod Descending).ToList

                            Dim teams1 = (From teams2 In _dbPools.Teams
                                          Where teams2.Sport = sport And teams2.TeamName <> "dummy").ToList

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

    Private Sub CreateContendersTable(thisTimePeriod As String, poolAlias As String, teams1 As List(Of Team), leagueSize As Int32, cronJobName As String)
        GetTableSize(leagueSize)

        GetContendersTableHeader(teams1)

        AddUserStatusToContenderTable(thisTimePeriod, poolAlias, teams1, leagueSize, cronJobName)

    End Sub

    Private Sub CreateLoserTable(thisTimePeriod As String, poolAlias As String, queryTimePeriods As List(Of ScheduleTimePeriod), LoserCollectionSorted As Dictionary(Of String, Loser), cronJobName As String)
        SortLosers(thisTimePeriod, poolAlias, queryTimePeriods, cronJobName)

        AddLosersToLoserTable(LoserCollectionSorted)
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

            Dim _dbLoserPool1 As New LosersPoolContext
            Dim timeState = CStr(Session("timeState"))


            Try
                Using (_dbLoserPool1)
                    Dim dailyUserChoices = (From user1 In _dbLoserPool1.UserChoicesList
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
                            If user1.Team1Available = True Or (user1.UserPick = teams1(0).TeamName And timeState = "Season End") Then
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
                            If user1.Team2Available = True Or (user1.UserPick = teams1(1).TeamName And timeState = "Season End") Then
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
                            If user1.Team3Available = True Or (user1.UserPick = teams1(2).TeamName And timeState = "Season End") Then
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
                            If user1.Team4Available = True Or (user1.UserPick = teams1(3).TeamName And timeState = "Season End") Then
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
                            If user1.Team5Available = True Or (user1.UserPick = teams1(4).TeamName And timeState = "Season End") Then
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
                            If user1.Team6Available = True Or (user1.UserPick = teams1(5).TeamName And timeState = "Season End") Then
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
                            If user1.Team7Available = True Or (user1.UserPick = teams1(6).TeamName And timeState = "Season End") Then
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
                            If user1.Team8Available = True Or (user1.UserPick = teams1(7).TeamName And timeState = "Season End") Then
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
                            If user1.Team9Available = True Or (user1.UserPick = teams1(8).TeamName And timeState = "Season End") Then
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
                            If user1.Team10Available = True Or (user1.UserPick = teams1(9).TeamName And timeState = "Season End") Then
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
                            If user1.Team11Available = True Or (user1.UserPick = teams1(10).TeamName And timeState = "Season End") Then
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
                            If user1.Team12Available = True Or (user1.UserPick = teams1(11).TeamName And timeState = "Season End") Then
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
                            If user1.Team13Available = True Or (user1.UserPick = teams1(12).TeamName And timeState = "Season End") Then
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
                            If user1.Team14Available = True Or (user1.UserPick = teams1(13).TeamName And timeState = "Season End") Then
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
                            If user1.Team15Available = True Or (user1.UserPick = teams1(14).TeamName And timeState = "Season End") Then
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
                            If user1.Team16Available = True Or (user1.UserPick = teams1(15).TeamName And timeState = "Season End") Then
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
                            If user1.Team17Available = True Or (user1.UserPick = teams1(16).TeamName And timeState = "Season End") Then
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
                            If user1.Team18Available = True Or (user1.UserPick = teams1(17).TeamName And timeState = "Season End") Then
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
                            If user1.Team19Available = True Or (user1.UserPick = teams1(18).TeamName And timeState = "Season End") Then
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
                            If user1.Team20Available = True Or (user1.UserPick = teams1(19).TeamName And timeState = "Season End") Then
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
                            If user1.Team21Available = True Or (user1.UserPick = teams1(20).TeamName And timeState = "Season End") Then
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
                            If user1.Team22Available = True Or (user1.UserPick = teams1(21).TeamName And timeState = "Season End") Then
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
                            If user1.Team23Available = True Or (user1.UserPick = teams1(22).TeamName And timeState = "Season End") Then
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
                            If user1.Team24Available = True Or (user1.UserPick = teams1(23).TeamName And timeState = "Season End") Then
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
                            If user1.Team25Available = True Or (user1.UserPick = teams1(24).TeamName And timeState = "Season End") Then
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
                            If user1.Team26Available = True Or (user1.UserPick = teams1(25).TeamName And timeState = "Season End") Then
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
                            If user1.Team27Available = True Or (user1.UserPick = teams1(26).TeamName And timeState = "Season End") Then
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
                            If user1.Team28Available = True Or (user1.UserPick = teams1(27).TeamName And timeState = "Season End") Then
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
                            If user1.Team29Available = True Or (user1.UserPick = teams1(28).TeamName And timeState = "Season End") Then
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
                            If user1.Team30Available = True Or (user1.UserPick = teams1(29).TeamName And timeState = "Season End") Then
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
                            If user1.Team31Available = True Or (user1.UserPick = teams1(30).TeamName And timeState = "Season End") Then
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
                            If user1.Team32Available = True Or (user1.UserPick = teams1(31).TeamName And timeState = "Season End") Then
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

    Private Sub SortLosers(thisTimePeriod As String, poolAlias As String, queryTimePeriods As List(Of ScheduleTimePeriod), cronJobName As String)

        Dim _dbLoserPool2 As New LosersPoolContext

        Try
            Using (_dbLoserPool2)
                For Each timeP1 In queryTimePeriods

                    Dim queryTimePeriodLosers = (From loser1 In _dbLoserPool2.LoserList
                                          Where loser1.TimePeriod = timeP1.TimePeriod And loser1.PoolAlias = poolAlias And loser1.CronJob = cronJobName).ToList

                    For Each loser1 In queryTimePeriodLosers

                        If loser1.LosingPick <> "Not Made" Then
                            Dim loser2 = New Loser
                            loser2.UserName = loser1.UserName
                            loser2.TimePeriod = loser1.TimePeriod
                            loser2.LosingPick = loser1.LosingPick
                            LoserCollectionSorted.Add(loser2.UserName, loser2)
                        End If
                    Next
                    For Each loser1 In queryTimePeriodLosers
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
        Response.Redirect("~/JoinPool/MyPools.aspx")
    End Sub


End Class