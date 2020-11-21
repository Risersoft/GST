Imports risersoft.shared.Extensions
Imports risersoft.app.mxent

Public Class frmEwayBillPartB
    Inherits frmMax
    Friend fMat As frmEwayBill

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.InitForm()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub InitForm()

    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        myWinSQL.AssignCmb(fMat.dsCombo, "TransMode", "", Me.cmb_TransMode)
        myWinSQL.AssignCmb(fMat.dsCombo, "VehicleType", "", Me.cmb_VehicleType)
        myWinSQL.AssignCmb(fMat.dsCombo, "StateCode", "", Me.cmb_FromState)
        myWinSQL.AssignCmb(fMat.dsCombo, "ReasonCode", "", Me.cmb_ReasonCode)
        If Me.GetSoftData(oView, prepMode, prepIDX) Then
            PrepForm = True
        End If
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click, btnCancel.Click
        Me.InitError()
        If myUtils.NullNot(Me.cmb_TransMode.Value) Then WinFormUtils.AddError(Me.cmb_TransMode, "Select a Transportation Mode")
        If myUtils.NullNot(Me.cmb_FromState.Value) Then WinFormUtils.AddError(Me.cmb_FromState, "Select From State")
        If myUtils.NullNot(Me.cmb_ReasonCode.Value) Then WinFormUtils.AddError(Me.cmb_ReasonCode, "Select Reason Code")

        If Not myUtils.NullNot(Me.cmb_TransMode.Value) Then
            If myRow("TransMode") = 1 Then
                If myUtils.NullNot(Me.cmb_VehicleType.Value) Then WinFormUtils.AddError(Me.cmb_VehicleType, "Select a Vehicle Type")
                If myUtils.NullNot(Me.txt_VehicleNo.Value) Then WinFormUtils.AddError(Me.txt_VehicleNo, "Required Vehicle No.")
            Else
                If myUtils.NullNot(Me.txt_TransDocNo.Value) Then WinFormUtils.AddError(Me.txt_TransDocNo, "Required Transport Document Number")
            End If
        End If

        If Me.CanSave Then
            cm.EndCurrentEdit()
            Me.SaveSoftData()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

    End Sub

End Class