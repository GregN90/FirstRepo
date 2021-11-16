<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFileBackup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstJobs = New System.Windows.Forms.ListView()
        Me.cmdNewJob = New System.Windows.Forms.Button()
        Me.tmrTimer = New System.Windows.Forms.Timer(Me.components)
        Me.chkPause = New System.Windows.Forms.CheckBox()
        Me.lstActivity = New System.Windows.Forms.ListBox()
        Me.chkShowActivity = New System.Windows.Forms.CheckBox()
        Me.cmdRunSelectedBackup = New System.Windows.Forms.Button()
        Me.cmdClearActivity = New System.Windows.Forms.Button()
        Me.cmdDeleteJob = New System.Windows.Forms.Button()
        Me.chkSuppressDelete = New System.Windows.Forms.CheckBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.txtProgress = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileAgeToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdAbortBackup = New System.Windows.Forms.Button()
        Me.txtCompletionTime = New System.Windows.Forms.TextBox()
        Me.cmdDuplicate = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Backup Jobs:"
        '
        'lstJobs
        '
        Me.lstJobs.GridLines = True
        Me.lstJobs.HideSelection = False
        Me.lstJobs.Location = New System.Drawing.Point(24, 56)
        Me.lstJobs.MultiSelect = False
        Me.lstJobs.Name = "lstJobs"
        Me.lstJobs.Size = New System.Drawing.Size(750, 206)
        Me.lstJobs.TabIndex = 4
        Me.lstJobs.UseCompatibleStateImageBehavior = False
        '
        'cmdNewJob
        '
        Me.cmdNewJob.Location = New System.Drawing.Point(694, 19)
        Me.cmdNewJob.Name = "cmdNewJob"
        Me.cmdNewJob.Size = New System.Drawing.Size(80, 31)
        Me.cmdNewJob.TabIndex = 5
        Me.cmdNewJob.Text = "&New Job..."
        Me.cmdNewJob.UseVisualStyleBackColor = True
        '
        'tmrTimer
        '
        '
        'chkPause
        '
        Me.chkPause.AutoSize = True
        Me.chkPause.Location = New System.Drawing.Point(526, 280)
        Me.chkPause.Name = "chkPause"
        Me.chkPause.Size = New System.Drawing.Size(101, 17)
        Me.chkPause.TabIndex = 6
        Me.chkPause.Text = "Pause Backups"
        Me.chkPause.UseVisualStyleBackColor = True
        '
        'lstActivity
        '
        Me.lstActivity.FormattingEnabled = True
        Me.lstActivity.Location = New System.Drawing.Point(24, 309)
        Me.lstActivity.Name = "lstActivity"
        Me.lstActivity.Size = New System.Drawing.Size(749, 134)
        Me.lstActivity.TabIndex = 7
        '
        'chkShowActivity
        '
        Me.chkShowActivity.AutoSize = True
        Me.chkShowActivity.Location = New System.Drawing.Point(24, 280)
        Me.chkShowActivity.Name = "chkShowActivity"
        Me.chkShowActivity.Size = New System.Drawing.Size(127, 17)
        Me.chkShowActivity.TabIndex = 8
        Me.chkShowActivity.Text = "Show Activity Screen"
        Me.chkShowActivity.UseVisualStyleBackColor = True
        '
        'cmdRunSelectedBackup
        '
        Me.cmdRunSelectedBackup.Location = New System.Drawing.Point(654, 273)
        Me.cmdRunSelectedBackup.Name = "cmdRunSelectedBackup"
        Me.cmdRunSelectedBackup.Size = New System.Drawing.Size(119, 28)
        Me.cmdRunSelectedBackup.TabIndex = 9
        Me.cmdRunSelectedBackup.Text = "&Run selected backup"
        Me.cmdRunSelectedBackup.UseVisualStyleBackColor = True
        '
        'cmdClearActivity
        '
        Me.cmdClearActivity.Location = New System.Drawing.Point(157, 273)
        Me.cmdClearActivity.Name = "cmdClearActivity"
        Me.cmdClearActivity.Size = New System.Drawing.Size(81, 28)
        Me.cmdClearActivity.TabIndex = 10
        Me.cmdClearActivity.Text = "&Clear Activity"
        Me.cmdClearActivity.UseVisualStyleBackColor = True
        '
        'cmdDeleteJob
        '
        Me.cmdDeleteJob.Location = New System.Drawing.Point(608, 19)
        Me.cmdDeleteJob.Name = "cmdDeleteJob"
        Me.cmdDeleteJob.Size = New System.Drawing.Size(80, 31)
        Me.cmdDeleteJob.TabIndex = 11
        Me.cmdDeleteJob.Text = "&Delete Job"
        Me.cmdDeleteJob.UseVisualStyleBackColor = True
        '
        'chkSuppressDelete
        '
        Me.chkSuppressDelete.AutoSize = True
        Me.chkSuppressDelete.Location = New System.Drawing.Point(416, 280)
        Me.chkSuppressDelete.Name = "chkSuppressDelete"
        Me.chkSuppressDelete.Size = New System.Drawing.Size(104, 17)
        Me.chkSuppressDelete.TabIndex = 12
        Me.chkSuppressDelete.Text = "Suppress Delete"
        Me.chkSuppressDelete.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(244, 281)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(51, 13)
        Me.lblProgress.TabIndex = 13
        Me.lblProgress.Text = "Progress:"
        '
        'txtProgress
        '
        Me.txtProgress.Location = New System.Drawing.Point(301, 278)
        Me.txtProgress.Name = "txtProgress"
        Me.txtProgress.Size = New System.Drawing.Size(48, 20)
        Me.txtProgress.TabIndex = 14
        Me.txtProgress.Text = "0%"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileAgeToolToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 15
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileAgeToolToolStripMenuItem
        '
        Me.FileAgeToolToolStripMenuItem.Name = "FileAgeToolToolStripMenuItem"
        Me.FileAgeToolToolStripMenuItem.Size = New System.Drawing.Size(96, 20)
        Me.FileAgeToolToolStripMenuItem.Text = "File Age Tool..."
        '
        'cmdAbortBackup
        '
        Me.cmdAbortBackup.Location = New System.Drawing.Point(595, 268)
        Me.cmdAbortBackup.Name = "cmdAbortBackup"
        Me.cmdAbortBackup.Size = New System.Drawing.Size(119, 25)
        Me.cmdAbortBackup.TabIndex = 16
        Me.cmdAbortBackup.Text = "&Abort Job"
        Me.cmdAbortBackup.UseVisualStyleBackColor = True
        '
        'txtCompletionTime
        '
        Me.txtCompletionTime.Location = New System.Drawing.Point(368, 268)
        Me.txtCompletionTime.Name = "txtCompletionTime"
        Me.txtCompletionTime.Size = New System.Drawing.Size(135, 20)
        Me.txtCompletionTime.TabIndex = 17
        Me.txtCompletionTime.Text = "Completion Time: 17:43"
        '
        'cmdDuplicate
        '
        Me.cmdDuplicate.Location = New System.Drawing.Point(522, 19)
        Me.cmdDuplicate.Name = "cmdDuplicate"
        Me.cmdDuplicate.Size = New System.Drawing.Size(80, 31)
        Me.cmdDuplicate.TabIndex = 18
        Me.cmdDuplicate.Text = "D&uplicate Job"
        Me.cmdDuplicate.UseVisualStyleBackColor = True
        '
        'frmFileBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 461)
        Me.Controls.Add(Me.cmdDuplicate)
        Me.Controls.Add(Me.txtCompletionTime)
        Me.Controls.Add(Me.cmdAbortBackup)
        Me.Controls.Add(Me.txtProgress)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.chkSuppressDelete)
        Me.Controls.Add(Me.cmdDeleteJob)
        Me.Controls.Add(Me.cmdClearActivity)
        Me.Controls.Add(Me.cmdRunSelectedBackup)
        Me.Controls.Add(Me.chkShowActivity)
        Me.Controls.Add(Me.lstActivity)
        Me.Controls.Add(Me.chkPause)
        Me.Controls.Add(Me.cmdNewJob)
        Me.Controls.Add(Me.lstJobs)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmFileBackup"
        Me.Text = "File Backup"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstJobList As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lstJobs As ListView
    Friend WithEvents cmdNewJob As Button
    Friend WithEvents tmrTimer As Timer
    Friend WithEvents chkPause As CheckBox
    Friend WithEvents lstActivity As ListBox
    Friend WithEvents chkShowActivity As CheckBox
    Friend WithEvents cmdRunSelectedBackup As Button
    Friend WithEvents cmdClearActivity As Button
    Friend WithEvents cmdDeleteJob As Button
    Friend WithEvents chkSuppressDelete As CheckBox
    Friend WithEvents lblProgress As Label
    Friend WithEvents txtProgress As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileAgeToolToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cmdAbortBackup As Button
    Friend WithEvents txtCompletionTime As TextBox
    Friend WithEvents cmdDuplicate As Button
End Class
