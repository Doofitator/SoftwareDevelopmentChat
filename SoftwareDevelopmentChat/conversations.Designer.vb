<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_conversations
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btn_newMessage = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_newMessage
        '
        Me.btn_newMessage.Location = New System.Drawing.Point(13, 13)
        Me.btn_newMessage.Name = "btn_newMessage"
        Me.btn_newMessage.Size = New System.Drawing.Size(163, 38)
        Me.btn_newMessage.TabIndex = 0
        Me.btn_newMessage.Text = "New conversation"
        Me.btn_newMessage.UseVisualStyleBackColor = True
        '
        'frm_conversations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(188, 589)
        Me.Controls.Add(Me.btn_newMessage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frm_conversations"
        Me.Text = "Conversations"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btn_newMessage As Button
End Class
