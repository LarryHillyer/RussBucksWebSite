Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Linq

Imports RussBucksPools.PlayoffPool
Imports RussBucksPools.PlayoffPool.Models

Imports RussBucksPools.JoinPools
Imports RussBucksPools.JoinPools.Models


Public Class JoinPlayoffPool
    Inherits System.Web.UI.Page

    Private _dbPlayoffPool As New PlayoffPoolContext
    Private _dbPool As New PoolDbContext


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        UserNameTextBox.Focus()

    End Sub

    Public Sub JoinLoserPoolBtn_Click(sender As Object, e As EventArgs)

        Dim UserName1 As String = CStr(UserNameTextBox.Text)

        If Not (UserName1 = "") Then

            Using (_dbPlayoffPool)


            End Using
        Else
            UserNameTextBox.Text = ""
            UserNameTextBox.Focus()
            JoinError.Text = "ERROR:  Invalid user name"
        End If


    End Sub


End Class