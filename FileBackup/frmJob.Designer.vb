<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJob
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grpFolders = New System.Windows.Forms.GroupBox()
        Me.cmdDestinationNav = New System.Windows.Forms.Button()
        Me.cmdNavSource = New System.Windows.Forms.Button()
        Me.txtDestinationFolder = New System.Windows.Forms.TextBox()
        Me.txtSourceFolder = New System.Windows.Forms.TextBox()
        Me.grpSchedule = New System.Windows.Forms.GroupBox()
        Me.cboMinutes = New System.Windows.Forms.ComboBox()
        Me.cboHours = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboDay = New System.Windows.Forms.ComboBox()
        Me.cboFrequency = New System.Windows.Forms.ComboBox()
        Me.grpDaysOpt = New System.Windows.Forms.GroupBox()
        Me.optSun = New System.Windows.Forms.RadioButton()
        Me.optSat = New System.Windows.Forms.RadioButton()
        Me.optFri = New System.Windows.Forms.RadioButton()
        Me.optThu = New System.Windows.Forms.RadioButton()
        Me.optWed = New System.Windows.Forms.RadioButton()
        Me.optTue = New System.Windows.Forms.RadioButton()
        Me.optMon = New System.Windows.Forms.RadioButton()
        Me.grpDaysChk = New System.Windows.Forms.GroupBox()
        Me.chkSun = New System.Windows.Forms.CheckBox()
        Me.chkSat = New System.Windows.Forms.CheckBox()
        Me.chkFri = New System.Windows.Forms.CheckBox()
        Me.chkThu = New System.Windows.Forms.CheckBox()
        Me.chkWed = New System.Windows.Forms.CheckBox()
        Me.chkTue = New System.Windows.Forms.CheckBox()
        Me.chkMon = New System.Windows.Forms.CheckBox()
        Me.lblFrequency = New System.Windows.Forms.Label()
        Me.lblDays = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.dirFolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.grpFolders.SuspendLayout()
        Me.grpSchedule.SuspendLayout()
        Me.grpDaysOpt.SuspendLayout()
        Me.grpDaysChk.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Job Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Source Folder:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Destination Folder:"
        '
        'grpFolders
        '
        Me.grpFolders.Controls.Add(Me.cmdDestinationNav)
        Me.grpFolders.Controls.Add(Me.cmdNavSource)
        Me.grpFolders.Controls.Add(Me.txtDestinationFolder)
        Me.grpFolders.Controls.Add(Me.txtSourceFolder)
        Me.grpFolders.Controls.Add(Me.Label2)
        Me.grpFolders.Controls.Add(Me.Label3)
        Me.grpFolders.Location = New System.Drawing.Point(28, 63)
        Me.grpFolders.Name = "grpFolders"
        Me.grpFolders.Size = New System.Drawing.Size(578, 101)
        Me.grpFolders.TabIndex = 4
        Me.grpFolders.TabStop = False
        Me.grpFolders.Text = "Folder Details"
        '
        'cmdDestinationNav
        '
        Me.cmdDestinationNav.Location = New System.Drawing.Point(542, 59)
        Me.cmdDestinationNav.Name = "cmdDestinationNav"
        Me.cmdDestinationNav.Size = New System.Drawing.Size(26, 22)
        Me.cmdDestinationNav.TabIndex = 6
        Me.cmdDestinationNav.Text = "..."
        Me.cmdDestinationNav.UseVisualStyleBackColor = True
        '
        'cmdNavSource
        '
        Me.cmdNavSource.Location = New System.Drawing.Point(542, 25)
        Me.cmdNavSource.Name = "cmdNavSource"
        Me.cmdNavSource.Size = New System.Drawing.Size(26, 22)
        Me.cmdNavSource.TabIndex = 5
        Me.cmdNavSource.Text = "..."
        Me.cmdNavSource.UseVisualStyleBackColor = True
        '
        'txtDestinationFolder
        '
        Me.txtDestinationFolder.Location = New System.Drawing.Point(127, 59)
        Me.txtDestinationFolder.Name = "txtDestinationFolder"
        Me.txtDestinationFolder.Size = New System.Drawing.Size(399, 20)
        Me.txtDestinationFolder.TabIndex = 4
        '
        'txtSourceFolder
        '
        Me.txtSourceFolder.Location = New System.Drawing.Point(127, 26)
        Me.txtSourceFolder.Name = "txtSourceFolder"
        Me.txtSourceFolder.Size = New System.Drawing.Size(400, 20)
        Me.txtSourceFolder.TabIndex = 3
        '
        'grpSchedule
        '
        Me.grpSchedule.Controls.Add(Me.cboMinutes)
        Me.grpSchedule.Controls.Add(Me.cboHours)
        Me.grpSchedule.Controls.Add(Me.Label4)
        Me.grpSchedule.Controls.Add(Me.cboDay)
        Me.grpSchedule.Controls.Add(Me.cboFrequency)
        Me.grpSchedule.Controls.Add(Me.grpDaysOpt)
        Me.grpSchedule.Controls.Add(Me.grpDaysChk)
        Me.grpSchedule.Controls.Add(Me.lblFrequency)
        Me.grpSchedule.Controls.Add(Me.lblDays)
        Me.grpSchedule.Location = New System.Drawing.Point(28, 179)
        Me.grpSchedule.Name = "grpSchedule"
        Me.grpSchedule.Size = New System.Drawing.Size(578, 129)
        Me.grpSchedule.TabIndex = 5
        Me.grpSchedule.TabStop = False
        Me.grpSchedule.Text = "Schedule"
        '
        'cboMinutes
        '
        Me.cboMinutes.FormattingEnabled = True
        Me.cboMinutes.Location = New System.Drawing.Point(188, 93)
        Me.cboMinutes.Name = "cboMinutes"
        Me.cboMinutes.Size = New System.Drawing.Size(56, 21)
        Me.cboMinutes.TabIndex = 12
        '
        'cboHours
        '
        Me.cboHours.FormattingEnabled = True
        Me.cboHours.Location = New System.Drawing.Point(112, 93)
        Me.cboHours.Name = "cboHours"
        Me.cboHours.Size = New System.Drawing.Size(56, 21)
        Me.cboHours.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Backup Time:"
        '
        'cboDay
        '
        Me.cboDay.FormattingEnabled = True
        Me.cboDay.Location = New System.Drawing.Point(112, 59)
        Me.cboDay.Name = "cboDay"
        Me.cboDay.Size = New System.Drawing.Size(132, 21)
        Me.cboDay.TabIndex = 9
        '
        'cboFrequency
        '
        Me.cboFrequency.FormattingEnabled = True
        Me.cboFrequency.Location = New System.Drawing.Point(112, 26)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(132, 21)
        Me.cboFrequency.TabIndex = 8
        '
        'grpDaysOpt
        '
        Me.grpDaysOpt.Controls.Add(Me.optSun)
        Me.grpDaysOpt.Controls.Add(Me.optSat)
        Me.grpDaysOpt.Controls.Add(Me.optFri)
        Me.grpDaysOpt.Controls.Add(Me.optThu)
        Me.grpDaysOpt.Controls.Add(Me.optWed)
        Me.grpDaysOpt.Controls.Add(Me.optTue)
        Me.grpDaysOpt.Controls.Add(Me.optMon)
        Me.grpDaysOpt.Location = New System.Drawing.Point(313, 10)
        Me.grpDaysOpt.Name = "grpDaysOpt"
        Me.grpDaysOpt.Size = New System.Drawing.Size(403, 37)
        Me.grpDaysOpt.TabIndex = 7
        Me.grpDaysOpt.TabStop = False
        '
        'optSun
        '
        Me.optSun.AutoSize = True
        Me.optSun.Location = New System.Drawing.Point(350, 16)
        Me.optSun.Name = "optSun"
        Me.optSun.Size = New System.Drawing.Size(44, 17)
        Me.optSun.TabIndex = 6
        Me.optSun.TabStop = True
        Me.optSun.Text = "Sun"
        Me.optSun.UseVisualStyleBackColor = True
        '
        'optSat
        '
        Me.optSat.AutoSize = True
        Me.optSat.Location = New System.Drawing.Point(297, 16)
        Me.optSat.Name = "optSat"
        Me.optSat.Size = New System.Drawing.Size(41, 17)
        Me.optSat.TabIndex = 5
        Me.optSat.TabStop = True
        Me.optSat.Text = "Sat"
        Me.optSat.UseVisualStyleBackColor = True
        '
        'optFri
        '
        Me.optFri.AutoSize = True
        Me.optFri.Location = New System.Drawing.Point(222, 16)
        Me.optFri.Name = "optFri"
        Me.optFri.Size = New System.Drawing.Size(36, 17)
        Me.optFri.TabIndex = 4
        Me.optFri.TabStop = True
        Me.optFri.Text = "Fri"
        Me.optFri.UseVisualStyleBackColor = True
        '
        'optThu
        '
        Me.optThu.AutoSize = True
        Me.optThu.Location = New System.Drawing.Point(169, 16)
        Me.optThu.Name = "optThu"
        Me.optThu.Size = New System.Drawing.Size(44, 17)
        Me.optThu.TabIndex = 3
        Me.optThu.TabStop = True
        Me.optThu.Text = "Thu"
        Me.optThu.UseVisualStyleBackColor = True
        '
        'optWed
        '
        Me.optWed.AutoSize = True
        Me.optWed.Location = New System.Drawing.Point(116, 16)
        Me.optWed.Name = "optWed"
        Me.optWed.Size = New System.Drawing.Size(48, 17)
        Me.optWed.TabIndex = 2
        Me.optWed.TabStop = True
        Me.optWed.Text = "Wed"
        Me.optWed.UseVisualStyleBackColor = True
        '
        'optTue
        '
        Me.optTue.AutoSize = True
        Me.optTue.Location = New System.Drawing.Point(63, 16)
        Me.optTue.Name = "optTue"
        Me.optTue.Size = New System.Drawing.Size(44, 17)
        Me.optTue.TabIndex = 1
        Me.optTue.TabStop = True
        Me.optTue.Text = "Tue"
        Me.optTue.UseVisualStyleBackColor = True
        '
        'optMon
        '
        Me.optMon.AutoSize = True
        Me.optMon.Location = New System.Drawing.Point(10, 16)
        Me.optMon.Name = "optMon"
        Me.optMon.Size = New System.Drawing.Size(46, 17)
        Me.optMon.TabIndex = 0
        Me.optMon.TabStop = True
        Me.optMon.Text = "Mon"
        Me.optMon.UseVisualStyleBackColor = True
        '
        'grpDaysChk
        '
        Me.grpDaysChk.Controls.Add(Me.chkSun)
        Me.grpDaysChk.Controls.Add(Me.chkSat)
        Me.grpDaysChk.Controls.Add(Me.chkFri)
        Me.grpDaysChk.Controls.Add(Me.chkThu)
        Me.grpDaysChk.Controls.Add(Me.chkWed)
        Me.grpDaysChk.Controls.Add(Me.chkTue)
        Me.grpDaysChk.Controls.Add(Me.chkMon)
        Me.grpDaysChk.Location = New System.Drawing.Point(313, 49)
        Me.grpDaysChk.Name = "grpDaysChk"
        Me.grpDaysChk.Size = New System.Drawing.Size(403, 38)
        Me.grpDaysChk.TabIndex = 3
        Me.grpDaysChk.TabStop = False
        '
        'chkSun
        '
        Me.chkSun.AutoSize = True
        Me.chkSun.Location = New System.Drawing.Point(350, 15)
        Me.chkSun.Name = "chkSun"
        Me.chkSun.Size = New System.Drawing.Size(45, 17)
        Me.chkSun.TabIndex = 6
        Me.chkSun.Text = "Sun"
        Me.chkSun.UseVisualStyleBackColor = True
        '
        'chkSat
        '
        Me.chkSat.AutoSize = True
        Me.chkSat.Location = New System.Drawing.Point(297, 15)
        Me.chkSat.Name = "chkSat"
        Me.chkSat.Size = New System.Drawing.Size(42, 17)
        Me.chkSat.TabIndex = 5
        Me.chkSat.Text = "Sat"
        Me.chkSat.UseVisualStyleBackColor = True
        '
        'chkFri
        '
        Me.chkFri.AutoSize = True
        Me.chkFri.Location = New System.Drawing.Point(222, 15)
        Me.chkFri.Name = "chkFri"
        Me.chkFri.Size = New System.Drawing.Size(37, 17)
        Me.chkFri.TabIndex = 4
        Me.chkFri.Text = "Fri"
        Me.chkFri.UseVisualStyleBackColor = True
        '
        'chkThu
        '
        Me.chkThu.AutoSize = True
        Me.chkThu.Location = New System.Drawing.Point(169, 15)
        Me.chkThu.Name = "chkThu"
        Me.chkThu.Size = New System.Drawing.Size(45, 17)
        Me.chkThu.TabIndex = 3
        Me.chkThu.Text = "Thu"
        Me.chkThu.UseVisualStyleBackColor = True
        '
        'chkWed
        '
        Me.chkWed.AutoSize = True
        Me.chkWed.Location = New System.Drawing.Point(116, 15)
        Me.chkWed.Name = "chkWed"
        Me.chkWed.Size = New System.Drawing.Size(49, 17)
        Me.chkWed.TabIndex = 2
        Me.chkWed.Text = "Wed"
        Me.chkWed.UseVisualStyleBackColor = True
        '
        'chkTue
        '
        Me.chkTue.AutoSize = True
        Me.chkTue.Location = New System.Drawing.Point(63, 15)
        Me.chkTue.Name = "chkTue"
        Me.chkTue.Size = New System.Drawing.Size(45, 17)
        Me.chkTue.TabIndex = 1
        Me.chkTue.Text = "Tue"
        Me.chkTue.UseVisualStyleBackColor = True
        '
        'chkMon
        '
        Me.chkMon.AutoSize = True
        Me.chkMon.Location = New System.Drawing.Point(10, 16)
        Me.chkMon.Name = "chkMon"
        Me.chkMon.Size = New System.Drawing.Size(47, 17)
        Me.chkMon.TabIndex = 0
        Me.chkMon.Text = "Mon"
        Me.chkMon.UseVisualStyleBackColor = True
        '
        'lblFrequency
        '
        Me.lblFrequency.AutoSize = True
        Me.lblFrequency.Location = New System.Drawing.Point(26, 29)
        Me.lblFrequency.Name = "lblFrequency"
        Me.lblFrequency.Size = New System.Drawing.Size(60, 13)
        Me.lblFrequency.TabIndex = 1
        Me.lblFrequency.Text = "Frequency:"
        '
        'lblDays
        '
        Me.lblDays.AutoSize = True
        Me.lblDays.Location = New System.Drawing.Point(26, 62)
        Me.lblDays.Name = "lblDays"
        Me.lblDays.Size = New System.Drawing.Size(80, 13)
        Me.lblDays.TabIndex = 2
        Me.lblDays.Text = "Backup Day(s):"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(89, 26)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(371, 20)
        Me.txtName.TabIndex = 10
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(481, 29)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(125, 17)
        Me.chkEnabled.TabIndex = 11
        Me.chkEnabled.Text = "Backup Job Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(405, 322)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(101, 32)
        Me.cmdSave.TabIndex = 12
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(512, 322)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(101, 32)
        Me.cmdCancel.TabIndex = 13
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frmJob
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(633, 366)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.chkEnabled)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.grpSchedule)
        Me.Controls.Add(Me.grpFolders)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmJob"
        Me.Text = "Backup Job"
        Me.grpFolders.ResumeLayout(False)
        Me.grpFolders.PerformLayout()
        Me.grpSchedule.ResumeLayout(False)
        Me.grpSchedule.PerformLayout()
        Me.grpDaysOpt.ResumeLayout(False)
        Me.grpDaysOpt.PerformLayout()
        Me.grpDaysChk.ResumeLayout(False)
        Me.grpDaysChk.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents grpFolders As GroupBox
    Friend WithEvents grpSchedule As GroupBox
    Friend WithEvents lblFrequency As Label
    Friend WithEvents lblDays As Label
    Friend WithEvents grpDaysChk As GroupBox
    Friend WithEvents chkSun As CheckBox
    Friend WithEvents chkSat As CheckBox
    Friend WithEvents chkFri As CheckBox
    Friend WithEvents chkThu As CheckBox
    Friend WithEvents chkWed As CheckBox
    Friend WithEvents chkTue As CheckBox
    Friend WithEvents chkMon As CheckBox
    Friend WithEvents cboFrequency As ComboBox
    Friend WithEvents grpDaysOpt As GroupBox
    Friend WithEvents optSun As RadioButton
    Friend WithEvents optSat As RadioButton
    Friend WithEvents optFri As RadioButton
    Friend WithEvents optThu As RadioButton
    Friend WithEvents optWed As RadioButton
    Friend WithEvents optTue As RadioButton
    Friend WithEvents optMon As RadioButton
    Friend WithEvents cboDay As ComboBox
    Friend WithEvents cmdDestinationNav As Button
    Friend WithEvents cmdNavSource As Button
    Friend WithEvents txtDestinationFolder As TextBox
    Friend WithEvents txtSourceFolder As TextBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents chkEnabled As CheckBox
    Friend WithEvents cmdSave As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents dirFolderBrowser As FolderBrowserDialog
    Friend WithEvents cboMinutes As ComboBox
    Friend WithEvents cboHours As ComboBox
    Friend WithEvents Label4 As Label
End Class
