Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmGstCompany
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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblNum As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txt_Email As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_FaxNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_FaxArea As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_FaxCountry As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_PhNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_PhArea As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_PhCountry As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_PinCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_Web As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_City As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_Address As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_CompName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Public WithEvents GrpAddress As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
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
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnDelReg = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddReg = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGrid2 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnDelete = New Infragistics.Win.Misc.UltraButton()
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddNew = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.GrpAddress = New System.Windows.Forms.GroupBox()
        Me.cmb_Country = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_State = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_Email = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_FaxNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_FaxArea = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_FaxCountry = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_PhNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_PhArea = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_PhCountry = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_PinCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_Web = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txt_City = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txt_Address = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_PANNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_CompName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblNum = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_BrandName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_HOCampusID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_FinStartDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pic_Logo = New System.Windows.Forms.PictureBox()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.btnSavepic = New Infragistics.Win.Misc.UltraButton()
        Me.btnBrowsePic = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.btnSearch = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.UltraTabPageControl5.SuspendLayout()
        CType(Me.UltraGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl2.SuspendLayout()
        Me.GrpAddress.SuspendLayout()
        CType(Me.cmb_Country, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_State, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_FaxNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_FaxArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_FaxCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PhNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PhArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PhCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Web, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_City, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Address, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txt_PANNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.txt_BrandName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_HOCampusID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_FinStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.pic_Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel13.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraTabSharedControlsPage1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.UltraGrid1)
        Me.UltraTabPageControl4.Controls.Add(Me.Panel3)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(636, 176)
        '
        'UltraGrid1
        '
        Me.UltraGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(636, 146)
        Me.UltraGrid1.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 146)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(636, 30)
        Me.Panel3.TabIndex = 3
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnSearch)
        Me.Panel6.Controls.Add(Me.btnDelReg)
        Me.Panel6.Controls.Add(Me.btnAddReg)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(636, 30)
        Me.Panel6.TabIndex = 6
        '
        'btnDelReg
        '
        Me.btnDelReg.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDelReg.Location = New System.Drawing.Point(492, 0)
        Me.btnDelReg.Name = "btnDelReg"
        Me.btnDelReg.Size = New System.Drawing.Size(72, 30)
        Me.btnDelReg.TabIndex = 0
        Me.btnDelReg.Text = "Delete"
        '
        'btnAddReg
        '
        Me.btnAddReg.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAddReg.Location = New System.Drawing.Point(564, 0)
        Me.btnAddReg.Name = "btnAddReg"
        Me.btnAddReg.Size = New System.Drawing.Size(72, 30)
        Me.btnAddReg.TabIndex = 2
        Me.btnAddReg.Text = "Add New"
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Controls.Add(Me.UltraGrid2)
        Me.UltraTabPageControl5.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(636, 240)
        '
        'UltraGrid2
        '
        Me.UltraGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGrid2.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid2.Name = "UltraGrid2"
        Me.UltraGrid2.Size = New System.Drawing.Size(636, 210)
        Me.UltraGrid2.TabIndex = 4
        Me.UltraGrid2.Text = "Campus"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnEdit)
        Me.Panel2.Controls.Add(Me.btnAddNew)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 210)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(636, 30)
        Me.Panel2.TabIndex = 5
        '
        'btnDelete
        '
        Me.btnDelete.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDelete.Location = New System.Drawing.Point(422, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 30)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        '
        'btnEdit
        '
        Me.btnEdit.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnEdit.Location = New System.Drawing.Point(494, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 30)
        Me.btnEdit.TabIndex = 4
        Me.btnEdit.Text = "Edit"
        '
        'btnAddNew
        '
        Me.btnAddNew.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAddNew.Location = New System.Drawing.Point(564, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(72, 30)
        Me.btnAddNew.TabIndex = 5
        Me.btnAddNew.Text = "Add New"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraTabControl2)
        Me.UltraTabPageControl1.Controls.Add(Me.GrpAddress)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(640, 431)
        '
        'UltraTabControl2
        '
        Me.UltraTabControl2.Controls.Add(Me.UltraTabSharedControlsPage2)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl5)
        Me.UltraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl2.Location = New System.Drawing.Point(0, 229)
        Me.UltraTabControl2.Name = "UltraTabControl2"
        Me.UltraTabControl2.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.UltraTabControl2.Size = New System.Drawing.Size(640, 202)
        Me.UltraTabControl2.TabIndex = 2
        UltraTab4.TabPage = Me.UltraTabPageControl4
        UltraTab4.Text = "GST Reg"
        UltraTab5.TabPage = Me.UltraTabPageControl5
        UltraTab5.Text = "Campus"
        Me.UltraTabControl2.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab4, UltraTab5})
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(636, 176)
        '
        'GrpAddress
        '
        Me.GrpAddress.Controls.Add(Me.cmb_Country)
        Me.GrpAddress.Controls.Add(Me.cmb_State)
        Me.GrpAddress.Controls.Add(Me.Label7)
        Me.GrpAddress.Controls.Add(Me.Label10)
        Me.GrpAddress.Controls.Add(Me.Label9)
        Me.GrpAddress.Controls.Add(Me.txt_Email)
        Me.GrpAddress.Controls.Add(Me.txt_FaxNum)
        Me.GrpAddress.Controls.Add(Me.txt_FaxArea)
        Me.GrpAddress.Controls.Add(Me.Label12)
        Me.GrpAddress.Controls.Add(Me.txt_FaxCountry)
        Me.GrpAddress.Controls.Add(Me.txt_PhNum)
        Me.GrpAddress.Controls.Add(Me.txt_PhArea)
        Me.GrpAddress.Controls.Add(Me.Label13)
        Me.GrpAddress.Controls.Add(Me.txt_PhCountry)
        Me.GrpAddress.Controls.Add(Me.Label14)
        Me.GrpAddress.Controls.Add(Me.txt_PinCode)
        Me.GrpAddress.Controls.Add(Me.Label15)
        Me.GrpAddress.Controls.Add(Me.txt_Web)
        Me.GrpAddress.Controls.Add(Me.Label16)
        Me.GrpAddress.Controls.Add(Me.txt_City)
        Me.GrpAddress.Controls.Add(Me.Label17)
        Me.GrpAddress.Controls.Add(Me.txt_Address)
        Me.GrpAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpAddress.Location = New System.Drawing.Point(0, 64)
        Me.GrpAddress.Name = "GrpAddress"
        Me.GrpAddress.Size = New System.Drawing.Size(640, 165)
        Me.GrpAddress.TabIndex = 1
        Me.GrpAddress.TabStop = False
        Me.GrpAddress.Text = "Head Office"
        '
        'cmb_Country
        '
        Appearance1.FontData.BoldAsString = "False"
        Me.cmb_Country.Appearance = Appearance1
        Me.cmb_Country.Location = New System.Drawing.Point(96, 87)
        Me.cmb_Country.Name = "cmb_Country"
        Me.cmb_Country.Size = New System.Drawing.Size(200, 22)
        Me.cmb_Country.TabIndex = 13
        Me.cmb_Country.Text = "UltraCombo5"
        '
        'cmb_State
        '
        Appearance2.FontData.BoldAsString = "False"
        Me.cmb_State.Appearance = Appearance2
        Me.cmb_State.Location = New System.Drawing.Point(96, 115)
        Me.cmb_State.Name = "cmb_State"
        Me.cmb_State.Size = New System.Drawing.Size(200, 22)
        Me.cmb_State.TabIndex = 19
        Me.cmb_State.Text = "UltraCombo5"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(51, 119)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 14)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "* State"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(35, 90)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 14)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "* Country"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(57, 144)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 14)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Email"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Email
        '
        Me.txt_Email.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.FontData.BoldAsString = "False"
        Me.txt_Email.Appearance = Appearance3
        Me.txt_Email.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Email.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.txt_Email.Location = New System.Drawing.Point(96, 143)
        Me.txt_Email.Name = "txt_Email"
        Me.txt_Email.Size = New System.Drawing.Size(518, 17)
        Me.txt_Email.TabIndex = 21
        Me.txt_Email.Text = "ultratexteditor12"
        '
        'txt_FaxNum
        '
        Appearance4.FontData.BoldAsString = "False"
        Me.txt_FaxNum.Appearance = Appearance4
        Me.txt_FaxNum.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_FaxNum.Location = New System.Drawing.Point(436, 86)
        Me.txt_FaxNum.Name = "txt_FaxNum"
        Me.txt_FaxNum.Size = New System.Drawing.Size(178, 17)
        Me.txt_FaxNum.TabIndex = 17
        Me.txt_FaxNum.Text = "UltraTextEditor9"
        '
        'txt_FaxArea
        '
        Appearance5.FontData.BoldAsString = "False"
        Me.txt_FaxArea.Appearance = Appearance5
        Me.txt_FaxArea.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_FaxArea.Location = New System.Drawing.Point(396, 87)
        Me.txt_FaxArea.Name = "txt_FaxArea"
        Me.txt_FaxArea.Size = New System.Drawing.Size(32, 17)
        Me.txt_FaxArea.TabIndex = 16
        Me.txt_FaxArea.Text = "UltraTextEditor10"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(310, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 14)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Fax No."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_FaxCountry
        '
        Appearance6.FontData.BoldAsString = "False"
        Me.txt_FaxCountry.Appearance = Appearance6
        Me.txt_FaxCountry.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_FaxCountry.Location = New System.Drawing.Point(358, 87)
        Me.txt_FaxCountry.Name = "txt_FaxCountry"
        Me.txt_FaxCountry.Size = New System.Drawing.Size(30, 17)
        Me.txt_FaxCountry.TabIndex = 15
        Me.txt_FaxCountry.Text = "UltraTextEditor11"
        '
        'txt_PhNum
        '
        Appearance7.FontData.BoldAsString = "False"
        Me.txt_PhNum.Appearance = Appearance7
        Me.txt_PhNum.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_PhNum.Location = New System.Drawing.Point(436, 64)
        Me.txt_PhNum.Name = "txt_PhNum"
        Me.txt_PhNum.Size = New System.Drawing.Size(178, 17)
        Me.txt_PhNum.TabIndex = 11
        Me.txt_PhNum.Text = "UltraTextEditor8"
        '
        'txt_PhArea
        '
        Appearance8.FontData.BoldAsString = "False"
        Me.txt_PhArea.Appearance = Appearance8
        Me.txt_PhArea.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_PhArea.Location = New System.Drawing.Point(396, 64)
        Me.txt_PhArea.Name = "txt_PhArea"
        Me.txt_PhArea.Size = New System.Drawing.Size(32, 17)
        Me.txt_PhArea.TabIndex = 10
        Me.txt_PhArea.Text = "UltraTextEditor7"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(311, 66)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 14)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Ph. No."
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_PhCountry
        '
        Appearance9.FontData.BoldAsString = "False"
        Me.txt_PhCountry.Appearance = Appearance9
        Me.txt_PhCountry.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_PhCountry.Location = New System.Drawing.Point(358, 64)
        Me.txt_PhCountry.Name = "txt_PhCountry"
        Me.txt_PhCountry.Size = New System.Drawing.Size(30, 17)
        Me.txt_PhCountry.TabIndex = 9
        Me.txt_PhCountry.Text = "UltraTextEditor6"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(42, 65)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 14)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Pincode"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_PinCode
        '
        Appearance10.FontData.BoldAsString = "False"
        Me.txt_PinCode.Appearance = Appearance10
        Me.txt_PinCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_PinCode.Location = New System.Drawing.Point(96, 64)
        Me.txt_PinCode.Name = "txt_PinCode"
        Me.txt_PinCode.Size = New System.Drawing.Size(130, 17)
        Me.txt_PinCode.TabIndex = 7
        Me.txt_PinCode.Text = "UltraTextEditor5"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(324, 42)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 14)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Web"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Web
        '
        Appearance11.FontData.BoldAsString = "False"
        Me.txt_Web.Appearance = Appearance11
        Me.txt_Web.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Web.Location = New System.Drawing.Point(358, 41)
        Me.txt_Web.Name = "txt_Web"
        Me.txt_Web.Size = New System.Drawing.Size(256, 17)
        Me.txt_Web.TabIndex = 5
        Me.txt_Web.Text = "UltraTextEditor4"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(65, 42)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(28, 14)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "City"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_City
        '
        Appearance12.FontData.BoldAsString = "False"
        Me.txt_City.Appearance = Appearance12
        Me.txt_City.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_City.Location = New System.Drawing.Point(96, 41)
        Me.txt_City.Name = "txt_City"
        Me.txt_City.Size = New System.Drawing.Size(130, 17)
        Me.txt_City.TabIndex = 3
        Me.txt_City.Text = "UltraTextEditor3"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(31, 19)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(62, 14)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "* Address"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Address
        '
        Me.txt_Address.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance13.FontData.BoldAsString = "False"
        Me.txt_Address.Appearance = Appearance13
        Me.txt_Address.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Address.Location = New System.Drawing.Point(96, 18)
        Me.txt_Address.Name = "txt_Address"
        Me.txt_Address.Size = New System.Drawing.Size(518, 17)
        Me.txt_Address.TabIndex = 1
        Me.txt_Address.Text = "UltraTextEditor2"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txt_PANNum)
        Me.Panel1.Controls.Add(Me.txt_CompName)
        Me.Panel1.Controls.Add(Me.lblNum)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(640, 64)
        Me.Panel1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(40, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 15)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "PAN No."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_PANNum
        '
        Appearance14.FontData.BoldAsString = "False"
        Me.txt_PANNum.Appearance = Appearance14
        Me.txt_PANNum.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PANNum.Location = New System.Drawing.Point(96, 35)
        Me.txt_PANNum.Name = "txt_PANNum"
        Me.txt_PANNum.Size = New System.Drawing.Size(213, 21)
        Me.txt_PANNum.TabIndex = 10
        Me.txt_PANNum.Text = "UltraTextEditor3"
        '
        'txt_CompName
        '
        Me.txt_CompName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance15.FontData.BoldAsString = "True"
        Appearance15.FontData.SizeInPoints = 11.0!
        Me.txt_CompName.Appearance = Appearance15
        Me.txt_CompName.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_CompName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_CompName.Location = New System.Drawing.Point(96, 8)
        Me.txt_CompName.Name = "txt_CompName"
        Me.txt_CompName.Size = New System.Drawing.Size(518, 22)
        Me.txt_CompName.TabIndex = 1
        Me.txt_CompName.Text = "ULTRATEXTEDITOR2"
        '
        'lblNum
        '
        Me.lblNum.AutoSize = True
        Me.lblNum.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNum.Location = New System.Drawing.Point(35, 10)
        Me.lblNum.Name = "lblNum"
        Me.lblNum.Size = New System.Drawing.Size(58, 18)
        Me.lblNum.TabIndex = 0
        Me.lblNum.Text = "* Name"
        Me.lblNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraLabel1)
        Me.UltraTabPageControl2.Controls.Add(Me.txt_BrandName)
        Me.UltraTabPageControl2.Controls.Add(Me.Label1)
        Me.UltraTabPageControl2.Controls.Add(Me.cmb_HOCampusID)
        Me.UltraTabPageControl2.Controls.Add(Me.UltraLabel13)
        Me.UltraTabPageControl2.Controls.Add(Me.dt_FinStartDate)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(640, 431)
        '
        'UltraLabel1
        '
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(49, 79)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(44, 14)
        Me.UltraLabel1.TabIndex = 12
        Me.UltraLabel1.Text = "BRAND"
        '
        'txt_BrandName
        '
        Appearance16.FontData.BoldAsString = "False"
        Me.txt_BrandName.Appearance = Appearance16
        Me.txt_BrandName.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_BrandName.Location = New System.Drawing.Point(98, 77)
        Me.txt_BrandName.Name = "txt_BrandName"
        Me.txt_BrandName.Size = New System.Drawing.Size(120, 17)
        Me.txt_BrandName.TabIndex = 11
        Me.txt_BrandName.Text = "UltraTextEditor3"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "HO Campus"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_HOCampusID
        '
        Appearance17.FontData.BoldAsString = "False"
        Me.cmb_HOCampusID.Appearance = Appearance17
        Me.cmb_HOCampusID.Location = New System.Drawing.Point(96, 135)
        Me.cmb_HOCampusID.Name = "cmb_HOCampusID"
        Me.cmb_HOCampusID.Size = New System.Drawing.Size(179, 22)
        Me.cmb_HOCampusID.TabIndex = 3
        Me.cmb_HOCampusID.Text = "UltraCombo5"
        '
        'UltraLabel13
        '
        Me.UltraLabel13.AutoSize = True
        Me.UltraLabel13.Location = New System.Drawing.Point(19, 108)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(74, 14)
        Me.UltraLabel13.TabIndex = 0
        Me.UltraLabel13.Text = "Fin Start Date"
        '
        'dt_FinStartDate
        '
        Me.dt_FinStartDate.Location = New System.Drawing.Point(96, 105)
        Me.dt_FinStartDate.Name = "dt_FinStartDate"
        Me.dt_FinStartDate.Size = New System.Drawing.Size(179, 21)
        Me.dt_FinStartDate.TabIndex = 1
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.pic_Logo)
        Me.UltraTabPageControl3.Controls.Add(Me.Panel13)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(640, 431)
        '
        'pic_Logo
        '
        Me.pic_Logo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pic_Logo.Location = New System.Drawing.Point(0, 0)
        Me.pic_Logo.Name = "pic_Logo"
        Me.pic_Logo.Size = New System.Drawing.Size(640, 87)
        Me.pic_Logo.TabIndex = 20
        Me.pic_Logo.TabStop = False
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.btnSavepic)
        Me.Panel13.Controls.Add(Me.btnBrowsePic)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(640, 431)
        Me.Panel13.TabIndex = 0
        '
        'btnSavepic
        '
        Me.btnSavepic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance18.FontData.BoldAsString = "True"
        Me.btnSavepic.Appearance = Appearance18
        Me.btnSavepic.Location = New System.Drawing.Point(541, 93)
        Me.btnSavepic.Name = "btnSavepic"
        Me.btnSavepic.Size = New System.Drawing.Size(89, 32)
        Me.btnSavepic.TabIndex = 1
        Me.btnSavepic.Text = "Save As ..."
        '
        'btnBrowsePic
        '
        Me.btnBrowsePic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance19.FontData.BoldAsString = "True"
        Me.btnBrowsePic.Appearance = Appearance19
        Me.btnBrowsePic.Location = New System.Drawing.Point(446, 93)
        Me.btnBrowsePic.Name = "btnBrowsePic"
        Me.btnBrowsePic.Size = New System.Drawing.Size(89, 32)
        Me.btnBrowsePic.TabIndex = 0
        Me.btnBrowsePic.Text = "Browse ..."
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance20.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance20
        Me.UltraTabControl1.SharedControls.AddRange(New System.Windows.Forms.Control() {Me.Panel1})
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(644, 457)
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Basic"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Config"
        UltraTab3.TabPage = Me.UltraTabPageControl3
        UltraTab3.Text = "Logo"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3})
        Me.UltraTabControl1.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Controls.Add(Me.Panel1)
        Me.UltraTabSharedControlsPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(640, 431)
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 457)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(644, 36)
        Me.Panel4.TabIndex = 1
        '
        'btnSave
        '
        Appearance21.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance21
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(380, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 36)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance22.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance22
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(468, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 36)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance23.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance23
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(556, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 36)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|BMP Files|*.bmp"
        '
        'btnSearch
        '
        Me.btnSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSearch.Location = New System.Drawing.Point(420, 0)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(72, 30)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "Search"
        '
        'frmGstCompany
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Company"
        Me.ClientSize = New System.Drawing.Size(644, 493)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGstCompany"
        Me.Text = "Company"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.UltraTabPageControl5.ResumeLayout(False)
        CType(Me.UltraGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl2.ResumeLayout(False)
        Me.GrpAddress.ResumeLayout(False)
        Me.GrpAddress.PerformLayout()
        CType(Me.cmb_Country, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_State, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_FaxNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_FaxArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_FaxCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PhNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PhArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PhCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Web, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_City, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Address, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txt_PANNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CompName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        CType(Me.txt_BrandName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_HOCampusID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_FinStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.pic_Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel13.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.UltraTabSharedControlsPage1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_HOCampusID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_FinStartDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents btnSavepic As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnBrowsePic As Infragistics.Win.Misc.UltraButton
    Friend WithEvents pic_Logo As System.Windows.Forms.PictureBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cmb_Country As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmb_State As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents UltraTabControl2 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGrid1 As UltraGrid
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGrid2 As UltraGrid
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents txt_PANNum As UltraTextEditor
    Friend WithEvents Panel6 As Windows.Forms.Panel
    Friend WithEvents btnDelReg As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddReg As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnDelete As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddNew As Infragistics.Win.Misc.UltraButton
    Friend WithEvents txt_BrandName As UltraTextEditor
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents btnSearch As Infragistics.Win.Misc.UltraButton

#End Region
End Class

