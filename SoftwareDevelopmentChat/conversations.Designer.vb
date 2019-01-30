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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_conversations))
        Me.btn_newMessage = New System.Windows.Forms.Button()
        Me.cbx_class = New System.Windows.Forms.ComboBox()
        Me.pnl_streams = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'btn_newMessage
        '
        Me.btn_newMessage.Location = New System.Drawing.Point(13, 40)
        Me.btn_newMessage.Name = "btn_newMessage"
        Me.btn_newMessage.Size = New System.Drawing.Size(163, 38)
        Me.btn_newMessage.TabIndex = 0
        Me.btn_newMessage.Text = "New conversation"
        Me.btn_newMessage.UseVisualStyleBackColor = True
        '
        'cbx_class
        '
        Me.cbx_class.FormattingEnabled = True
        Me.cbx_class.Location = New System.Drawing.Point(13, 13)
        Me.cbx_class.Name = "cbx_class"
        Me.cbx_class.Size = New System.Drawing.Size(163, 21)
        Me.cbx_class.TabIndex = 1
        Me.cbx_class.Text = "[Not Implemented]"
        '
        'pnl_streams
        '
        Me.pnl_streams.AutoSize = True
        Me.pnl_streams.Location = New System.Drawing.Point(0, 84)
        Me.pnl_streams.Name = "pnl_streams"
        Me.pnl_streams.Size = New System.Drawing.Size(187, 373)
        Me.pnl_streams.TabIndex = 2
        '
        'frm_conversations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(188, 461)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnl_streams)
        Me.Controls.Add(Me.cbx_class)
        Me.Controls.Add(Me.btn_newMessage)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_conversations"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_newMessage As Button
    Friend WithEvents cbx_class As ComboBox
    Friend WithEvents pnl_streams As Panel
End Class
