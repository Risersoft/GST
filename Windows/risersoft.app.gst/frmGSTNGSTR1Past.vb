Imports System.Windows.Forms
Imports risersoft.API.GSTN.GSTR2
Imports Newtonsoft.Json.Serialization
Imports risersoft.app.mxform
Imports risersoft.app.mxform.gst
Imports System.Configuration

Public Class frmGSTNGSTR1Past
    Inherits frmMax
    Dim myViewGSTReg, myViewHistory, myViewB2CS, myViewNil, myViewHSN, myViewAdvance As New clsWinView
    Dim f As New frmApiTaskStatus

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridSumm)
        myViewGSTReg.SetGrid(Me.UltraGridGSTReg)
        myViewHistory.SetGrid(Me.UltraGridTaskHistory)
        myViewB2CS.SetGrid(Me.UltraGridB2CS)
        myViewNil.SetGrid(Me.UltraGridNil)
        myViewHSN.SetGrid(Me.UltraGridHSN)
        myViewAdvance.SetGrid(Me.UltraGridAdvance)

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
        Dim objModel As frmGSTNGSTR1PastModel = Me.InitData("frmGSTNGSTR1PastModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            UltraGridGSTReg.Text = myUtils.cStrTN(myRow("CompName"))
            If myUtils.NullNot(cmb_ReturnPeriodID.Value) Then
                Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(Me.Controller, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))
                cmb_ReturnPeriodID.Value = ReturnPeriodID
            End If
            If myUtils.NullNot(cmb_Operation.Value) Then
                cmb_Operation.Value = "PAN"
            End If
            If myUtils.NullNot(cmb_Period.Value) Then
                cmb_Period.Value = "Selected Period"
            End If

            UltraPanel4.Visible = False
            cmb_ToID.Visible = False
            cmb_FromID.Visible = False
            UltraLabelFrom.Visible = False
            UltraLabelTo.Visible = False

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Invoice"))
            myViewGSTReg.PrepEdit(Me.Model.GridViews("GSTReg"))
            myViewHistory.PrepEdit(Me.Model.GridViews("History"))
            myViewB2CS.PrepEdit(Me.Model.GridViews("B2CS"))
            myViewNil.PrepEdit(Me.Model.GridViews("NIL"))
            myViewHSN.PrepEdit(Me.Model.GridViews("HSN"))
            myViewAdvance.PrepEdit(Me.Model.GridViews("Advance"))
            myWinSQL.AssignCmb(Me.Model.dsCombo, "PostPeriod", "", Me.cmb_ReturnPeriodID)
            myWinSQL.AssignCmb(Me.Model.dsCombo, "PostPeriod", "", Me.cmb_FromID)
            myWinSQL.AssignCmb(Me.Model.dsCombo, "PostPeriod", "", Me.cmb_ToID)
            myWinSQL.AssignCmb(Me.Model.dsCombo, "GstReg", "", Me.cmb_GstRegID)
            Me.cmb_Operation.ValueList = Me.Model.ValueLists("Operation").ComboList
            Me.cmb_Period.ValueList = Me.Model.ValueLists("Period").ComboList

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
            myViewB2CS.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            myViewNil.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            myViewHSN.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            myViewAdvance.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
            Me.cmb_GstRegID.Value = myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
        End If
    End Sub

    Private Sub cmb_Period_Leave(sender As Object, e As EventArgs) Handles cmb_Period.Leave, cmb_Period.AfterCloseUp
        If cmb_Period.Value = "Custom" Then
            cmb_ToID.Visible = True
            cmb_FromID.Visible = True
            UltraLabelFrom.Visible = True
            UltraLabelTo.Visible = True
            cmb_ToID.ReadOnly = False
            cmb_FromID.ReadOnly = False
        End If
    End Sub

    Private Sub reBindView(ds As DataSet)
        myViewGSTReg.mainGrid.BindView(ds,, 0)
        myViewGSTReg.mainGrid.Genwidth(True)
        myView.mainGrid.BindView(ds,, 1)
        myView.mainGrid.Genwidth(True)
        myViewHistory.mainGrid.BindView(ds,, 7)
        myViewHistory.mainGrid.Genwidth(True)
        myViewB2CS.mainGrid.BindView(ds,, 2)
        myViewB2CS.mainGrid.Genwidth(True)
        myViewHSN.mainGrid.BindView(ds,, 4)
        myViewHSN.mainGrid.Genwidth(True)
        myViewNil.mainGrid.BindView(ds,, 3)
        myViewNil.mainGrid.Genwidth(True)
        myViewAdvance.mainGrid.BindView(ds,, 5)
        myViewAdvance.mainGrid.Genwidth(True)

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Me.cmb_ReturnPeriodID.Value = myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value)
        If myUtils.cValTN(cmb_ToID.Value) > 0 Then Me.cmb_ToID.Value = myUtils.cValTN(cmb_ToID.SelectedRow.Cells("PostPeriodID").Value)
        If myUtils.cValTN(cmb_FromID.Value) > 0 Then Me.cmb_FromID.Value = myUtils.cValTN(cmb_FromID.SelectedRow.Cells("PostPeriodID").Value)
        If Not Me.cmb_Operation.SelectedItem Is Nothing Then
            Dim str1 As String = Me.cmb_Operation.Value
            Me.cmb_Operation.Value = str1
        End If
        If Not Me.cmb_Period.SelectedItem Is Nothing Then
            Dim str1 As String = Me.cmb_Period.Value
            Me.cmb_Period.Value = str1
        End If

        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@CompanyID", myUtils.cValTN(myRow("CompanyID")), GetType(Integer), False))
        If myUtils.cValTN(cmb_FromID.Value) > 0 Then Params.Add(New clsSQLParam("@from", myUtils.cValTN(cmb_FromID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
        If myUtils.cValTN(cmb_ToID.Value) > 0 Then Params.Add(New clsSQLParam("@to", myUtils.cValTN(cmb_ToID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@period", "'" & myUtils.cStrTN(cmb_Period.Value) & "'", GetType(String), False))

        Dim oRet = Me.GenerateParamsOutput("dbsumm", Params)
        Me.reBindView(oRet.Data)

        UltraPanel4.Visible = False
        cmb_ReturnPeriodID.ReadOnly = True
        cmb_FromID.ReadOnly = True
        cmb_ToID.ReadOnly = True
        cmb_Operation.ReadOnly = True
        cmb_Period.ReadOnly = True

    End Sub

    Private Sub btnCancel1_Click(sender As Object, e As EventArgs) Handles btnCancel1.Click
        UltraPanel4.Visible = False
    End Sub

    Private Sub DownloadTaxpayerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadTaxpayerToolStripMenuItem.Click
        Dim oret = GetParams("downloadtp", "")
        MsgBox(oret.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub ExcelFileForDownloadPastDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelFileForDownloadPastDataToolStripMenuItem.Click
        Dim oret = GetParams("excel", "")
        MsgBox(oret.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Me.cmb_ReturnPeriodID.ReadOnly = False
        Me.cmb_Period.ReadOnly = False
        Me.cmb_Operation.ReadOnly = False
        UltraPanel4.Visible = True
    End Sub

    Private Function GetParams(Key As String, UploadType As String)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@CompanyID", myUtils.cValTN(myRow("CompanyID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@uploadtype", "'" & myUtils.cStrTN(UploadType) & "'", GetType(String), False))
        Params.Add(New clsSQLParam("@period", "'" & myUtils.cStrTN(cmb_Period.Value) & "'", GetType(String), False))
        If Me.cmb_Operation.Value = "Custom" Then
            Params.Add(New clsSQLParam("@from", myUtils.cValTN(cmb_FromID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@to", myUtils.cValTN(cmb_ToID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))
        End If

        Dim oRet = Me.GenerateParamsOutput(Key, Params)
        Dim result As Guid
        If System.Guid.TryParse(oRet.Description, result) Then
            f.AddTask(result.ToString)
        End If
        Return oRet

    End Function


End Class