Public Class frm_groupChatSelector
    Private Sub btn_new_Click(sender As Object, e As EventArgs) Handles btn_new.Click
        Dim userFields As New List(Of TextBox) 'new list
        For Each control In Me.Controls        'for each control on form
            If TypeOf (control) Is TextBox Then 'if control is a textbox
                userFields.Add(control)         'add it to my list
            End If
        Next

        If userFields.Count < 5 Then            'so long as there isn't more than 5 textboxes on the form,
            Dim txt As New TextBox              'make new textbox
            txt.Top = (29 * userFields.Count) + 40  'set top
            txt.Left = 16                       'set left
            txt.Width = 192                     'set width
            Me.Controls.Add(txt)                'add to form
            Me.Height += txt.Height + 10        'increase form's height by textbox's height & padding
            btn_cancel.Top += txt.Height + 10   '|
            btn_new.Top += txt.Height + 10      '|move buttons by same amount
            btn_ok.Top += txt.Height + 10       '|
            AddHandler txt.TextChanged, AddressOf txt_grpMember_textchanged 'make it run textchanged when its text is changed
        End If

        btn_ok.Enabled = False                    'just in case it was enabled before - we don't want it enabled now because there's a new empty box
    End Sub

    Private Sub txt_grpMember_textchanged(sender As Object, e As EventArgs) Handles txt_grpMember1.TextChanged, txt_grpMember2.TextChanged

        Dim textValids As New List(Of Boolean)
        Dim textValues As New List(Of String)




        For Each control In Me.Controls        'for each control on form
            If TypeOf (control) Is TextBox Then 'if control is a textbox
                Dim text As TextBox = CType(control, TextBox)   'convert control to a textbox (legit just for intellisense)

                'if textbox contains text and isn't your username and hasn't already been typed in and is a real user then put a true in textvalids else false
                If Not text.Text = "" And Not UppercaseFirstLetter(text.Text) = frm_main.txt_userName.Text And Not textValues.Contains(UppercaseFirstLetter(text.Text)) And Not readUserID(text.Text.ToLower) = 0 Then
                    textValids.Add(True)
                    text.ForeColor = Color.Black
                Else
                    textValids.Add(False)
                    text.ForeColor = Color.Red
                End If
                textValues.Add(UppercaseFirstLetter(text.Text))
            End If
        Next

        btn_ok.Enabled = checkValids(textValids) 'check textvalids to make sure there's no falses. If so, make ok enabled.

    End Sub

    Private Function checkValids(arr As List(Of Boolean)) As Boolean
        For i As Integer = 1 To arr.Count - 1
            If arr(i) = False Then Return False
            If arr(i) <> arr(0) Then Return False
        Next
        Return True
    End Function

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
        Me.DialogResult = DialogResult.OK   'let whoever called this form know that it's ok       
        Me.Close()                          'close
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Me.DialogResult = DialogResult.Cancel   'let whoever called this form know its cancelled
        Me.Close()                              'close
    End Sub
End Class