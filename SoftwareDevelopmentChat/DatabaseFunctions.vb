Imports System.Data.SqlClient
Module DatabaseFunctions
    'This module will contain all functions that are used to talk to the database, from sending messages to sending login credentials


    'Create ADO.NET objects.
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Function runSQL(ByVal Query As String)
        'Create a Connection object.
        myConn = New SqlConnection("Data Source=120.150.110.21,1433;Network Library=DBMSSOCN;Initial Catalog=SoftDevChat;uid=sa;pwd=sys@dmin")
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
