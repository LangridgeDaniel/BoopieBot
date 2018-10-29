Imports System.Data.SQLite

Public Class Settings

#Region "RL Rank"
    Public Function GetRLRank()
        'vbcrlf'

        Dim Rank As String = "1's: " & My.Settings.RLRankOnes & ", 2's: " & My.Settings.RLRankTwos & ", 3's: " & My.Settings.RLRankThrees & ", Solo 3's: " & My.Settings.RLRankSoloThrees
        Return Rank

    End Function
    Public Sub UpdateRLRank()
        Dim Valid As Boolean = True

        Do
            Dim Ones As String = InputBox("Please Enter your 1's rank", "1's Rank")
            If Len(Ones) > 0 Then
                Valid = True
                My.Settings.RLRankOnes = Ones
            Else
                'do nothing
            End If
        Loop Until Valid = True

        Valid = True
        Do
            Dim Twos As String = InputBox("Please Enter your 2's rank", "2's Rank")
            If Len(Twos) > 0 Then
                Valid = True
                My.Settings.RLRankTwos = Twos
            Else
                'do nothing
            End If
        Loop Until Valid = True

        Valid = True
        Do
            Dim Threes As String = InputBox("Please Enter your 3's rank", "3's Rank")
            If Len(Threes) > 0 Then
                Valid = True
                My.Settings.RLRankThrees = Threes
            Else
                'do nothing
            End If
        Loop Until Valid = True

        Valid = True
        Do
            Dim SThrees As String = InputBox("Please Enter your Solo 3's rank", "Solo 3's Rank")
            If Len(SThrees) > 0 Then
                Valid = True
                My.Settings.RLRankSoloThrees = SThrees
            Else
                'do nothing
            End If
        Loop Until Valid = True

        My.Settings.Save()

    End Sub
#End Region

#Region "Garlic Bread"
    Public Function GetGarlicBreadOdds(subs As Boolean)
        If subs = True Then
            Return My.Settings.SubGarlicBreadOdds()
        Else
            Return My.Settings.ViewerGarlicBreadOdds()
        End If
    End Function
    Public Sub UpdateGarlicBreadOdds(subs As Boolean)
        Dim Valid As Boolean = True

        If subs = False Then
            Do
                Dim Odds As String = InputBox("What would you like the Garlic Bread odds for Viewers to be? (1 win out of of?)", "Garlic Bread Odds")
                If Len(Odds) > 0 Then
                    If IsNumeric(Odds) Then
                        Valid = True
                        My.Settings.ViewerGarlicBreadOdds = Odds
                    Else
                        MsgBox("Please enter a number!")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        Else
            Do
                Dim Odds As String = InputBox("What would you like the Garlic Bread odds for Subs to be? (1 win out of of?)", "Garlic Bread Odds")
                If Len(Odds) > 0 Then
                    If IsNumeric(Odds) Then
                        Valid = True
                        My.Settings.SubGarlicBreadOdds = Odds
                    Else
                        MsgBox("Please enter a number!")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        End If

        My.Settings.Save()

    End Sub
    Public Function GetGarlicBreadPayout(subs As Boolean)
        If subs = True Then
            Return My.Settings.ViewerGarlicBreadMultiplier()
        Else
            Return My.Settings.SubGarlicBreadMultiplier()
        End If
    End Function
    Public Sub UpdateGarlicBreadPayout(subs As Boolean)
        Dim Valid As Boolean = True

        If subs = False Then
            Do
                Dim Multiplier As String = InputBox("What would you like the payout multiplier to be for Garlic Bread for Viewers?", "Garlic Bread Payout")
                If Len(Multiplier) > 0 Then
                    If IsNumeric(Multiplier) Then
                        My.Settings.ViewerRouletteMultiplier = Multiplier
                        Valid = True
                    Else
                        MsgBox("Please enter a number")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        Else
            Do
                Dim Multiplier As String = InputBox("What would you like the payout multiplier to be for Garlic Bread for Subs?", "Garlic Bread Payout")
                If Len(Multiplier) > 0 Then
                    If IsNumeric(Multiplier) Then
                        My.Settings.SubRouletteMultiplier = Multiplier
                        Valid = True
                    Else
                        MsgBox("Please enter a number")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        End If

        My.Settings.Save()
    End Sub
#End Region

#Region "Roulette"
    Public Function GetRouletteOdds(subs As Boolean)
        If subs = True Then
            Return My.Settings.SubRouletteOdds()
        Else
            Return My.Settings.ViewerRouletteOdds()
        End If
    End Function
    Public Sub UpdateRouletteOdds(subs As Boolean)
        Dim Valid As Boolean = True

        If subs = False Then
            Do
                Dim Odds As String = InputBox("What would you like the Roulette odds for Viewers to be? (1 win out of of?)", "Roulette Odds")
                If Len(Odds) > 0 Then
                    If IsNumeric(Odds) Then
                        Valid = True
                        My.Settings.ViewerRouletteOdds = Odds
                    Else
                        MsgBox("Please enter a number!")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        Else
            Do
                Dim Odds As String = InputBox("What would you like the Roulette odds for Subs to be? (1 win out of of?)", "Roulette Odds")
                If Len(Odds) > 0 Then
                    If IsNumeric(Odds) Then
                        Valid = True
                        My.Settings.SubRouletteOdds = Odds
                    Else
                        MsgBox("Please enter a number!")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        End If

        My.Settings.Save()

    End Sub
    Public Function GetRoulettePayout(subs As Boolean)
        If subs = True Then
            Return My.Settings.SubRouletteMultiplier()
        Else
            Return My.Settings.ViewerRouletteMultiplier()
        End If
    End Function
    Public Sub UpdateRoulettePayout(subs As Boolean)
        Dim Valid As Boolean = True

        If subs = False Then
            Do
                Dim Multiplier As String = InputBox("What would you like the payout multiplier to be for Roulette for Viewers?", "Roulette Payout")
                If Len(Multiplier) > 0 Then
                    If IsNumeric(Multiplier) Then
                        My.Settings.ViewerRouletteMultiplier = Multiplier
                        Valid = True
                    Else
                        MsgBox("Please enter a number")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        Else
            Do
                Dim Multiplier As String = InputBox("What would you like the payout multiplier to be for Roulette for Subs?", "Roulette Payout")
                If Len(Multiplier) > 0 Then
                    If IsNumeric(Multiplier) Then
                        My.Settings.SubRouletteMultiplier = Multiplier
                        Valid = True
                    Else
                        MsgBox("Please enter a number")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        End If

        My.Settings.Save()
    End Sub
#End Region

#Region "Slots"
    Public Function GetSlotsOdds(subs As Boolean)
        If subs = True Then
            Return My.Settings.ViewerSlotsOdds()
        Else
            Return My.Settings.SubSlotsOdds()
        End If
    End Function
    Public Sub UpdateSlotsOdds(subs As Boolean)
        Dim Valid As Boolean = True

        If subs = False Then
            Do
                Dim Odds As String = InputBox("What would you like the Slots odds to be for viewers? (1 win out of of?)", "Slots Odds")
                If Len(Odds) > 0 Then
                    If IsNumeric(Odds) Then
                        My.Settings.ViewerSlotsMultiplier = Odds
                        Valid = True
                    Else
                        MsgBox("Please enter a number")
                        Valid = False
                    End If
                Else
                    'Do nothing
                End If
            Loop Until Valid = True
        Else
            Do
                Dim Odds As String = InputBox("What would you like the Slots odds to be for subs? (1 win out of of?)", "Slots Odds")
                If Len(Odds) > 0 Then
                    If IsNumeric(Odds) Then
                        My.Settings.SubSlotsMultiplier = Odds
                        Valid = True
                    Else
                        MsgBox("Please enter a number")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        End If

        My.Settings.Save()
    End Sub
    Public Function GetViewerSlotsPayout(subs As Boolean)
        If subs = True Then
            Return My.Settings.SubSlotsOdds()
        Else
            Return My.Settings.ViewerSlotsOdds()
        End If
    End Function
    Public Sub UpdateSlotsPayout(subs As Boolean)
        Dim Valid As Boolean = True

        If subs = False Then
            Do
                Dim Multiplier As String = InputBox("What do you want the payout multiplier to be for Slots for Viewers?", "Slots Payout")
                If Len(Multiplier) > 0 Then
                    If IsNumeric(Multiplier) Then
                        My.Settings.ViewerSlotsMultiplier = Multiplier
                        Valid = True
                    Else
                        MsgBox("Please enter a number")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        Else
            Do
                Dim Multiplier As String = InputBox("What do you want the payout multiplier to be for Slots for Subs?", "Slots Payout")
                If Len(Multiplier) > 0 Then
                    If IsNumeric(Multiplier) Then
                        My.Settings.SubSlotsMultiplier = Multiplier
                        Valid = True
                    Else
                        MsgBox("Please enter a number")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        End If
    End Sub
#End Region

#Region "Discord"
    Public Function GetDiscord()
        Return My.Settings.Discord
    End Function
    Public Sub UpdateDiscord()
        Dim Valid As Boolean = True

        Do
            Dim Link As String = InputBox("Please enter your discord link!", "Discord Link")
            If Link <> "" Then
                If Len(Link) > 0 Then
                    My.Settings.Discord = Link
                    Valid = True
                Else
                    MsgBox("Please enter a value")
                    Valid = False
                End If
            Else
                'do nothing
            End If
        Loop Until Valid = True

        My.Settings.Save()
    End Sub
#End Region

#Region "Prefix"
    Public Function GetPrefix()
        Return My.Settings.CommandPrefix()
    End Function
    Public Sub UpdatePrefix()
        Dim Valid As Boolean = True

        Do
            Dim Prefix As Char = InputBox("Please enter your new Prefix Character", "Command Prefix")
            If Len(Prefix) <> 0 Then
                My.Settings.CommandPrefix = Prefix
                Valid = True
            Else
                'do nothing
            End If
        Loop Until Valid = True

        My.Settings.Save()
    End Sub

#End Region

#Region "Timed Boopie increase"
    Public Sub UpdateTimeDelay()
        Dim Valid As Boolean = True

        Do
            Dim Delay As String = InputBox("What would you like the time delay (in minutes) to be for everyone to get paid Boopies?", "Boopie Time Delay")
            If Len(Delay) > 0 Then
                If IsNumeric(Delay) Then
                    Valid = True
                    My.Settings.BoopieDelay = Delay
                Else
                    MsgBox("Please enter a number!")
                    Valid = False
                End If
            Else
                'do nothing
            End If
        Loop Until Valid = True

        My.Settings.Save()

        MsgBox(GetBoopieTimeDelay)
    End Sub
    Public Function GetBoopieTimeDelay()
        Return My.Settings.BoopieDelay()
    End Function
    Public Sub UpdateTimedPayout(subs As Boolean)
        Dim Valid As Boolean = True

        If subs = False Then

            Do
                Dim Payout As String = InputBox("What would you like the Boopie Increase to be for Viewers?", "Boopie Timed increase")
                If Len(Payout) > 0 Then
                    If IsNumeric(Payout) Then
                        Valid = True
                        My.Settings.ViewerBoopiePayout = Payout
                    Else
                        MsgBox("Please enter a number!")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True

        Else
            Do
                Dim Payout As String = InputBox("What would you like the Boopie Increase to be for Subs?", "Boopie Timed increase")
                If Len(Payout) > 0 Then
                    If IsNumeric(Payout) Then
                        Valid = True
                        My.Settings.SubBoopiePayout = Payout
                    Else
                        MsgBox("Please enter a number!")
                        Valid = False
                    End If
                Else
                    'do nothing
                End If
            Loop Until Valid = True
        End If
        My.Settings.Save()
    End Sub
    Public Function GetBoopiePayout(subs As Boolean)
        If subs = True Then
            Return My.Settings.SubBoopiePayout()
        Else
            Return My.Settings.ViewerBoopiePayout()
        End If
    End Function
#End Region
End Class
