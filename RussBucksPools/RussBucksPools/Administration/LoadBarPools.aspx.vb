Imports System.Xml
Imports System.Xml.Linq

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models

Public Class LoadBarPools
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

                    Dim queryMembers = (From members1 In _dbPools.BarPoolList).ToList

                    If queryMembers.Count >= 1 Then
                        For Each member1 In queryMembers
                            _dbPools.BarPoolList.Remove(member1)
                        Next
                    End If

                    _dbPools.SaveChanges()

                    Dim BarPoolListDoc = XDocument.Load(".\BarPoolList.xml")

                    Dim AllMembers = (From user2 In BarPoolListDoc.Descendants("BarPoolList").Descendants("BarPool")
                                    Select New PoolAlias1 With {.UserId = user2.Attribute("UserId").Value,
                                                             .PoolName = user2.Elements("PoolName").Value,
                                                             .PoolAlias = user2.Elements("PoolAlias").Value,
                                                             .Sport = user2.Elements("Sport").Value}).ToList

                    For Each member1 In AllMembers
                        Dim member2 As New PoolAlias1
                        member2.UserId = member1.UserId
                        member2.PoolName = member1.PoolName
                        member2.PoolAlias = member1.PoolAlias
                        member2.Sport = member1.Sport
                        _dbPools.BarPoolList.Add(member2)
                        _dbPools.SaveChanges()
                    Next

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class