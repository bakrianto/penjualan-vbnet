Imports MySql.Data.MySqlClient

Public Class Form2
    Dim conn As MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myAdapter As New MySqlDataAdapter
    Dim myData As New DataTable
    Dim SQL As String
    Dim RD As MySqlDataReader

    Sub konek()
        conn = New MySqlConnection()
        conn.ConnectionString = "server=localhost;user id=root;" &
                                "password=;database=penjualan"
        conn.Open()
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                MessageBox.Show("Usernama atau Password masih kosong")
            Else
                Call konek()

                myCommand.Connection = conn
                myCommand.CommandType = CommandType.Text
                myCommand.CommandText = "select * from pengguna where username='" & TextBox1.Text & "' and password = '" & TextBox2.Text & "'"
                RD = myCommand.ExecuteReader()
                RD.Read()

                If RD.HasRows Then
                    If RD("level").ToString = "owner" Then
                        Me.Hide()
                        FormUtama.Show()
                    ElseIf RD("level").ToString = "manager" Then
                        Me.Hide()
                        Form7.Show()
                    Else
                        Me.Hide()
                        Form8.Show()
                    End If
                Else
                    MessageBox.Show("Anda belum terdaftar")

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class