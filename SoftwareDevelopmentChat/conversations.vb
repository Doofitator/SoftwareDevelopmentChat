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
    Dim StreamButtons As Integer = 1
    Private Sub btn_newMessage_Click(sender As Object, e As EventArgs) Handles btn_newMessage.Click

        Dim recipient As String = InputBox("Pick a user you would like to chat with.")
        Try
            Dim recipientID As String = readUserID(recipient)
        Catch
            If MsgBox("Something went horribly wrong and the user could not be found. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(errorInfo.ToString) 'show them the details from the public errorinfo exception on databasefunctions.vb
            End If
        End Try

        Dim StreamNameString As String = recipient & " and " & frm_main.txt_userName.Text

        Dim sql As String = "insert into tbl_streams (StreamName) values ('" & StreamNameString & "')"
        'Dim UserButtons As List(Of Button) = New List(Of Button)
        Dim UserButtons As List(Of Button) = New List(Of Button)


        If userExists(recipient) Then
            If writeSQL(sql) Then
                MsgBox("Conversation created successfully.", vbOKOnly, "Success")
                Dim btn As New Button
                'btn.Location = New Point(13, 57 + UserButtons.Count * 6)
                btn.Top = 57 + (UserButtons.Count * 6)
                btn.Left = 13
                btn.Height = 38
                btn.Width = 163
                btn.Name = "btn" & StreamNameString
                btn.Text = StreamNameString
                Me.Controls.Add(btn)
                UserButtons.Add(btn)
                StreamButtons = UserButtons.Count + 1
                AddHandler btn.Click, AddressOf RecipientHandler
            Else
                MsgBox("An error has occured.", vbOKOnly, "Failure")
                MsgBox(errorInfo.ToString)
            End If
        Else
            MsgBox("This user doesn't exist" & vbNewLine & "Please check the spelling and try again")
        End If

    End Sub

    Sub RecipientHandler()
        ' Do stuff
    End Sub
End Class