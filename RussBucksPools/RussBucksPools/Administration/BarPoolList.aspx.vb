Imports System.Xml
Imports System.Xml.Linq

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models

Public Class BarPoolList
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

                    Dim BarPoolListDoc = XDocument.Load(".\BarPoolList.xml")

                    Dim queryMembers = (From user1 In BarPoolListDoc.Descendants("BarPoolList").Descendants("BarPool")).ToList

                    For Each member1 In queryMembers
                        BarPoolListDoc.Descendants("BarPoolList").Descendants("BarPool").Remove()
                    Next

                    Dim queryMembers1 = (From user1 In _dbPools.BarPoolList).ToList

                    For Each user1 In queryMembers1

                        Dim xEBarPool As New XElement("BarPool")
                        Dim xAUserId As New XAttribute("UserId", user1.UserId)
                        Dim xEPoolName As New XElement("PoolName", user1.PoolName)
                        Dim xEPoolAlias As New XElement("PoolAlias", user1.PoolAlias)
                        Dim xESport As New XElement("Sport", user1.Sport)

                        BarPoolListDoc.Element("BarPoolList").AddFirst(xEBarPool)
                        BarPoolListDoc.Element("BarPoolList").Element("BarPool").Add(xAUserId)
                        BarPoolListDoc.Element("BarPoolList").Element("BarPool").Add(xEPoolName)
                        BarPoolListDoc.Element("BarPoolList").Element("BarPool").Add(xEPoolAlias)
                        BarPoolListDoc.Element("BarPoolList").Element("BarPool").Add(xESport)

                    Next
                    BarPoolListDoc.Save(".\BarPoolList.xml")
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class