Imports MySql.Data.MySqlClient

Public Class Form4
    Dim conn As MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myAdapter As New MySqlDataAdapter
    Dim myData As New DataTable
    Dim RD As MySqlDataReader
    Dim SQL As String
    Dim DGV As DataGridView
    Sub konek()
        conn = New MySqlConnection()
        conn.ConnectionString = "server=localhost;user id=root;" &
                                "password=;database=penjualan"
        conn.Open()
    End Sub
    Sub tampilP()
        Call konek()

        Try
            SQL = "select * from kategori"

            myCommand.Connection = conn
            myCommand.CommandText = SQL
            myData.Clear()

            myAdapter.SelectCommand = myCommand
            myAdapter.Fill(myData)

            DataGridView1.DataSource = myData

        Catch myerror As MySqlException
            MessageBox.Show("Error: " & myerror.Message)
        Finally
            conn.Dispose()
        End Try
    End Sub
    Sub bersihkanForm()
        Call konek()
        myCommand.Connection = conn
        myCommand.CommandType = CommandType.Text
        myCommand.CommandText = SQL
        myCommand.ExecuteNonQuery()
        myCommand.Dispose()
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilP()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or
           TextBox2.Text = "" Then
            MsgBox("Data Belum Terisi Dengan Lengkap!")
        Else
            SQL = "insert into kategori (id,kode,nama) 
                  values ('""',
                          '" & TextBox1.Text & "',
                          '" & TextBox2.Text & "' )"
            Call bersihkanForm()
            MsgBox("Data Berhasil Tersimpan")
            Call tampilP()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SQL = "DELETE from kategori where kode = '" & TextBox1.Text & "'"
        myCommand.Connection = conn
        myCommand.CommandText = SQL
        myAdapter.SelectCommand = myCommand
        MsgBox("Data Dihapus!")
        Call bersihkanForm()
        Call tampilP()

        Button1.Enabled = True
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        Dim a As Integer
        a = DataGridView1.CurrentRow.Index
        If a = DataGridView1.NewRowIndex Then
            MsgBox("Data Tidak Ada!")
        Else
            TextBox3.Text = DataGridView1.Item(0, a).Value
            TextBox1.Text = DataGridView1.Item(1, a).Value
            TextBox2.Text = DataGridView1.Item(2, a).Value
            Call konek()
            myCommand.Connection = conn
            myCommand.CommandType = CommandType.Text
            myCommand.CommandText = "select * from kategori where kode='" & TextBox1.Text & "'"
            RD = myCommand.ExecuteReader()
            RD.Read()
            Button1.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SQL = "UPDATE kategori set kode='" & TextBox1.Text & "',nama='" & TextBox2.Text & "' where id='" & TextBox3.Text & "'"
        Call bersihkanForm()
        MsgBox("Data Telah Diperbarui!")
        Call tampilP()
        Button1.Enabled = True
    End Sub
End Class