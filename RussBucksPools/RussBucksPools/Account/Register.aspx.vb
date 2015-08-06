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
Partial Public Class Register
    Inherits Page

    Private _dbApp As New ApplicationDbContext
    Private _dbPools As New PoolDbContext
    Private _dbLoserPool As New LosersPoolContext


    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)

        Dim userName As String = Email.Text
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim signInManager = Context.GetOwinContext().Get(Of ApplicationSignInManager)()
        Dim user = New ApplicationUser() With {.UserName = userName, .Email = userName}
        Dim result = manager.Create(user, Password.Text)
        If result.Succeeded Then

            ' For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            ' Dim code = manager.GenerateEmailConfirmationToken(user.Id)
            ' Dim callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request)
            ' manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=""" & callbackUrl & """>here</a>.")

            signInManager.SignIn(user, isPersistent:=False, rememberBrowser:=False)

            Try
                Using (_dbApp)
                    Dim queryFolders = (From folder1 In _dbApp.AppFolders).SingleOrDefault
                    Session("rootFolder") = queryFolders.rootFolder


                    Using (_dbPools)
                        Dim userId As String
                        Dim user1 As String = CStr(user.UserName)

                        Dim Ai As Int16 = InStr(user1, "@")
                        Dim Pi As Int16 = InStr(user1, ".")

                        userId = Left(user1, Ai - 1) + Mid(user1, (Ai + 1), Pi - Ai - 1) + Right(user1, Len(user1) - Pi)
                        Session("userId") = userId
                        Session("userId2") = userName

                        'seed Pools table if needed
                        Dim poolList2 = New SeedPools

                        'seed Teams table if needed
                        Dim teamList2 = New SeedTeams

                        SuperUser.SeedSuperUser()

                        Dim querySuperUser1 = (From user2 In _dbApp.SuperUsers
                            Where user2.SuperUserName = userName).SingleOrDefault

                        If querySuperUser1 Is Nothing Then
                            Response.Redirect("~/Account/EULA.aspx")

                        Else
                            Response.Redirect("~/Default.aspx")

                        End If
                    End Using
                End Using
            Catch ex As Exception

            End Try


            'IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
        Else
            ErrorMessage.Text = result.Errors.FirstOrDefault()
        End If

    End Sub

End Class
