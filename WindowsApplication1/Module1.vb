Imports MySql.Data.MySqlClient
Module Module1
    Private Function ConString() As String
        Return "server=localhost;user=root;password=;database=test"
    End Function

    Public conn As MySqlConnection
    Public Function KonekDb() As MySqlConnection
        Try
            conn = New MySqlConnection(ConString)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return conn
    End Function
End Module
