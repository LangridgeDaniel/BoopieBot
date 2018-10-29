<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.TxtBox_UserName = New System.Windows.Forms.TextBox()
        Me.TxtBox_NickName = New System.Windows.Forms.TextBox()
        Me.TxtBox_OAuth = New System.Windows.Forms.TextBox()
        Me.ChkBox_Save = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ChkBox_Dev = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Location = New System.Drawing.Point(12, 96)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(120, 23)
        Me.Btn_Connect.TabIndex = 0
        Me.Btn_Connect.Text = "Connect"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'TxtBox_UserName
        '
        Me.TxtBox_UserName.Location = New System.Drawing.Point(12, 12)
        Me.TxtBox_UserName.Name = "TxtBox_UserName"
        Me.TxtBox_UserName.Size = New System.Drawing.Size(224, 20)
        Me.TxtBox_UserName.TabIndex = 1
        '
        'TxtBox_NickName
        '
        Me.TxtBox_NickName.Location = New System.Drawing.Point(12, 38)
        Me.TxtBox_NickName.Name = "TxtBox_NickName"
        Me.TxtBox_NickName.Size = New System.Drawing.Size(224, 20)
        Me.TxtBox_NickName.TabIndex = 2
        '
        'TxtBox_OAuth
        '
        Me.TxtBox_OAuth.Location = New System.Drawing.Point(12, 64)
        Me.TxtBox_OAuth.Name = "TxtBox_OAuth"
        Me.TxtBox_OAuth.Size = New System.Drawing.Size(224, 20)
        Me.TxtBox_OAuth.TabIndex = 3
        '
        'ChkBox_Save
        '
        Me.ChkBox_Save.AutoSize = True
        Me.ChkBox_Save.Location = New System.Drawing.Point(144, 100)
        Me.ChkBox_Save.Name = "ChkBox_Save"
        Me.ChkBox_Save.Size = New System.Drawing.Size(92, 17)
        Me.ChkBox_Save.TabIndex = 4
        Me.ChkBox_Save.Text = "Save Settings"
        Me.ChkBox_Save.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(242, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Host Username"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(242, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Bot Username"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(242, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Bot OAuth Token"
        '
        'ChkBox_Dev
        '
        Me.ChkBox_Dev.AutoSize = True
        Me.ChkBox_Dev.Location = New System.Drawing.Point(245, 100)
        Me.ChkBox_Dev.Name = "ChkBox_Dev"
        Me.ChkBox_Dev.Size = New System.Drawing.Size(76, 17)
        Me.ChkBox_Dev.TabIndex = 8
        Me.ChkBox_Dev.Text = "Dev Mode"
        Me.ChkBox_Dev.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(144, 123)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(92, 17)
        Me.CheckBox1.TabIndex = 9
        Me.CheckBox1.Text = "FirstExecution"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 138)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.ChkBox_Dev)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ChkBox_Save)
        Me.Controls.Add(Me.TxtBox_OAuth)
        Me.Controls.Add(Me.TxtBox_NickName)
        Me.Controls.Add(Me.TxtBox_UserName)
        Me.Controls.Add(Me.Btn_Connect)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Btn_Connect As Button
    Friend WithEvents TxtBox_UserName As TextBox
    Friend WithEvents TxtBox_NickName As TextBox
    Friend WithEvents TxtBox_OAuth As TextBox
    Friend WithEvents ChkBox_Save As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ChkBox_Dev As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
End Class
