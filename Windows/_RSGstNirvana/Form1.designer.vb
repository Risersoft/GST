Partial Class Form1
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.txtSQL = New System.Windows.Forms.TextBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbDatabaseName = New System.Windows.Forms.ComboBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtTimeout = New System.Windows.Forms.NumericUpDown()
        Me.label1 = New System.Windows.Forms.Label()
        Me.chkIntegratedSecurity = New System.Windows.Forms.CheckBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.gvMain = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblQueryStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblRowsReturned = New System.Windows.Forms.ToolStripStatusLabel()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRunQuery = New System.Windows.Forms.ToolStripButton()
        Me.btnStopQuery = New System.Windows.Forms.ToolStripButton()
        Me.tcMain.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        CType(Me.txtTimeout, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPage2.SuspendLayout()
        CType(Me.gvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statusStrip1.SuspendLayout()
        Me.toolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tabPage1)
        Me.tcMain.Controls.Add(Me.tabPage2)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 25)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(676, 497)
        Me.tcMain.TabIndex = 1
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.txtSQL)
        Me.tabPage1.Controls.Add(Me.groupBox1)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage1.Size = New System.Drawing.Size(668, 471)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "Connection and SQL"
        Me.tabPage1.UseVisualStyleBackColor = True
        '
        'txtSQL
        '
        Me.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSQL.Location = New System.Drawing.Point(3, 143)
        Me.txtSQL.Multiline = True
        Me.txtSQL.Name = "txtSQL"
        Me.txtSQL.Size = New System.Drawing.Size(662, 325)
        Me.txtSQL.TabIndex = 2
        Me.txtSQL.Text = "SELECT"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.cmbDatabaseName)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.txtTimeout)
        Me.groupBox1.Controls.Add(Me.label1)
        Me.groupBox1.Controls.Add(Me.chkIntegratedSecurity)
        Me.groupBox1.Controls.Add(Me.txtPassword)
        Me.groupBox1.Controls.Add(Me.label2)
        Me.groupBox1.Controls.Add(Me.txtUsername)
        Me.groupBox1.Controls.Add(Me.lblUsername)
        Me.groupBox1.Controls.Add(Me.txtServerName)
        Me.groupBox1.Controls.Add(Me.lblPassword)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupBox1.Location = New System.Drawing.Point(3, 3)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(662, 140)
        Me.groupBox1.TabIndex = 1
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Connection String Builder"
        '
        'cmbDatabaseName
        '
        Me.cmbDatabaseName.FormattingEnabled = True
        Me.cmbDatabaseName.Location = New System.Drawing.Point(494, 24)
        Me.cmbDatabaseName.Name = "cmbDatabaseName"
        Me.cmbDatabaseName.Size = New System.Drawing.Size(149, 21)
        Me.cmbDatabaseName.TabIndex = 4
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(374, 28)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(87, 13)
        Me.label3.TabIndex = 10
        Me.label3.Text = "Database Name:"
        '
        'txtTimeout
        '
        Me.txtTimeout.Location = New System.Drawing.Point(494, 51)
        Me.txtTimeout.Name = "txtTimeout"
        Me.txtTimeout.Size = New System.Drawing.Size(77, 20)
        Me.txtTimeout.TabIndex = 9
        Me.txtTimeout.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(374, 53)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(105, 13)
        Me.label1.TabIndex = 8
        Me.label1.Text = "Connection Timeout:"
        '
        'chkIntegratedSecurity
        '
        Me.chkIntegratedSecurity.AutoSize = True
        Me.chkIntegratedSecurity.Location = New System.Drawing.Point(111, 51)
        Me.chkIntegratedSecurity.Name = "chkIntegratedSecurity"
        Me.chkIntegratedSecurity.Size = New System.Drawing.Size(137, 17)
        Me.chkIntegratedSecurity.TabIndex = 1
        Me.chkIntegratedSecurity.Text = "&Use Integrated Security"
        Me.chkIntegratedSecurity.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(111, 103)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(236, 20)
        Me.txtPassword.TabIndex = 3
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(23, 28)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(72, 13)
        Me.label2.TabIndex = 1
        Me.label2.Text = "Server Name:"
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(111, 76)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(236, 20)
        Me.txtUsername.TabIndex = 2
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(23, 76)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 2
        Me.lblUsername.Text = "Username:"
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(111, 25)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(236, 20)
        Me.txtServerName.TabIndex = 0
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(23, 103)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 3
        Me.lblPassword.Text = "Password:"
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.gvMain)
        Me.tabPage2.Controls.Add(Me.statusStrip1)
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage2.Size = New System.Drawing.Size(668, 471)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "Query Results"
        Me.tabPage2.UseVisualStyleBackColor = True
        '
        'gvMain
        '
        Me.gvMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMain.Location = New System.Drawing.Point(3, 3)
        Me.gvMain.Name = "gvMain"
        Me.gvMain.Size = New System.Drawing.Size(662, 441)
        Me.gvMain.TabIndex = 10
        Me.gvMain.Text = "Delivery Schedule"
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblQueryStatus, Me.lblRowsReturned})
        Me.statusStrip1.Location = New System.Drawing.Point(3, 444)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(662, 24)
        Me.statusStrip1.TabIndex = 1
        Me.statusStrip1.Text = "statusStrip1"
        '
        'lblQueryStatus
        '
        Me.lblQueryStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblQueryStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.lblQueryStatus.Name = "lblQueryStatus"
        Me.lblQueryStatus.Size = New System.Drawing.Size(43, 19)
        Me.lblQueryStatus.Text = "Ready"
        '
        'lblRowsReturned
        '
        Me.lblRowsReturned.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblRowsReturned.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.lblRowsReturned.Name = "lblRowsReturned"
        Me.lblRowsReturned.Size = New System.Drawing.Size(48, 19)
        Me.lblRowsReturned.Text = "0 Rows"
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRunQuery, Me.btnStopQuery})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.Size = New System.Drawing.Size(676, 25)
        Me.toolStrip1.TabIndex = 2
        Me.toolStrip1.Text = "toolStrip1"
        '
        'btnRunQuery
        '
        Me.btnRunQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRunQuery.Image = CType(resources.GetObject("btnRunQuery.Image"), System.Drawing.Image)
        Me.btnRunQuery.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRunQuery.Name = "btnRunQuery"
        Me.btnRunQuery.Size = New System.Drawing.Size(23, 22)
        Me.btnRunQuery.Text = "Run Query"
        '
        'btnStopQuery
        '
        Me.btnStopQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnStopQuery.Enabled = False
        Me.btnStopQuery.Image = CType(resources.GetObject("btnStopQuery.Image"), System.Drawing.Image)
        Me.btnStopQuery.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStopQuery.Name = "btnStopQuery"
        Me.btnStopQuery.Size = New System.Drawing.Size(23, 22)
        Me.btnStopQuery.Text = "btnStopQuery"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(676, 522)
        Me.Controls.Add(Me.tcMain)
        Me.Controls.Add(Me.toolStrip1)
        Me.Name = "Form1"
        Me.Text = "Cancellable Query Demo"
        Me.tcMain.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        Me.tabPage1.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        CType(Me.txtTimeout, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPage2.ResumeLayout(False)
        Me.tabPage2.PerformLayout()
        CType(Me.gvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private tcMain As System.Windows.Forms.TabControl
    Private tabPage1 As System.Windows.Forms.TabPage
    Private groupBox1 As System.Windows.Forms.GroupBox
    Private chkIntegratedSecurity As System.Windows.Forms.CheckBox
    Private txtPassword As System.Windows.Forms.TextBox
    Private label2 As System.Windows.Forms.Label
    Private txtUsername As System.Windows.Forms.TextBox
    Private lblUsername As System.Windows.Forms.Label
    Private txtServerName As System.Windows.Forms.TextBox
    Private lblPassword As System.Windows.Forms.Label
    Private tabPage2 As System.Windows.Forms.TabPage
    Private txtTimeout As System.Windows.Forms.NumericUpDown
    Private label1 As System.Windows.Forms.Label
    Private cmbDatabaseName As System.Windows.Forms.ComboBox
    Private label3 As System.Windows.Forms.Label
    Private txtSQL As System.Windows.Forms.TextBox
    Private toolStrip1 As System.Windows.Forms.ToolStrip
    Private WithEvents btnRunQuery As System.Windows.Forms.ToolStripButton
    Private WithEvents btnStopQuery As System.Windows.Forms.ToolStripButton
    Private statusStrip1 As System.Windows.Forms.StatusStrip
    Private lblQueryStatus As System.Windows.Forms.ToolStripStatusLabel
    Private lblRowsReturned As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents gvMain As Infragistics.Win.UltraWinGrid.UltraGrid
End Class

