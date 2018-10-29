Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets

Imports Microsoft.Office.Interop

Public Class BoopieBotMainMenu
#Region "Global Variables"
    Private CommandThread As Thread 'General Variables
    Private appPath As String = Application.StartupPath()

    Dim LstBox_Chat As New myListBox
    Dim BotHandle As String = "@" + Form1.BotName
    Public Channel As String = Form1.Channel

    Dim LastBoopieIncrease As DateTime = DateTime.Now

    Dim CommandPrefix As Char = "?"
    Dim Preferences As New Settings

    Dim xlLocation = Application.StartupPath + "\BoopieDataBase.xlsx"
    Dim xlApp As New Excel.Application
    Dim xlBook As Excel.Workbook = xlApp.Workbooks.Open(xlLocation)
    Dim xlSheet As Excel.Worksheet = xlBook.Worksheets("Viewers")

#End Region
    'This is a sexy comment

#Region "Loading Shit"
    Private Sub BoopieBotMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Control.CheckForIllegalCrossThreadCalls = False

        Timer1.Interval = Preferences.GetBoopieTimeDelay * 100000

        Dim Stuff As New List(Of String)
        Stuff = GetViewersList()

        If My.Settings.BoopieDelay = "0" Then
            Form1.RESETALLSETTINGS("System")
        End If

        UpdateListView("System")

        LstBox_Chat_1.Visible = False

        LoadChatLog()

        Me.Height = 700
        If Form1.ChkBox_Dev.Checked = True Then
            Me.Width = 1375
        Else
            Me.Width = 500
        End If

        If Form1.ChkBox_Dev.Checked = False Then
            TwitchData1.Visible = False
            Me.Width = 543
        End If

        LstBox_Chat.Visible = True

        Me.Controls.Add(LstBox_Chat)

        CommandThread = New Thread(AddressOf StartParam)
        CommandThread.IsBackground = True
        CommandThread.Start()

    End Sub

    Private Sub LoadChatLog()
        With LstBox_Chat
            .Top = 12
            .Left = 12
            .Width = 504
            .Height = 264
            .Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            .HorizontalScrollbar = True
        End With
    End Sub

    Private Sub LoadProfanity(ByRef Profanity As List(Of String))

        Try
            Dim FileReader As New System.IO.StreamReader(appPath + "/BadWords.txt")

            Profanity.Clear()

            While FileReader.EndOfStream <> True
                Profanity.Add(FileReader.ReadLine().Trim().ToLower())
            End While
        Catch
            MsgBox("Failed to load BadWords.txt, have entered a test word to avoid crashes")
            Profanity.Add("shit")
        End Try

    End Sub

    Private Sub UpdateListView(Which As String)
        Dim MaxRow As Integer = xlSheet.Cells(2, 6).value

        Try
            LstView_DataBase.Items.Clear()

            If Which = "System" Then
                xlApp.Visible = True
            End If

            Dim C(4) As String
            Dim Done As Boolean = False
            Dim Row As Integer = 2
            Dim LvItem As New ListViewItem

            Do
                Dim temp As String = xlSheet.Cells(Row, 1).Value
                If temp = "" And Row = MaxRow + 1 Then 'Every value has been added
                    Done = True
                ElseIf temp <> "" Then 'There are still more values to add
                    C(1) = xlSheet.Cells(Row, 1).value
                    C(2) = xlSheet.Cells(Row, 2).value
                    C(3) = xlSheet.Cells(Row, 3).value
                    C(4) = xlSheet.Cells(Row, 4).value

                    LvItem = Me.LstView_DataBase.Items.Add(C(1))
                    LvItem.SubItems.AddRange(New String() {C(2), C(3), C(4)})
                End If

                Row += 1
            Loop Until Done = True
        Catch ex As Exception
            MsgBox("Failed to load DataBase. Boopies will not be available for this execution!!!!")
        End Try
    End Sub
#End Region

#Region "Main Stream"
    Sub StartParam()

        Dim Profanity As New List(Of String)
        LoadProfanity(Profanity)
        Dim ProfanityCount As Int64 = 0

        Dim ChatAddition As String

        Dim MessageList As New List(Of String)
        Dim TempWord As String

        Dim LineCount As Int64 = 0

        Dim ProfanityFound As Boolean

        While True

            Try
                CommandPrefix = My.Settings.CommandPrefix()

                Dim message As String = readMessage()
                Dim Lmessage As String = LCase(message)

                Dim MessageSender As String
                Dim ParsedMessage As String

                Dim UserCount As Integer = 1
                Dim MessageCount As Integer = 1
                Dim validMessage1, ValidMessage2 As Boolean

                If LineCount > 9 Then
                    For Each letter As Char In Lmessage
                        If letter = "!" Then
                            validMessage1 = True
                            Exit For 'This is used to Parse the message and user. Can't be asked to use RegEx
                        Else 'Perhaps change to RegEx at a later date.
                            UserCount += 1
                        End If
                        validMessage1 = False
                    Next

                    For Each letter As Char In Lmessage
                        If letter = ":" And MessageCount <> 1 Then
                            ValidMessage2 = True
                            Exit For
                        Else
                            MessageCount += 1
                        End If
                        ValidMessage2 = False
                    Next

                    If validMessage1 = True And ValidMessage2 = True Then
                        MessageSender = Mid(Lmessage, 2, UserCount - 2)
                        ParsedMessage = Mid(message, MessageCount + 1, Len(message) - MessageCount)
                    End If

                    AddtoList(message)
                    ChatAddition = MessageSender & ": " & ParsedMessage
                    AddToChat(ChatAddition)

                    ParsedMessage = LCase(ParsedMessage)

                    ProfanityFound = False

                    ParsedMessage = ParsedMessage + " "
                    TempWord = ""
                    MessageList.Clear()

                    Dim CurrentLetter As Char

                    For i = 1 To Len(ParsedMessage) + 1
                        CurrentLetter = Mid(ParsedMessage, i, 1)
                        If CurrentLetter = " " Then
                            MessageList.Add(LCase(TempWord))
                            TempWord = ""
                        Else
                            TempWord = TempWord + CurrentLetter
                        End If
                    Next

                    If message = "PING :tmi.twitch.tv" Then
                        PongRequest() 'Verify bot it still active on Twitch
                    Else

                        If CheckForProfanity(Profanity, ParsedMessage) Then
                            ProfanityFound = True
                            ProfanityCount += 1
                            If ProfanityCount = 10 Then
                                sendChatMessage("Stream has reached the goal of 12 yr old who has just discoverd swearing. This stream is no longer PG", BotName)
                            ElseIf ProfanityCount = 50 Then
                                sendChatMessage("Stream has reached the goal of 50 bad words. This stream should have a maturity rating", BotName)
                            ElseIf ProfanityCount = 100 Then
                                sendChatMessage("Stream has reached the goal of 100 bad words. Kappa This stream should be TOS banned", BotName)
                            ElseIf ProfanityCount = 500 Then
                                sendChatMessage("I have no words for this. 500 swear words.... Say goodbye to your account, your getting TOS banned", BotName)
                            End If

                            If MessageList(0) <> CommandPrefix + "echo" Then
                                sendChatMessage("@" & MessageSender & ", Watch your profanity!!! Stream profanity count is: " & ProfanityCount, BotName)
                            End If
                        End If

                        If ParsedMessage(0) = CommandPrefix Then
                            Commands(MessageList, ParsedMessage, MessageSender, ProfanityFound, ProfanityCount)
                        End If

                        If MessageList.Count >= 2 Then
                            If LCase(MessageList(0)) = LCase(BotHandle) Or LCase(MessageList(1)) = LCase(BotHandle) Then
                                RespondToHello(MessageSender, MessageList)
                            End If
                        End If 'Respond to Hello
                    End If
                End If
            Catch ex As Exception
            End Try

            LineCount += 1
        End While
    End Sub
#End Region

#Region "Commands"

    Public Sub Commands(MessageList As List(Of String), ParsedMessage As String, MessageSender As String, ProfanityFound As Boolean, ByVal ProfanityCount As Integer) 'Checks what command after the prefix

        Dim CommandPrefix As String = Preferences.GetPrefix()

        If ParsedMessage(0) = CommandPrefix Then

            If MessageList(0) = CommandPrefix + "roulette" And MessageList.Count > 1 Then '!Roulette Game
                If IsNumeric(MessageList(1)) Then
                    If Convert.ToInt64(MessageList(1)) <= GetUserBoopies(MessageSender, "") Then
                        Roulette(MessageSender, MessageList(1))
                    Else
                        sendChatMessage("@" & MessageSender & ", you don't have enough boopies for that bet.", BotName)
                    End If
                ElseIf LCase(MessageList(1)) = "all" Then
                    If GetUserBoopies(MessageSender, "") <> 0 Then
                        Roulette(MessageSender, GetUserBoopies(MessageSender, ""))
                    Else
                        sendChatMessage("@" & MessageSender & ", please send a numerical value for your bet ammount", BotName)
                    End If
                End If
            End If

            If MessageList(0) = CommandPrefix + "garlicbread" And MessageList.Count > 1 Then '!Roulette Game
                If IsNumeric(MessageList(1)) Then
                    If Convert.ToInt64(MessageList(1)) <= GetUserBoopies(MessageSender, "") Then
                        GarlicBread(MessageSender, MessageList(1))
                    Else
                        sendChatMessage("@" & MessageSender & ", you don't have enough boopies for that bet.", BotName)
                    End If
                ElseIf LCase(MessageList(1)) = "all" Then
                    If GetUserBoopies(MessageSender, "") <> 0 Then
                        GarlicBread(MessageSender, GetUserBoopies(MessageSender, ""))
                    Else
                        sendChatMessage("@" & MessageSender & ", please send a numerical value for your bet ammount", BotName)
                    End If
                End If
            End If

            If MessageList(0) = CommandPrefix + "echo" Then '!Echo debug command
                EchoFunction(MessageSender, MessageList, ProfanityFound, ProfanityCount)
            End If

            If MessageList(0) = CommandPrefix + "discord" Then '!Discord Command
                Discord(MessageSender)
            End If

            If MessageList(0) = CommandPrefix + "rank" Then 'RL Rank Command
                RLRank(MessageSender)
            End If

            If MessageList(0) = CommandPrefix + "uptime" Then '!UpTime Command
                Dim UpTime As String = GetUpTime()
                sendChatMessage("@" & MessageSender & ", total uptime is: " & UpTime, BotName)
            End If

            If MessageList(0) = CommandPrefix + "boopies" Then '!Boopies Command
                If MessageList.Count >= 2 Then
                    If MessageList(1)(0) = "@" Then
                        Dim user As String = Mid(MessageList(1), 2, Len(MessageList(1)))
                        sendChatMessage("@" & MessageSender & ", " & MessageList(1) & " has: " & GetUserBoopies(user, "") & " Boopies!", BotName)
                    Else
                        sendChatMessage("@" & MessageSender & ", you have: " & GetUserBoopies(MessageSender, "") & " boopies!", BotName)
                    End If
                Else
                    sendChatMessage("@" & MessageSender & ", you have: " & GetUserBoopies(MessageSender, "") & " boopies!", BotName)
                End If
            End If

            If MessageList(0) = CommandPrefix + "commands" Then
                Commands(MessageSender)
            End If

            If MessageList(0) = CommandPrefix + "transferpoints" And MessageList.Count > 2 Then
                TransferPoints(MessageSender, MessageList(1), MessageList(2))
            End If

            If MessageList(0) = CommandPrefix + "addpoints" And MessageList.Count > 2 Then
                If IsNumeric(MessageList(1)) = False And IsNumeric(MessageList(2)) = True Then
                    AddPoints(MessageSender, MessageList(1), MessageList(2))
                Else
                    sendChatMessage("Please enter the command in the format addpoints <User> <Points>", BotName)
                End If
            End If
        End If

    End Sub

    Public Sub Roulette(user As String, value As String)

        Dim random As New Random
        Dim odds, multiplier As Integer

        If IsSub(user) = True Then
            odds = Preferences.GetRouletteOdds(True) 'Change to GET when added
            multiplier = Preferences.GetRoulettePayout(True)
        Else
            odds = Preferences.GetRouletteOdds(False)
            multiplier = Preferences.GetRoulettePayout(False)
        End If

        Dim Number As Integer = random.Next(0, 101)

        If Number < odds Then
            Dim Winnings As Int64 = value * multiplier
            sendChatMessage("@" & user & " bet: " & value & " boopies, and won! Netting them: " & Winnings & " boopies. They now have: " & GetUserBoopies(user, "") + Winnings, BotName)
            UpdateDatabase(user, Winnings, True, "")
        Else
            sendChatMessage("@" & user & " bet: " & value & " boopies, and lost! They now have: " & GetUserBoopies(user, "") - value, BotName)
            UpdateDatabase(user, value, False, "")
        End If

        UpdateListView("")

    End Sub

    Public Sub GarlicBread(User As String, Value As String)

        Dim random As New Random
        Dim odds, multiplier As Integer
        Dim Number As Integer = random.Next(0, 101)

        If IsSub(User) = True Then
            odds = Preferences.GetGarlicBreadOdds(True) 'Change to GET when added
            multiplier = Preferences.GetGarlicBreadPayout(True)
        Else
            odds = Preferences.GetGarlicBreadOdds(False)
            multiplier = Preferences.GetGarlicBreadPayout(False)
        End If

        If Number < odds Then
            Dim Winnings As Int64 = Value * multiplier
            sendChatMessage("@" & User & " bet: " & Value & " boopies, and got a good piece of Garlic Bread! Netting them: " & Winnings & " boopies. They now have: " & GetUserBoopies(User, "") + Winnings, BotName)
            UpdateDatabase(User, Winnings, True, "")
        Else
            sendChatMessage("@" & User & " bet: " & Value & " boopies, and got a mouldy piece of Garlic Bread! They now have: " & GetUserBoopies(User, "") - Value, BotName)
            UpdateDatabase(User, Value, False, "")
        End If

        UpdateListView("")

    End Sub

    Public Sub RespondToHello(user As String, MessageList As List(Of String))
        Dim Valid As Boolean = False
        Dim random As New Random
        Dim Value As Integer = random.Next(0, 5)

        For i = 1 To 2
            If LCase(MessageList(1)).Contains("hi") Or LCase(MessageList(0)).Contains("hi") Then
                Valid = True
            ElseIf LCase(MessageList(1)).Contains("hello") Or LCase(MessageList(0)).Contains("hello") Then
                Valid = True
            ElseIf LCase(MessageList(1)).Contains("yo") Or LCase(MessageList(0)).Contains("yo") Then
                Valid = True
            ElseIf LCase(MessageList(1)).Contains("wassup") Or LCase(MessageList(0)).Contains("wassup") Then
                Valid = True
            ElseIf LCase(MessageList(1)).Contains("sah") Or LCase(MessageList(0)).Contains("sah") Then
                Valid = True
            ElseIf LCase(MessageList(1)).Contains("hey") Or LCase(MessageList(0)).Contains("hey") Then
                Valid = True
            End If
        Next

        If Valid = True Then
            Select Case Value
                Case 0
                    sendChatMessage("@" & user & ", Sup!", BotName)
                Case 1
                    sendChatMessage("@" & user & ", Yo!", BotName)
                Case 2
                    sendChatMessage("@" & user & ", Hey!", BotName)
                Case 3
                    sendChatMessage("@" & user & ", Hi!", BotName)
                Case 4
                    sendChatMessage("@" & user & ", Howdy!", BotName)
            End Select

        End If
    End Sub

    Public Sub EchoFunction(User As String, MessageList As List(Of String), profanityFound As Boolean, ByVal ProfanityCount As Integer)

        Dim TempMessage As String = ""

        If profanityFound = False Then

            If MessageList.Count > 1 Then
                For i = 1 To MessageList.Count - 1
                    TempMessage = TempMessage + MessageList(i) + " "
                Next
                Me.sendChatMessage("@" & User & ": " & TempMessage, BotName)
            Else
                Me.sendChatMessage("@" & User & ", nothing to echo!", BotName)
            End If
        Else
            sendChatMessage("@" & User & ", Im not repeating that Profanity!! Profanity Count is: " & ProfanityCount, BotName)
            ProfanityCount += 1
        End If

    End Sub

    Public Function GetUpTime()

        Dim wRequest As WebRequest
        Dim wResponce As WebResponse
        Dim Reader As StreamReader

        Dim UpTime As String = ""

        Dim Address As String = "https://beta.decapi.me/twitch/uptime/" + Channel

        wRequest = WebRequest.Create(Address)
        wResponce = wRequest.GetResponse

        Reader = New StreamReader(wResponce.GetResponseStream)

        UpTime = Reader.ReadToEnd
        Reader.Close()

        Return UpTime

        'https://beta.decapi.me/twitch/uptime/mrboopy

    End Function

    Public Sub Discord(User)

        sendChatMessage("@" & User & ", join my discord at: " & Preferences.GetDiscord() & "!", BotName)

    End Sub

    Public Sub RLRank(User)

        sendChatMessage("@" & User & ", " & Preferences.GetRLRank, BotName)

    End Sub

    Private Sub TopPoints(User)



    End Sub

    Private Sub TransferPoints(Transferer As String, Reciever As String, Value As String)

        If IsNumeric(Value) = False Then
            sendChatMessage("@" & Transferer & ", please enter a valid Boopie value to transfer", BotName)
        Else 'worked out value is Numerical
            Dim TransferAmount As Integer = Convert.ToInt64(Value)

            If GetViewerDBID(Reciever, "") <> 0 Then
                If GetUserBoopies(Transferer, "") < TransferAmount Then 'Check to see if User has enough boopies for the transfer
                    sendChatMessage("@" & Transferer & ", you do not have enough boopies for that!! You have " & GetUserBoopies(Transferer, "") & " boopies", BotName)
                Else 'Main Transfer Code, needs doing once the database is in place

                    UpdateDatabase(Transferer, Value, False, "")
                    UpdateDatabase(Reciever, Value, True, "")

                    sendChatMessage("@" & Transferer & ", has given @" & Reciever & " " & Value & " Boopies. What a kind sod!", BotName)
                End If
            Else
                sendChatMessage("@" & Transferer & ", " & Reciever & " does not exist. try again later!", BotName)
            End If
        End If

            If GetUserBoopies(Transferer, "") < Value Then
            sendChatMessage("@" & Transferer & ", you do not have enough boopies for that!! You have " & GetUserBoopies(Transferer, "") & " boopies", BotName)

        Else 'Main Transfer Code
            If IsNumeric(Value) = False Then

            End If
        End If

        UpdateListView("")
    End Sub

    Private Sub AddPoints(User As String, reciever As String, value As String)
        Dim SuperMod As Boolean = IsSuperMod(User)

        If User = Channel Or SuperMod = True Then
            If IsNumeric(value) Then
                value = Convert.ToInt64(value)
                UpdateDatabase(reciever, value, True, "")
                sendChatMessage("@" & User & ", you have given @" & reciever & ", " & value & " Boopies! They now have " & GetUserBoopies(reciever, ""), BotName)
            Else
                sendChatMessage("@" & User & ", please enter a numerical value", BotName)
            End If
        Else
            sendChatMessage("@" & User & ", you don't have permission to perform this command. Only the broadcaster can!", BotName)
        End If

        UpdateListView("")

    End Sub

    Private Sub Commands(User As String)

        sendChatMessage("@" & User & ", Please go to this GitHub repo for an extensive list of all possible commands: https://github.com/LangridgeDaniel/BoopieBot-Commands", BotName)

    End Sub

#End Region

#Region "Back End shit"
    Private Sub PongRequest()
        'sendChatMessage("Pong!!!", "")
        sendIrcMessage("PONG :tmi.twitch.tv", "")
    End Sub

    Function CheckForProfanity(Profanity, Message)

        For Each word In Profanity
            If Message.Contains(word) Then
                Return True
                Exit For
            End If
        Next

        Return False

    End Function

    Private Function GetViewersList()

        'https://tmi.twitch.tv/group/user/mrboopy/chatters

        Dim ViewerListTemp As New List(Of String)
        Dim ViewerList As New List(Of String)
        Dim ModsList As New List(Of String)

        Dim wRequest As WebRequest
        Dim wResponce As WebResponse
        Dim Reader As StreamReader

        Dim File As String = ""

        Dim Address As String = "https://tmi.twitch.tv/group/user/" + Channel + "/chatters"

        wRequest = WebRequest.Create(Address)
        wResponce = wRequest.GetResponse

        Reader = New StreamReader(wResponce.GetResponseStream)

        File = Reader.ReadToEnd
        Reader.Close()

        Dim TempWord As String = ""

        For Each letter In File
            If letter = vbLf Then
                ViewerListTemp.Add(TempWord)
                TempWord = ""
            ElseIf letter = Chr(34) Or letter = " " Or letter = "[" Or letter = "]" Or letter = "," Or letter = "}" Then 'Testing for " (Chr(34) is the ascii for ")
                'Do nothing
            Else
                TempWord = TempWord + letter
            End If
        Next

        Dim Viewers, Mods As Boolean
        Dim skip As Boolean

        For Each entry In ViewerListTemp
            If entry = "moderators:" Then
                Mods = True
                skip = True
            End If

            If entry = "viewers:" Then
                Viewers = True
                skip = True
            End If

            If entry = "" Then
                Mods = False
                Viewers = False
            End If

            If Mods = True And skip = False Then
                ModsList.Add(entry)
            End If
            If Viewers = True And skip = False Then
                ViewerList.Add(entry)
            End If

            skip = False
        Next

        ViewerListTemp.Clear()

        ViewerListTemp.Add("MODS:")
        For Each i In ModsList
            ViewerListTemp.Add(i)
        Next

        ViewerListTemp.Add("VIEWERS:")
        For Each i In ViewerList
            ViewerListTemp.Add(i)
        Next

        ListView1.Items.Clear()
        For Each i In ViewerListTemp
            ListView1.Items.Add(ViewerListTemp(1))
        Next

        Return ViewerListTemp

    End Function

    Private Sub IncreaseViewerBoopies() 'Needs finishing when the database has been made!!!!!!!!

        Dim viewerlist As New List(Of String)
        Dim TempList As New List(Of Integer)
        Dim ViewerIncrease As Integer = Preferences.GetBoopiePayout(False)
        Dim SubIncrease As Integer = Preferences.GetBoopiePayout(True)

        LastBoopieIncrease = DateTime.Now
        viewerlist = GetViewersList()

        sendChatMessage("Increasing", BotName)

        For Each i In viewerlist
            If i = "VIEWERS:" Or i = "MODS:" Then
                'do nothing
            Else
                If GetHashLocation(i) <> 0 Then
                    If DBIsSub(i) = True Then
                        UpdateDatabase(i, Preferences.GetBoopiePayout(True), True, "System")
                    Else
                        UpdateDatabase(i, Preferences.GetBoopiePayout(False), True, "System")
                    End If
                Else 'User not in DB
                    UpdateDatabase(i, Preferences.GetBoopiePayout(False), True, "")
                End If
            End If
        Next

        UpdateListView("")
        sendChatMessage("Increased viewers boopies by: " & ViewerIncrease & ", and sub boopies by: " & SubIncrease & ", next increase in: " & Preferences.GetBoopieTimeDelay & " Minutes!", BotName)

    End Sub
#End Region

#Region "Database functions"

    Private Function CalculateUserHash(UserName As String)

        Dim UserHash As Int64 = 0
        Dim RunningTotal As Int64 = 0

        For Each letter In UserName
            RunningTotal += Asc(LCase(letter)) - 60
        Next

        UserHash = RunningTotal

        Return UserHash

    End Function

    Public Function GetHashLocation(User As String)

        Dim UserHash As Integer = CalculateUserHash(User)
        Dim count As Integer = 0
        Dim BlankFound As Boolean = False

        Do
            If UserHash <> 0 Then
                If xlSheet.Cells(UserHash + count, 2).value = User Then
                    Return UserHash + count
                ElseIf xlSheet.Cells(UserHash + count, 2).value <> "" Then
                    count += 1
                ElseIf xlSheet.Cells(UserHash + count, 2).value = "" Then
                    Return 0
                End If
            End If
        Loop

    End Function

    Private Function UpdateDatabase(User As String, Change As Int64, Win As Boolean, Which As String)

        Dim HashLocation As Integer = GetHashLocation(User)

        If HashLocation <> 0 Then
            If Win = True Then
                xlSheet.Cells(HashLocation, 3).value = xlSheet.Cells(HashLocation, 3).value + Change
                Return True
            Else
                xlSheet.Cells(HashLocation, 3).value = xlSheet.Cells(HashLocation, 3).value - Change
                Return True
            End If
        End If

        If Which = "" Then
            AddViewer(User)
            UpdateDatabase(User, Change, Win, "")
        End If

    End Function

    Private Function GetUserBoopies(user As String, Which As String) 'Need sorting

        Dim HashLocation As Integer = GetHashLocation(user)

        If HashLocation <> 0 Then
            Return xlSheet.Cells(HashLocation, 3).value
        End If

        If Which = "" Then
            AddViewer(user)
            GetUserBoopies(user, "")
        End If

        Return 0

    End Function

    Private Function IsSub(User As String) 'Needs sorting

        Dim UserHash As Integer = CalculateUserHash(User)

        Return False

        Dim wRequest As WebRequest
        Dim wResponce As WebResponse
        Dim Reader As StreamReader

        Dim Text As String = ""

        Dim Address As String = "https://decapi.me/twitch/subage/" + Channel + "/" + User

        wRequest = WebRequest.Create(Address)
        wResponce = wRequest.GetResponse

        Reader = New StreamReader(wResponce.GetResponseStream)

        Text = Reader.ReadToEnd
        Reader.Close()

        For Each letter In Text
            If letter = "," Then
                Return True
            Else
                Return False
            End If
        Next

        'https://decapi.me/twitch/subage/:channel/:user

    End Function

    Private Sub UpdateDBSub(User As String, Update As String)

        Dim HashLocation As Integer = GetHashLocation(User)

        If HashLocation <> 0 Then
            If Update = True Then
                xlSheet.Cells(HashLocation, 4).value = "True"
            Else
                xlSheet.Cells(HashLocation, 4).value = "False"
            End If
        End If

    End Sub

    Private Function DBIsSub(User As String)

        Dim HashLocation As Integer = GetHashLocation(User)

        If HashLocation <> 0 Then
            If xlSheet.Cells(HashLocation, 4).value = "True" Then
                Return True
            Else
                Return False
            End If
        End If
        Return False

    End Function

    Private Sub AddViewer(Viewer As String)

        Dim HashLocation As Integer = CalculateUserHash(Viewer)
        Dim DBCount As Integer = xlSheet.Cells(2, 7).value + 1
        xlSheet.Cells(2, 7).value = DBCount

        xlSheet.Cells(HashLocation, 1).value = DBCount
        xlSheet.Cells(HashLocation, 2).value = Viewer
        xlSheet.Cells(HashLocation, 3).value = 0
        xlSheet.Cells(HashLocation, 5).value = False

        If IsSub(Viewer) = True Then
            xlSheet.Cells(HashLocation, 4).value = True
        Else
            xlSheet.Cells(HashLocation, 4).value = False
        End If

        If HashLocation > xlSheet.Cells(2, 6).Value Then
            xlSheet.Cells(2, 6).value = HashLocation
        End If

    End Sub

    Public Function GetViewerDBID(Viewer As String, Which As String)

        Dim HashLocation As Integer = GetHashLocation(Viewer)
        Dim ViewerInDB As Boolean = False
        Dim ViewerID As Integer

        Dim DBCount As Integer = xlSheet.Cells(2, 6).value + 1

        If HashLocation <> 0 Then
            ViewerID = xlSheet.Cells(HashLocation, 1).value
            ViewerInDB = True
        End If


        If ViewerInDB = True Then
            Return ViewerID
        Else
            If Which = "" Then
                AddViewer(Viewer)
            End If
            Return 0
        End If

    End Function

    Public Sub ToggleSuperMod(Viewer As String, HashLocation As Integer, Which As String)

        Dim Value As String = xlSheet.Cells(HashLocation, 5).value

        If Which = "Add" Then
            If Value = "False" Then
                sendChatMessage("@" & Viewer & " has been added as a Super Mod!!", BotName)
                xlSheet.Cells(HashLocation, 5).value = "True"
            Else
                sendChatMessage("@" & Viewer & " is already a Super Mod!!", BotName)
            End If
        ElseIf Which = "Remove" Then
            If Value = "True" Then
                sendChatMessage("@" & Viewer & " has been removed as a Super Mod!!", BotName)
                xlSheet.Cells(HashLocation, 5).value = "False"
            Else
                sendChatMessage("@" & Viewer & " is already not a Super Mod!!", BotName)
            End If
        End If
    End Sub

    Public Function IsSuperMod(Viewer)

        Dim HashLocation As Integer = GetHashLocation(Viewer)

        If HashLocation = 0 Then
            Return False

        Else
            If xlSheet.Cells(HashLocation, 5).value = "True" Then
                Return True
            Else
                Return False
            End If
        End If

    End Function
#End Region

#Region "Chat and List function"
    Public Sub AddtoList(s As String)
        If TwitchData1.InvokeRequired Then
            Dim sd As New stringDelegate(AddressOf AddtoList)
            Me.Invoke(sd, New Object() {s})
        Else
            TwitchData1.Items.Add(s)
            TwitchData1.SelectedIndex = TwitchData1.Items.Count - 1
        End If
    End Sub
    Private Delegate Sub stringDelegate(s As String)
    Public Sub AddToChat(s)
        If LstBox_Chat_1.InvokeRequired Then
            Dim sd As New stringDelegate(AddressOf AddToChat)
            Me.Invoke(sd, New Object() {s})
        Else
            LstBox_Chat_1.Items.Add(s)
            LstBox_Chat_1.SelectedIndex = LstBox_Chat_1.Items.Count - 1
            'LstBox_Chat_1.ClearSelected()

            LstBox_Chat.Items.Add(LstBox_Chat_1.SelectedItem)
            LstBox_Chat.SelectedIndex = LstBox_Chat.Items.Count - 1
            LstBox_Chat.ClearSelected()
        End If
    End Sub
#End Region

#Region "Timer Functions"
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        'Dim TempTime As TimeSpan = DateTime.Now - LastBoopieIncrease

        'MsgBox("Last Increase: " & LastBoopieIncrease & ", Current Time: " & DateTime.Now & ", Time difference in Mins: " & Math.Sign(TempTime.TotalMinutes) * Math.Floor(Math.Abs(TempTime.TotalMinutes) * 100) / 100.0)

        'If Convert.ToInt16(Math.Sign(TempTime.TotalMinutes) * Math.Floor(Math.Abs(TempTime.TotalMinutes) * 100) / 100.0) = Convert.ToInt16(Preferences.GetBoopieTimeDelay) Then
        '    IncreaseViewerBoopies()
        'End If

        IncreaseViewerBoopies()

    End Sub
#End Region

#Region "Button stuff"
    Private Sub Btn_Disconnect_Click(sender As Object, e As EventArgs) Handles Btn_Disconnect.Click
        Dim result As Integer = MessageBox.Show("Are you sure you want to DC the bot?", "", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            xlBook.Save()
            xlBook = Nothing
            xlSheet = Nothing
            xlApp.Quit()
            xlApp = Nothing

            leaveRoom(Form1.Channel)
            Me.Close()
        Else
            'Do nothing
        End If
    End Sub

    Private Sub Btn_SendMessage_Click(sender As Object, e As EventArgs) Handles Btn_SendMessage.Click
        If TxtBox_Message.Text = "" Then
            MsgBox("Please actually type a message before clicking send!!")
        Else
            sendChatMessage(TxtBox_Message.Text, BotName)
            TxtBox_Message.Clear()
        End If
    End Sub

    Private Sub Btn_UpdateSettings_Click(sender As Object, e As EventArgs) Handles Btn_UpdateSettings.Click
        Me.Enabled = False
        Frm_ChangePreferences.Show()
    End Sub

#End Region


#Region "IRC Variables"
    Public username As String 'IRC Varibles
    Private channelc As String

    Private tcpClient As TcpClient
    Private inputStream As StreamReader
    Private outputStream As StreamWriter

    Public BotName As String = Form1.BotName
    Dim ChannelName As String = Form1.Channel
#End Region
#Region "IRC Shit"
    Sub IrcClient(ip As String, port As Integer, username As String, password As String)

        tcpClient = New TcpClient(ip, port)
        inputStream = New StreamReader(tcpClient.GetStream())
        outputStream = New StreamWriter(tcpClient.GetStream())

        outputStream.WriteLine("PASS " + password)
        outputStream.WriteLine("NICK " + username)
        outputStream.WriteLine("USER " + username + " 8 * :" + username)
        outputStream.Flush()

    End Sub

    Sub joinRoom(channel As String)

        channelc = channel
        outputStream.WriteLine("JOIN #" + channel)
        outputStream.Flush()

    End Sub

    Sub leaveRoom(channel As String)

        sendChatMessage(BotName & " is peacing the scene!", BotName)
        outputStream.WriteLine("PART #" + channel)

    End Sub

    Sub sendIrcMessage(Message As String, User As String)

        outputStream.WriteLine(Message)
        outputStream.Flush()

        Dim messageList As New List(Of String)
        Dim ParsedMessage As String
        Dim TempWord As String

        Dim TempMessage As String

        Dim ValidMessage1 As Boolean
        Dim Lmessage As String = LCase(Message)
        Dim MessageCount As Integer = 1

        For Each letter As Char In Message
            If letter = ":" And MessageCount <> 1 Then
                ValidMessage1 = True
                Exit For
            Else
                MessageCount += 1
            End If
            ValidMessage1 = False
        Next

        If ValidMessage1 = True Then
            ParsedMessage = Mid(Message, MessageCount + 1, Len(Message) - MessageCount)
        End If

        ParsedMessage = ParsedMessage + " "
        TempWord = ""
        messageList.Clear()

        Dim CurrentLetter As Char

        For i = 1 To Len(ParsedMessage) + 1
            CurrentLetter = Mid(ParsedMessage, i, 1)
            If CurrentLetter = " " Then
                messageList.Add(TempWord)
                TempWord = ""
            Else
                TempWord = TempWord + CurrentLetter
            End If
        Next

        If messageList(0) <> "PONG" Then

            For i = 0 To messageList.Count - 1
                TempMessage = TempMessage + messageList(i)
                TempMessage = TempMessage + " "
            Next

            TempMessage = User + ": " + TempMessage

            AddtoList(Message)
            AddToChat(TempMessage)
        Else
            AddtoList(Message)
        End If

    End Sub

    Sub sendChatMessage(message As String, User As String)

        sendIrcMessage(":" + username + "!" + username + "@" + ".tmi.twitch.tv PRIVMSG #" + channelc + " :" + message, User)

    End Sub

    Function ircConnected()

        If tcpClient.Connected Then
            'MsgBox("Connected, this form will now stay open, and the menu will open over it. DO NOT CLOSE THIS STARTING FORM UNTIL YOU WANT THE BOT TO END!!!!")
            Return True
        Else
            MsgBox("!Connected")
            Return False
        End If

    End Function

    Function readMessage()

        Dim Message As String = inputStream.ReadLine()
        Return Message

    End Function
#End Region

End Class