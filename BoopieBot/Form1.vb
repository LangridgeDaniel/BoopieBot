Public Class Form1
    Public Channel As String
    Public BotName As String
    Public OAuth As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.TwitchRemember = True Then
            TxtBox_NickName.Text = My.Settings.TwitchUser
            TxtBox_OAuth.Text = My.Settings.TwitchOAuth
            TxtBox_UserName.Text = My.Settings.TwitchChannel
            ChkBox_Save.Checked = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click

        If CheckBox1.Checked = True Then
            RESETALLSETTINGS("System")
        End If

        If TxtBox_NickName.Text = "" Or TxtBox_UserName.Text = "" Or TxtBox_OAuth.Text = "" Then
            MsgBox("Please ensure you have filled out every field!!")
        Else
            If ChkBox_Save.Checked = True Then
                My.Settings.TwitchRemember = True
                My.Settings.TwitchChannel = TxtBox_UserName.Text
                My.Settings.TwitchUser = TxtBox_NickName.Text
                My.Settings.TwitchOAuth = TxtBox_OAuth.Text
                My.Settings.Save()
            Else
                My.Settings.TwitchRemember = False
                My.Settings.Save()
            End If

            Channel = TxtBox_UserName.Text
            BotName = TxtBox_NickName.Text
            OAuth = TxtBox_OAuth.Text

            BoopieBotMainMenu.IrcClient("irc.twitch.tv", 6667, BotName, OAuth)
            BoopieBotMainMenu.joinRoom(Channel)
            If BoopieBotMainMenu.ircConnected() Then
                BoopieBotMainMenu.Show()
            End If
            BoopieBotMainMenu.sendChatMessage(BotName + " is alive!", BotName)

        End If

    End Sub

    Public Sub RESETALLSETTINGS(Which As String)

        If Which = "User" Then
            Dim result As Integer = MessageBox.Show("Are you positive you want to reset the bot to factory settings????? This will reset all odds that have been changed in app, but won't affect Boopies Scores", "", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                My.Settings.ViewerRouletteOdds = 50
                My.Settings.ViewerRouletteMultiplier = 1
                My.Settings.SubRouletteOdds = 75
                My.Settings.SubRouletteMultiplier = 2

                My.Settings.ViewerSlotsOdds = 50
                My.Settings.ViewerSlotsMultiplier = 1
                My.Settings.SubSlotsOdds = 75
                My.Settings.SubSlotsMultiplier = 2

                My.Settings.ViewerGarlicBreadMultiplier = 50
                My.Settings.SubGarlicBreadMultiplier = 1
                My.Settings.ViewerGarlicBreadOdds = 75
                My.Settings.SubGarlicBreadOdds = 2

                My.Settings.Discord = "NA"

                My.Settings.RLRankOnes = "NA"
                My.Settings.RLRankTwos = "NA"
                My.Settings.RLRankThrees = "NA"
                My.Settings.RLRankSoloThrees = "NA"

                My.Settings.CommandPrefix = "?"

                My.Settings.BoopieDelay = 60
                My.Settings.ViewerBoopiePayout = 100
                My.Settings.SubBoopiePayout = 200

                My.Settings.Save()
            End If

        ElseIf Which = "System" Then
            My.Settings.ViewerRouletteOdds = 2
            My.Settings.ViewerRouletteMultiplier = 2
            My.Settings.SubRouletteOdds = 2
            My.Settings.SubRouletteMultiplier = 2

            My.Settings.ViewerSlotsOdds = 2
            My.Settings.ViewerSlotsMultiplier = 2
            My.Settings.SubSlotsOdds = 2
            My.Settings.SubSlotsMultiplier = 2

            My.Settings.ViewerGarlicBreadMultiplier = 50
            My.Settings.SubGarlicBreadMultiplier = 1
            My.Settings.ViewerGarlicBreadOdds = 75
            My.Settings.SubGarlicBreadOdds = 2

            My.Settings.Discord = "NA"

            My.Settings.RLRankOnes = "NA"
            My.Settings.RLRankTwos = "NA"
            My.Settings.RLRankThrees = "NA"
            My.Settings.RLRankSoloThrees = "NA"

            My.Settings.CommandPrefix = "?"

            My.Settings.BoopieDelay = 60
            My.Settings.ViewerBoopiePayout = 100
            My.Settings.SubBoopiePayout = 200

            My.Settings.ViewerGarlicBreadMultiplier = 2
            My.Settings.SubGarlicBreadMultiplier = 2
            My.Settings.ViewerGarlicBreadOdds = 2
            My.Settings.SubGarlicBreadOdds = 2

            My.Settings.Save()
        End If

    End Sub

End Class
