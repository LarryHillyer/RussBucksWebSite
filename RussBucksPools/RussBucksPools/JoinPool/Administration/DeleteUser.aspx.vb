﻿Imports RussBucksPools.LosersPool.Models
Imports RussBucksPools.JoinPools.Models
Public Class DeleteUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserHandle1.Focus()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim _dbLosersPool As New LosersPoolContext
        Dim _dbPools As New PoolDbContext
        Dim poolAlias = PoolName1.Text

        Try
            Using (_dbLosersPool)
                Using (_dbPools)

                    Dim queryPool = (From pool1 In _dbPools.PoolParameters
                                     Where pool1.poolAlias = poolAlias).SingleOrDefault
                    If queryPool Is Nothing Then
                        PoolName1.Text = ""
                        InvalidPool1.Text = "Error: Invalid Pool"
                        InvalidPool1.ForeColor = Drawing.Color.Red
                    Else
                        Dim queryUser = (From user1 In _dbPools.BarPoolList
                                         Where user1.UserName = UserHandle1.Text And user1.PoolAlias = poolAlias).SingleOrDefault

                        If queryUser Is Nothing Then
                            UserHandle1.Text = ""
                            InvalidHandle1.Text = "Error: Invalid User Handle"
                            InvalidHandle1.ForeColor = Drawing.Color.Red
                            Exit Sub
                        Else
                            _dbPools.BarPoolList.Remove(queryUser)

                            _dbPools.SaveChanges()

                            Dim queryUserChoices = (From userChoice1 In _dbLosersPool.UserChoicesList
                                                    Where userChoice1.UserName = queryUser.UserName And userChoice1.PoolAlias = poolAlias).ToList

                            If queryUserChoices.Count > 0 Then
                                For Each user1 In queryUserChoices
                                    _dbLosersPool.UserChoicesList.Remove(user1)
                                Next
                            End If

                            Dim queryLoser = (From loser1 In _dbLosersPool.LoserList
                                               Where loser1.UserName = queryUser.UserName And loser1.PoolAlias = poolAlias).SingleOrDefault

                            If queryLoser Is Nothing Then
                            Else
                                _dbLosersPool.LoserList.Remove(queryLoser)
                            End If

                            _dbLosersPool.SaveChanges()

                            UserHandle1.Text = ""
                            Response.Redirect("~/JoinPool/Default.aspx")

                        End If

                    End If

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class