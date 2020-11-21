Imports System.Windows.Forms
Imports risersoft.API.GSTN.GSTR1
Imports Newtonsoft.Json.Serialization
Imports risersoft.API.GSTN
Imports System.Text
Imports risersoft.app.mxform.gst
Imports System.Configuration

Public Class frmGSTNEWB
    Inherits frmMax
    Dim myViewGSTReg, myViewHistory As New clsWinView
    Dim f As New frmApiTaskStatus

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridSummary)
        myViewGSTReg.SetGrid(Me.UltraGridGSTReg)
        myViewHistory.SetGrid(Me.UltraGridTaskHistory)

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
        Dim objModel As frmGSTNEWBModel = Me.InitData("frmGSTNEWBModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            UltraGridGSTReg.Text = myUtils.cStrTN(myRow("CompName"))

            Dim dt As DataTable = Me.Model.DatasetCollection("ds").Tables("return")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Me.cmb_ReturnPeriodID.Value = dt.Rows(0)("PostPeriodID")
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

    Private Sub UltraGridGSTReg_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridGSTReg.AfterRowActivate
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            myView.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            Me.cmb_GstRegID.Value = myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
        End If
    End Sub

    Private Sub GenerateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateToolStripMenuItem.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim oret = GetParams("generate", "")
            MsgBox(oret.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        Else
            MsgBox("Please Select GSTIN")
        End If
    End Sub

    Private Sub ConsolidatedEWAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsolidatedEWAToolStripMenuItem.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim oret = GetParams("template", "")
            MsgBox(oret.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        Else
            MsgBox("Please Select GSTIN")
        End If
    End Sub

    Private Sub GSTNErrorReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GSTNErrorReportToolStripMenuItem.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim oRet = GetParams("gstn_error", "")
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        Else
            MsgBox("Please Select GSTIN")
        End If
    End Sub

    Private Sub PerformReconciliationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PerformReconciliationToolStripMenuItem.Click
        Dim oret = GetParams("reconcile", "")
        MsgBox(oret.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub GenerateReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateReportToolStripMenuItem.Click
        Dim oret = GetParams("generatereport", "")
        MsgBox(oret.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub reBindView(ds As DataSet)
        myViewGSTReg.mainGrid.BindView(ds,, 0)
        myViewGSTReg.mainGrid.Genwidth(True)
        myView.mainGrid.BindView(ds,, 1)
        myView.mainGrid.Genwidth(True)
        myViewHistory.mainGrid.BindView(ds,, 3)
        myViewHistory.mainGrid.Genwidth(True)
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
