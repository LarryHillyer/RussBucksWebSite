Imports System
Imports System.Linq
Imports System.Xml.Linq
Imports System.Globalization
Imports System.Threading

Imports RussBucksPools.JoinPools.Models
Imports RussBucksPools.LosersPool.Models


Public Class Season_End
    Inherits System.Web.UI.Page

    Private I2 As Integer

    Private UserForeColor As System.Drawing.Color = Drawing.Color.Green
    Private UserBackColor As System.Drawing.Color = Drawing.Color.LightGreen


    Private AvailableForeColor As System.Drawing.Color = Drawing.Color.Green
    Private AvailableBackColor As System.Drawing.Color = Drawing.Color.LightGreen

    Private NotAvailableForeColor As System.Drawing.Color = Drawing.Color.Red
    Private NotAvailableBackColor As System.Drawing.Color = Drawing.Color.LightSalmon


    Private TeamBackColor As System.Drawing.Color = Drawing.Color.White

    Private LoserCollectionSorted As New Dictionary(Of String, Loser)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If


        If Not Page.IsPostBack Then

            Dim _dbLoserPool As New LosersPoolContext

            Dim rootFolder = CStr(Cache("rootFolder"))
            System.IO.Directory.SetCurrentDirectory(rootFolder)



            Try
                Using (_dbLoserPool)

                    Dim EName = CStr(Session("userId"))
                    Dim UserResults = _dbLoserPool.UserResultList.ToList

                    For Each userResult1 In UserResults
                        _dbLoserPool.UserResultList.Remove(userResult1)
                    Next

                    _dbLoserPool.SaveChanges()


                    I2 = CInt(Cache("timeIncrement"))

                    Dim dayId = CStr(Session("dayNumber"))
                    'Dim dayId = dayNumber



                    Dim queryUser = (From user1 In _dbLoserPool.Users
                                     Where user1.UserId = EName).Single

                    If queryUser.OptionState = "Enter Picks" Then
                        dayId = "day" + CStr(CInt(Mid(dayId, 4)) - I2)
                        If dayId = "day-1" Then
                            dayId = "day0"
                            Response.Redirect("~/LosersPool/NoPreseasonStandings.aspx")

                        End If
                    End If



                    WashingtonImage1.ImageUrl = "~/MLB ICONS/Washington Label.png"
                    WashingtonIcon1.ImageUrl = "~/MLB ICONS/Washington.png"
                    WashingtonImage1.BackColor = TeamBackColor

                    MiamiImage1.ImageUrl = "~/MLB ICONS/Miami Label.png"
                    MiamiIcon1.ImageUrl = "~/MLB ICONS/Miami.png"
                    MiamiImage1.BackColor = TeamBackColor

                    ColoradoImage1.ImageUrl = "~/MLB ICONS/Colorado Label.png"
                    ColoradoIcon1.ImageUrl = "~/MLB ICONS/Colorado.png"
                    ColoradoImage1.BackColor = TeamBackColor

                    ArizonaImage1.ImageUrl = "~/MLB ICONS/Arizona Label.png"
                    ArizonaIcon1.ImageUrl = "~/MLB ICONS/Arizona.png"
                    ArizonaImage1.BackColor = TeamBackColor

                    SanFranciscoImage1.ImageUrl = "~/MLB ICONS/San Francisco Label.png"
                    SanFranciscoIcon1.ImageUrl = "~/MLB ICONS/San Francisco.png"
                    SanFranciscoImage1.BackColor = TeamBackColor

                    SanDiegoImage1.ImageUrl = "~/MLB ICONS/San Diego Label.png"
                    SanDiegoIcon1.ImageUrl = "~/MLB ICONS/San Diego.png"
                    SanDiegoImage1.BackColor = TeamBackColor

                    PittsburgImage1.ImageUrl = "~/MLB ICONS/Pittsburg Label.png"
                    PittsburgIcon1.ImageUrl = "~/MLB ICONS/Pittsburg.png"
                    PittsburgImage1.BackColor = TeamBackColor

                    CincinnatiImage1.ImageUrl = "~/MLB ICONS/Cincinnati Label.png"
                    CincinnatiIcon1.ImageUrl = "~/MLB ICONS/Cincinnati.png"
                    CincinnatiImage1.BackColor = TeamBackColor

                    TorontoImage1.ImageUrl = "~/MLB ICONS/Toronto Label.png"
                    TorontoIcon1.ImageUrl = "~/MLB ICONS/Toronto.png"
                    TorontoImage1.BackColor = TeamBackColor

                    NYYankeesImage1.ImageUrl = "~/MLB ICONS/NY Yankees Label.png"
                    NYYankeesIcon1.ImageUrl = "~/MLB ICONS/NY Yankees.png"
                    NYYankeesImage1.BackColor = TeamBackColor

                    BostonImage1.ImageUrl = "~/MLB ICONS/Boston Label.png"
                    BostonIcon1.ImageUrl = "~/MLB ICONS/Boston.png"
                    BostonImage1.BackColor = TeamBackColor

                    TampaBayImage1.ImageUrl = "~/MLB ICONS/Tampa Bay Label.png"
                    TampaBayIcon1.ImageUrl = "~/MLB ICONS/Tampa Bay.png"
                    TampaBayImage1.BackColor = TeamBackColor

                    AtlantaImage1.ImageUrl = "~/MLB ICONS/Atlanta Label.png"
                    AtlantaIcon1.ImageUrl = "~/MLB ICONS/Atlanta.png"
                    AtlantaImage1.BackColor = TeamBackColor

                    PhiladelphiaImage1.ImageUrl = "~/MLB ICONS/Philadelphia Label.png"
                    PhiladelphiaIcon1.ImageUrl = "~/MLB ICONS/Philadelphia.png"
                    PhiladelphiaImage1.BackColor = TeamBackColor

                    ChicagoWhiteSoxImage1.ImageUrl = "~/MLB ICONS/Chicago White Sox Label.png"
                    ChicagoWhiteSoxIcon1.ImageUrl = "~/MLB ICONS/Chicago White Sox.png"
                    ChicagoWhiteSoxImage1.BackColor = TeamBackColor

                    DetroitImage1.ImageUrl = "~/MLB ICONS/Detroit Label.png"
                    DetroitIcon1.ImageUrl = "~/MLB ICONS/Detroit.png"
                    DetroitImage1.BackColor = TeamBackColor

                    KansasCityImage1.ImageUrl = "~/MLB ICONS/Kansas City Label.png"
                    KansasCityIcon1.ImageUrl = "~/MLB ICONS/Kansas City.png"
                    KansasCityImage1.BackColor = TeamBackColor

                    ClevelandImage1.ImageUrl = "~/MLB ICONS/Cleveland Label.png"
                    ClevelandIcon1.ImageUrl = "~/MLB ICONS/Cleveland.png"
                    ClevelandImage1.BackColor = TeamBackColor

                    MilwaukeeImage1.ImageUrl = "~/MLB ICONS/Milwaukee Label.png"
                    MilwaukeeIcon1.ImageUrl = "~/MLB ICONS/Milwaukee.png"
                    MilwaukeeImage1.BackColor = TeamBackColor

                    LADodgersImage1.ImageUrl = "~/MLB ICONS/LA Dodgers Label.png"
                    LADodgersIcon1.ImageUrl = "~/MLB ICONS/LA Dodgers.png"
                    LADodgersImage1.BackColor = TeamBackColor

                    MinnesotaImage1.ImageUrl = "~/MLB ICONS/Minnesota Label.png"
                    MinnesotaIcon1.ImageUrl = "~/MLB ICONS/Minnesota.png"
                    MinnesotaImage1.BackColor = TeamBackColor

                    OaklandImage1.ImageUrl = "~/MLB ICONS/Oakland Label.png"
                    OaklandIcon1.ImageUrl = "~/MLB ICONS/Oakland.png"
                    OaklandImage1.BackColor = TeamBackColor

                    HoustonImage1.ImageUrl = "~/MLB ICONS/Houston Label.png"
                    HoustonIcon1.ImageUrl = "~/MLB ICONS/Houston.png"
                    HoustonImage1.BackColor = TeamBackColor

                    TexasImage1.ImageUrl = "~/MLB ICONS/Texas Label.png"
                    TexasIcon1.ImageUrl = "~/MLB ICONS/Texas.png"
                    TexasImage1.BackColor = TeamBackColor

                    STLouisImage1.ImageUrl = "~/MLB ICONS/St. Louis Label.png"
                    STLouisIcon1.ImageUrl = "~/MLB ICONS/St. Louis.png"
                    STLouisImage1.BackColor = TeamBackColor

                    ChicagoCubsImage1.ImageUrl = "~/MLB ICONS/Chicago Cubs Label.png"
                    ChicagoCubsIcon1.ImageUrl = "~/MLB ICONS/Chicago Cubs.png"
                    ChicagoCubsImage1.BackColor = TeamBackColor

                    LAAngelsImage1.ImageUrl = "~/MLB ICONS/LA Angels Label.png"
                    LAAngelsIcon1.ImageUrl = "~/MLB ICONS/LA Angels.png"
                    LAAngelsImage1.BackColor = TeamBackColor

                    SeattleImage1.ImageUrl = "~/MLB ICONS/Seattle Label.png"
                    SeattleIcon1.ImageUrl = "~/MLB ICONS/Seattle.png"
                    SeattleImage1.BackColor = TeamBackColor

                    NYMetsImage1.ImageUrl = "~/MLB ICONS/NY Mets Label.png"
                    NYMetsIcon1.ImageUrl = "~/MLB ICONS/NY Mets.png"
                    NYMetsImage1.BackColor = TeamBackColor

                    BaltimoreImage1.ImageUrl = "~/MLB ICONS/Baltimore Label.png"
                    BaltimoreIcon1.ImageUrl = "~/MLB ICONS/Baltimore.png"
                    BaltimoreImage1.BackColor = TeamBackColor


                    Dim dailyUserChoices As New List(Of UserChoices)

                    dailyUserChoices = (From user1 In _dbLoserPool.UserChoicesList
                                            Where user1.Contender = True And user1.DayId = dayId
                                            Select user1).ToList()

                    For Each user1 In dailyUserChoices

                        Dim user2 = New UserResult
                        user2.ListId = user1.ListId
                        user2.UserID = user1.UserID
                        user2.UserName = user1.UserName
                        user2.DayId = dayId

                        If user1.UserPick = "Washington" Then
                            user2.Washington = False
                        Else
                            user2.Washington = user1.Washington
                        End If

                        If user1.UserPick = "Miami" Then
                            user2.Miami = False
                        Else
                            user2.Miami = user1.Miami
                        End If

                        If user1.UserPick = "Colorado" Then
                            user2.Colorado = False
                        Else
                            user2.Colorado = user1.Colorado
                        End If

                        If user1.UserPick = "Arizona" Then
                            user2.Arizona = False
                        Else
                            user2.Arizona = user1.Arizona
                        End If

                        If user1.UserPick = "San Francisco" Then
                            user2.SanFrancisco = False
                        Else
                            user2.SanFrancisco = user1.SanFrancisco
                        End If

                        If user1.UserPick = "San Diego" Then
                            user2.SanDiego = False
                        Else
                            user2.SanDiego = user1.SanDiego
                        End If

                        If user1.UserPick = "Pittsburg" Then
                            user2.Pittsburg = False
                        Else
                            user2.Pittsburg = user1.Pittsburg
                        End If

                        If user1.UserPick = "Cincinnati" Then
                            user2.Cincinnati = False
                        Else
                            user2.Cincinnati = user1.Cincinnati
                        End If

                        If user1.UserPick = "Toronto" Then
                            user2.Toronto = False
                        Else
                            user2.Toronto = user1.Toronto
                        End If

                        If user1.UserPick = "NY Yankees" Then
                            user2.NYYankees = False
                        Else
                            user2.NYYankees = user1.NYYankees
                        End If

                        If user1.UserPick = "Boston" Then
                            user2.Boston = False
                        Else
                            user2.Boston = user1.Boston
                        End If

                        If user1.UserPick = "Tampa Bay" Then
                            user2.TampaBay = False
                        Else
                            user2.TampaBay = user1.TampaBay
                        End If

                        If user1.UserPick = "Atlanta" Then
                            user2.Atlanta = False
                        Else
                            user2.Atlanta = user1.Atlanta
                        End If

                        If user1.UserPick = "Philadelphia" Then
                            user2.Philadelphia = False
                        Else
                            user2.Philadelphia = user1.Philadelphia
                        End If

                        If user1.UserPick = "Chicago White Sox" Then
                            user2.ChicagoWhiteSox = False
                        Else
                            user2.ChicagoWhiteSox = user1.ChicagoWhiteSox
                        End If

                        If user1.UserPick = "Detroit" Then
                            user2.Detroit = False
                        Else
                            user2.Detroit = user1.Detroit
                        End If

                        If user1.UserPick = "Kansas City" Then
                            user2.KansasCity = False
                        Else
                            user2.KansasCity = user1.KansasCity
                        End If

                        If user1.UserPick = "Cleveland" Then
                            user2.Cleveland = False
                        Else
                            user2.Cleveland = user1.Cleveland
                        End If

                        If user1.UserPick = "Milwaukee" Then
                            user2.Milwaukee = False
                        Else
                            user2.Milwaukee = user1.Milwaukee
                        End If

                        If user1.UserPick = "LA Dodgers" Then
                            user2.LADodgers = False
                        Else
                            user2.LADodgers = user1.LADodgers
                        End If

                        If user1.UserPick = "Minnesota" Then
                            user2.Minnesota = False
                        Else
                            user2.Minnesota = user1.Minnesota
                        End If

                        If user1.UserPick = "Oakland" Then
                            user2.Oakland = False
                        Else
                            user2.Oakland = user1.Oakland
                        End If

                        If user1.UserPick = "Houston" Then
                            user2.Houston = False
                        Else
                            user2.Houston = user1.Houston
                        End If

                        If user1.UserPick = "Texas" Then
                            user2.Texas = False
                        Else
                            user2.Texas = user1.Texas
                        End If

                        If user1.UserPick = "St. Louis" Then
                            user2.STLouis = False
                        Else
                            user2.STLouis = user1.STLouis
                        End If

                        If user1.UserPick = "Chicago Cubs" Then
                            user2.ChicagoCubs = False
                        Else
                            user2.ChicagoCubs = user1.ChicagoCubs
                        End If

                        If user1.UserPick = "LA Angels" Then
                            user2.LAAngels = False
                        Else
                            user2.LAAngels = user1.LAAngels
                        End If

                        If user1.UserPick = "Seattle" Then
                            user2.Seattle = False
                        Else
                            user2.Seattle = user1.Seattle
                        End If

                        If user1.UserPick = "NY Mets" Then
                            user2.NYMets = False
                        Else
                            user2.NYMets = user1.NYMets
                        End If

                        If user1.UserPick = "Baltimore" Then
                            user2.Baltimore = False
                        Else
                            user2.Baltimore = user1.Baltimore
                        End If

                        Dim dRow As New TableRow

                        Dim dCell1 As New TableCell
                        dCell1.Text = user2.UserName
                        dCell1.ForeColor = UserForeColor
                        dCell1.BackColor = UserBackColor
                        dCell1.CssClass = "UserName1"
                        dRow.Cells.Add(dCell1)

                        Dim dCell2 As New TableCell
                        If user2.Washington = True Then
                            dCell2.Text = "A"
                            dCell2.ForeColor = AvailableForeColor
                            dCell2.BackColor = AvailableBackColor
                        Else
                            dCell2.Text = "na"
                            dCell2.ForeColor = NotAvailableForeColor
                            dCell2.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell2)

                        Dim dCell3 As New TableCell
                        If user2.Miami = True Then
                            dCell3.Text = "A"
                            dCell3.ForeColor = AvailableForeColor
                            dCell3.BackColor = AvailableBackColor

                        Else
                            dCell3.Text = "na"
                            dCell3.ForeColor = NotAvailableForeColor
                            dCell3.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell3)

                        Dim dCell4 As New TableCell
                        If user2.Colorado = True Then
                            dCell4.Text = "A"
                            dCell4.ForeColor = AvailableForeColor
                            dCell4.BackColor = AvailableBackColor

                        Else
                            dCell4.Text = "na"
                            dCell4.ForeColor = NotAvailableForeColor
                            dCell4.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell4)

                        Dim dCell5 As New TableCell
                        If user2.Arizona = True Then
                            dCell5.Text = "A"
                            dCell5.ForeColor = AvailableForeColor
                            dCell5.BackColor = AvailableBackColor

                        Else
                            dCell5.Text = "na"
                            dCell5.ForeColor = NotAvailableForeColor
                            dCell5.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell5)

                        Dim dCell6 As New TableCell
                        If user2.SanFrancisco = True Then
                            dCell6.Text = "A"
                            dCell6.ForeColor = AvailableForeColor
                            dCell6.BackColor = AvailableBackColor

                        Else
                            dCell6.Text = "na"
                            dCell6.ForeColor = NotAvailableForeColor
                            dCell6.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell6)

                        Dim dCell7 As New TableCell
                        If user2.SanDiego = True Then
                            dCell7.Text = "A"
                            dCell7.ForeColor = AvailableForeColor
                            dCell7.BackColor = AvailableBackColor

                        Else
                            dCell7.Text = "na"
                            dCell7.ForeColor = NotAvailableForeColor
                            dCell7.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell7)

                        Dim dCell8 As New TableCell
                        If user2.Pittsburg = True Then
                            dCell8.Text = "A"
                            dCell8.ForeColor = AvailableForeColor
                            dCell8.BackColor = AvailableBackColor

                        Else
                            dCell8.Text = "na"
                            dCell8.ForeColor = NotAvailableForeColor
                            dCell8.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell8)

                        Dim dCell9 As New TableCell
                        If user2.Cincinnati = True Then
                            dCell9.Text = "A"
                            dCell9.ForeColor = AvailableForeColor
                            dCell9.BackColor = AvailableBackColor

                        Else
                            dCell9.Text = "na"
                            dCell9.ForeColor = NotAvailableForeColor
                            dCell9.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell9)

                        Dim dCell10 As New TableCell
                        If user2.Toronto = True Then
                            dCell10.Text = "A"
                            dCell10.ForeColor = AvailableForeColor
                            dCell10.BackColor = AvailableBackColor

                        Else
                            dCell10.Text = "na"
                            dCell10.ForeColor = NotAvailableForeColor
                            dCell10.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell10)

                        Dim dCell11 As New TableCell
                        If user2.NYYankees = True Then
                            dCell11.Text = "A"
                            dCell11.ForeColor = AvailableForeColor
                            dCell11.BackColor = AvailableBackColor

                        Else
                            dCell11.Text = "na"
                            dCell11.ForeColor = NotAvailableForeColor
                            dCell11.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell11)

                        Dim dCell12 As New TableCell
                        If user2.Boston = True Then
                            dCell12.Text = "A"
                            dCell12.ForeColor = AvailableForeColor
                            dCell12.BackColor = AvailableBackColor

                        Else
                            dCell12.Text = "na"
                            dCell12.ForeColor = NotAvailableForeColor
                            dCell12.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell12)

                        Dim dCell13 As New TableCell
                        If user2.TampaBay = True Then
                            dCell13.Text = "A"
                            dCell13.ForeColor = AvailableForeColor
                            dCell13.BackColor = AvailableBackColor

                        Else
                            dCell13.Text = "na"
                            dCell13.ForeColor = NotAvailableForeColor
                            dCell13.BackColor = NotAvailableBackColor
                        End If
                        dRow.Cells.Add(dCell13)

                        Dim dCell14 As New TableCell
                        If user2.Atlanta = True Then
                            dCell14.Text = "A"
                            dCell14.ForeColor = AvailableForeColor
                            dCell14.BackColor = AvailableBackColor

                        Else
                            dCell14.Text = "na"
                            dCell14.ForeColor = NotAvailableForeColor
                            dCell14.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell14)

                        Dim dCell15 As New TableCell
                        If user2.Philadelphia = True Then
                            dCell15.Text = "A"
                            dCell15.ForeColor = AvailableForeColor
                            dCell15.BackColor = AvailableBackColor
                        Else
                            dCell15.Text = "na"
                            dCell15.ForeColor = NotAvailableForeColor
                            dCell15.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell15)

                        Dim dCell16 As New TableCell
                        If user2.ChicagoWhiteSox = True Then
                            dCell16.Text = "A"
                            dCell16.ForeColor = AvailableForeColor
                            dCell16.BackColor = AvailableBackColor

                        Else
                            dCell16.Text = "na"
                            dCell16.ForeColor = NotAvailableForeColor
                            dCell16.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell16)

                        Dim dCell17 As New TableCell
                        If user2.Detroit = True Then
                            dCell17.Text = "A"
                            dCell17.ForeColor = AvailableForeColor
                            dCell17.BackColor = AvailableBackColor

                        Else
                            dCell17.Text = "na"
                            dCell17.ForeColor = NotAvailableForeColor
                            dCell17.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell17)

                        Dim dCell18 As New TableCell
                        If user2.KansasCity = True Then
                            dCell18.Text = "A"
                            dCell18.ForeColor = AvailableForeColor
                            dCell18.BackColor = AvailableBackColor

                        Else
                            dCell18.Text = "na"
                            dCell18.ForeColor = NotAvailableForeColor
                            dCell18.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell18)

                        Dim dCell19 As New TableCell
                        If user2.Cleveland = True Then
                            dCell19.Text = "A"
                            dCell19.ForeColor = AvailableForeColor
                            dCell19.BackColor = AvailableBackColor

                        Else
                            dCell19.Text = "na"
                            dCell19.ForeColor = NotAvailableForeColor
                            dCell19.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell19)

                        Dim dCell20 As New TableCell
                        If user2.Milwaukee = True Then
                            dCell20.Text = "A"
                            dCell20.ForeColor = AvailableForeColor
                            dCell20.BackColor = AvailableBackColor

                        Else
                            dCell20.Text = "na"
                            dCell20.ForeColor = NotAvailableForeColor
                            dCell20.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell20)

                        Dim dCell21 As New TableCell
                        If user2.LADodgers = True Then
                            dCell21.Text = "A"
                            dCell21.ForeColor = AvailableForeColor
                            dCell21.BackColor = AvailableBackColor

                        Else
                            dCell21.Text = "na"
                            dCell21.ForeColor = NotAvailableForeColor
                            dCell21.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell21)

                        Dim dCell22 As New TableCell
                        If user2.Minnesota = True Then
                            dCell22.Text = "A"
                            dCell22.ForeColor = AvailableForeColor
                            dCell22.BackColor = AvailableBackColor

                        Else
                            dCell22.Text = "na"
                            dCell22.ForeColor = NotAvailableForeColor
                            dCell22.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell22)

                        Dim dCell23 As New TableCell
                        If user2.Oakland = True Then
                            dCell23.Text = "A"
                            dCell23.ForeColor = AvailableForeColor
                            dCell23.BackColor = AvailableBackColor

                        Else
                            dCell23.Text = "na"
                            dCell23.ForeColor = NotAvailableForeColor
                            dCell23.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell23)

                        Dim dCell24 As New TableCell
                        If user2.Houston = True Then
                            dCell24.Text = "A"
                            dCell24.ForeColor = AvailableForeColor
                            dCell24.BackColor = AvailableBackColor

                        Else
                            dCell24.Text = "na"
                            dCell24.ForeColor = NotAvailableForeColor
                            dCell24.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell24)

                        Dim dCell25 As New TableCell
                        If user2.Texas = True Then
                            dCell25.Text = "A"
                            dCell25.ForeColor = AvailableForeColor
                            dCell25.BackColor = AvailableBackColor

                        Else
                            dCell25.Text = "na"
                            dCell25.ForeColor = NotAvailableForeColor
                            dCell25.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell25)

                        Dim dCell26 As New TableCell
                        If user2.STLouis = True Then
                            dCell26.Text = "A"
                            dCell26.ForeColor = AvailableForeColor
                            dCell26.BackColor = AvailableBackColor

                        Else
                            dCell26.Text = "na"
                            dCell26.ForeColor = NotAvailableForeColor
                            dCell26.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell26)

                        Dim dCell27 As New TableCell
                        If user2.ChicagoCubs = True Then
                            dCell27.Text = "A"
                            dCell27.ForeColor = AvailableForeColor
                            dCell27.BackColor = AvailableBackColor

                        Else
                            dCell27.Text = "na"
                            dCell27.ForeColor = NotAvailableForeColor
                            dCell27.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell27)

                        Dim dCell28 As New TableCell
                        If user2.LAAngels = True Then
                            dCell28.Text = "A"
                            dCell28.ForeColor = AvailableForeColor
                            dCell28.BackColor = AvailableBackColor

                        Else
                            dCell28.Text = "na"
                            dCell28.ForeColor = NotAvailableForeColor
                            dCell28.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell28)

                        Dim dCell29 As New TableCell
                        If user2.Seattle = True Then
                            dCell29.Text = "A"
                            dCell29.ForeColor = AvailableForeColor
                            dCell29.BackColor = AvailableBackColor

                        Else
                            dCell29.Text = "na"
                            dCell29.ForeColor = NotAvailableForeColor
                            dCell29.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell29)

                        Dim dCell30 As New TableCell
                        If user2.NYMets = True Then
                            dCell30.Text = "A"
                            dCell30.ForeColor = AvailableForeColor
                            dCell30.BackColor = AvailableBackColor

                        Else
                            dCell30.Text = "na"
                            dCell30.ForeColor = NotAvailableForeColor
                            dCell30.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell30)

                        Dim dCell31 As New TableCell
                        If user2.Baltimore = True Then
                            dCell31.Text = "A"
                            dCell31.ForeColor = AvailableForeColor
                            dCell31.BackColor = AvailableBackColor

                        Else
                            dCell31.Text = "na"
                            dCell31.ForeColor = NotAvailableForeColor
                            dCell31.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell31)

                        Contenders2.Rows.Add(dRow)

                    Next

                    Contenders2.DataBind()


                    Dim queryLosers = (From loser1 In _dbLoserPool.LoserList
                                       Order By loser1.DayId Descending).ToList

                    Dim queryDays = (From timePeriod1 In _dbLoserPool.ScheduleTimePeriods
                                       Where Not timePeriod1.startDayDate Is Nothing
                                       Order By timePeriod1.dayID Descending).ToList

                    For Each day1 In queryDays

                        Dim queryDayLosers = (From loser1 In _dbLoserPool.LoserList
                                              Where loser1.DayId = day1.dayID).ToList

                        For Each loser1 In queryDayLosers

                            If loser1.LosingPick <> "Not Made" Then
                                Dim loser2 = New Loser
                                loser2.UserName = loser1.UserName
                                loser2.DayId = loser1.DayId
                                loser2.LosingPick = loser1.LosingPick
                                LoserCollectionSorted.Add(loser2.UserName, loser2)
                            End If
                        Next
                        For Each loser1 In queryDayLosers
                            If loser1.LosingPick = "Not Made" Then
                                Dim loser2 = New Loser
                                loser2.UserName = loser1.UserName
                                loser2.DayId = loser1.DayId
                                loser2.LosingPick = loser1.LosingPick
                                LoserCollectionSorted.Add(loser2.UserName, loser2)
                            End If
                        Next


                    Next


                    For Each loser1 In LoserCollectionSorted

                        Dim dRow As New TableRow

                        Dim dCell1 As New TableCell
                        dCell1.Text = loser1.Value.UserName
                        dCell1.ForeColor = Drawing.Color.Red
                        dCell1.BackColor = Drawing.Color.LightSalmon
                        dRow.Cells.Add(dCell1)

                        Dim dCell2 As New TableCell
                        dCell2.Text = loser1.Value.DayId
                        dCell2.ForeColor = Drawing.Color.Red
                        dCell2.BackColor = Drawing.Color.LightSalmon
                        dRow.Cells.Add(dCell2)

                        Dim dCell3 As New TableCell
                        dCell3.Text = loser1.Value.LosingPick
                        dCell3.ForeColor = Drawing.Color.Red
                        dCell3.BackColor = Drawing.Color.LightSalmon
                        dRow.Cells.Add(dCell3)

                        LoserTable1.Rows.Add(dRow)
                    Next

                    LoserTable1.DataBind()

                    ViewState("dayNumber") = dayId

                End Using
            Catch ex As Exception

            End Try


        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("~/JoinPool/MyPools.aspx")
    End Sub


End Class