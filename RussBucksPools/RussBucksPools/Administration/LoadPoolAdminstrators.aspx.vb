Imports System.Xml
Imports System.Xml.Linq

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models

Public Class LoadPoolAdminstrators
    Inherits System.Web.UI.Page

    Private _dbPools As New PoolDbContext
    Private _dbApp As New ApplicationDbContext

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Using (_dbPools)
                Using (_dbApp)

                    Dim rootFolder = (From param1 In _dbApp.AppFolders).Single
                    System.IO.Directory.SetCurrentDirectory(rootFolder.driverRootFolder)

                    Dim queryMembers = (From members1 In _dbPools.PoolAdministrators).ToList

                    If queryMembers.Count >= 1 Then
                        For Each member1 In queryMembers
                            _dbPools.PoolAdministrators.Remove(member1)
                        Next
                    End If

                    _dbPools.SaveChanges()

                    Dim PoolAdministratorListDoc = XDocument.Load(".\PoolAdministratorList.xml")

                    Dim AllMembers = (From user2 In PoolAdministratorListDoc.Descendants("PoolAdministratorList").Descendants("PoolAdministrator")
                                    Select New PoolAdministrator1 With {.PoolAdministrator = user2.Elements("PoolAdmin1").Value}).ToList

                    For Each member1 In AllMembers
                        Dim member2 As New PoolAdministrator1
                        member2.PoolAdministrator = member1.PoolAdministrator
                        _dbPools.PoolAdministrators.Add(member2)
                        _dbPools.SaveChanges()
                    Next

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class