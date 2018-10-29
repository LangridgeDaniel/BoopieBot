Public Class Frm_ChangePreferences
    Dim Preferences As New Settings

    Private Sub Frm_ChangePreferences_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Height = 510
        Me.Width = 600

    End Sub

#Region "Roulette"
    Private Sub Btn_ViewerUpdateRouletteOdds_Click(sender As Object, e As EventArgs) Handles Btn_ViewerUpdateRouletteOdds.Click
        Preferences.UpdateRouletteOdds(False)
    End Sub
    Private Sub Btn_ViewerUpdateRoulettePayout_Click(sender As Object, e As EventArgs) Handles Btn_ViewerUpdateRoulettePayout.Click
        Preferences.UpdateRoulettePayout(False)
    End Sub
    Private Sub Btn_SubUpdateRouletteOdds_Click(sender As Object, e As EventArgs) Handles Btn_SubUpdateRouletteOdds.Click
        Preferences.UpdateRouletteOdds(True)
    End Sub
    Private Sub Btn_SubUpdateRoulettePayout_Click(sender As Object, e As EventArgs) Handles Btn_SubUpdateRoulettePayout.Click
        Preferences.UpdateRoulettePayout(True)
    End Sub
#End Region

#Region "Slots"
    Private Sub Btn_ViewerUpdateSlotsOdds_Click(sender As Object, e As EventArgs) Handles Btn_ViewerUpdateSlotsOdds.Click
        Preferences.UpdateSlotsOdds(False)
    End Sub
    Private Sub Btn_ViewerUpdateSlotsPayout_Click(sender As Object, e As EventArgs) Handles Btn_ViewerUpdateSlotsPayout.Click
        Preferences.UpdateSlotsPayout(False)
    End Sub
    Private Sub Btn_SubUpdateSlotsOdds_Click(sender As Object, e As EventArgs) Handles Btn_SubUpdateSlotsOdds.Click
        Preferences.UpdateSlotsOdds(True)
    End Sub
    Private Sub Btn_SubUpdateSlotsPayout_Click(sender As Object, e As EventArgs) Handles Btn_SubUpdateSlotsPayout.Click
        Preferences.UpdateSlotsPayout(True)
    End Sub
#End Region

#Region "Timed Boopie Increase"
    Private Sub Btn_SubEditBoopieValue_Click(sender As Object, e As EventArgs) Handles Btn_SubEditBoopieValue.Click
        Preferences.UpdateTimedPayout(True)
    End Sub
    Private Sub Btn_EditBoopieTimeDelay_Click(sender As Object, e As EventArgs) Handles Btn_EditBoopieTimeDelay.Click
        Preferences.UpdateTimeDelay()
        BoopieBotMainMenu.Timer1.Interval = Preferences.GetBoopieTimeDelay * 100000
    End Sub
    Private Sub Btn_ViewerEditBoopieValue_Click(sender As Object, e As EventArgs) Handles Btn_ViewerEditBoopieValue.Click
        Preferences.UpdateTimedPayout(False)
    End Sub
#End Region

#Region "SuperMod"
    Private Sub Btn_AddSuperMod_Click(sender As Object, e As EventArgs) Handles Btn_AddSuperMod.Click

        Dim Valid As Boolean = True

        Do
            Dim User As String = LCase(InputBox("Please enter the name of the user you wish to SuperMod", "SuperMod"))

            If Len(User) > 0 Then
                Dim HashLocation As Integer = BoopieBotMainMenu.GetHashLocation(User)
                If HashLocation = 0 Then
                    MsgBox("User: " & User & " is not in the Database, please ask them to perform an action involving boopies!")
                Else
                    BoopieBotMainMenu.ToggleSuperMod(User, HashLocation, "Add")
                End If
            Else
                'do nothing
            End If
        Loop Until Valid = True

    End Sub
    Private Sub Btn_RemoveSuperMod_Click(sender As Object, e As EventArgs) Handles Btn_RemoveSuperMod.Click

        Dim Valid As Boolean = True

        Do
            Dim User As String = LCase(InputBox("Please enter the name of the user you wish to SuperMod", "SuperMod"))

            If Len(User) > 0 Then
                Dim HashLocation As Integer = BoopieBotMainMenu.GetHashLocation(User)
                If HashLocation = 0 Then
                    MsgBox("User: " & User & " is not in the Database, please ask them to perform an action involving boopies!")
                Else
                    BoopieBotMainMenu.ToggleSuperMod(User, HashLocation, "Remove")
                End If
            Else
                'do nothing
            End If
        Loop Until Valid = True

    End Sub
#End Region

    Private Sub Btn_BackToChat_Click(sender As Object, e As EventArgs) Handles Btn_BackToChat.Click
        BoopieBotMainMenu.Enabled = True
        Me.Close()
    End Sub

    Private Sub Btn_UpdateRLRanks_Click(sender As Object, e As EventArgs) Handles Btn_UpdateRLRanks.Click
        Preferences.UpdateRLRank()
    End Sub

    Private Sub Btn_UpdateDiscord_Click(sender As Object, e As EventArgs) Handles Btn_UpdateDiscord.Click
        Preferences.UpdateDiscord()
    End Sub

    Private Sub Btn_UpdatePrefix_Click(sender As Object, e As EventArgs) Handles Btn_UpdatePrefix.Click
        Preferences.UpdatePrefix()
    End Sub

    Private Sub Btn_ResetPreferences_Click(sender As Object, e As EventArgs) Handles Btn_ResetPreferences.Click
        Form1.RESETALLSETTINGS("User")
    End Sub

End Class