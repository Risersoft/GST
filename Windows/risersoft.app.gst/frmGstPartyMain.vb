Imports risersoft.shared.Extensions
Imports risersoft.app.mxent
Imports risersoft.app.mxform.gst
Imports risersoft.app.mxform

Public Class frmGstPartyMain
    Inherits frmMax
    Dim dvState As DataView, objModel As frmPartyMainModel

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridSel)

        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Sel"))

            myWinSQL.AssignCmb(Me.dsCombo, "CO", "", Me.cmb_Country, "<STRWIDTH>0-0-1-3</STRWIDTH>")
            dvState = myWinSQL.AssignCmb(Me.dsCombo, "SU", "", Me.cmb_State, "<STRWIDTH>0-0-0-1-3</STRWIDTH>", 2)
            dvState.RowFilter = ""

            Me.cmb_mpnick.ValueList = Me.Model.ValueLists("mpnick").ComboList

            myWinSQL.AssignCmb(Me.dsCombo, "partytype", "", Me.cmb_PartyType)
            myWinSQL.AssignCmb(Me.dsCombo, "partysubtype", "", Me.cmb_PartySubType)

            Return True
        End If
        Return False
    End Function

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        objModel = Me.InitData("frmPartyMainModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then

            Me.cmb_PartyType.Value = myUtils.cStrTN(myRow("partytype"))
            Me.cmb_PartySubType.Value = myUtils.cStrTN(myRow("partysubtype"))
            If Me.cmb_PartyType.SelectedRow IsNot Nothing Then
                If Me.cmb_PartySubType.SelectedRow Is Nothing Then
                    Me.Text = Me.cmb_PartyType.SelectedRow.Cells("Descrip").Value
                Else
                    Me.Text = Me.cmb_PartyType.SelectedRow.Cells("Descrip").Value & " " & Me.cmb_PartySubType.SelectedRow.Cells("Descrip").Value
                End If

                Me.UltraTabControl1.Tabs(0).Selected = True

                Me.FormPrepared = True
            End If
        End If
        Return Me.FormPrepared
    End Function
    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click, btnAddNew.Click, btnCreate.Click

        cm.EndCurrentEdit()
        Select Case LCase(sender.name)
            Case "btnedit"
                If Not IsNothing(myView.mainGrid.ActiveRow) Then
                    Dim nr As DataRow = myUtils.DataRowFromGridRow(myView.mainGrid.ActiveRow)

                    Me.EditParty(nr)
                End If
            Case "btnaddnew"
                Dim nr As DataRow = myTables.AddNewRow(Me.dsForm.Tables("party"))

                Me.EditParty(nr)
            Case "btncreate"
                If Me.myView.mainGrid.myGrid.Rows.Count = 0 Then
                    Dim nr As DataRow = myTables.AddNewRow(Me.dsForm.Tables("party"))

                    myFuncsBase.CopyAdd(Me.Controller, myRow.Row, nr)
                    myFuncsBase.SetPartyNameAddress(myRow.Row, Me.dsForm.Tables("party"), True)
                End If
        End Select
    End Sub


    Private Sub EditParty(rParty As DataRow)

        Dim f As New frmGstPartySelect
        f.fp = Me
        If f.PrepFormrow(rParty) Then
            f.ShowDialog()
        End If

    End Sub
    Public Overrides Function VSave() As Boolean
        Me.InitError() : WinFormUtils.InitTabBacks(Me.UltraTabControl1)
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then
            If Me.SaveModel() Then
                risersoft.app.mxform.myFuncs.RefreshPartyCombo = True
                Me.BXML = "<BROWSE KEY=""sls.party"" ID=""" & frmIDX & """/>"
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub cmb_Country_ValueChanged(sender As Object, e As EventArgs) Handles cmb_Country.ValueChanged
        If Me.FormPrepared Then
            Dim str1 As String = ""
            If Not Me.cmb_Country.SelectedRow Is Nothing Then str1 = Me.cmb_Country.SelectedRow.Cells("countrycode").Value
            dvState.RowFilter = "countrycode='" & str1 & "'"
        End If
    End Sub
End Class