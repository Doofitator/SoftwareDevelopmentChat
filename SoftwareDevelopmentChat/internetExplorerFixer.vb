Module internetExplorerFixer
    Function changeBrowserIEVersion()

        ' // copied from https://social.msdn.microsoft.com/Forums/vstudio/en-US/bc469203-cb4a-4477-a8b3-996121b424e6/webbrowser?forum=vbgeneral. Fixes webbrowser rendering issues
        Dim AppName As String = My.Application.Info.AssemblyName

        Dim VersionCode As Integer
        Dim Version As String = ""
        Dim ieVersion As Object = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Internet Explorer").GetValue("svcUpdateVersion")
        If ieVersion Is Nothing Then
            ieVersion = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Internet Explorer").GetValue("Version")
        End If
        If ieVersion IsNot Nothing Then
            Version = ieVersion.ToString.Substring(0, ieVersion.ToString.IndexOf("."c))
            Select Case Version
                Case "7"
                    VersionCode = 7000
                Case "8"
                    VersionCode = 8888
                Case "9"
                    VersionCode = 9999
                Case "10"
                    VersionCode = 10001
                Case Else
                    If CInt(Version) >= 11 Then
                        VersionCode = 11001
                    Else
                        Throw New Exception("IE Version not supported")
                    End If
            End Select
        Else
            Throw New Exception("Registry error")
        End If
        'Check if the right emulation is set
        'if not, Set Emulation to highest level possible on the user machine
        Dim Root As String = "HKEY_CURRENT_USER\"
        Dim Key As String = "Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"
        Dim CurrentSetting As String = CStr(Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Key).GetValue(AppName & ".exe"))
        If CurrentSetting Is Nothing OrElse CInt(CurrentSetting) <> VersionCode Then
            Microsoft.Win32.Registry.SetValue(Root & Key, AppName & ".exe", VersionCode)
            Microsoft.Win32.Registry.SetValue(Root & Key, AppName & ".vshost.exe", VersionCode)
        End If
    End Function
End Module
