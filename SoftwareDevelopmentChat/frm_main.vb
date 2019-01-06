Public Class frm_main

    'TODO: Fix tabindex on this form. It's terrible.

    Private Sub frm_main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 421 'set the width to half what it is in the design editor (it's only double there so we can see both windows)
        If Me.Left < frm_conversations.Width + 50 Then 'make sure that the form loads far enough to the right that the conversations form fits next to it
            Me.UserMoving = False 'so that the other form doesn't move with this one
            Me.Left = frm_conversations.Width + 50
            Me.UserMoving = True 'back to normal
        End If

        If My.Settings.rememberMe = True Then               'if the app is configured to remember you
            txt_userName.Text = My.Settings.userName        'set all
            txt_password.Text = My.Settings.userPassword    'the saved
            chk_rememberMe.Checked = My.Settings.rememberMe 'settings
            btn_login_Click(sender, e)                      'and login
        End If

        frm_debug.Show() 'this is just a debugging form so we can test functions
    End Sub

    Private Sub rbtn_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_userNew.CheckedChanged, rbtn_userExist.CheckedChanged
        Dim control As RadioButton = CType(sender, RadioButton)
        If control Is rbtn_userExist Then
            lbl_passwordRepeat.Visible = False
            txt_passwordRepeat.Visible = False
            btn_login.Text = "Login"
        ElseIf control Is rbtn_userNew Then             'all this is just radio button stuff for the new user / existing user
            lbl_passwordRepeat.Visible = True
            txt_passwordRepeat.Visible = True
            btn_login.Text = "Sign Up"
        End If
    End Sub

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        If rbtn_userExist.Checked Then 'if it's an existing user
            'check password & user against database

            If passwordCorrect(txt_userName.Text, txt_password.Text) Then
                GoTo correctPassword
            Else
                MsgBox("Username or password incorrect. Please try again.", vbOKOnly, "Login failed") 'no database yet so login failed
                Exit Sub
            End If

correctPassword:

            'happy with password? Then are we supposed to remember you?
            If chk_rememberMe.Checked Then
                My.Settings.userName = txt_userName.Text
                My.Settings.userPassword = txt_password.Text
                My.Settings.Save()
            End If

            'Okay. That's all sorted, now login!
            login()
        ElseIf rbtn_userNew.Checked Then 'if it's a new user
            If txt_passwordRepeat.Text = txt_password.Text Then 'if both password fields match
                addUser(txt_userName.Text, txt_password.Text)
            Else
                MsgBox("Passwords do not match")
            End If
        End If
    End Sub

    ' // The following is for moving the conversation form with this one, adapted from http://www.vbforums.com/showthread.php?611932-snap-or-dock-forms-together#2 //
    Public UserMoving As Boolean = True

    Private Sub frm_main_move(sender As Object, e As EventArgs) Handles Me.Move
        If Me.UserMoving Then
            Dim mainRectangle = Me.Bounds
            Me.UserMoving = False   'tell the docked form that its the 'computer' moving the form, not the user
            frm_conversations.Location = New Point(mainRectangle.Left - frm_conversations.Width, mainRectangle.Top)
            Me.UserMoving = True   'reset the flag after moving is complete
        End If
    End Sub
    ' // End fancy form moving //

    Private Sub chk_rememberMe_CheckedChanged(sender As Object, e As EventArgs) Handles chk_rememberMe.CheckedChanged
        My.Settings.rememberMe = chk_rememberMe.Checked 'save the state of the rememberme
        My.Settings.Save()                              'checkbox in the app's settings (see
        'https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-create-a-new-setting-at-design-time)
    End Sub

    Private Sub frm_main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        frm_conversations.Height = Me.Height 'keep the other form in sync with this one

        ' // move all the controls with the form //
        ' This might need to become a function one day. Could get very big.
        grp_chat.Width = Me.Width - 40
        grp_chat.Height = Me.Height - 60
        txt_message.Width = grp_chat.Width - 95
        btn_send.Left = txt_message.Left + txt_message.Width + 10
        txt_message.Top = grp_chat.Height - txt_message.Height - 10
        btn_send.Top = txt_message.Top
        ' // end move controls //
    End Sub
End Class
