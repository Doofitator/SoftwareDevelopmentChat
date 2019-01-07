Imports System.ComponentModel

Public Class frm_conversations
    Private Sub frm_conversations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frm_main.UserMoving = False
        Me.Left = frm_main.Left - Me.Width
        Me.Height = frm_main.Height
        Me.Top = frm_main.Top
        frm_main.UserMoving = True
    End Sub

    ' // The following is for moving the main form with this one, adapted from http://www.vbforums.com/showthread.php?611932-snap-or-dock-forms-together#2 //

    Private Sub frm_main_move(sender As Object, e As EventArgs) Handles Me.Move
        If frm_main.UserMoving Then
            Dim mainRectangle = Me.Bounds
            frm_main.UserMoving = False   'tell the docked form that its the 'computer' moving the form, not the user
            frm_main.Location = New Point(mainRectangle.Right, mainRectangle.Top)
            frm_main.UserMoving = True   'reset the flag after moving is complete
        End If
    End Sub

    ' // End fancy form moving //

    Private Sub frm_conversations_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End 'this just means that now, no matter what exit button you click, the entire program shuts down
    End Sub

    Private Sub btn_newMessage_Click(sender As Object, e As EventArgs) Handles btn_newMessage.Click

        Dim recipient As String = InputBox("Type the recipient's username:")
        Try
            Dim recipientID As String = readUserID(recipient)
        Catch
            If MsgBox("Something went horribly wrong and the stream couldn't be created. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(errorInfo.ToString) 'show them the details from the public errorinfo exception on databasefunctions.vb
            End If
            Exit Sub
        End Try

        Dim StreamNameString As String = recipient & " and " & frm_main.txt_userName.Text

        Dim sql As String = "insert into tbl_stream (StreamName) values ('" & MakeSQLSafe(StreamNameString) & "')"

        Dim btn As Button = New Button
        btn.Location = New Point(20, 20)
        btn.Name = "btn" & StreamNameString
        btn.Text = StreamNameString
        Me.Controls.Add(btn)
        AddHandler btn.Click, AddressOf StreamNameStringHandle

    End Sub
End Class