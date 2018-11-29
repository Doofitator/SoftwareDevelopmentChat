Public Class frm_main
    Private Sub frm_main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 421 'set the width to half what it is in the design editor (it's only double there so we can see both windows)
        If Me.Left < frm_conversations.Width + 50 Then 'make sure that the form loads far enough to the right that the conversations form fits next to it
            Me.UserMoving = False 'so that the other form doesn't move with this one
            Me.Left = frm_conversations.Width + 50
            Me.UserMoving = True 'back to normal
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
        'check password & user against database
        MsgBox("Username or password incorrect. Please try again.", vbOKOnly, "Login failed") 'no database yet so login failed
        Exit Sub 'this ends the sub after the failed login, obviously, once there's a database, this will be inside an if statement and will only run if the password is incorrect.

        'happy with password? Then login!
        login()
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
End Class
