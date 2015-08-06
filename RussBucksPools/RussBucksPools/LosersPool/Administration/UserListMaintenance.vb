Imports System
Imports System.Linq
Imports System.Web

Imports RussBucksPools.LosersPool
Imports RussBucksPools.LosersPool.Models

Namespace LosersPool.Administration
    Public Class UserListMaintenance
        Implements IDisposable

        Private _db As New LosersPoolContext

        Public Property UserId As String

        Public Sub New()

        End Sub

        Public Sub AddUsers()

            'UserId = GetUserId()

            Dim newUser As New LosersPool.Models.User
            newUser.UserId = UserId
            newUser.Administrator = True

        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose

            If Not (_db Is Nothing) Then
                _db.Dispose()
                _db = Nothing
            End If

        End Sub

        'Public Function GetUserId() As String

        'End Function



    End Class
End Namespace