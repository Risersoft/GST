Imports risersoft.app.mxform.gst
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports risersoft.shared.Extensions

Public Class frmEwayBillcopy
    Inherits frmMax

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.InitForm()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmEwayBillModel = Me.InitData("frmEwayBillModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_SupplyType)
            myWinSQL.AssignCmb(Me.dsCombo, "SubSupplyType", "", Me.cmb_SubSupplyType)
            myWinSQL.AssignCmb(Me.dsCombo, "DocumentType", "", Me.cmb_DocType)
            myWinSQL.AssignCmb(Me.dsCombo, "Transporter", "", Me.cmb_TransVendorID)
            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_fromStateCode)
            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_toStateCode)
            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_actfromStateCode)
            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_acttoStateCode)
            myWinSQL.AssignCmb(Me.dsCombo, "TransactionType", "", Me.cmb_TransactionType)

            If myUtils.cValTN(myRow("Invoiceid")) > 0 Then
                Dim dt As DataTable = Me.Model.DatasetCollection("invoice").Tables(0)
                If dt.Rows.Count > 0 Then
                    Me.txt_InvoiceNum.Text = dt.Rows(0)("InvoiceNum")
                    Me.txt_InvoiceDate.Text = dt.Rows(0)("InvoiceDate")
                End If
            Else
                Dim dt As DataTable = Me.Model.DatasetCollection("challan").Tables(0)
                If dt.Rows.Count > 0 Then
                    Me.txt_InvoiceNum.Text = dt.Rows(0)("ChNum")
                    Me.txt_InvoiceDate.Text = dt.Rows(0)("ChDt")
                End If
            End If

            If prepMode = EnumfrmMode.acEditM AndAlso Not myUtils.NullNot(myRow("ewaybillnum")) Then
                btnGenerate.Enabled = False
            End If

            'cmb_TransactionType.Value = myRow("TransactionType")


            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            Dim dt2 As DataTable = Me.Model.DatasetCollection("party").Tables(0)
            Me.txt_fromGstin.Text = myUtils.cStrTN(dt2.Rows(0)("fromGstin"))
            Me.txt_toGstin.Text = myUtils.cStrTN(dt2.Rows(0)("toGstin"))
            Me.textfromTrdName.Text = myUtils.cStrTN(dt2.Rows(0)("fromTrdName"))
            Me.textToTrdName.Text = myUtils.cStrTN(dt2.Rows(0)("toTrdName"))
            myView.PrepEdit(Me.Model.GridViews("PartB"))
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

    Private Sub cmb_TransactionType_Leave(sender As Object, e As EventArgs) Handles cmb_TransactionType.Leave, cmb_TransactionType.AfterCloseUp
        UltraTabControl3.Tabs("DISP").Visible = ((Not IsNothing(cmb_TransactionType.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cValTN(cmb_TransactionType.Value), "3", "4"))
        UltraTabControl4.Tabs("SHIP").Visible = ((Not IsNothing(cmb_TransactionType.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cValTN(cmb_TransactionType.Value), "2", "4"))
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'If Me.frmMode = EnumfrmMode.acEditM Then
        '    If myView.mainGrid.myDS.Tables(0).Select("entereddate is null").Length > 0 Then
        '        MsgBox("Transportation Details Already Exists... You need to cancel First detail", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
        '    Else
        '        Dim f As New frmEwayBillPartB
        '        f.fMat = Me
        '        If f.PrepForm(myView, EnumfrmMode.acAddM, "", "") Then f.ShowDialog()
        '    End If
        'Else
        '    MsgBox("You need to save first before proceeding ..", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
        'End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        'Dim f As New frmEwayBillPartB
        'f.fMat = Me
        'If f.PrepForm(Me.myView, EnumfrmMode.acEditM, "") Then f.ShowDialog()
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If Me.VSave() Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", Me.Model.SelectedRow("campusid")("gstregid"), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ewaybillid", frmIDX, GetType(Integer), False))
            Dim oRet = Me.GenerateParamsOutput("generate", Params)
            If oRet.Success Then
                Me.PrepForm(pView, frmMode, frmIDX, Me.Controller.Data.VarBagXML(Me.vBag))
            Else
                MsgBox(oRet.Message,, myWinApp.Vars("appname"))
            End If
        End If
    End Sub

    Private Sub btnCancelEWB_Click(sender As Object, e As EventArgs) Handles btnCancelEWB.Click
        myRow("CancelDate") = Now.Date
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Me.Controller.PrintingPress.ShowSpecPrint(myView.Model, "crpewaybill", "", frmIDX)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@ewaybillID", frmIDX, GetType(Integer), False))
        Dim rr() As DataRow = Me.AdvancedSelect("transporter", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            cmb_TransVendorID.Value = myUtils.cValTN(rr(0)("VendorID"))
        End If
    End Sub

End Class




'Imports risersoft.app.mxform.gst
'Imports risersoft.app.mxent
'Imports risersoft.app.mxform
'Imports risersoft.shared.Extensions

'Public Class frmEwayBill
'    Inherits frmMax

'    Public Sub New()
'        ' This call is required by the designer.
'        InitializeComponent()
'        Me.InitForm()
'        ' Add any initialization after the InitializeComponent() call.
'    End Sub

'    Private Sub InitForm()
'        myView.SetGrid(Me.UltraGrid1)
'        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
'    End Sub

'    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
'        Me.FormPrepared = False
'        Dim objModel As frmEwayBillModel = Me.InitData("frmEwayBillModel", oview, prepMode, prepIdx, strXML)
'        If Me.BindModel(objModel, oview) Then
'            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID)
'            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_SupplyType)
'            myWinSQL.AssignCmb(Me.dsCombo, "SubSupplyType", "", Me.cmb_SubSupplyType)
'            myWinSQL.AssignCmb(Me.dsCombo, "DocumentType", "", Me.cmb_DocType)
'            myWinSQL.AssignCmb(Me.dsCombo, "Transporter", "", Me.cmb_TransVendorID)
'            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_fromStateCode)
'            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_toStateCode)
'            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_actfromStateCode)
'            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_acttoStateCode)
'            myWinSQL.AssignCmb(Me.dsCombo, "TransactionType", "", Me.cmb_TransactionType)

'            If myUtils.cValTN(myRow("Invoiceid")) > 0 Then
'                Dim dt As DataTable = Me.Model.DatasetCollection("invoice").Tables(0)
'                If dt.Rows.Count > 0 Then
'                    Me.txt_InvoiceNum.Text = dt.Rows(0)("InvoiceNum")
'                    Me.txt_InvoiceDate.Text = dt.Rows(0)("InvoiceDate")
'                End If
'            Else
'                Dim dt As DataTable = Me.Model.DatasetCollection("challan").Tables(0)
'                If dt.Rows.Count > 0 Then
'                    Me.txt_InvoiceNum.Text = dt.Rows(0)("ChNum")
'                    Me.txt_InvoiceDate.Text = dt.Rows(0)("ChDt")
'                End If
'            End If

'            If prepMode = EnumfrmMode.acEditM AndAlso Not myUtils.NullNot(myRow("ewaybillnum")) Then
'                btnGenerate.Enabled = False
'            End If

'            'cmb_TransactionType.Value = myRow("TransactionType")


'            Me.FormPrepared = True
'        End If
'        Return Me.FormPrepared
'    End Function

'    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
'        If MyBase.BindModel(NewModel, oView) Then
'            Dim dt2 As DataTable = Me.Model.DatasetCollection("party").Tables(0)
'            Me.txt_fromGstin.Text = myUtils.cStrTN(dt2.Rows(0)("fromGstin"))
'            Me.txt_toGstin.Text = myUtils.cStrTN(dt2.Rows(0)("toGstin"))
'            Me.textfromTrdName.Text = myUtils.cStrTN(dt2.Rows(0)("fromTrdName"))
'            Me.textToTrdName.Text = myUtils.cStrTN(dt2.Rows(0)("toTrdName"))
'            myView.PrepEdit(Me.Model.GridViews("PartB"))
'            Return True
'        End If
'        Return False
'    End Function

'    Public Overrides Function VSave() As Boolean
'        Me.InitError()
'        VSave = False
'        cm.EndCurrentEdit()
'        If Me.ValidateData() Then
'            If Me.SaveModel() Then
'                Return True
'            End If
'        Else
'            Me.SetError()
'        End If
'        Me.Refresh()
'    End Function

'    Private Sub cmb_TransactionType_Leave(sender As Object, e As EventArgs) Handles cmb_TransactionType.Leave, cmb_TransactionType.AfterCloseUp
'        UltraTabControl3.Tabs("DISP").Visible = ((Not IsNothing(cmb_TransactionType.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cValTN(cmb_TransactionType.Value), "3", "4"))
'        UltraTabControl4.Tabs("SHIP").Visible = ((Not IsNothing(cmb_TransactionType.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cValTN(cmb_TransactionType.Value), "2", "4"))
'    End Sub

'    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
'        If Me.frmMode = EnumfrmMode.acEditM Then
'            If myView.mainGrid.myDS.Tables(0).Select("entereddate is null").Length > 0 Then
'                MsgBox("Transportation Details Already Exists... You need to cancel First detail", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
'            Else
'                Dim f As New frmEwayBillPartB
'                f.fMat = Me
'                If f.PrepForm(myView, EnumfrmMode.acAddM, "", "") Then f.ShowDialog()
'            End If
'        Else
'            MsgBox("You need to save first before proceeding ..", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
'        End If
'    End Sub

'    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
'        Dim f As New frmEwayBillPartB
'        f.fMat = Me
'        If f.PrepForm(Me.myView, EnumfrmMode.acEditM, "") Then f.ShowDialog()
'    End Sub

'    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
'        If Me.VSave() Then
'            Dim Params As New List(Of clsSQLParam)
'            Params.Add(New clsSQLParam("@gstregid", Me.Model.SelectedRow("campusid")("gstregid"), GetType(Integer), False))
'            Params.Add(New clsSQLParam("@ewaybillid", frmIDX, GetType(Integer), False))
'            Dim oRet = Me.GenerateParamsOutput("generate", Params)
'            If oRet.Success Then
'                Me.PrepForm(pView, frmMode, frmIDX, Me.Controller.Data.VarBagXML(Me.vBag))
'            Else
'                MsgBox(oRet.Message,, myWinApp.Vars("appname"))
'            End If
'        End If
'    End Sub

'    Private Sub btnCancelEWB_Click(sender As Object, e As EventArgs) Handles btnCancelEWB.Click
'        myRow("CancelDate") = Now.Date
'    End Sub

'    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
'        Me.Controller.PrintingPress.ShowSpecPrint(myView.Model, "crpewaybill", "", frmIDX)
'    End Sub

'    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
'        Dim Params As New List(Of clsSQLParam)
'        Params.Add(New clsSQLParam("@ewaybillID", frmIDX, GetType(Integer), False))
'        Dim rr() As DataRow = Me.AdvancedSelect("transporter", Params)
'        If Not rr Is Nothing AndAlso rr.Length > 0 Then
'            cmb_TransVendorID.Value = myUtils.cValTN(rr(0)("VendorID"))
'        End If
'    End Sub

'End Class