Imports System.Xml
Imports System.Xml.Linq

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models

Public Class LoadMyPools
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

                    Dim queryMembers = (From members1 In _dbPools.MyPools).ToList

                    If queryMembers.Count >= 1 Then
                        For Each member1 In queryMembers
                            _dbPools.MyPools.Remove(member1)
                        Next
                    End If

                    _dbPools.SaveChanges()

                    Dim MyPoolsListDoc = XDocument.Load(".\MyPoolsList.xml")

                    Dim AllMembers = (From user2 In MyPoolsListDoc.Descendants("MyPoolsList").Descendants("User")
                                    Select New MyPool With {.UserId = user2.Elements("UserId").Value,
                                                            .EName = user2.Elements("EName").Value,
                                                            .Loser = user2.Elements("Loser").Value,
                                                            .Playoff = user2.Elements("Playoff").Value}).ToList

                    For Each member1 In AllMembers
                        Dim member2 As New MyPool
                        member2.UserId = member1.UserId
                        member2.EName = member1.EName
                        member2.Loser = member1.Loser
                        member2.Playoff = member1.Playoff
                        _dbPools.MyPools.Add(member2)
                        _dbPools.SaveChanges()
                    Next

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class