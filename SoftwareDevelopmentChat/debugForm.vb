Public Class frm_debug 'this form will be removed or hidden in the final version.
    Private Sub btn_forceLogin_Click(sender As Object, e As EventArgs) Handles btn_forceLogin.Click
        login()
    End Sub

    Private Sub btn_forceCredentialSave(sender As Object, e As EventArgs) Handles btn_forceCredentials.Click
        If frm_main.chk_rememberMe.Checked Then
            My.Settings.userName = frm_main.txt_userName.Text
            My.Settings.userPassword = frm_main.txt_password.Text
            My.Settings.Save()
        End If
    End Sub

    Private Sub btn_newUser_Click(sender As Object, e As EventArgs) Handles btn_newUser.Click
        writeNewUser(frm_main.txt_userName.Text, frm_main.txt_passwordRepeat.Text)
    End Sub
End Class