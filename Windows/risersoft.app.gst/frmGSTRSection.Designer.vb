<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGSTRSection
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
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.txt_CDNOrigFilter = New risersoft.[shared].win.ctlFlexEditorScintilla()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.txt_TypeFilter = New risersoft.[shared].win.ctlFlexEditorScintilla()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.txt_OuterFilter = New risersoft.[shared].win.ctlFlexEditorScintilla()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txt_ReturnType = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_DocType2 = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Section = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmb_DocType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraTabControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage3 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage4 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.btnDeleteGSTRSec = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.txt_ReturnType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_DocType2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Section, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_DocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl3.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl4.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.txt_CDNOrigFilter)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(610, 72)
        '
        'txt_CDNOrigFilter
        '
        Me.txt_CDNOrigFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.txt_CDNOrigFilter.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_CDNOrigFilter.LabelText = ""
        Me.txt_CDNOrigFilter.Lexer = ScintillaNET.Lexer.Sql
        Me.txt_CDNOrigFilter.Location = New System.Drawing.Point(0, 0)
        Me.txt_CDNOrigFilter.Name = "txt_CDNOrigFilter"
        Me.txt_CDNOrigFilter.Size = New System.Drawing.Size(610, 72)
        Me.txt_CDNOrigFilter.TabIndex = 11
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.txt_TypeFilter)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(610, 71)
        '
        'txt_TypeFilter
        '
        Me.txt_TypeFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.txt_TypeFilter.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_TypeFilter.LabelText = ""
        Me.txt_TypeFilter.Lexer = ScintillaNET.Lexer.Sql
        Me.txt_TypeFilter.Location = New System.Drawing.Point(0, 0)
        Me.txt_TypeFilter.Name = "txt_TypeFilter"
        Me.txt_TypeFilter.Size = New System.Drawing.Size(610, 71)
        Me.txt_TypeFilter.TabIndex = 11
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.txt_OuterFilter)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(610, 70)
        '
        'txt_OuterFilter
        '
        Me.txt_OuterFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.txt_OuterFilter.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_OuterFilter.LabelText = ""
        Me.txt_OuterFilter.Lexer = ScintillaNET.Lexer.Sql
        Me.txt_OuterFilter.Location = New System.Drawing.Point(0, 0)
        Me.txt_OuterFilter.Name = "txt_OuterFilter"
        Me.txt_OuterFilter.Size = New System.Drawing.Size(610, 74)
        Me.txt_OuterFilter.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txt_ReturnType)
        Me.Panel1.Controls.Add(Me.UltraLabel6)
        Me.Panel1.Controls.Add(Me.UltraLabel3)
        Me.Panel1.Controls.Add(Me.cmb_DocType2)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Controls.Add(Me.UltraLabel13)
        Me.Panel1.Controls.Add(Me.txt_Section)
        Me.Panel1.Controls.Add(Me.cmb_DocType)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(614, 84)
        Me.Panel1.TabIndex = 2
        '
        'txt_ReturnType
        '
        Appearance1.FontData.BoldAsString = "False"
        Me.txt_ReturnType.Appearance = Appearance1
        Me.txt_ReturnType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_ReturnType.Location = New System.Drawing.Point(89, 47)
        Me.txt_ReturnType.Name = "txt_ReturnType"
        Me.txt_ReturnType.Size = New System.Drawing.Size(192, 17)
        Me.txt_ReturnType.TabIndex = 46
        Me.txt_ReturnType.Text = "UltraTextEditor1"
        '
        'UltraLabel6
        '
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(19, 49)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel6.TabIndex = 45
        Me.UltraLabel6.Text = "Return Type"
        '
        'UltraLabel3
        '
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(324, 49)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(59, 14)
        Me.UltraLabel3.TabIndex = 39
        Me.UltraLabel3.Text = "Doc Type2"
        '
        'cmb_DocType2
        '
        Appearance2.FontData.BoldAsString = "False"
        Me.cmb_DocType2.Appearance = Appearance2
        Me.cmb_DocType2.Location = New System.Drawing.Point(386, 46)
        Me.cmb_DocType2.Name = "cmb_DocType2"
        Me.cmb_DocType2.Size = New System.Drawing.Size(196, 22)
        Me.cmb_DocType2.TabIndex = 38
        Me.cmb_DocType2.Text = "UltraCombo5"
        '
        'UltraLabel1
        '
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(330, 19)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(53, 14)
        Me.UltraLabel1.TabIndex = 35
        Me.UltraLabel1.Text = "Doc Type"
        '
        'UltraLabel13
        '
        Me.UltraLabel13.AutoSize = True
        Me.UltraLabel13.Location = New System.Drawing.Point(44, 20)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(42, 14)
        Me.UltraLabel13.TabIndex = 34
        Me.UltraLabel13.Text = "Section"
        '
        'txt_Section
        '
        Appearance3.FontData.BoldAsString = "False"
        Me.txt_Section.Appearance = Appearance3
        Me.txt_Section.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
        Me.txt_Section.Location = New System.Drawing.Point(89, 19)
        Me.txt_Section.Name = "txt_Section"
        Me.txt_Section.Size = New System.Drawing.Size(192, 17)
        Me.txt_Section.TabIndex = 23
        Me.txt_Section.Text = "UltraTextEditor1"
        '
        'cmb_DocType
        '
        Appearance4.FontData.BoldAsString = "False"
        Me.cmb_DocType.Appearance = Appearance4
        Me.cmb_DocType.Location = New System.Drawing.Point(386, 16)
        Me.cmb_DocType.Name = "cmb_DocType"
        Me.cmb_DocType.Size = New System.Drawing.Size(196, 22)
        Me.cmb_DocType.TabIndex = 21
        Me.cmb_DocType.Text = "UltraCombo5"
        '
        'UltraTabControl3
        '
        Me.UltraTabControl3.Controls.Add(Me.UltraTabSharedControlsPage3)
        Me.UltraTabControl3.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraTabControl3.Location = New System.Drawing.Point(0, 84)
        Me.UltraTabControl3.Name = "UltraTabControl3"
        Me.UltraTabControl3.SharedControlsPage = Me.UltraTabSharedControlsPage3
        Me.UltraTabControl3.Size = New System.Drawing.Size(614, 98)
        Me.UltraTabControl3.TabIndex = 60
        UltraTab2.TabPage = Me.UltraTabPageControl3
        UltraTab2.Text = "CDN Orig Filter"
        Me.UltraTabControl3.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab2})
        '
        'UltraTabSharedControlsPage3
        '
        Me.UltraTabSharedControlsPage3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage3.Name = "UltraTabSharedControlsPage3"
        Me.UltraTabSharedControlsPage3.Size = New System.Drawing.Size(610, 72)
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 182)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(614, 97)
        Me.UltraTabControl1.TabIndex = 61
        UltraTab3.TabPage = Me.UltraTabPageControl1
        UltraTab3.Text = "Type Filter"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab3})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(610, 71)
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(610, 77)
        '
        'UltraTabControl2
        '
        Me.UltraTabControl2.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl2.Name = "UltraTabControl2"
        Me.UltraTabControl2.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.UltraTabControl2.Size = New System.Drawing.Size(200, 100)
        Me.UltraTabControl2.TabIndex = 0
        '
        'UltraTabControl4
        '
        Me.UltraTabControl4.Controls.Add(Me.UltraTabSharedControlsPage4)
        Me.UltraTabControl4.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraTabControl4.Location = New System.Drawing.Point(0, 279)
        Me.UltraTabControl4.Name = "UltraTabControl4"
        Me.UltraTabControl4.SharedControlsPage = Me.UltraTabSharedControlsPage4
        Me.UltraTabControl4.Size = New System.Drawing.Size(614, 96)
        Me.UltraTabControl4.TabIndex = 62
        UltraTab1.TabPage = Me.UltraTabPageControl2
        UltraTab1.Text = "Outer Filter"
        Me.UltraTabControl4.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1})
        '
        'UltraTabSharedControlsPage4
        '
        Me.UltraTabSharedControlsPage4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage4.Name = "UltraTabSharedControlsPage4"
        Me.UltraTabSharedControlsPage4.Size = New System.Drawing.Size(610, 70)
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnDeleteGSTRSec)
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 377)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(614, 39)
        Me.Panel4.TabIndex = 63
        '
        'btnSave
        '
        Appearance6.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance6
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(350, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 39)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance7.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance7
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(438, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 39)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance8.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance8
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(526, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 39)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'btnDeleteGSTRSec
        '
        Appearance5.BackColor = System.Drawing.Color.LightCoral
        Appearance5.FontData.BoldAsString = "True"
        Me.btnDeleteGSTRSec.Appearance = Appearance5
        Me.btnDeleteGSTRSec.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteGSTRSec.Location = New System.Drawing.Point(0, 0)
        Me.btnDeleteGSTRSec.Name = "btnDeleteGSTRSec"
        Me.btnDeleteGSTRSec.Size = New System.Drawing.Size(88, 39)
        Me.btnDeleteGSTRSec.TabIndex = 13
        Me.btnDeleteGSTRSec.Text = "Delete"
        Me.btnDeleteGSTRSec.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'frmGSTRSection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "GSTRSection"
        Me.ClientSize = New System.Drawing.Size(614, 416)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.UltraTabControl4)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.UltraTabControl3)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmGSTRSection"
        Me.Text = "GSTRSection"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txt_ReturnType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_DocType2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Section, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_DocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl3.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl4.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_DocType2 As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_Section As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmb_DocType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_CDNOrigFilter As ctlFlexEditorScintilla
    Friend WithEvents UltraTabControl3 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage3 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents txt_TypeFilter As ctlFlexEditorScintilla
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabControl2 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabControl4 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage4 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents txt_OuterFilter As ctlFlexEditorScintilla
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents txt_ReturnType As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents btnDeleteGSTRSec As Infragistics.Win.Misc.UltraButton
End Class
