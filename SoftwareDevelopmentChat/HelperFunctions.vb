Module HelperFunctions
    'This module will contain all functions that don't talk to the database. It really doesn't need to exist,
    'but it means that the forms don't have so much crap in them. For example, the main form really doesn't 
    'need the login Function To reside In it, but it could.
    'Basically, this is where everything that doesn't have a 'handles' option goes.

    Public Function login() 'bring the chat window over, change text, etc., & load the conversations form
        Dim i As Integer = frm_main.grp_login.Left                  'so all this is basically
        While frm_main.grp_chat.Left > i + 10                       'just a fancy animation thing
            frm_main.grp_chat.Left = frm_main.grp_chat.Left - 10    'to bring the second groupbox over the top of the first.
            Threading.Thread.Sleep(20) 'animation speed
            frm_main.Refresh() 'this is needed otherwise it just jumps on top instead of sliding
        End While 'Animation is over :)


        frm_main.grp_login.Visible = False 'we don't need this now because we're logged in, so we will hide it.

        'change the title & make it resiseable
        frm_main.Text = "Chat"
        frm_main.FormBorderStyle = FormBorderStyle.Sizable
        frm_main.MaximizeBox = True

        'load the conversations form
        frm_conversations.Show() 'TODO: Something after this point makes the forms 'jump'. Need to investigate. Added as issue #1

        Return True
    End Function

    Function addUser(ByVal username As String, ByVal password As String) 'technically belongs here as it doesn't actually run any server stuff - just calls another function to do it. It should in future encrypt passwords on their way out to the database.
        If Not userExists(username) Then 'check database to see if user exists
            If writeSQL("insert into tbl_users (Name, Password) values ('" & username & "', '" & password & "')") Then 'if insert new user command was successful

                ' // the following will click the 'existing user' radio button and login. There's probably nicer ways of doing this but I can't be bothered to work them out.

                MsgBox("User created successfully.", vbOKOnly, "Success")
                frm_main.rbtn_userExist.PerformClick()
                frm_main.btn_login.PerformClick() 'no need to enter password. Should already be there.

                '// end

            Else
                If MsgBox("Something went horribly wrong and the user wasn't created. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                    MsgBox(errorInfo.ToString) 'show them the details from the public errorinfo exception on databasefunctions.vb
                End If
            End If
        Else
            MsgBox("Username is taken. Please try again.", vbOKOnly & vbExclamation, "Error creating user")
        End If
    End Function

    Public dontshowpassword As Boolean = False

    Function passwordCorrect(ByVal username As String, ByVal password As String) As Boolean 'returns try/false after checking database for password
        Dim result As String = readUserPassword(MakeSQLSafe(username))
        If result = "False" And userExists(MakeSQLSafe(username)) Then 'if the readUserPassword function failed (and thus returned false as a string because that's what its supposed to do)
            If MsgBox("Something went horribly wrong and the password couldn't be verified against the database. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(errorInfo.ToString) 'show them the details from the public errorinfo exception on databasefunctions.vb
            End If
        ElseIf result = "False" And Not userExists(MakeSQLSafe(username)) Then
            MsgBox("Username incorrect. Please try again.", vbOKOnly, "Login failed")
            dontshowpassword = True
        End If
        Try
            If result = frm_main.txt_password.Text Then 'if the password is what has been entered
                Return True
            Else Return False
            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            If MsgBox("Something went horribly wrong and the password couldn't be verified. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
            Return False
        End Try
    End Function

    Function loadStreams() 'load existing streams
        Try
            Dim UserButtons As List(Of Button) = New List(Of Button)
            For Each Control In frm_conversations.Controls
                If TypeOf Control Is Button Then UserButtons.Add(Control)
            Next

            Try
                For Each stream As String In getStreamArr() 'for each stream on the database, make a button for it
                    Dim btn As New Button
                    'btn.Location = New Point(13, 57 + UserButtons.Count * 6)
                    btn.Top = 57 + ((UserButtons.Count - 1) * 47)
                    btn.Left = 13
                    btn.Height = 38
                    btn.Width = 163
                    btn.Name = "btn_" & stream
                    btn.Text = stream
                    frm_conversations.Controls.Add(btn)
                    UserButtons.Add(btn)
                    frm_conversations.StreamButtons = UserButtons.Count + 1
                    AddHandler btn.Click, AddressOf frm_conversations.RecipientHandler
                Next
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
                If MsgBox("Something went horribly wrong and the streams couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                    MsgBox(errorInfo.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
                End If
            End Try
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            If MsgBox("Something went horribly wrong and the streams couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
            Return False
        End Try
    End Function

    Public Function MakeSQLSafe(ByVal sql As String) As String ' Ripped this function from https://stackoverflow.com/questions/725367/how-to-filter-out-some-vulnerability-causing-characters-in-query-string.
        '                                                        There used to be a TODO here but I've decided that it's safe enough for now.

        '// HENRY: If you're confused as to why this is here and what it does, check out https://www.youtube.com/watch?v=_jKylhJtPmI //

        If sql.Contains("'") Then
            sql = sql.Replace("'", "''")
        End If
        If sql.Contains("""") Then
            sql = sql.Replace("""", """""")
        End If

        Return sql
    End Function

    Function loadMessages()
        Try
            Dim Userlabels As List(Of Label) = New List(Of Label)


            Try
                For Each message As String In getMessageArr(MakeSQLSafe(frm_main.grp_chat.Text)) 'for each message on the database, make a label for it
                    'For Each Control In frm_main.grp_chat.Controls
                    ' If TypeOf Control Is Label Then Userlabels.Add(Control)
                    ' Next

                    Dim lbl As New Label
                    lbl.AutoSize = True
                    lbl.Text = message
                    lbl.Left = frm_main.grp_chat.Left + 10
                    If theySentTheMessage(message) Then
                        lbl.BackColor = Color.White
                    Else
                        lbl.BackColor = Color.Blue
                        lbl.ForeColor = Color.White
                    End If
                    lbl.Top = 30 + ((Userlabels.Count - 1) * 15)

                    Try
                        lbl.Name = "lbl" & readMessageID(message)
                    Catch ex As Exception
                        Console.WriteLine(ex.ToString)
                        If MsgBox("Something went horribly wrong and the message IDs couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                            MsgBox(errorInfo.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
                            Exit Function
                        End If
                    End Try
                    'Console.WriteLine(lbl.Name & " TOP: " & lbl.Top & " USERLABELCOUT: " & Userlabels.Count)
                    For Each ctrl In Userlabels
                        Console.Write(ctrl.Name & ", ")
                    Next
                    'Console.WriteLine()
                    frm_main.grp_chat.Controls.Add(lbl)
                    Userlabels.Add(lbl)
                Next
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
                If MsgBox("Something went horribly wrong and the messages couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                    MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
                    Exit Function
                End If
            End Try
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            If MsgBox("Something went horribly wrong and the messages couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
            Return False
        End Try
    End Function



    Function writeMessage(ByVal message As String, ByVal streamName As String, ByVal username As String) 'when you send a message it needs to write to the database


        'the messages table has the following columns:
        'ID (auto incrememnt)
        'StreamID                   <-- Get from streamName
        'FromID                     <-- Get from Username
        'Timestamp                  <-- Get from utc_datetime
        'Message                    <-- Get from message
        'Active                     <-- idk yet not important
        'Read                       <-- idk yet not important

        Dim StreamID As Integer = readStreamID(MakeSQLSafe(streamName))     '|
        Dim fromID As Integer = readUserID(MakeSQLSafe(username))           '|  <-- getting variables as listed above
        Dim timestamp As DateTime = DateTime.Now                            '|

        Console.WriteLine(message)

        Try
            writeSQL("insert into tbl_messages (StreamID, FromID, Timestamp, Message) values ('" & StreamID & "', '" & fromID & "', '" & timestamp & "', '" & MakeSQLSafe(message) & "')")
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Console.WriteLine(ex.ToString)
            If MsgBox("Something went horribly wrong and the message couldn't be sent. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(errorInfo.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
        End Try

        frm_main.txt_message.Text = "" 'reset textbox

        'now run LoadMessages again to refresh the messages in the chat
        loadMessages()

    End Function
End Module
