Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform.gst
Imports risersoft.shared.Extensions
Imports uwg = Infragistics.Win.UltraWinGrid

Public Class frmGstPaymentCustomer
    Inherits frmMax
    Dim dvDivision, dvADVSubTyp As DataView, OrgPaymentType As String = ""
    Dim oSort As clsWinSort

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(UltraGridItemList)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGstPaymentCustomerModel = Me.InitData("frmGstPaymentCustomerModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            Me.cmb_CustomerID.Value = myRow("customerid")
            If Me.cmb_CustomerID.SelectedRow IsNot Nothing Then Me.txt_CTIN.Text = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value)

            If Me.Model.DatasetCollection.ContainsKey("OrigVouch") Then
                UltraPanelAmndment.Visible = True
                Dim dt As DataTable = Me.Model.DatasetCollection("OrigVouch").Tables(0)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblvouchnum.Text = "Document No.  " & " - " & dt.Rows(0)("vouchnum")
                    lbldated.Text = "Document Date" & " - " & dt.Rows(0)("dated")
                    lblPartyName.Text = "Party Name" & " - " & dt.Rows(0)("PartyName")
                    lblPartyGSTIN.Text = "Party GSTIN" & " - " & dt.Rows(0)("CTIN")
                    OrgPaymentType = myUtils.cStrTN(dt.Rows(0)("AdvanceVouchType"))
                    cmb_AdvanceVouchType.ReadOnly = True
                End If
            Else
                UltraPanelAmndment.Visible = False
            End If



            Me.SetControlsValue(Me.UltraTabControl1.Tabs(0).TabPage)

            oSort = New clsWinSort(myView, Nothing, Nothing, Nothing, "LineSNum")
            oSort.renumber()

            HandleCampus()
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID)
            myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)
            Me.cmb_Diff_Percent.ValueList = Me.Model.ValueLists("Diff_Percent").ComboList
            myWinSQL.AssignCmb(Me.dsCombo, "ReturnPeriod", "", Me.cmb_ReturnPeriodID)
            myWinSQL.AssignCmb(Me.dsCombo, "AdvanceVouchType", "", Me.cmb_AdvanceVouchType)
            myWinSQL.AssignCmb(Me.dsCombo, "PartyGSTReg", "", Me.cmb_PartyGSTRegType)
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

    Private Sub HandleCampus()
        dvDivision.RowFilter = FilterDivision(Me.Controller, Me.fRow, cmb_CampusID, Nothing)
        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value

    End Sub

    Public Function DivisionFilter(cmb1 As UltraCombo, cmb2 As UltraCombo) As String
        Dim CodeList As New List(Of String)
        If Not IsNothing(cmb1) AndAlso Not IsNothing(cmb1.SelectedRow) Then CodeList.AddRange(Split(myUtils.cStrTN(cmb1.SelectedRow.Cells("DivisionCodeList").Value), ","))
        If Not IsNothing(cmb2) AndAlso Not IsNothing(cmb2.SelectedRow) Then CodeList.AddRange(Split(myUtils.cStrTN(cmb2.SelectedRow.Cells("DivisionCodeList").Value), ","))
        Dim str1 As String = myUtils.MakeCSV2(",", "'0'", True, CodeList.ToArray)
        Dim str2 As String = "DivisionCode in (" & str1 & ")"

        Return str2
    End Function

    Public Function FilterDivision(Controller As clsWinController, fRow As DataRow, cmb1 As Infragistics.Win.UltraWinGrid.UltraCombo, cmb2 As Infragistics.Win.UltraWinGrid.UltraCombo) As String
        Dim str As String = "0=1"
        Dim str1 As String = Controller.AppModel.strFilterDBAppUser(Controller, fRow, "DivisionID")
        Dim str2 As String = DivisionFilter(cmb1, cmb2)
        If str1.Trim.Length > 0 OrElse str2.Trim.Length > 0 Then str = myUtils.CombineWhere(False, str1, str2)
        Return str
    End Function

    Private Sub EnableButton(Bool As Boolean)
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnAdd.Enabled = True Else btnAdd.Enabled = Bool
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnDel.Enabled = True Else btnDel.Enabled = Bool
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_CampusID.Leave, cmb_CampusID.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub cmb_PartyID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp
        If (Not IsNothing(cmb_CustomerID.SelectedRow)) Then txt_CTIN.Text = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value)
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            EnableButton(True)
            oSort.renumber()
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Count = 0 OrElse GridRowValidate() Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
            gr.Cells("LineSNum").Value = myUtils.MaxVal(myView.mainGrid.myDv.Table, "LineSNum") + 1
            myView.mainGrid.UpdateData()
            oSort.renumber()
        End If
    End Sub

    Private Sub UltraGridItemList_AfterCellUpdate(sender As Object, e As CellEventArgs) Handles UltraGridItemList.AfterCellUpdate
        If myUtils.IsInList(e.Cell.Column.Key, "I_RT", "C_RT", "S_RT", "Cess_Rt", "AD_AMT") Then
            e.Cell.Row.Cells("IAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("I_RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("AD_AMT").Value) * myUtils.cValTN(myRow("Diff_Percent")) / 10000, 2)
            e.Cell.Row.Cells("CAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("C_RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("AD_AMT").Value) * myUtils.cValTN(myRow("Diff_Percent")) / 10000, 2)
            e.Cell.Row.Cells("SAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("S_RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("AD_AMT").Value) * myUtils.cValTN(myRow("Diff_Percent")) / 10000, 2)
            e.Cell.Row.Cells("CSAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("Cess_Rt").Value) * myUtils.cValTN(e.Cell.Row.Cells("AD_AMT").Value) / 100, 2)
        End If
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If Not GridRowValidate() Then
            e.Cancel = True
        End If
    End Sub

    Private Function GridRowValidate()
        Dim ItemValidate As Boolean = True
        Dim r1 As DataRow = myWinUtils.DataRowFromGridRow(myView.mainGrid.myGrid.ActiveRow)
        If myUtils.cValTN(r1("AD_AMT")) = 0 AndAlso myUtils.cValTN(r1("RT")) = 0 Then
            ItemValidate = False
        Else
            ItemValidate = True
        End If
        Return ItemValidate
    End Function
End Class