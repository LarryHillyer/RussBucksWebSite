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


Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Submit1_Click(sender As Object, e As EventArgs)
        Dim _dbPools = New PoolDbContext
        Dim _dbApp = New ApplicationDbContext

        Dim queryPoolAdministrators = (From poolAdmin1 In _dbPools.PoolAdministrators
                                       Where poolAdmin1.PoolAdministratorAlias = CommisionerCode1.Text).ToList

        If queryPoolAdministrators.Count = 0 Then
            Response.Redirect("~/Account/Register.aspx")
        Else
            Submit1.Visible = False
            CommisionerCode1.Visible = False
            Panel1.Visible = True
        End If

    End Sub

    Protected Sub Agree1_Click(sender As Object, e As EventArgs)
        Dim dummy = "dummy"
    End Sub

    Protected Sub Disagree1_Click(sender As Object, e As EventArgs)
        Dim dummy = "dummy"
    End Sub


End Class