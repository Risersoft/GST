<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGSTNEWBPast
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab6 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridGSTReg = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraGridSummary = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridTaskHistory = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.cmb_GstRegID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_PANNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_CompName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraPanel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnUpdate = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel1 = New Infragistics.Win.Misc.UltraButton()
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
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnChange = New Infragistics.Win.Misc.UltraButton()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.GSTNToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadTaxpayerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelFileForDownloadPastDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.UltraGridGSTReg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.UltraGridSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.UltraGridTaskHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.cmb_GstRegID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PANNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel4.ClientArea.SuspendLayout()
        Me.UltraPanel4.SuspendLayout()
        Me.UltraPanel3.ClientArea.SuspendLayout()
        Me.UltraPanel3.SuspendLayout()
        CType(Me.cmb_ToID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_FromID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_Period, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_Operation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_ReturnPeriodID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.UltraGridGSTReg)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(880, 319)
        '
        'UltraGridGSTReg
        '
        Me.UltraGridGSTReg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridGSTReg.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridGSTReg.Name = "UltraGridGSTReg"
        Me.UltraGridGSTReg.Size = New System.Drawing.Size(880, 319)
        Me.UltraGridGSTReg.TabIndex = 1
        Me.UltraGridGSTReg.Text = "Campus"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(880, 319)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGridSummary)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(880, 319)
        Me.Panel2.TabIndex = 16
        '
        'UltraGridSummary
        '
        Me.UltraGridSummary.AllowDrop = True
        Me.UltraGridSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridSummary.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridSummary.Name = "UltraGridSummary"
        Me.UltraGridSummary.Size = New System.Drawing.Size(880, 319)
        Me.UltraGridSummary.TabIndex = 0
        Me.UltraGridSummary.Text = "Campus"
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6"
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(880, 319)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraGridTaskHistory)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
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
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
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
        Appearance1.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance1
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(884, 345)
        Me.UltraTabControl1.TabIndex = 72
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
        Me.UltraPanel2.TabIndex = 73
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
        Appearance14.FontData.SizeInPoints = 8.25!
        Appearance14.TextHAlignAsString = "Right"
        Appearance14.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance14
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
        Appearance15.FontData.SizeInPoints = 8.25!
        Appearance15.TextHAlignAsString = "Right"
        Appearance15.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance15
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
        Appearance16.FontData.SizeInPoints = 8.25!
        Appearance16.TextHAlignAsString = "Right"
        Appearance16.TextVAlignAsString = "Middle"
        Me.UltraLabel12.Appearance = Appearance16
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
        Me.UltraPanel4.TabIndex = 76
        '
        'btnUpdate
        '
        Appearance17.FontData.BoldAsString = "True"
        Me.btnUpdate.Appearance = Appearance17
        Me.btnUpdate.Location = New System.Drawing.Point(114, 4)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(81, 24)
        Me.btnUpdate.TabIndex = 68
        Me.btnUpdate.Text = "&Update"
        '
        'btnCancel1
        '
        Appearance18.FontData.BoldAsString = "True"
        Me.btnCancel1.Appearance = Appearance18
        Me.btnCancel1.Location = New System.Drawing.Point(23, 4)
        Me.btnCancel1.Name = "btnCancel1"
        Me.btnCancel1.Size = New System.Drawing.Size(81, 24)
        Me.btnCancel1.TabIndex = 67
        Me.btnCancel1.Text = "&Cancel"
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
        Me.UltraPanel3.TabIndex = 75
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
        Appearance19.FontData.SizeInPoints = 8.25!
        Appearance19.TextHAlignAsString = "Right"
        Appearance19.TextVAlignAsString = "Middle"
        Me.UltraLabelTo.Appearance = Appearance19
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
        Appearance20.FontData.SizeInPoints = 8.25!
        Appearance20.TextHAlignAsString = "Right"
        Appearance20.TextVAlignAsString = "Middle"
        Me.UltraLabelFrom.Appearance = Appearance20
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
        Appearance21.FontData.SizeInPoints = 8.25!
        Appearance21.TextHAlignAsString = "Right"
        Appearance21.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance21
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
        Appearance22.FontData.SizeInPoints = 8.25!
        Appearance22.TextHAlignAsString = "Right"
        Appearance22.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance22
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
        Appearance23.FontData.SizeInPoints = 8.25!
        Appearance23.TextHAlignAsString = "Right"
        Appearance23.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance23
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel2.Location = New System.Drawing.Point(8, 8)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(83, 14)
        Me.UltraLabel2.TabIndex = 59
        Me.UltraLabel2.Text = "Return Period :"
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
        Me.UltraPanel1.TabIndex = 74
        '
        'btnChange
        '
        Appearance24.FontData.BoldAsString = "True"
        Me.btnChange.Appearance = Appearance24
        Me.btnChange.Location = New System.Drawing.Point(109, 30)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(67, 24)
        Me.btnChange.TabIndex = 64
        Me.btnChange.Text = "&Change"
        '
        'UltraLabel4
        '
        Appearance25.FontData.SizeInPoints = 8.25!
        Appearance25.TextHAlignAsString = "Right"
        Appearance25.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance25
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
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GSTNToolStripMenuItem, Me.DownloadToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(884, 24)
        Me.MenuStrip1.TabIndex = 55
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GSTNToolStripMenuItem
        '
        Me.GSTNToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadTaxpayerToolStripMenuItem})
        Me.GSTNToolStripMenuItem.Name = "GSTNToolStripMenuItem"
        Me.GSTNToolStripMenuItem.Size = New System.Drawing.Size(49, 20)
        Me.GSTNToolStripMenuItem.Text = "GSTN"
        '
        'DownloadTaxpayerToolStripMenuItem
        '
        Me.DownloadTaxpayerToolStripMenuItem.Name = "DownloadTaxpayerToolStripMenuItem"
        Me.DownloadTaxpayerToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.DownloadTaxpayerToolStripMenuItem.Text = "Download Taxpayer"
        '
        'DownloadToolStripMenuItem
        '
        Me.DownloadToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelFileForDownloadPastDataToolStripMenuItem})
        Me.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem"
        Me.DownloadToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.DownloadToolStripMenuItem.Text = "Download"
        '
        'ExcelFileForDownloadPastDataToolStripMenuItem
        '
        Me.ExcelFileForDownloadPastDataToolStripMenuItem.Name = "ExcelFileForDownloadPastDataToolStripMenuItem"
        Me.ExcelFileForDownloadPastDataToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.ExcelFileForDownloadPastDataToolStripMenuItem.Text = "Excel File for Download Past Data"
        '
        'frmGSTNEWBPast
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "EWB Past Data Import"
        Me.ClientSize = New System.Drawing.Size(884, 547)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.UltraPanel2)
        Me.Controls.Add(Me.UltraPanel4)
        Me.Controls.Add(Me.UltraPanel3)
        Me.Controls.Add(Me.UltraPanel1)
        Me.Name = "frmGSTNEWBPast"
        Me.Text = "EWB Past Data Import"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.UltraGridGSTReg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraGridSummary, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.UltraPanel4.ClientArea.ResumeLayout(False)
        Me.UltraPanel4.ResumeLayout(False)
        Me.UltraPanel3.ClientArea.ResumeLayout(False)
        Me.UltraPanel3.ClientArea.PerformLayout()
        Me.UltraPanel3.ResumeLayout(False)
        CType(Me.cmb_ToID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_FromID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_Period, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_Operation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_ReturnPeriodID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.PerformLayout()
        Me.UltraPanel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SaveFileDialog1 As Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents UltraGridSummary As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraGridTaskHistory As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraGridGSTReg As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmb_GstRegID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_PANNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CompName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraPanel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnUpdate As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraPanel3 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmb_ToID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabelTo As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_FromID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabelFrom As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_Period As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents cmb_Operation As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_ReturnPeriodID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnChange As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents MenuStrip1 As Windows.Forms.MenuStrip
    Friend WithEvents GSTNToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadTaxpayerToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelFileForDownloadPastDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
End Class
