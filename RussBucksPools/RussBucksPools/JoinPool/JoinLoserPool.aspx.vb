Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Linq

Imports RussBucksPools.LosersPool.Models

Imports RussBucksPools.JoinPools
Imports RussBucksPools.JoinPools.Models

Public Class JoinLoserPool
    Inherits System.Web.UI.Page

    Private _dbLoserPool As New LosersPoolContext
    Private _dbPools As New PoolDbContext

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        UserNameTextBox.Focus()
    End Sub


    Public Sub JoinLoserPoolBtn_Click(sender As Object, e As EventArgs)

        Dim userName As String = CStr(UserNameTextBox.Text)

        If Not (userName = "") Then
            Try
                Using (_dbLoserPool)
                    Using (_dbPools)

                        Dim poolAlias = CStr(Session("poolAlias"))
                        Dim EName As String = CStr(Session("userId"))

                        Dim queryBarPoolList = (From bPL1 In _dbPools.BarPoolList
                            Where bPL1.UserId = EName And bPL1.PoolAlias = poolAlias).SingleOrDefault

                        Dim queryPoolAdmin = Nothing

                        If queryBarPoolList Is Nothing Then
                        Else
                            queryPoolAdmin = (From pool1 In _dbPools.PoolParameters
                                              Where pool1.poolAdministrator = EName And pool1.poolAlias = poolAlias).SingleOrDefault
                        End If

                        Dim queryPoolParam1 = (From poolParam1 In _dbPools.PoolParameters
                                               Where poolParam1.poolAlias = poolAlias).Single


                        If queryBarPoolList Is Nothing And queryPoolAdmin Is Nothing Then
                            Dim newUser1 As New PoolAlias1

                            newUser1.PoolName = queryPoolParam1.poolName
                            newUser1.Sport = queryPoolParam1.Sport
                            newUser1.UserId = EName
                            newUser1.PoolAlias = poolAlias
                            newUser1.UserName = UserNameTextBox.Text
                            _dbPools.BarPoolList.Add(newUser1)
                            _dbPools.SaveChanges()
                        ElseIf queryPoolAdmin Is Nothing Then

                            UserNameTextBox.Text = ""
                            UserNameTextBox.Focus()
                            JoinError.Text = "ERROR: User name is already in use"
                            Response.Redirect("./JoinLoserPool.aspx")
                        ElseIf Not queryPoolAdmin Is Nothing Then
                            Dim queryBarPoolList1 = (From user1 In _dbPools.BarPoolList
                                                     Where user1.UserId = EName And user1.PoolAlias = poolAlias).Single
                            queryBarPoolList1.UserName = UserNameTextBox.Text
                            _dbPools.SaveChanges()
                        End If



                        Dim queryPoolParam = (From param1 In _dbPools.PoolParameters
                                          Where param1.poolAlias = poolAlias).Single


                        Dim queryUserChoices = (From user1 In _dbLoserPool.UserChoicesList
                                               Where user1.UserID = EName And user1.PoolAlias = poolAlias).ToList

                        If queryUserChoices.Count = 0 Then

                            Dim newUser3 As New UserChoices
                            newUser3.UserID = EName
                            newUser3.UserName = userName
                            newUser3.PoolAlias = poolAlias
                            newUser3.TimePeriod = queryPoolParam.timePeriodName + "1"
                            newUser3.Contender = True
                            newUser3.Team1Available = True
                            newUser3.Team2Available = True
                            newUser3.Team3Available = True
                            newUser3.Team4Available = True
                            newUser3.Team5Available = True
                            newUser3.Team6Available = True
                            newUser3.Team7Available = True
                            newUser3.Team8Available = True
                            newUser3.Team9Available = True
                            newUser3.Team10Available = True
                            newUser3.Team11Available = True
                            newUser3.Team12Available = True
                            newUser3.Team13Available = True
                            newUser3.Team14Available = True
                            newUser3.Team15Available = True
                            newUser3.Team16Available = True
                            newUser3.Team17Available = True
                            newUser3.Team18Available = True
                            newUser3.Team19Available = True
                            newUser3.Team20Available = True
                            newUser3.Team21Available = True
                            newUser3.Team22Available = True
                            newUser3.Team23Available = True
                            newUser3.Team24Available = True
                            newUser3.Team25Available = True
                            newUser3.Team26Available = True
                            newUser3.Team27Available = True
                            newUser3.Team28Available = True
                            newUser3.Team29Available = True
                            newUser3.Team30Available = True
                            newUser3.Team31Available = True
                            newUser3.Team32Available = True

                            newUser3.AdministrationPick = False
                            newUser3.Sport = queryPoolParam.Sport
                            newUser3.UserIsTied = False
                            newUser3.UserIsWinning = False
                            newUser3.UserPickPostponed = False


                            _dbLoserPool.UserChoicesList.Add(newUser3)
                            _dbLoserPool.SaveChanges()

                        End If

                        Response.Redirect("~/Default.aspx")


                    End Using
                End Using

            Catch ex As Exception

            End Try
        Else
            UserNameTextBox.Text = ""
            UserNameTextBox.Focus()
            JoinError.Text = "ERROR:  Invalid user name"
        End If

    End Sub



End Class