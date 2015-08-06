Imports System.Xml
Imports System.Xml.Linq

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models

Public Class MyPoolsList
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

                    Dim MyPoolsListDoc = XDocument.Load(".\MyPoolsList.xml")

                    Dim queryMembers = (From user1 In MyPoolsListDoc.Descendants("MyPoolsList").Descendants("User")).ToList

                    For Each member1 In queryMembers
                        MyPoolsListDoc.Descendants("MyPoolsList").Descendants("User").Remove()
                    Next

                    Dim queryMembers1 = (From user1 In _dbPools.MyPools).ToList

                    For Each user1 In queryMembers1

                        Dim xEPoolAdministrator As New XElement("User")
                        Dim xEUserId As New XElement("UserId", user1.UserId)
                        Dim xEEName As New XElement("EName", user1.EName)
                        Dim xELoser As New XElement("Loser", user1.Loser)
                        Dim xEPlayoff As New XElement("Playoff", user1.Playoff)

                        MyPoolsListDoc.Element("MyPoolsList").AddFirst(xEPoolAdministrator)
                        MyPoolsListDoc.Element("MyPoolsList").Element("User").Add(xEUserId)
                        MyPoolsListDoc.Element("MyPoolsList").Element("User").Add(xEEName)
                        MyPoolsListDoc.Element("MyPoolsList").Element("User").Add(xELoser)
                        MyPoolsListDoc.Element("MyPoolsList").Element("User").Add(xEPlayoff)

                    Next
                    MyPoolsListDoc.Save(".\MyPoolsList.xml")
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class