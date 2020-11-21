Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmGstImportEWB
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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnGeneratePayload = New Infragistics.Win.Misc.UltraButton()
        Me.btnOpen = New Infragistics.Win.Misc.UltraButton()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 511)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(760, 36)
        Me.Panel4.TabIndex = 1
        '
        'btnSave
        '
        Appearance1.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance1
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(496, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 36)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance2.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance2
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(584, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 36)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance3.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance3
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(672, 0)
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnGeneratePayload)
        Me.Panel1.Controls.Add(Me.btnOpen)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(760, 100)
        Me.Panel1.TabIndex = 2
        '
        'btnGeneratePayload
        '
        Appearance4.FontData.BoldAsString = "True"
        Me.btnGeneratePayload.Appearance = Appearance4
        Me.btnGeneratePayload.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnGeneratePayload.Location = New System.Drawing.Point(88, 0)
        Me.btnGeneratePayload.Name = "btnGeneratePayload"
        Me.btnGeneratePayload.Size = New System.Drawing.Size(88, 100)
        Me.btnGeneratePayload.TabIndex = 9
        Me.btnGeneratePayload.Text = "&Generate Payload"
        '
        'btnOpen
        '
        Appearance5.FontData.BoldAsString = "True"
        Me.btnOpen.Appearance = Appearance5
        Me.btnOpen.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnOpen.Location = New System.Drawing.Point(0, 0)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(88, 100)
        Me.btnOpen.TabIndex = 8
        Me.btnOpen.Text = "&Import Counter Party"
        '
        'UltraGrid1
        '
        Me.UltraGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 100)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(760, 411)
        Me.UltraGrid1.TabIndex = 3
        Me.UltraGrid1.Text = "Campus"
        '
        'frmGSTNEWB
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "GSTN EWB Import/Export"
        Me.ClientSize = New System.Drawing.Size(760, 547)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGSTNEWB"
        Me.Text = "GSTN EWB Import/Export"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents UltraGrid1 As UltraGrid
    Friend WithEvents btnOpen As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnGeneratePayload As Infragistics.Win.Misc.UltraButton

#End Region
End Class

