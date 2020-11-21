Imports Infragistics.Win.UltraWinEditors
Imports Infragistics.Win.Misc
Imports Infragistics.Win.UltraWinTabControl
Imports risersoft.shared.Extensions
Imports risersoft.app.mxent

Public Class frmGstPartySelect
    Inherits frmMax
    Protected Friend fp As frmMax
    Dim PartyType As String, dvState As DataView

    Private Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel)
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myWinSQL.AssignCmb(NewModel.dsCombo, "CO", "", Me.cmb_SelCountry, "<STRWIDTH>0-0-1-3</STRWIDTH>")
        dvState = myWinSQL.AssignCmb(NewModel.dsCombo, "SU", "", Me.cmb_SelState, "<STRWIDTH>0-0-0-1-3</STRWIDTH>", 2)
        dvState.RowFilter = ""

        Me.cmb_ITCInElg.ValueList = NewModel.ValueLists("ITCInElg").ComboList
        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxAreaCode", "", Me.cmb_TaxAreaID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "GstRegType", "", Me.cmb_GstRegType)


        Return True
    End Function

    Public Overrides Function PrepFormrow(r1 As DataRow) As Boolean
        Me.FormPrepared = False

        If (Not r1 Is Nothing) Then
            If Me.BindModel(fp.Model) Then
                PartyType = myUtils.cStrTN(fp.myRow("partytype")).Trim.ToUpper
                For Each str1 As String In New String() {"V", "C", "F"}
                    If Me.UltraTabControl1.Tabs.Exists(str1) Then Me.UltraTabControl1.Tabs(str1).Visible = False
                Next
                Select Case PartyType
                    Case "V"
                        Me.txt_Code.Name = "txt_VendorCode"

                    Case "C"
                        Me.txt_Code.Name = "txt_CustomerCode"

                    Case "F"
                        Me.txt_Code.Name = "txt_FICode"

                    Case Else
                        Me.UltraPanelSub.Visible = False
                End Select

                If Me.UltraTabControl1.Tabs.Exists(PartyType) Then Me.UltraTabControl1.Tabs(PartyType).Visible = True

                If Me.InitData(r1) Then
                    Me.Text = "Selectable Party for " & CType(fp, frmGstPartyMain).txt_MPName.Text
                    Me.UltraTabControl1.Tabs(0).Selected = True   'initially visible
                    Me.FormPrepared = True
                End If
            End If
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If myUtils.NullNot(cmb_TaxAreaID.Value) Then WinFormUtils.AddError(cmb_TaxAreaID, "Please Select Tax Area Code")
        If Me.CanSave Then
            If Me.cmb_SelCountry.SelectedRow Is Nothing Then myRow("selcountrycode") = "" Else myRow("selcountrycode") = Me.cmb_SelCountry.SelectedRow.Cells("countrycode").Value
            If Me.cmb_SelState.SelectedRow Is Nothing Then myRow("selstatecode") = "" Else myRow("selstatecode") = Me.cmb_SelState.SelectedRow.Cells("subdivisioncode").Value
            Me.DialogResult = DialogResult.OK
            VSave = True
        End If
    End Function

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyAdd.Click
        Dim btn As UltraButton = CType(sender, UltraButton)
        Dim rr() As DataRow

        'TODO: Apply party filters.
        Select Case LCase(btn.Name)
            Case "btncopyadd"
                myFuncsBase.CopyAdd(Me.Controller, fp.myRow.Row, myRow.Row)

        End Select
    End Sub

    Private Sub cmb_SelCountry_ValueChanged(sender As Object, e As EventArgs) Handles cmb_SelCountry.ValueChanged
        If Me.FormPrepared Then
            Dim str1 As String = ""
            If Not Me.cmb_SelCountry.SelectedRow Is Nothing Then str1 = Me.cmb_SelCountry.SelectedRow.Cells("countrycode").Value
            dvState.RowFilter = "countrycode='" & str1 & "'"
        End If
    End Sub
End Class