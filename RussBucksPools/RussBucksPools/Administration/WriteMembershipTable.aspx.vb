Imports System.Xml
Imports System.Xml.Linq

Imports RussBucksPools.LosersPool.Models

Public Class WriteMembershipTable
    Inherits System.Web.UI.Page

    Private _dbMembers As New ApplicationDbContext
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Using (_dbMembers)

                Dim rootFolder = (From param1 In _dbMembers.AppFolders).Single
                System.IO.Directory.SetCurrentDirectory(rootFolder.driverRootFolder)

                Dim membersListDoc = XDocument.Load(".\MembersList.xml")

                Dim queryMembers = (From user1 In membersListDoc.Descendants("Users").Descendants("User")).ToList

                For Each member1 In queryMembers
                    membersListDoc.Descendants("Users").Descendants("User").Remove()
                Next

                Dim queryMembers1 = (From user1 In _dbMembers.Users).ToList

                For Each user1 In queryMembers1

                    Dim xEUser As New XElement("User")
                    Dim xAId As New XAttribute("Id", user1.Id)
                    Dim xEEmail As New XElement("Email", user1.Email)
                    Dim xEEmailConfirmed As New XElement("EmailConfirmed", user1.EmailConfirmed)
                    Dim xEPasswordHash As New XElement("PasswordHash", user1.PasswordHash)
                    Dim xESecurityStamp As New XElement("SecurityStamp", user1.SecurityStamp)
                    Dim xEPhoneNumberConfirmed As New XElement("PhoneNumberConfirmed", user1.PhoneNumberConfirmed)
                    Dim xETwoFactorEnabled As New XElement("TwoFactorEnabled", user1.TwoFactorEnabled)
                    Dim xELockoutEnabled As New XElement("LockoutEnabled", user1.LockoutEnabled)
                    Dim xEAccessFailedCount As New XElement("AccessFailedCount", user1.AccessFailedCount)
                    Dim xEUserName As New XElement("UserName", user1.UserName)

                    membersListDoc.Element("Users").AddFirst(xEUser)
                    membersListDoc.Element("Users").Element("User").Add(xAId)
                    membersListDoc.Element("Users").Element("User").Add(xEEmail)
                    membersListDoc.Element("Users").Element("User").Add(xEEmailConfirmed)
                    membersListDoc.Element("Users").Element("User").Add(xEPasswordHash)
                    membersListDoc.Element("Users").Element("User").Add(xESecurityStamp)
                    membersListDoc.Element("Users").Element("User").Add(xEPhoneNumberConfirmed)
                    membersListDoc.Element("Users").Element("User").Add(xETwoFactorEnabled)
                    membersListDoc.Element("Users").Element("User").Add(xELockoutEnabled)
                    membersListDoc.Element("Users").Element("User").Add(xEAccessFailedCount)
                    membersListDoc.Element("Users").Element("User").Add(xEUserName)

                Next
                membersListDoc.Save(".\MembersList.xml")

            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class