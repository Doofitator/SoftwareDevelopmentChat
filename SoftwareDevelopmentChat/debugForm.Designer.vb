<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_debug
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
        Me.btn_forceLogin = New System.Windows.Forms.Button()
        Me.btn_forceCredentials = New System.Windows.Forms.Button()
        Me.btn_newUser = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_forceLogin
        '
        Me.btn_forceLogin.Location = New System.Drawing.Point(35, 25)
        Me.btn_forceLogin.Name = "btn_forceLogin"
        Me.btn_forceLogin.Size = New System.Drawing.Size(138, 23)
        Me.btn_forceLogin.TabIndex = 1
        Me.btn_forceLogin.Text = "Successful login"
        Me.btn_forceLogin.UseVisualStyleBackColor = True
        '
        'btn_forceCredentials
        '
        Me.btn_forceCredentials.Location = New System.Drawing.Point(175, 83)
        Me.btn_forceCredentials.Name = "btn_forceCredentials"
        Me.btn_forceCredentials.Size = New System.Drawing.Size(139, 23)
        Me.btn_forceCredentials.TabIndex = 2
        Me.btn_forceCredentials.Text = "Force Credential Save"
        Me.btn_forceCredentials.UseVisualStyleBackColor = True
        '
        'btn_newUser
        '
        Me.btn_newUser.Location = New System.Drawing.Point(309, 41)
        Me.btn_newUser.Name = "btn_newUser"
        Me.btn_newUser.Size = New System.Drawing.Size(126, 23)
        Me.btn_newUser.TabIndex = 3
        Me.btn_newUser.Text = "Write new user"
        Me.btn_newUser.UseVisualStyleBackColor = True
        '
        'frm_debug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 113)
        Me.Controls.Add(Me.btn_newUser)
        Me.Controls.Add(Me.btn_forceCredentials)
        Me.Controls.Add(Me.btn_forceLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_debug"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "debugForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_forceLogin As Button
    Friend WithEvents btn_forceCredentials As Button
    Friend WithEvents btn_newUser As Button
End Class
