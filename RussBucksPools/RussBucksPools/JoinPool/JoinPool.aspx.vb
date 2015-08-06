Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Owin


Imports RussBucksPools.JoinPools
Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models
Imports RussBucksPools.PlayoffPool.Models


Public Class JoinPool
    Inherits System.Web.UI.Page

    Private PoolsList As New List(Of String)
    Private SportList As New List(Of String)
    Private _dbPools As New PoolDbContext

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        Dim _dbPools As New PoolDbContext

        Dim EName As String = CStr(Session("userId"))



        PoolNameTextBox.Focus()

        Try
            Using (_dbPools)

                If Not Page.IsPostBack Then

                    Dim poolList1 = (From pool1 In _dbPools.PoolAdministrators
                                     Where pool1.PoolAdministrator = EName).ToList

                    If poolList1.Count > 0 Then

                        Admin1.Visible = True
                        CreatePool1.Visible = True
                        Label2.Visible = True
                        Pools1.Visible = True
                        Label4.Visible = True
                        Sport1.Visible = True
                        Label1.Visible = True
                        PoolAlias1.Visible = True
                        Button1.Visible = True
                        DeleteUser1.Visible = True

                        Dim pools2 = (From pool1 In _dbPools.Pools).ToList

                        For Each pool1 In pools2
                            PoolsList.Add(pool1.PoolName)
                        Next

                        Pools1.DataSource = PoolsList
                        Pools1.DataBind()

                        If Not Page.IsPostBack Then
                            Pools1.SelectedIndex = Pools1.Items.IndexOf(Pools1.Items.FindByText("LoserPool"))
                        End If

                        Dim querySports = (From sport1 In _dbPools.Sports
                                           Where sport1.PoolName = Pools1.SelectedItem.Text).ToList

                        For Each sport2 In querySports
                            SportList.Add(sport2.SportName)
                        Next

                        Sport1.DataSource = SportList
                        Sport1.DataBind()

                        If Not Page.IsPostBack Then
                            Sport1.SelectedIndex = Sport1.Items.IndexOf(Sport1.Items.FindByText("football"))
                        End If

                    End If
                End If
            End Using
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub FindPool_Click(sender As Object, e As EventArgs)

        Dim _dbPools As New PoolDbContext
        Dim _dbApp As New ApplicationDbContext
        Dim EName As String = CStr(Session("userId"))
        Dim EName2 As String = CStr(Session("userId2"))
        Dim PoolName1 As String = PoolNameTextBox.Text

        Try

            Using (_dbPools)
                If Not (PoolName1 = "") Then

                    Dim validUser = (From pool1 In _dbPools.BarPoolList
                                       Where pool1.UserId = EName And pool1.PoolAlias = PoolName1).SingleOrDefault

                    If validUser Is Nothing Then
                    Else
                        Label3.ForeColor = Drawing.Color.Red

                        Label3.Text = "Error: User already joined pool"
                        Label3.Visible = True

                        PoolNameTextBox.Text = ""
                        PoolNameTextBox.Focus()

                        Exit Sub
                    End If

                    Dim validUser1 = (From user1 In _dbApp.Users
                                      Where user1.UserName = EName2).Single

                    Dim validPool = (From pool1 In _dbPools.PoolParameters
                                 Where pool1.poolAlias = PoolName1).SingleOrDefault



                    If validPool Is Nothing Then
                        Label3.ForeColor = Drawing.Color.Red

                        Label3.Text = "Error: Invalid Pool"
                        Label3.Visible = True

                        PoolNameTextBox.Text = ""
                        PoolNameTextBox.Focus()

                        Exit Sub
                    Else

                        Dim validPoolAdminCode = (From poolAdmin1 In _dbPools.PoolAdministrators
                          Where poolAdmin1.PoolAdministrator = validPool.poolAdministrator And poolAdmin1.PoolAdministratorAlias = validUser1.CommissionerCode).SingleOrDefault

                        If validPoolAdminCode Is Nothing Then
                            Label3.ForeColor = Drawing.Color.Red

                            Label3.Text = "Error: Invalid Pool"
                            Label3.Visible = True

                            PoolNameTextBox.Text = ""
                            PoolNameTextBox.Focus()

                            Exit Sub

                        End If

                        Session("poolAlias") = PoolName1

                        If validPool.poolName = "LoserPool" Then
                            PoolNameTextBox.Focus()
                            PoolNameTextBox.Text = ""

                            Response.Redirect("~/JoinPool/JoinLoserPool.aspx")
                        ElseIf validPool.poolName = "PlayoffPool" Then
                            PoolNameTextBox.Focus()
                            PoolNameTextBox.Text = ""
                            Response.Redirect("~/JoinPool/JoinPlayoffPool.aspx")
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        Dim _dbPools1 As New PoolDbContext
        Dim userId = CStr(Session("userId"))

        If PoolAlias1.Text Is Nothing Or PoolAlias1.Text = "" Then
            Label5.Text = "Error: Incomplete Information"
            Label5.Visible = True
            Exit Sub
        End If

        Try
            Using (_dbPools1)

                Session("poolAlias") = PoolAlias1.Text

                Dim queryPool = (From pool1 In _dbPools1.BarPoolList
                                 Where pool1.PoolAlias = PoolAlias1.Text).SingleOrDefault

                If queryPool Is Nothing Then



                    Dim user1 As New PoolAlias1
                    user1.UserId = userId
                    user1.PoolName = Pools1.SelectedItem.Text
                    user1.Sport = Sport1.SelectedItem.Text
                    user1.PoolAlias = PoolAlias1.Text

                    _dbPools1.BarPoolList.Add(user1)

                    Dim poolParam1 As New PoolParameter

                    Dim queryPool1 = (From pool1 In _dbPools1.Pools
                                           Where pool1.Sport = Sport1.SelectedItem.Text And pool1.PoolName = Pools1.SelectedItem.Text).Single

                    poolParam1.timePeriodName = queryPool1.timePeriodName
                    poolParam1.timePeriodIncrement = queryPool1.timePeriodIncrement
                    poolParam1.poolAlias = PoolAlias1.Text
                    poolParam1.poolName = Pools1.SelectedItem.Text
                    poolParam1.Sport = Sport1.SelectedItem.Text
                    poolParam1.poolAdministrator = userId

                    _dbPools1.PoolParameters.Add(poolParam1)

                    _dbPools1.SaveChanges()


                    If queryPool1.PoolName = "LoserPool" Then
                        Response.Redirect("./JoinLoserPool.aspx")
                    End If

                Else
                    Label5.Text = "Pool Already Exists"
                    Label5.Visible = True
                End If

            End Using

        Catch ex As Exception

        End Try

    End Sub
End Class