Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Linq
Imports System.Data

Imports RussBucksPools.LosersPool.Models

Imports RussBucksPools.JoinPools
Imports RussBucksPools.JoinPools.Models


Public Class ReadScheduleTimePeriodXMLFile

    Private _dbLoserPool As New LosersPoolContext

    Public Sub New(filepath As String)

        Try
            Using (_dbLoserPool)

                Dim scheduleTimePeriodList = XDocument.Load(filepath)

                Dim queryScheduleTimePeriod = (From dayElement In scheduleTimePeriodList.Descendants("scheduleTimePeriod").Descendants("day")
                                               Select New scheduleTimePeriodXML With {.dayId = dayElement.Attribute("dayNumber").Value,
                                                                                      .startDayTime = dayElement.Elements("startDayTime").Value,
                                                                                      .startDayDate = dayElement.Elements("startDayDate").Value,
                                                                                      .startGameTime = dayElement.Elements("startGameTime").Value,
                                                                                      .startGameDate = dayElement.Elements("startGameDate").Value,
                                                                                      .endDayTime = dayElement.Elements("endDayTime").Value,
                                                                                      .endDayDate = dayElement.Elements("endDayDate").Value}).ToList

                For Each timePeriod In queryScheduleTimePeriod

                    Dim timePeriodEntity = New ScheduleTimePeriod
                    timePeriodEntity.dayID = timePeriod.dayId

                    If timePeriod.startDayDate = "" Then
                        timePeriodEntity.startGameDate = Nothing
                    Else
                        timePeriodEntity.startDayDate = timePeriod.startDayDate
                    End If

                    If (timePeriod.startDayTime = "") Then
                        timePeriodEntity.startDayTime = Nothing
                    Else
                        timePeriodEntity.startDayTime = timePeriod.startDayTime
                    End If

                    timePeriodEntity.startGameDate = timePeriod.startGameDate
                    timePeriodEntity.startGameTime = timePeriod.startGameTime

                    If (timePeriod.endDayDate = "") Then
                        timePeriodEntity.endDayDate = Nothing
                    Else
                        timePeriodEntity.endDayDate = timePeriod.endDayDate
                    End If

                    If (timePeriod.endDayDate = "") Then
                        timePeriodEntity.endDayTime = Nothing
                    Else
                        timePeriodEntity.endDayTime = timePeriod.endDayTime
                    End If

                    _dbLoserPool.ScheduleTimePeriods.Add(timePeriodEntity)
                    _dbLoserPool.SaveChanges()

                Next

                Dim dummy = "dummy"

            End Using
        Catch ex As Exception

        End Try

    End Sub

End Class

Public Class UserChoiceList

    Private _dbLoserPool As New LosersPoolContext

    Public Sub New(filepath As String, wNumber As String, rootFolder As String)
        Try
            Using (_dbLoserPool)

                Dim queryUser1 = (From users1 In _dbLoserPool.UserChoicesList).ToList

                If _dbLoserPool.UserChoicesList.Count >= 1 Then
                    Exit Sub
                End If

                _dbLoserPool.SaveChanges()

                System.IO.Directory.SetCurrentDirectory(rootFolder)

                Dim userChoicesXDocument = XDocument.Load(filepath)
                'Dim user1 As New userChoicesXML
                Dim DailyPossibleChoicesForAllUsers = (From dayElement In userChoicesXDocument.Descendants("UserChoicesList").Descendants("Day").Descendants("User")
                                                        Select New userChoicesXML With {.UserID = dayElement.Attribute("UserId").Value,
                                                                                        .DayId = dayElement.Attribute("DayId").Value,
                                                                                        .UserName = dayElement.Elements("UserName").Value,
                                                                                        .ConfirmationNumber = CInt(dayElement.Elements("ConfirmationNumber").Value),
                                                                                        .UserPick = dayElement.Elements("UserPick").Value,
                                                                                        .Washington = CBool(dayElement.Elements("Washington").Value),
                                                                                        .Miami = CBool(dayElement.Elements("Miami").Value),
                                                                                        .Colorado = CBool(dayElement.Elements("Colorado").Value),
                                                                                        .Arizona = CBool(dayElement.Elements("Arizona").Value),
                                                                                        .SanFrancisco = CBool(dayElement.Elements("SanFrancisco").Value),
                                                                                        .SanDiego = CBool(dayElement.Elements("SanDiego").Value),
                                                                                        .Pittsburg = CBool(dayElement.Elements("Pittsburg").Value),
                                                                                        .Cincinnati = CBool(dayElement.Elements("Cincinnati").Value),
                                                                                        .Toronto = CBool(dayElement.Elements("Toronto").Value),
                                                                                        .NYYankees = CBool(dayElement.Elements("NYYankees").Value),
                                                                                        .Boston = CBool(dayElement.Elements("Boston").Value),
                                                                                        .TampaBay = CBool(dayElement.Elements("TampaBay").Value),
                                                                                        .Atlanta = CBool(dayElement.Elements("Atlanta").Value),
                                                                                        .Philadelphia = CBool(dayElement.Elements("Philadelphia").Value),
                                                                                        .ChicagoWhiteSox = CBool(dayElement.Elements("ChicagoWhiteSox").Value),
                                                                                        .Detroit = CBool(dayElement.Elements("Detroit").Value),
                                                                                        .KansasCity = CBool(dayElement.Elements("KansasCity").Value),
                                                                                        .Cleveland = CBool(dayElement.Elements("Cleveland").Value),
                                                                                        .Milwaukee = CBool(dayElement.Elements("Milwaukee").Value),
                                                                                        .LADodgers = CBool(dayElement.Elements("LADodgers").Value),
                                                                                        .Minnesota = CBool(dayElement.Elements("Minnesota").Value),
                                                                                        .Oakland = CBool(dayElement.Elements("Oakland").Value),
                                                                                        .Houston = CBool(dayElement.Elements("Houston").Value),
                                                                                        .Texas = CBool(dayElement.Elements("Texas").Value),
                                                                                        .STLouis = CBool(dayElement.Elements("STLouis").Value),
                                                                                        .ChicagoCubs = CBool(dayElement.Elements("ChicagoCubs").Value),
                                                                                        .LAAngels = CBool(dayElement.Elements("LAAngels").Value),
                                                                                        .Seattle = CBool(dayElement.Elements("Seattle").Value),
                                                                                        .NYMets = CBool(dayElement.Elements("NYMets").Value),
                                                                                        .Baltimore = CBool(dayElement.Elements("Baltimore").Value),
                                                                                        .Contender = CBool(dayElement.Elements("Contender").Value)}).ToList

                For Each user1 In DailyPossibleChoicesForAllUsers
                    Dim user2 = New UserChoices
                    user2.ListId = _dbLoserPool.UserChoicesList.Count + 1
                    user2.UserID = user1.UserID
                    user2.UserName = user1.UserName
                    user2.DayId = user1.DayId
                    user2.ConfirmationNumber = user1.ConfirmationNumber
                    user2.Contender = user1.Contender
                    user2.UserPick = user1.UserPick
                    user2.Washington = user1.Washington
                    user2.Miami = user1.Miami
                    user2.Colorado = user1.Colorado
                    user2.Arizona = user1.Arizona
                    user2.SanFrancisco = user1.SanFrancisco
                    user2.SanDiego = user1.SanDiego
                    user2.Pittsburg = user1.Pittsburg
                    user2.Cincinnati = user1.Cincinnati
                    user2.Toronto = user1.Toronto
                    user2.NYYankees = user1.NYYankees
                    user2.Boston = user1.Boston
                    user2.TampaBay = user1.TampaBay
                    user2.Atlanta = user1.Atlanta
                    user2.Philadelphia = user1.Philadelphia
                    user2.ChicagoWhiteSox = user1.ChicagoWhiteSox
                    user2.Detroit = user1.Detroit
                    user2.KansasCity = user1.KansasCity
                    user2.Cleveland = user1.Cleveland
                    user2.Milwaukee = user1.Milwaukee
                    user2.LADodgers = user1.LADodgers
                    user2.Minnesota = user1.Minnesota
                    user2.Oakland = user1.Oakland
                    user2.Houston = user1.Houston
                    user2.Texas = user1.Texas
                    user2.STLouis = user1.STLouis
                    user2.ChicagoCubs = user1.ChicagoCubs
                    user2.LAAngels = user1.LAAngels
                    user2.Seattle = user1.Seattle
                    user2.NYMets = user1.NYMets
                    user2.Baltimore = user1.Baltimore

                    _dbLoserPool.UserChoicesList.Add(user2)
                Next

                _dbLoserPool.SaveChanges()

                Dim dummy = "dummy"
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class

Public Class UserList

    Public Sub New(filepath As String)
        Dim _dbLoserPool As New LosersPoolContext
        Try
            Using (_dbLoserPool)

                Dim queryUser1 = (From users1 In _dbLoserPool.Users).ToList

                If _dbLoserPool.Users.Count >= 1 Then
                    Exit Sub
                End If

                _dbLoserPool.SaveChanges()

                Dim userListXDocument = XDocument.Load(filepath)
                'Dim user1 As New UserXML
                Dim AllUsers = (From user2 In userListXDocument.Descendants("UserList").Descendants("User")
                               Select New UserXML With {.UserId = user2.Attribute("UserId").Value,
                               .UserName = user2.Elements("UserName").Value,
                               .Administrator = CBool(user2.Elements("Adminstrator").Value),
                               .OptionState = user2.Elements("OptionState").Value}).ToList

                For Each user1 In AllUsers
                    Dim user2 As New LosersPool.Models.User
                    user2.UserId = user1.UserId
                    user2.UserName = user1.UserName
                    user2.Administrator = user1.Administrator
                    If user1.OptionState = "" Then
                        user2.OptionState = Nothing
                    Else
                        user2.OptionState = user1.OptionState

                    End If
                    _dbLoserPool.Users.Add(user2)
                Next

                _dbLoserPool.SaveChanges()
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class