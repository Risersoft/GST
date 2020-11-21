<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDashboardSetting
    Inherits frmMax

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.InitForm()
    End Sub

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
        Me.components = New System.ComponentModel.Container()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDashboardSetting))
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_FieldName = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.cmb_CompanyID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_GSTRegID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabelCompany = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabelGSTReg = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraCarousel1 = New Infragistics.Win.UltraWinCarousel.UltraCarousel()
        Me.UltraFlowLayoutManager1 = New Infragistics.Win.Misc.UltraFlowLayoutManager(Me.components)
        Me.UltraTilePanel1 = New Infragistics.Win.Misc.UltraTilePanel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.cmb_FieldName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_GSTRegID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraCarousel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraFlowLayoutManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTilePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTilePanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 703)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1008, 39)
        Me.Panel4.TabIndex = 2
        '
        'btnSave
        '
        Appearance8.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance8
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(744, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 39)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance9.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance9
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(832, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 39)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance10.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance10
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(920, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 39)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabelCompany)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnAdd)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_FieldName)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraLabelGSTReg)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_CompanyID)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_GSTRegID)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(1008, 84)
        Me.UltraPanel2.TabIndex = 67
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance12.FontData.BoldAsString = "True"
        Me.btnAdd.Appearance = Appearance12
        Me.btnAdd.Location = New System.Drawing.Point(819, 57)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(83, 24)
        Me.btnAdd.TabIndex = 72
        Me.btnAdd.Text = "Add"
        '
        'cmb_FieldName
        '
        Me.cmb_FieldName.Location = New System.Drawing.Point(96, 13)
        Me.cmb_FieldName.Name = "cmb_FieldName"
        Me.cmb_FieldName.Size = New System.Drawing.Size(211, 21)
        Me.cmb_FieldName.TabIndex = 69
        '
        'cmb_CompanyID
        '
        Me.cmb_CompanyID.Location = New System.Drawing.Point(95, 48)
        Me.cmb_CompanyID.Name = "cmb_CompanyID"
        Me.cmb_CompanyID.Size = New System.Drawing.Size(213, 22)
        Me.cmb_CompanyID.TabIndex = 65
        '
        'UltraLabel1
        '
        Appearance13.FontData.SizeInPoints = 8.25!
        Appearance13.TextHAlignAsString = "Right"
        Appearance13.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance13
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabel1.Location = New System.Drawing.Point(47, 16)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(39, 14)
        Me.UltraLabel1.TabIndex = 61
        Me.UltraLabel1.Text = "Level :"
        '
        'cmb_GSTRegID
        '
        Me.cmb_GSTRegID.Location = New System.Drawing.Point(95, 48)
        Me.cmb_GSTRegID.Name = "cmb_GSTRegID"
        Me.cmb_GSTRegID.Size = New System.Drawing.Size(213, 22)
        Me.cmb_GSTRegID.TabIndex = 71
        '
        'UltraLabelCompany
        '
        Appearance11.FontData.SizeInPoints = 8.25!
        Appearance11.TextHAlignAsString = "Right"
        Appearance11.TextVAlignAsString = "Middle"
        Me.UltraLabelCompany.Appearance = Appearance11
        Me.UltraLabelCompany.AutoSize = True
        Me.UltraLabelCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabelCompany.Location = New System.Drawing.Point(23, 51)
        Me.UltraLabelCompany.Name = "UltraLabelCompany"
        Me.UltraLabelCompany.Size = New System.Drawing.Size(61, 14)
        Me.UltraLabelCompany.TabIndex = 64
        Me.UltraLabelCompany.Text = "Company :"
        '
        'UltraLabelGSTReg
        '
        Appearance14.FontData.SizeInPoints = 8.25!
        Appearance14.TextHAlignAsString = "Right"
        Appearance14.TextVAlignAsString = "Middle"
        Me.UltraLabelGSTReg.Appearance = Appearance14
        Me.UltraLabelGSTReg.AutoSize = True
        Me.UltraLabelGSTReg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.UltraLabelGSTReg.Location = New System.Drawing.Point(24, 51)
        Me.UltraLabelGSTReg.Name = "UltraLabelGSTReg"
        Me.UltraLabelGSTReg.Size = New System.Drawing.Size(59, 14)
        Me.UltraLabelGSTReg.TabIndex = 70
        Me.UltraLabelGSTReg.Text = "GST Reg :"
        '
        'UltraCarousel1
        '
        Me.UltraCarousel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraCarousel1.Location = New System.Drawing.Point(0, 84)
        Me.UltraCarousel1.Name = "UltraCarousel1"
        Me.UltraCarousel1.Path = New Infragistics.Win.UltraWinCarousel.CarouselPath("EllipseBottom", New System.Drawing.PointF() {CType(resources.GetObject("UltraCarousel1.Path"), System.Drawing.PointF), CType(resources.GetObject("UltraCarousel1.Path1"), System.Drawing.PointF)}, New Byte() {CType(0, Byte), CType(1, Byte)})
        Me.UltraCarousel1.Size = New System.Drawing.Size(1008, 300)
        Me.UltraCarousel1.TabIndex = 68
        Me.UltraCarousel1.Text = "UltraCarousel1"
        '
        'UltraTilePanel1
        '
        Me.UltraTilePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTilePanel1.Location = New System.Drawing.Point(0, 384)
        Me.UltraTilePanel1.Name = "UltraTilePanel1"
        Me.UltraTilePanel1.NormalModeDimensions = New System.Drawing.Size(0, 0)
        Me.UltraTilePanel1.Size = New System.Drawing.Size(1008, 319)
        Me.UltraTilePanel1.TabIndex = 69
        '
        'frmDashboardSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "Create Dashboard"
        Me.ClientSize = New System.Drawing.Size(1008, 742)
        Me.Controls.Add(Me.UltraTilePanel1)
        Me.Controls.Add(Me.UltraCarousel1)
        Me.Controls.Add(Me.UltraPanel2)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "frmDashboardSetting"
        Me.Text = "Create Dashboard"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.PerformLayout()
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.cmb_FieldName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_GSTRegID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraCarousel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraFlowLayoutManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTilePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTilePanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmb_FieldName As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents cmb_CompanyID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabelCompany As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_GSTRegID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabelGSTReg As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraCarousel1 As Infragistics.Win.UltraWinCarousel.UltraCarousel
    Friend WithEvents UltraFlowLayoutManager1 As Infragistics.Win.Misc.UltraFlowLayoutManager
    Friend WithEvents btnAdd As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTilePanel1 As Infragistics.Win.Misc.UltraTilePanel
End Class
