Public Class FormUtama

    Private Sub MasterPenggunaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterPenggunaToolStripMenuItem.Click
        Form3.Visible = True
    End Sub

    Private Sub FormUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form3.Visible = False

    End Sub
End Class