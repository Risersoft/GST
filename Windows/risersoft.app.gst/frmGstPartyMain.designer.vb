Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmGstPartyMain
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
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnAddNew As Infragistics.Win.Misc.UltraButton
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
    Friend WithEvents txt_Description As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_Area As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_Turnover As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_Remark As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_NumEmp As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents chk_IsTrader As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chk_IsInspAgency As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chk_IsContractor As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chk_IsConsultant As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chk_IsEndUser As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chk_IsEPC As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chk_IsDealer As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents cmb_IsGovt As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents UltraGridSel As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Public WithEvents GrpAddress As System.Windows.Forms.GroupBox
    Friend WithEvents btnCreate As Infragistics.Win.Misc.UltraButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
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
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraGridSel = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnCreate = New Infragistics.Win.Misc.UltraButton()
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddNew = New Infragistics.Win.Misc.UltraButton()
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
        Me.cmb_mpnick = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txt_MPName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblNum = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmb_PartySubType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmb_PartyType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_IsGovt = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_Remark = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Description = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Area = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_NumEmp = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Turnover = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chk_IsCompetitor = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_IsTrader = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_IsInspAgency = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_IsContractor = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_IsConsultant = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_IsEndUser = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_IsEPC = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_IsDealer = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.UltraGridSel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
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
        CType(Me.cmb_mpnick, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_MPName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.cmb_PartySubType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_PartyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_IsGovt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Remark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Area, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_NumEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Turnover, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.chk_IsCompetitor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_IsTrader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_IsInspAgency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_IsContractor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_IsConsultant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_IsEndUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_IsEPC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_IsDealer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraTabSharedControlsPage1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl1.Controls.Add(Me.GrpAddress)
        Me.UltraTabPageControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(640, 424)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGridSel)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 147)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(640, 277)
        Me.Panel2.TabIndex = 16
        '
        'UltraGridSel
        '
        Me.UltraGridSel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridSel.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridSel.Name = "UltraGridSel"
        Me.UltraGridSel.Size = New System.Drawing.Size(640, 247)
        Me.UltraGridSel.TabIndex = 0
        Me.UltraGridSel.Text = "Selectable Parties"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnCreate)
        Me.Panel3.Controls.Add(Me.btnEdit)
        Me.Panel3.Controls.Add(Me.btnAddNew)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 247)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(640, 30)
        Me.Panel3.TabIndex = 7
        '
        'btnCreate
        '
        Me.btnCreate.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCreate.Location = New System.Drawing.Point(0, 0)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(104, 30)
        Me.btnCreate.TabIndex = 0
        Me.btnCreate.Text = "Create Default"
        '
        'btnEdit
        '
        Me.btnEdit.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnEdit.Location = New System.Drawing.Point(498, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 30)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "Edit"
        '
        'btnAddNew
        '
        Me.btnAddNew.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAddNew.Location = New System.Drawing.Point(568, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(72, 30)
        Me.btnAddNew.TabIndex = 2
        Me.btnAddNew.Text = "Add New"
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
        Me.GrpAddress.Location = New System.Drawing.Point(0, 0)
        Me.GrpAddress.Name = "GrpAddress"
        Me.GrpAddress.Size = New System.Drawing.Size(640, 147)
        Me.GrpAddress.TabIndex = 1
        Me.GrpAddress.TabStop = False
        Me.GrpAddress.Text = "Head Office"
        '
        'cmb_Country
        '
        Appearance1.FontData.BoldAsString = "False"
        Me.cmb_Country.Appearance = Appearance1
        Me.cmb_Country.Location = New System.Drawing.Point(96, 88)
        Me.cmb_Country.Name = "cmb_Country"
        Me.cmb_Country.Size = New System.Drawing.Size(200, 22)
        Me.cmb_Country.TabIndex = 7
        Me.cmb_Country.Text = "UltraCombo5"
        '
        'cmb_State
        '
        Appearance2.FontData.BoldAsString = "False"
        Me.cmb_State.Appearance = Appearance2
        Me.cmb_State.Location = New System.Drawing.Point(96, 117)
        Me.cmb_State.Name = "cmb_State"
        Me.cmb_State.Size = New System.Drawing.Size(200, 22)
        Me.cmb_State.TabIndex = 9
        Me.cmb_State.Text = "UltraCombo5"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(51, 121)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 14)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "* State"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(35, 92)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 14)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "* Country"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(319, 120)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 14)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Email"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Email
        '
        Appearance3.FontData.BoldAsString = "False"
        Me.txt_Email.Appearance = Appearance3
        Me.txt_Email.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Email.Location = New System.Drawing.Point(358, 119)
        Me.txt_Email.Name = "txt_Email"
        Me.txt_Email.Size = New System.Drawing.Size(256, 17)
        Me.txt_Email.TabIndex = 21
        Me.txt_Email.Text = "UltraTextEditor12"
        '
        'txt_FaxNum
        '
        Appearance4.FontData.BoldAsString = "False"
        Me.txt_FaxNum.Appearance = Appearance4
        Me.txt_FaxNum.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_FaxNum.Location = New System.Drawing.Point(436, 92)
        Me.txt_FaxNum.Name = "txt_FaxNum"
        Me.txt_FaxNum.Size = New System.Drawing.Size(178, 17)
        Me.txt_FaxNum.TabIndex = 19
        Me.txt_FaxNum.Text = "UltraTextEditor9"
        '
        'txt_FaxArea
        '
        Appearance5.FontData.BoldAsString = "False"
        Me.txt_FaxArea.Appearance = Appearance5
        Me.txt_FaxArea.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_FaxArea.Location = New System.Drawing.Point(396, 91)
        Me.txt_FaxArea.Name = "txt_FaxArea"
        Me.txt_FaxArea.Size = New System.Drawing.Size(32, 17)
        Me.txt_FaxArea.TabIndex = 18
        Me.txt_FaxArea.Text = "UltraTextEditor10"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(310, 92)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 14)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Fax No."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_FaxCountry
        '
        Appearance6.FontData.BoldAsString = "False"
        Me.txt_FaxCountry.Appearance = Appearance6
        Me.txt_FaxCountry.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_FaxCountry.Location = New System.Drawing.Point(358, 91)
        Me.txt_FaxCountry.Name = "txt_FaxCountry"
        Me.txt_FaxCountry.Size = New System.Drawing.Size(30, 17)
        Me.txt_FaxCountry.TabIndex = 17
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
        Me.txt_PhNum.TabIndex = 15
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
        Me.txt_PhArea.TabIndex = 14
        Me.txt_PhArea.Text = "UltraTextEditor7"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(311, 65)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 14)
        Me.Label13.TabIndex = 12
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
        Me.txt_PhCountry.TabIndex = 13
        Me.txt_PhCountry.Text = "UltraTextEditor6"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(42, 66)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 14)
        Me.Label14.TabIndex = 4
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
        Me.txt_PinCode.Size = New System.Drawing.Size(200, 17)
        Me.txt_PinCode.TabIndex = 5
        Me.txt_PinCode.Text = "UltraTextEditor5"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(324, 41)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 14)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Web"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Web
        '
        Appearance11.FontData.BoldAsString = "False"
        Me.txt_Web.Appearance = Appearance11
        Me.txt_Web.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Web.Location = New System.Drawing.Point(358, 40)
        Me.txt_Web.Name = "txt_Web"
        Me.txt_Web.Size = New System.Drawing.Size(256, 17)
        Me.txt_Web.TabIndex = 11
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
        Me.txt_City.Location = New System.Drawing.Point(96, 40)
        Me.txt_City.Name = "txt_City"
        Me.txt_City.Size = New System.Drawing.Size(200, 17)
        Me.txt_City.TabIndex = 3
        Me.txt_City.Text = "UltraTextEditor3"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(31, 18)
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
        Me.txt_Address.Location = New System.Drawing.Point(96, 16)
        Me.txt_Address.Name = "txt_Address"
        Me.txt_Address.Size = New System.Drawing.Size(518, 17)
        Me.txt_Address.TabIndex = 1
        Me.txt_Address.Text = "UltraTextEditor2"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmb_mpnick)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.txt_MPName)
        Me.Panel1.Controls.Add(Me.lblNum)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(640, 64)
        Me.Panel1.TabIndex = 0
        '
        'cmb_mpnick
        '
        Me.cmb_mpnick.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cmb_mpnick.Location = New System.Drawing.Point(94, 38)
        Me.cmb_mpnick.Name = "cmb_mpnick"
        Me.cmb_mpnick.Size = New System.Drawing.Size(200, 21)
        Me.cmb_mpnick.TabIndex = 7
        Me.cmb_mpnick.Text = "ULTRACOMBOEDITOR1"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(30, 38)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 16)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "Nick"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_MPName
        '
        Appearance14.FontData.BoldAsString = "True"
        Appearance14.FontData.SizeInPoints = 11.0!
        Me.txt_MPName.Appearance = Appearance14
        Me.txt_MPName.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_MPName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_MPName.Location = New System.Drawing.Point(94, 6)
        Me.txt_MPName.Name = "txt_MPName"
        Me.txt_MPName.Size = New System.Drawing.Size(456, 22)
        Me.txt_MPName.TabIndex = 5
        Me.txt_MPName.Text = "ULTRATEXTEDITOR2"
        '
        'lblNum
        '
        Me.lblNum.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNum.Location = New System.Drawing.Point(30, 6)
        Me.lblNum.Name = "lblNum"
        Me.lblNum.Size = New System.Drawing.Size(56, 16)
        Me.lblNum.TabIndex = 4
        Me.lblNum.Text = "Name"
        Me.lblNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.Label19)
        Me.UltraTabPageControl2.Controls.Add(Me.cmb_PartySubType)
        Me.UltraTabPageControl2.Controls.Add(Me.Label8)
        Me.UltraTabPageControl2.Controls.Add(Me.cmb_PartyType)
        Me.UltraTabPageControl2.Controls.Add(Me.cmb_IsGovt)
        Me.UltraTabPageControl2.Controls.Add(Me.Label6)
        Me.UltraTabPageControl2.Controls.Add(Me.txt_Remark)
        Me.UltraTabPageControl2.Controls.Add(Me.Label4)
        Me.UltraTabPageControl2.Controls.Add(Me.Label1)
        Me.UltraTabPageControl2.Controls.Add(Me.txt_Description)
        Me.UltraTabPageControl2.Controls.Add(Me.Label2)
        Me.UltraTabPageControl2.Controls.Add(Me.txt_Area)
        Me.UltraTabPageControl2.Controls.Add(Me.Label5)
        Me.UltraTabPageControl2.Controls.Add(Me.txt_NumEmp)
        Me.UltraTabPageControl2.Controls.Add(Me.Label3)
        Me.UltraTabPageControl2.Controls.Add(Me.txt_Turnover)
        Me.UltraTabPageControl2.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl2.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(640, 424)
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(24, 326)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 16)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "Sub Type"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_PartySubType
        '
        Appearance15.FontData.BoldAsString = "False"
        Me.cmb_PartySubType.Appearance = Appearance15
        Me.cmb_PartySubType.Location = New System.Drawing.Point(96, 323)
        Me.cmb_PartySubType.Name = "cmb_PartySubType"
        Me.cmb_PartySubType.ReadOnly = True
        Me.cmb_PartySubType.Size = New System.Drawing.Size(179, 22)
        Me.cmb_PartySubType.TabIndex = 21
        Me.cmb_PartySubType.Text = "UltraCombo5"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(24, 296)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 16)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_PartyType
        '
        Appearance16.FontData.BoldAsString = "False"
        Me.cmb_PartyType.Appearance = Appearance16
        Me.cmb_PartyType.Location = New System.Drawing.Point(96, 293)
        Me.cmb_PartyType.Name = "cmb_PartyType"
        Me.cmb_PartyType.ReadOnly = True
        Me.cmb_PartyType.Size = New System.Drawing.Size(179, 22)
        Me.cmb_PartyType.TabIndex = 19
        Me.cmb_PartyType.Text = "UltraCombo5"
        '
        'cmb_IsGovt
        '
        Me.cmb_IsGovt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ValueListItem1.DataValue = False
        ValueListItem1.DisplayText = "Private"
        ValueListItem2.DataValue = True
        ValueListItem2.DisplayText = "Government"
        Me.cmb_IsGovt.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2})
        Me.cmb_IsGovt.Location = New System.Drawing.Point(96, 151)
        Me.cmb_IsGovt.Name = "cmb_IsGovt"
        Me.cmb_IsGovt.Size = New System.Drawing.Size(289, 21)
        Me.cmb_IsGovt.TabIndex = 9
        Me.cmb_IsGovt.Text = "UltraComboEditor9"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(50, 206)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 14)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Remark"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Remark
        '
        Me.txt_Remark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance17.FontData.BoldAsString = "False"
        Me.txt_Remark.Appearance = Appearance17
        Me.txt_Remark.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Remark.Location = New System.Drawing.Point(96, 203)
        Me.txt_Remark.Multiline = True
        Me.txt_Remark.Name = "txt_Remark"
        Me.txt_Remark.Size = New System.Drawing.Size(289, 48)
        Me.txt_Remark.TabIndex = 12
        Me.txt_Remark.Text = "UltraTextEditor7"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 154)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 14)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Ownership"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 180)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Description"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Description
        '
        Me.txt_Description.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance18.FontData.BoldAsString = "False"
        Me.txt_Description.Appearance = Appearance18
        Me.txt_Description.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Description.Location = New System.Drawing.Point(96, 179)
        Me.txt_Description.Name = "txt_Description"
        Me.txt_Description.Size = New System.Drawing.Size(289, 17)
        Me.txt_Description.TabIndex = 7
        Me.txt_Description.Text = "UltraTextEditor1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(62, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Area"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Area
        '
        Appearance19.FontData.BoldAsString = "False"
        Me.txt_Area.Appearance = Appearance19
        Me.txt_Area.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Area.Location = New System.Drawing.Point(96, 127)
        Me.txt_Area.Name = "txt_Area"
        Me.txt_Area.Size = New System.Drawing.Size(200, 17)
        Me.txt_Area.TabIndex = 5
        Me.txt_Area.Text = "UltraTextEditor4"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 260)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 14)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "No. of employees"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_NumEmp
        '
        Appearance20.FontData.BoldAsString = "False"
        Me.txt_NumEmp.Appearance = Appearance20
        Me.txt_NumEmp.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_NumEmp.Location = New System.Drawing.Point(96, 259)
        Me.txt_NumEmp.Name = "txt_NumEmp"
        Me.txt_NumEmp.Size = New System.Drawing.Size(200, 17)
        Me.txt_NumEmp.TabIndex = 3
        Me.txt_NumEmp.Text = "UltraTextEditor5"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(42, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 14)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Turnover"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Turnover
        '
        Appearance21.FontData.BoldAsString = "False"
        Me.txt_Turnover.Appearance = Appearance21
        Me.txt_Turnover.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Turnover.Location = New System.Drawing.Point(96, 103)
        Me.txt_Turnover.Name = "txt_Turnover"
        Me.txt_Turnover.Size = New System.Drawing.Size(200, 17)
        Me.txt_Turnover.TabIndex = 1
        Me.txt_Turnover.Text = "UltraTextEditor3"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chk_IsCompetitor)
        Me.GroupBox1.Controls.Add(Me.chk_IsTrader)
        Me.GroupBox1.Controls.Add(Me.chk_IsInspAgency)
        Me.GroupBox1.Controls.Add(Me.chk_IsContractor)
        Me.GroupBox1.Controls.Add(Me.chk_IsConsultant)
        Me.GroupBox1.Controls.Add(Me.chk_IsEndUser)
        Me.GroupBox1.Controls.Add(Me.chk_IsEPC)
        Me.GroupBox1.Controls.Add(Me.chk_IsDealer)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(415, 73)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(199, 216)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Click All that Apply"
        '
        'chk_IsCompetitor
        '
        Appearance22.FontData.BoldAsString = "False"
        Appearance22.FontData.SizeInPoints = 8.0!
        Me.chk_IsCompetitor.Appearance = Appearance22
        Me.chk_IsCompetitor.Location = New System.Drawing.Point(16, 188)
        Me.chk_IsCompetitor.Name = "chk_IsCompetitor"
        Me.chk_IsCompetitor.Size = New System.Drawing.Size(152, 16)
        Me.chk_IsCompetitor.TabIndex = 7
        Me.chk_IsCompetitor.Text = "Competitor"
        '
        'chk_IsTrader
        '
        Appearance23.FontData.BoldAsString = "False"
        Appearance23.FontData.SizeInPoints = 8.0!
        Me.chk_IsTrader.Appearance = Appearance23
        Me.chk_IsTrader.Location = New System.Drawing.Point(16, 166)
        Me.chk_IsTrader.Name = "chk_IsTrader"
        Me.chk_IsTrader.Size = New System.Drawing.Size(152, 16)
        Me.chk_IsTrader.TabIndex = 6
        Me.chk_IsTrader.Text = "Trader"
        '
        'chk_IsInspAgency
        '
        Appearance24.FontData.BoldAsString = "False"
        Appearance24.FontData.SizeInPoints = 8.0!
        Me.chk_IsInspAgency.Appearance = Appearance24
        Me.chk_IsInspAgency.Location = New System.Drawing.Point(16, 142)
        Me.chk_IsInspAgency.Name = "chk_IsInspAgency"
        Me.chk_IsInspAgency.Size = New System.Drawing.Size(152, 16)
        Me.chk_IsInspAgency.TabIndex = 5
        Me.chk_IsInspAgency.Text = "Inspection Agency"
        '
        'chk_IsContractor
        '
        Appearance25.FontData.BoldAsString = "False"
        Appearance25.FontData.SizeInPoints = 8.0!
        Me.chk_IsContractor.Appearance = Appearance25
        Me.chk_IsContractor.Location = New System.Drawing.Point(16, 118)
        Me.chk_IsContractor.Name = "chk_IsContractor"
        Me.chk_IsContractor.Size = New System.Drawing.Size(152, 16)
        Me.chk_IsContractor.TabIndex = 4
        Me.chk_IsContractor.Text = "Contractor"
        '
        'chk_IsConsultant
        '
        Appearance26.FontData.BoldAsString = "False"
        Appearance26.FontData.SizeInPoints = 8.0!
        Me.chk_IsConsultant.Appearance = Appearance26
        Me.chk_IsConsultant.Location = New System.Drawing.Point(16, 94)
        Me.chk_IsConsultant.Name = "chk_IsConsultant"
        Me.chk_IsConsultant.Size = New System.Drawing.Size(152, 16)
        Me.chk_IsConsultant.TabIndex = 3
        Me.chk_IsConsultant.Text = "Consultant"
        '
        'chk_IsEndUser
        '
        Appearance27.FontData.BoldAsString = "False"
        Appearance27.FontData.SizeInPoints = 8.0!
        Me.chk_IsEndUser.Appearance = Appearance27
        Me.chk_IsEndUser.Location = New System.Drawing.Point(16, 70)
        Me.chk_IsEndUser.Name = "chk_IsEndUser"
        Me.chk_IsEndUser.Size = New System.Drawing.Size(152, 16)
        Me.chk_IsEndUser.TabIndex = 2
        Me.chk_IsEndUser.Text = "End User"
        '
        'chk_IsEPC
        '
        Appearance28.FontData.BoldAsString = "False"
        Appearance28.FontData.SizeInPoints = 8.0!
        Me.chk_IsEPC.Appearance = Appearance28
        Me.chk_IsEPC.Location = New System.Drawing.Point(16, 46)
        Me.chk_IsEPC.Name = "chk_IsEPC"
        Me.chk_IsEPC.Size = New System.Drawing.Size(152, 16)
        Me.chk_IsEPC.TabIndex = 1
        Me.chk_IsEPC.Text = "EPC"
        '
        'chk_IsDealer
        '
        Appearance29.FontData.BoldAsString = "False"
        Appearance29.FontData.SizeInPoints = 8.0!
        Me.chk_IsDealer.Appearance = Appearance29
        Me.chk_IsDealer.Location = New System.Drawing.Point(16, 25)
        Me.chk_IsDealer.Name = "chk_IsDealer"
        Me.chk_IsDealer.Size = New System.Drawing.Size(152, 16)
        Me.chk_IsDealer.TabIndex = 0
        Me.chk_IsDealer.Text = "Dealer"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance30.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance30
        Me.UltraTabControl1.SharedControls.AddRange(New System.Windows.Forms.Control() {Me.Panel1})
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(644, 450)
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Basic"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Info"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.TabsPerRow = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Controls.Add(Me.Panel1)
        Me.UltraTabSharedControlsPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(640, 424)
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 450)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(644, 36)
        Me.Panel4.TabIndex = 1
        '
        'btnSave
        '
        Appearance31.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance31
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(380, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 36)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance32.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance32
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(468, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 36)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance33.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance33
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(556, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 36)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'frmGstPartyMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Party"
        Me.ClientSize = New System.Drawing.Size(644, 486)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGstPartyMain"
        Me.Text = "Party"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraGridSel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
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
        CType(Me.cmb_mpnick, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_MPName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        CType(Me.cmb_PartySubType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_PartyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_IsGovt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Remark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Area, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_NumEmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Turnover, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.chk_IsCompetitor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_IsTrader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_IsInspAgency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_IsContractor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_IsConsultant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_IsEndUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_IsEPC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_IsDealer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.UltraTabSharedControlsPage1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chk_IsCompetitor As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents cmb_Country As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmb_State As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmb_mpnick As UltraComboEditor
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents txt_MPName As UltraTextEditor
    Friend WithEvents lblNum As Windows.Forms.Label
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents cmb_PartySubType As UltraCombo
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents cmb_PartyType As UltraCombo

#End Region
End Class

