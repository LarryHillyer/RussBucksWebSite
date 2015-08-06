Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema


Public Class PoolParameter

    <Key>
    Public Property poolId As Int32
    Public Property Sport As String
    'Public Property timeIncrement As String
    Public Property poolAlias As String

End Class
