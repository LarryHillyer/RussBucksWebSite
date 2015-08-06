Imports System.Xml
Imports System.Xml.Linq

Imports RussBucksPools.LosersPool.Models

Public Class LoadMembersTable
    Inherits System.Web.UI.Page

    Private _dbMembers As New ApplicationDbContext


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Using (_dbMembers)


                Dim rootFolder = (From param1 In _dbMembers.AppFolders).Single
                System.IO.Directory.SetCurrentDirectory(rootFolder.driverRootFolder)

                Dim queryMembers = (From members1 In _dbMembers.Users).ToList

                If queryMembers.Count >= 1 Then
                    For Each member1 In queryMembers
                        _dbMembers.Users.Remove(member1)
                    Next
                End If

                _dbMembers.SaveChanges()

                Dim MembersListDoc = XDocument.Load(".\MembersList.xml")

                Dim AllMembers = (From user2 In MembersListDoc.Descendants("Users").Descendants("User")
                                Select New AppUser With {.Id = user2.Attribute("Id").Value,
                                                         .Email = user2.Elements("Email").Value,
                                                         .EmailConfirmed = CBool(user2.Elements("EmailConfirmed").Value),
                                                         .PasswordHash = user2.Elements("PasswordHash").Value,
                                                         .SecurityStamp = user2.Elements("SecurityStamp").Value,
                                                         .PhoneNumberConfirmed = CBool(user2.Elements("PhoneNumberConfirmed").Value),
                                                         .TwoFactorEnabled = CBool(user2.Elements("TwoFactorEnabled").Value),
                                                         .LockoutEnabled = CBool(user2.Elements("LockoutEnabled").Value),
                                                         .AccessFailedCount = CInt(user2.Elements("AccessFailedCount").Value),
                                                         .UserName = user2.Elements("UserName").Value}).ToList

                For Each member1 In AllMembers
                    Dim member2 As New ApplicationUser
                    member2.Id = member1.Id
                    member2.Email = member1.Email
                    member2.EmailConfirmed = member1.EmailConfirmed
                    member2.PasswordHash = member1.PasswordHash
                    member2.SecurityStamp = member1.SecurityStamp
                    member2.PhoneNumberConfirmed = member1.PhoneNumberConfirmed
                    member2.TwoFactorEnabled = member1.TwoFactorEnabled
                    member2.LockoutEnabled = member1.LockoutEnabled
                    member2.AccessFailedCount = member1.AccessFailedCount
                    member2.UserName = member1.UserName
                    _dbMembers.Users.Add(member2)
                    _dbMembers.SaveChanges()
                Next


            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class