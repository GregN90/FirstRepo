Public Class frmJob

    Dim mobjJob As clsJob
    Dim mblnEdited As Boolean

    Private Sub cmdNavSource_Click(sender As Object, e As EventArgs) Handles cmdNavSource.Click
        With dirFolderBrowser
            .SelectedPath = txtSourceFolder.Text
            .ShowDialog()
            txtSourceFolder.Text = .SelectedPath()
        End With
    End Sub

    Private Sub cmdDestinationNav_Click(sender As Object, e As EventArgs) Handles cmdDestinationNav.Click
        With dirFolderBrowser
            .SelectedPath = txtDestinationFolder.Text
            .ShowDialog()
            txtDestinationFolder.Text = .SelectedPath()
        End With
    End Sub

    Public Sub ShowJob(ByRef objJob As clsJob)

        mobjJob = objJob

        Initialise()

        With objJob
            txtName.Text = .Name

            chkEnabled.Checked = .Enabled

            txtDestinationFolder.Text = .DestinationFolder
            txtSourceFolder.Text = .SourceFolder

            cboFrequency.SelectedIndex = .Frequency - 1

            cboDay.SelectedIndex = .DayInMonth
            cboHours.SelectedIndex = .Hours
            cboMinutes.SelectedIndex = .Minutes

            optMon.Checked = .Day(1)
            optTue.Checked = .Day(2)
            optWed.Checked = .Day(3)
            optThu.Checked = .Day(4)
            optFri.Checked = .Day(5)
            optSat.Checked = .Day(6)
            optSun.Checked = .Day(7)

            chkMon.Checked = .TickedDay(1)
            chkTue.Checked = .TickedDay(2)
            chkWed.Checked = .TickedDay(3)
            chkThu.Checked = .TickedDay(4)
            chkFri.Checked = .TickedDay(5)
            chkSat.Checked = .TickedDay(6)
            chkSun.Checked = .TickedDay(7)
        End With

        cmdSave.Enabled = False
        mblnEdited = False

        Me.ShowDialog()
        objJob = mobjJob

    End Sub

    Public Function GrabJob() As clsJob

        Dim lobjJob As New clsJob

        With lobjJob
            .Name = txtName.Text

            .Enabled = chkEnabled.Checked

            .DestinationFolder = Trim(txtDestinationFolder.Text)
            If Strings.Right(.DestinationFolder, 1) <> "\" Then .DestinationFolder = .DestinationFolder & "\"

            .SourceFolder = Trim(txtSourceFolder.Text)
            If Strings.Right(.SourceFolder, 1) <> "\" Then .SourceFolder = .SourceFolder & "\"

            .Frequency = cboFrequency.SelectedIndex + 1

            .DayInMonth = cboDay.SelectedIndex
            .Hours = cboHours.Text
            .Minutes = cboMinutes.Text

            .Day(1) = optMon.Checked
            .Day(2) = optTue.Checked
            .Day(3) = optWed.Checked
            .Day(4) = optThu.Checked
            .Day(5) = optFri.Checked
            .Day(6) = optSat.Checked
            .Day(7) = optSun.Checked

            .TickedDay(1) = chkMon.Checked
            .TickedDay(2) = chkTue.Checked
            .TickedDay(3) = chkWed.Checked
            .TickedDay(4) = chkThu.Checked
            .TickedDay(5) = chkFri.Checked
            .TickedDay(6) = chkSat.Checked
            .TickedDay(7) = chkSun.Checked

            .Edited = mblnEdited
            .DeriveLogFileName()
        End With

        Return lobjJob

    End Function

    Private Sub Initialise()

        Dim lintIndex As Integer

        With cboFrequency
            .Items.Clear()
            .Items.Add("Daily")
            .Items.Add("Weekly")
            .Items.Add("Monthly")
            .Text = "Daily"
        End With

        With cboDay
            .Items.Clear()
            For lintIndex = 1 To 28
                .Items.Add(lintIndex.ToString)
            Next
        End With

        cboHours.Items.Clear()
        cboMinutes.Items.Clear()

        For llngIndex = 0 To 23
            cboHours.Items.Add(Format(llngIndex, "00"))
        Next

        For llngIndex = 0 To 59
            cboMinutes.Items.Add(Format(llngIndex, "00"))
        Next

    End Sub

    Private Sub cboFrequency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFrequency.SelectedIndexChanged

        Edited()

        Select Case cboFrequency.Text
            Case "Daily"
                grpDaysChk.Visible = True
                grpDaysOpt.Visible = False
                cboDay.Visible = False

                With grpDaysChk
                    .Left = lblDays.Left + lblDays.Width + 5
                    .Top = lblDays.Top - 12
                End With

            Case "Weekly"
                grpDaysChk.Visible = False
                grpDaysOpt.Visible = True
                cboDay.Visible = False

                With grpDaysOpt
                    .Left = lblDays.Left + lblDays.Width + 5
                    .Top = lblDays.Top - 12
                End With

            Case "Monthly"
                grpDaysChk.Visible = False
                grpDaysOpt.Visible = False
                cboDay.Visible = True
        End Select
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        Dim lstrMessage As String = ""
        Dim lobjFAC As New clsFAC

        If txtName.Text = "" Then
            lstrMessage = "Job Name cannot be blank"
        ElseIf txtSourceFolder.Text = "" Then
            lstrMessage = "Source Folder cannot be blank"
        ElseIf txtDestinationFolder.Text = "" Then
            lstrMessage = "Destination Folder cannot be blank"
        End If

        If lstrMessage <> "" Then
            MsgBox(lstrMessage, vbOKOnly, "Invalid Entry")
        Else
            mobjJob = GrabJob()
            mobjJob.CalculateNextRun()
            Me.Close()
        End If

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

        If cmdSave.Enabled = True Then
            If MsgBox("Are you sure you want to cancel? All changes will be discarded.", vbYesNo, "Cancel Changes") = vbYes Then
                mobjJob = Nothing
                Me.Close()
            End If
        Else
            mobjJob = Nothing
            Me.Close()
        End If

    End Sub

    Private Sub Edited()
        mblnEdited = True
        cmdSave.Enabled = True
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Edited()
    End Sub

    Private Sub txtSourceFolder_TextChanged(sender As Object, e As EventArgs) Handles txtSourceFolder.TextChanged
        Edited()
    End Sub

    Private Sub txtDestinationFolder_TextChanged(sender As Object, e As EventArgs) Handles txtDestinationFolder.TextChanged
        Edited()
    End Sub

    Private Sub chkEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnabled.CheckedChanged
        Edited()
    End Sub

    Private Sub cboDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDay.SelectedIndexChanged
        Edited()
    End Sub

    Private Sub optMon_CheckedChanged(sender As Object, e As EventArgs) Handles optMon.CheckedChanged
        Edited()
    End Sub

    Private Sub optTue_CheckedChanged(sender As Object, e As EventArgs) Handles optTue.CheckedChanged
        Edited()
    End Sub

    Private Sub optWed_CheckedChanged(sender As Object, e As EventArgs) Handles optWed.CheckedChanged
        Edited()
    End Sub

    Private Sub optThu_CheckedChanged(sender As Object, e As EventArgs) Handles optThu.CheckedChanged
        Edited()
    End Sub

    Private Sub optFri_CheckedChanged(sender As Object, e As EventArgs) Handles optFri.CheckedChanged
        Edited()
    End Sub

    Private Sub optSat_CheckedChanged(sender As Object, e As EventArgs) Handles optSat.CheckedChanged
        Edited()
    End Sub

    Private Sub optSun_CheckedChanged(sender As Object, e As EventArgs) Handles optSun.CheckedChanged
        Edited()
    End Sub

    Private Sub chkMon_CheckedChanged(sender As Object, e As EventArgs) Handles chkMon.CheckedChanged
        Edited()
    End Sub

    Private Sub chkTue_CheckedChanged(sender As Object, e As EventArgs) Handles chkTue.CheckedChanged
        Edited()
    End Sub

    Private Sub chkWed_CheckedChanged(sender As Object, e As EventArgs) Handles chkWed.CheckedChanged
        Edited()
    End Sub

    Private Sub chkThu_CheckedChanged(sender As Object, e As EventArgs) Handles chkThu.CheckedChanged
        Edited()
    End Sub

    Private Sub chkFri_CheckedChanged(sender As Object, e As EventArgs) Handles chkFri.CheckedChanged
        Edited()
    End Sub

    Private Sub chkSat_CheckedChanged(sender As Object, e As EventArgs) Handles chkSat.CheckedChanged
        Edited()
    End Sub

    Private Sub chkSun_CheckedChanged(sender As Object, e As EventArgs) Handles chkSun.CheckedChanged
        Edited()
    End Sub

    Private Sub cboHours_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboHours.SelectedIndexChanged
        Edited()
    End Sub

    Private Sub cboMinutes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMinutes.SelectedIndexChanged
        Edited()
    End Sub

    Private Sub frmJob_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class