Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.ModelBinding

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models

Public Class MyPools
    Inherits System.Web.UI.Page

    Private MyPoolList As New List(Of MyPool1)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If
        Dim _dbPools As New PoolDbContext
        Dim EName As String = CStr(Session("userId"))

        Try
            Using (_dbPools)


                Dim poolUserQuery = (From barPool1 In _dbPools.BarPoolList
                                     Where barPool1.UserId = EName).ToList



                For Each pool1 In poolUserQuery

                    Dim sports1 = (From sport1 In _dbPools.Sports
                                   Where sport1.SportName = pool1.Sport And sport1.PoolName = pool1.PoolName).Single

                    Dim mp1 = New MyPool1
                    mp1.PoolName = pool1.PoolAlias
                    mp1.PageURL = sports1.PageUrl
                    MyPoolList.Add(mp1)
                Next

            End Using
        Catch ex As Exception

        End Try

    End Sub


    Public Function GetMyPools1() As List(Of MyPool1)

        Return MyPoolList

    End Function

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        Dim _dbPools1 As New PoolDbContext
        Dim EName As String = CStr(Session("userId"))
        Dim poolAlias2 = CStr(sender.text)
        Try
            Using (_dbPools1)

                Dim queryPoolName = (From poolName1 In _dbPools1.BarPoolList
                                     Where poolName1.PoolAlias = poolAlias2 And poolName1.UserId = EName).Single

                Dim queryPoolParam = (From param1 In _dbPools1.PoolParameters
                      Where param1.poolAlias = poolAlias2).Single

                If queryPoolParam.CronJob Is Nothing Or queryPoolParam.CronJob = "" Then
                    Response.Redirect("~/JoinPool/InactivePool.aspx")
                End If

                Session("poolAlias") = queryPoolParam.poolAlias
                Session("Sport") = queryPoolParam.Sport
                Session("timePeriodIncrement") = queryPoolParam.timePeriodIncrement
                Session("timePeriodName") = queryPoolParam.timePeriodName
                Session("seasonStartDate") = queryPoolParam.seasonStartDate
                Session("seasonStartTime") = queryPoolParam.seasonStartTime
                Session("cronJobName") = queryPoolParam.CronJob

                If queryPoolName.PoolName = "LoserPool" Then
                    Response.Redirect("../LosersPool/LoserPoolHome.aspx")
                ElseIf queryPoolName.PoolName = "PlayoffPool" Then
                    Response.Redirect("../PlayoffPool/Default.aspx")
                End If

            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class

Public Class MyPool1
    Public Property PoolName As String
    Public Property PageURL As String

End Class


