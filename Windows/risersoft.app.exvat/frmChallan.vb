Imports risersoft.app.mxform
Imports risersoft.app.mxform.gst

Public Class frmChallan
    Inherits frmMax
    Private Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmChallanModel = Me.InitData("frmChallanModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            Me.lblInsReq.Text = myUtils.cStrTN(myWinSQL2.ParamValue("@InsReq", Me.Model.ModelParams))

            If myUtils.cValTN(myRow("DispatchID")) > 0 Then
                Me.txtTakenAmtCurr.Value = myUtils.cStrTN(myWinSQL2.ParamValue("@TakenAmtCurr", Me.Model.ModelParams))
                Me.txtTakenAmtOther.Value = myUtils.cStrTN(myWinSQL2.ParamValue("@TakenAmtOther", Me.Model.ModelParams))
                Me.txtDeducAmtOther.Value = myUtils.cStrTN(myWinSQL2.ParamValue("@DeducAmtOther", Me.Model.ModelParams))
            Else
                PanelAmt.Visible = False
            End If

            If myUtils.cStrTN(myRow("ChallanNum")).Trim.Length > 0 Then
                btnOK.Enabled = False
                btnSave.Enabled = False
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myWinSQL.AssignCmb(Me.dsCombo, "TaxInvoiceType", "", Me.cmb_TaxInvoiceType)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        CalcAmount()
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

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        If Me.txt_InsureDecl.Text.Trim.Length = 0 Then Me.txt_InsureDecl.Text = "National Insurance Company Limited, Declaration No. "
    End Sub

    Private Sub txt_AccessAmount_Leave(sender As Object, e As EventArgs) Handles txt_AccessAmount.Leave
        CalcAmount()
    End Sub

    Private Sub CalcAmount()
        If myUtils.cValTN(myRow("DispatchID")) = 0 Then
            txt_AccessAmount.Value = 0
        End If
        txt_FinalAmount.Value = myUtils.cValTN(txt_AmountTot.Value) - myUtils.cValTN(txt_AccessAmount.Value)
    End Sub

    Private Sub btnGenChallan_Click(sender As Object, e As EventArgs) Handles btnGenChallan.Click
        If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanNum")), "") Then
            If myUtils.cValTN(myRow("CheckedByID")) > 0 Then
                Dim oRet As clsProcOutput = Me.GenerateDataOutput("genaratechallanno", myRow.Row.Table.DataSet, 0)
                If oRet.Success Then
                    txt_ChallanNum.Text = oRet.Description.Trim
                End If
            Else
                MsgBox("'Sign As Checked' can not be blank.", MsgBoxStyle.Information, myWinApp.Vars("appname"))
            End If
        End If
    End Sub
End Class
