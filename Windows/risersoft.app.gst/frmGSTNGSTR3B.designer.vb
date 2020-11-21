Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmGSTNGSTR3B
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
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab7 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance39 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance40 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance41 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance42 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraGridPaymentDetail = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridGSTReg = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraGridSumm = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage3 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.Panel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnLiabLedger = New Infragistics.Win.Misc.UltraButton()
        Me.btnITCLedger = New Infragistics.Win.Misc.UltraButton()
        Me.btnCashLedger = New Infragistics.Win.Misc.UltraButton()
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton()
        Me.btnOffset = New Infragistics.Win.Misc.UltraButton()
        Me.UltraGridPayment = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridTaskHistory = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl7 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridITCRev = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel3 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelITC = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddITC = New Infragistics.Win.Misc.UltraButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnUpdate = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_GstRegID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_ReturnPeriodID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_PANNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_CompName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.btnChange = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel1 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.GSTNToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PopulatePaymentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateJSONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UltraPanel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.FinalTaxSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CalculatedTaxSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PaymentTaxSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.UltraGridPaymentDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.UltraGridGSTReg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.UltraGridSumm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl6.SuspendLayout()
        CType(Me.UltraTabControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl3.SuspendLayout()
        Me.Panel4.ClientArea.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.UltraGridPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.UltraGridTaskHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl7.SuspendLayout()
        CType(Me.UltraGridITCRev, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel3.ClientArea.SuspendLayout()
        Me.UltraPanel3.SuspendLayout()
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
        Me.UltraPanel4.ClientArea.SuspendLayout()
        Me.UltraPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(876, 163)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UltraGridPaymentDetail)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(876, 163)
        Me.Panel1.TabIndex = 16
        '
        'UltraGridPaymentDetail
        '
        Me.UltraGridPaymentDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridPaymentDetail.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridPaymentDetail.Name = "UltraGridPaymentDetail"
        Me.UltraGridPaymentDetail.Size = New System.Drawing.Size(876, 163)
        Me.UltraGridPaymentDetail.TabIndex = 0
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.UltraGridGSTReg)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(880, 362)
        '
        'UltraGridGSTReg
        '
        Me.UltraGridGSTReg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridGSTReg.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridGSTReg.Name = "UltraGridGSTReg"
        Me.UltraGridGSTReg.Size = New System.Drawing.Size(880, 362)
        Me.UltraGridGSTReg.TabIndex = 1
        Me.UltraGridGSTReg.Text = "Campus"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(880, 362)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGridSumm)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(880, 362)
        Me.Panel2.TabIndex = 16
        '
        'UltraGridSumm
        '
        Me.UltraGridSumm.AllowDrop = True
        Me.UltraGridSumm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridSumm.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridSumm.Name = "UltraGridSumm"
        Me.UltraGridSumm.Size = New System.Drawing.Size(880, 362)
        Me.UltraGridSumm.TabIndex = 0
        Me.UltraGridSumm.Text = "Campus"
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Controls.Add(Me.UltraTabControl3)
        Me.UltraTabPageControl6.Controls.Add(Me.Panel4)
        Me.UltraTabPageControl6.Controls.Add(Me.UltraGridPayment)
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6"
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(880, 362)
        '
        'UltraTabControl3
        '
        Me.UltraTabControl3.Controls.Add(Me.UltraTabSharedControlsPage3)
        Me.UltraTabControl3.Controls.Add(Me.UltraTabPageControl5)
        Me.UltraTabControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl3.Location = New System.Drawing.Point(0, 141)
        Me.UltraTabControl3.Name = "UltraTabControl3"
        Appearance1.FontData.BoldAsString = "True"
        Me.UltraTabControl3.SelectedTabAppearance = Appearance1
        Me.UltraTabControl3.SharedControlsPage = Me.UltraTabSharedControlsPage3
        Me.UltraTabControl3.Size = New System.Drawing.Size(880, 189)
        Me.UltraTabControl3.TabIndex = 55
        Me.UltraTabControl3.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab1.TabPage = Me.UltraTabPageControl5
        UltraTab1.Text = "Payment Detail"
        Me.UltraTabControl3.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1})
        Me.UltraTabControl3.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage3
        '
        Me.UltraTabSharedControlsPage3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage3.Name = "UltraTabSharedControlsPage3"
        Me.UltraTabSharedControlsPage3.Size = New System.Drawing.Size(876, 163)
        '
        'Panel4
        '
        '
        'Panel4.ClientArea
        '
        Me.Panel4.ClientArea.Controls.Add(Me.btnLiabLedger)
        Me.Panel4.ClientArea.Controls.Add(Me.btnITCLedger)
        Me.Panel4.ClientArea.Controls.Add(Me.btnCashLedger)
        Me.Panel4.ClientArea.Controls.Add(Me.btnEdit)
        Me.Panel4.ClientArea.Controls.Add(Me.btnOffset)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 330)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(880, 32)
        Me.Panel4.TabIndex = 57
        '
        'btnLiabLedger
        '
        Me.btnLiabLedger.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnLiabLedger.Location = New System.Drawing.Point(168, 0)
        Me.btnLiabLedger.Name = "btnLiabLedger"
        Me.btnLiabLedger.Size = New System.Drawing.Size(84, 32)
        Me.btnLiabLedger.TabIndex = 11
        Me.btnLiabLedger.Text = "&Liab Ledger"
        '
        'btnITCLedger
        '
        Me.btnITCLedger.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnITCLedger.Location = New System.Drawing.Point(84, 0)
        Me.btnITCLedger.Name = "btnITCLedger"
        Me.btnITCLedger.Size = New System.Drawing.Size(84, 32)
        Me.btnITCLedger.TabIndex = 9
        Me.btnITCLedger.Text = "&ITC Ledger"
        '
        'btnCashLedger
        '
        Me.btnCashLedger.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCashLedger.Location = New System.Drawing.Point(0, 0)
        Me.btnCashLedger.Name = "btnCashLedger"
        Me.btnCashLedger.Size = New System.Drawing.Size(84, 32)
        Me.btnCashLedger.TabIndex = 10
        Me.btnCashLedger.Text = "&Cash Ledger"
        '
        'btnEdit
        '
        Me.btnEdit.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnEdit.Location = New System.Drawing.Point(712, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(84, 32)
        Me.btnEdit.TabIndex = 7
        Me.btnEdit.Text = "&Edit"
        '
        'btnOffset
        '
        Me.btnOffset.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOffset.Location = New System.Drawing.Point(796, 0)
        Me.btnOffset.Name = "btnOffset"
        Me.btnOffset.Size = New System.Drawing.Size(84, 32)
        Me.btnOffset.TabIndex = 8
        Me.btnOffset.Text = "&Offset"
        '
        'UltraGridPayment
        '
        Me.UltraGridPayment.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraGridPayment.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridPayment.Name = "UltraGridPayment"
        Me.UltraGridPayment.Size = New System.Drawing.Size(880, 141)
        Me.UltraGridPayment.TabIndex = 56
        Me.UltraGridPayment.Text = "Campus"
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(880, 362)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraGridTaskHistory)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(880, 362)
        '
        'UltraGridTaskHistory
        '
        Me.UltraGridTaskHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridTaskHistory.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridTaskHistory.Name = "UltraGridTaskHistory"
        Me.UltraGridTaskHistory.Size = New System.Drawing.Size(880, 362)
        Me.UltraGridTaskHistory.TabIndex = 1
        Me.UltraGridTaskHistory.Text = "Campus"
        '
        'UltraTabPageControl7
        '
        Me.UltraTabPageControl7.Controls.Add(Me.UltraGridITCRev)
        Me.UltraTabPageControl7.Controls.Add(Me.UltraPanel3)
        Me.UltraTabPageControl7.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl7.Name = "UltraTabPageControl7"
        Me.UltraTabPageControl7.Size = New System.Drawing.Size(880, 362)
        '
        'UltraGridITCRev
        '
        Me.UltraGridITCRev.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridITCRev.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridITCRev.Name = "UltraGridITCRev"
        Me.UltraGridITCRev.Size = New System.Drawing.Size(880, 330)
        Me.UltraGridITCRev.TabIndex = 54
        Me.UltraGridITCRev.Text = "Campus"
        '
        'UltraPanel3
        '
        '
        'UltraPanel3.ClientArea
        '
        Me.UltraPanel3.ClientArea.Controls.Add(Me.btnDelITC)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.btnAddITC)
        Me.UltraPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel3.Location = New System.Drawing.Point(0, 330)
        Me.UltraPanel3.Name = "UltraPanel3"
        Me.UltraPanel3.Size = New System.Drawing.Size(880, 32)
        Me.UltraPanel3.TabIndex = 55
        '
        'btnDelITC
        '
        Me.btnDelITC.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDelITC.Location = New System.Drawing.Point(712, 0)
        Me.btnDelITC.Name = "btnDelITC"
        Me.btnDelITC.Size = New System.Drawing.Size(84, 32)
        Me.btnDelITC.TabIndex = 7
        Me.btnDelITC.Text = "&Delete"
        '
        'btnAddITC
        '
        Me.btnAddITC.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAddITC.Location = New System.Drawing.Point(796, 0)
        Me.btnAddITC.Name = "btnAddITC"
        Me.btnAddITC.Size = New System.Drawing.Size(84, 32)
        Me.btnAddITC.TabIndex = 8
        Me.btnAddITC.Text = "&Add New"
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
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl7)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 123)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance2.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance2
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(884, 388)
        Me.UltraTabControl1.TabIndex = 52
        Me.UltraTabControl1.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab5.TabPage = Me.UltraTabPageControl3
        UltraTab5.Text = "GstReg"
        UltraTab4.TabPage = Me.UltraTabPageControl1
        UltraTab4.Text = "Summary"
        UltraTab6.TabPage = Me.UltraTabPageControl6
        UltraTab6.Text = "Payment"
        UltraTab3.Key = "status"
        UltraTab3.TabPage = Me.UltraTabPageControl4
        UltraTab3.Text = "Task Status"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Task History"
        UltraTab7.TabPage = Me.UltraTabPageControl7
        UltraTab7.Text = "ITC Reversal"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab5, UltraTab4, UltraTab6, UltraTab3, UltraTab2, UltraTab7})
        Me.UltraTabControl1.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(880, 362)
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnUpdate)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_GstRegID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_ReturnPeriodID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel3)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel2)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.txt_PANNum)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel12)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.txt_CompName)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnChange)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnCancel1)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 45)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(884, 78)
        Me.UltraPanel2.TabIndex = 53
        '
        'btnUpdate
        '
        Appearance33.FontData.BoldAsString = "True"
        Me.btnUpdate.Appearance = Appearance33
        Me.btnUpdate.Location = New System.Drawing.Point(332, 42)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(81, 24)
        Me.btnUpdate.TabIndex = 70
        Me.btnUpdate.Text = "&Update"
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
        Appearance34.FontData.SizeInPoints = 8.25!
        Appearance34.TextHAlignAsString = "Right"
        Appearance34.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance34
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
        Appearance35.FontData.SizeInPoints = 8.25!
        Appearance35.TextHAlignAsString = "Right"
        Appearance35.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance35
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
        Appearance36.FontData.SizeInPoints = 8.25!
        Appearance36.TextHAlignAsString = "Right"
        Appearance36.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance36
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
        Appearance37.FontData.SizeInPoints = 8.25!
        Appearance37.TextHAlignAsString = "Right"
        Appearance37.TextVAlignAsString = "Middle"
        Me.UltraLabel12.Appearance = Appearance37
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
        'btnChange
        '
        Appearance38.FontData.BoldAsString = "True"
        Me.btnChange.Appearance = Appearance38
        Me.btnChange.Location = New System.Drawing.Point(243, 42)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(81, 24)
        Me.btnChange.TabIndex = 69
        Me.btnChange.Text = "&Change"
        '
        'btnCancel1
        '
        Appearance39.FontData.BoldAsString = "True"
        Me.btnCancel1.Appearance = Appearance39
        Me.btnCancel1.Location = New System.Drawing.Point(243, 42)
        Me.btnCancel1.Name = "btnCancel1"
        Me.btnCancel1.Size = New System.Drawing.Size(81, 24)
        Me.btnCancel1.TabIndex = 71
        Me.btnCancel1.Text = "&Cancel"
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.MenuStrip1)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(884, 45)
        Me.UltraPanel1.TabIndex = 54
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GSTNToolStripMenuItem, Me.ExcelToolStripMenuItem, Me.DataToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(884, 24)
        Me.MenuStrip1.TabIndex = 55
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GSTNToolStripMenuItem
        '
        Me.GSTNToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.PopulatePaymentToolStripMenuItem, Me.FileToolStripMenuItem})
        Me.GSTNToolStripMenuItem.Name = "GSTNToolStripMenuItem"
        Me.GSTNToolStripMenuItem.Size = New System.Drawing.Size(49, 20)
        Me.GSTNToolStripMenuItem.Text = "GSTN"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'PopulatePaymentToolStripMenuItem
        '
        Me.PopulatePaymentToolStripMenuItem.Name = "PopulatePaymentToolStripMenuItem"
        Me.PopulatePaymentToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.PopulatePaymentToolStripMenuItem.Text = "Populate Payment"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FinalTaxSummaryToolStripMenuItem, Me.CalculatedTaxSummaryToolStripMenuItem, Me.PaymentTaxSummaryToolStripMenuItem})
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'DataToolStripMenuItem
        '
        Me.DataToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerateJSONToolStripMenuItem, Me.EditToolStripMenuItem})
        Me.DataToolStripMenuItem.Name = "DataToolStripMenuItem"
        Me.DataToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.DataToolStripMenuItem.Text = "Data"
        '
        'GenerateJSONToolStripMenuItem
        '
        Me.GenerateJSONToolStripMenuItem.Name = "GenerateJSONToolStripMenuItem"
        Me.GenerateJSONToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.GenerateJSONToolStripMenuItem.Text = "Generate JSON"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'UltraPanel4
        '
        '
        'UltraPanel4.ClientArea
        '
        Me.UltraPanel4.ClientArea.Controls.Add(Me.btnSave)
        Me.UltraPanel4.ClientArea.Controls.Add(Me.btnCancel)
        Me.UltraPanel4.ClientArea.Controls.Add(Me.btnOK)
        Me.UltraPanel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel4.Location = New System.Drawing.Point(0, 511)
        Me.UltraPanel4.Name = "UltraPanel4"
        Me.UltraPanel4.Size = New System.Drawing.Size(884, 36)
        Me.UltraPanel4.TabIndex = 73
        '
        'btnSave
        '
        Appearance40.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance40
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(680, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 36)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance41.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance41
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(748, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 36)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance42.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance42
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(816, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 36)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        '
        'FinalTaxSummaryToolStripMenuItem
        '
        Me.FinalTaxSummaryToolStripMenuItem.Name = "FinalTaxSummaryToolStripMenuItem"
        Me.FinalTaxSummaryToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.FinalTaxSummaryToolStripMenuItem.Text = "Final Tax Summary"
        '
        'CalculatedTaxSummaryToolStripMenuItem
        '
        Me.CalculatedTaxSummaryToolStripMenuItem.Name = "CalculatedTaxSummaryToolStripMenuItem"
        Me.CalculatedTaxSummaryToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.CalculatedTaxSummaryToolStripMenuItem.Text = "Calculated Tax Summary"
        '
        'PaymentTaxSummaryToolStripMenuItem
        '
        Me.PaymentTaxSummaryToolStripMenuItem.Name = "PaymentTaxSummaryToolStripMenuItem"
        Me.PaymentTaxSummaryToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.PaymentTaxSummaryToolStripMenuItem.Text = "Payment Tax Summary"
        '
        'frmGSTNGSTR3B
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "GSTR3B Data Sync"
        Me.ClientSize = New System.Drawing.Size(884, 547)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.UltraPanel4)
        Me.Controls.Add(Me.UltraPanel2)
        Me.Controls.Add(Me.UltraPanel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGSTNGSTR3B"
        Me.Text = "GSTR3B Data Sync"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl5.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.UltraGridPaymentDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.UltraGridGSTReg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraGridSumm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl6.ResumeLayout(False)
        CType(Me.UltraTabControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl3.ResumeLayout(False)
        Me.Panel4.ClientArea.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.UltraGridPayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.UltraGridTaskHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl7.ResumeLayout(False)
        CType(Me.UltraGridITCRev, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel3.ClientArea.ResumeLayout(False)
        Me.UltraPanel3.ResumeLayout(False)
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
    Friend WithEvents UltraGridSumm As UltraGrid
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridTaskHistory As UltraGrid
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridGSTReg As UltraGrid
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnUpdate As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnChange As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmb_GstRegID As UltraCombo
    Friend WithEvents cmb_ReturnPeriodID As UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_PANNum As UltraTextEditor
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CompName As UltraTextEditor
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents MenuStrip1 As Windows.Forms.MenuStrip
    Friend WithEvents GSTNToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents PopulatePaymentToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateJSONToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabControl3 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage3 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents UltraGridPaymentDetail As UltraGrid
    Friend WithEvents UltraGridPayment As UltraGrid
    Friend WithEvents Panel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnLiabLedger As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnITCLedger As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCashLedger As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOffset As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl7 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridITCRev As UltraGrid
    Friend WithEvents UltraPanel3 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelITC As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddITC As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraPanel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents FinalTaxSummaryToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CalculatedTaxSummaryToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents PaymentTaxSummaryToolStripMenuItem As Windows.Forms.ToolStripMenuItem

#End Region
End Class

