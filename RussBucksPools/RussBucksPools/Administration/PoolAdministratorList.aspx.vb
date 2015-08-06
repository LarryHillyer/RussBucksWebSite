Imports System.Xml
Imports System.Xml.Linq

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models

Public Class PoolAdministratorList
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

                    Dim PoolAdministrationListDoc = XDocument.Load(".\PoolAdministratorList.xml")

                    Dim queryMembers = (From user1 In PoolAdministrationListDoc.Descendants("PoolAdministratorList").Descendants("PoolAdministrator")).ToList

                    For Each member1 In queryMembers
                        PoolAdministrationListDoc.Descendants("PoolAdministratorList").Descendants("PoolAdministrator").Remove()
                    Next

                    Dim queryMembers1 = (From user1 In _dbPools.PoolAdministrators).ToList

                    For Each user1 In queryMembers1

                        Dim xEPoolAdministrator As New XElement("PoolAdministrator")
                        Dim xEPoolAdmin1 As New XElement("PoolAdmin1", user1.PoolAdministrator)

                        PoolAdministrationListDoc.Element("PoolAdministratorList").AddFirst(xEPoolAdministrator)
                        PoolAdministrationListDoc.Element("PoolAdministratorList").Element("PoolAdministrator").Add(xEPoolAdmin1)
                    Next
                    PoolAdministrationListDoc.Save(".\PoolAdministratorList.xml")
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class