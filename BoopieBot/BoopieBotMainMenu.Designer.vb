<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BoopieBotMainMenu
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
        Me.components = New System.ComponentModel.Container()
        Me.TwitchData1 = New System.Windows.Forms.ListBox()
        Me.Btn_Disconnect = New System.Windows.Forms.Button()
        Me.LstBox_Chat_1 = New System.Windows.Forms.ListBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TxtBox_Message = New System.Windows.Forms.TextBox()
        Me.Btn_SendMessage = New System.Windows.Forms.Button()
        Me.Btn_UpdateSettings = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.LstView_DataBase = New System.Windows.Forms.ListView()
        Me.ViewerID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ViewerName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ViewerBoopies = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IsSubscriber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'TwitchData1
        '
        Me.TwitchData1.FormattingEnabled = True
        Me.TwitchData1.Location = New System.Drawing.Point(522, 12)
        Me.TwitchData1.Name = "TwitchData1"
        Me.TwitchData1.Size = New System.Drawing.Size(820, 277)
        Me.TwitchData1.TabIndex = 0
        '
        'Btn_Disconnect
        '
        Me.Btn_Disconnect.Location = New System.Drawing.Point(411, 624)
        Me.Btn_Disconnect.Name = "Btn_Disconnect"
        Me.Btn_Disconnect.Size = New System.Drawing.Size(99, 27)
        Me.Btn_Disconnect.TabIndex = 1
        Me.Btn_Disconnect.Text = "Disconnect Bot"
        Me.Btn_Disconnect.UseVisualStyleBackColor = True
        '
        'LstBox_Chat_1
        '
        Me.LstBox_Chat_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstBox_Chat_1.FormattingEnabled = True
        Me.LstBox_Chat_1.ItemHeight = 20
        Me.LstBox_Chat_1.Location = New System.Drawing.Point(522, 295)
        Me.LstBox_Chat_1.Name = "LstBox_Chat_1"
        Me.LstBox_Chat_1.Size = New System.Drawing.Size(504, 244)
        Me.LstBox_Chat_1.TabIndex = 2
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'TxtBox_Message
        '
        Me.TxtBox_Message.Location = New System.Drawing.Point(12, 305)
        Me.TxtBox_Message.Name = "TxtBox_Message"
        Me.TxtBox_Message.Size = New System.Drawing.Size(417, 20)
        Me.TxtBox_Message.TabIndex = 3
        '
        'Btn_SendMessage
        '
        Me.Btn_SendMessage.Location = New System.Drawing.Point(435, 294)
        Me.Btn_SendMessage.Name = "Btn_SendMessage"
        Me.Btn_SendMessage.Size = New System.Drawing.Size(75, 40)
        Me.Btn_SendMessage.TabIndex = 4
        Me.Btn_SendMessage.Text = "Send Message"
        Me.Btn_SendMessage.UseVisualStyleBackColor = True
        '
        'Btn_UpdateSettings
        '
        Me.Btn_UpdateSettings.Location = New System.Drawing.Point(12, 622)
        Me.Btn_UpdateSettings.Margin = New System.Windows.Forms.Padding(1)
        Me.Btn_UpdateSettings.Name = "Btn_UpdateSettings"
        Me.Btn_UpdateSettings.Size = New System.Drawing.Size(206, 31)
        Me.Btn_UpdateSettings.TabIndex = 5
        Me.Btn_UpdateSettings.Text = "Update Settings"
        Me.Btn_UpdateSettings.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        Me.Timer2.Interval = 60000
        '
        'LstView_DataBase
        '
        Me.LstView_DataBase.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ViewerID, Me.ViewerName, Me.ViewerBoopies, Me.IsSubscriber})
        Me.LstView_DataBase.GridLines = True
        Me.LstView_DataBase.Location = New System.Drawing.Point(12, 344)
        Me.LstView_DataBase.Margin = New System.Windows.Forms.Padding(1)
        Me.LstView_DataBase.MultiSelect = False
        Me.LstView_DataBase.Name = "LstView_DataBase"
        Me.LstView_DataBase.Size = New System.Drawing.Size(500, 260)
        Me.LstView_DataBase.TabIndex = 6
        Me.LstView_DataBase.UseCompatibleStateImageBehavior = False
        Me.LstView_DataBase.View = System.Windows.Forms.View.Details
        '
        'ViewerID
        '
        Me.ViewerID.Text = "Viewer ID"
        Me.ViewerID.Width = 120
        '
        'ViewerName
        '
        Me.ViewerName.Text = "Viewer Name"
        Me.ViewerName.Width = 120
        '
        'ViewerBoopies
        '
        Me.ViewerBoopies.Text = "Viewer Boopies"
        Me.ViewerBoopies.Width = 120
        '
        'IsSubscriber
        '
        Me.IsSubscriber.Text = "Is Sub?"
        Me.IsSubscriber.Width = 120
        '
        'ListView1
        '
        Me.ListView1.Location = New System.Drawing.Point(1028, 295)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(1)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(289, 193)
        Me.ListView1.TabIndex = 7
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'BoopieBotMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 656)
        Me.ControlBox = False
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.LstView_DataBase)
        Me.Controls.Add(Me.Btn_UpdateSettings)
        Me.Controls.Add(Me.Btn_SendMessage)
        Me.Controls.Add(Me.TxtBox_Message)
        Me.Controls.Add(Me.LstBox_Chat_1)
        Me.Controls.Add(Me.Btn_Disconnect)
        Me.Controls.Add(Me.TwitchData1)
        Me.Name = "BoopieBotMainMenu"
        Me.Text = "BoopieBotMainMenu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents LstView_DataBase As ListView
    Friend WithEvents ViewerID As ColumnHeader
    Friend WithEvents ViewerName As ColumnHeader
    Friend WithEvents ViewerBoopies As ColumnHeader
    Friend WithEvents IsSubscriber As ColumnHeader
    Friend WithEvents TwitchData1 As ListBox
    Friend WithEvents Btn_Disconnect As Button
    Friend WithEvents LstBox_Chat_1 As ListBox
    Friend WithEvents TxtBox_Message As TextBox
    Friend WithEvents Btn_SendMessage As Button
    Friend WithEvents Btn_UpdateSettings As Button
    Friend WithEvents ListView1 As ListView
End Class
