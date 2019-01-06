Imports System.Data.SqlClient
Module DatabaseFunctions
    'This module will contain all functions that are used to talk to the database, from sending messages to sending login credentials


    'Create ADO.NET objects.
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Public connectionString As String = "Data Source=120.150.110.21,1433;Network Library=DBMSSOCN;Initial Catalog=SoftDevChat;uid=sa;pwd=sys@dmin" 'this is the string required to connect to the server
    '                                    Data source is the IP address followed by port. Initial catalog is the database name.

    Function writeSQL(ByVal Query As String) 'with any luck, all database WRITE functions will call this one at some point.
        'Create a Connection object.
        myConn = New SqlConnection(connectionString)

        'Create a Command object.
        myCmd = myConn.CreateCommand
        myCmd.CommandText = Query

        'Open the connection.
        myConn.Open()

        Try
            myCmd.ExecuteNonQuery()
            myConn.Close()
        Catch ex As Exception
            myConn.Close()
            MsgBox("E: " & ex.ToString)
        End Try
    End Function

    Public Function MakeSQLSafe(ByVal sql As String) As String ' Ripped this function from https://stackoverflow.com/questions/725367/how-to-filter-out-some-vulnerability-causing-characters-in-query-string.
        '                                                        TODO: Need to make it more efficient and handle illegal characters better (not remove them, but make them safe) to avoid people's passwords 
        '                                                              changing here without them knowing & then Not being able to get back in.

        '// HENRY: If you're confused as to why this is here and what it does, check out https://www.youtube.com/watch?v=_jKylhJtPmI //

        Dim strIllegalChars As String = "/?-^%{}[];$=*`#|&@\<>()+,\"
        If sql.Contains("'") Then
            sql = sql.Replace("'", "''")
        End If
        If sql.Contains("""") Then
            sql = sql.Replace("""", """""")
        End If
        For Each c As Char In strIllegalChars
            If sql.Contains(c.ToString) Then
                sql = sql.Replace(c.ToString, "")
            End If
        Next

        Return sql
    End Function

    Function readUserPassword(ByVal Name As String) As String
        'Create a Connection object.
        myConn = New SqlConnection(connectionString)

        'Create a Command object.
        myCmd = myConn.CreateCommand
        myCmd.CommandText = "select Password from tbl_users where convert(varchar, Name) = '" & Name & "'" 'TODO: Currently there's nothing stopping someone from having the same username as someone else. Need to fix this, or this function will crash.

        'Open the connection.
        myConn.Open()

        Dim result As String = False 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader()
            While reader.Read
                result = reader.GetString(0)
            End While
            myConn.Close()
        Catch ex As Exception
            myConn.Close()
            MsgBox("E: " & ex.ToString)
            Return False
        End Try

        Return result
    End Function
End Module
