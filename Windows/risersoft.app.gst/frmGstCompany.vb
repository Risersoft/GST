Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports risersoft.app.mxform.gst
Imports risersoft.app.mxform
Imports Newtonsoft.Json
Imports risersoft.API.GSTN.Public
Imports risersoft.shared.cloud

Public Class frmGstCompany
    Inherits frmMax
    Dim dvState As DataView
    Dim myVueCampus As New clsWinView
    Private Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        myVueCampus.SetGrid(Me.UltraGrid2)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGstCompanyModel = Me.InitData("frmGstCompanyModel", oView, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oView) Then

            If frmMode = EnumfrmMode.acEditM Then
                If Not myUtils.NullNot(myRow("tclogo")) Then
                    WinFormUtils.FillPB(Me.pic_Logo, myRow("tclogo"))
                    Me.pic_Logo.rePicsize(0)
                End If
            End If

            If myVueCampus.mainGrid.myDS.Tables(0).Select.Length = 0 Then
                Me.cmb_HOCampusID.Visible = False
                Label1.Visible = False
            End If

            btnSearch.Enabled = False

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub PopulateCampus()
        Dim oRet As clsProcOutput = Me.GenerateIDOutput("generate", myUtils.cValTN(frmIDX))
        If oRet.Success Then
            Me.UpdateViewData(myVueCampus, oRet)
        Else
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
        myWinSQL.AssignCmb(oRet.Data, "", "", Me.cmb_HOCampusID)
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myVueCampus.PrepEdit(Me.Model.GridViews("Campus"))
            myView.PrepEdit(Me.Model.GridViews("GstReg"), Me.btnAddReg, Me.btnDelReg)
            myWinSQL.AssignCmb(Me.dsCombo, "CO", "", Me.cmb_Country, "<STRWIDTH>0-0-1-3</STRWIDTH>")
            dvState = myWinSQL.AssignCmb(Me.dsCombo, "SU", "", Me.cmb_State, "<STRWIDTH>0-0-0-1-3</STRWIDTH>", 2)
            dvState.RowFilter = ""
            myWinSQL.AssignCmb(myVueCampus.mainGrid.myDS, "", "", Me.cmb_HOCampusID)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then
            If hasNewImg Then WinFormUtils.FillDSFromPB(myRow.Row, "picperson", Me.pic_Logo)
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub btnBrowsePic_Click(sender As Object, e As EventArgs) Handles btnBrowsePic.Click
        If Me.OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Me.pic_Logo.Image = System.Drawing.Image.FromFile(Me.OpenFileDialog1.FileName)
            Me.pic_Logo.rePicsize(0)
            hasNewImg = True
        End If
    End Sub

    Private Sub btnSavepic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavepic.Click
        If Me.SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Me.pic_Logo.Image.Save(Me.SaveFileDialog1.FileName)
        End If
    End Sub

    Private Sub cmb_Country_ValueChanged(sender As Object, e As EventArgs) Handles cmb_Country.ValueChanged
        If Me.FormPrepared Then
            Dim str1 As String = ""
            If Not Me.cmb_Country.SelectedRow Is Nothing Then str1 = Me.cmb_Country.SelectedRow.Cells("countrycode").Value
            dvState.RowFilter = "countrycode='" & str1 & "'"
        End If
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        If Me.VSave Then
            Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myVueCampus, "add", GetType(frmGstCampus), "CampusID", "<PARAMS COMPANYID=""" & frmIDX & """/>")
            If Not nr Is Nothing Then Me.PopulateCampus()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myVueCampus, "edit", GetType(frmGstCampus), "CampusID", "")
        If Not nr Is Nothing Then Me.PopulateCampus()
    End Sub

    Private Sub UltraGrid1_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGrid1.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = myWinUtils.DataRowFromGridRow(myView.mainGrid.myGrid.ActiveRow)
        btnSearch.Enabled = True

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstin", "'" & myUtils.cStrTN(myView.mainGrid.myGrid.ActiveRow.Cells("GSTIN").Value) & "'", GetType(String), False))

            Dim oRet = Me.GenerateParamsOutput("search", Params)
            Dim obj = oRet.JsonData
            If oRet.Success AndAlso obj.success Then
                Dim model = JsonConvert.DeserializeObject(Of TaxPayerModel)(obj.Data)
                Dim oProc As New clsPOCOConverter(Me.Controller)
                Dim dt1 = oProc.GenerateRow(model, False, "").Table
                Dim f As New frmGrid
                f.myView.mainGrid.BindGridData(dt1.DataSet)
                If f.PrepForm(Me.myView, EnumfrmMode.acEditM, frmIDX) Then
                    f.ShowDialog()
                End If
                MsgBox(oRet.Message)
            Else
                MsgBox(obj.message)
            End If

        End If

    End Sub

End Class