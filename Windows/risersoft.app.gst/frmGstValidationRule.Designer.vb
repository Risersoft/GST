<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGstValidationRule
    Inherits frmMax

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.InitForm()
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
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chk_IsDisabled = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_ResultType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_FieldName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_RuleLevel = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_RuleType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_ValidationText = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Code = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmb_DocType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.txt_Expression = New risersoft.[shared].win.ctlFlexEditorScintilla()
        Me.btnDeleteValidRule = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.chk_IsDisabled, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_ResultType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_FieldName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_RuleLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_RuleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ValidationText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_DocType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chk_IsDisabled)
        Me.Panel1.Controls.Add(Me.UltraLabel6)
        Me.Panel1.Controls.Add(Me.cmb_ResultType)
        Me.Panel1.Controls.Add(Me.UltraLabel5)
        Me.Panel1.Controls.Add(Me.txt_FieldName)
        Me.Panel1.Controls.Add(Me.UltraLabel4)
        Me.Panel1.Controls.Add(Me.cmb_RuleLevel)
        Me.Panel1.Controls.Add(Me.UltraLabel3)
        Me.Panel1.Controls.Add(Me.cmb_RuleType)
        Me.Panel1.Controls.Add(Me.txt_ValidationText)
        Me.Panel1.Controls.Add(Me.UltraLabel2)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Controls.Add(Me.UltraLabel13)
        Me.Panel1.Controls.Add(Me.txt_Code)
        Me.Panel1.Controls.Add(Me.cmb_DocType)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(611, 156)
        Me.Panel1.TabIndex = 1
        '
        'chk_IsDisabled
        '
        Me.chk_IsDisabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_IsDisabled.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.chk_IsDisabled.Location = New System.Drawing.Point(24, 129)
        Me.chk_IsDisabled.Name = "chk_IsDisabled"
        Me.chk_IsDisabled.Size = New System.Drawing.Size(82, 21)
        Me.chk_IsDisabled.TabIndex = 254
        Me.chk_IsDisabled.Text = "IsDisabled"
        '
        'UltraLabel6
        '
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(342, 18)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(65, 14)
        Me.UltraLabel6.TabIndex = 45
        Me.UltraLabel6.Text = "Result Type"
        '
        'cmb_ResultType
        '
        Appearance1.FontData.BoldAsString = "False"
        Me.cmb_ResultType.Appearance = Appearance1
        Me.cmb_ResultType.Location = New System.Drawing.Point(410, 15)
        Me.cmb_ResultType.Name = "cmb_ResultType"
        Me.cmb_ResultType.Size = New System.Drawing.Size(196, 22)
        Me.cmb_ResultType.TabIndex = 44
        Me.cmb_ResultType.Text = "UltraCombo5"
        '
        'UltraLabel5
        '
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(24, 109)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(62, 14)
        Me.UltraLabel5.TabIndex = 43
        Me.UltraLabel5.Text = "Field Name"
        '
        'txt_FieldName
        '
        Appearance2.FontData.BoldAsString = "False"
        Me.txt_FieldName.Appearance = Appearance2
        Me.txt_FieldName.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_FieldName.Location = New System.Drawing.Point(89, 108)
        Me.txt_FieldName.Name = "txt_FieldName"
        Me.txt_FieldName.Size = New System.Drawing.Size(254, 17)
        Me.txt_FieldName.TabIndex = 42
        Me.txt_FieldName.Text = "UltraTextEditor1"
        '
        'UltraLabel4
        '
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(349, 110)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(58, 14)
        Me.UltraLabel4.TabIndex = 41
        Me.UltraLabel4.Text = "Rule Level"
        '
        'cmb_RuleLevel
        '
        Appearance3.FontData.BoldAsString = "False"
        Me.cmb_RuleLevel.Appearance = Appearance3
        Me.cmb_RuleLevel.Location = New System.Drawing.Point(410, 107)
        Me.cmb_RuleLevel.Name = "cmb_RuleLevel"
        Me.cmb_RuleLevel.Size = New System.Drawing.Size(196, 22)
        Me.cmb_RuleLevel.TabIndex = 40
        Me.cmb_RuleLevel.Text = "UltraCombo5"
        '
        'UltraLabel3
        '
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(351, 80)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(56, 14)
        Me.UltraLabel3.TabIndex = 39
        Me.UltraLabel3.Text = "Rule Type"
        '
        'cmb_RuleType
        '
        Appearance4.FontData.BoldAsString = "False"
        Me.cmb_RuleType.Appearance = Appearance4
        Me.cmb_RuleType.Location = New System.Drawing.Point(410, 77)
        Me.cmb_RuleType.Name = "cmb_RuleType"
        Me.cmb_RuleType.Size = New System.Drawing.Size(196, 22)
        Me.cmb_RuleType.TabIndex = 38
        Me.cmb_RuleType.Text = "UltraCombo5"
        '
        'txt_ValidationText
        '
        Me.txt_ValidationText.AcceptsReturn = True
        Me.txt_ValidationText.Location = New System.Drawing.Point(89, 44)
        Me.txt_ValidationText.Multiline = True
        Me.txt_ValidationText.Name = "txt_ValidationText"
        Me.txt_ValidationText.Size = New System.Drawing.Size(254, 54)
        Me.txt_ValidationText.TabIndex = 37
        Me.txt_ValidationText.Text = "UltraTextEditor3"
        '
        'UltraLabel2
        '
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(7, 44)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(79, 14)
        Me.UltraLabel2.TabIndex = 36
        Me.UltraLabel2.Text = "Validation Text"
        '
        'UltraLabel1
        '
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(354, 49)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(53, 14)
        Me.UltraLabel1.TabIndex = 35
        Me.UltraLabel1.Text = "Doc Type"
        '
        'UltraLabel13
        '
        Me.UltraLabel13.AutoSize = True
        Me.UltraLabel13.Location = New System.Drawing.Point(55, 16)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(31, 14)
        Me.UltraLabel13.TabIndex = 34
        Me.UltraLabel13.Text = "Code"
        '
        'txt_Code
        '
        Appearance5.FontData.BoldAsString = "False"
        Me.txt_Code.Appearance = Appearance5
        Me.txt_Code.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Code.Location = New System.Drawing.Point(89, 15)
        Me.txt_Code.Name = "txt_Code"
        Me.txt_Code.Size = New System.Drawing.Size(192, 17)
        Me.txt_Code.TabIndex = 23
        Me.txt_Code.Text = "UltraTextEditor1"
        '
        'cmb_DocType
        '
        Appearance6.FontData.BoldAsString = "False"
        Me.cmb_DocType.Appearance = Appearance6
        Me.cmb_DocType.Location = New System.Drawing.Point(410, 46)
        Me.cmb_DocType.Name = "cmb_DocType"
        Me.cmb_DocType.Size = New System.Drawing.Size(196, 22)
        Me.cmb_DocType.TabIndex = 21
        Me.cmb_DocType.Text = "UltraCombo5"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnDeleteValidRule)
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 349)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(611, 39)
        Me.Panel4.TabIndex = 2
        '
        'btnSave
        '
        Appearance8.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance8
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(347, 0)
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
        Me.btnCancel.Location = New System.Drawing.Point(435, 0)
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
        Me.btnOK.Location = New System.Drawing.Point(523, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 39)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'txt_Expression
        '
        Me.txt_Expression.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_Expression.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_Expression.LabelText = ""
        Me.txt_Expression.Lexer = ScintillaNET.Lexer.Json
        Me.txt_Expression.Location = New System.Drawing.Point(0, 156)
        Me.txt_Expression.Name = "txt_Expression"
        Me.txt_Expression.Size = New System.Drawing.Size(611, 193)
        Me.txt_Expression.TabIndex = 10
        '
        'btnDeleteValidRule
        '
        Appearance7.BackColor = System.Drawing.Color.LightCoral
        Appearance7.FontData.BoldAsString = "True"
        Me.btnDeleteValidRule.Appearance = Appearance7
        Me.btnDeleteValidRule.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteValidRule.Location = New System.Drawing.Point(0, 0)
        Me.btnDeleteValidRule.Name = "btnDeleteValidRule"
        Me.btnDeleteValidRule.Size = New System.Drawing.Size(88, 39)
        Me.btnDeleteValidRule.TabIndex = 13
        Me.btnDeleteValidRule.Text = "Delete"
        Me.btnDeleteValidRule.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'frmGstValidationRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "Validation Rule"
        Me.ClientSize = New System.Drawing.Size(611, 388)
        Me.Controls.Add(Me.txt_Expression)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmGstValidationRule"
        Me.Text = "Validation Rule"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chk_IsDisabled, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_ResultType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_FieldName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_RuleLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_RuleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ValidationText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_DocType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents txt_Code As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmb_DocType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_RuleLevel As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_RuleType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_ValidationText As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_FieldName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_Expression As ctlFlexEditorScintilla
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_ResultType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents chk_IsDisabled As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents btnDeleteValidRule As Infragistics.Win.Misc.UltraButton
End Class
