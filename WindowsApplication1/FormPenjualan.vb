Imports MySql.Data.MySqlClient
Imports System.Data

Public Class FormPenjualan
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
            SQL = "select * from cart"

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
        ComboBox1.SelectedItem = ""
    End Sub
    Sub simpanCart()
        Call konek()
        myCommand.Connection = conn
        myCommand.CommandType = CommandType.Text
        myCommand.CommandText = SQL
        myCommand.ExecuteNonQuery()
        myCommand.Dispose()
    End Sub
    Private Sub FormPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilP()
        TextBox1.Text = Format(Now, "yyyy-MM-dd")
        getShopkeeper()
        Hitung_DATAGRIDVIEW()
    End Sub
    Sub getShopkeeper()
        Call konek()
        myCommand.Connection = conn
        myCommand.CommandText = "select nama from pengguna where level = 'shopkeeper'"

        Dim AD As New MySqlDataAdapter
        Dim DT As New DataTable

        AD.SelectCommand = myCommand
        AD.Fill(DT)

        For Each row As DataRow In DT.Rows
            ComboBox1.Items.Add(row.Item("nama"))
        Next

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or
           ComboBox1.SelectedItem = "" Then
            MsgBox("Data Belum Terisi Dengan Lengkap!")
        Else
            SQL = "insert into penjualan (id,diskon,pengguna,barang,tanggal,total) 
                  values ('""',
                          '" & TextBox1.Text & "',
                          '" & ComboBox1.SelectedItem & "',
                          '""',
                          '" & TextBox3.Text & "',
                          '""',
                          )"
            Call bersihkanForm()
            MsgBox("Data Berhasil Tersimpan")
            Call tampilP()
            TextBox4.Text = ""
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Call konek()
        myCommand.Connection = conn
        myCommand.CommandType = CommandType.Text
        myCommand.CommandText = "select * from barang where kode = '" & TextBox4.Text & "'"
        RD = myCommand.ExecuteReader()
        RD.Read()

        If RD.HasRows Then
            SQL = "insert into cart (no,kd_barang,nama_barang,quantity,harga,jumlah,shopkeeper,no_nota,tanggal,total)
                    value ('""',
                            '" & RD.Item("kode").ToString & "',
                            '" & RD.Item("nama").ToString & "',
                            '" & RD.Item("quantity".ToString) & "',
                            '" & RD.Item("harga_jual").ToString & "',
                            '1',
                            '" & ComboBox1.SelectedItem & "',
                            '" & TextBox3.Text & "',
                            '" & TextBox1.Text & "',
                            '" & RD.Item("harga_jual").ToString & "')"
            Call simpanCart()
            MsgBox("Tambah Barang lain?")
            Call tampilP()
            Hitung_DATAGRIDVIEW()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim selectedRow = DataGridView1.Columns(e.ColumnIndex)
            MsgBox(selectedRow.ToString)
        End If
    End Sub
    Dim mRow As Integer = 0
    Dim newpage As Boolean = True
    Private Sub PrintDocument1_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        With DataGridView1
            Dim fmt As StringFormat = New StringFormat(StringFormatFlags.LineLimit)
            fmt.LineAlignment = StringAlignment.Center
            fmt.Trimming = StringTrimming.EllipsisCharacter
            Dim y As Single = e.MarginBounds.Top
            Do While mRow < .RowCount
                Dim row As DataGridViewRow = .Rows(mRow)
                Dim x As Single = e.MarginBounds.Left
                Dim h As Single = 0
                For Each cell As DataGridViewCell In row.Cells
                    Dim rc As RectangleF = New RectangleF(x, y, cell.Size.Width, cell.Size.Height)
                    e.Graphics.DrawRectangle(Pens.Black, rc.Left, rc.Top, rc.Width, rc.Height)
                    If (newpage) Then
                        e.Graphics.DrawString(DataGridView1.Columns(cell.ColumnIndex).HeaderText, .Font, Brushes.Black, rc, fmt)
                    Else
                        e.Graphics.DrawString(DataGridView1.Rows(cell.RowIndex).Cells(cell.ColumnIndex).FormattedValue.ToString(), .Font, Brushes.Black, rc, fmt)
                    End If
                    x += rc.Width
                    h = Math.Max(h, rc.Height)
                Next
                newpage = False
                y += h
                mRow += 1
                If y + h > e.MarginBounds.Bottom Then
                    e.HasMorePages = True
                    mRow -= 1
                    newpage = True
                    Exit Sub
                End If
            Loop
            mRow = 0
        End With
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call konek()
        SQL = "truncate table cart"
        myCommand.Connection = conn
        myCommand.CommandType = CommandType.Text
        myCommand.CommandText = SQL
        myCommand.ExecuteNonQuery()
        myCommand.Dispose()
        Call tampilP()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = TimeOfDay
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox3.Text = ""
        ComboBox1.SelectedItem = ""
        TextBox5.Text = ""
        Call konek()
        SQL = "truncate table cart"
        myCommand.Connection = conn
        myCommand.CommandType = CommandType.Text
        myCommand.CommandText = SQL
        myCommand.ExecuteNonQuery()
        myCommand.Dispose()
        Call tampilP()
    End Sub
    Sub Hitung_DATAGRIDVIEW()
        Dim total As Double
        total = 0
        For t As Integer = 0 To DataGridView1.Rows.Count - 1
            total = total + Val(DataGridView1.Rows(t).Cells(9).Value)
            '-----------cell 2 disini menunjukan posisi field yang akan kita jumlahkan
        Next
        TextBox5.Text = total
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        Dim totBel As Int32 = Convert.ToInt32(TextBox5.Text)
        Dim bayar As Int32 = Convert.ToInt32(TextBox6.Text)
        TextBox7.Text = (bayar - totBel).ToString
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form4.Visible = True
    End Sub

End Class