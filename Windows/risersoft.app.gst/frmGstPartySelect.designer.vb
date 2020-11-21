Imports System.Windows.Forms
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmGstPartySelect
    Inherits frmMax


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.InitForm()
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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_Division As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_SelEmail As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_SelFaxNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_SelFaxArea As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_SelFaxCountry As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_SelPhNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_SelPhArea As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_SelPhCOuntry As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_SelPinCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_SelWeb As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_SelCity As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_SelAddress As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents chk_sameashead As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents btnCopyAdd As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmb_SelState As Infragistics.Win.UltraWinGrid.UltraCombo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
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
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance39 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance40 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance41 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance42 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance43 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance44 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance45 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance46 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance47 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance48 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance49 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance50 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance51 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab7 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab6 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance52 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance53 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance54 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl17 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.cmb_ITCInElg = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.LabelIneligible = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_ContactPersonName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmb_SelCountry = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_SelAddress = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_SelPinCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelEmail = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmb_SelState = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_SelWeb = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.chk_sameashead = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_SelFaxArea = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelFaxCountry = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_SelFaxNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelPhNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelPhArea = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelPhCOuntry = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl18 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraLabel20 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_TinNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmb_TaxAreaID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_ECCRange = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_ECCDiv = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel16 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_PanNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_CSTNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_ECCNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_STNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel11 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_perexcise = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_perstax = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_insurance = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.cmb_transmode = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.cmb_payterms = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.cmb_shipterms = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.UltraLabel10 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel9 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_discount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel14 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Excise = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SalesTax = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel15 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Freight = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Division = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_SelCity = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.btnCopyAdd = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage3 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraPanelSub = New Infragistics.Win.Misc.UltraPanel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmb_GstRegType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_GSTIN = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txt_Code = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblNum = New System.Windows.Forms.Label()
        Me.txt_SelEMailCc = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_SelEMailBcc = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label14 = New System.Windows.Forms.Label()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl17.SuspendLayout()
        CType(Me.cmb_ITCInElg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ContactPersonName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_SelCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelPinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_SelState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelWeb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_sameashead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelFaxArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelFaxCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelFaxNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelPhNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelPhArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelPhCOuntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl18.SuspendLayout()
        CType(Me.txt_TinNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_TaxAreaID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ECCRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ECCDiv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PanNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CSTNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ECCNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_STNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.txt_perexcise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_perstax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_insurance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_transmode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_payterms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_shipterms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_discount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Excise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SalesTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Freight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.txt_Division, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelCity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraPanelSub.ClientArea.SuspendLayout()
        Me.UltraPanelSub.SuspendLayout()
        CType(Me.cmb_GstRegType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_GSTIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelEMailCc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelEMailBcc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraTabPageControl17
        '
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelEMailBcc)
        Me.UltraTabPageControl17.Controls.Add(Me.Label14)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelEMailCc)
        Me.UltraTabPageControl17.Controls.Add(Me.Label13)
        Me.UltraTabPageControl17.Controls.Add(Me.cmb_ITCInElg)
        Me.UltraTabPageControl17.Controls.Add(Me.LabelIneligible)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_ContactPersonName)
        Me.UltraTabPageControl17.Controls.Add(Me.Label9)
        Me.UltraTabPageControl17.Controls.Add(Me.Label10)
        Me.UltraTabPageControl17.Controls.Add(Me.cmb_SelCountry)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelAddress)
        Me.UltraTabPageControl17.Controls.Add(Me.Label11)
        Me.UltraTabPageControl17.Controls.Add(Me.Label2)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelPinCode)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelEmail)
        Me.UltraTabPageControl17.Controls.Add(Me.Label8)
        Me.UltraTabPageControl17.Controls.Add(Me.cmb_SelState)
        Me.UltraTabPageControl17.Controls.Add(Me.Label5)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelWeb)
        Me.UltraTabPageControl17.Controls.Add(Me.chk_sameashead)
        Me.UltraTabPageControl17.Controls.Add(Me.Label7)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelFaxArea)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelFaxCountry)
        Me.UltraTabPageControl17.Controls.Add(Me.Label4)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelFaxNum)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelPhNum)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelPhArea)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelPhCOuntry)
        Me.UltraTabPageControl17.Controls.Add(Me.Label6)
        Me.UltraTabPageControl17.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl17.Name = "UltraTabPageControl17"
        Me.UltraTabPageControl17.Size = New System.Drawing.Size(682, 273)
        '
        'cmb_ITCInElg
        '
        Me.cmb_ITCInElg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ITCInElg.Location = New System.Drawing.Point(109, 203)
        Me.cmb_ITCInElg.Name = "cmb_ITCInElg"
        Me.cmb_ITCInElg.Size = New System.Drawing.Size(105, 21)
        Me.cmb_ITCInElg.TabIndex = 42
        Me.cmb_ITCInElg.Text = "UltraComboEditor1"
        '
        'LabelIneligible
        '
        Me.LabelIneligible.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        Appearance3.TextVAlignAsString = "Middle"
        Me.LabelIneligible.Appearance = Appearance3
        Me.LabelIneligible.AutoSize = True
        Me.LabelIneligible.Location = New System.Drawing.Point(33, 206)
        Me.LabelIneligible.Name = "LabelIneligible"
        Me.LabelIneligible.Size = New System.Drawing.Size(70, 14)
        Me.LabelIneligible.TabIndex = 43
        Me.LabelIneligible.Text = "ITC Ineligible"
        '
        'txt_ContactPersonName
        '
        Appearance4.FontData.BoldAsString = "False"
        Me.txt_ContactPersonName.Appearance = Appearance4
        Me.txt_ContactPersonName.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_ContactPersonName.Location = New System.Drawing.Point(110, 167)
        Me.txt_ContactPersonName.Name = "txt_ContactPersonName"
        Me.txt_ContactPersonName.Size = New System.Drawing.Size(456, 17)
        Me.txt_ContactPersonName.TabIndex = 22
        Me.txt_ContactPersonName.Text = "UltraTextEditor2"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(11, 163)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 34)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Contact Person Name"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(5, 109)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 16)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Country"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_SelCountry
        '
        Appearance5.FontData.BoldAsString = "False"
        Me.cmb_SelCountry.Appearance = Appearance5
        Me.cmb_SelCountry.Location = New System.Drawing.Point(109, 109)
        Me.cmb_SelCountry.Name = "cmb_SelCountry"
        Me.cmb_SelCountry.Size = New System.Drawing.Size(256, 22)
        Me.cmb_SelCountry.TabIndex = 17
        Me.cmb_SelCountry.Text = "UltraCombo5"
        '
        'txt_SelAddress
        '
        Appearance6.FontData.BoldAsString = "False"
        Me.txt_SelAddress.Appearance = Appearance6
        Me.txt_SelAddress.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelAddress.Location = New System.Drawing.Point(109, 14)
        Me.txt_SelAddress.Name = "txt_SelAddress"
        Me.txt_SelAddress.Size = New System.Drawing.Size(456, 17)
        Me.txt_SelAddress.TabIndex = 1
        Me.txt_SelAddress.Text = "UltraTextEditor2"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(5, 135)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 16)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "State"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(37, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Address"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelPinCode
        '
        Appearance7.FontData.BoldAsString = "False"
        Me.txt_SelPinCode.Appearance = Appearance7
        Me.txt_SelPinCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelPinCode.Location = New System.Drawing.Point(109, 38)
        Me.txt_SelPinCode.Name = "txt_SelPinCode"
        Me.txt_SelPinCode.Size = New System.Drawing.Size(120, 17)
        Me.txt_SelPinCode.TabIndex = 3
        Me.txt_SelPinCode.Text = "UltraTextEditor5"
        '
        'txt_SelEmail
        '
        Appearance8.FontData.BoldAsString = "False"
        Me.txt_SelEmail.Appearance = Appearance8
        Me.txt_SelEmail.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelEmail.Location = New System.Drawing.Point(109, 62)
        Me.txt_SelEmail.Name = "txt_SelEmail"
        Me.txt_SelEmail.Size = New System.Drawing.Size(256, 17)
        Me.txt_SelEmail.TabIndex = 5
        Me.txt_SelEmail.Text = "UltraTextEditor12"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(53, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 16)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Email"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_SelState
        '
        Appearance9.FontData.BoldAsString = "False"
        Me.cmb_SelState.Appearance = Appearance9
        Me.cmb_SelState.Location = New System.Drawing.Point(109, 135)
        Me.cmb_SelState.Name = "cmb_SelState"
        Me.cmb_SelState.Size = New System.Drawing.Size(256, 22)
        Me.cmb_SelState.TabIndex = 19
        Me.cmb_SelState.Text = "UltraCombo5"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(37, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Pincode"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelWeb
        '
        Appearance10.FontData.BoldAsString = "False"
        Me.txt_SelWeb.Appearance = Appearance10
        Me.txt_SelWeb.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelWeb.Location = New System.Drawing.Point(442, 62)
        Me.txt_SelWeb.Name = "txt_SelWeb"
        Me.txt_SelWeb.Size = New System.Drawing.Size(224, 17)
        Me.txt_SelWeb.TabIndex = 7
        Me.txt_SelWeb.Text = "UltraTextEditor4"
        '
        'chk_sameashead
        '
        Me.chk_sameashead.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_sameashead.Location = New System.Drawing.Point(389, 139)
        Me.chk_sameashead.Name = "chk_sameashead"
        Me.chk_sameashead.Size = New System.Drawing.Size(112, 24)
        Me.chk_sameashead.TabIndex = 20
        Me.chk_sameashead.Text = "Address is Same as Head Office"
        Me.chk_sameashead.Visible = False
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(386, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 16)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Fax No."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelFaxArea
        '
        Appearance11.FontData.BoldAsString = "False"
        Me.txt_SelFaxArea.Appearance = Appearance11
        Me.txt_SelFaxArea.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelFaxArea.Location = New System.Drawing.Point(482, 86)
        Me.txt_SelFaxArea.Name = "txt_SelFaxArea"
        Me.txt_SelFaxArea.Size = New System.Drawing.Size(32, 17)
        Me.txt_SelFaxArea.TabIndex = 14
        Me.txt_SelFaxArea.Text = "UltraTextEditor10"
        '
        'txt_SelFaxCountry
        '
        Appearance12.FontData.BoldAsString = "False"
        Me.txt_SelFaxCountry.Appearance = Appearance12
        Me.txt_SelFaxCountry.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelFaxCountry.Location = New System.Drawing.Point(442, 86)
        Me.txt_SelFaxCountry.Name = "txt_SelFaxCountry"
        Me.txt_SelFaxCountry.Size = New System.Drawing.Size(32, 17)
        Me.txt_SelFaxCountry.TabIndex = 13
        Me.txt_SelFaxCountry.Text = "UltraTextEditor11"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(370, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Web"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelFaxNum
        '
        Appearance13.FontData.BoldAsString = "False"
        Me.txt_SelFaxNum.Appearance = Appearance13
        Me.txt_SelFaxNum.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelFaxNum.Location = New System.Drawing.Point(522, 86)
        Me.txt_SelFaxNum.Name = "txt_SelFaxNum"
        Me.txt_SelFaxNum.Size = New System.Drawing.Size(144, 17)
        Me.txt_SelFaxNum.TabIndex = 15
        Me.txt_SelFaxNum.Text = "UltraTextEditor9"
        '
        'txt_SelPhNum
        '
        Appearance14.FontData.BoldAsString = "False"
        Me.txt_SelPhNum.Appearance = Appearance14
        Me.txt_SelPhNum.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelPhNum.Location = New System.Drawing.Point(189, 86)
        Me.txt_SelPhNum.Name = "txt_SelPhNum"
        Me.txt_SelPhNum.Size = New System.Drawing.Size(176, 17)
        Me.txt_SelPhNum.TabIndex = 11
        Me.txt_SelPhNum.Text = "UltraTextEditor8"
        '
        'txt_SelPhArea
        '
        Appearance15.FontData.BoldAsString = "False"
        Me.txt_SelPhArea.Appearance = Appearance15
        Me.txt_SelPhArea.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelPhArea.Location = New System.Drawing.Point(149, 86)
        Me.txt_SelPhArea.Name = "txt_SelPhArea"
        Me.txt_SelPhArea.Size = New System.Drawing.Size(32, 17)
        Me.txt_SelPhArea.TabIndex = 10
        Me.txt_SelPhArea.Text = "UltraTextEditor7"
        '
        'txt_SelPhCOuntry
        '
        Appearance16.FontData.BoldAsString = "False"
        Me.txt_SelPhCOuntry.Appearance = Appearance16
        Me.txt_SelPhCOuntry.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelPhCOuntry.Location = New System.Drawing.Point(109, 86)
        Me.txt_SelPhCOuntry.Name = "txt_SelPhCOuntry"
        Me.txt_SelPhCOuntry.Size = New System.Drawing.Size(32, 17)
        Me.txt_SelPhCOuntry.TabIndex = 9
        Me.txt_SelPhCOuntry.Text = "UltraTextEditor6"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(37, 86)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Ph. No."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UltraTabPageControl18
        '
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel20)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_TinNum)
        Me.UltraTabPageControl18.Controls.Add(Me.Label25)
        Me.UltraTabPageControl18.Controls.Add(Me.cmb_TaxAreaID)
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel12)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_ECCRange)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_ECCDiv)
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel16)
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel3)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_PanNum)
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel4)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_CSTNum)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_ECCNum)
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel7)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_STNum)
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel11)
        Me.UltraTabPageControl18.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl18.Name = "UltraTabPageControl18"
        Me.UltraTabPageControl18.Size = New System.Drawing.Size(682, 273)
        '
        'UltraLabel20
        '
        Me.UltraLabel20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance17.TextHAlignAsString = "Right"
        Me.UltraLabel20.Appearance = Appearance17
        Me.UltraLabel20.AutoSize = True
        Me.UltraLabel20.Location = New System.Drawing.Point(59, 82)
        Me.UltraLabel20.Name = "UltraLabel20"
        Me.UltraLabel20.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel20.TabIndex = 14
        Me.UltraLabel20.Text = "TIN No."
        '
        'txt_TinNum
        '
        Me.txt_TinNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance18.TextHAlignAsString = "Right"
        Me.txt_TinNum.Appearance = Appearance18
        Me.txt_TinNum.Location = New System.Drawing.Point(110, 79)
        Me.txt_TinNum.Name = "txt_TinNum"
        Me.txt_TinNum.Size = New System.Drawing.Size(208, 21)
        Me.txt_TinNum.TabIndex = 15
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(26, 55)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(78, 14)
        Me.Label25.TabIndex = 4
        Me.Label25.Text = "Tax Area Code"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_TaxAreaID
        '
        Appearance19.FontData.BoldAsString = "False"
        Me.cmb_TaxAreaID.Appearance = Appearance19
        Me.cmb_TaxAreaID.Location = New System.Drawing.Point(109, 51)
        Me.cmb_TaxAreaID.Name = "cmb_TaxAreaID"
        Me.cmb_TaxAreaID.Size = New System.Drawing.Size(208, 22)
        Me.cmb_TaxAreaID.TabIndex = 5
        Me.cmb_TaxAreaID.Text = "UltraCombo5"
        '
        'UltraLabel12
        '
        Me.UltraLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance20.TextHAlignAsString = "Right"
        Me.UltraLabel12.Appearance = Appearance20
        Me.UltraLabel12.AutoSize = True
        Me.UltraLabel12.Location = New System.Drawing.Point(42, 185)
        Me.UltraLabel12.Name = "UltraLabel12"
        Me.UltraLabel12.Size = New System.Drawing.Size(65, 14)
        Me.UltraLabel12.TabIndex = 10
        Me.UltraLabel12.Text = "ECC Range"
        '
        'txt_ECCRange
        '
        Me.txt_ECCRange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance21.TextHAlignAsString = "Right"
        Me.txt_ECCRange.Appearance = Appearance21
        Me.txt_ECCRange.Location = New System.Drawing.Point(110, 182)
        Me.txt_ECCRange.Name = "txt_ECCRange"
        Me.txt_ECCRange.Size = New System.Drawing.Size(208, 21)
        Me.txt_ECCRange.TabIndex = 11
        '
        'txt_ECCDiv
        '
        Me.txt_ECCDiv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance22.TextHAlignAsString = "Right"
        Me.txt_ECCDiv.Appearance = Appearance22
        Me.txt_ECCDiv.Location = New System.Drawing.Point(110, 206)
        Me.txt_ECCDiv.Name = "txt_ECCDiv"
        Me.txt_ECCDiv.Size = New System.Drawing.Size(208, 21)
        Me.txt_ECCDiv.TabIndex = 13
        '
        'UltraLabel16
        '
        Me.UltraLabel16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance23.TextHAlignAsString = "Right"
        Me.UltraLabel16.Appearance = Appearance23
        Me.UltraLabel16.AutoSize = True
        Me.UltraLabel16.Location = New System.Drawing.Point(36, 209)
        Me.UltraLabel16.Name = "UltraLabel16"
        Me.UltraLabel16.Size = New System.Drawing.Size(71, 14)
        Me.UltraLabel16.TabIndex = 12
        Me.UltraLabel16.Text = "ECC Division"
        '
        'UltraLabel3
        '
        Me.UltraLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance24.TextHAlignAsString = "Right"
        Me.UltraLabel3.Appearance = Appearance24
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(58, 27)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(48, 14)
        Me.UltraLabel3.TabIndex = 0
        Me.UltraLabel3.Text = "PAN No."
        '
        'txt_PanNum
        '
        Me.txt_PanNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance25.TextHAlignAsString = "Right"
        Me.txt_PanNum.Appearance = Appearance25
        Me.txt_PanNum.Location = New System.Drawing.Point(109, 24)
        Me.txt_PanNum.Name = "txt_PanNum"
        Me.txt_PanNum.Size = New System.Drawing.Size(208, 21)
        Me.txt_PanNum.TabIndex = 1
        '
        'UltraLabel4
        '
        Me.UltraLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance26.TextHAlignAsString = "Right"
        Me.UltraLabel4.Appearance = Appearance26
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(59, 136)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(48, 14)
        Me.UltraLabel4.TabIndex = 6
        Me.UltraLabel4.Text = "CST No."
        '
        'txt_CSTNum
        '
        Me.txt_CSTNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance27.TextHAlignAsString = "Right"
        Me.txt_CSTNum.Appearance = Appearance27
        Me.txt_CSTNum.Location = New System.Drawing.Point(110, 133)
        Me.txt_CSTNum.Name = "txt_CSTNum"
        Me.txt_CSTNum.Size = New System.Drawing.Size(208, 21)
        Me.txt_CSTNum.TabIndex = 7
        '
        'txt_ECCNum
        '
        Me.txt_ECCNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance28.TextHAlignAsString = "Right"
        Me.txt_ECCNum.Appearance = Appearance28
        Me.txt_ECCNum.Location = New System.Drawing.Point(110, 157)
        Me.txt_ECCNum.Name = "txt_ECCNum"
        Me.txt_ECCNum.Size = New System.Drawing.Size(208, 21)
        Me.txt_ECCNum.TabIndex = 9
        '
        'UltraLabel7
        '
        Me.UltraLabel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance29.TextHAlignAsString = "Right"
        Me.UltraLabel7.Appearance = Appearance29
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(49, 161)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(58, 14)
        Me.UltraLabel7.TabIndex = 8
        Me.UltraLabel7.Text = "Excise No."
        '
        'txt_STNum
        '
        Me.txt_STNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance30.TextHAlignAsString = "Right"
        Me.txt_STNum.Appearance = Appearance30
        Me.txt_STNum.Location = New System.Drawing.Point(110, 106)
        Me.txt_STNum.Name = "txt_STNum"
        Me.txt_STNum.Size = New System.Drawing.Size(208, 21)
        Me.txt_STNum.TabIndex = 3
        '
        'UltraLabel11
        '
        Me.UltraLabel11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance31.TextHAlignAsString = "Right"
        Me.UltraLabel11.Appearance = Appearance31
        Me.UltraLabel11.AutoSize = True
        Me.UltraLabel11.Location = New System.Drawing.Point(32, 109)
        Me.UltraLabel11.Name = "UltraLabel11"
        Me.UltraLabel11.Size = New System.Drawing.Size(75, 14)
        Me.UltraLabel11.TabIndex = 2
        Me.UltraLabel11.Text = "Sales Tax No."
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel1)
        Me.UltraTabPageControl1.Controls.Add(Me.txt_perexcise)
        Me.UltraTabPageControl1.Controls.Add(Me.txt_perstax)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel2)
        Me.UltraTabPageControl1.Controls.Add(Me.cmb_insurance)
        Me.UltraTabPageControl1.Controls.Add(Me.cmb_transmode)
        Me.UltraTabPageControl1.Controls.Add(Me.cmb_payterms)
        Me.UltraTabPageControl1.Controls.Add(Me.cmb_shipterms)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel10)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel9)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel5)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel6)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel8)
        Me.UltraTabPageControl1.Controls.Add(Me.txt_discount)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel14)
        Me.UltraTabPageControl1.Controls.Add(Me.txt_Excise)
        Me.UltraTabPageControl1.Controls.Add(Me.txt_SalesTax)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel15)
        Me.UltraTabPageControl1.Controls.Add(Me.txt_Freight)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel13)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(682, 273)
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance32.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance32
        Me.UltraLabel1.Location = New System.Drawing.Point(369, 174)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(40, 23)
        Me.UltraLabel1.TabIndex = 16
        Me.UltraLabel1.Text = "%"
        '
        'txt_perexcise
        '
        Me.txt_perexcise.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance33.TextHAlignAsString = "Right"
        Me.txt_perexcise.Appearance = Appearance33
        Me.txt_perexcise.Location = New System.Drawing.Point(416, 174)
        Me.txt_perexcise.Name = "txt_perexcise"
        Me.txt_perexcise.Size = New System.Drawing.Size(108, 21)
        Me.txt_perexcise.TabIndex = 17
        '
        'txt_perstax
        '
        Me.txt_perstax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance34.TextHAlignAsString = "Right"
        Me.txt_perstax.Appearance = Appearance34
        Me.txt_perstax.Location = New System.Drawing.Point(416, 198)
        Me.txt_perstax.Name = "txt_perstax"
        Me.txt_perstax.Size = New System.Drawing.Size(108, 21)
        Me.txt_perstax.TabIndex = 19
        '
        'UltraLabel2
        '
        Me.UltraLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance35.TextHAlignAsString = "Right"
        Me.UltraLabel2.Appearance = Appearance35
        Me.UltraLabel2.Location = New System.Drawing.Point(369, 198)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(40, 23)
        Me.UltraLabel2.TabIndex = 18
        Me.UltraLabel2.Text = "%"
        '
        'cmb_insurance
        '
        ValueListItem1.DataValue = "Insurance by You"
        ValueListItem2.DataValue = "Insurance by Us"
        Me.cmb_insurance.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2})
        Me.cmb_insurance.Location = New System.Drawing.Point(136, 99)
        Me.cmb_insurance.Name = "cmb_insurance"
        Me.cmb_insurance.Size = New System.Drawing.Size(272, 21)
        Me.cmb_insurance.TabIndex = 7
        Me.cmb_insurance.Text = "UltraComboEditor1"
        '
        'cmb_transmode
        '
        Me.cmb_transmode.Location = New System.Drawing.Point(136, 72)
        Me.cmb_transmode.Name = "cmb_transmode"
        Me.cmb_transmode.Size = New System.Drawing.Size(272, 21)
        Me.cmb_transmode.TabIndex = 5
        Me.cmb_transmode.Text = "UltraComboEditor1"
        '
        'cmb_payterms
        '
        Me.cmb_payterms.Location = New System.Drawing.Point(136, 45)
        Me.cmb_payterms.Name = "cmb_payterms"
        Me.cmb_payterms.Size = New System.Drawing.Size(272, 21)
        Me.cmb_payterms.TabIndex = 3
        Me.cmb_payterms.Text = "UltraComboEditor1"
        '
        'cmb_shipterms
        '
        Me.cmb_shipterms.Location = New System.Drawing.Point(136, 18)
        Me.cmb_shipterms.Name = "cmb_shipterms"
        Me.cmb_shipterms.Size = New System.Drawing.Size(272, 21)
        Me.cmb_shipterms.TabIndex = 1
        Me.cmb_shipterms.Text = "UltraComboEditor1"
        '
        'UltraLabel10
        '
        Me.UltraLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance36.TextHAlignAsString = "Right"
        Me.UltraLabel10.Appearance = Appearance36
        Me.UltraLabel10.Location = New System.Drawing.Point(25, 72)
        Me.UltraLabel10.Name = "UltraLabel10"
        Me.UltraLabel10.Size = New System.Drawing.Size(104, 23)
        Me.UltraLabel10.TabIndex = 4
        Me.UltraLabel10.Text = "Mode of Transport"
        '
        'UltraLabel9
        '
        Me.UltraLabel9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance37.TextHAlignAsString = "Right"
        Me.UltraLabel9.Appearance = Appearance37
        Me.UltraLabel9.Location = New System.Drawing.Point(25, 45)
        Me.UltraLabel9.Name = "UltraLabel9"
        Me.UltraLabel9.Size = New System.Drawing.Size(104, 23)
        Me.UltraLabel9.TabIndex = 2
        Me.UltraLabel9.Text = "Terms of Payment"
        '
        'UltraLabel5
        '
        Me.UltraLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance38.TextHAlignAsString = "Right"
        Me.UltraLabel5.Appearance = Appearance38
        Me.UltraLabel5.Location = New System.Drawing.Point(41, 99)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(88, 23)
        Me.UltraLabel5.TabIndex = 6
        Me.UltraLabel5.Text = "Insurance"
        '
        'UltraLabel6
        '
        Me.UltraLabel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance39.TextHAlignAsString = "Right"
        Me.UltraLabel6.Appearance = Appearance39
        Me.UltraLabel6.Location = New System.Drawing.Point(73, 18)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(56, 23)
        Me.UltraLabel6.TabIndex = 0
        Me.UltraLabel6.Text = "Shipment"
        '
        'UltraLabel8
        '
        Me.UltraLabel8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance40.TextHAlignAsString = "Right"
        Me.UltraLabel8.Appearance = Appearance40
        Me.UltraLabel8.Location = New System.Drawing.Point(81, 126)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(48, 23)
        Me.UltraLabel8.TabIndex = 8
        Me.UltraLabel8.Text = "Discount"
        '
        'txt_discount
        '
        Me.txt_discount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance41.TextHAlignAsString = "Right"
        Me.txt_discount.Appearance = Appearance41
        Me.txt_discount.Location = New System.Drawing.Point(136, 126)
        Me.txt_discount.Name = "txt_discount"
        Me.txt_discount.Size = New System.Drawing.Size(208, 21)
        Me.txt_discount.TabIndex = 9
        '
        'UltraLabel14
        '
        Me.UltraLabel14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance42.TextHAlignAsString = "Right"
        Me.UltraLabel14.Appearance = Appearance42
        Me.UltraLabel14.Location = New System.Drawing.Point(89, 174)
        Me.UltraLabel14.Name = "UltraLabel14"
        Me.UltraLabel14.Size = New System.Drawing.Size(40, 23)
        Me.UltraLabel14.TabIndex = 12
        Me.UltraLabel14.Text = "Excise"
        '
        'txt_Excise
        '
        Me.txt_Excise.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance43.TextHAlignAsString = "Right"
        Me.txt_Excise.Appearance = Appearance43
        Me.txt_Excise.Location = New System.Drawing.Point(136, 174)
        Me.txt_Excise.Name = "txt_Excise"
        Me.txt_Excise.Size = New System.Drawing.Size(208, 21)
        Me.txt_Excise.TabIndex = 13
        '
        'txt_SalesTax
        '
        Me.txt_SalesTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance44.TextHAlignAsString = "Right"
        Me.txt_SalesTax.Appearance = Appearance44
        Me.txt_SalesTax.Location = New System.Drawing.Point(136, 198)
        Me.txt_SalesTax.Name = "txt_SalesTax"
        Me.txt_SalesTax.Size = New System.Drawing.Size(208, 21)
        Me.txt_SalesTax.TabIndex = 15
        '
        'UltraLabel15
        '
        Me.UltraLabel15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance45.TextHAlignAsString = "Right"
        Me.UltraLabel15.Appearance = Appearance45
        Me.UltraLabel15.Location = New System.Drawing.Point(73, 198)
        Me.UltraLabel15.Name = "UltraLabel15"
        Me.UltraLabel15.Size = New System.Drawing.Size(56, 23)
        Me.UltraLabel15.TabIndex = 14
        Me.UltraLabel15.Text = "Sales Tax"
        '
        'txt_Freight
        '
        Me.txt_Freight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance46.TextHAlignAsString = "Right"
        Me.txt_Freight.Appearance = Appearance46
        Me.txt_Freight.Location = New System.Drawing.Point(136, 150)
        Me.txt_Freight.Name = "txt_Freight"
        Me.txt_Freight.Size = New System.Drawing.Size(208, 21)
        Me.txt_Freight.TabIndex = 11
        '
        'UltraLabel13
        '
        Me.UltraLabel13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance47.TextHAlignAsString = "Right"
        Me.UltraLabel13.Appearance = Appearance47
        Me.UltraLabel13.Location = New System.Drawing.Point(81, 150)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(48, 23)
        Me.UltraLabel13.TabIndex = 10
        Me.UltraLabel13.Text = "Freight"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 457)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(686, 45)
        Me.Panel4.TabIndex = 3
        '
        'btnCancel
        '
        Appearance48.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance48
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(510, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 45)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance49.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance49
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(598, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 45)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(39, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Division"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Division
        '
        Appearance50.FontData.BoldAsString = "False"
        Me.txt_Division.Appearance = Appearance50
        Me.txt_Division.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Division.Location = New System.Drawing.Point(111, 17)
        Me.txt_Division.Name = "txt_Division"
        Me.txt_Division.Size = New System.Drawing.Size(192, 17)
        Me.txt_Division.TabIndex = 1
        Me.txt_Division.Text = "UltraTextEditor1"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(39, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "City"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelCity
        '
        Appearance51.FontData.BoldAsString = "False"
        Me.txt_SelCity.Appearance = Appearance51
        Me.txt_SelCity.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelCity.Location = New System.Drawing.Point(111, 46)
        Me.txt_SelCity.Name = "txt_SelCity"
        Me.txt_SelCity.Size = New System.Drawing.Size(192, 17)
        Me.txt_SelCity.TabIndex = 3
        Me.txt_SelCity.Text = "UltraTextEditor3"
        '
        'btnCopyAdd
        '
        Me.btnCopyAdd.Location = New System.Drawing.Point(322, 17)
        Me.btnCopyAdd.Name = "btnCopyAdd"
        Me.btnCopyAdd.Size = New System.Drawing.Size(108, 50)
        Me.btnCopyAdd.TabIndex = 4
        Me.btnCopyAdd.Text = "Copy Address from Main Party"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txt_Division)
        Me.Panel1.Controls.Add(Me.txt_SelCity)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.btnCopyAdd)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 71)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(686, 87)
        Me.Panel1.TabIndex = 1
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl17)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl18)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 158)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage3
        Me.UltraTabControl1.Size = New System.Drawing.Size(686, 299)
        Me.UltraTabControl1.TabIndex = 0
        UltraTab7.TabPage = Me.UltraTabPageControl17
        UltraTab7.Text = "Address"
        UltraTab2.TabPage = Me.UltraTabPageControl18
        UltraTab2.Text = "Statutory"
        UltraTab6.Key = "comm"
        UltraTab6.TabPage = Me.UltraTabPageControl1
        UltraTab6.Text = "Commercial"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab7, UltraTab2, UltraTab6})
        '
        'UltraTabSharedControlsPage3
        '
        Me.UltraTabSharedControlsPage3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage3.Name = "UltraTabSharedControlsPage3"
        Me.UltraTabSharedControlsPage3.Size = New System.Drawing.Size(682, 273)
        '
        'UltraPanelSub
        '
        '
        'UltraPanelSub.ClientArea
        '
        Me.UltraPanelSub.ClientArea.Controls.Add(Me.Label12)
        Me.UltraPanelSub.ClientArea.Controls.Add(Me.cmb_GstRegType)
        Me.UltraPanelSub.ClientArea.Controls.Add(Me.txt_GSTIN)
        Me.UltraPanelSub.ClientArea.Controls.Add(Me.Label26)
        Me.UltraPanelSub.ClientArea.Controls.Add(Me.txt_Code)
        Me.UltraPanelSub.ClientArea.Controls.Add(Me.lblNum)
        Me.UltraPanelSub.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanelSub.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanelSub.Name = "UltraPanelSub"
        Me.UltraPanelSub.Size = New System.Drawing.Size(686, 71)
        Me.UltraPanelSub.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(355, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 16)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "GSTReg Type"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_GstRegType
        '
        Appearance52.FontData.BoldAsString = "False"
        Me.cmb_GstRegType.Appearance = Appearance52
        Me.cmb_GstRegType.Location = New System.Drawing.Point(443, 13)
        Me.cmb_GstRegType.Name = "cmb_GstRegType"
        Me.cmb_GstRegType.Size = New System.Drawing.Size(179, 22)
        Me.cmb_GstRegType.TabIndex = 25
        Me.cmb_GstRegType.Text = "UltraCombo5"
        '
        'txt_GSTIN
        '
        Me.txt_GSTIN.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance53.FontData.BoldAsString = "True"
        Appearance53.FontData.SizeInPoints = 11.0!
        Me.txt_GSTIN.Appearance = Appearance53
        Me.txt_GSTIN.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_GSTIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_GSTIN.Location = New System.Drawing.Point(111, 43)
        Me.txt_GSTIN.Name = "txt_GSTIN"
        Me.txt_GSTIN.Size = New System.Drawing.Size(224, 22)
        Me.txt_GSTIN.TabIndex = 20
        Me.txt_GSTIN.Text = "ULTRATEXTEDITOR2"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(52, 47)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(56, 13)
        Me.Label26.TabIndex = 19
        Me.Label26.Text = "GST No."
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Code
        '
        Appearance54.FontData.BoldAsString = "True"
        Appearance54.FontData.SizeInPoints = 11.0!
        Me.txt_Code.Appearance = Appearance54
        Me.txt_Code.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Code.Location = New System.Drawing.Point(111, 12)
        Me.txt_Code.Name = "txt_Code"
        Me.txt_Code.ReadOnly = True
        Me.txt_Code.Size = New System.Drawing.Size(224, 22)
        Me.txt_Code.TabIndex = 1
        Me.txt_Code.Text = "UltraTextEditor2"
        '
        'lblNum
        '
        Me.lblNum.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNum.Location = New System.Drawing.Point(47, 12)
        Me.lblNum.Name = "lblNum"
        Me.lblNum.Size = New System.Drawing.Size(56, 16)
        Me.lblNum.TabIndex = 0
        Me.lblNum.Text = "Code"
        Me.lblNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelEMailCc
        '
        Appearance2.FontData.BoldAsString = "False"
        Me.txt_SelEMailCc.Appearance = Appearance2
        Me.txt_SelEMailCc.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelEMailCc.Location = New System.Drawing.Point(331, 203)
        Me.txt_SelEMailCc.Name = "txt_SelEMailCc"
        Me.txt_SelEMailCc.Size = New System.Drawing.Size(256, 17)
        Me.txt_SelEMailCc.TabIndex = 45
        Me.txt_SelEMailCc.Text = "UltraTextEditor12"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(269, 203)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 17)
        Me.Label13.TabIndex = 44
        Me.Label13.Text = "Email CC"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelEMailBcc
        '
        Appearance1.FontData.BoldAsString = "False"
        Me.txt_SelEMailBcc.Appearance = Appearance1
        Me.txt_SelEMailBcc.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelEMailBcc.Location = New System.Drawing.Point(330, 230)
        Me.txt_SelEMailBcc.Name = "txt_SelEMailBcc"
        Me.txt_SelEMailBcc.Size = New System.Drawing.Size(256, 17)
        Me.txt_SelEMailBcc.TabIndex = 47
        Me.txt_SelEMailBcc.Text = "UltraTextEditor12"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(253, 230)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 17)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "Email BCC"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmGstPartySelect
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Selectable Party"
        Me.ClientSize = New System.Drawing.Size(686, 502)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.UltraPanelSub)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGstPartySelect"
        Me.Text = "Selectable Party"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl17.ResumeLayout(False)
        Me.UltraTabPageControl17.PerformLayout()
        CType(Me.cmb_ITCInElg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ContactPersonName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_SelCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelPinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_SelState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelWeb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_sameashead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelFaxArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelFaxCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelFaxNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelPhNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelPhArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelPhCOuntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl18.ResumeLayout(False)
        Me.UltraTabPageControl18.PerformLayout()
        CType(Me.txt_TinNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_TaxAreaID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ECCRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ECCDiv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PanNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CSTNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ECCNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_STNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        CType(Me.txt_perexcise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_perstax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_insurance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_transmode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_payterms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_shipterms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_discount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Excise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SalesTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Freight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.txt_Division, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelCity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.UltraPanelSub.ClientArea.ResumeLayout(False)
        Me.UltraPanelSub.ClientArea.PerformLayout()
        Me.UltraPanelSub.ResumeLayout(False)
        CType(Me.cmb_GstRegType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_GSTIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelEMailCc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelEMailBcc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage3 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl17 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl18 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents cmb_insurance As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents cmb_transmode As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents cmb_payterms As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents cmb_shipterms As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents UltraLabel10 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel9 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_discount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel14 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_Excise As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_SalesTax As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel15 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_Freight As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_perexcise As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_perstax As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_ECCRange As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_ECCDiv As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel16 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_PanNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CSTNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_ECCNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_STNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel11 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraPanelSub As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents txt_Code As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblNum As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmb_SelCountry As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label25 As Label
    Friend WithEvents cmb_TaxAreaID As UltraCombo
    Friend WithEvents UltraLabel20 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_TinNum As UltraTextEditor
    Friend WithEvents txt_GSTIN As UltraTextEditor
    Friend WithEvents Label26 As Label
    Friend WithEvents txt_ContactPersonName As UltraTextEditor
    Friend WithEvents Label9 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents cmb_GstRegType As UltraCombo
    Friend WithEvents cmb_ITCInElg As UltraComboEditor
    Friend WithEvents LabelIneligible As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_SelEMailBcc As UltraTextEditor
    Friend WithEvents Label14 As Label
    Friend WithEvents txt_SelEMailCc As UltraTextEditor
    Friend WithEvents Label13 As Label

#End Region
End Class

