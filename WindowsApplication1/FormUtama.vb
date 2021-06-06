Public Class FormUtama

    Private Sub MasterPenggunaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterPenggunaToolStripMenuItem.Click
        Form3.Visible = True
    End Sub

    Private Sub FormUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form3.Visible = False
        Form1.Visible = False
        Form4.Visible = False
        Form5.Visible = False
    End Sub

    Private Sub FormUtama_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True

    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        Dim kaluar As String
        kaluar = MsgBox("Apakah Yakin Mau keluar?", vbYesNo, "toko Apprel Store")
        If kaluar = vbYes Then
            End
        End If
    End Sub

    Private Sub MasterBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterBarangToolStripMenuItem.Click
        Form1.Visible = True
    End Sub

    Private Sub MasterKategoriToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriToolStripMenuItem.Click
        Form4.Visible = True

    End Sub

    Private Sub DiskonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiskonToolStripMenuItem.Click
        Form5.Visible = True

    End Sub

    Private Sub PromoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PromoToolStripMenuItem.Click
        Form6.Visible = True
    End Sub
End Class