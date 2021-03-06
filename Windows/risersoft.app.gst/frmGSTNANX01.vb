﻿Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports risersoft.API.GSTN.GSTR1
Imports Newtonsoft.Json.Serialization
Imports risersoft.app.mxform
Imports risersoft.API.GSTN
Imports System.Text
Imports risersoft.app.mxform.gst
Imports System.Configuration

Public Class frmGSTNANX01
    Inherits frmMax
    Dim myViewGSTReg, myViewHistory, myViewSummary As New clsWinView
    Dim f As New frmApiTaskStatus

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridSumm)
        myViewGSTReg.SetGrid(Me.UltraGridGSTReg)
        myViewHistory.SetGrid(Me.UltraGridTaskHistory)
        myViewSummary.SetGrid(Me.UltraGridSummary)

        OpenFileDialog1.FileName = ""
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
        Dim objModel As frmGSTNANX01Model = Me.InitData("frmGSTNANX01Model", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            UltraGridGSTReg.Text = myUtils.cStrTN(myRow("CompName"))

            If myUtils.NullNot(cmb_ReturnPeriodID.Value) Then
                Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(Me.Controller, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))
                cmb_ReturnPeriodID.Value = ReturnPeriodID
            End If

            btnUpdate.Visible = False

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Invoice"))
            myViewGSTReg.PrepEdit(Me.Model.GridViews("GSTReg"))
            myViewHistory.PrepEdit(Me.Model.GridViews("History"))
            myWinSQL.AssignCmb(Me.Model.dsCombo, "PostPeriod", "", Me.cmb_ReturnPeriodID)
            myWinSQL.AssignCmb(Me.Model.dsCombo, "GstReg", "", Me.cmb_GstRegID)
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

    Private Sub UltraGridGSTReg_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridGSTReg.AfterRowActivate
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            myView.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            Me.cmb_GstRegID.Value = myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
        End If
    End Sub

    Private Sub SyncDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SyncDataToolStripMenuItem.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim oRet = GetParams("upload", "UM")
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub

    Private Sub DownloadSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadSummaryToolStripMenuItem.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim oRet = GetParams("summary", "")
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub

    Private Sub SubmitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubmitToolStripMenuItem.Click
        Dim oRet = GetParams("submit", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CompanyID", myUtils.cValTN(myRow("CompanyID")), GetType(Integer), False))

            Dim oRet = Me.GenerateParamsOutput("signdata", Params)
            Dim cert = DSCUtils.getCertificate()
            Dim toBeSigned As String = oRet.Description
            Dim byt = Encoding.UTF8.GetBytes(toBeSigned)
            Dim sign = Convert.ToBase64String(DSCUtils.SignCms(byt, cert))
            Params.Add(New clsSQLParam("@sign", sign, GetType(String), False))
            oRet = Me.GenerateParamsOutput("filesign", Params)
            MsgBox(oRet.Message)
        End If
    End Sub

    Private Sub CleanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CleanToolStripMenuItem.Click
        Dim oRet = GetParams("clean", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oRet = GetParams("excel", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub PDFFileInGSTNFormatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFFileInGSTNFormatToolStripMenuItem.Click
        Dim oRet = GetParams("pdf", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub InvoiceDefermentReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvoiceDefermentReportToolStripMenuItem.Click
        Dim oRet = GetParams("excel", "defer")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub ConsolidatedInvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsolidatedInvoiceToolStripMenuItem.Click
        Dim oRet = GetParams("template", "IS")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub GSTNErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GSTNErrorToolStripMenuItem.Click
        Dim oRet = GetParams("gstn_error", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub DeleteDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteDataToolStripMenuItem.Click
        Dim oRet = GetParams("delete", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub DeleteCounterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteCounterToolStripMenuItem.Click
        Dim oRet = GetParams("deletecp", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub DeleteImportedOnlineDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteImportedOnlineDataToolStripMenuItem.Click
        Dim oRet = GetParams("deleteimportonline", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub NewModifiedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewModifiedToolStripMenuItem.Click
        Dim oRet = GetParams("payload", "UM")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim oRet = GetParams("payload", "DEL")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub reBindView(ds As DataSet)
        myViewGSTReg.mainGrid.BindView(ds,, 0)
        myViewGSTReg.mainGrid.Genwidth(True)
        myView.mainGrid.BindView(ds,, 1)
        myView.mainGrid.Genwidth(True)
        myViewHistory.mainGrid.BindView(ds,, 3)
        myViewHistory.mainGrid.Genwidth(True)
        myViewSummary.mainGrid.BindView(ds,, 4)
        myViewSummary.mainGrid.Genwidth(True)
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

    Private Sub btnInvoiceSeries_Click(sender As Object, e As EventArgs) Handles btnInvoiceSeries.Click
        'Dim f As New frmGstDocNumSeries
        'f.fMat = Me
        'If f.PrepForm(Me.myView, EnumfrmMode.acEditM, frmIDX) Then f.ShowDialog()
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Me.cmb_ReturnPeriodID.ReadOnly = False
        btnUpdate.Visible = True
        btnCancel1.Visible = True
        btnChange.Visible = False
    End Sub

    Private Function GetParams(Key As String, UploadType As String)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
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

End Class
