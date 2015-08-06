Imports System.Data.Entity

Namespace PlayoffPool.Models
    Public Class PlayoffPoolContext
        Inherits DbContext

        Public Property playoffPoolUsers As DbSet(Of playoffPoolUser)

        Public Sub New()
            MyBase.New("PlayoffPool-RussBucks-Test")
        End Sub

    End Class
End Namespace
