Public Class frm_settings
    Private Sub btn_color_Click(sender As Object, e As EventArgs) Handles btn_color.Click
        dlog_color.ShowDialog()
    End Sub

    Private Sub frm_settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        grp_chatSettings.Text = "'" & frm_main.grp_chat.Text & "' Chat Settings"
    End Sub
End Class