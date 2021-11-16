<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileList
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
        Me.txtList = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFileCount = New System.Windows.Forms.TextBox()
        Me.cmdSaveFileList = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtList
        '
        Me.txtList.Location = New System.Drawing.Point(12, 34)
        Me.txtList.Multiline = True
        Me.txtList.Name = "txtList"
        Me.txtList.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtList.Size = New System.Drawing.Size(501, 307)
        Me.txtList.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Total Number of Files:"
        '
        'txtFileCount
        '
        Me.txtFileCount.Location = New System.Drawing.Point(124, 6)
        Me.txtFileCount.Name = "txtFileCount"
        Me.txtFileCount.Size = New System.Drawing.Size(82, 20)
        Me.txtFileCount.TabIndex = 2
        '
        'cmdSaveFileList
        '
        Me.cmdSaveFileList.Location = New System.Drawing.Point(219, 7)
        Me.cmdSaveFileList.Name = "cmdSaveFileList"
        Me.cmdSaveFileList.Size = New System.Drawing.Size(97, 21)
        Me.cmdSaveFileList.TabIndex = 3
        Me.cmdSaveFileList.Text = "&Save to File"
        Me.cmdSaveFileList.UseVisualStyleBackColor = True
        '
        'frmFileList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 371)
        Me.Controls.Add(Me.cmdSaveFileList)
        Me.Controls.Add(Me.txtFileCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtList)
        Me.Name = "frmFileList"
        Me.Text = "File List"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtList As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtFileCount As TextBox
    Friend WithEvents cmdSaveFileList As Button
End Class
