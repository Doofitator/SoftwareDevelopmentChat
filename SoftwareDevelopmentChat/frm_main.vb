﻿Public Class frm_main

    ' // Image background credit https://www.y2architecture.com.au/marist-college-bendigo-champagnat/

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

        For Each control In grp_chat.Controls
            control.enabled = False 'disable all the chat controls so they dont interfere with tab jumping
        Next

        tabIndexFixer(True)
    End Sub

    Function tabIndexFixer(ByVal isExistingUser As Boolean) 'probably really should be on helperfunctions module but it is something that only this form will access so nvm
        If isExistingUser Then
            rbtn_userExist.TabIndex = 1
            rbtn_userNew.TabIndex = 2
            txt_userName.TabIndex = 3
            txt_password.TabIndex = 4
            chk_rememberMe.TabIndex = 5
            btn_login.TabIndex = 6
            txt_passwordRepeat.TabIndex = 0
        Else                                    'almost - interference from the other groupbox, & the two radio buttons are treated as the same control. Will fix in a tic.
            rbtn_userExist.TabIndex = 1
            rbtn_userNew.TabIndex = 2
            txt_userName.TabIndex = 3
            txt_password.TabIndex = 4
            txt_passwordRepeat.TabIndex = 5
            chk_rememberMe.TabIndex = 6
            btn_login.TabIndex = 7
        End If
    End Function

    Private Sub rbtn_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_userNew.CheckedChanged, rbtn_userExist.CheckedChanged
        Dim control As RadioButton = CType(sender, RadioButton)
        If control Is rbtn_userExist Then
            lbl_passwordRepeat.Visible = False
            txt_passwordRepeat.Visible = False
            lbl_passwordRepeat.Enabled = False
            txt_passwordRepeat.Enabled = False
            btn_login.Text = "Login"
            tabIndexFixer(True)
        ElseIf control Is rbtn_userNew Then             'all this is just radio button stuff for the new user / existing user
            lbl_passwordRepeat.Visible = True
            txt_passwordRepeat.Visible = True
            lbl_passwordRepeat.Enabled = True
            txt_passwordRepeat.Enabled = True
            btn_login.Text = "Sign Up"
            tabIndexFixer(False)
        End If
    End Sub

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Dim userName As String = txt_userName.Text              'get username
        Dim PlainTextPassword As String = txt_password.Text     'get password

        '// fancy encryption stuff from encryption.vb //

        Dim password As String  'new string to hold encrypted password

        Dim wrapper As Encryption = New Encryption(userName)             'make a new encrypted string with the key of username
        password = wrapper.EncryptData(PlainTextPassword)   'encrypt the password with the key of the username

        '// end //

        If rbtn_userExist.Checked Then 'if it's an existing user
            'check password & user against database

            If passwordCorrect(MakeSQLSafe(userName), MakeSQLSafe(password)) Then
                GoTo correctPassword
            Else
                'MsgBox("Username ('" & userName & "') or password ('" & PlainTextPassword & "') and thus encrypted password ('" & password & "') incorrect. Please try again.", vbOKOnly, "Login failed") 'for debugging
                If Not dontshowpassword Then MsgBox("Password incorrect. Please try again.", vbOKOnly, "Login failed") Else dontshowpassword = False
                Exit Sub
            End If

correctPassword:

            'happy with password? Then are we supposed to remember you?
            If chk_rememberMe.Checked Then
                My.Settings.userName = userName
                My.Settings.userPassword = password
                My.Settings.Save()
            End If

            'Okay. That's all sorted, now login!
            login()
        ElseIf rbtn_userNew.Checked Then 'if it's a new user
            If txt_passwordRepeat.Text = txt_password.Text Then 'if both password fields match
                addUser(MakeSQLSafe(userName), MakeSQLSafe(password)) 'function on helperfunctions.vb
            Else
                MsgBox("Passwords do not match. Please try again.", vbOKOnly & vbExclamation, "Error creating user")
            End If
        End If

        For Each control In grp_login.Controls
            control.enabled = False 'diable all the login controls so they dont interfere with tab jumping
        Next
        For Each control In grp_chat.Controls
            control.enabled = True 'enable all the chat controls
        Next
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

    Public Sub btn_send_Click(sender As Object, e As EventArgs) Handles btn_send.Click
        writeMessage(txt_message.Text, grp_chat.Text, txt_userName.Text)
    End Sub

    Public Sub doSomeStuffingAbout()
        Me.btn_send = New System.Windows.Forms.Button()                    ' // from here to END is a whole heap of stuffing about because looping through the labels 
        Me.txt_message = New System.Windows.Forms.TextBox()                ' // and deleting them when you change chats didn't work, so I had to clear every control and rebuild the important ones.

        '
        'btn_send
        '
        Me.btn_send.Location = New System.Drawing.Point(300, 408)
        Me.btn_send.Name = "btn_send"
        Me.btn_send.Size = New System.Drawing.Size(75, 23)
        Me.btn_send.TabIndex = 1
        Me.btn_send.Text = "Send"
        Me.btn_send.UseVisualStyleBackColor = True
        '
        'txt_message
        '
        Me.txt_message.Location = New System.Drawing.Point(7, 410)
        Me.txt_message.Name = "txt_message"
        Me.txt_message.Size = New System.Drawing.Size(287, 20)
        Me.txt_message.TabIndex = 0

        Me.grp_chat.Controls.Add(Me.btn_send)
        Me.grp_chat.Controls.Add(Me.txt_message)
    End Sub

    Private Sub pbx_settings_Click(sender As Object, e As EventArgs) Handles pbx_settings.Click
        If Not grp_chat.Text = "" Then 'basically if there's a chat open
            frm_settings.ShowDialog() 'show settings
        End If
    End Sub
End Class
