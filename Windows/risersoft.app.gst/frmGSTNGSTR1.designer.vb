Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmGSTNGSTR1
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents UltraGridSumm As Infragistics.Win.UltraWinGrid.UltraGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.UltraGridSummary = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridGSTReg = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraTabControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraGridSumm = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridTaskHistory = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnUpdate = New Infragistics.Win.Misc.UltraButton()
        Me.btnChange = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_GstRegID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_ReturnPeriodID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_PANNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_CompName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.btnCancel1 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnInvoiceSeries = New Infragistics.Win.Misc.UltraButton()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.GSTNToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SyncDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MultipleSyncDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SubmitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CleanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PDFFileInGSTNFormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsolidatedInvoiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsolidatedPaymentDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GSTNErrorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InvoiceDefermentReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteInvoiceDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeletePaymentDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteImportedOnlineDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateJSONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewModifiedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UltraPanel3 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnSync = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.UltraGridSummary, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.cmb_ReturnPeriodID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PANNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.UltraPanel3.ClientArea.SuspendLayout()
        Me.UltraPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.Panel3)
        Me.UltraTabPageControl4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(876, 203)
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.UltraGridSummary)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(876, 203)
        Me.Panel3.TabIndex = 16
        '
        'UltraGridSummary
        '
        Me.UltraGridSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridSummary.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridSummary.Name = "UltraGridSummary"
        Me.UltraGridSummary.Size = New System.Drawing.Size(876, 203)
        Me.UltraGridSummary.TabIndex = 0
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.UltraGridGSTReg)
        Me.UltraTabPageControl3.Controls.Add(Me.UltraPanel3)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(880, 364)
        '
        'UltraGridGSTReg
        '
        Me.UltraGridGSTReg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridGSTReg.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridGSTReg.Name = "UltraGridGSTReg"
        Me.UltraGridGSTReg.Size = New System.Drawing.Size(880, 332)
        Me.UltraGridGSTReg.TabIndex = 1
        Me.UltraGridGSTReg.Text = "Campus"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(880, 364)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraTabControl2)
        Me.Panel2.Controls.Add(Me.UltraGridSumm)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(880, 364)
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
        Me.UltraTabControl2.Size = New System.Drawing.Size(880, 229)
        Me.UltraTabControl2.TabIndex = 52
        Me.UltraTabControl2.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab1.TabPage = Me.UltraTabPageControl4
        UltraTab1.Text = "GSTN Summary"
        Me.UltraTabControl2.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1})
        Me.UltraTabControl2.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(876, 203)
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
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(880, 364)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraGridTaskHistory)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(880, 364)
        '
        'UltraGridTaskHistory
        '
        Me.UltraGridTaskHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridTaskHistory.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridTaskHistory.Name = "UltraGridTaskHistory"
        Me.UltraGridTaskHistory.Size = New System.Drawing.Size(880, 364)
        Me.UltraGridTaskHistory.TabIndex = 1
        Me.UltraGridTaskHistory.Text = "Campus"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl5)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 157)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance2.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance2
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(884, 390)
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab5.TabPage = Me.UltraTabPageControl3
        UltraTab5.Text = "GstReg"
        UltraTab4.TabPage = Me.UltraTabPageControl1
        UltraTab4.Text = "Summary"
        UltraTab3.Key = "status"
        UltraTab3.TabPage = Me.UltraTabPageControl5
        UltraTab3.Text = "Task Status"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Task History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab5, UltraTab4, UltraTab3, UltraTab2})
        Me.UltraTabControl1.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(880, 364)
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnUpdate)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnChange)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_GstRegID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_ReturnPeriodID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel3)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel2)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.txt_PANNum)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel12)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.txt_CompName)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnCancel1)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 79)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(884, 78)
        Me.UltraPanel2.TabIndex = 50
        '
        'btnUpdate
        '
        Appearance3.FontData.BoldAsString = "True"
        Me.btnUpdate.Appearance = Appearance3
        Me.btnUpdate.Location = New System.Drawing.Point(332, 44)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(81, 24)
        Me.btnUpdate.TabIndex = 70
        Me.btnUpdate.Text = "&Update"
        '
        'btnChange
        '
        Appearance4.FontData.BoldAsString = "True"
        Me.btnChange.Appearance = Appearance4
        Me.btnChange.Location = New System.Drawing.Point(243, 44)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(81, 24)
        Me.btnChange.TabIndex = 69
        Me.btnChange.Text = "&Change"
        '
        'cmb_GstRegID
        '
        Me.cmb_GstRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_GstRegID.Location = New System.Drawing.Point(653, 44)
        Me.cmb_GstRegID.Name = "cmb_GstRegID"
        Me.cmb_GstRegID.ReadOnly = True
        Me.cmb_GstRegID.Size = New System.Drawing.Size(213, 22)
        Me.cmb_GstRegID.TabIndex = 63
        '
        'cmb_ReturnPeriodID
        '
        Me.cmb_ReturnPeriodID.Location = New System.Drawing.Point(106, 44)
        Me.cmb_ReturnPeriodID.Name = "cmb_ReturnPeriodID"
        Me.cmb_ReturnPeriodID.ReadOnly = True
        Me.cmb_ReturnPeriodID.Size = New System.Drawing.Size(129, 22)
        Me.cmb_ReturnPeriodID.TabIndex = 62
        '
        'UltraLabel3
        '
        Me.UltraLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance5
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel3.Location = New System.Drawing.Point(600, 46)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(46, 14)
        Me.UltraLabel3.TabIndex = 61
        Me.UltraLabel3.Text = "GSTIN :"
        '
        'UltraLabel2
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance6
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel2.Location = New System.Drawing.Point(16, 46)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(83, 14)
        Me.UltraLabel2.TabIndex = 59
        Me.UltraLabel2.Text = "Return Period :"
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance7
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel1.Location = New System.Drawing.Point(593, 17)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(53, 14)
        Me.UltraLabel1.TabIndex = 57
        Me.UltraLabel1.Text = "PAN No :"
        '
        'txt_PANNum
        '
        Me.txt_PANNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PANNum.Location = New System.Drawing.Point(653, 14)
        Me.txt_PANNum.Name = "txt_PANNum"
        Me.txt_PANNum.ReadOnly = True
        Me.txt_PANNum.Size = New System.Drawing.Size(213, 21)
        Me.txt_PANNum.TabIndex = 56
        '
        'UltraLabel12
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel12.Appearance = Appearance8
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
        Me.txt_CompName.Size = New System.Drawing.Size(449, 21)
        Me.txt_CompName.TabIndex = 54
        '
        'btnCancel1
        '
        Appearance9.FontData.BoldAsString = "True"
        Me.btnCancel1.Appearance = Appearance9
        Me.btnCancel1.Location = New System.Drawing.Point(244, 44)
        Me.btnCancel1.Name = "btnCancel1"
        Me.btnCancel1.Size = New System.Drawing.Size(81, 24)
        Me.btnCancel1.TabIndex = 72
        Me.btnCancel1.Text = "&Cancel"
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnInvoiceSeries)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.MenuStrip1)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(884, 79)
        Me.UltraPanel1.TabIndex = 51
        '
        'btnInvoiceSeries
        '
        Me.btnInvoiceSeries.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance10.FontData.BoldAsString = "True"
        Me.btnInvoiceSeries.Appearance = Appearance10
        Me.btnInvoiceSeries.Location = New System.Drawing.Point(664, 39)
        Me.btnInvoiceSeries.Name = "btnInvoiceSeries"
        Me.btnInvoiceSeries.Size = New System.Drawing.Size(191, 24)
        Me.btnInvoiceSeries.TabIndex = 56
        Me.btnInvoiceSeries.Text = "&Invoice Number Template Series"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GSTNToolStripMenuItem, Me.DownloadToolStripMenuItem, Me.DeleteToolStripMenu, Me.GenerateJSONToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(884, 24)
        Me.MenuStrip1.TabIndex = 55
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GSTNToolStripMenuItem
        '
        Me.GSTNToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SyncDataToolStripMenuItem, Me.MultipleSyncDataToolStripMenuItem, Me.DownloadSummaryToolStripMenuItem, Me.SubmitToolStripMenuItem, Me.FileToolStripMenuItem, Me.CleanToolStripMenuItem})
        Me.GSTNToolStripMenuItem.Name = "GSTNToolStripMenuItem"
        Me.GSTNToolStripMenuItem.Size = New System.Drawing.Size(49, 20)
        Me.GSTNToolStripMenuItem.Text = "GSTN"
        '
        'SyncDataToolStripMenuItem
        '
        Me.SyncDataToolStripMenuItem.Name = "SyncDataToolStripMenuItem"
        Me.SyncDataToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.SyncDataToolStripMenuItem.Text = "Sync Data"
        '
        'MultipleSyncDataToolStripMenuItem
        '
        Me.MultipleSyncDataToolStripMenuItem.Name = "MultipleSyncDataToolStripMenuItem"
        Me.MultipleSyncDataToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.MultipleSyncDataToolStripMenuItem.Text = "Multiple Sync Data"
        '
        'DownloadSummaryToolStripMenuItem
        '
        Me.DownloadSummaryToolStripMenuItem.Name = "DownloadSummaryToolStripMenuItem"
        Me.DownloadSummaryToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.DownloadSummaryToolStripMenuItem.Text = "Download Summary"
        '
        'SubmitToolStripMenuItem
        '
        Me.SubmitToolStripMenuItem.Name = "SubmitToolStripMenuItem"
        Me.SubmitToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.SubmitToolStripMenuItem.Text = "Submit"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'CleanToolStripMenuItem
        '
        Me.CleanToolStripMenuItem.Name = "CleanToolStripMenuItem"
        Me.CleanToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.CleanToolStripMenuItem.Text = "Clean"
        '
        'DownloadToolStripMenuItem
        '
        Me.DownloadToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.PDFFileInGSTNFormatToolStripMenuItem, Me.ConsolidatedInvoiceToolStripMenuItem, Me.ConsolidatedPaymentDataToolStripMenuItem, Me.GSTNErrorToolStripMenuItem, Me.InvoiceDefermentReportToolStripMenuItem})
        Me.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem"
        Me.DownloadToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.DownloadToolStripMenuItem.Text = "Download"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel File in GSTN Format"
        '
        'PDFFileInGSTNFormatToolStripMenuItem
        '
        Me.PDFFileInGSTNFormatToolStripMenuItem.Name = "PDFFileInGSTNFormatToolStripMenuItem"
        Me.PDFFileInGSTNFormatToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.PDFFileInGSTNFormatToolStripMenuItem.Text = "PDF File in GSTN Format"
        '
        'ConsolidatedInvoiceToolStripMenuItem
        '
        Me.ConsolidatedInvoiceToolStripMenuItem.Name = "ConsolidatedInvoiceToolStripMenuItem"
        Me.ConsolidatedInvoiceToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.ConsolidatedInvoiceToolStripMenuItem.Text = "Consolidated Invoice Data"
        '
        'ConsolidatedPaymentDataToolStripMenuItem
        '
        Me.ConsolidatedPaymentDataToolStripMenuItem.Name = "ConsolidatedPaymentDataToolStripMenuItem"
        Me.ConsolidatedPaymentDataToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.ConsolidatedPaymentDataToolStripMenuItem.Text = "Consolidated Payment Data"
        '
        'GSTNErrorToolStripMenuItem
        '
        Me.GSTNErrorToolStripMenuItem.Name = "GSTNErrorToolStripMenuItem"
        Me.GSTNErrorToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.GSTNErrorToolStripMenuItem.Text = "GSTN Error Report"
        '
        'InvoiceDefermentReportToolStripMenuItem
        '
        Me.InvoiceDefermentReportToolStripMenuItem.Name = "InvoiceDefermentReportToolStripMenuItem"
        Me.InvoiceDefermentReportToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.InvoiceDefermentReportToolStripMenuItem.Text = "Invoice Deferment Report"
        '
        'DeleteToolStripMenu
        '
        Me.DeleteToolStripMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteInvoiceDataToolStripMenuItem, Me.DeletePaymentDataToolStripMenuItem, Me.DeleteImportedOnlineDataToolStripMenuItem})
        Me.DeleteToolStripMenu.Name = "DeleteToolStripMenu"
        Me.DeleteToolStripMenu.Size = New System.Drawing.Size(52, 20)
        Me.DeleteToolStripMenu.Text = "Delete"
        '
        'DeleteInvoiceDataToolStripMenuItem
        '
        Me.DeleteInvoiceDataToolStripMenuItem.Name = "DeleteInvoiceDataToolStripMenuItem"
        Me.DeleteInvoiceDataToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.DeleteInvoiceDataToolStripMenuItem.Text = "Delete Invoice Data"
        '
        'DeletePaymentDataToolStripMenuItem
        '
        Me.DeletePaymentDataToolStripMenuItem.Name = "DeletePaymentDataToolStripMenuItem"
        Me.DeletePaymentDataToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.DeletePaymentDataToolStripMenuItem.Text = "Delete Payment Data"
        '
        'DeleteImportedOnlineDataToolStripMenuItem
        '
        Me.DeleteImportedOnlineDataToolStripMenuItem.Name = "DeleteImportedOnlineDataToolStripMenuItem"
        Me.DeleteImportedOnlineDataToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.DeleteImportedOnlineDataToolStripMenuItem.Text = "Delete Imported/Online Data"
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
        Me.UltraPanel3.ClientArea.Controls.Add(Me.btnSync)
        Me.UltraPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel3.Location = New System.Drawing.Point(0, 332)
        Me.UltraPanel3.Name = "UltraPanel3"
        Me.UltraPanel3.Size = New System.Drawing.Size(880, 32)
        Me.UltraPanel3.TabIndex = 57
        '
        'btnSync
        '
        Me.btnSync.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSync.Location = New System.Drawing.Point(796, 0)
        Me.btnSync.Name = "btnSync"
        Me.btnSync.Size = New System.Drawing.Size(84, 32)
        Me.btnSync.TabIndex = 8
        Me.btnSync.Text = "&Sync"
        '
        'frmGSTNGSTR1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "GSTR1 Data Sync"
        Me.ClientSize = New System.Drawing.Size(884, 547)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.UltraPanel2)
        Me.Controls.Add(Me.UltraPanel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmGSTNGSTR1"
        Me.Text = "GSTR1 Data Sync"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.UltraGridSummary, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.cmb_ReturnPeriodID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PANNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.PerformLayout()
        Me.UltraPanel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.UltraPanel3.ClientArea.ResumeLayout(False)
        Me.UltraPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents UltraGridTaskHistory As UltraGrid
    Friend WithEvents btnSeries As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabControl2 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents UltraGridSummary As UltraGrid
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridGSTReg As UltraGrid
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmb_ReturnPeriodID As UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_PANNum As UltraTextEditor
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CompName As UltraTextEditor
    Friend WithEvents GSTN As Windows.Forms.MenuStrip
    Friend WithEvents cmb_GstRegID As UltraCombo
    Friend WithEvents ToolStripMenuItem1 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents PDFFileInToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConsolidatedToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents MenuStrip1 As Windows.Forms.MenuStrip
    Friend WithEvents GSTNToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SyncDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents PDFFileInGSTNFormatToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConsolidatedInvoiceToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConsolidatedPaymentDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GSTNErrorToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenu As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteInvoiceDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateJSONToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewModifiedToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnInvoiceSeries As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnUpdate As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnChange As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents InvoiceDefermentReportToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeletePaymentDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteImportedOnlineDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MultipleSyncDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadSummaryToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SubmitToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CleanToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UltraPanel3 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnSync As Infragistics.Win.Misc.UltraButton

#End Region
End Class



