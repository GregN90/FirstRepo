<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAgeList
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
        Me.lblSourceFolder = New System.Windows.Forms.Label()
        Me.txtSourceFolder = New System.Windows.Forms.TextBox()
        Me.cmdSourceNavigate = New System.Windows.Forms.Button()
        Me.dirFolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.treFiles = New System.Windows.Forms.TreeView()
        Me.cmdScanFolder = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboDateParameter = New System.Windows.Forms.ComboBox()
        Me.lblOlder = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.lblPleaseWait = New System.Windows.Forms.Label()
        Me.cmdLarger = New System.Windows.Forms.Button()
        Me.cmdSmaller = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.txtCurrentFolder = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSelectedTotal = New System.Windows.Forms.TextBox()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.lblLonger = New System.Windows.Forms.Label()
        Me.txtMaxPathLength = New System.Windows.Forms.TextBox()
        Me.cboComparison = New System.Windows.Forms.ComboBox()
        Me.cmdShowIdentifiedFiles = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblSourceFolder
        '
        Me.lblSourceFolder.AutoSize = True
        Me.lblSourceFolder.Location = New System.Drawing.Point(17, 18)
        Me.lblSourceFolder.Name = "lblSourceFolder"
        Me.lblSourceFolder.Size = New System.Drawing.Size(76, 13)
        Me.lblSourceFolder.TabIndex = 0
        Me.lblSourceFolder.Text = "Source Folder:"
        '
        'txtSourceFolder
        '
        Me.txtSourceFolder.Location = New System.Drawing.Point(105, 18)
        Me.txtSourceFolder.Name = "txtSourceFolder"
        Me.txtSourceFolder.Size = New System.Drawing.Size(616, 20)
        Me.txtSourceFolder.TabIndex = 1
        '
        'cmdSourceNavigate
        '
        Me.cmdSourceNavigate.Location = New System.Drawing.Point(727, 19)
        Me.cmdSourceNavigate.Name = "cmdSourceNavigate"
        Me.cmdSourceNavigate.Size = New System.Drawing.Size(24, 19)
        Me.cmdSourceNavigate.TabIndex = 2
        Me.cmdSourceNavigate.Text = "..."
        Me.cmdSourceNavigate.UseVisualStyleBackColor = True
        '
        'treFiles
        '
        Me.treFiles.Location = New System.Drawing.Point(20, 117)
        Me.treFiles.Name = "treFiles"
        Me.treFiles.Size = New System.Drawing.Size(657, 433)
        Me.treFiles.TabIndex = 3
        '
        'cmdScanFolder
        '
        Me.cmdScanFolder.Location = New System.Drawing.Point(651, 48)
        Me.cmdScanFolder.Name = "cmdScanFolder"
        Me.cmdScanFolder.Size = New System.Drawing.Size(100, 26)
        Me.cmdScanFolder.TabIndex = 4
        Me.cmdScanFolder.Text = "Scan Folder"
        Me.cmdScanFolder.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Flag when"
        '
        'cboDateParameter
        '
        Me.cboDateParameter.FormattingEnabled = True
        Me.cboDateParameter.Location = New System.Drawing.Point(105, 50)
        Me.cboDateParameter.Name = "cboDateParameter"
        Me.cboDateParameter.Size = New System.Drawing.Size(146, 21)
        Me.cboDateParameter.TabIndex = 6
        '
        'lblOlder
        '
        Me.lblOlder.AutoSize = True
        Me.lblOlder.Location = New System.Drawing.Point(257, 53)
        Me.lblOlder.Name = "lblOlder"
        Me.lblOlder.Size = New System.Drawing.Size(64, 13)
        Me.lblOlder.TabIndex = 7
        Me.lblOlder.Text = "is older than"
        '
        'dtpDate
        '
        Me.dtpDate.Location = New System.Drawing.Point(399, 50)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(139, 20)
        Me.dtpDate.TabIndex = 8
        Me.dtpDate.Value = New Date(2020, 12, 2, 11, 9, 0, 0)
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Location = New System.Drawing.Point(546, 47)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(99, 26)
        Me.cmdUpdate.TabIndex = 9
        Me.cmdUpdate.Text = "Update Tree"
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'lblPleaseWait
        '
        Me.lblPleaseWait.AutoSize = True
        Me.lblPleaseWait.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPleaseWait.Location = New System.Drawing.Point(190, 218)
        Me.lblPleaseWait.Name = "lblPleaseWait"
        Me.lblPleaseWait.Size = New System.Drawing.Size(317, 31)
        Me.lblPleaseWait.TabIndex = 10
        Me.lblPleaseWait.Text = "Scanning...Please Wait"
        '
        'cmdLarger
        '
        Me.cmdLarger.Location = New System.Drawing.Point(659, 558)
        Me.cmdLarger.Name = "cmdLarger"
        Me.cmdLarger.Size = New System.Drawing.Size(18, 20)
        Me.cmdLarger.TabIndex = 11
        Me.cmdLarger.Text = "+"
        Me.cmdLarger.UseVisualStyleBackColor = True
        '
        'cmdSmaller
        '
        Me.cmdSmaller.Location = New System.Drawing.Point(635, 558)
        Me.cmdSmaller.Name = "cmdSmaller"
        Me.cmdSmaller.Size = New System.Drawing.Size(18, 20)
        Me.cmdSmaller.TabIndex = 12
        Me.cmdSmaller.Text = "-"
        Me.cmdSmaller.UseVisualStyleBackColor = True
        '
        'cmdStop
        '
        Me.cmdStop.Location = New System.Drawing.Point(563, 556)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(59, 21)
        Me.cmdStop.TabIndex = 13
        Me.cmdStop.Text = "&Stop"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'txtCurrentFolder
        '
        Me.txtCurrentFolder.Location = New System.Drawing.Point(20, 556)
        Me.txtCurrentFolder.Name = "txtCurrentFolder"
        Me.txtCurrentFolder.Size = New System.Drawing.Size(525, 20)
        Me.txtCurrentFolder.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Selected Total:"
        '
        'txtSelectedTotal
        '
        Me.txtSelectedTotal.Location = New System.Drawing.Point(105, 87)
        Me.txtSelectedTotal.Name = "txtSelectedTotal"
        Me.txtSelectedTotal.Size = New System.Drawing.Size(146, 20)
        Me.txtSelectedTotal.TabIndex = 16
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(260, 85)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(61, 22)
        Me.cmdClear.TabIndex = 17
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'lblLonger
        '
        Me.lblLonger.AutoSize = True
        Me.lblLonger.Location = New System.Drawing.Point(607, 90)
        Me.lblLonger.Name = "lblLonger"
        Me.lblLonger.Size = New System.Drawing.Size(70, 13)
        Me.lblLonger.TabIndex = 18
        Me.lblLonger.Text = "is longer than"
        '
        'txtMaxPathLength
        '
        Me.txtMaxPathLength.Location = New System.Drawing.Point(686, 89)
        Me.txtMaxPathLength.Name = "txtMaxPathLength"
        Me.txtMaxPathLength.Size = New System.Drawing.Size(60, 20)
        Me.txtMaxPathLength.TabIndex = 19
        '
        'cboComparison
        '
        Me.cboComparison.FormattingEnabled = True
        Me.cboComparison.Location = New System.Drawing.Point(257, 50)
        Me.cboComparison.Name = "cboComparison"
        Me.cboComparison.Size = New System.Drawing.Size(135, 21)
        Me.cboComparison.TabIndex = 20
        '
        'cmdShowIdentifiedFiles
        '
        Me.cmdShowIdentifiedFiles.Location = New System.Drawing.Point(327, 85)
        Me.cmdShowIdentifiedFiles.Name = "cmdShowIdentifiedFiles"
        Me.cmdShowIdentifiedFiles.Size = New System.Drawing.Size(124, 22)
        Me.cmdShowIdentifiedFiles.TabIndex = 21
        Me.cmdShowIdentifiedFiles.Text = "Show Highlighted &Files"
        Me.cmdShowIdentifiedFiles.UseVisualStyleBackColor = True
        '
        'frmAgeList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(763, 580)
        Me.Controls.Add(Me.cmdShowIdentifiedFiles)
        Me.Controls.Add(Me.cboComparison)
        Me.Controls.Add(Me.txtMaxPathLength)
        Me.Controls.Add(Me.lblLonger)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.txtSelectedTotal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCurrentFolder)
        Me.Controls.Add(Me.cmdStop)
        Me.Controls.Add(Me.cmdSmaller)
        Me.Controls.Add(Me.cmdLarger)
        Me.Controls.Add(Me.lblPleaseWait)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.lblOlder)
        Me.Controls.Add(Me.cboDateParameter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdScanFolder)
        Me.Controls.Add(Me.treFiles)
        Me.Controls.Add(Me.cmdSourceNavigate)
        Me.Controls.Add(Me.txtSourceFolder)
        Me.Controls.Add(Me.lblSourceFolder)
        Me.Name = "frmAgeList"
        Me.Text = "Age List"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSourceFolder As Label
    Friend WithEvents txtSourceFolder As TextBox
    Friend WithEvents cmdSourceNavigate As Button
    Friend WithEvents dirFolderBrowser As FolderBrowserDialog
    Friend WithEvents treFiles As TreeView
    Friend WithEvents cmdScanFolder As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cboDateParameter As ComboBox
    Friend WithEvents lblOlder As Label
    Friend WithEvents dtpDate As DateTimePicker
    Friend WithEvents cmdUpdate As Button
    Friend WithEvents lblPleaseWait As Label
    Friend WithEvents cmdLarger As Button
    Friend WithEvents cmdSmaller As Button
    Friend WithEvents cmdStop As Button
    Friend WithEvents txtCurrentFolder As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtSelectedTotal As TextBox
    Friend WithEvents cmdClear As Button
    Friend WithEvents lblLonger As Label
    Friend WithEvents txtMaxPathLength As TextBox
    Friend WithEvents cboComparison As ComboBox
    Friend WithEvents cmdShowIdentifiedFiles As Button
End Class
