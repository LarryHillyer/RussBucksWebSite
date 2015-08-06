Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Owin

Imports RussBucksPools.LosersPool.Models
Imports RussBucksPools.JoinPools.Models


Public Class Eula
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Submit1_Click(sender As Object, e As EventArgs)
        Dim _dbApp = New ApplicationDbContext
        Dim _dbPools = New PoolDbContext
        Dim userID = CStr(Session("userId"))

        Dim userID2 = CStr(Session("userId2"))

        Try
            Using (_dbApp)
                Using (_dbPools)



                    Dim queryPoolAdministrators = (From poolAdmin1 In _dbPools.PoolAdministrators
                               Where poolAdmin1.PoolAdministratorAlias = CommisionerCode1.Text).ToList

                    If queryPoolAdministrators.Count = 0 Then
                        Dim queryUser = (From user1 In _dbApp.Users
                                         Where user1.UserName = userID2).Single

                        _dbApp.Users.Remove(queryUser)
                        _dbApp.SaveChanges()

                        Session("userId") = Nothing
                        Session("userId2") = Nothing

                        Response.Redirect("~/")

                    Else
                        Dim queryUser = (From user1 In _dbApp.Users
                                        Where user1.UserName = userID2).Single

                        queryUser.CommissionerCode = CommisionerCode1.Text
                        _dbApp.SaveChanges()

                        Submit1.Visible = False
                        CommisionerCode1.Visible = False
                        Panel1.Visible = True
                    End If

                End Using
            End Using
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub Disagree1_Click(sender As Object, e As EventArgs)
        Dim _dbApp1 As New ApplicationDbContext
        Dim _dbPools1 As New PoolDbContext
        Dim userID = CStr(Session("userId"))
        Dim userID2 = CStr(Session("userId2"))
        Try
            Using (_dbApp1)
                Using (_dbPools1)
                    Dim queryUser = (From user1 In _dbApp1.Users
                                    Where user1.UserName = userID2).Single

                    _dbApp1.Users.Remove(queryUser)
                    _dbApp1.SaveChanges()

                    Session("userId") = Nothing
                    Session("userId2") = Nothing

                    Response.Redirect("~/")

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Agree1_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Default.aspx")
    End Sub
End Class