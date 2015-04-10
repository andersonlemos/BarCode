<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.lblVideoSource = New System.Windows.Forms.Label()
        Me.cboVideoSource = New System.Windows.Forms.ComboBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnStart = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.VideoSourcePlayer1 = New AForge.Controls.VideoSourcePlayer()
        Me.SuspendLayout()
        '
        'lblVideoSource
        '
        Me.lblVideoSource.AutoSize = True
        Me.lblVideoSource.Location = New System.Drawing.Point(18, 19)
        Me.lblVideoSource.Name = "lblVideoSource"
        Me.lblVideoSource.Size = New System.Drawing.Size(71, 13)
        Me.lblVideoSource.TabIndex = 9
        Me.lblVideoSource.Text = "Video Source"
        '
        'cboVideoSource
        '
        Me.cboVideoSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVideoSource.FormattingEnabled = True
        Me.cboVideoSource.Location = New System.Drawing.Point(21, 35)
        Me.cboVideoSource.Name = "cboVideoSource"
        Me.cboVideoSource.Size = New System.Drawing.Size(300, 21)
        Me.cboVideoSource.TabIndex = 8
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(20, 354)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(301, 46)
        Me.btnStart.TabIndex = 11
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(21, 267)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(300, 81)
        Me.TextBox1.TabIndex = 13
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(20, 406)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(301, 46)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Stop"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'VideoSourcePlayer1
        '
        Me.VideoSourcePlayer1.Location = New System.Drawing.Point(20, 62)
        Me.VideoSourcePlayer1.Name = "VideoSourcePlayer1"
        Me.VideoSourcePlayer1.Size = New System.Drawing.Size(301, 199)
        Me.VideoSourcePlayer1.TabIndex = 32
        Me.VideoSourcePlayer1.Text = "VideoSourcePlayer1"
        Me.VideoSourcePlayer1.VideoSource = Nothing
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 468)
        Me.Controls.Add(Me.VideoSourcePlayer1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.lblVideoSource)
        Me.Controls.Add(Me.cboVideoSource)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BarCode Scanner"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblVideoSource As System.Windows.Forms.Label
    Friend WithEvents cboVideoSource As System.Windows.Forms.ComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents VideoSourcePlayer1 As AForge.Controls.VideoSourcePlayer

End Class
