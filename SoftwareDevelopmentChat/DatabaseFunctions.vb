Imports System.Data.SqlClient
Module DatabaseFunctions
    'This module will contain all functions that are used to talk to the database, from sending messages to sending login credentials


    'Create ADO.NET objects.
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Function writeNewUser(ByVal username As String, ByVal password As String)
        'Create a Connection object.
        myConn = New SqlConnection("Data Source=120.150.110.21,1433;Network Library=DBMSSOCN;Initial Catalog=SoftDevChat;uid=sa;pwd=sys@dmin")


        'Create a Command object.
        myCmd = myConn.CreateCommand
        myCmd.CommandText = "INSERT INTO tbl_users (Name, Password) VALUES (" & username & ", " & password & ")"

        'Open the connection.
        myConn.Open()

        myCmd.ExecuteNonQuery()
    End Function
End Module
