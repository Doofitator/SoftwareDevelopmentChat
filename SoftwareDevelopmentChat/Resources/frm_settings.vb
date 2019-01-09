Public Class frm_settings
    Private Sub btn_color_Click(sender As Object, e As EventArgs) Handles btn_color.Click
        If dlog_color.ShowDialog <> Windows.Forms.DialogResult.Cancel Then 'if user actually chose a color
            writeColor(dlog_color.Color, frm_main.grp_chat.Text) 'write the color to the database
        End If
        loadMessages() 'reload messages to refresh colors
    End Sub

    Private Sub frm_settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        grp_chatSettings.Text = "'" & frm_main.grp_chat.Text & "' Chat Options" 'make it look pretty

        If frm_main.chk_rememberMe.Checked = True Then btn_logout.Enabled = True 'enable logout button if you're permanantly logged in (else you can just close and open the app again to logout)
    End Sub

    Private Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        My.Settings.userName = ""           '|
        My.Settings.userPassword = ""       '| clear all   
        My.Settings.rememberMe = False      '| app settings
        My.Settings.Save()                  '|
        MsgBox("App will now close.", vbInformation, "Logout successful")
        frm_main.Close() 'close so they have to log back in next time they open it
    End Sub

    Private Sub btn_download_Click(sender As Object, e As EventArgs) Handles btn_download.Click
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "txt files (*.txt)|*.txt"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            IO.File.WriteAllLines(saveFileDialog1.FileName, CType(getMessagesArr(frm_main.grp_chat.Text, , True), String()))
        End If
    End Sub
End Class