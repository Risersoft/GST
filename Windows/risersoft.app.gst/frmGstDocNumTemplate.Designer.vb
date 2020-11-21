<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGstDocNumTemplate
    Inherits frmMax

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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.cmb_Level = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.btnNewSeries = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_CompanyID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabelCompany = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_GSTRegID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabelGSTReg = New Infragistics.Win.Misc.UltraLabel()
        Me.Panel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.UltraGridLevel = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelSeries = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.cmb_Level, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_GSTRegID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.ClientArea.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.UltraGridLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.ClientArea.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_Level)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnNewSeries)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_CompanyID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabelCompany)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_GSTRegID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabelGSTReg)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(834, 84)
        Me.UltraPanel2.TabIndex = 66
        '
        'cmb_Level
        '
        Me.cmb_Level.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Level.Location = New System.Drawing.Point(96, 13)
        Me.cmb_Level.Name = "cmb_Level"
        Me.cmb_Level.Size = New System.Drawing.Size(211, 21)
        Me.cmb_Level.TabIndex = 69
        '
        'btnNewSeries
        '
        Appearance1.FontData.BoldAsString = "True"
        Me.btnNewSeries.Appearance = Appearance1
        Me.btnNewSeries.Location = New System.Drawing.Point(557, 13)
        Me.btnNewSeries.Name = "btnNewSeries"
        Me.btnNewSeries.Size = New System.Drawing.Size(116, 31)
        Me.btnNewSeries.TabIndex = 68
        Me.btnNewSeries.Text = "Add New Series"
        '
        'cmb_CompanyID
        '
        Me.cmb_CompanyID.Location = New System.Drawing.Point(95, 51)
        Me.cmb_CompanyID.Name = "cmb_CompanyID"
        Me.cmb_CompanyID.Size = New System.Drawing.Size(213, 22)
        Me.cmb_CompanyID.TabIndex = 65
        '
        'UltraLabelCompany
        '
        Appearance2.FontData.SizeInPoints = 8.25!
        Appearance2.TextHAlignAsString = "Right"
        Appearance2.TextVAlignAsString = "Middle"
        Me.UltraLabelCompany.Appearance = Appearance2
        Me.UltraLabelCompany.AutoSize = True
        Me.UltraLabelCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabelCompany.Location = New System.Drawing.Point(27, 53)
        Me.UltraLabelCompany.Name = "UltraLabelCompany"
        Me.UltraLabelCompany.Size = New System.Drawing.Size(61, 14)
        Me.UltraLabelCompany.TabIndex = 64
        Me.UltraLabelCompany.Text = "Company :"
        '
        'UltraLabel1
        '
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        Appearance3.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance3
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel1.Location = New System.Drawing.Point(50, 16)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(39, 14)
        Me.UltraLabel1.TabIndex = 61
        Me.UltraLabel1.Text = "Level :"
        '
        'cmb_GSTRegID
        '
        Me.cmb_GSTRegID.Location = New System.Drawing.Point(95, 51)
        Me.cmb_GSTRegID.Name = "cmb_GSTRegID"
        Me.cmb_GSTRegID.Size = New System.Drawing.Size(213, 22)
        Me.cmb_GSTRegID.TabIndex = 71
        '
        'UltraLabelGSTReg
        '
        Me.UltraLabelGSTReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabelGSTReg.Appearance = Appearance4
        Me.UltraLabelGSTReg.AutoSize = True
        Me.UltraLabelGSTReg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabelGSTReg.Location = New System.Drawing.Point(33, 53)
        Me.UltraLabelGSTReg.Name = "UltraLabelGSTReg"
        Me.UltraLabelGSTReg.Size = New System.Drawing.Size(56, 14)
        Me.UltraLabelGSTReg.TabIndex = 70
        Me.UltraLabelGSTReg.Text = "GSTReg :"
        '
        'Panel1
        '
        '
        'Panel1.ClientArea
        '
        Me.Panel1.ClientArea.Controls.Add(Me.btnSave)
        Me.Panel1.ClientArea.Controls.Add(Me.btnCancel)
        Me.Panel1.ClientArea.Controls.Add(Me.btnOK)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 382)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(834, 39)
        Me.Panel1.TabIndex = 74
        '
        'btnSave
        '
        Appearance5.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance5
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(630, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 39)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance6.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance6
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(698, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 39)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance7.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance7
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(766, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 39)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        '
        'UltraGridLevel
        '
        Me.UltraGridLevel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridLevel.Location = New System.Drawing.Point(0, 84)
        Me.UltraGridLevel.Name = "UltraGridLevel"
        Me.UltraGridLevel.Size = New System.Drawing.Size(834, 266)
        Me.UltraGridLevel.TabIndex = 73
        Me.UltraGridLevel.Text = "Campus"
        '
        'Panel4
        '
        '
        'Panel4.ClientArea
        '
        Me.Panel4.ClientArea.Controls.Add(Me.btnDelSeries)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 350)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(834, 32)
        Me.Panel4.TabIndex = 75
        '
        'btnDelSeries
        '
        Me.btnDelSeries.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDelSeries.Location = New System.Drawing.Point(750, 0)
        Me.btnDelSeries.Name = "btnDelSeries"
        Me.btnDelSeries.Size = New System.Drawing.Size(84, 32)
        Me.btnDelSeries.TabIndex = 7
        Me.btnDelSeries.Text = "&Delete"
        '
        'frmGstDocNumTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "GST Document Number Series Settings"
        Me.ClientSize = New System.Drawing.Size(834, 421)
        Me.Controls.Add(Me.UltraGridLevel)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.UltraPanel2)
        Me.Name = "frmGstDocNumTemplate"
        Me.Text = "GST Document Number Series Settings"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.PerformLayout()
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.cmb_Level, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_GSTRegID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ClientArea.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.UltraGridLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ClientArea.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_CompanyID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabelCompany As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Panel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraGridLevel As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents btnNewSeries As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmb_Level As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents cmb_GSTRegID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabelGSTReg As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Panel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelSeries As Infragistics.Win.Misc.UltraButton
End Class
