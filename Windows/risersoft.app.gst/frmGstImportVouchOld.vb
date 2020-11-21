Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports risersoft.app.mxform.gst
Imports risersoft.app.shared
Imports risersoft.app2.shared

Public Class frmGstImportVouchOld
    Inherits frmMax

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        OpenFileDialog1.FileName = ""
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGstImportVouchNewModel = Me.InitData("frmGstImportVouchModel", oview, prepMode, prepIdx, strXML)

        If Me.BindModel(objModel, oview) Then



            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function



    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then


            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function
    Private Sub PrepViews(ds As DataSet)
        'bind view data
    End Sub
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        OpenFileDialog1.Title = "Open File"
        OpenFileDialog1.DefaultExt = "xlsx"
        OpenFileDialog1.Filter = "XLS Files|*.xls|XLSX Files|*.xlsx"
        OpenFileDialog1.FilterIndex = 2
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim f As New frmSSGImport
            If f.OpenFile(Me.OpenFileDialog1.FileName) Then
                If f.ShowDialog() = DialogResult.OK Then
                    Dim dsImport As New DataSet
                    myUtils.AddTable(dsImport, f.GenerateTable("customer"), "cust")
                    myUtils.AddTable(dsImport, f.GenerateTable("invoice"), "inv")
                    myUtils.AddTable(dsImport, f.GenerateTable("advance"), "adv")
                    Me.Model.DatasetCollection.AddUpd("import", dsImport)
                    Me.PrepViews(dsImport)
                End If
                f.CloseWB()
            End If

        End If
    End Sub
End Class