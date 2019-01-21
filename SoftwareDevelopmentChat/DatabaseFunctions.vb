Imports System.Data.SqlClient
Module DatabaseFunctions
    'This module will contain all functions that are used to talk to the database, from sending messages to sending login credentials


    'Create ADO.NET objects.
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Public errorInfo As Exception

    Public connectionString As String = "Data Source=120.150.110.21,1433;Network Library=DBMSSOCN;Initial Catalog=SoftDevChat;uid=sa;pwd=sys@dmin" 'this is the string required to connect to the server
    '                                    Data source is the IP address followed by port. Initial catalog is the database name.

    Function writeSQL(ByVal Query As String) As Boolean 'with any luck, all database WRITE functions will call this one at some point.
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = Query

        'Open the connection.
        MyConn.Open()

        Try
            myCmd.ExecuteNonQuery() 'run sql script
            MyConn.Close() 'close connection
            Return True
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex 'make error information publicly available
            Return False
        End Try
    End Function

    Function userExists(ByVal name As String) As Boolean
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select count(*) from tbl_users where convert(varchar, Name) = '" & MakeSQLSafe(name) & "'"

        'Open the connection.
        MyConn.Open()

        If myCmd.ExecuteScalar = 1 Then 'if there is one result returned, then the username already exists in the database.
            MyConn.Close()
            Return True
        Else
            MyConn.Close()
            Return False
        End If
    End Function

    Function streamExists(ByVal users As Array) As Boolean

        Dim sql_partOne As String = "SELECT count(*) FROM (select ' ' + REPLACE(convert(varchar(MAX), replace(convert(varchar(MAX),streamName), ',','')),' ','  ') + ' ' as streamName from tbl_streams) t WHERE"
        Dim sql_partTwo As String = " streamName like '% "
        Dim sql_partThree As String = " %' AND "
        Dim sql_partFour As String = " REPLACE("
        Dim sql_partFive As String = "streamName,' "
        Dim sql_partSix As String = " ','')"
        Dim sql_partSeven As String = ", ' "
        Dim sql_partEight As String = " ','')"
        Dim sql_partNine As String = " = ''"

        Dim builtLikes As String = ""
        Dim builtReplaces1 As String = ""
        Dim builtReplaces2 As String = ""
        Dim builtSql As String = ""
        Dim first As Boolean = True

        For Each user As String In users
            builtLikes += sql_partTwo & user & sql_partThree
            builtReplaces1 += sql_partFour
            If first Then
                builtReplaces2 += sql_partFive & user & sql_partSix
            Else
                builtReplaces2 += sql_partSeven & user & sql_partEight
            End If
            first = False
        Next
        builtLikes += sql_partTwo & "and" & sql_partThree
        builtReplaces1 += sql_partFour
        builtReplaces2 += sql_partSeven & "and" & sql_partEight & sql_partNine

        Console.WriteLine(sql_partOne & builtLikes & builtReplaces1 & builtReplaces2)
        builtSql = sql_partOne & builtLikes & builtReplaces1 & builtReplaces2

        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = builtSql 'set command to check sql1
        'Open the connection.
        MyConn.Open()

        If myCmd.ExecuteScalar > 0 Then 'if more than zero results returned, then the stream already exists in the database.
            MyConn.Close()
            Return True ' exists
        Else
            Return False ' not exist
        End If
    End Function

    Function readUserPassword(ByVal Name As String) As String 'function to read passwords from database.
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select Password from tbl_users where convert(varchar, Name) = '" & MakeSQLSafe(Name) & "'"

        'Open the connection.
        MyConn.Open()

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetString(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            MyConn.Close() 'close connection
            Dim decryptor As New Encryption(Name) 'new instance of encryption module
            result = decryptor.DecryptData(result) 'decript with username key
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            Return "False"
        End Try

        Return result
    End Function

    Function readUserID(ByVal Name As String) As Integer 'function to read IDs from database.
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select ID from tbl_users where convert(varchar, Name) = '" & MakeSQLSafe(Name) & "'"

        'Open the connection.
        MyConn.Open()

        Dim result As Integer = 0 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetInt32(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            Return 0
        End Try

        Return result
    End Function

    Function readUserName(ByVal ID As Integer) As String 'function to read Names from database.
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select Name from tbl_users where ID = '" & ID & "'"

        'Open the connection.
        MyConn.Open()

        Dim result As String = 0 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetString(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            Return 0
        End Try

        Return result
    End Function

    Function getStreamArr()
        Dim streams As New List(Of String)

        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select StreamName from tbl_streams Where streamName like '%" & MakeSQLSafe(frm_main.txt_userName.Text) & "%'" 'select streamname where it includes your name

        'Open the connection.
        MyConn.Open()

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader
            ' Loop through our records, reading "StreamName" and put into array1
            While reader.Read()
                streams.Add(CType(reader("StreamName"), String)) 'add streamname to list as string
            End While

            MyConn.Close() 'close connection
            Return streams.ToArray
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            Return False
        End Try

    End Function

    Function getMessagesArr(ByVal stream As String, Optional amount As Integer = 0, Optional IncludeNames As Boolean = False)
        Dim messages As New List(Of String)

        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand

        Dim sql As String
        If Not amount = 0 Then
            sql = "select Message, Timestamp from (select top " & amount & " Message, Timestamp From tbl_messages where StreamID='" & MakeSQLSafe(readStreamID(stream)) & "' Order By Timestamp DESC) T1 Order by Timestamp"
        Else
            sql = "select Message, FromID from tbl_messages Where StreamID = '" & MakeSQLSafe(readStreamID(stream)) & "'" 'select all messages where it includes the stream ID
        End If

        myCmd.CommandText = sql
        'console.writeline(myCmd.CommandText)

        'Open the connection.
        MyConn.Open()

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader
            ' Loop through our records, reading "Message" and put into array1
            While reader.Read()
                If IncludeNames Then
                    Dim uN As String = readUserName(CType(reader("FromID"), Integer))
                    messages.Add(uN & ": " & CType(reader("Message"), String)) 'add message to list as string
                Else
                    messages.Add(CType(reader("Message"), String)) 'add message to list as string
                End If
            End While

            MyConn.Close() 'close connection
            Return messages.ToArray
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            Return False
        End Try

    End Function

    Function readStreamID(ByVal streamName As String) As Integer 'returns 0 on fail
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select StreamID from tbl_streams where convert(varchar, StreamName) = '" & MakeSQLSafe(streamName) & "'"

        'Open the connection.
        MyConn.Open()

        Dim result As Integer = 0 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetInt32(0) 'get first value of field (because there should only be one record returned as there shouldn't be streams doubleups).
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            Return 0
        End Try

        Return result
    End Function

    Function readStreamIDFromMessageID(ByVal messageID As Integer) As Integer 'returns 0 on fail
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select StreamID from tbl_messages where ID = '" & messageID & "'"

        'Open the connection.
        MyConn.Open()

        Dim result As Integer = 0 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetInt32(0) 'get first value of field (because there should only be one record returned as there shouldn't be streams doubleups).
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            Return 0
        End Try

        Return result
    End Function

    Function readMessageID(ByVal Message As String) As Integer 'returns 0 on fail
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select ID from tbl_messages where convert(varchar(MAX), Message) = '" & MakeSQLSafe(Message) & "'" 'probably a less bandwidth hogging way of doing this but its not 1986 anymore so it doesn't really matter

        'Open the connection.
        MyConn.Open()

        Dim result As Integer = 0 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetInt32(0) 'get first value of field
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            Return 0
        End Try

        Return result
    End Function

    Function senderName(ByVal message As String) As String
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select Name from tbl_users where ID = (select FromID from tbl_messages where convert(varchar(MAX), Message) = '" & MakeSQLSafe(message) & "')"
        'console.writeline(myCmd.CommandText)

        'Open the connection.
        MyConn.Open()

        Dim FromName As String = "" 'who sent the message

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                FromName = reader.GetString(0) 'get first value of field
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            FromName = "" 'fail
            If MsgBox("Something went horribly wrong and the messages couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
        End Try

        Return FromName
    End Function

    Function getLatestMessageInStream(ByVal streamID As Integer) As String
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "Select Message from tbl_messages where streamID=" & streamID & " order by ID ASC"

        'Open the connection.
        MyConn.Open()

        Dim Message As String = ""

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                Message = reader.GetString(0) 'get first value of field, as that's all we want
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            Message = "" 'fail
            If MsgBox("Something went horribly wrong and the messages couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
        End Try

        Return Message
    End Function

    Function readRecipt(ByVal message As String) As Boolean
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select RecipientRead from tbl_messages where convert(varchar(MAX), Message) = '" & MakeSQLSafe(message) & "'"
        'console.writeline(myCmd.CommandText)

        'Open the connection.
        MyConn.Open()

        Dim read As Boolean = False

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                read = reader.GetBoolean(0) 'get first value of field
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            read = False 'fail
            If MsgBox("Something went horribly wrong and the messages couldn't be loaded. View technical details?", vbExclamation + vbYesNo, "Something happened") = MsgBoxResult.Yes Then 'if user wants technical details
                MsgBox(ex.ToString) 'something went wrong that we didn't expect to happen. Display error msg.
            End If
        End Try

        Return read
    End Function

    Function getMessageColor() As Color 'reads BubbleColor field of tbl_streams
        'Create a Connection object.
        Dim MyConn = New SqlConnection(connectionString)

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select BubbleColor from tbl_streams where convert(varchar, StreamName) = '" & frm_main.grp_chat.Text & "'"

        'Open the connection.
        MyConn.Open()

        Dim result As String = "False"

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetString(0) 'get first value of field (because there should only be one record returned as there shouldn't be stream doubleups).
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            errorInfo = ex
            result = "False"
        End Try

        If Not result = "False" Then
            If Not result.Contains("A=") Then
                Return decipherColor(result)
            Else
                Return fixARGB(result)
            End If
        Else
            Return Color.Blue 'fallback to blue default on fail
        End If

    End Function
End Module
