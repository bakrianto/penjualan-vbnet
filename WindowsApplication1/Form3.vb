Imports MySql.Data.MySqlClient

Public Class Form3
    Dim conn As MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myAdapter As New MySqlDataAdapter
    Dim myData As New DataTable
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
            SQL = "select * from pengguna"

            myCommand.Connection = conn
            myCommand.CommandText = SQL
            myData.Clear()

            myAdapter.SelectCommand = myCommand
            myAdapter.Fill(myData)

            DataGridView1.DataSource = myData

            conn.Close()
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
        TextBox3.Text = ""
        ComboBox1.SelectedItem = ""
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilP()
        ComboBox1.Items.Add("owner")
        ComboBox1.Items.Add("manager")
        ComboBox1.Items.Add("shopkeeper")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or
           TextBox2.Text = "" Or
           TextBox3.Text = "" Or
           ComboBox1.SelectedItem = "" Then
            MsgBox("Data Belum Terisi Dengan Lengkap!")
        Else
            SQL = "insert into pengguna (id,nama,username,password,level) 
                  values ('""',
                          '" & TextBox1.Text & "',
                          '" & TextBox2.Text & "',
                          '" & TextBox3.Text & "',
                          '" & ComboBox1.SelectedItem & "' )"
            Call bersihkanForm()
            MsgBox("Data Berhasil Tersimpan")
            Call tampilP()
            DataGridView1.Refresh()
        End If
    End Sub
End Class
