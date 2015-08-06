Imports System.IO

Imports RussBucksPools.LosersPool.Models

Public Class CreateFTPFiles
    Inherits System.Web.UI.Page
    Private _dbApp As New ApplicationDbContext

    Private FtpBatchFileList As New Dictionary(Of String, String)
    Private FtpTextFileList As New Dictionary(Of String, String)
    Private XMLFileList As New Dictionary(Of String, String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        FtpBatchFileList.Add("file1", "FtpScheduleFile1Script.Bat")
        FtpBatchFileList.Add("file2", "FtpScheduleFile3Script.Bat")
        FtpBatchFileList.Add("file3", "FtpScheduleFile5Script.Bat")
        FtpBatchFileList.Add("file4", "FtpScoringUpdateScript.Bat")
        FtpBatchFileList.Add("file5", "FtpScoringUpdateFinal0Script.Bat")
        FtpBatchFileList.Add("file6", "FtpScoringUpdateFinal1Script.Bat")
        FtpBatchFileList.Add("file7", "FtpScoringUpdateFinal3Script.Bat")
        FtpBatchFileList.Add("file8", "FtpScoringUpdateFinal5Script.Bat")

        FtpTextFileList.Add("file1", "FtpScheduleFile1Script.Txt")
        FtpTextFileList.Add("file2", "FtpScheduleFile3Script.Txt")
        FtpTextFileList.Add("file3", "FtpScheduleFile5Script.Txt")
        FtpTextFileList.Add("file4", "FtpScoringUpdateScript.Txt")
        FtpTextFileList.Add("file5", "FtpScoringUpdateFinal0Script.Txt")
        FtpTextFileList.Add("file6", "FtpScoringUpdateFinal1Script.Txt")
        FtpTextFileList.Add("file7", "FtpScoringUpdateFinal3Script.Txt")
        FtpTextFileList.Add("file8", "FtpScoringUpdateFinal5Script.Txt")

        XMLFileList.Add("file1", "ScheduleFile1.xml")
        XMLFileList.Add("file2", "ScheduleFile3.xml")
        XMLFileList.Add("file3", "ScheduleFile5.xml")
        XMLFileList.Add("file4", "scoringUpdate.xml")
        XMLFileList.Add("file5", "scoringUpdateFinal0.xml")
        XMLFileList.Add("file6", "scoringUpdateFinal1.xml")
        XMLFileList.Add("file7", "scoringUpdateFinal3.xml")
        XMLFileList.Add("file8", "scoringUpdateFinalDay5.xml")

        Try
            Using (_dbApp)
                Dim rootFolder = (From param1 In _dbApp.AppFolders).Single
                System.IO.Directory.SetCurrentDirectory(rootFolder.driverRootFolder)

                For Each filename In FtpBatchFileList
                    Dim filePath = ".\" + filename.Value
                    File.Delete(filePath)
                    File.AppendAllText(filePath, "ftp -s:" + rootFolder.driverRootFolder + "\" + FtpTextFileList(filename.Key))

                Next

                For Each filename In FtpTextFileList
                    Dim filePath = ".\" + filename.Value
                    File.Delete(filePath)
                    Dim writeFile As New StreamWriter(filePath)
                    writeFile.WriteLine("open 127.0.0.1 " + CStr(PortNumber1.Text))
                    writeFile.WriteLine("RussbucksBaseball")
                    writeFile.WriteLine("Boatin_123")
                    writeFile.WriteLine("put " + ".\" + XMLFileList(filename.Key))
                    writeFile.WriteLine("quit")
                    writeFile.Close()
                Next
            End Using

        Catch ex As Exception

        End Try
    End Sub
End Class