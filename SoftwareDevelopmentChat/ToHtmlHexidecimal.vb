Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

'//copied this from https://stackoverflow.com/questions/982028/convert-net-color-objects-to-hex-codes-and-back & converted to VB from C# even though I only need toHtmlHexadeciaml

Module ToHtmlHexidecimal
    <Extension()>
    Function ToHtmlHexadecimal(ByVal color As Color) As String
        Return String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B)
    End Function

    Public htmlColorRegex As Regex = New Regex("^#((?'R'[0-9a-f]{2})(?'G'[0-9a-f]{2})(?'B'[0-9a-f]{2}))" & "|((?'R'[0-9a-f])(?'G'[0-9a-f])(?'B'[0-9a-f]))$", RegexOptions.Compiled Or RegexOptions.IgnoreCase)

    Function FromHtmlHexadecimal(ByVal colorString As String) As Color
        If colorString Is Nothing Then
            Throw New ArgumentNullException("colorString")
        End If

        Dim match = htmlColorRegex.Match(colorString)

        If Not match.Success Then
            Dim msg = "The string ""{0}"" doesn't represent"
            msg += "a valid HTML hexadecimal color"
            msg = String.Format(msg, colorString)
            Throw New ArgumentException(msg, "colorString")
        End If

        Return Color.FromArgb(ColorComponentToValue(match.Groups("R").Value), ColorComponentToValue(match.Groups("G").Value), ColorComponentToValue(match.Groups("B").Value))
    End Function

    Private Function ColorComponentToValue(ByVal component As String) As Integer
        Debug.Assert(component IsNot Nothing)
        Debug.Assert(component.Length > 0)
        Debug.Assert(component.Length <= 2)

        If component.Length = 1 Then
            component += component
        End If

        Return Integer.Parse(component, System.Globalization.NumberStyles.HexNumber)
    End Function
End Module
