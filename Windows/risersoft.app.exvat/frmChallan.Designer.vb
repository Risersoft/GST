Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmChallan
	Inherits frmMax
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.InitForm()
        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_ChallanNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents chk_Returnable As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chk_NotBilled As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_RG23ADate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_RG23ANum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel9 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel10 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_PLADate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_PLANum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel11 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_RG23CDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_RG23CNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_InsureDecl As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraButton1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel14 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_SubHeadingNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel28 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_removalDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents dt_challandate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraLabel20 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel21 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents lblInsReq As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_AmountTot As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmb_TaxInvoiceType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_remark As Infragistics.Win.UltraWinEditors.UltraTextEditor
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
        Dim Appearance52 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance53 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance54 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance55 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance56 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance57 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance58 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance59 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance60 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance61 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance62 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance63 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance64 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance65 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraLabel23 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_VouchNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_challandate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_ChallanNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnGenChallan = New Infragistics.Win.Misc.UltraButton()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.chk_Returnable = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chk_NotBilled = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_RG23ADate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_RG23ANum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel9 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel10 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_PLADate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_PLANum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel11 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_RG23CDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_RG23CNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_InsureDecl = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraButton1 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel14 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_SubHeadingNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel28 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_removalDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel20 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_remark = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel21 = New Infragistics.Win.Misc.UltraLabel()
        Me.lblInsReq = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_AmountTot = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmb_TaxInvoiceType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_Rg23aAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_PlaAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_Rg23cAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel15 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_FinalAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_AccessAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel17 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtTakenAmtCurr = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txtDeducAmtOther = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txtTakenAmtOther = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel16 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel18 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel19 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel22 = New Infragistics.Win.Misc.UltraLabel()
        Me.PanelAmt = New System.Windows.Forms.Panel()
        Me.UltraLabel24 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_VehicleNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_challandate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ChallanNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.chk_Returnable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_NotBilled, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_RG23ADate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_RG23ANum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_PLADate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PLANum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_RG23CDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_RG23CNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_InsureDecl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SubHeadingNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_removalDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_remark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_AmountTot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_TaxInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Rg23aAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PlaAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Rg23cAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_FinalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_AccessAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTakenAmtCurr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeducAmtOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTakenAmtOther, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelAmt.SuspendLayout()
        CType(Me.txt_VehicleNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UltraLabel24)
        Me.Panel1.Controls.Add(Me.txt_VehicleNum)
        Me.Panel1.Controls.Add(Me.UltraLabel23)
        Me.Panel1.Controls.Add(Me.txt_VouchNum)
        Me.Panel1.Controls.Add(Me.dt_challandate)
        Me.Panel1.Controls.Add(Me.UltraLabel2)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Controls.Add(Me.txt_ChallanNum)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(646, 65)
        Me.Panel1.TabIndex = 0
        '
        'UltraLabel23
        '
        Appearance3.TextHAlignAsString = "Right"
        Me.UltraLabel23.Appearance = Appearance3
        Me.UltraLabel23.AutoSize = True
        Me.UltraLabel23.Location = New System.Drawing.Point(81, 40)
        Me.UltraLabel23.Name = "UltraLabel23"
        Me.UltraLabel23.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel23.TabIndex = 4
        Me.UltraLabel23.Text = "Voucher No."
        '
        'txt_VouchNum
        '
        Appearance4.FontData.BoldAsString = "False"
        Appearance4.FontData.ItalicAsString = "False"
        Appearance4.FontData.Name = "Arial"
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.FontData.StrikeoutAsString = "False"
        Appearance4.FontData.UnderlineAsString = "False"
        Me.txt_VouchNum.Appearance = Appearance4
        Me.txt_VouchNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_VouchNum.Location = New System.Drawing.Point(151, 37)
        Me.txt_VouchNum.Name = "txt_VouchNum"
        Me.txt_VouchNum.ReadOnly = True
        Me.txt_VouchNum.Size = New System.Drawing.Size(159, 21)
        Me.txt_VouchNum.TabIndex = 5
        '
        'dt_challandate
        '
        Me.dt_challandate.FormatString = "dd MMM yyyy hh:mm"
        Me.dt_challandate.Location = New System.Drawing.Point(423, 9)
        Me.dt_challandate.MaskInput = "{LOC}dd/mm/yyyy hh:mm"
        Me.dt_challandate.Name = "dt_challandate"
        Me.dt_challandate.Size = New System.Drawing.Size(160, 21)
        Me.dt_challandate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.dt_challandate.SpinWrap = True
        Me.dt_challandate.TabIndex = 3
        '
        'UltraLabel2
        '
        Appearance5.TextHAlignAsString = "Right"
        Me.UltraLabel2.Appearance = Appearance5
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(78, 12)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(70, 14)
        Me.UltraLabel2.TabIndex = 0
        Me.UltraLabel2.Text = "Challan Num"
        '
        'UltraLabel1
        '
        Appearance6.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance6
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(350, 13)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(70, 14)
        Me.UltraLabel1.TabIndex = 2
        Me.UltraLabel1.Text = "Challan Date"
        '
        'txt_ChallanNum
        '
        Appearance7.FontData.BoldAsString = "False"
        Appearance7.FontData.ItalicAsString = "False"
        Appearance7.FontData.Name = "Arial"
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.FontData.StrikeoutAsString = "False"
        Appearance7.FontData.UnderlineAsString = "False"
        Me.txt_ChallanNum.Appearance = Appearance7
        Me.txt_ChallanNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_ChallanNum.Location = New System.Drawing.Point(151, 9)
        Me.txt_ChallanNum.Name = "txt_ChallanNum"
        Me.txt_ChallanNum.ReadOnly = True
        Me.txt_ChallanNum.Size = New System.Drawing.Size(159, 21)
        Me.txt_ChallanNum.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnGenChallan)
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 433)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(646, 41)
        Me.Panel4.TabIndex = 36
        '
        'btnGenChallan
        '
        Appearance8.FontData.BoldAsString = "True"
        Me.btnGenChallan.Appearance = Appearance8
        Me.btnGenChallan.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnGenChallan.Location = New System.Drawing.Point(0, 0)
        Me.btnGenChallan.Name = "btnGenChallan"
        Me.btnGenChallan.Size = New System.Drawing.Size(88, 41)
        Me.btnGenChallan.TabIndex = 3
        Me.btnGenChallan.Text = "Generate Challan"
        '
        'btnSave
        '
        Appearance9.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance9
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(382, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 41)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Appearance10.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance10
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(470, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 41)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        '
        'btnOK
        '
        Appearance11.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance11
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(558, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 41)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "&OK"
        '
        'chk_Returnable
        '
        Me.chk_Returnable.Location = New System.Drawing.Point(419, 74)
        Me.chk_Returnable.Name = "chk_Returnable"
        Me.chk_Returnable.Size = New System.Drawing.Size(168, 24)
        Me.chk_Returnable.TabIndex = 3
        Me.chk_Returnable.Text = "Returnable"
        '
        'chk_NotBilled
        '
        Me.chk_NotBilled.Location = New System.Drawing.Point(419, 103)
        Me.chk_NotBilled.Name = "chk_NotBilled"
        Me.chk_NotBilled.Size = New System.Drawing.Size(168, 24)
        Me.chk_NotBilled.TabIndex = 6
        Me.chk_NotBilled.Text = "Not to be Billed"
        '
        'UltraLabel7
        '
        Appearance12.TextHAlignAsString = "Right"
        Me.UltraLabel7.Appearance = Appearance12
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(87, 181)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(62, 14)
        Me.UltraLabel7.TabIndex = 9
        Me.UltraLabel7.Text = "RG23A No."
        '
        'UltraLabel8
        '
        Appearance13.TextHAlignAsString = "Right"
        Me.UltraLabel8.Appearance = Appearance13
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(281, 181)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(28, 14)
        Me.UltraLabel8.TabIndex = 11
        Me.UltraLabel8.Text = "Date"
        '
        'dt_RG23ADate
        '
        Appearance14.FontData.BoldAsString = "False"
        Appearance14.FontData.ItalicAsString = "False"
        Appearance14.FontData.Name = "Arial"
        Appearance14.FontData.SizeInPoints = 8.25!
        Appearance14.FontData.StrikeoutAsString = "False"
        Appearance14.FontData.UnderlineAsString = "False"
        Me.dt_RG23ADate.Appearance = Appearance14
        Me.dt_RG23ADate.FormatString = "dddd dd MMM yyyy"
        Me.dt_RG23ADate.Location = New System.Drawing.Point(312, 178)
        Me.dt_RG23ADate.Name = "dt_RG23ADate"
        Me.dt_RG23ADate.NullText = "Not Defined"
        Me.dt_RG23ADate.Size = New System.Drawing.Size(144, 21)
        Me.dt_RG23ADate.TabIndex = 12
        '
        'txt_RG23ANum
        '
        Appearance15.FontData.BoldAsString = "False"
        Appearance15.FontData.ItalicAsString = "False"
        Appearance15.FontData.Name = "Arial"
        Appearance15.FontData.SizeInPoints = 8.25!
        Appearance15.FontData.StrikeoutAsString = "False"
        Appearance15.FontData.UnderlineAsString = "False"
        Me.txt_RG23ANum.Appearance = Appearance15
        Me.txt_RG23ANum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_RG23ANum.Location = New System.Drawing.Point(152, 178)
        Me.txt_RG23ANum.Name = "txt_RG23ANum"
        Me.txt_RG23ANum.Size = New System.Drawing.Size(120, 21)
        Me.txt_RG23ANum.TabIndex = 10
        '
        'UltraLabel9
        '
        Appearance16.TextHAlignAsString = "Right"
        Me.UltraLabel9.Appearance = Appearance16
        Me.UltraLabel9.AutoSize = True
        Me.UltraLabel9.Location = New System.Drawing.Point(103, 209)
        Me.UltraLabel9.Name = "UltraLabel9"
        Me.UltraLabel9.Size = New System.Drawing.Size(46, 14)
        Me.UltraLabel9.TabIndex = 15
        Me.UltraLabel9.Text = "PLA No."
        '
        'UltraLabel10
        '
        Appearance17.TextHAlignAsString = "Right"
        Me.UltraLabel10.Appearance = Appearance17
        Me.UltraLabel10.AutoSize = True
        Me.UltraLabel10.Location = New System.Drawing.Point(281, 209)
        Me.UltraLabel10.Name = "UltraLabel10"
        Me.UltraLabel10.Size = New System.Drawing.Size(28, 14)
        Me.UltraLabel10.TabIndex = 17
        Me.UltraLabel10.Text = "Date"
        '
        'dt_PLADate
        '
        Appearance18.FontData.BoldAsString = "False"
        Appearance18.FontData.ItalicAsString = "False"
        Appearance18.FontData.Name = "Arial"
        Appearance18.FontData.SizeInPoints = 8.25!
        Appearance18.FontData.StrikeoutAsString = "False"
        Appearance18.FontData.UnderlineAsString = "False"
        Me.dt_PLADate.Appearance = Appearance18
        Me.dt_PLADate.FormatString = "dddd dd MMM yyyy"
        Me.dt_PLADate.Location = New System.Drawing.Point(312, 206)
        Me.dt_PLADate.Name = "dt_PLADate"
        Me.dt_PLADate.NullText = "Not Defined"
        Me.dt_PLADate.Size = New System.Drawing.Size(144, 21)
        Me.dt_PLADate.TabIndex = 18
        '
        'txt_PLANum
        '
        Appearance19.FontData.BoldAsString = "False"
        Appearance19.FontData.ItalicAsString = "False"
        Appearance19.FontData.Name = "Arial"
        Appearance19.FontData.SizeInPoints = 8.25!
        Appearance19.FontData.StrikeoutAsString = "False"
        Appearance19.FontData.UnderlineAsString = "False"
        Me.txt_PLANum.Appearance = Appearance19
        Me.txt_PLANum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_PLANum.Location = New System.Drawing.Point(152, 206)
        Me.txt_PLANum.Name = "txt_PLANum"
        Me.txt_PLANum.Size = New System.Drawing.Size(120, 21)
        Me.txt_PLANum.TabIndex = 16
        '
        'UltraLabel11
        '
        Appearance20.TextHAlignAsString = "Right"
        Me.UltraLabel11.Appearance = Appearance20
        Me.UltraLabel11.AutoSize = True
        Me.UltraLabel11.Location = New System.Drawing.Point(86, 238)
        Me.UltraLabel11.Name = "UltraLabel11"
        Me.UltraLabel11.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel11.TabIndex = 21
        Me.UltraLabel11.Text = "RG23C No."
        '
        'UltraLabel12
        '
        Appearance21.TextHAlignAsString = "Right"
        Me.UltraLabel12.Appearance = Appearance21
        Me.UltraLabel12.AutoSize = True
        Me.UltraLabel12.Location = New System.Drawing.Point(281, 238)
        Me.UltraLabel12.Name = "UltraLabel12"
        Me.UltraLabel12.Size = New System.Drawing.Size(28, 14)
        Me.UltraLabel12.TabIndex = 23
        Me.UltraLabel12.Text = "Date"
        '
        'dt_RG23CDate
        '
        Appearance22.FontData.BoldAsString = "False"
        Appearance22.FontData.ItalicAsString = "False"
        Appearance22.FontData.Name = "Arial"
        Appearance22.FontData.SizeInPoints = 8.25!
        Appearance22.FontData.StrikeoutAsString = "False"
        Appearance22.FontData.UnderlineAsString = "False"
        Me.dt_RG23CDate.Appearance = Appearance22
        Me.dt_RG23CDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_RG23CDate.Location = New System.Drawing.Point(312, 234)
        Me.dt_RG23CDate.Name = "dt_RG23CDate"
        Me.dt_RG23CDate.NullText = "Not Defined"
        Me.dt_RG23CDate.Size = New System.Drawing.Size(144, 21)
        Me.dt_RG23CDate.TabIndex = 24
        '
        'txt_RG23CNum
        '
        Appearance23.FontData.BoldAsString = "False"
        Appearance23.FontData.ItalicAsString = "False"
        Appearance23.FontData.Name = "Arial"
        Appearance23.FontData.SizeInPoints = 8.25!
        Appearance23.FontData.StrikeoutAsString = "False"
        Appearance23.FontData.UnderlineAsString = "False"
        Me.txt_RG23CNum.Appearance = Appearance23
        Me.txt_RG23CNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_RG23CNum.Location = New System.Drawing.Point(152, 234)
        Me.txt_RG23CNum.Name = "txt_RG23CNum"
        Me.txt_RG23CNum.Size = New System.Drawing.Size(120, 21)
        Me.txt_RG23CNum.TabIndex = 22
        '
        'UltraLabel6
        '
        Appearance24.TextHAlignAsString = "Left"
        Me.UltraLabel6.Appearance = Appearance24
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(35, 294)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(114, 14)
        Me.UltraLabel6.TabIndex = 29
        Me.UltraLabel6.Text = "Insurance Declaration"
        '
        'txt_InsureDecl
        '
        Appearance25.FontData.BoldAsString = "False"
        Appearance25.FontData.ItalicAsString = "False"
        Appearance25.FontData.Name = "Arial"
        Appearance25.FontData.SizeInPoints = 8.25!
        Appearance25.FontData.StrikeoutAsString = "False"
        Appearance25.FontData.UnderlineAsString = "False"
        Me.txt_InsureDecl.Appearance = Appearance25
        Me.txt_InsureDecl.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_InsureDecl.Location = New System.Drawing.Point(152, 290)
        Me.txt_InsureDecl.Name = "txt_InsureDecl"
        Me.txt_InsureDecl.Size = New System.Drawing.Size(376, 21)
        Me.txt_InsureDecl.TabIndex = 30
        '
        'UltraButton1
        '
        Appearance26.FontData.BoldAsString = "True"
        Me.UltraButton1.Appearance = Appearance26
        Me.UltraButton1.Location = New System.Drawing.Point(536, 290)
        Me.UltraButton1.Name = "UltraButton1"
        Me.UltraButton1.Size = New System.Drawing.Size(95, 24)
        Me.UltraButton1.TabIndex = 31
        Me.UltraButton1.Text = "Fill"
        '
        'UltraLabel13
        '
        Appearance27.TextHAlignAsString = "Right"
        Me.UltraLabel13.Appearance = Appearance27
        Me.UltraLabel13.AutoSize = True
        Me.UltraLabel13.Location = New System.Drawing.Point(23, 107)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(126, 14)
        Me.UltraLabel13.TabIndex = 4
        Me.UltraLabel13.Text = "Removal Date and Time"
        '
        'UltraLabel14
        '
        Appearance28.TextHAlignAsString = "Right"
        Me.UltraLabel14.Appearance = Appearance28
        Me.UltraLabel14.AutoSize = True
        Me.UltraLabel14.Location = New System.Drawing.Point(59, 266)
        Me.UltraLabel14.Name = "UltraLabel14"
        Me.UltraLabel14.Size = New System.Drawing.Size(90, 14)
        Me.UltraLabel14.TabIndex = 27
        Me.UltraLabel14.Text = "Sub Heading No."
        '
        'txt_SubHeadingNum
        '
        Appearance29.FontData.BoldAsString = "False"
        Appearance29.FontData.ItalicAsString = "False"
        Appearance29.FontData.Name = "Arial"
        Appearance29.FontData.SizeInPoints = 8.25!
        Appearance29.FontData.StrikeoutAsString = "False"
        Appearance29.FontData.UnderlineAsString = "False"
        Me.txt_SubHeadingNum.Appearance = Appearance29
        Me.txt_SubHeadingNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_SubHeadingNum.Location = New System.Drawing.Point(152, 262)
        Me.txt_SubHeadingNum.Name = "txt_SubHeadingNum"
        Me.txt_SubHeadingNum.Size = New System.Drawing.Size(479, 21)
        Me.txt_SubHeadingNum.TabIndex = 28
        '
        'UltraLabel28
        '
        Appearance30.TextHAlignAsString = "Right"
        Me.UltraLabel28.Appearance = Appearance30
        Me.UltraLabel28.AutoSize = True
        Me.UltraLabel28.Location = New System.Drawing.Point(81, 78)
        Me.UltraLabel28.Name = "UltraLabel28"
        Me.UltraLabel28.Size = New System.Drawing.Size(68, 14)
        Me.UltraLabel28.TabIndex = 1
        Me.UltraLabel28.Text = "Invoice Type"
        '
        'dt_removalDate
        '
        Appearance31.FontData.BoldAsString = "False"
        Appearance31.FontData.ItalicAsString = "False"
        Appearance31.FontData.Name = "Arial"
        Appearance31.FontData.SizeInPoints = 8.25!
        Appearance31.FontData.StrikeoutAsString = "False"
        Appearance31.FontData.UnderlineAsString = "False"
        Me.dt_removalDate.Appearance = Appearance31
        Me.dt_removalDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.dt_removalDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_removalDate.Location = New System.Drawing.Point(152, 103)
        Me.dt_removalDate.Name = "dt_removalDate"
        Me.dt_removalDate.NullText = "Not Defined"
        Me.dt_removalDate.Size = New System.Drawing.Size(200, 21)
        Me.dt_removalDate.TabIndex = 5
        '
        'UltraLabel20
        '
        Appearance32.TextHAlignAsString = "Right"
        Me.UltraLabel20.Appearance = Appearance32
        Me.UltraLabel20.AutoSize = True
        Me.UltraLabel20.Location = New System.Drawing.Point(100, 135)
        Me.UltraLabel20.Name = "UltraLabel20"
        Me.UltraLabel20.Size = New System.Drawing.Size(49, 14)
        Me.UltraLabel20.TabIndex = 7
        Me.UltraLabel20.Text = "Remarks"
        '
        'txt_remark
        '
        Me.txt_remark.AcceptsReturn = True
        Appearance33.FontData.BoldAsString = "False"
        Appearance33.FontData.ItalicAsString = "False"
        Appearance33.FontData.Name = "Arial"
        Appearance33.FontData.SizeInPoints = 8.25!
        Appearance33.FontData.StrikeoutAsString = "False"
        Appearance33.FontData.UnderlineAsString = "False"
        Me.txt_remark.Appearance = Appearance33
        Me.txt_remark.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_remark.Location = New System.Drawing.Point(152, 131)
        Me.txt_remark.Multiline = True
        Me.txt_remark.Name = "txt_remark"
        Me.txt_remark.Size = New System.Drawing.Size(479, 40)
        Me.txt_remark.TabIndex = 8
        '
        'UltraLabel21
        '
        Appearance34.TextHAlignAsString = "Right"
        Me.UltraLabel21.Appearance = Appearance34
        Me.UltraLabel21.Location = New System.Drawing.Point(10, 322)
        Me.UltraLabel21.Name = "UltraLabel21"
        Me.UltraLabel21.Size = New System.Drawing.Size(136, 35)
        Me.UltraLabel21.TabIndex = 32
        Me.UltraLabel21.Text = "Insurance Requirement from Sales"
        '
        'lblInsReq
        '
        Appearance35.TextHAlignAsString = "Left"
        Me.lblInsReq.Appearance = Appearance35
        Me.lblInsReq.Location = New System.Drawing.Point(152, 322)
        Me.lblInsReq.Name = "lblInsReq"
        Me.lblInsReq.Size = New System.Drawing.Size(161, 16)
        Me.lblInsReq.TabIndex = 33
        Me.lblInsReq.Text = "Required"
        '
        'UltraLabel5
        '
        Appearance36.TextHAlignAsString = "Right"
        Me.UltraLabel5.Appearance = Appearance36
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(483, 325)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(29, 14)
        Me.UltraLabel5.TabIndex = 34
        Me.UltraLabel5.Text = "Total"
        '
        'txt_AmountTot
        '
        Appearance37.FontData.BoldAsString = "False"
        Appearance37.FontData.ItalicAsString = "False"
        Appearance37.FontData.Name = "Arial"
        Appearance37.FontData.SizeInPoints = 8.25!
        Appearance37.FontData.StrikeoutAsString = "False"
        Appearance37.FontData.UnderlineAsString = "False"
        Appearance37.TextHAlignAsString = "Right"
        Me.txt_AmountTot.Appearance = Appearance37
        Me.txt_AmountTot.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_AmountTot.Location = New System.Drawing.Point(515, 320)
        Me.txt_AmountTot.Name = "txt_AmountTot"
        Me.txt_AmountTot.ReadOnly = True
        Me.txt_AmountTot.Size = New System.Drawing.Size(116, 21)
        Me.txt_AmountTot.TabIndex = 35
        '
        'cmb_TaxInvoiceType
        '
        Appearance38.BackColor = System.Drawing.SystemColors.Window
        Appearance38.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmb_TaxInvoiceType.DisplayLayout.Appearance = Appearance38
        Me.cmb_TaxInvoiceType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmb_TaxInvoiceType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance39.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance39.BorderColor = System.Drawing.SystemColors.Window
        Me.cmb_TaxInvoiceType.DisplayLayout.GroupByBox.Appearance = Appearance39
        Appearance40.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmb_TaxInvoiceType.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance40
        Me.cmb_TaxInvoiceType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance41.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance41.BackColor2 = System.Drawing.SystemColors.Control
        Appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance41.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmb_TaxInvoiceType.DisplayLayout.GroupByBox.PromptAppearance = Appearance41
        Me.cmb_TaxInvoiceType.DisplayLayout.MaxColScrollRegions = 1
        Me.cmb_TaxInvoiceType.DisplayLayout.MaxRowScrollRegions = 1
        Appearance42.BackColor = System.Drawing.SystemColors.Window
        Appearance42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.ActiveCellAppearance = Appearance42
        Appearance43.BackColor = System.Drawing.SystemColors.Highlight
        Appearance43.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.ActiveRowAppearance = Appearance43
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance44.BackColor = System.Drawing.SystemColors.Window
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.CardAreaAppearance = Appearance44
        Appearance45.BorderColor = System.Drawing.Color.Silver
        Appearance45.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.CellAppearance = Appearance45
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.CellPadding = 0
        Appearance46.BackColor = System.Drawing.SystemColors.Control
        Appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance46.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance46.BorderColor = System.Drawing.SystemColors.Window
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.GroupByRowAppearance = Appearance46
        Appearance47.TextHAlignAsString = "Left"
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.HeaderAppearance = Appearance47
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance48.BackColor = System.Drawing.SystemColors.Window
        Appearance48.BorderColor = System.Drawing.Color.Silver
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.RowAppearance = Appearance48
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance49.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmb_TaxInvoiceType.DisplayLayout.Override.TemplateAddRowAppearance = Appearance49
        Me.cmb_TaxInvoiceType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmb_TaxInvoiceType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmb_TaxInvoiceType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmb_TaxInvoiceType.Location = New System.Drawing.Point(152, 74)
        Me.cmb_TaxInvoiceType.Name = "cmb_TaxInvoiceType"
        Me.cmb_TaxInvoiceType.ReadOnly = True
        Me.cmb_TaxInvoiceType.Size = New System.Drawing.Size(200, 22)
        Me.cmb_TaxInvoiceType.TabIndex = 2
        Me.cmb_TaxInvoiceType.Text = "UltraCombo1"
        '
        'txt_Rg23aAmount
        '
        Appearance50.FontData.BoldAsString = "False"
        Appearance50.FontData.ItalicAsString = "False"
        Appearance50.FontData.Name = "Arial"
        Appearance50.FontData.SizeInPoints = 8.25!
        Appearance50.FontData.StrikeoutAsString = "False"
        Appearance50.FontData.UnderlineAsString = "False"
        Me.txt_Rg23aAmount.Appearance = Appearance50
        Me.txt_Rg23aAmount.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_Rg23aAmount.Location = New System.Drawing.Point(511, 178)
        Me.txt_Rg23aAmount.Name = "txt_Rg23aAmount"
        Me.txt_Rg23aAmount.Size = New System.Drawing.Size(120, 21)
        Me.txt_Rg23aAmount.TabIndex = 14
        '
        'txt_PlaAmount
        '
        Appearance51.FontData.BoldAsString = "False"
        Appearance51.FontData.ItalicAsString = "False"
        Appearance51.FontData.Name = "Arial"
        Appearance51.FontData.SizeInPoints = 8.25!
        Appearance51.FontData.StrikeoutAsString = "False"
        Appearance51.FontData.UnderlineAsString = "False"
        Me.txt_PlaAmount.Appearance = Appearance51
        Me.txt_PlaAmount.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_PlaAmount.Location = New System.Drawing.Point(511, 205)
        Me.txt_PlaAmount.Name = "txt_PlaAmount"
        Me.txt_PlaAmount.Size = New System.Drawing.Size(120, 21)
        Me.txt_PlaAmount.TabIndex = 20
        '
        'txt_Rg23cAmount
        '
        Appearance52.FontData.BoldAsString = "False"
        Appearance52.FontData.ItalicAsString = "False"
        Appearance52.FontData.Name = "Arial"
        Appearance52.FontData.SizeInPoints = 8.25!
        Appearance52.FontData.StrikeoutAsString = "False"
        Appearance52.FontData.UnderlineAsString = "False"
        Me.txt_Rg23cAmount.Appearance = Appearance52
        Me.txt_Rg23cAmount.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_Rg23cAmount.Location = New System.Drawing.Point(511, 234)
        Me.txt_Rg23cAmount.Name = "txt_Rg23cAmount"
        Me.txt_Rg23cAmount.Size = New System.Drawing.Size(120, 21)
        Me.txt_Rg23cAmount.TabIndex = 26
        '
        'UltraLabel3
        '
        Appearance53.TextHAlignAsString = "Right"
        Me.UltraLabel3.Appearance = Appearance53
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(465, 182)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel3.TabIndex = 13
        Me.UltraLabel3.Text = "Amount"
        '
        'UltraLabel4
        '
        Appearance54.TextHAlignAsString = "Right"
        Me.UltraLabel4.Appearance = Appearance54
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(465, 210)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel4.TabIndex = 19
        Me.UltraLabel4.Text = "Amount"
        '
        'UltraLabel15
        '
        Appearance55.TextHAlignAsString = "Right"
        Me.UltraLabel15.Appearance = Appearance55
        Me.UltraLabel15.AutoSize = True
        Me.UltraLabel15.Location = New System.Drawing.Point(465, 238)
        Me.UltraLabel15.Name = "UltraLabel15"
        Me.UltraLabel15.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel15.TabIndex = 25
        Me.UltraLabel15.Text = "Amount"
        '
        'txt_FinalAmount
        '
        Appearance56.FontData.BoldAsString = "False"
        Appearance56.FontData.ItalicAsString = "False"
        Appearance56.FontData.Name = "Arial"
        Appearance56.FontData.SizeInPoints = 8.25!
        Appearance56.FontData.StrikeoutAsString = "False"
        Appearance56.FontData.UnderlineAsString = "False"
        Appearance56.TextHAlignAsString = "Right"
        Me.txt_FinalAmount.Appearance = Appearance56
        Me.txt_FinalAmount.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_FinalAmount.Location = New System.Drawing.Point(515, 21)
        Me.txt_FinalAmount.Name = "txt_FinalAmount"
        Me.txt_FinalAmount.ReadOnly = True
        Me.txt_FinalAmount.Size = New System.Drawing.Size(116, 21)
        Me.txt_FinalAmount.TabIndex = 37
        '
        'txt_AccessAmount
        '
        Appearance57.FontData.BoldAsString = "False"
        Appearance57.FontData.ItalicAsString = "False"
        Appearance57.FontData.Name = "Arial"
        Appearance57.FontData.SizeInPoints = 8.25!
        Appearance57.FontData.StrikeoutAsString = "False"
        Appearance57.FontData.UnderlineAsString = "False"
        Me.txt_AccessAmount.Appearance = Appearance57
        Me.txt_AccessAmount.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_AccessAmount.Location = New System.Drawing.Point(187, 22)
        Me.txt_AccessAmount.Name = "txt_AccessAmount"
        Me.txt_AccessAmount.Size = New System.Drawing.Size(116, 21)
        Me.txt_AccessAmount.TabIndex = 38
        '
        'UltraLabel17
        '
        Appearance58.TextHAlignAsString = "Right"
        Me.UltraLabel17.Appearance = Appearance58
        Me.UltraLabel17.AutoSize = True
        Me.UltraLabel17.Location = New System.Drawing.Point(441, 25)
        Me.UltraLabel17.Name = "UltraLabel17"
        Me.UltraLabel17.Size = New System.Drawing.Size(71, 14)
        Me.UltraLabel17.TabIndex = 40
        Me.UltraLabel17.Text = "Final Amount"
        '
        'txtTakenAmtCurr
        '
        Appearance59.FontData.BoldAsString = "False"
        Appearance59.FontData.ItalicAsString = "False"
        Appearance59.FontData.Name = "Arial"
        Appearance59.FontData.SizeInPoints = 8.25!
        Appearance59.FontData.StrikeoutAsString = "False"
        Appearance59.FontData.UnderlineAsString = "False"
        Me.txtTakenAmtCurr.Appearance = Appearance59
        Me.txtTakenAmtCurr.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txtTakenAmtCurr.Location = New System.Drawing.Point(320, 22)
        Me.txtTakenAmtCurr.Name = "txtTakenAmtCurr"
        Me.txtTakenAmtCurr.ReadOnly = True
        Me.txtTakenAmtCurr.Size = New System.Drawing.Size(116, 21)
        Me.txtTakenAmtCurr.TabIndex = 41
        '
        'txtDeducAmtOther
        '
        Appearance60.FontData.BoldAsString = "False"
        Appearance60.FontData.ItalicAsString = "False"
        Appearance60.FontData.Name = "Arial"
        Appearance60.FontData.SizeInPoints = 8.25!
        Appearance60.FontData.StrikeoutAsString = "False"
        Appearance60.FontData.UnderlineAsString = "False"
        Me.txtDeducAmtOther.Appearance = Appearance60
        Me.txtDeducAmtOther.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txtDeducAmtOther.Location = New System.Drawing.Point(187, 46)
        Me.txtDeducAmtOther.Name = "txtDeducAmtOther"
        Me.txtDeducAmtOther.ReadOnly = True
        Me.txtDeducAmtOther.Size = New System.Drawing.Size(116, 21)
        Me.txtDeducAmtOther.TabIndex = 42
        '
        'txtTakenAmtOther
        '
        Appearance61.FontData.BoldAsString = "False"
        Appearance61.FontData.ItalicAsString = "False"
        Appearance61.FontData.Name = "Arial"
        Appearance61.FontData.SizeInPoints = 8.25!
        Appearance61.FontData.StrikeoutAsString = "False"
        Appearance61.FontData.UnderlineAsString = "False"
        Me.txtTakenAmtOther.Appearance = Appearance61
        Me.txtTakenAmtOther.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txtTakenAmtOther.Location = New System.Drawing.Point(320, 46)
        Me.txtTakenAmtOther.Name = "txtTakenAmtOther"
        Me.txtTakenAmtOther.ReadOnly = True
        Me.txtTakenAmtOther.Size = New System.Drawing.Size(116, 21)
        Me.txtTakenAmtOther.TabIndex = 43
        '
        'UltraLabel16
        '
        Appearance62.TextHAlignAsString = "Right"
        Me.UltraLabel16.Appearance = Appearance62
        Me.UltraLabel16.AutoSize = True
        Me.UltraLabel16.Location = New System.Drawing.Point(187, 5)
        Me.UltraLabel16.Name = "UltraLabel16"
        Me.UltraLabel16.Size = New System.Drawing.Size(97, 14)
        Me.UltraLabel16.TabIndex = 44
        Me.UltraLabel16.Text = "Duduction Amount"
        '
        'UltraLabel18
        '
        Appearance63.TextHAlignAsString = "Right"
        Me.UltraLabel18.Appearance = Appearance63
        Me.UltraLabel18.AutoSize = True
        Me.UltraLabel18.Location = New System.Drawing.Point(320, 5)
        Me.UltraLabel18.Name = "UltraLabel18"
        Me.UltraLabel18.Size = New System.Drawing.Size(78, 14)
        Me.UltraLabel18.TabIndex = 45
        Me.UltraLabel18.Text = "Taken Amount"
        '
        'UltraLabel19
        '
        Appearance64.TextHAlignAsString = "Right"
        Me.UltraLabel19.Appearance = Appearance64
        Me.UltraLabel19.AutoSize = True
        Me.UltraLabel19.Location = New System.Drawing.Point(117, 25)
        Me.UltraLabel19.Name = "UltraLabel19"
        Me.UltraLabel19.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel19.TabIndex = 46
        Me.UltraLabel19.Text = "This Challan"
        '
        'UltraLabel22
        '
        Appearance65.TextHAlignAsString = "Right"
        Me.UltraLabel22.Appearance = Appearance65
        Me.UltraLabel22.AutoSize = True
        Me.UltraLabel22.Location = New System.Drawing.Point(110, 49)
        Me.UltraLabel22.Name = "UltraLabel22"
        Me.UltraLabel22.Size = New System.Drawing.Size(74, 14)
        Me.UltraLabel22.TabIndex = 47
        Me.UltraLabel22.Text = "Other Challan"
        '
        'PanelAmt
        '
        Me.PanelAmt.Controls.Add(Me.txtTakenAmtOther)
        Me.PanelAmt.Controls.Add(Me.UltraLabel22)
        Me.PanelAmt.Controls.Add(Me.txt_FinalAmount)
        Me.PanelAmt.Controls.Add(Me.UltraLabel19)
        Me.PanelAmt.Controls.Add(Me.txt_AccessAmount)
        Me.PanelAmt.Controls.Add(Me.UltraLabel18)
        Me.PanelAmt.Controls.Add(Me.UltraLabel17)
        Me.PanelAmt.Controls.Add(Me.UltraLabel16)
        Me.PanelAmt.Controls.Add(Me.txtTakenAmtCurr)
        Me.PanelAmt.Controls.Add(Me.txtDeducAmtOther)
        Me.PanelAmt.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelAmt.Location = New System.Drawing.Point(0, 361)
        Me.PanelAmt.Name = "PanelAmt"
        Me.PanelAmt.Size = New System.Drawing.Size(646, 72)
        Me.PanelAmt.TabIndex = 48
        '
        'UltraLabel24
        '
        Appearance1.TextHAlignAsString = "Right"
        Me.UltraLabel24.Appearance = Appearance1
        Me.UltraLabel24.AutoSize = True
        Me.UltraLabel24.Location = New System.Drawing.Point(352, 40)
        Me.UltraLabel24.Name = "UltraLabel24"
        Me.UltraLabel24.Size = New System.Drawing.Size(68, 14)
        Me.UltraLabel24.TabIndex = 12
        Me.UltraLabel24.Text = "Vehicle Num"
        '
        'txt_VehicleNum
        '
        Appearance2.FontData.BoldAsString = "False"
        Appearance2.FontData.ItalicAsString = "False"
        Appearance2.FontData.Name = "Arial"
        Appearance2.FontData.SizeInPoints = 8.25!
        Appearance2.FontData.StrikeoutAsString = "False"
        Appearance2.FontData.UnderlineAsString = "False"
        Me.txt_VehicleNum.Appearance = Appearance2
        Me.txt_VehicleNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_VehicleNum.Location = New System.Drawing.Point(423, 37)
        Me.txt_VehicleNum.Name = "txt_VehicleNum"
        Me.txt_VehicleNum.ReadOnly = True
        Me.txt_VehicleNum.Size = New System.Drawing.Size(160, 21)
        Me.txt_VehicleNum.TabIndex = 13
        '
        'frmChallan
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "PERFORMA DELIVERY CHALLAN"
        Me.ClientSize = New System.Drawing.Size(646, 474)
        Me.Controls.Add(Me.PanelAmt)
        Me.Controls.Add(Me.UltraLabel15)
        Me.Controls.Add(Me.UltraLabel4)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.txt_Rg23cAmount)
        Me.Controls.Add(Me.txt_PlaAmount)
        Me.Controls.Add(Me.txt_Rg23aAmount)
        Me.Controls.Add(Me.cmb_TaxInvoiceType)
        Me.Controls.Add(Me.UltraLabel5)
        Me.Controls.Add(Me.txt_AmountTot)
        Me.Controls.Add(Me.lblInsReq)
        Me.Controls.Add(Me.UltraLabel21)
        Me.Controls.Add(Me.UltraLabel20)
        Me.Controls.Add(Me.txt_remark)
        Me.Controls.Add(Me.dt_removalDate)
        Me.Controls.Add(Me.UltraLabel28)
        Me.Controls.Add(Me.UltraLabel14)
        Me.Controls.Add(Me.txt_SubHeadingNum)
        Me.Controls.Add(Me.UltraLabel13)
        Me.Controls.Add(Me.UltraButton1)
        Me.Controls.Add(Me.UltraLabel6)
        Me.Controls.Add(Me.txt_InsureDecl)
        Me.Controls.Add(Me.UltraLabel11)
        Me.Controls.Add(Me.UltraLabel12)
        Me.Controls.Add(Me.dt_RG23CDate)
        Me.Controls.Add(Me.txt_RG23CNum)
        Me.Controls.Add(Me.UltraLabel9)
        Me.Controls.Add(Me.UltraLabel10)
        Me.Controls.Add(Me.dt_PLADate)
        Me.Controls.Add(Me.txt_PLANum)
        Me.Controls.Add(Me.UltraLabel7)
        Me.Controls.Add(Me.UltraLabel8)
        Me.Controls.Add(Me.dt_RG23ADate)
        Me.Controls.Add(Me.txt_RG23ANum)
        Me.Controls.Add(Me.chk_NotBilled)
        Me.Controls.Add(Me.chk_Returnable)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.Name = "frmChallan"
        Me.Text = "PERFORMA DELIVERY CHALLAN"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_challandate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ChallanNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.chk_Returnable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_NotBilled, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_RG23ADate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_RG23ANum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_PLADate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PLANum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_RG23CDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_RG23CNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_InsureDecl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SubHeadingNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_removalDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_remark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_AmountTot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_TaxInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Rg23aAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PlaAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Rg23cAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_FinalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_AccessAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTakenAmtCurr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeducAmtOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTakenAmtOther, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelAmt.ResumeLayout(False)
        Me.PanelAmt.PerformLayout()
        CType(Me.txt_VehicleNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_Rg23aAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_PlaAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_Rg23cAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel15 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_FinalAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_AccessAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel17 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtTakenAmtCurr As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txtDeducAmtOther As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txtTakenAmtOther As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel16 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel18 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel19 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel22 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents PanelAmt As Windows.Forms.Panel
    Friend WithEvents btnGenChallan As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel23 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_VouchNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel24 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_VehicleNum As Infragistics.Win.UltraWinEditors.UltraTextEditor

#End Region
End Class

