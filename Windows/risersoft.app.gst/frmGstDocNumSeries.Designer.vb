<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGstDocNumSeries
    Inherits frmMax

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        InitForm()
    End Sub

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
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab6 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridGSTReg = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridNoSeries = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnAddSeries = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnRecalculate = New Infragistics.Win.Misc.UltraButton()
        Me.btnAuto = New Infragistics.Win.Misc.UltraButton()
        Me.btnUpdate = New Infragistics.Win.Misc.UltraButton()
        Me.btnChange = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_ReturnPeriodID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_CompName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.btnCancel1 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Panel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.btnDelSeries = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.UltraGridGSTReg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl6.SuspendLayout()
        CType(Me.UltraGridNoSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.ClientArea.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.cmb_ReturnPeriodID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.Panel1.ClientArea.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.UltraGridGSTReg)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(796, 336)
        '
        'UltraGridGSTReg
        '
        Me.UltraGridGSTReg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridGSTReg.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridGSTReg.Name = "UltraGridGSTReg"
        Me.UltraGridGSTReg.Size = New System.Drawing.Size(796, 336)
        Me.UltraGridGSTReg.TabIndex = 1
        Me.UltraGridGSTReg.Text = "Campus"
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Controls.Add(Me.UltraGridNoSeries)
        Me.UltraTabPageControl6.Controls.Add(Me.Panel4)
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6"
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(796, 336)
        '
        'UltraGridNoSeries
        '
        Me.UltraGridNoSeries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridNoSeries.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridNoSeries.Name = "UltraGridNoSeries"
        Me.UltraGridNoSeries.Size = New System.Drawing.Size(796, 304)
        Me.UltraGridNoSeries.TabIndex = 58
        Me.UltraGridNoSeries.Text = "Campus"
        '
        'Panel4
        '
        '
        'Panel4.ClientArea
        '
        Me.Panel4.ClientArea.Controls.Add(Me.btnDelSeries)
        Me.Panel4.ClientArea.Controls.Add(Me.btnAddSeries)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 304)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(796, 32)
        Me.Panel4.TabIndex = 76
        '
        'btnAddSeries
        '
        Appearance2.FontData.BoldAsString = "True"
        Me.btnAddSeries.Appearance = Appearance2
        Me.btnAddSeries.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAddSeries.Location = New System.Drawing.Point(715, 0)
        Me.btnAddSeries.Name = "btnAddSeries"
        Me.btnAddSeries.Size = New System.Drawing.Size(81, 32)
        Me.btnAddSeries.TabIndex = 76
        Me.btnAddSeries.Text = "&Add"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(796, 336)
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnRecalculate)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnAuto)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnUpdate)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnChange)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_ReturnPeriodID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel2)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel12)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.txt_CompName)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnCancel1)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(800, 84)
        Me.UltraPanel2.TabIndex = 51
        '
        'btnRecalculate
        '
        Appearance3.FontData.BoldAsString = "True"
        Me.btnRecalculate.Appearance = Appearance3
        Me.btnRecalculate.Location = New System.Drawing.Point(675, 47)
        Me.btnRecalculate.Name = "btnRecalculate"
        Me.btnRecalculate.Size = New System.Drawing.Size(81, 26)
        Me.btnRecalculate.TabIndex = 74
        Me.btnRecalculate.Text = "&Recalculate"
        '
        'btnAuto
        '
        Appearance4.FontData.BoldAsString = "True"
        Me.btnAuto.Appearance = Appearance4
        Me.btnAuto.Location = New System.Drawing.Point(588, 46)
        Me.btnAuto.Name = "btnAuto"
        Me.btnAuto.Size = New System.Drawing.Size(81, 26)
        Me.btnAuto.TabIndex = 73
        Me.btnAuto.Text = "&Auto"
        '
        'btnUpdate
        '
        Appearance5.FontData.BoldAsString = "True"
        Me.btnUpdate.Appearance = Appearance5
        Me.btnUpdate.Location = New System.Drawing.Point(332, 47)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(81, 26)
        Me.btnUpdate.TabIndex = 70
        Me.btnUpdate.Text = "&Update"
        '
        'btnChange
        '
        Appearance6.FontData.BoldAsString = "True"
        Me.btnChange.Appearance = Appearance6
        Me.btnChange.Location = New System.Drawing.Point(243, 46)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(81, 26)
        Me.btnChange.TabIndex = 69
        Me.btnChange.Text = "&Change"
        '
        'cmb_ReturnPeriodID
        '
        Me.cmb_ReturnPeriodID.Location = New System.Drawing.Point(106, 47)
        Me.cmb_ReturnPeriodID.Name = "cmb_ReturnPeriodID"
        Me.cmb_ReturnPeriodID.ReadOnly = True
        Me.cmb_ReturnPeriodID.Size = New System.Drawing.Size(129, 22)
        Me.cmb_ReturnPeriodID.TabIndex = 62
        '
        'UltraLabel2
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance7
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel2.Location = New System.Drawing.Point(16, 50)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(83, 14)
        Me.UltraLabel2.TabIndex = 59
        Me.UltraLabel2.Text = "Return Period :"
        '
        'UltraLabel12
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel12.Appearance = Appearance8
        Me.UltraLabel12.AutoSize = True
        Me.UltraLabel12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel12.Location = New System.Drawing.Point(6, 17)
        Me.UltraLabel12.Name = "UltraLabel12"
        Me.UltraLabel12.Size = New System.Drawing.Size(95, 14)
        Me.UltraLabel12.TabIndex = 55
        Me.UltraLabel12.Text = "Company Name :"
        '
        'txt_CompName
        '
        Me.txt_CompName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_CompName.Location = New System.Drawing.Point(106, 14)
        Me.txt_CompName.Name = "txt_CompName"
        Me.txt_CompName.ReadOnly = True
        Me.txt_CompName.Size = New System.Drawing.Size(365, 21)
        Me.txt_CompName.TabIndex = 54
        '
        'btnCancel1
        '
        Appearance9.FontData.BoldAsString = "True"
        Me.btnCancel1.Appearance = Appearance9
        Me.btnCancel1.Location = New System.Drawing.Point(244, 47)
        Me.btnCancel1.Name = "btnCancel1"
        Me.btnCancel1.Size = New System.Drawing.Size(81, 26)
        Me.btnCancel1.TabIndex = 72
        Me.btnCancel1.Text = "&Cancel"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl6)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 84)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance10.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance10
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(800, 362)
        Me.UltraTabControl1.TabIndex = 53
        Me.UltraTabControl1.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab5.TabPage = Me.UltraTabPageControl3
        UltraTab5.Text = "GstReg"
        UltraTab6.TabPage = Me.UltraTabPageControl6
        UltraTab6.Text = "Invoice Number Series"
        UltraTab1.Key = "status"
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Task Status"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab5, UltraTab6, UltraTab1})
        Me.UltraTabControl1.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(796, 336)
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "XLS Files|*.xls|XLSX Files|*.xlsx"
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
        Me.Panel1.Location = New System.Drawing.Point(0, 446)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 39)
        Me.Panel1.TabIndex = 74
        '
        'btnSave
        '
        Appearance11.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance11
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(596, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 39)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance12.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance12
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(664, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 39)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance13.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance13
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(732, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 39)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        '
        'btnDelSeries
        '
        Appearance1.FontData.BoldAsString = "True"
        Me.btnDelSeries.Appearance = Appearance1
        Me.btnDelSeries.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDelSeries.Location = New System.Drawing.Point(634, 0)
        Me.btnDelSeries.Name = "btnDelSeries"
        Me.btnDelSeries.Size = New System.Drawing.Size(81, 32)
        Me.btnDelSeries.TabIndex = 77
        Me.btnDelSeries.Text = "&Delete"
        '
        'frmGstDocNumSeries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "Invoice Number Series"
        Me.ClientSize = New System.Drawing.Size(800, 485)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.UltraPanel2)
        Me.Name = "frmGstDocNumSeries"
        Me.Text = "Invoice Number Series"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.UltraGridGSTReg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl6.ResumeLayout(False)
        CType(Me.UltraGridNoSeries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ClientArea.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.PerformLayout()
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.cmb_ReturnPeriodID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.Panel1.ClientArea.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnUpdate As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnChange As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmb_ReturnPeriodID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CompName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents btnCancel1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridGSTReg As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridNoSeries As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents btnRecalculate As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAuto As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents SaveFileDialog1 As Windows.Forms.SaveFileDialog
    Friend WithEvents Panel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnAddSeries As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Panel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnDelSeries As Infragistics.Win.Misc.UltraButton
End Class
