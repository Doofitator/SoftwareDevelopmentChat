Module DatabaseFunctions
    'This module will contain all functions that are used to talk to the database, from sending messages to sending login credentials

    Public DatabaseConnection = New OleDb.OleDbConnection
    Public newMedia As System.IO.FileInfo = Nothing

    Function connect()
        DatabaseConnection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=http://marist.vic.edu.au/showcase/vce_soft_dev/chat/SoftDevChat.accdb"
        DatabaseConnection.Open()
    End Function
End Module
