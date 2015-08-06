Imports System.Data.Entity

Namespace JoinPools.Models

    Public Class PoolDbContext

        Inherits DbContext

        Public Property Pools As DbSet(Of Pool)
        Public Property Teams As DbSet(Of Team)
        Public Property BarPoolList As DbSet(Of PoolAlias1)
        Public Property PoolAdministrators As DbSet(Of PoolAdministrator1)
        Public Property PoolParameters As DbSet(Of PoolParameter)
        Public Property Sports As DbSet(Of Sport)
        Public Property MBLBaseballScheduledGames As DbSet(Of MLBScheduledGame)
        Public Property CustomMLBScheduledGames As DbSet(Of CustomMLBScheduleGame)
        Public Property QueuedScheduledGames As DbSet(Of QueuedScheduleGame)
        Public Property Tests As DbSet(Of Test)
        Public Property Licensees As DbSet(Of licensee)

        Public Sub New()

            MyBase.New("Pools-RussBucks-WebSite")

        End Sub

    End Class



End Namespace

