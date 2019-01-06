Imports System.Data.SqlClient
Module DatabaseFunctions
    'This module will contain all functions that are used to talk to the database, from sending messages to sending login credentials


    'Create ADO.NET objects.
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Public connectionString As String = "Data Source=120.150.110.21,1433;Network Library=DBMSSOCN;Initial Catalog=SoftDevChat;uid=sa;pwd=sys@dmin" 'this is the string required to connect to the server

    Function runSQL(ByVal Query As String) 'with any luck, all other functions on this module will call this one at some point.
        'Create a Connection object.
        myConn = New SqlConnection(connectionString)
        '                           Data source is the IP address followed by port. Initial catalog is the database name.

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
        '                                                        TODO: Need to make it more efficient and handle illegal characters better (not remove them, but make them safe).

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

    Function addUser(ByVal username As String, ByVal password As String)
        runSQL("insert into tbl_users (Name, Password) values ('" & username & "' '" & password & "')")
        ' // SQL script makes a new record in tbl_users with the corresponding values for username and password.
    End Function
End Module
