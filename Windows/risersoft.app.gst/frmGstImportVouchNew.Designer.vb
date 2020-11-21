<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGstImportVouchNew
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
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDownload = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_DocType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.CtlUpLoad1 = New risersoft.[shared].win.ctlUpLoad()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.btnFileDetails = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_DocType1 = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.cmb_DocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_DocType1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(870, 234)
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraTabControl1)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraPanel2)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(874, 397)
        Me.UltraPanel1.TabIndex = 47
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl5)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 137)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance1.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance1
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(874, 260)
        Me.UltraTabControl1.TabIndex = 222
        Me.UltraTabControl1.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab3.Key = "status"
        UltraTab3.TabPage = Me.UltraTabPageControl5
        UltraTab3.Text = "Task Status"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab3})
        Me.UltraTabControl1.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(870, 234)
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnDownload)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.CtlUpLoad1)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel7)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnFileDetails)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_DocType)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_DocType1)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(874, 137)
        Me.UltraPanel2.TabIndex = 221
        '
        'btnDownload
        '
        Appearance5.FontData.BoldAsString = "True"
        Me.btnDownload.Appearance = Appearance5
        Me.btnDownload.Location = New System.Drawing.Point(608, 12)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(175, 24)
        Me.btnDownload.TabIndex = 221
        Me.btnDownload.Text = "&Download Template"
        '
        'cmb_DocType
        '
        Me.cmb_DocType.Location = New System.Drawing.Point(119, 28)
        Me.cmb_DocType.Name = "cmb_DocType"
        Me.cmb_DocType.ReadOnly = True
        Me.cmb_DocType.Size = New System.Drawing.Size(210, 22)
        Me.cmb_DocType.TabIndex = 48
        '
        'CtlUpLoad1
        '
        Me.CtlUpLoad1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtlUpLoad1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.CtlUpLoad1.Location = New System.Drawing.Point(12, 63)
        Me.CtlUpLoad1.Name = "CtlUpLoad1"
        Me.CtlUpLoad1.Size = New System.Drawing.Size(852, 33)
        Me.CtlUpLoad1.TabIndex = 220
        '
        'UltraLabel7
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance6
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(26, 31)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(84, 14)
        Me.UltraLabel7.TabIndex = 47
        Me.UltraLabel7.Text = "Document Type"
        '
        'btnFileDetails
        '
        Appearance7.FontData.BoldAsString = "True"
        Me.btnFileDetails.Appearance = Appearance7
        Me.btnFileDetails.Location = New System.Drawing.Point(686, 103)
        Me.btnFileDetails.Name = "btnFileDetails"
        Me.btnFileDetails.Size = New System.Drawing.Size(175, 24)
        Me.btnFileDetails.TabIndex = 54
        Me.btnFileDetails.Text = "&Open File Details Page"
        '
        'cmb_DocType1
        '
        Me.cmb_DocType1.Location = New System.Drawing.Point(119, 28)
        Me.cmb_DocType1.Name = "cmb_DocType1"
        Me.cmb_DocType1.ReadOnly = True
        Me.cmb_DocType1.Size = New System.Drawing.Size(210, 21)
        Me.cmb_DocType1.TabIndex = 222
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "XLS Files|*.xls|XLSX Files|*.xlsx"
        '
        'frmGstImportVouchNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "GST Data Import Utility"
        Me.ClientSize = New System.Drawing.Size(874, 397)
        Me.Controls.Add(Me.UltraPanel1)
        Me.MaximizeBox = False
        Me.Name = "frmGstImportVouchNew"
        Me.Text = "GST Data Import Utility"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.PerformLayout()
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.cmb_DocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_DocType1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmb_DocType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents btnFileDetails As Infragistics.Win.Misc.UltraButton
    Friend WithEvents CtlUpLoad1 As ctlUpLoad
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents SaveFileDialog1 As Windows.Forms.SaveFileDialog
    Friend WithEvents btnDownload As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmb_DocType1 As Infragistics.Win.UltraWinEditors.UltraComboEditor
End Class
