Imports System.Data
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myAdapter As New MySqlDataAdapter
    Dim RD As MySqlDataReader
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
        SQL = "select * from barang"

            myCommand.Connection = conn
            myCommand.CommandText = SQL

            myAdapter.SelectCommand = myCommand
            myAdapter.Fill(myData)

        DataGridView1.DataSource = myData
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
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
    End Sub
    Sub aturDGV()
        Try
            DGV.Columns(0).HeaderText = "no"
            DGV.Columns(1).HeaderText = "Nama"
            DGV.Columns(2).HeaderText = "email"
            DGV.Columns(3).HeaderText = "username"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilP()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or
           TextBox2.Text = "" Or
           TextBox3.Text = "" Or
           TextBox4.Text = "" Or
           TextBox5.Text = "" Or
           TextBox6.Text = "" Or
           TextBox7.Text = "" Then
            MsgBox("Data Belum Terisi Dengan Lengkap!")
        Else
            SQL = "insert into barang (kode,kategori,nama,quantity,harga_beli,harga_jual,stok) 
                  values ('" & TextBox1.Text & "',
                          '" & TextBox2.Text & "',
                          '" & TextBox3.Text & "',
                          '" & TextBox4.Text & "',
                          '" & TextBox5.Text & "',
                          '" & TextBox6.Text & "',
                          '" & TextBox7.Text & "' )"
            Call bersihkanForm()
            MsgBox("Data Berhasil Tersimpan")
            Call tampilP()
            DataGridView1.Refresh()
        End If
    End Sub
    Private Sub DataGridView1_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        Dim a As Integer
        a = DataGridView1.CurrentRow.Index
        If a = DataGridView1.NewRowIndex Then
            MsgBox("Data Tidak Ada!")
        Else
            TextBox1.Text = DataGridView1.Item(1, a).Value
            TextBox2.Text = DataGridView1.Item(2, a).Value
            TextBox3.Text = DataGridView1.Item(3, a).Value
            TextBox4.Text = DataGridView1.Item(4, a).Value
            TextBox5.Text = DataGridView1.Item(5, a).Value
            TextBox6.Text = DataGridView1.Item(6, a).Value
            TextBox7.Text = DataGridView1.Item(7, a).Value
            Call konek()
            myCommand.Connection = conn
            myCommand.CommandType = CommandType.Text
            myCommand.CommandText = "select * from barang where kode='" & TextBox1.Text & "'"
            RD = myCommand.ExecuteReader()
            RD.Read()
            Button1.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SQL = "UPDATE barang set kode='" & TextBox1.Text & "',kategori='" & TextBox2.Text & "',nama='" & TextBox3.Text & "',quantity ='" & TextBox4.Text & "',harga_beli='" & TextBox5.Text & "',harga_jual='" & TextBox6.Text & "',stok='" & TextBox7.Text & "' where kode='" & TextBox1.Text & "'"
        Call bersihkanForm()
        MsgBox("Data Telah Diperbarui!")
        'Call tampilP().
        DataGridView1.Refresh()
        Button1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label10.Text = TimeOfDay
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        bersihkanForm()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        tampilP()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class

