Imports risersoft.API.GSTN
Imports System.Text
Imports risersoft.app.mxform.gst
Imports risersoft.API.GSTN.GSTR3B
Imports risersoft.shared.Extensions
Imports Newtonsoft.Json
Imports System.Configuration

Public Class frmGSTNGSTR3B
    Inherits frmMax
    Dim myViewGSTReg, myViewHistory, myViewITCRev, myViewPayment, myViewPaymentDetail As New clsWinView
    Dim f As New frmApiTaskStatus

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridSumm)
        myViewGSTReg.SetGrid(Me.UltraGridGSTReg)
        myViewHistory.SetGrid(Me.UltraGridTaskHistory)
        myViewITCRev.SetGrid(Me.UltraGridITCRev)
        myViewPayment.SetGrid(Me.UltraGridPayment)
        myViewPaymentDetail.SetGrid(Me.UltraGridPaymentDetail)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        OpenFileDialog1.FileName = ""
        f.AddtoTab(Me.UltraTabControl1, "status", True)
        f.SetParent(Me)
        AddHandler f.LinkClicked, Sub(s, filePath)
                                      Me.SaveFileDialog1.FileName = filePath
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
        Dim objModel As frmGSTNGSTR3BModel = Me.InitData("frmGSTNGSTR3BModel", oview, prepMode, prepIdx, strXML)
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
            myViewITCRev.PrepEdit(Me.Model.GridViews("ITCRev"), btnAddITC, btnDelITC)
            myViewPayment.PrepEdit(Me.Model.GridViews("Payment"))
            myViewPaymentDetail.PrepEdit(Me.Model.GridViews("PaymentDetail"))
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

    Private Sub UltraGridPayment_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridPayment.AfterRowActivate
        If Not IsNothing(myViewPayment.mainGrid.myGrid.ActiveRow) Then
            myViewPaymentDetail.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myViewPayment.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Dim oRet = GetParams("upload", "UM")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub PopulatePaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PopulatePaymentToolStripMenuItem.Click
        Dim oRet = GetParams("payment", "")
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

    Private Sub FinalTaxSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinalTaxSummaryToolStripMenuItem.Click
        Dim oRet = GetParams("excel", "FinalTax")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub CalculatedTaxSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculatedTaxSummaryToolStripMenuItem.Click
        Dim oRet = GetParams("excel", "CalcTax")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub PaymentTaxSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentTaxSummaryToolStripMenuItem.Click
        Dim oRet = GetParams("excel", "Payment")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub TaxSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim oRet = GetParams("excel", "Tax")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))

    End Sub

    Private Sub GenerateJSONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateJSONToolStripMenuItem.Click
        Dim oRet = GetParams("payload", "UM")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim f As New frmGSTR3B
        If f.PrepForm(Me.myView, EnumfrmMode.acEditM, frmIDX) Then f.ShowDialog()
    End Sub

    Private Sub btnCashLedger_Click(sender As Object, e As EventArgs) Handles btnCashLedger.Click
        Dim oRet = GetParams("cashdetail", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub btnITCLedger_Click(sender As Object, e As EventArgs) Handles btnITCLedger.Click
        Dim oRet = GetParams("itcdetail", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub btnLiabLedger_Click(sender As Object, e As EventArgs) Handles btnLiabLedger.Click
        Dim oRet = GetParams("libdetails", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim f As New frmPaidITC
        f.fMat = Me
        If f.PrepForm(myViewPaymentDetail, EnumfrmMode.acAddM, "") Then
            f.ShowDialog()
            Dim obj As New Pditc With {.i_pdi = f.myRow.Row("Iamt_I"), .i_pdc = f.myRow.Row("Iamt_C"), .i_pds = f.myRow.Row("Iamt_S"), .c_pdi = f.myRow.Row("Camt_I"), .c_pdc = f.myRow.Row("Camt_C"), .s_pdi = f.myRow.Row("Samt_I"), .s_pds = f.myRow.Row("Samt_S"), .cs_pdcs = f.myRow.Row("CsAmt_Cs")}
            Dim str1 As String = JsonConvert.SerializeObject(obj)
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@pditc", "'" & myUtils.cStrTN(str1) & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value), GetType(Integer), False))

            Dim oRet = Me.Model.GenerateParamsOutput("savepditc", Params)
            MsgBox(oRet.Message)

        End If
    End Sub

    Private Sub btnOffset_Click(sender As Object, e As EventArgs) Handles btnOffset.Click
        Dim oRet = GetParams("newoffset", "")
        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
    End Sub

    Private Sub btnCancel1_Click(sender As Object, e As EventArgs) Handles btnCancel1.Click
        btnUpdate.Visible = False
        btnCancel1.Visible = False
        cmb_ReturnPeriodID.ReadOnly = True
        btnChange.Visible = True
        Me.cmb_ReturnPeriodID.Value = myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value)
    End Sub

    Private Sub reBindView(ds As DataSet)
        myViewGSTReg.mainGrid.BindView(ds,, 0)
        myViewGSTReg.mainGrid.Genwidth(True)
        myView.mainGrid.BindView(ds,, 1)
        myView.mainGrid.Genwidth(True)
        myViewPayment.mainGrid.BindView(ds,, 5)
        myViewPayment.mainGrid.Genwidth(True)
        myViewPaymentDetail.mainGrid.BindView(ds,, 2)
        myViewPaymentDetail.mainGrid.Genwidth(True)
        myViewHistory.mainGrid.BindView(ds,, 4)
        myViewHistory.mainGrid.Genwidth(True)
        myViewITCRev.mainGrid.BindView(ds,, 6)
        myViewITCRev.mainGrid.Genwidth(True)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Me.cmb_ReturnPeriodID.Value = myUtils.cValTN(cmb_ReturnPeriodID.SelectedRow.Cells("PostPeriodID").Value)
        btnUpdate.Visible = False
        btnCancel1.Visible = False
        btnChange.Visible = True
        Me.cmb_ReturnPeriodID.ReadOnly = True
        Dim oRet = Me.GetParams("dbsumm", "")
        Me.reBindView(oRet.Data)
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Me.cmb_ReturnPeriodID.ReadOnly = False
        btnUpdate.Visible = True
        btnCancel1.Visible = True
        btnChange.Visible = False
    End Sub

    Private Function GetParams(Key As String, UploadType As String) As clsProcOutput
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




'****************************************************************
'Private Sub btnGeneratePayload_Click(sender As Object, e As EventArgs)
'    If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
'        Dim Params As New List(Of clsSQLParam)
'        Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
'        Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("ReturnPeriodID").Value), GetType(Integer), False))
'        Params.Add(New clsSQLParam("@uploadtype", "'UM'", GetType(String), False))

'        Dim oRet = Me.GenerateParamsOutput("payload", Params)
'        MsgBox(oRet.Message, MsgBoxStyle.Information, Me.Controller.Vars("appname"))
'    End If
'End Sub