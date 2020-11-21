Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform.gst
Imports System.Configuration

Public Class frmGstDocNumSeries
    Inherits frmMax
    Friend fMat As frmGSTNGSTR1
    Dim myViewInvoiceSeries As New clsWinView
    Dim f As New frmApiTaskStatus

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridGSTReg)
        myViewInvoiceSeries.SetGrid(Me.UltraGridNoSeries)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        f.AddtoTab(Me.UltraTabControl1, "status", True)
        f.SetParent(Me)
        AddHandler f.LinkClicked, Sub(s, filePath)
                                      Me.SaveFileDialog1.FileName = System.IO.Path.GetFileName(filePath)
                                      Me.GetExtensionName()
                                      If Me.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                          Dim client = Me.Controller.App.objAppExtender.FileProviderClient(Me.Controller, ConfigurationManager.AppSettings("StorageContainerName"))
                                          client.DownloadFile(filePath, Me.SaveFileDialog1.FileName)
                                      End If
                                  End Sub
    End Sub

    Public Function GetExtensionName()
        Dim FileExtCode = System.IO.Path.GetExtension(myUtils.cStrTN(Me.SaveFileDialog1.FileName))
        Dim FileExt = myWinFtp.FileExtText(FileExtCode)
        Dim strFilter As String = FileExt & " (*" & FileExtCode & ")|*" & FileExtCode
        Me.SaveFileDialog1.Filter = strFilter
    End Function

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGstDocNumSeriesModel = Me.InitData("frmGstDocNumSeriesModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            UltraGridGSTReg.Text = myUtils.cStrTN(myRow("CompName"))
            If myUtils.NullNot(cmb_ReturnPeriodID.Value) Then
                Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(Me.Controller, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))
                cmb_ReturnPeriodID.Value = ReturnPeriodID
            End If
            Me.txt_CompName.Text = fMat.myRow("CompName")

            btnUpdate.Visible = False

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("GSTReg"))
            myViewInvoiceSeries.PrepEdit(Me.Model.GridViews("Series"),, btnDelSeries)
            myWinSQL.AssignCmb(Me.Model.dsCombo, "PostPeriod", "", Me.cmb_ReturnPeriodID)

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

    Private Sub UltraGridGSTReg_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridGSTReg.AfterRowActivate
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            myViewInvoiceSeries.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
        End If
    End Sub

    Private Sub reBindView(ds As DataSet)
        myViewInvoiceSeries.mainGrid.BindView(ds,, 1)
        myViewInvoiceSeries.mainGrid.Genwidth(True)
        myView.mainGrid.BindView(ds,, 0)
        myView.mainGrid.Genwidth(True)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        btnUpdate.Visible = False
        btnCancel1.Visible = False
        btnChange.Visible = True
        cmb_ReturnPeriodID.ReadOnly = True
        Dim oRet = Me.GetParams("dbsumm", "")
        Me.reBindView(oRet.Data)
    End Sub

    Private Sub btnCancel1_Click(sender As Object, e As EventArgs) Handles btnCancel1.Click
        btnUpdate.Visible = False
        btnCancel1.Visible = False
        cmb_ReturnPeriodID.ReadOnly = True
        btnChange.Visible = True
        Me.cmb_ReturnPeriodID.Value = myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value)
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Me.cmb_ReturnPeriodID.ReadOnly = False
        btnUpdate.Visible = True
        btnCancel1.Visible = True
        btnChange.Visible = False
    End Sub

    Private Sub btnAuto_Click(sender As Object, e As EventArgs) Handles btnAuto.Click
        Dim oret = GetParams("auto", "")
        If oret.success Then
            For Each dt1 As DataTable In oret.Data.Tables
                myUtils.DeleteRows(myViewInvoiceSeries.mainGrid.myDS.Tables(0).Select())      'Delete existing rows so that new rows may be added
                For Each r1 As DataRow In dt1.Select()
                    Dim nr As DataRow = myUtils.CopyOneRow(r1, myViewInvoiceSeries.mainGrid.myDS.Tables(0))
                Next
            Next
        End If

    End Sub

    Private Sub btnRecalculate_Click(sender As Object, e As EventArgs) Handles btnRecalculate.Click
        Dim oret = GetParams("recalculate", "")
        If oret.success Then
            MsgBox("Recalculated", MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub

    Private Function GetParams(Key As String, UploadType As String)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@CompanyID", myUtils.cValTN(myRow("CompanyID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@uploadtype", "'" & myUtils.cStrTN(UploadType) & "'", GetType(String), False))

        Dim oRet = Me.GenerateParamsOutput(Key, Params)
        Dim result As Guid
        If System.Guid.TryParse(oRet.Description, result) Then
            f.AddTask(result.ToString)
        End If
        Return oRet

    End Function

    Private Sub btnAddSeries_Click(sender As Object, e As EventArgs) Handles btnAddSeries.Click
        Dim gr As UltraGridRow
        gr = myViewInvoiceSeries.mainGrid.ButtonAction("add")
        If myUtils.cValTN(cmb_ReturnPeriodID.Value) > 0 Then gr.Cells("ReturnPeriodID").Value = myUtils.cValTN(cmb_ReturnPeriodID.Value)
        If Not myView.mainGrid.myGrid.ActiveRow Is Nothing Then gr.Cells("GstRegID").Value = myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
        myViewInvoiceSeries.mainGrid.UpdateData()
    End Sub

End Class
