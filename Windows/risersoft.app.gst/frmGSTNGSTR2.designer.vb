Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmGSTNGSTR2
    Inherits frmMax

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        InitForm()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab6 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.UltraGridCPSumm = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridGSTReg = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraTabControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraGridSumm = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridTaskHistory = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.cmb_GstRegID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_PANNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_CompName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnChange = New Infragistics.Win.Misc.UltraButton()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.GSTNToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadCounterPartyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReconcileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PerformReconciliationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmailMismatchReportToVendorsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualReconciliationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsolidatedInvoiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsolidatedPaymentDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelFileForToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelFileForManualReconciliationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteTaxPayerInvoiceDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteTaxPayerPaymentDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteCounterPartyDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateJSONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewModifiedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UltraPanel3 = New Infragistics.Win.Misc.UltraPanel()
        Me.cmb_ToID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabelTo = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_FromID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabelFrom = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_Period = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.cmb_Operation = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_ReturnPeriodID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.btnUpdate = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel1 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraPanel4 = New Infragistics.Win.Misc.UltraPanel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.UltraGridCPSumm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.UltraGridGSTReg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl2.SuspendLayout()
        CType(Me.UltraGridSumm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.UltraGridTaskHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.cmb_GstRegID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PANNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.UltraPanel3.ClientArea.SuspendLayout()
        Me.UltraPanel3.SuspendLayout()
        CType(Me.cmb_ToID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_FromID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_Period, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_Operation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_ReturnPeriodID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel4.ClientArea.SuspendLayout()
        Me.UltraPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.Panel3)
        Me.UltraTabPageControl4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(876, 122)
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.UltraGridCPSumm)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(876, 122)
        Me.Panel3.TabIndex = 16
        '
        'UltraGridCPSumm
        '
        Me.UltraGridCPSumm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridCPSumm.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridCPSumm.Name = "UltraGridCPSumm"
        Me.UltraGridCPSumm.Size = New System.Drawing.Size(876, 122)
        Me.UltraGridCPSumm.TabIndex = 0
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.UltraGridGSTReg)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(880, 283)
        '
        'UltraGridGSTReg
        '
        Me.UltraGridGSTReg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridGSTReg.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridGSTReg.Name = "UltraGridGSTReg"
        Me.UltraGridGSTReg.Size = New System.Drawing.Size(880, 283)
        Me.UltraGridGSTReg.TabIndex = 1
        Me.UltraGridGSTReg.Text = "Campus"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(880, 283)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraTabControl2)
        Me.Panel2.Controls.Add(Me.UltraGridSumm)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(880, 283)
        Me.Panel2.TabIndex = 16
        '
        'UltraTabControl2
        '
        Me.UltraTabControl2.Controls.Add(Me.UltraTabSharedControlsPage2)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl2.Location = New System.Drawing.Point(0, 135)
        Me.UltraTabControl2.Name = "UltraTabControl2"
        Appearance1.FontData.BoldAsString = "True"
        Me.UltraTabControl2.SelectedTabAppearance = Appearance1
        Me.UltraTabControl2.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.UltraTabControl2.Size = New System.Drawing.Size(880, 148)
        Me.UltraTabControl2.TabIndex = 52
        Me.UltraTabControl2.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab1.TabPage = Me.UltraTabPageControl4
        UltraTab1.Text = "Counter Party Summary"
        Me.UltraTabControl2.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1})
        Me.UltraTabControl2.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(876, 122)
        '
        'UltraGridSumm
        '
        Me.UltraGridSumm.AllowDrop = True
        Me.UltraGridSumm.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraGridSumm.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridSumm.Name = "UltraGridSumm"
        Me.UltraGridSumm.Size = New System.Drawing.Size(880, 135)
        Me.UltraGridSumm.TabIndex = 0
        Me.UltraGridSumm.Text = "Campus"
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6"
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(880, 283)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraGridTaskHistory)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(880, 319)
        '
        'UltraGridTaskHistory
        '
        Me.UltraGridTaskHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridTaskHistory.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridTaskHistory.Name = "UltraGridTaskHistory"
        Me.UltraGridTaskHistory.Size = New System.Drawing.Size(880, 319)
        Me.UltraGridTaskHistory.TabIndex = 1
        Me.UltraGridTaskHistory.Text = "Campus"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl6)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 202)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance2.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance2
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(884, 345)
        Me.UltraTabControl1.TabIndex = 52
        Me.UltraTabControl1.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab5.TabPage = Me.UltraTabPageControl3
        UltraTab5.Text = "GstReg"
        UltraTab4.TabPage = Me.UltraTabPageControl1
        UltraTab4.Text = "Summary"
        UltraTab6.Key = "status"
        UltraTab6.TabPage = Me.UltraTabPageControl6
        UltraTab6.Text = "Task Status"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Task History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab5, UltraTab4, UltraTab6, UltraTab2})
        Me.UltraTabControl1.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(880, 319)
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_GstRegID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel3)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.txt_PANNum)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel12)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.txt_CompName)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 124)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(884, 78)
        Me.UltraPanel2.TabIndex = 53
        '
        'cmb_GstRegID
        '
        Me.cmb_GstRegID.Location = New System.Drawing.Point(106, 43)
        Me.cmb_GstRegID.Name = "cmb_GstRegID"
        Me.cmb_GstRegID.ReadOnly = True
        Me.cmb_GstRegID.Size = New System.Drawing.Size(213, 22)
        Me.cmb_GstRegID.TabIndex = 63
        '
        'UltraLabel3
        '
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        Appearance3.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance3
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel3.Location = New System.Drawing.Point(54, 45)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(46, 14)
        Me.UltraLabel3.TabIndex = 61
        Me.UltraLabel3.Text = "GSTIN :"
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance4
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel1.Location = New System.Drawing.Point(594, 15)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(53, 14)
        Me.UltraLabel1.TabIndex = 57
        Me.UltraLabel1.Text = "PAN No :"
        '
        'txt_PANNum
        '
        Me.txt_PANNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PANNum.Location = New System.Drawing.Point(654, 12)
        Me.txt_PANNum.Name = "txt_PANNum"
        Me.txt_PANNum.ReadOnly = True
        Me.txt_PANNum.Size = New System.Drawing.Size(213, 21)
        Me.txt_PANNum.TabIndex = 56
        '
        'UltraLabel12
        '
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel12.Appearance = Appearance5
        Me.UltraLabel12.AutoSize = True
        Me.UltraLabel12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel12.Location = New System.Drawing.Point(6, 16)
        Me.UltraLabel12.Name = "UltraLabel12"
        Me.UltraLabel12.Size = New System.Drawing.Size(95, 14)
        Me.UltraLabel12.TabIndex = 55
        Me.UltraLabel12.Text = "Company Name :"
        '
        'txt_CompName
        '
        Me.txt_CompName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_CompName.Location = New System.Drawing.Point(106, 13)
        Me.txt_CompName.Name = "txt_CompName"
        Me.txt_CompName.ReadOnly = True
        Me.txt_CompName.Size = New System.Drawing.Size(473, 21)
        Me.txt_CompName.TabIndex = 54
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnChange)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraLabel4)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.MenuStrip1)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(884, 62)
        Me.UltraPanel1.TabIndex = 54
        '
        'btnChange
        '
        Appearance6.FontData.BoldAsString = "True"
        Me.btnChange.Appearance = Appearance6
        Me.btnChange.Location = New System.Drawing.Point(109, 30)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(67, 24)
        Me.btnChange.TabIndex = 64
        Me.btnChange.Text = "&Change"
        '
        'UltraLabel4
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance7
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel4.Location = New System.Drawing.Point(35, 34)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(66, 14)
        Me.UltraLabel4.TabIndex = 63
        Me.UltraLabel4.Text = "Parameter :"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GSTNToolStripMenuItem, Me.ReconcileToolStripMenuItem, Me.DownloadToolStripMenuItem, Me.DeleteToolStripMenu, Me.GenerateJSONToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(884, 24)
        Me.MenuStrip1.TabIndex = 55
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GSTNToolStripMenuItem
        '
        Me.GSTNToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadCounterPartyToolStripMenuItem})
        Me.GSTNToolStripMenuItem.Name = "GSTNToolStripMenuItem"
        Me.GSTNToolStripMenuItem.Size = New System.Drawing.Size(49, 20)
        Me.GSTNToolStripMenuItem.Text = "GSTN"
        '
        'DownloadCounterPartyToolStripMenuItem
        '
        Me.DownloadCounterPartyToolStripMenuItem.Name = "DownloadCounterPartyToolStripMenuItem"
        Me.DownloadCounterPartyToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.DownloadCounterPartyToolStripMenuItem.Text = "Download Counter Party"
        '
        'ReconcileToolStripMenuItem
        '
        Me.ReconcileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PerformReconciliationToolStripMenuItem, Me.GenerateReportToolStripMenuItem, Me.EmailMismatchReportToVendorsToolStripMenuItem, Me.ManualReconciliationToolStripMenuItem})
        Me.ReconcileToolStripMenuItem.Name = "ReconcileToolStripMenuItem"
        Me.ReconcileToolStripMenuItem.Size = New System.Drawing.Size(70, 20)
        Me.ReconcileToolStripMenuItem.Text = "Reconcile"
        '
        'PerformReconciliationToolStripMenuItem
        '
        Me.PerformReconciliationToolStripMenuItem.Name = "PerformReconciliationToolStripMenuItem"
        Me.PerformReconciliationToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.PerformReconciliationToolStripMenuItem.Text = "Perform Reconciliation"
        '
        'GenerateReportToolStripMenuItem
        '
        Me.GenerateReportToolStripMenuItem.Name = "GenerateReportToolStripMenuItem"
        Me.GenerateReportToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.GenerateReportToolStripMenuItem.Text = "Generate Report"
        '
        'EmailMismatchReportToVendorsToolStripMenuItem
        '
        Me.EmailMismatchReportToVendorsToolStripMenuItem.Name = "EmailMismatchReportToVendorsToolStripMenuItem"
        Me.EmailMismatchReportToVendorsToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.EmailMismatchReportToVendorsToolStripMenuItem.Text = "Email Mismatch Report To Vendors"
        '
        'ManualReconciliationToolStripMenuItem
        '
        Me.ManualReconciliationToolStripMenuItem.Name = "ManualReconciliationToolStripMenuItem"
        Me.ManualReconciliationToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.ManualReconciliationToolStripMenuItem.Text = "Manual Reconciliation"
        '
        'DownloadToolStripMenuItem
        '
        Me.DownloadToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.ConsolidatedInvoiceToolStripMenuItem, Me.ConsolidatedPaymentDataToolStripMenuItem, Me.ExcelFileForToolStripMenuItem, Me.ExcelFileForManualReconciliationToolStripMenuItem})
        Me.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem"
        Me.DownloadToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.DownloadToolStripMenuItem.Text = "Download"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel File in GSTN Format"
        '
        'ConsolidatedInvoiceToolStripMenuItem
        '
        Me.ConsolidatedInvoiceToolStripMenuItem.Name = "ConsolidatedInvoiceToolStripMenuItem"
        Me.ConsolidatedInvoiceToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.ConsolidatedInvoiceToolStripMenuItem.Text = "Consolidated Invoice Data"
        '
        'ConsolidatedPaymentDataToolStripMenuItem
        '
        Me.ConsolidatedPaymentDataToolStripMenuItem.Name = "ConsolidatedPaymentDataToolStripMenuItem"
        Me.ConsolidatedPaymentDataToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.ConsolidatedPaymentDataToolStripMenuItem.Text = "Consolidated Payment Data"
        '
        'ExcelFileForToolStripMenuItem
        '
        Me.ExcelFileForToolStripMenuItem.Name = "ExcelFileForToolStripMenuItem"
        Me.ExcelFileForToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.ExcelFileForToolStripMenuItem.Text = "Excel File for Counter Party Data"
        '
        'ExcelFileForManualReconciliationToolStripMenuItem
        '
        Me.ExcelFileForManualReconciliationToolStripMenuItem.Name = "ExcelFileForManualReconciliationToolStripMenuItem"
        Me.ExcelFileForManualReconciliationToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.ExcelFileForManualReconciliationToolStripMenuItem.Text = "Excel File for Manual Reconciliation"
        '
        'DeleteToolStripMenu
        '
        Me.DeleteToolStripMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteTaxPayerInvoiceDataToolStripMenuItem, Me.DeleteTaxPayerPaymentDataToolStripMenuItem, Me.DeleteCounterPartyDataToolStripMenuItem})
        Me.DeleteToolStripMenu.Name = "DeleteToolStripMenu"
        Me.DeleteToolStripMenu.Size = New System.Drawing.Size(52, 20)
        Me.DeleteToolStripMenu.Text = "Delete"
        '
        'DeleteTaxPayerInvoiceDataToolStripMenuItem
        '
        Me.DeleteTaxPayerInvoiceDataToolStripMenuItem.Name = "DeleteTaxPayerInvoiceDataToolStripMenuItem"
        Me.DeleteTaxPayerInvoiceDataToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.DeleteTaxPayerInvoiceDataToolStripMenuItem.Text = "Delete TaxPayer Invoice Data"
        '
        'DeleteTaxPayerPaymentDataToolStripMenuItem
        '
        Me.DeleteTaxPayerPaymentDataToolStripMenuItem.Name = "DeleteTaxPayerPaymentDataToolStripMenuItem"
        Me.DeleteTaxPayerPaymentDataToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.DeleteTaxPayerPaymentDataToolStripMenuItem.Text = "Delete TaxPayer Payment Data"
        '
        'DeleteCounterPartyDataToolStripMenuItem
        '
        Me.DeleteCounterPartyDataToolStripMenuItem.Name = "DeleteCounterPartyDataToolStripMenuItem"
        Me.DeleteCounterPartyDataToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.DeleteCounterPartyDataToolStripMenuItem.Text = "Delete Counter Party Data"
        '
        'GenerateJSONToolStripMenuItem
        '
        Me.GenerateJSONToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewModifiedToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.GenerateJSONToolStripMenuItem.Name = "GenerateJSONToolStripMenuItem"
        Me.GenerateJSONToolStripMenuItem.Size = New System.Drawing.Size(97, 20)
        Me.GenerateJSONToolStripMenuItem.Text = "Generate JSON"
        '
        'NewModifiedToolStripMenuItem
        '
        Me.NewModifiedToolStripMenuItem.Name = "NewModifiedToolStripMenuItem"
        Me.NewModifiedToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.NewModifiedToolStripMenuItem.Text = "New/Modified"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'UltraPanel3
        '
        '
        'UltraPanel3.ClientArea
        '
        Me.UltraPanel3.ClientArea.Controls.Add(Me.cmb_ToID)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.UltraLabelTo)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.cmb_FromID)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.UltraLabelFrom)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.cmb_Period)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.cmb_Operation)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.UltraLabel7)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.UltraLabel6)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.cmb_ReturnPeriodID)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.UltraLabel2)
        Me.UltraPanel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel3.Location = New System.Drawing.Point(0, 62)
        Me.UltraPanel3.Name = "UltraPanel3"
        Me.UltraPanel3.Size = New System.Drawing.Size(884, 32)
        Me.UltraPanel3.TabIndex = 65
        '
        'cmb_ToID
        '
        Me.cmb_ToID.Location = New System.Drawing.Point(784, 5)
        Me.cmb_ToID.Name = "cmb_ToID"
        Me.cmb_ToID.ReadOnly = True
        Me.cmb_ToID.Size = New System.Drawing.Size(88, 22)
        Me.cmb_ToID.TabIndex = 74
        '
        'UltraLabelTo
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabelTo.Appearance = Appearance8
        Me.UltraLabelTo.AutoSize = True
        Me.UltraLabelTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabelTo.Location = New System.Drawing.Point(754, 8)
        Me.UltraLabelTo.Name = "UltraLabelTo"
        Me.UltraLabelTo.Size = New System.Drawing.Size(24, 14)
        Me.UltraLabelTo.TabIndex = 73
        Me.UltraLabelTo.Text = "To :"
        '
        'cmb_FromID
        '
        Me.cmb_FromID.Location = New System.Drawing.Point(644, 5)
        Me.cmb_FromID.Name = "cmb_FromID"
        Me.cmb_FromID.ReadOnly = True
        Me.cmb_FromID.Size = New System.Drawing.Size(88, 22)
        Me.cmb_FromID.TabIndex = 72
        '
        'UltraLabelFrom
        '
        Appearance9.FontData.SizeInPoints = 8.25!
        Appearance9.TextHAlignAsString = "Right"
        Appearance9.TextVAlignAsString = "Middle"
        Me.UltraLabelFrom.Appearance = Appearance9
        Me.UltraLabelFrom.AutoSize = True
        Me.UltraLabelFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabelFrom.Location = New System.Drawing.Point(600, 8)
        Me.UltraLabelFrom.Name = "UltraLabelFrom"
        Me.UltraLabelFrom.Size = New System.Drawing.Size(38, 14)
        Me.UltraLabelFrom.TabIndex = 71
        Me.UltraLabelFrom.Text = "From :"
        '
        'cmb_Period
        '
        Me.cmb_Period.Location = New System.Drawing.Point(421, 6)
        Me.cmb_Period.Name = "cmb_Period"
        Me.cmb_Period.ReadOnly = True
        Me.cmb_Period.Size = New System.Drawing.Size(168, 21)
        Me.cmb_Period.TabIndex = 70
        '
        'cmb_Operation
        '
        Me.cmb_Operation.Location = New System.Drawing.Point(258, 6)
        Me.cmb_Operation.Name = "cmb_Operation"
        Me.cmb_Operation.ReadOnly = True
        Me.cmb_Operation.Size = New System.Drawing.Size(98, 21)
        Me.cmb_Operation.TabIndex = 69
        '
        'UltraLabel7
        '
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.TextHAlignAsString = "Right"
        Appearance10.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance10
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel7.Location = New System.Drawing.Point(370, 9)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(45, 14)
        Me.UltraLabel7.TabIndex = 65
        Me.UltraLabel7.Text = "Period :"
        '
        'UltraLabel6
        '
        Appearance11.FontData.SizeInPoints = 8.25!
        Appearance11.TextHAlignAsString = "Right"
        Appearance11.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance11
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel6.Location = New System.Drawing.Point(208, 8)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(44, 14)
        Me.UltraLabel6.TabIndex = 63
        Me.UltraLabel6.Text = "Scope :"
        '
        'cmb_ReturnPeriodID
        '
        Me.cmb_ReturnPeriodID.Location = New System.Drawing.Point(97, 5)
        Me.cmb_ReturnPeriodID.Name = "cmb_ReturnPeriodID"
        Me.cmb_ReturnPeriodID.ReadOnly = True
        Me.cmb_ReturnPeriodID.Size = New System.Drawing.Size(98, 22)
        Me.cmb_ReturnPeriodID.TabIndex = 62
        '
        'UltraLabel2
        '
        Appearance12.FontData.SizeInPoints = 8.25!
        Appearance12.TextHAlignAsString = "Right"
        Appearance12.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance12
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel2.Location = New System.Drawing.Point(8, 8)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(83, 14)
        Me.UltraLabel2.TabIndex = 59
        Me.UltraLabel2.Text = "Return Period :"
        '
        'btnUpdate
        '
        Appearance13.FontData.BoldAsString = "True"
        Me.btnUpdate.Appearance = Appearance13
        Me.btnUpdate.Location = New System.Drawing.Point(114, 4)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(81, 24)
        Me.btnUpdate.TabIndex = 68
        Me.btnUpdate.Text = "&Update"
        '
        'btnCancel1
        '
        Appearance14.FontData.BoldAsString = "True"
        Me.btnCancel1.Appearance = Appearance14
        Me.btnCancel1.Location = New System.Drawing.Point(23, 4)
        Me.btnCancel1.Name = "btnCancel1"
        Me.btnCancel1.Size = New System.Drawing.Size(81, 24)
        Me.btnCancel1.TabIndex = 67
        Me.btnCancel1.Text = "&Cancel"
        '
        'UltraPanel4
        '
        '
        'UltraPanel4.ClientArea
        '
        Me.UltraPanel4.ClientArea.Controls.Add(Me.btnUpdate)
        Me.UltraPanel4.ClientArea.Controls.Add(Me.btnCancel1)
        Me.UltraPanel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel4.Location = New System.Drawing.Point(0, 94)
        Me.UltraPanel4.Name = "UltraPanel4"
        Me.UltraPanel4.Size = New System.Drawing.Size(884, 30)
        Me.UltraPanel4.TabIndex = 71
        '
        'frmGSTNGSTR2
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Vendor Reconciliation"
        Me.ClientSize = New System.Drawing.Size(884, 547)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.UltraPanel2)
        Me.Controls.Add(Me.UltraPanel4)
        Me.Controls.Add(Me.UltraPanel3)
        Me.Controls.Add(Me.UltraPanel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGSTNGSTR2"
        Me.Text = "Vendor Reconciliation"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.UltraGridCPSumm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.UltraGridGSTReg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl2.ResumeLayout(False)
        CType(Me.UltraGridSumm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.UltraGridTaskHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.PerformLayout()
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.cmb_GstRegID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PANNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.PerformLayout()
        Me.UltraPanel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.UltraPanel3.ClientArea.ResumeLayout(False)
        Me.UltraPanel3.ClientArea.PerformLayout()
        Me.UltraPanel3.ResumeLayout(False)
        CType(Me.cmb_ToID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_FromID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_Period, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_Operation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_ReturnPeriodID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel4.ClientArea.ResumeLayout(False)
        Me.UltraPanel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents UltraTabControl2 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents UltraGridCPSumm As UltraGrid
    Friend WithEvents UltraGridSumm As UltraGrid
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridTaskHistory As UltraGrid
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridGSTReg As UltraGrid
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmb_GstRegID As UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_PANNum As UltraTextEditor
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CompName As UltraTextEditor
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents MenuStrip1 As Windows.Forms.MenuStrip
    Friend WithEvents GSTNToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConsolidatedInvoiceToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConsolidatedPaymentDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenu As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateJSONToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewModifiedToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadCounterPartyToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReconcileToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents PerformReconciliationToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateReportToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmailMismatchReportToVendorsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelFileForToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteTaxPayerInvoiceDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents btnChange As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraPanel3 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnUpdate As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmb_Period As UltraComboEditor
    Friend WithEvents cmb_Operation As UltraComboEditor
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_ReturnPeriodID As UltraCombo
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraPanel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmb_ToID As UltraCombo
    Friend WithEvents UltraLabelTo As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_FromID As UltraCombo
    Friend WithEvents UltraLabelFrom As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents ManualReconciliationToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelFileForManualReconciliationToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteTaxPayerPaymentDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteCounterPartyDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem

#End Region
End Class

