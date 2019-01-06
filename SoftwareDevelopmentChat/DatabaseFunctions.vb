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
End Module
