Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform.gst
Imports risersoft.shared.Extensions
Imports uwg = Infragistics.Win.UltraWinGrid

Public Class frmGstValidationRule
    Inherits frmMax

    Private Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGstValidationRuleModel = Me.InitData("frmGstValidationRuleModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myWinSQL.AssignCmb(NewModel.dsCombo, "DocType", "", Me.cmb_DocType)
            myWinSQL.AssignCmb(NewModel.dsCombo, "RuleType", "", Me.cmb_RuleType)
            myWinSQL.AssignCmb(NewModel.dsCombo, "RuleLevel", "", Me.cmb_RuleLevel)
            myWinSQL.AssignCmb(NewModel.dsCombo, "ResultType", "", Me.cmb_ResultType)
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

    Private Sub btnDeleteValidRule_Click(sender As Object, e As EventArgs) Handles btnDeleteValidRule.Click
        If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
            Me.DeleteEntity("rule", frmIDX, True)
        End If
        WinFormUtils.ButtonAction(Me, "btnOK")
    End Sub

End Class