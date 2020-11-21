Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmGSTNGSTR1old
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
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnGetAll = New Infragistics.Win.Misc.UltraButton()
        Me.btnUpload = New Infragistics.Win.Misc.UltraButton()
        Me.btnConnect = New Infragistics.Win.Misc.UltraButton()
        Me.btnFile = New Infragistics.Win.Misc.UltraButton()
        Me.btnGeneratePayload = New Infragistics.Win.Misc.UltraButton()
        Me.btnOpen = New Infragistics.Win.Misc.UltraButton()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.UltraGrid2 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.UltraGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Appearance28.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance28
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(496, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 36)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance29.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance29
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(584, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 36)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance30.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance30
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
        Me.Panel1.Controls.Add(Me.btnGetAll)
        Me.Panel1.Controls.Add(Me.btnUpload)
        Me.Panel1.Controls.Add(Me.btnConnect)
        Me.Panel1.Controls.Add(Me.btnFile)
        Me.Panel1.Controls.Add(Me.btnGeneratePayload)
        Me.Panel1.Controls.Add(Me.btnOpen)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(760, 100)
        Me.Panel1.TabIndex = 2
        '
        'btnGetAll
        '
        Appearance31.FontData.BoldAsString = "True"
        Me.btnGetAll.Appearance = Appearance31
        Me.btnGetAll.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnGetAll.Location = New System.Drawing.Point(440, 0)
        Me.btnGetAll.Name = "btnGetAll"
        Me.btnGetAll.Size = New System.Drawing.Size(88, 100)
        Me.btnGetAll.TabIndex = 13
        Me.btnGetAll.Text = "&Get All"
        '
        'btnUpload
        '
        Appearance32.FontData.BoldAsString = "True"
        Me.btnUpload.Appearance = Appearance32
        Me.btnUpload.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnUpload.Location = New System.Drawing.Point(352, 0)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(88, 100)
        Me.btnUpload.TabIndex = 12
        Me.btnUpload.Text = "&Upload"
        '
        'btnConnect
        '
        Appearance33.FontData.BoldAsString = "True"
        Me.btnConnect.Appearance = Appearance33
        Me.btnConnect.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnConnect.Location = New System.Drawing.Point(264, 0)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(88, 100)
        Me.btnConnect.TabIndex = 11
        Me.btnConnect.Text = "&Connect"
        '
        'btnFile
        '
        Appearance34.FontData.BoldAsString = "True"
        Me.btnFile.Appearance = Appearance34
        Me.btnFile.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnFile.Location = New System.Drawing.Point(176, 0)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(88, 100)
        Me.btnFile.TabIndex = 10
        Me.btnFile.Text = "&File"
        '
        'btnGeneratePayload
        '
        Appearance35.FontData.BoldAsString = "True"
        Me.btnGeneratePayload.Appearance = Appearance35
        Me.btnGeneratePayload.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnGeneratePayload.Location = New System.Drawing.Point(88, 0)
        Me.btnGeneratePayload.Name = "btnGeneratePayload"
        Me.btnGeneratePayload.Size = New System.Drawing.Size(88, 100)
        Me.btnGeneratePayload.TabIndex = 9
        Me.btnGeneratePayload.Text = "&Generate Payload"
        '
        'btnOpen
        '
        Appearance36.FontData.BoldAsString = "True"
        Me.btnOpen.Appearance = Appearance36
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
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(760, 262)
        Me.UltraGrid1.TabIndex = 3
        Me.UltraGrid1.Text = "Invoices"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGrid1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 249)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(760, 262)
        Me.Panel2.TabIndex = 4
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.UltraGrid2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 100)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(760, 149)
        Me.Panel3.TabIndex = 5
        '
        'UltraGrid2
        '
        Me.UltraGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGrid2.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid2.Name = "UltraGrid2"
        Me.UltraGrid2.Size = New System.Drawing.Size(760, 149)
        Me.UltraGrid2.TabIndex = 4
        Me.UltraGrid2.Text = "Campus"
        '
        'frmGSTNGSTR1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "GSTN GSTR1 Sync"
        Me.ClientSize = New System.Drawing.Size(760, 547)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGSTNGSTR1"
        Me.Text = "GSTN GSTR1 Sync"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.UltraGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents UltraGrid1 As UltraGrid
    Friend WithEvents btnOpen As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnGeneratePayload As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnFile As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnConnect As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnGetAll As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnUpload As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents UltraGrid2 As UltraGrid

#End Region
End Class

