<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_main))
        Me.grp_login = New System.Windows.Forms.GroupBox()
        Me.chk_rememberMe = New System.Windows.Forms.CheckBox()
        Me.pbx_appLogo = New System.Windows.Forms.PictureBox()
        Me.lbl_passwordRepeat = New System.Windows.Forms.Label()
        Me.txt_passwordRepeat = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbl_IAmA = New System.Windows.Forms.Label()
        Me.lbl_password = New System.Windows.Forms.Label()
        Me.lbl_userName = New System.Windows.Forms.Label()
        Me.rbtn_userExist = New System.Windows.Forms.RadioButton()
        Me.rbtn_userNew = New System.Windows.Forms.RadioButton()
        Me.btn_login = New System.Windows.Forms.Button()
        Me.txt_password = New System.Windows.Forms.TextBox()
        Me.txt_userName = New System.Windows.Forms.TextBox()
        Me.grp_chat = New System.Windows.Forms.GroupBox()
        Me.btn_send = New System.Windows.Forms.Button()
        Me.txt_message = New System.Windows.Forms.TextBox()
        Me.pbx_settings = New System.Windows.Forms.PictureBox()
        Me.pnl_messages = New System.Windows.Forms.Panel()
        Me.grp_login.SuspendLayout()
        CType(Me.pbx_appLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_chat.SuspendLayout()
        CType(Me.pbx_settings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grp_login
        '
        Me.grp_login.BackColor = System.Drawing.SystemColors.Control
        Me.grp_login.Controls.Add(Me.chk_rememberMe)
        Me.grp_login.Controls.Add(Me.pbx_appLogo)
        Me.grp_login.Controls.Add(Me.lbl_passwordRepeat)
        Me.grp_login.Controls.Add(Me.txt_passwordRepeat)
        Me.grp_login.Controls.Add(Me.Label4)
        Me.grp_login.Controls.Add(Me.lbl_IAmA)
        Me.grp_login.Controls.Add(Me.lbl_password)
        Me.grp_login.Controls.Add(Me.lbl_userName)
        Me.grp_login.Controls.Add(Me.rbtn_userExist)
        Me.grp_login.Controls.Add(Me.rbtn_userNew)
        Me.grp_login.Controls.Add(Me.btn_login)
        Me.grp_login.Controls.Add(Me.txt_password)
        Me.grp_login.Controls.Add(Me.txt_userName)
        Me.grp_login.Location = New System.Drawing.Point(12, 12)
        Me.grp_login.Name = "grp_login"
        Me.grp_login.Size = New System.Drawing.Size(381, 437)
        Me.grp_login.TabIndex = 0
        Me.grp_login.TabStop = False
        '
        'chk_rememberMe
        '
        Me.chk_rememberMe.AutoSize = True
        Me.chk_rememberMe.Location = New System.Drawing.Point(23, 342)
        Me.chk_rememberMe.Name = "chk_rememberMe"
        Me.chk_rememberMe.Size = New System.Drawing.Size(102, 17)
        Me.chk_rememberMe.TabIndex = 12
        Me.chk_rememberMe.Text = "Remember login"
        Me.chk_rememberMe.UseVisualStyleBackColor = True
        '
        'pbx_appLogo
        '
        Me.pbx_appLogo.Image = Global.SoftwareDevelopmentChat.My.Resources.Resources.Logo
        Me.pbx_appLogo.InitialImage = Nothing
        Me.pbx_appLogo.Location = New System.Drawing.Point(6, 19)
        Me.pbx_appLogo.Name = "pbx_appLogo"
        Me.pbx_appLogo.Size = New System.Drawing.Size(369, 125)
        Me.pbx_appLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbx_appLogo.TabIndex = 11
        Me.pbx_appLogo.TabStop = False
        '
        'lbl_passwordRepeat
        '
        Me.lbl_passwordRepeat.AutoSize = True
        Me.lbl_passwordRepeat.Location = New System.Drawing.Point(20, 315)
        Me.lbl_passwordRepeat.Name = "lbl_passwordRepeat"
        Me.lbl_passwordRepeat.Size = New System.Drawing.Size(91, 13)
        Me.lbl_passwordRepeat.TabIndex = 10
        Me.lbl_passwordRepeat.Text = "Repeat Password"
        Me.lbl_passwordRepeat.Visible = False
        '
        'txt_passwordRepeat
        '
        Me.txt_passwordRepeat.Location = New System.Drawing.Point(150, 312)
        Me.txt_passwordRepeat.Name = "txt_passwordRepeat"
        Me.txt_passwordRepeat.PasswordChar = Global.Microsoft.VisualBasic.ChrW(183)
        Me.txt_passwordRepeat.Size = New System.Drawing.Size(208, 20)
        Me.txt_passwordRepeat.TabIndex = 9
        Me.txt_passwordRepeat.Visible = False
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(6, 406)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(369, 28)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Created by Henry Sheahan and Ash Sharkey, Software Development 2019."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_IAmA
        '
        Me.lbl_IAmA.AutoSize = True
        Me.lbl_IAmA.Location = New System.Drawing.Point(72, 206)
        Me.lbl_IAmA.Name = "lbl_IAmA"
        Me.lbl_IAmA.Size = New System.Drawing.Size(39, 13)
        Me.lbl_IAmA.TabIndex = 7
        Me.lbl_IAmA.Text = "I am a:"
        '
        'lbl_password
        '
        Me.lbl_password.AutoSize = True
        Me.lbl_password.Location = New System.Drawing.Point(58, 289)
        Me.lbl_password.Name = "lbl_password"
        Me.lbl_password.Size = New System.Drawing.Size(53, 13)
        Me.lbl_password.TabIndex = 6
        Me.lbl_password.Text = "Password"
        '
        'lbl_userName
        '
        Me.lbl_userName.AutoSize = True
        Me.lbl_userName.Location = New System.Drawing.Point(56, 263)
        Me.lbl_userName.Name = "lbl_userName"
        Me.lbl_userName.Size = New System.Drawing.Size(55, 13)
        Me.lbl_userName.TabIndex = 5
        Me.lbl_userName.Text = "Username"
        '
        'rbtn_userExist
        '
        Me.rbtn_userExist.AutoSize = True
        Me.rbtn_userExist.Checked = True
        Me.rbtn_userExist.Location = New System.Drawing.Point(150, 204)
        Me.rbtn_userExist.Name = "rbtn_userExist"
        Me.rbtn_userExist.Size = New System.Drawing.Size(86, 17)
        Me.rbtn_userExist.TabIndex = 4
        Me.rbtn_userExist.TabStop = True
        Me.rbtn_userExist.Text = "Existing User"
        Me.rbtn_userExist.UseVisualStyleBackColor = True
        '
        'rbtn_userNew
        '
        Me.rbtn_userNew.AutoSize = True
        Me.rbtn_userNew.Location = New System.Drawing.Point(150, 227)
        Me.rbtn_userNew.Name = "rbtn_userNew"
        Me.rbtn_userNew.Size = New System.Drawing.Size(72, 17)
        Me.rbtn_userNew.TabIndex = 3
        Me.rbtn_userNew.Text = "New User"
        Me.rbtn_userNew.UseVisualStyleBackColor = True
        '
        'btn_login
        '
        Me.btn_login.Location = New System.Drawing.Point(283, 338)
        Me.btn_login.Name = "btn_login"
        Me.btn_login.Size = New System.Drawing.Size(75, 23)
        Me.btn_login.TabIndex = 2
        Me.btn_login.Text = "Login"
        Me.btn_login.UseVisualStyleBackColor = True
        '
        'txt_password
        '
        Me.txt_password.Location = New System.Drawing.Point(150, 286)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(183)
        Me.txt_password.Size = New System.Drawing.Size(208, 20)
        Me.txt_password.TabIndex = 1
        '
        'txt_userName
        '
        Me.txt_userName.Location = New System.Drawing.Point(150, 260)
        Me.txt_userName.Name = "txt_userName"
        Me.txt_userName.Size = New System.Drawing.Size(208, 20)
        Me.txt_userName.TabIndex = 0
        '
        'grp_chat
        '
        Me.grp_chat.Controls.Add(Me.pnl_messages)
        Me.grp_chat.Controls.Add(Me.btn_send)
        Me.grp_chat.Controls.Add(Me.txt_message)
        Me.grp_chat.Location = New System.Drawing.Point(433, 12)
        Me.grp_chat.Name = "grp_chat"
        Me.grp_chat.Size = New System.Drawing.Size(381, 437)
        Me.grp_chat.TabIndex = 1
        Me.grp_chat.TabStop = False
        '
        'btn_send
        '
        Me.btn_send.Location = New System.Drawing.Point(300, 408)
        Me.btn_send.Name = "btn_send"
        Me.btn_send.Size = New System.Drawing.Size(75, 23)
        Me.btn_send.TabIndex = 1
        Me.btn_send.Text = "Send"
        Me.btn_send.UseVisualStyleBackColor = True
        '
        'txt_message
        '
        Me.txt_message.Location = New System.Drawing.Point(7, 410)
        Me.txt_message.Name = "txt_message"
        Me.txt_message.Size = New System.Drawing.Size(287, 20)
        Me.txt_message.TabIndex = 0
        '
        'pbx_settings
        '
        Me.pbx_settings.Image = CType(resources.GetObject("pbx_settings.Image"), System.Drawing.Image)
        Me.pbx_settings.Location = New System.Drawing.Point(363, 12)
        Me.pbx_settings.Name = "pbx_settings"
        Me.pbx_settings.Size = New System.Drawing.Size(34, 31)
        Me.pbx_settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbx_settings.TabIndex = 2
        Me.pbx_settings.TabStop = False
        Me.pbx_settings.Visible = False
        '
        'pnl_messages
        '
        Me.pnl_messages.AutoScroll = True
        Me.pnl_messages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_messages.Location = New System.Drawing.Point(0, 21)
        Me.pnl_messages.Name = "pnl_messages"
        Me.pnl_messages.Size = New System.Drawing.Size(381, 383)
        Me.pnl_messages.TabIndex = 2
        '
        'frm_main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 461)
        Me.Controls.Add(Me.pbx_settings)
        Me.Controls.Add(Me.grp_chat)
        Me.Controls.Add(Me.grp_login)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_main"
        Me.Text = "Login"
        Me.grp_login.ResumeLayout(False)
        Me.grp_login.PerformLayout()
        CType(Me.pbx_appLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_chat.ResumeLayout(False)
        Me.grp_chat.PerformLayout()
        CType(Me.pbx_settings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grp_login As GroupBox
    Friend WithEvents grp_chat As GroupBox
    Friend WithEvents lbl_passwordRepeat As Label
    Friend WithEvents txt_passwordRepeat As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lbl_IAmA As Label
    Friend WithEvents lbl_password As Label
    Friend WithEvents lbl_userName As Label
    Friend WithEvents rbtn_userExist As RadioButton
    Friend WithEvents rbtn_userNew As RadioButton
    Friend WithEvents btn_login As Button
    Friend WithEvents txt_password As TextBox
    Friend WithEvents txt_userName As TextBox
    Friend WithEvents pbx_appLogo As PictureBox
    Friend WithEvents chk_rememberMe As CheckBox
    Friend WithEvents btn_send As Button
    Friend WithEvents txt_message As TextBox
    Friend WithEvents pbx_settings As PictureBox
    Friend WithEvents pnl_messages As Panel
End Class
