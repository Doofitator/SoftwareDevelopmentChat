Public Class frm_richText

    Dim editor As New LiveSwitch.TextControl.Editor

    Private Sub frm_richText_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        editor.Top = 0
        editor.Left = 0
        editor.Width = Me.Width
        editor.Height = Me.Height - 66
        editor.DocumentText = frm_main.txt_message.Text
        Me.Controls.Add(editor)
    End Sub

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
        frm_main.txt_message.Text = editor.BodyHtml
        frm_main.btn_send.PerformClick()
        Me.Close()
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Me.Close()
    End Sub
End Class