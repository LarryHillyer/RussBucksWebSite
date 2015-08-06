Imports RussBucksPools.JoinPools.Models

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim _dbPools1 As New PoolDbContext
        Dim _dbApp1 As New ApplicationDbContext

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        Dim userId = CStr(Session("userId"))

        Try
            Using (_dbPools1)
                Using (_dbApp1)
                    Dim queryLicensees = (From licensee1 In _dbPools1.Licensees
                                          Where licensee1.licenseeUserId = userId).SingleOrDefault

                    Dim querySuperUser = (From super1 In _dbApp1.SuperUsers
                                          Where super1.SuperUserName = userId).SingleOrDefault

                    If queryLicensees Is Nothing Or querySuperUser Is Nothing Then
                    Else
                        Label3.Visible = True
                        Label1.Visible = True
                        PoolCommisioner1.Visible = True
                        Label2.Visible = True
                        CommisionerCode1.Visible = True
                        Submit1.Visible = True
                    End If
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Submit1_Click(sender As Object, e As EventArgs)

        Dim _dbPools2 As New PoolDbContext

        Dim commisionerEmail = PoolCommisioner1.Text
        Dim commisionerCode = CommisionerCode1.Text

        Try
            Using (_dbPools2)
                Dim poolAdministrator1 As New PoolAdministrator1

                If commisionerEmail = "" Or commisionerEmail Is Nothing Or commisionerCode = "" Or commisionerCode Is Nothing Then
                    If commisionerEmail = "" Or commisionerEmail Is Nothing Then
                        Label4.Text = "Error: Enter E-mail"
                        Label4.Visible = True
                    Else
                        Label4.Text = "Error: Enter Commisioner Code"
                        Label4.Visible = True
                    End If
                Else
                    Dim userId As String
                    Dim user1 As String = CStr(commisionerEmail)

                    Dim Ai As Int16 = InStr(user1, "@")
                    Dim Pi As Int16 = InStr(user1, ".")

                    userId = Left(user1, Ai - 1) + Mid(user1, (Ai + 1), Pi - Ai - 1) + Right(user1, Len(user1) - Pi)

                    poolAdministrator1.PoolAdministrator = userId
                    poolAdministrator1.LicenseeUserId = CStr(Session("userId"))
                    poolAdministrator1.PoolAdministratorAlias = commisionerCode

                    _dbPools2.PoolAdministrators.Add(poolAdministrator1)
                    _dbPools2.SaveChanges()

                    Label4.Visible = False
                    PoolCommisioner1.Text = ""
                    CommisionerCode1.Text = ""
                End If


            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class