Imports System.Windows.Forms
Imports Infragistics.Win.UltraWinEditors
Imports Infragistics.Win.UltraWinGrid

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmGstCampus
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
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance39 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance40 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance41 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab7 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl17 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.btnCopyAdd = New Infragistics.Win.Misc.UltraButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmb_SelCountry = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmb_SelState = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_SelAddress = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_SelPinCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chk_sameashead = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_SelFaxArea = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelFaxCountry = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelFaxNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelPhNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelPhArea = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelPhCOuntry = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl18 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_PanNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmb_TaxAreaID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel20 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_TinNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_FactoryLicenseNumber = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txt_InvoiceNote = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmb_DivisionCodeList = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txt_PONote = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.chk_AcceptsEnq = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_AcceptsOrder = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.txt_EmailPur = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_EmailSls = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmb_GSTRegID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_Division = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_campuscode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_DispName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_CampusType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_TCName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_SelCity = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage3 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl17.SuspendLayout()
        CType(Me.cmb_SelCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_SelState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelPinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_sameashead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelFaxArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelFaxCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelFaxNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelPhNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelPhArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelPhCOuntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl18.SuspendLayout()
        CType(Me.txt_PanNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_TaxAreaID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_TinNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_FactoryLicenseNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.txt_InvoiceNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_DivisionCodeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PONote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_AcceptsEnq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_AcceptsOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_EmailPur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_EmailSls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmb_GSTRegID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Division, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_campuscode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_DispName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CampusType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_TCName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SelCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl17
        '
        Me.UltraTabPageControl17.Controls.Add(Me.btnCopyAdd)
        Me.UltraTabPageControl17.Controls.Add(Me.Label10)
        Me.UltraTabPageControl17.Controls.Add(Me.cmb_SelCountry)
        Me.UltraTabPageControl17.Controls.Add(Me.Label11)
        Me.UltraTabPageControl17.Controls.Add(Me.cmb_SelState)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelAddress)
        Me.UltraTabPageControl17.Controls.Add(Me.Label2)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelPinCode)
        Me.UltraTabPageControl17.Controls.Add(Me.Label5)
        Me.UltraTabPageControl17.Controls.Add(Me.chk_sameashead)
        Me.UltraTabPageControl17.Controls.Add(Me.Label7)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelFaxArea)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelFaxCountry)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelFaxNum)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelPhNum)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelPhArea)
        Me.UltraTabPageControl17.Controls.Add(Me.txt_SelPhCOuntry)
        Me.UltraTabPageControl17.Controls.Add(Me.Label6)
        Me.UltraTabPageControl17.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl17.Name = "UltraTabPageControl17"
        Me.UltraTabPageControl17.Size = New System.Drawing.Size(682, 288)
        '
        'btnCopyAdd
        '
        Me.btnCopyAdd.Location = New System.Drawing.Point(128, 154)
        Me.btnCopyAdd.Name = "btnCopyAdd"
        Me.btnCopyAdd.Size = New System.Drawing.Size(124, 39)
        Me.btnCopyAdd.TabIndex = 19
        Me.btnCopyAdd.Text = "Copy Address from Main Party"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(71, 88)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 14)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "* Country"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_SelCountry
        '
        Appearance1.FontData.BoldAsString = "False"
        Me.cmb_SelCountry.Appearance = Appearance1
        Me.cmb_SelCountry.Location = New System.Drawing.Point(126, 85)
        Me.cmb_SelCountry.Name = "cmb_SelCountry"
        Me.cmb_SelCountry.Size = New System.Drawing.Size(256, 22)
        Me.cmb_SelCountry.TabIndex = 13
        Me.cmb_SelCountry.Text = "UltraCombo5"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(84, 118)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 14)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "* State"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_SelState
        '
        Appearance2.FontData.BoldAsString = "False"
        Me.cmb_SelState.Appearance = Appearance2
        Me.cmb_SelState.Location = New System.Drawing.Point(126, 114)
        Me.cmb_SelState.Name = "cmb_SelState"
        Me.cmb_SelState.Size = New System.Drawing.Size(256, 22)
        Me.cmb_SelState.TabIndex = 15
        Me.cmb_SelState.Text = "UltraCombo5"
        '
        'txt_SelAddress
        '
        Me.txt_SelAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.FontData.BoldAsString = "False"
        Me.txt_SelAddress.Appearance = Appearance3
        Me.txt_SelAddress.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelAddress.Location = New System.Drawing.Point(126, 14)
        Me.txt_SelAddress.Name = "txt_SelAddress"
        Me.txt_SelAddress.Size = New System.Drawing.Size(540, 17)
        Me.txt_SelAddress.TabIndex = 1
        Me.txt_SelAddress.Text = "UltraTextEditor2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(68, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "* Address"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelPinCode
        '
        Appearance4.FontData.BoldAsString = "False"
        Me.txt_SelPinCode.Appearance = Appearance4
        Me.txt_SelPinCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelPinCode.Location = New System.Drawing.Point(126, 37)
        Me.txt_SelPinCode.Name = "txt_SelPinCode"
        Me.txt_SelPinCode.Size = New System.Drawing.Size(120, 17)
        Me.txt_SelPinCode.TabIndex = 3
        Me.txt_SelPinCode.Text = "UltraTextEditor5"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(78, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 14)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Pincode"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chk_sameashead
        '
        Me.chk_sameashead.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_sameashead.Location = New System.Drawing.Point(436, 110)
        Me.chk_sameashead.Name = "chk_sameashead"
        Me.chk_sameashead.Size = New System.Drawing.Size(192, 24)
        Me.chk_sameashead.TabIndex = 16
        Me.chk_sameashead.Text = "Address is Same as Head Office"
        Me.chk_sameashead.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(395, 63)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 14)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Fax No."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_SelFaxArea
        '
        Appearance5.FontData.BoldAsString = "False"
        Me.txt_SelFaxArea.Appearance = Appearance5
        Me.txt_SelFaxArea.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelFaxArea.Location = New System.Drawing.Point(482, 62)
        Me.txt_SelFaxArea.Name = "txt_SelFaxArea"
        Me.txt_SelFaxArea.Size = New System.Drawing.Size(32, 17)
        Me.txt_SelFaxArea.TabIndex = 10
        Me.txt_SelFaxArea.Text = "UltraTextEditor10"
        '
        'txt_SelFaxCountry
        '
        Appearance6.FontData.BoldAsString = "False"
        Me.txt_SelFaxCountry.Appearance = Appearance6
        Me.txt_SelFaxCountry.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelFaxCountry.Location = New System.Drawing.Point(442, 62)
        Me.txt_SelFaxCountry.Name = "txt_SelFaxCountry"
        Me.txt_SelFaxCountry.Size = New System.Drawing.Size(32, 17)
        Me.txt_SelFaxCountry.TabIndex = 9
        Me.txt_SelFaxCountry.Text = "UltraTextEditor11"
        '
        'txt_SelFaxNum
        '
        Appearance7.FontData.BoldAsString = "False"
        Me.txt_SelFaxNum.Appearance = Appearance7
        Me.txt_SelFaxNum.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelFaxNum.Location = New System.Drawing.Point(522, 62)
        Me.txt_SelFaxNum.Name = "txt_SelFaxNum"
        Me.txt_SelFaxNum.Size = New System.Drawing.Size(144, 17)
        Me.txt_SelFaxNum.TabIndex = 11
        Me.txt_SelFaxNum.Text = "UltraTextEditor9"
        '
        'txt_SelPhNum
        '
        Appearance8.FontData.BoldAsString = "False"
        Me.txt_SelPhNum.Appearance = Appearance8
        Me.txt_SelPhNum.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelPhNum.Location = New System.Drawing.Point(206, 61)
        Me.txt_SelPhNum.Name = "txt_SelPhNum"
        Me.txt_SelPhNum.Size = New System.Drawing.Size(176, 17)
        Me.txt_SelPhNum.TabIndex = 7
        Me.txt_SelPhNum.Text = "UltraTextEditor8"
        '
        'txt_SelPhArea
        '
        Appearance9.FontData.BoldAsString = "False"
        Me.txt_SelPhArea.Appearance = Appearance9
        Me.txt_SelPhArea.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelPhArea.Location = New System.Drawing.Point(166, 61)
        Me.txt_SelPhArea.Name = "txt_SelPhArea"
        Me.txt_SelPhArea.Size = New System.Drawing.Size(32, 17)
        Me.txt_SelPhArea.TabIndex = 6
        Me.txt_SelPhArea.Text = "UltraTextEditor7"
        '
        'txt_SelPhCOuntry
        '
        Appearance10.FontData.BoldAsString = "False"
        Me.txt_SelPhCOuntry.Appearance = Appearance10
        Me.txt_SelPhCOuntry.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelPhCOuntry.Location = New System.Drawing.Point(126, 61)
        Me.txt_SelPhCOuntry.Name = "txt_SelPhCOuntry"
        Me.txt_SelPhCOuntry.Size = New System.Drawing.Size(32, 17)
        Me.txt_SelPhCOuntry.TabIndex = 5
        Me.txt_SelPhCOuntry.Text = "UltraTextEditor6"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(82, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 14)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Ph. No."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UltraTabPageControl18
        '
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel3)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_PanNum)
        Me.UltraTabPageControl18.Controls.Add(Me.Label25)
        Me.UltraTabPageControl18.Controls.Add(Me.cmb_TaxAreaID)
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel20)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_TinNum)
        Me.UltraTabPageControl18.Controls.Add(Me.txt_FactoryLicenseNumber)
        Me.UltraTabPageControl18.Controls.Add(Me.UltraLabel1)
        Me.UltraTabPageControl18.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl18.Name = "UltraTabPageControl18"
        Me.UltraTabPageControl18.Size = New System.Drawing.Size(682, 288)
        '
        'UltraLabel3
        '
        Me.UltraLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance11.TextHAlignAsString = "Right"
        Me.UltraLabel3.Appearance = Appearance11
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(73, 22)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(48, 14)
        Me.UltraLabel3.TabIndex = 20
        Me.UltraLabel3.Text = "PAN No."
        '
        'txt_PanNum
        '
        Me.txt_PanNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance12.TextHAlignAsString = "Right"
        Me.txt_PanNum.Appearance = Appearance12
        Me.txt_PanNum.Location = New System.Drawing.Point(126, 19)
        Me.txt_PanNum.Name = "txt_PanNum"
        Me.txt_PanNum.Size = New System.Drawing.Size(208, 21)
        Me.txt_PanNum.TabIndex = 21
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(44, 84)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(78, 14)
        Me.Label25.TabIndex = 18
        Me.Label25.Text = "Tax Area Code"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_TaxAreaID
        '
        Appearance13.FontData.BoldAsString = "False"
        Me.cmb_TaxAreaID.Appearance = Appearance13
        Me.cmb_TaxAreaID.Location = New System.Drawing.Point(127, 80)
        Me.cmb_TaxAreaID.Name = "cmb_TaxAreaID"
        Me.cmb_TaxAreaID.ReadOnly = True
        Me.cmb_TaxAreaID.Size = New System.Drawing.Size(208, 22)
        Me.cmb_TaxAreaID.TabIndex = 19
        Me.cmb_TaxAreaID.Text = "UltraCombo5"
        '
        'UltraLabel20
        '
        Appearance14.TextHAlignAsString = "Right"
        Me.UltraLabel20.Appearance = Appearance14
        Me.UltraLabel20.AutoSize = True
        Me.UltraLabel20.Location = New System.Drawing.Point(80, 52)
        Me.UltraLabel20.Name = "UltraLabel20"
        Me.UltraLabel20.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel20.TabIndex = 16
        Me.UltraLabel20.Text = "TIN No."
        '
        'txt_TinNum
        '
        Appearance15.TextHAlignAsString = "Right"
        Me.txt_TinNum.Appearance = Appearance15
        Me.txt_TinNum.Location = New System.Drawing.Point(126, 49)
        Me.txt_TinNum.Name = "txt_TinNum"
        Me.txt_TinNum.Size = New System.Drawing.Size(207, 21)
        Me.txt_TinNum.TabIndex = 17
        '
        'txt_FactoryLicenseNumber
        '
        Appearance16.TextHAlignAsString = "Right"
        Me.txt_FactoryLicenseNumber.Appearance = Appearance16
        Me.txt_FactoryLicenseNumber.Location = New System.Drawing.Point(126, 112)
        Me.txt_FactoryLicenseNumber.Name = "txt_FactoryLicenseNumber"
        Me.txt_FactoryLicenseNumber.Size = New System.Drawing.Size(207, 21)
        Me.txt_FactoryLicenseNumber.TabIndex = 15
        '
        'UltraLabel1
        '
        Appearance17.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance17
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(18, 115)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(105, 14)
        Me.UltraLabel1.TabIndex = 14
        Me.UltraLabel1.Text = "Factory License No."
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.Label19)
        Me.UltraTabPageControl3.Controls.Add(Me.txt_InvoiceNote)
        Me.UltraTabPageControl3.Controls.Add(Me.cmb_DivisionCodeList)
        Me.UltraTabPageControl3.Controls.Add(Me.Label4)
        Me.UltraTabPageControl3.Controls.Add(Me.Label17)
        Me.UltraTabPageControl3.Controls.Add(Me.txt_PONote)
        Me.UltraTabPageControl3.Controls.Add(Me.chk_AcceptsEnq)
        Me.UltraTabPageControl3.Controls.Add(Me.chk_AcceptsOrder)
        Me.UltraTabPageControl3.Controls.Add(Me.txt_EmailPur)
        Me.UltraTabPageControl3.Controls.Add(Me.Label14)
        Me.UltraTabPageControl3.Controls.Add(Me.txt_EmailSls)
        Me.UltraTabPageControl3.Controls.Add(Me.Label13)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(682, 288)
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label19.Location = New System.Drawing.Point(22, 213)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 40)
        Me.Label19.TabIndex = 15
        Me.Label19.Text = "Invoice Note"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_InvoiceNote
        '
        Me.txt_InvoiceNote.AcceptsReturn = True
        Me.txt_InvoiceNote.Location = New System.Drawing.Point(126, 213)
        Me.txt_InvoiceNote.Multiline = True
        Me.txt_InvoiceNote.Name = "txt_InvoiceNote"
        Me.txt_InvoiceNote.Size = New System.Drawing.Size(384, 48)
        Me.txt_InvoiceNote.TabIndex = 16
        Me.txt_InvoiceNote.Text = "UltraTextEditor3"
        '
        'cmb_DivisionCodeList
        '
        Appearance18.BackColor = System.Drawing.SystemColors.Window
        Appearance18.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmb_DivisionCodeList.DisplayLayout.Appearance = Appearance18
        Me.cmb_DivisionCodeList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmb_DivisionCodeList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance19.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance19.BorderColor = System.Drawing.SystemColors.Window
        Me.cmb_DivisionCodeList.DisplayLayout.GroupByBox.Appearance = Appearance19
        Appearance20.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmb_DivisionCodeList.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance20
        Me.cmb_DivisionCodeList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance21.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance21.BackColor2 = System.Drawing.SystemColors.Control
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmb_DivisionCodeList.DisplayLayout.GroupByBox.PromptAppearance = Appearance21
        Me.cmb_DivisionCodeList.DisplayLayout.MaxColScrollRegions = 1
        Me.cmb_DivisionCodeList.DisplayLayout.MaxRowScrollRegions = 1
        Appearance22.BackColor = System.Drawing.SystemColors.Window
        Appearance22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmb_DivisionCodeList.DisplayLayout.Override.ActiveCellAppearance = Appearance22
        Appearance23.BackColor = System.Drawing.SystemColors.Highlight
        Appearance23.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmb_DivisionCodeList.DisplayLayout.Override.ActiveRowAppearance = Appearance23
        Me.cmb_DivisionCodeList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmb_DivisionCodeList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance24.BackColor = System.Drawing.SystemColors.Window
        Me.cmb_DivisionCodeList.DisplayLayout.Override.CardAreaAppearance = Appearance24
        Appearance25.BorderColor = System.Drawing.Color.Silver
        Appearance25.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmb_DivisionCodeList.DisplayLayout.Override.CellAppearance = Appearance25
        Me.cmb_DivisionCodeList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmb_DivisionCodeList.DisplayLayout.Override.CellPadding = 0
        Appearance26.BackColor = System.Drawing.SystemColors.Control
        Appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance26.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance26.BorderColor = System.Drawing.SystemColors.Window
        Me.cmb_DivisionCodeList.DisplayLayout.Override.GroupByRowAppearance = Appearance26
        Appearance27.TextHAlignAsString = "Left"
        Me.cmb_DivisionCodeList.DisplayLayout.Override.HeaderAppearance = Appearance27
        Me.cmb_DivisionCodeList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmb_DivisionCodeList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance28.BackColor = System.Drawing.SystemColors.Window
        Appearance28.BorderColor = System.Drawing.Color.Silver
        Me.cmb_DivisionCodeList.DisplayLayout.Override.RowAppearance = Appearance28
        Me.cmb_DivisionCodeList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance29.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmb_DivisionCodeList.DisplayLayout.Override.TemplateAddRowAppearance = Appearance29
        Me.cmb_DivisionCodeList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmb_DivisionCodeList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmb_DivisionCodeList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmb_DivisionCodeList.Location = New System.Drawing.Point(126, 16)
        Me.cmb_DivisionCodeList.Name = "cmb_DivisionCodeList"
        Me.cmb_DivisionCodeList.Size = New System.Drawing.Size(264, 22)
        Me.cmb_DivisionCodeList.TabIndex = 12
        Me.cmb_DivisionCodeList.Text = "UltraCombo1"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label4.Location = New System.Drawing.Point(12, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Divicion Code List"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label17.Location = New System.Drawing.Point(28, 158)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(96, 40)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "PO Note"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_PONote
        '
        Me.txt_PONote.AcceptsReturn = True
        Me.txt_PONote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PONote.Location = New System.Drawing.Point(126, 158)
        Me.txt_PONote.Multiline = True
        Me.txt_PONote.Name = "txt_PONote"
        Me.txt_PONote.Size = New System.Drawing.Size(540, 48)
        Me.txt_PONote.TabIndex = 9
        Me.txt_PONote.Text = "UltraTextEditor3"
        '
        'chk_AcceptsEnq
        '
        Me.chk_AcceptsEnq.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_AcceptsEnq.Location = New System.Drawing.Point(126, 100)
        Me.chk_AcceptsEnq.Name = "chk_AcceptsEnq"
        Me.chk_AcceptsEnq.Size = New System.Drawing.Size(200, 25)
        Me.chk_AcceptsEnq.TabIndex = 5
        Me.chk_AcceptsEnq.Text = "Accepts Sales Enquiry"
        '
        'chk_AcceptsOrder
        '
        Me.chk_AcceptsOrder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_AcceptsOrder.Location = New System.Drawing.Point(126, 69)
        Me.chk_AcceptsOrder.Name = "chk_AcceptsOrder"
        Me.chk_AcceptsOrder.Size = New System.Drawing.Size(200, 25)
        Me.chk_AcceptsOrder.TabIndex = 4
        Me.chk_AcceptsOrder.Text = "Accepts Sales Order"
        '
        'txt_EmailPur
        '
        Appearance30.FontData.BoldAsString = "False"
        Me.txt_EmailPur.Appearance = Appearance30
        Me.txt_EmailPur.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_EmailPur.Location = New System.Drawing.Point(126, 133)
        Me.txt_EmailPur.Name = "txt_EmailPur"
        Me.txt_EmailPur.Size = New System.Drawing.Size(264, 17)
        Me.txt_EmailPur.TabIndex = 7
        Me.txt_EmailPur.Text = "UltraTextEditor12"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(37, 133)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(87, 16)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Email Purchase"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_EmailSls
        '
        Appearance31.FontData.BoldAsString = "False"
        Me.txt_EmailSls.Appearance = Appearance31
        Me.txt_EmailSls.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_EmailSls.Location = New System.Drawing.Point(126, 46)
        Me.txt_EmailSls.Name = "txt_EmailSls"
        Me.txt_EmailSls.Size = New System.Drawing.Size(264, 17)
        Me.txt_EmailSls.TabIndex = 3
        Me.txt_EmailSls.Text = "UltraTextEditor12"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(37, 46)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(87, 16)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Email Sales"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 463)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(686, 39)
        Me.Panel4.TabIndex = 2
        '
        'btnSave
        '
        Appearance32.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance32
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(422, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 39)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance33.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance33
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(510, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 39)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance34.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance34
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(598, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 39)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.cmb_GSTRegID)
        Me.Panel1.Controls.Add(Me.txt_Division)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.txt_campuscode)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txt_DispName)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmb_CampusType)
        Me.Panel1.Controls.Add(Me.txt_TCName)
        Me.Panel1.Controls.Add(Me.txt_SelCity)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(686, 149)
        Me.Panel1.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(59, 85)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(46, 14)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "* GSTIN"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_GSTRegID
        '
        Appearance35.FontData.BoldAsString = "False"
        Me.cmb_GSTRegID.Appearance = Appearance35
        Me.cmb_GSTRegID.Location = New System.Drawing.Point(111, 81)
        Me.cmb_GSTRegID.Name = "cmb_GSTRegID"
        Me.cmb_GSTRegID.Size = New System.Drawing.Size(256, 22)
        Me.cmb_GSTRegID.TabIndex = 33
        Me.cmb_GSTRegID.Text = "UltraCombo5"
        '
        'txt_Division
        '
        Appearance36.FontData.BoldAsString = "False"
        Me.txt_Division.Appearance = Appearance36
        Me.txt_Division.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Division.Location = New System.Drawing.Point(407, 109)
        Me.txt_Division.Name = "txt_Division"
        Me.txt_Division.Size = New System.Drawing.Size(192, 17)
        Me.txt_Division.TabIndex = 31
        Me.txt_Division.Text = "UltraTextEditor1"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(335, 109)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 16)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Branch"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_campuscode
        '
        Appearance37.FontData.BoldAsString = "False"
        Me.txt_campuscode.Appearance = Appearance37
        Me.txt_campuscode.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_campuscode.Location = New System.Drawing.Point(407, 57)
        Me.txt_campuscode.Name = "txt_campuscode"
        Me.txt_campuscode.Size = New System.Drawing.Size(192, 17)
        Me.txt_campuscode.TabIndex = 29
        Me.txt_campuscode.Text = "UltraTextEditor1"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(329, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 14)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Campus Code"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_DispName
        '
        Appearance38.FontData.BoldAsString = "False"
        Me.txt_DispName.Appearance = Appearance38
        Me.txt_DispName.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_DispName.Location = New System.Drawing.Point(111, 31)
        Me.txt_DispName.Name = "txt_DispName"
        Me.txt_DispName.Size = New System.Drawing.Size(192, 17)
        Me.txt_DispName.TabIndex = 23
        Me.txt_DispName.Text = "UltraTextEditor1"
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(13, 31)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 16)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Display Name"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(309, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Type"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_CampusType
        '
        Appearance39.FontData.BoldAsString = "False"
        Me.cmb_CampusType.Appearance = Appearance39
        Me.cmb_CampusType.Location = New System.Drawing.Point(407, 29)
        Me.cmb_CampusType.Name = "cmb_CampusType"
        Me.cmb_CampusType.Size = New System.Drawing.Size(192, 22)
        Me.cmb_CampusType.TabIndex = 21
        Me.cmb_CampusType.Text = "UltraCombo5"
        '
        'txt_TCName
        '
        Appearance40.FontData.BoldAsString = "False"
        Me.txt_TCName.Appearance = Appearance40
        Me.txt_TCName.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_TCName.Location = New System.Drawing.Point(111, 58)
        Me.txt_TCName.Name = "txt_TCName"
        Me.txt_TCName.Size = New System.Drawing.Size(192, 17)
        Me.txt_TCName.TabIndex = 25
        Me.txt_TCName.Text = "UltraTextEditor1"
        '
        'txt_SelCity
        '
        Appearance41.FontData.BoldAsString = "False"
        Me.txt_SelCity.Appearance = Appearance41
        Me.txt_SelCity.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_SelCity.Location = New System.Drawing.Point(111, 109)
        Me.txt_SelCity.Name = "txt_SelCity"
        Me.txt_SelCity.Size = New System.Drawing.Size(192, 17)
        Me.txt_SelCity.TabIndex = 27
        Me.txt_SelCity.Text = "UltraTextEditor3"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(39, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "City"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(39, 58)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 16)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "Print Name"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl17)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl18)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 149)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage3
        Me.UltraTabControl1.Size = New System.Drawing.Size(686, 314)
        Me.UltraTabControl1.TabIndex = 1
        UltraTab7.TabPage = Me.UltraTabPageControl17
        UltraTab7.Text = "Address"
        UltraTab2.TabPage = Me.UltraTabPageControl18
        UltraTab2.Text = "Statutory"
        UltraTab4.Key = "config"
        UltraTab4.TabPage = Me.UltraTabPageControl3
        UltraTab4.Text = "Config"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab7, UltraTab2, UltraTab4})
        '
        'UltraTabSharedControlsPage3
        '
        Me.UltraTabSharedControlsPage3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage3.Name = "UltraTabSharedControlsPage3"
        Me.UltraTabSharedControlsPage3.Size = New System.Drawing.Size(682, 288)
        '
        'frmCampus
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Campus"
        Me.ClientSize = New System.Drawing.Size(686, 502)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCampus"
        Me.Text = "Campus"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl17.ResumeLayout(False)
        Me.UltraTabPageControl17.PerformLayout()
        CType(Me.cmb_SelCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_SelState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelPinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_sameashead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelFaxArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelFaxCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelFaxNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelPhNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelPhArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelPhCOuntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl18.ResumeLayout(False)
        Me.UltraTabPageControl18.PerformLayout()
        CType(Me.txt_PanNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_TaxAreaID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_TinNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_FactoryLicenseNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        Me.UltraTabPageControl3.PerformLayout()
        CType(Me.txt_InvoiceNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_DivisionCodeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PONote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_AcceptsEnq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_AcceptsOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_EmailPur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_EmailSls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmb_GSTRegID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Division, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_campuscode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_DispName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CampusType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_TCName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SelCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage3 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl18 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents txt_FactoryLicenseNumber As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txt_PONote As UltraTextEditor
    Friend WithEvents chk_AcceptsEnq As UltraCheckEditor
    Friend WithEvents chk_AcceptsOrder As UltraCheckEditor
    Friend WithEvents txt_EmailPur As UltraTextEditor
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txt_EmailSls As UltraTextEditor
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label4 As Label
    Friend WithEvents cmb_DivisionCodeList As UltraCombo
    Friend WithEvents UltraLabel20 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_TinNum As UltraTextEditor
    Friend WithEvents Label18 As Label
    Friend WithEvents cmb_GSTRegID As UltraCombo
    Friend WithEvents txt_Division As UltraTextEditor
    Friend WithEvents Label15 As Label
    Friend WithEvents txt_campuscode As UltraTextEditor
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_DispName As UltraTextEditor
    Friend WithEvents Label12 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_CampusType As UltraCombo
    Friend WithEvents txt_TCName As UltraTextEditor
    Friend WithEvents txt_SelCity As UltraTextEditor
    Friend WithEvents Label3 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents UltraTabPageControl17 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents btnCopyAdd As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Label10 As Label
    Friend WithEvents cmb_SelCountry As UltraCombo
    Friend WithEvents Label11 As Label
    Friend WithEvents cmb_SelState As UltraCombo
    Friend WithEvents txt_SelAddress As UltraTextEditor
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_SelPinCode As UltraTextEditor
    Friend WithEvents Label5 As Label
    Friend WithEvents chk_sameashead As UltraCheckEditor
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_SelFaxArea As UltraTextEditor
    Friend WithEvents txt_SelFaxCountry As UltraTextEditor
    Friend WithEvents txt_SelFaxNum As UltraTextEditor
    Friend WithEvents txt_SelPhNum As UltraTextEditor
    Friend WithEvents txt_SelPhArea As UltraTextEditor
    Friend WithEvents txt_SelPhCOuntry As UltraTextEditor
    Friend WithEvents Label6 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents cmb_TaxAreaID As UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_PanNum As UltraTextEditor
    Friend WithEvents Label19 As Label
    Friend WithEvents txt_InvoiceNote As UltraTextEditor

#End Region
End Class

