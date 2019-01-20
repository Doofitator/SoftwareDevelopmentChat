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
    End Sub

    Private Sub txt_grpMember_textchanged(sender As Object, e As EventArgs) Handles txt_grpMember1.TextChanged, txt_grpMember1.TextChanged
        Dim txt As TextBox = CType(sender, TextBox)
        If readUserID(UppercaseFirstLetter(txt.Text)) = 0 Then
            txt.ForeColor = Color.Red
        Else
            txt.ForeColor = Color.Black
        End If
    End Sub

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class