﻿Public Class frm_main
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
        ElseIf control Is rbtn_userNew Then             'all this is just radio button stuff for the new user / existing user
            lbl_passwordRepeat.Visible = True
            txt_passwordRepeat.Visible = True
        End If
    End Sub

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        If rbtn_userExist.Checked Then 'if it's an existing user
            'check password & user against database
            MsgBox("Username or password incorrect. Please try again.", vbOKOnly, "Login failed") 'no database yet so login failed
            Exit Sub 'this ends the sub after the failed login, obviously, once there's a database, this will be inside an if statement and will only run if the password is incorrect.

            'happy with password? Then are we supposed to remember you?
            If chk_rememberMe.Checked Then
                My.Settings.userName = txt_userName.Text
                My.Settings.userPassword = txt_password.Text
                My.Settings.Save()
            End If

            'Okay. That's all sorted, now login!
            login()
        ElseIf rbtn_userNew.Checked Then 'if it's a new user

        End If
    End Sub

    Function login() 'bring the chat window over, change text, etc., & load the conversations form
        Dim i As Integer = grp_login.Left           'so all this is basically
        While grp_chat.Left > i + 10                'just a fancy animation thing
            grp_chat.Left = grp_chat.Left - 10      'to bring the second groupbox over the top of the first.
            Threading.Thread.Sleep(20) 'animation speed
            Me.Refresh() 'this is needed otherwise it just jumps on top instead of sliding
        End While                                   'Animation is over :)


        grp_login.Visible = False 'we don't need this now because we're logged in, so we will hide it.

        'change the title & make it resiseable
        Me.Text = "Chat"
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.MaximizeBox = True

        'load the conversations form
        frm_conversations.Show() 'TODO: Something after this point makes the forms 'jump'. Need to investigate. Added as issue #1

        Return True
    End Function

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
End Class
