<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_groupChatSelector
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_ok = New System.Windows.Forms.Button()
        Me.btn_cancel = New System.Windows.Forms.Button()
        Me.btn_new = New System.Windows.Forms.Button()
        Me.txt_grpMember1 = New System.Windows.Forms.TextBox()
        Me.txt_grpMember2 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please add recipients below:"
        '
        'btn_ok
        '
        Me.btn_ok.Enabled = False
        Me.btn_ok.Location = New System.Drawing.Point(295, 95)
        Me.btn_ok.Name = "btn_ok"
        Me.btn_ok.Size = New System.Drawing.Size(75, 23)
        Me.btn_ok.TabIndex = 1
        Me.btn_ok.Text = "Ok"
        Me.btn_ok.UseVisualStyleBackColor = True
        '
        'btn_cancel
        '
        Me.btn_cancel.Location = New System.Drawing.Point(214, 95)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(75, 23)
        Me.btn_cancel.TabIndex = 2
        Me.btn_cancel.Text = "Cancel"
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'btn_new
        '
        Me.btn_new.Location = New System.Drawing.Point(214, 65)
        Me.btn_new.Name = "btn_new"
        Me.btn_new.Size = New System.Drawing.Size(75, 23)
        Me.btn_new.TabIndex = 3
        Me.btn_new.Text = "Add another"
        Me.btn_new.UseVisualStyleBackColor = True
        '
        'txt_grpMember1
        '
        Me.txt_grpMember1.Location = New System.Drawing.Point(16, 39)
        Me.txt_grpMember1.Name = "txt_grpMember1"
        Me.txt_grpMember1.Size = New System.Drawing.Size(192, 20)
        Me.txt_grpMember1.TabIndex = 5
        '
        'txt_grpMember2
        '
        Me.txt_grpMember2.Location = New System.Drawing.Point(16, 68)
        Me.txt_grpMember2.Name = "txt_grpMember2"
        Me.txt_grpMember2.Size = New System.Drawing.Size(192, 20)
        Me.txt_grpMember2.TabIndex = 6
        '
        'frm_groupChatSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(382, 130)
        Me.Controls.Add(Me.txt_grpMember2)
        Me.Controls.Add(Me.txt_grpMember1)
        Me.Controls.Add(Me.btn_new)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.btn_ok)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_groupChatSelector"
        Me.Text = "Create Group Chat"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btn_ok As Button
    Friend WithEvents btn_cancel As Button
    Friend WithEvents btn_new As Button
    Friend WithEvents txt_grpMember1 As TextBox
    Friend WithEvents txt_grpMember2 As TextBox
End Class
