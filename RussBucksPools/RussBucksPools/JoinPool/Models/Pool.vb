Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports RussBucksPools.JoinPools.Models
Namespace JoinPools.Models
    Public Class Pool

        <Key>
        Public Property PoolId As Int32

        <Required>
        Public Property PoolName As String

        Public Property Sport As String
        Public Property timePeriodName As String
        Public Property timePeriodIncrement As String


    End Class
    Public Class SeedPools
        Public Sub New()
            Dim _dbPools2 As New PoolDbContext

            Try

                Using (_dbPools2)

                    Dim querypool1 = (From user1 In _dbPools2.Pools).ToList

                    If querypool1.Count >= 1 Then
                        Exit Sub
                    End If

                    Dim pool1 As New Pool

                    pool1.PoolName = "LoserPool"
                    pool1.Sport = "baseball"
                    pool1.timePeriodIncrement = "1"
                    pool1.timePeriodName = "Day"

                    _dbPools2.Pools.Add(pool1)

                    pool1 = New Pool

                    pool1.PoolName = "LoserPool"
                    pool1.Sport = "football"
                    pool1.timePeriodIncrement = "1"
                    pool1.timePeriodName = "Week"

                    _dbPools2.Pools.Add(pool1)

                    pool1 = New Pool

                    pool1.PoolName = "PlayoffPool"
                    pool1.Sport = "football"
                    pool1.timePeriodIncrement = "1"
                    pool1.timePeriodName = "Round"

                    _dbPools2.Pools.Add(pool1)

                    _dbPools2.SaveChanges()
                End Using
            Catch ex As Exception

            End Try

        End Sub

    End Class


End Namespace
