﻿Imports System.Data.SqlClient
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
        myConn = New SqlConnection(connectionString)

        'Create a Command object.
        myCmd = myConn.CreateCommand
        myCmd.CommandText = Query

        'Open the connection.
        myConn.Open()

        Try
            myCmd.ExecuteNonQuery() 'run sql script
            myConn.Close() 'close connection
            Return True
        Catch ex As Exception 'if a catastrophic error occurs
            myConn.Close() 'close the connection
            errorInfo = ex 'make error information publicly available
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

    Function userExists(ByVal name As String) As Boolean
        'Create a Connection object.
        myConn = New SqlConnection(connectionString)

        'Create a Command object.
        myCmd = myConn.CreateCommand
        myCmd.CommandText = "select count(*) from tbl_users where convert(varchar, Name) = '" & name & "'"

        'Open the connection.
        myConn.Open()

        If myCmd.ExecuteScalar = 1 Then 'if there is one result returned, then the username already exists in the database.
            myConn.Close()
            Return True
        Else
            myConn.Close()
            Return False
        End If
    End Function

    Function readUserPassword(ByVal Name As String) As String 'function to read passwords from database. This function should in future decrypt passwords too.
        'Create a Connection object.
        myConn = New SqlConnection(connectionString)

        'Create a Command object.
        myCmd = myConn.CreateCommand
        myCmd.CommandText = "select Password from tbl_users where convert(varchar, Name) = '" & Name & "'"

        'Open the connection.
        myConn.Open()

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetString(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            myConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            myConn.Close() 'close the connection
            errorInfo = ex
            Return "False"
        End Try

        Return result
    End Function
End Module
