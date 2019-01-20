Imports System.ComponentModel

Public Class frm_conversations
    Private Sub frm_conversations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frm_main.UserMoving = False
        Me.Left = frm_main.Left - Me.Width
        Me.Height = frm_main.Height
        Me.Top = frm_main.Top
        frm_main.UserMoving = True
        loadStreams()
        cbx_class.Items.Add("Add New...")
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
        frm_main.Close() 'this just means that now, no matter what exit button you click, the entire program shuts down
    End Sub

    Public StreamButtons As Integer = 1

    Private Sub btn_newMessage_Click(sender As Object, e As EventArgs) Handles btn_newMessage.Click

        'todo: https://github.com/Doofitator/SoftwareDevelopmentChat/issues/4

        If MsgBox("Create group chat?", vbYesNo, "Chat type") = MsgBoxResult.Yes Then


            'TODO: This is really dodgy.
            'For starters, say there's an existing chat between Ash and Henry, and we make a group chat between Ash, Henry and Lachie, the system thinks the chat already exists because one like it exists (line 69 as of writing)
            'next, you can make a group chat with yourself. There's nothing on the group chat maker form to stop you from typing your own name in.
            'on the same note, there's nothing stopping you typing someone else's name twice.
            'finally, when the stream button is created, there are no spaces between commas and names. There is also the potential for the button to run out of text space if the names are too long.


            Dim recipients As String = getGroupChatNames()
            'MsgBox(recipients) 'debugging
            Dim recipientString As String() = recipients.Split(New Char() {","c})
            Dim recipientsNumber As Integer = recipientString.Count
            Try
                'Dim recipientIDs As List(Of Integer)
                For Each recipient In recipientString
                    Dim recipientID As Integer = readUserID(recipient)
                Next
            Catch ex As Exception
                If MsgBox("Something went horribly wrong and the user(s) could not be found. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                    MsgBox(errorInfo.ToString) 'show them the details from the public errorinfo exception on databasefunctions.vb
                End If
            End Try

            Dim StreamNameString As String = recipients & " and " & frm_main.txt_userName.Text
            Dim sql As String = "insert into tbl_streams (StreamName) values ('" & StreamNameString & "')"
            Dim UserButtons As List(Of Button) = New List(Of Button)

            For Each recipient In recipientString
                If userExists(recipient) Then
                    'TODO: Fix the following function - streamExists needs to be written better. I'll work on that today (21/1/19).
                    If streamExists(recipients, frm_main.txt_userName.Text) Then MsgBox("Conversation already exists", vbOKOnly, "Error creating stream") : Exit Sub 'check if stream already exists & cancel if it does
                    If writeSQL(sql) Then
                        MsgBox("Conversation created successfully.", vbOKOnly, "Success")
                        Dim btn As New Button
                        'btn.Location = New Point(13, 57 + UserButtons.Count * 6)
                        btn.Top = 84 + ((UserButtons.Count - 1) * 47)
                        btn.Left = 13
                        btn.Height = 38
                        btn.Width = 163
                        btn.Name = "btn_" & StreamNameString
                        btn.Text = StreamNameString
                        Me.Controls.Add(btn)
                        UserButtons.Add(btn)
                        StreamButtons = UserButtons.Count + 1
                        AddHandler btn.Click, AddressOf RecipientHandler
                    Else
                        If MsgBox("Something went horribly wrong and the database couldn't be written to. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                            MsgBox(errorInfo.ToString)
                        End If
                    End If
                Else
                    MsgBox("Chat creation cancelled.")
                End If
            Next


        Else
            Dim recipient As String = UppercaseFirstLetter(InputBox("Type the recipient's username:").ToLower)
            Try
                Dim recipientID As Integer = readUserID(recipient)
            Catch
                If MsgBox("Something went horribly wrong and the user could not be found. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                    MsgBox(errorInfo.ToString) 'show them the details from the public errorinfo exception on databasefunctions.vb
                End If
            End Try

            Dim StreamNameString As String = recipient & " and " & frm_main.txt_userName.Text

            Dim sql As String = "insert into tbl_streams (StreamName) values ('" & StreamNameString & "')"
            'Dim UserButtons As List(Of Button) = New List(Of Button)

            Dim UserButtons As List(Of Button) = New List(Of Button)
            For Each Control In Me.Controls
                If TypeOf Control Is Button Then UserButtons.Add(Control)
            Next



            If userExists(recipient) Then
                If streamExists(recipient, frm_main.txt_userName.Text) Then MsgBox("Conversation already exists", vbOKOnly, "Error creating stream") : Exit Sub 'check if stream already exists & cancel if it does
                If writeSQL(sql) Then
                    MsgBox("Conversation created successfully.", vbOKOnly, "Success")
                    Dim btn As New Button
                    'btn.Location = New Point(13, 57 + UserButtons.Count * 6)
                    btn.Top = 84 + ((UserButtons.Count - 1) * 47)
                    btn.Left = 13
                    btn.Height = 38
                    btn.Width = 163
                    btn.Name = "btn_" & StreamNameString
                    btn.Text = StreamNameString
                    Me.Controls.Add(btn)
                    UserButtons.Add(btn)
                    StreamButtons = UserButtons.Count + 1
                    AddHandler btn.Click, AddressOf RecipientHandler
                Else
                    If MsgBox("Something went horribly wrong and the database couldn't be written to. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                        MsgBox(errorInfo.ToString)
                    End If
                End If
            Else
                MsgBox("This user doesn't exist" & vbNewLine & "Please check the spelling and try again")
            End If
        End If

    End Sub

    Public Shared Sub RecipientHandler(sender As Object, e As EventArgs)
        frm_main.pnl_messages.Controls.Clear()


        Dim btn As Button = CType(sender, Button)                           'get the button that was clicked
        Dim streamName As String = btn.Name.Replace("btn_", "")             'get the stream name from the button
        Dim streamID As Integer = readStreamID(MakeSQLSafe(streamName))     'get the stream ID from the name
        'console.writeline(streamName & " - " & streamID) 'for debugging
        frm_main.grp_chat.Text = streamName                                 'write the stream name on the top of the chat window for a) the user and b) the send button's code and c) the chat window to load the right messages

        loadMessages()

        For Each control In frm_main.grp_chat.Controls
            control.enabled = True 'enable all the chat controls
        Next
    End Sub

    Private Sub cbx_class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_class.SelectedIndexChanged
        'this is for selecting classes - you can have two chats with the same person in different classes. Something Mr. V and I talked about.
        'there would also be a global for each class, including everyone who is in it as a group chat.

        If cbx_class.SelectedItem = "Add New..." Then
            'ask if adding new or joining existing
            'if adding new, generate class code
            'if joinging existing, type in class code
        End If
    End Sub
End Class