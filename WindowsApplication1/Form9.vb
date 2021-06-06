Imports MySql.Data.MySqlClient
Imports System.Data
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Form9
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
            SQL = "select * from penjualan"

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
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
            Dim ExcelWorkBook As Microsoft.Office.Interop.Excel.Workbook
            Dim ExcelWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim a As Integer
            Dim b As Integer

            ExcelApp = New Microsoft.Office.Interop.Excel.Application
            ExcelWorkBook = ExcelApp.Workbooks.Add(misValue)
            ExcelWorkSheet = ExcelWorkBook.Sheets("sheet1")

            For a = 0 To DataGridView1.RowCount - 2
                For b = 0 To DataGridView1.ColumnCount - 1
                    For c As Integer = 1 To DataGridView1.Columns.Count
                        ExcelWorkSheet.Cells(1, c) = DataGridView1.Columns(c - 1).HeaderText
                        ExcelWorkSheet.Cells(a + 2, b + 1) = DataGridView1(b, a).Value.ToString()
                    Next
                Next
            Next

            ExcelWorkSheet.SaveAs("D:\DB\Penjualan " & Format(Now, "yyyy-MM-dd") & ".xlsx")
            ExcelWorkBook.Close()
            ExcelApp.Quit()

            'releaseObject(ExcelApp).
            'releaseObject(ExcelWorkBook).
            'releaseObject(ExcelWorkSheet).

            MsgBox("Hasil export tersimpan di D:\DB, dengan nama Penjualan " & Format(Now, "yyyy-MM-dd") & ".xlsx")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PrintPreviewDialog1.Document = PrintDocument1
        PrintDocument1.DefaultPageSettings.Landscape = True
        PrintPreviewDialog1.ShowDialog()
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilP()
        DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    End Sub
End Class