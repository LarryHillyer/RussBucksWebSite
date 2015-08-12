Imports System
Imports System.Threading.Tasks
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

' You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
Public Class ApplicationUser
    Inherits IdentityUser

    Public Property CommissionerCode As String

    Public Function GenerateUserIdentity(manager As ApplicationUserManager) As ClaimsIdentity
        ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        Dim userIdentity = manager.CreateIdentity(Me, DefaultAuthenticationTypes.ApplicationCookie)
        ' Add custom user claims here
        Return userIdentity
    End Function

    Public Function GenerateUserIdentityAsync(manager As ApplicationUserManager) As Task(Of ClaimsIdentity)
        Return Task.FromResult(GenerateUserIdentity(manager))
    End Function
End Class

Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser)

    Public Property SuperUsers As DbSet(Of SuperUser)
    Public Property AppFolders As DbSet(Of AppFolder)
    Public Property CronJobs As DbSet(Of CronJob)
    Public Property CronJobPools As DbSet(Of CronJobPool)
    Public Property CurrentCronJobs As DbSet(Of CurrentCronJob)

    Public Sub New()
        MyBase.New("RussBucks-Membership", throwIfV1Schema:=False)
    End Sub
    
    Public Shared Function Create As ApplicationDbContext
        Return New ApplicationDbContext()
    End Function    
End Class

#Region "Helpers"
Public Class IdentityHelper
    'Used for XSRF when linking external logins
    Public Const XsrfKey As String = "xsrfKey"

    Public Const ProviderNameKey As String = "providerName"
    Public Shared Function GetProviderNameFromRequest(request As HttpRequest) As String
        Return request.QueryString(ProviderNameKey)
    End Function

    Public Const CodeKey As String = "code"
    Public Shared Function GetCodeFromRequest(request As HttpRequest) As String
        Return request.QueryString(CodeKey)
    End Function

    Public Const UserIdKey As String = "userId"
    Public Shared Function GetUserIdFromRequest(request As HttpRequest) As String
        Return HttpUtility.UrlDecode(request.QueryString(UserIdKey))
    End Function

    Public Shared Function GetResetPasswordRedirectUrl(code As String, request As HttpRequest) As String
        Dim absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code)
        Return New Uri(request.Url, absoluteUri).AbsoluteUri.ToString()
    End Function

    Public Shared Function GetUserConfirmationRedirectUrl(code As String, userId As String, request As HttpRequest) As String
        Dim absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId)
        Return New Uri(request.Url, absoluteUri).AbsoluteUri.ToString()
    End Function

    Private Shared Function IsLocalUrl(url As String) As Boolean
        Return Not String.IsNullOrEmpty(url) AndAlso ((url(0) = "/"c AndAlso (url.Length = 1 OrElse (url(1) <> "/"c AndAlso url(1) <> "\"c))) OrElse (url.Length > 1 AndAlso url(0) = "~"c AndAlso url(1) = "/"c))
    End Function

    Public Shared Sub RedirectToReturnUrl(returnUrl As String, response As HttpResponse)
        If Not [String].IsNullOrEmpty(returnUrl) AndAlso IsLocalUrl(returnUrl) Then
            response.Redirect(returnUrl)
        Else
            response.Redirect("~/")
        End If
    End Sub
End Class

Public Class AppUser
    Public Property Id As String
    Public Property Email As String
    Public Property EmailConfirmed As Boolean
    Public Property PasswordHash As String
    Public Property SecurityStamp As String
    Public Property PhoneNumberConfirmed As Boolean
    Public Property TwoFactorEnabled As Boolean
    Public Property LockoutEnabled As Boolean
    Public Property AccessFailedCount As Int32
    Public Property UserName As String
    Public Property CommissionerCode As String
End Class

Public Class SuperUser
    <Key>
    Public Property SuperUserId As Int32
    Public Property SuperUserName As String

    Public Shared Sub SeedSuperUser()
        Dim _dbMembership As New ApplicationDbContext

        Try
            Using (_dbMembership)

                Dim queryMembers = (From members1 In _dbMembership.SuperUsers).ToList

                If queryMembers.Count = 0 Then
                    Dim sU1 As New SuperUser
                    sU1.SuperUserName = "lh4uhotmailcom"
                    _dbMembership.SuperUsers.Add(sU1)

                    _dbMembership.SaveChanges()

                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub


End Class

Public Class AppFolder
    <Key>
    Public Property FolderId As Int32
    Public Property rootFolder As String
    Public Property driverRootFolder As String
    Public Property scrapedFilesFolder As String
    Public Property scrapedScheduleFilesFolder As String
    Public Property testCronJobFolder As String
    Public Property scheduleCronJobFolder As String
    Public Property scoreCronJobFolder As String
End Class

Public Class CronJob
    <Key>
    Public Property CronJobId As Int32
    Public Property CronJobName As String
    Public Property SelectedSport As String
    Public Property SelectedPool As String
    Public Property SelectedSeasonStartDate As String
    Public Property SelectedSeasonStartGameDate As String
    Public Property SelectedSeasonEndDate As String
    Public Property UserTestIsSelected As Boolean
    Public Property ContinueTestIsSelected As Boolean
    Public Property CustomScheduleIsSelected As Boolean
    Public Property CronJobType As String
    Public Property CronJobNumber As String
    Public Property CronJobStartDateTime As String
    Public Property CronJobIsPreseason As Boolean

End Class

Public Class CronJobPool

    <Key>
    Public Property CronJobPoolId As Int32
    Public Property CronJobName As String
    Public Property CronJobPoolAlias As String
End Class

Public Class CurrentCronJob

    <Key>
    Public Property CronJobId As Int32
    Public Property CronJobName As String
    Public Property CronJobType As String
    Public Property CronJobNumber As String
    Public Property CronJobStartDateTime As String

End Class
#End Region