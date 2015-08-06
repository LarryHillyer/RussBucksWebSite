Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models

Partial Public Class Login
    Inherits Page

    Private _dbApp As New ApplicationDbContext
    Private _dbPools As New PoolDbContext
    Private _dbLoserPool As New LosersPoolContext


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        RegisterHyperLink.NavigateUrl = "Register"
        ' Enable this once you have account confirmation enabled for password reset functionality
        ' ForgotPasswordHyperLink.NavigateUrl = "Forgot"
        OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
        Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        If Not [String].IsNullOrEmpty(returnUrl) Then
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
        End If
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        If IsValid Then
            ' Validate the user password
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()

            ' This doen't count login failures towards account lockout
            ' To enable password failures to trigger lockout, change to shouldLockout := True
            Dim result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout:=False)

            Select Case result
                Case SignInStatus.Success

                    Dim userId As String
                    Dim user1 As String = CStr(Email.Text)

                    Dim Ai As Int16 = InStr(user1, "@")
                    Dim Pi As Int16 = InStr(user1, ".")

                    userId = Left(user1, Ai - 1) + Mid(user1, (Ai + 1), Pi - Ai - 1) + Right(user1, Len(user1) - Pi)
                    Session("userId") = userId
                    Session("userId2") = Email.Text
                    Try
                        Using (_dbApp)
                            Dim queryFolders = (From folder1 In _dbApp.AppFolders).SingleOrDefault
                            Session("rootFolder") = queryFolders.rootFolder
                        End Using
                    Catch ex As Exception

                    End Try

                    'seed Pools table if needed
                    Dim poolList2 = New SeedPools

                    'seed Teams table if needed
                    Dim teamList2 = New SeedTeams

                    SuperUser.SeedSuperUser()

                    Dim sportlist2 = New SeedSports

                    IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                    Exit Select
                Case SignInStatus.LockedOut
                    Response.Redirect("/Account/Lockout")
                    Exit Select
                Case SignInStatus.RequiresVerification
                    Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                    Request.QueryString("ReturnUrl"),
                                                    RememberMe.Checked),
                                      True)
                    Exit Select
                Case Else
                    FailureText.Text = "Invalid login attempt"
                    ErrorMessage.Visible = True
                    Exit Select
            End Select
        End If
    End Sub
End Class
