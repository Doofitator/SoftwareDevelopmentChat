<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_settings
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
        Me.dlog_color = New System.Windows.Forms.ColorDialog()
        Me.grp_chatSettings = New System.Windows.Forms.GroupBox()
        Me.btn_color = New System.Windows.Forms.Button()
        Me.grp_appSettings = New System.Windows.Forms.GroupBox()
        Me.btn_logout = New System.Windows.Forms.Button()
        Me.grp_chatSettings.SuspendLayout()
        Me.grp_appSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'dlog_color
        '
        Me.dlog_color.AnyColor = True
        Me.dlog_color.Color = System.Drawing.Color.Blue
        Me.dlog_color.FullOpen = True
        '
        'grp_chatSettings
        '
        Me.grp_chatSettings.Controls.Add(Me.btn_color)
        Me.grp_chatSettings.Location = New System.Drawing.Point(13, 13)
        Me.grp_chatSettings.Name = "grp_chatSettings"
        Me.grp_chatSettings.Size = New System.Drawing.Size(413, 82)
        Me.grp_chatSettings.TabIndex = 0
        Me.grp_chatSettings.TabStop = False
        Me.grp_chatSettings.Text = "Chat Settings"
        '
        'btn_color
        '
        Me.btn_color.Location = New System.Drawing.Point(7, 20)
        Me.btn_color.Name = "btn_color"
        Me.btn_color.Size = New System.Drawing.Size(75, 23)
        Me.btn_color.TabIndex = 0
        Me.btn_color.Text = "Chat color"
        Me.btn_color.UseVisualStyleBackColor = True
        '
        'grp_appSettings
        '
        Me.grp_appSettings.Controls.Add(Me.btn_logout)
        Me.grp_appSettings.Location = New System.Drawing.Point(13, 101)
        Me.grp_appSettings.Name = "grp_appSettings"
        Me.grp_appSettings.Size = New System.Drawing.Size(413, 82)
        Me.grp_appSettings.TabIndex = 1
        Me.grp_appSettings.TabStop = False
        Me.grp_appSettings.Text = "App Settings"
        '
        'btn_logout
        '
        Me.btn_logout.Enabled = False
        Me.btn_logout.Location = New System.Drawing.Point(332, 53)
        Me.btn_logout.Name = "btn_logout"
        Me.btn_logout.Size = New System.Drawing.Size(75, 23)
        Me.btn_logout.TabIndex = 0
        Me.btn_logout.Text = "Logout"
        Me.btn_logout.UseVisualStyleBackColor = True
        '
        'frm_settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 192)
        Me.Controls.Add(Me.grp_appSettings)
        Me.Controls.Add(Me.grp_chatSettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_settings"
        Me.Text = "Settings"
        Me.grp_chatSettings.ResumeLayout(False)
        Me.grp_appSettings.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dlog_color As ColorDialog
    Friend WithEvents grp_chatSettings As GroupBox
    Friend WithEvents grp_appSettings As GroupBox
    Friend WithEvents btn_color As Button
    Friend WithEvents btn_logout As Button
End Class
