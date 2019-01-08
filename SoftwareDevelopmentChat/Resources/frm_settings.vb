Public Class frm_settings
    Private Sub btn_color_Click(sender As Object, e As EventArgs) Handles btn_color.Click
        dlog_color.ShowDialog()
    End Sub

    Private Sub frm_settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        grp_chatSettings.Text = "'" & frm_main.grp_chat.Text & "' Chat Settings"

        If frm_main.chk_rememberMe.Checked = True Then btn_logout.Enabled = True
    End Sub

    Private Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        My.Settings.userName = ""           '|
        My.Settings.userPassword = ""       '| clear all   
        My.Settings.rememberMe = False      '| app settings
        My.Settings.Save()                  '|
        MsgBox("App will now close.", vbInformation, "Logout successful")
        frm_main.Close() 'close so they have to log back in next time they open it
    End Sub
End Class