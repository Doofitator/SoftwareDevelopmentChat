Public Class frm_settings
    Private Sub btn_color_Click(sender As Object, e As EventArgs) Handles btn_color.Click
        If dlog_color.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            WriteColor(dlog_color.Color, frm_main.grp_chat.Text)
        End If
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

    Private Sub Btn_font_Click(sender As Object, e As EventArgs) Handles btn_font.Click
        If dlog_font.ShowDialog <> Windows.Forms.DialogResult.Cancel Then

            Dim converter = New FontConverter()
            Dim FontAsString As String = converter.ConvertToString(dlog_font.Font)
            writeFont(FontAsString, frm_main.grp_chat.Text)
        End If
    End Sub
End Class