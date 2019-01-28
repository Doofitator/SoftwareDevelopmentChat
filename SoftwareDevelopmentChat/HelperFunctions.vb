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

        frm_main.pbx_settings.Visible = True
        frm_main.tmr_messageChecker.Enabled = True

        Return True
    End Function

    Function addUser(ByVal username As String, ByVal password As String) As Boolean 'returns true on success
        If Not userExists(username) Then 'check database to see if user exists
            If writeSQL("insert into tbl_users (Name, Password) values ('" & username & "', '" & password & "')") Then 'if insert new user command was successful

                ' // the following will click the 'existing user' radio button and login. There's probably nicer ways of doing this but I can't be bothered to work them out.

                MsgBox("User created successfully.", vbOKOnly, "Success")
                frm_main.rbtn_userExist.PerformClick()
                frm_main.btn_login.PerformClick() 'no need to enter password. Should already be there.

                '// end

                Return True
            Else
                If MsgBox("Something went horribly wrong and the user wasn't created. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                    MsgBox(errorInfo.ToString) 'show them the details from the public errorinfo exception on databasefunctions.vb
                End If
                Return False
            End If
        Else
            MsgBox("Username is taken. Please try again.", vbOKOnly & vbExclamation, "Error creating user")
            Return False
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
            'console.writeline(ex.ToString)
            If MsgBox("Something went horribly wrong and the password couldn't be verified. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
            Return False
        End Try
    End Function

    Function loadStreams() 'load existing streams
        Try
            Dim UserButtons As List(Of Button) = New List(Of Button)
            For Each control In frm_conversations.pnl_streams.Controls
                If TypeOf control Is Button Then UserButtons.Add(control)
            Next

            Try
                For Each stream As String In getStreamArr() 'for each stream on the database, make a button for it
                    Dim btn As New Button
                    'btn.Location = New Point(13, 57 + UserButtons.Count * 6)
                    btn.Top = ((UserButtons.Count) * 47)
                    btn.Left = 13
                    btn.Height = 38
                    btn.Width = 163
                    btn.Name = "btn_" & stream
                    btn.Text = stream
                    frm_conversations.pnl_streams.controls.add(btn)
                    UserButtons.Add(btn)
                    frm_conversations.StreamButtons = UserButtons.Count + 1
                    AddHandler btn.Click, AddressOf frm_conversations.RecipientHandler
                Next
            Catch ex As Exception
                'console.writeline(ex.ToString)
                If MsgBox("Something went horribly wrong and the streams couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                    MsgBox(errorInfo.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
                End If
            End Try
            Return True
        Catch ex As Exception
            'console.writeline(ex.ToString)
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

        Return sql
    End Function

    'the following variables is the HTML & css base code for all chat bubbles. It heas breaks where the bubble colours need to be inserted.
    Public html As String = "<html>
    <head>
        <style type=""text/css"">
            .them:before {
                content: '';
                width: 0px;
                height: 0px;
                position: absolute;
                border-left: 10px solid transparent;
                border-right: 10px solid gray;
                border-top: 10px solid gray;
                border-bottom: 10px solid transparent;
                left:-19px;
                top:6px;
            }
            
            .us:before {
                content: '';
                width: 0px;
                height: 0px;
                position: absolute;
                border-left: 10px solid "
    Public html2 = ";
                border-right: 10px solid transparent;
                border-top: 10px solid "
    Public html3 = ";
                border-bottom: 10px solid transparent;
                right: -20px;
                top: 6px;
            }
            
            .us {
                text-align: right;
                background: "
    Public html4 = ";
            }
            
            .them {
                text-align: left;
                background: gray;
                color:white;
                margin-left: 20px;
            }
            
            .chat {
                width: 300px;
                padding: 10px;
                font-family: arial;
                position:relative;
                border-radius:5px;
            }
            
            .read {
                border-bottom: 20px solid lightgrey;
            }
            
            .readName {
                position: relative;
                top: 30px;
                margin-top: -30px;
                color: grey;
            }
        </style>
    </head>
    <body style=""background-color: #f0f0f0"">"

    Private Declare Function GetActiveWindow Lib "user32" Alias "GetActiveWindow" () As IntPtr

    Function loadMessages()
        changeBrowserIEVersion() 'fix rendering issues

        Dim lastMessage As String 'the current message is always plopped into here - once the for each statement is finished, it will contain the last message.

        Try
            Dim UserWebBrowsers As List(Of WebBrowser) = New List(Of WebBrowser)

            Dim messagesArray As Array = getMessagesArr(MakeSQLSafe(frm_main.grp_chat.Text), 25)
            Try
                For Each message As String In messagesArray 'for each message on the database, make a label for it
                    'For Each Control In frm_main.grp_chat.Controls
                    ' If TypeOf Control Is Label Then Userlabels.Add(Control)
                    ' Next

                    Dim wbr As New WebBrowser

                    wbr.Width = frm_main.pnl_messages.Width - 32
                    'console.writeline("'" & message & "' was sent by them: " & theySentTheMessage(message))
                    Dim div As String
                    Dim readDiv As String = ""


                    If theySentTheMessage(message) Then
                        lastMessage = message
                        div = "<div class=""chat them"">"
                    Else
                        If Not readRecipt(message) Then
                            div = "<div class=""chat us"">"
                        Else
                            div = "<div class=""chat us"">"
                            If message = theLastMessageThatWasSentByUsAndIsReadIn(messagesArray) Then readDiv = "<div class=""readName"">Read</div>" : div = "<div class=""chat us read"">"
                        End If
                    End If



                    wbr.Left = 10

                    wbr.DocumentText = html & getMessageColor().ToHtmlHexadecimal & html2 & getMessageColor().ToHtmlHexadecimal & html3 & getMessageColor().ToHtmlHexadecimal & html4 & div & message & readDiv & "</div></body></html>"

                    wbr.Height = 80                         '| TODO: Fix this. 
                    wbr.Top = (UserWebBrowsers.Count * 80)  '| It could be better.
                    wbr.ScrollBarsEnabled = False

                    Try
                        wbr.Name = "wbr_" & theySentTheMessage(message) & "_" & readMessageID(message)
                    Catch ex As Exception
                        'console.writeline(ex.ToString)
                        If MsgBox("Something went horribly wrong and the message IDs couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                            MsgBox(errorInfo.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
                            Exit Function
                        End If
                    End Try
                    'console.writeline(lbl.Name & " TOP: " & lbl.Top & " USERLABELCOUT: " & Userlabels.Count)

                    'console.writeline()
                    frm_main.pnl_messages.Controls.Add(wbr)
                    UserWebBrowsers.Add(wbr)
                Next

            Catch ex As Exception
                'console.writeline(ex.ToString)
                If MsgBox("Something went horribly wrong and the messages couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                    MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
                    Exit Function
                End If
            End Try

            Try 'in case there are no messages in the stream
                Dim lastItem As WebBrowser = UserWebBrowsers(UserWebBrowsers.Count - 1) 'get latest message webbrowser
                frm_main.pnl_messages.ScrollControlIntoView(lastItem) 'scroll to last message
                If GetActiveWindow Then 'if this program is the active, focused window
                    Dim fromID As Integer = readUserID(senderName(lastMessage))
                    Dim messageID As Integer = CInt(lastItem.Name.Replace("wbr_", ""))
                    'from id is the user id of the name of the person who sent the contents of the latest webbrowser without all the html.
                    writeSQL("update tbl_messages set recipientRead = 1 where FromID = " & fromID & " and StreamID = " & readStreamIDFromMessageID(messageID)) 'write read recipt for all messages up to this one in this stream sent by 'them' if frm_main is the active window
                End If
            Catch
                'console.writeline("no messages")
            End Try
            Return True
        Catch ex As Exception
            'console.writeline(ex.ToString)
            If MsgBox("Something went horribly wrong and the messages couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
            Return False
        End Try
    End Function

    Function theLastMessageThatWasSentByUsAndIsReadIn(ByVal Messages As Array) As String 'returns the last message that was sent by 'us' and is read by the other person / persons
        Dim messagesList As New List(Of String)

        For Each message In Messages                'for each message we're given
            If Not theySentTheMessage(message) Then 'if we sent it
                If readRecipt(message) Then         'if it has been read
                    messagesList.Add(message)       'add it to our list
                End If
            End If
        Next

        Return messagesList(messagesList.Count - 1) 'retun the latest message in our list
    End Function

    Function theySentTheMessage(ByVal message As String) As Boolean 'this function works out who sent the message. If it was someone else, it is true, else false.


        'console.writeline(FromName & " vs " & frm_main.txt_userName.Text)
        If senderName(message) = frm_main.txt_userName.Text.ToLower Then
            Return False 'it was us
        Else
            Return True 'it was them
        End If
    End Function

    Function UppercaseFirstLetter(ByVal val As String) As String
        ' Test for nothing or empty.
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        ' Convert to character array.
        Dim array() As Char = val.ToCharArray

        ' Uppercase first character.
        array(0) = Char.ToUpper(array(0))

        ' Return new string.
        Return New String(array)
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
        Dim fromID As Integer = readUserID(MakeSQLSafe(username))           '|  <-- getting variables as listed above                       '|

        'console.writeline(message)

        Try
            writeSQL("insert into tbl_messages (StreamID, FromID, Timestamp, Message) values ('" & StreamID & "', '" & fromID & "', getdate(), '" & SwearFilter(MakeSQLSafe(message)) & "')")
        Catch ex As Exception
            'console.writeline(ex.ToString)
            'console.writeline(ex.ToString)
            If MsgBox("Something went horribly wrong and the message couldn't be sent. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(errorInfo.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
        End Try

        frm_main.txt_message.Text = "" 'reset textbox

        Dim biggestTop As Integer = 0
        Dim lastHeight As Integer = 0
        Dim UserWebBrowsersCount As Integer = 0
        For Each webbrowser In frm_main.pnl_messages.Controls
            If webbrowser.top > biggestTop Then biggestTop = webbrowser.top : lastHeight = webbrowser.height
            UserWebBrowsersCount += 1
        Next

        addMessageAfterTheFact(message, UserWebBrowsersCount, biggestTop, lastHeight)

    End Function

    Function addMessageAfterTheFact(ByVal message As String, ByVal userWebBrowsersCount As Integer, ByVal biggestTop As Integer, ByVal lastHeight As Integer)
        Dim wbr As New WebBrowser
        wbr.Width = frm_main.pnl_messages.Width - 32
        'console.writeline("'" & message & "' was sent by them: " & theySentTheMessage(message))
        Dim div As String
        Dim readDiv As String

        If theySentTheMessage(message) Then
            div = "<div class=""chat them"">"
        Else
            If Not readRecipt(message) Then
                div = "<div class=""chat us"">"
            Else
                div = "<div class=""chat us"">"
                If message = theLastMessageThatWasSentByUsAndIsReadIn(getMessagesArr(MakeSQLSafe(frm_main.grp_chat.Text), 25)) Then readDiv = "<div class=""readName"">Read</div>" : div = "<div class=""chat us read"">"
            End If
        End If
        wbr.Left = 10

        wbr.DocumentText = html & getMessageColor().ToHtmlHexadecimal & html2 & getMessageColor().ToHtmlHexadecimal & html3 & getMessageColor().ToHtmlHexadecimal & html4 & div & message & readDiv & "</div></body></html>"

        wbr.Height = 80
        wbr.Top = biggestTop + lastHeight + 10
        wbr.BringToFront()

        wbr.ScrollBarsEnabled = False
        Try
            wbr.Name = "wbr_" & theySentTheMessage(message) & "_" & readMessageID(message)
        Catch ex As Exception
            'console.writeline(ex.ToString)
            If MsgBox("Something went horribly wrong and the message IDs couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(errorInfo.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
                Exit Function
            End If
        End Try
        'console.writeline(lbl.Name & " TOP: " & lbl.Top & " USERLABELCOUT: " & Userlabels.Count)
        'console.writeline()
        frm_main.pnl_messages.Controls.Add(wbr)

        frm_main.pnl_messages.ScrollControlIntoView(wbr) 'scroll to last message
    End Function

    Function writeColor(ByVal colorToWrite As Color, ByVal stream As String)
        writeSQL("update tbl_streams set BubbleColor = '" & MakeSQLSafe(colorToWrite.ToString) & "' where convert(varchar, StreamName) = '" & MakeSQLSafe(stream) & "'")
    End Function

    Function decipherColor(ByVal undecipheredColor As String) As Color                      '|
        Dim deciphered As String = undecipheredColor.Split(New Char() {"[", "]"})(1)        '|
        decipherColor = System.Drawing.Color.FromName(deciphered)                           '|
    End Function                                                                            '|

    Function fixARGB(ByVal brokenARGB As String) As Color                                   '|
        Dim halfwayThere As String = brokenARGB.Split(New Char() {"[", "]"})(1)             '|
        Dim noA As String = Replace(halfwayThere, "A=", "")                                 '| Legit couldn't tell you how these work
        Dim noR As String = Replace(noA, "R=", "")                                          '| Ripped them off an old project & all I
        Dim noG As String = Replace(noR, "G=", "")                                          '| know is that they do work so eh
        Dim clean As String = Replace(noG, "B=", "")                                        '|
        Dim sept As String() = clean.Split(New Char() {","c})                               '|
        fixARGB = Color.FromArgb(sept(0), sept(1), sept(2), sept(3))                        '|
    End Function

    Function getGroupChatNames() As String 'returns "False" on error
        Dim newFrm_groupChatSelector As New frm_groupChatSelector
        Dim result As DialogResult = newFrm_groupChatSelector.ShowDialog
        If Not result = DialogResult.OK Then
            Return "False"
        Else
            Dim usernames As New List(Of String)

            For Each control In newFrm_groupChatSelector.Controls       'for each control on form
                If TypeOf (control) Is TextBox Then                     'if control is a textbox
                    Dim text As TextBox = CType(control, TextBox)       'convert control to a textbox (legit just for intellisense)
                    usernames.Add(UppercaseFirstLetter(text.Text))      'add the text to our list
                End If
            Next

            Dim resultString As String = ""

            For i As Integer = 0 To usernames.Count - 1
                resultString += usernames(i) & ","
            Next

            resultString = resultString.Trim().Substring(0, resultString.Length - 1)

            Return resultString
        End If
    End Function

    '_swear is a list of the swear words
    Private _swearArray As String() = {"shit", "shit", "ass", "etc"}

    Public Function SwearFilter(ByVal textBoxInput As String) As String
        Dim cleanedText As String = ""
        'doesn't catch first word for some reason, so just create 3 temp chars and remove them at the end
        textBoxInput = "xx " + textBoxInput
        textBoxInput = textBoxInput.ToLower
        For i As Integer = 0 To _swearArray.Length - 1
            textBoxInput = textBoxInput.Replace(_swearArray(i), " #")
        Next
        Return textBoxInput.Substring(3)
    End Function


End Module
