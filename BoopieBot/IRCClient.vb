Imports System.IO
Imports System.Net.Sockets

'This whole Module is here purely as a back up for if everything in the BoopieMainMenu fucks up
'Future reference for me. DON'T EDIT ANY OF THIS SHIT!!!!!!!!!!!!!!

Module IRCClient

    Public username As String
    Private channelc As String

    Private tcpClient As TcpClient
    Private inputStream As StreamReader
    Private outputStream As StreamWriter

    Dim BotName As String = Form1.BotName
    Dim ChannelName As String = Form1.Channel

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

        sendChatMessage("BoopiesTestBot is peacing the scene!")
        outputStream.WriteLine("PART #" + channel)

    End Sub

    Sub sendIrcMessage(message As String)

        outputStream.WriteLine(message)
        outputStream.Flush()

        Dim ParsedMessage As String

        Dim TempMessage As String

        Dim MessageCount As Integer = 0
        Dim ValidMessage As Boolean = False

        For Each letter As Char In LCase(message)
            If letter = "#" Then
                ValidMessage = True
                Exit For
            Else
                MessageCount += 1
            End If
            ValidMessage = False
        Next

        If ValidMessage = True Then
            ParsedMessage = Mid(message, MessageCount + 4 + Len(ChannelName), Len(message) - MessageCount)
        End If

        BoopieBotMainMenu.AddtoList(message)
        BoopieBotMainMenu.AddToChat(ParsedMessage)

    End Sub

    Sub sendChatMessage(message As String)

        sendIrcMessage(":" + username + "!" + username + "@" + ".tmi.twitch.tv PRIVMSG #" + channelc + " :" + message)

    End Sub

    Function ircConnected()

        If tcpClient.Connected Then
            MsgBox("Connected, this form will now stay open, and the menu will open over it. DO NOT CLOSE THIS STARTING FORM UNTIL YOU WANT THE BOT TO END!!!!")
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

End Module