Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform.gst

Public Class frmGstDocNumTemplate
    Inherits frmMax

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.InitForm()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridLevel)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGstDocNumTemplateModel = Me.InitData("frmGstDocNumTemplateModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then

            UltraLabelCompany.Visible = False
            UltraLabelGSTReg.Visible = False
            cmb_CompanyID.Visible = False
            cmb_GSTRegID.Visible = False
            btnNewSeries.Visible = False
            UltraGridLevel.Visible = False

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Levels"),, btnDelSeries)
            myWinSQL.AssignCmb(Me.Model.dsCombo, "Company", "", Me.cmb_CompanyID)
            myWinSQL.AssignCmb(Me.Model.dsCombo, "GstReg", "", Me.cmb_GSTRegID)
            Me.cmb_Level.ValueList = Me.Model.ValueLists("Level").ComboList

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

    Private Sub cmb_Level_Leave(sender As Object, e As EventArgs) Handles cmb_Level.Leave, cmb_Level.AfterCloseUp
        GetLevels(myUtils.cStrTN(Me.cmb_Level.Value), myUtils.cValTN(cmb_CompanyID.Value), myUtils.cValTN(Me.cmb_GSTRegID.Value))
    End Sub

    Private Sub cmb_CompanyID_Leave(sender As Object, e As EventArgs) Handles cmb_CompanyID.Leave, cmb_CompanyID.AfterCloseUp
        GetLevels(myUtils.cStrTN(Me.cmb_Level.Value), myUtils.cValTN(cmb_CompanyID.Value), myUtils.cValTN(Me.cmb_GSTRegID.Value))
    End Sub

    Private Sub cmb_GSTRegID_Leave(sender As Object, e As EventArgs) Handles cmb_GSTRegID.Leave, cmb_GSTRegID.AfterCloseUp
        GetLevels(myUtils.cStrTN(Me.cmb_Level.Value), myUtils.cValTN(cmb_CompanyID.Value), myUtils.cValTN(Me.cmb_GSTRegID.Value))
    End Sub

    Private Function GetLevels(Level As String, CompanyID As Integer, GSTRegID As Integer)
        If myUtils.IsInList(myUtils.cStrTN(Level), "Tenant") Then
            myView.mainGrid.myDv.RowFilter = "CompanyID is Null and GSTRegID is Null"
            cmb_CompanyID.Visible = False
            UltraLabelCompany.Visible = False
            cmb_GSTRegID.Visible = False
            UltraLabelGSTReg.Visible = False
        End If
        If myUtils.IsInList(myUtils.cStrTN(Level), "Company") Then
            cmb_CompanyID.Visible = True
            UltraLabelCompany.Visible = True
            cmb_GSTRegID.Visible = False
            UltraLabelGSTReg.Visible = False
            If Me.cmb_CompanyID.SelectedRow IsNot Nothing Then myView.mainGrid.myDv.RowFilter = "CompanyID = " & myUtils.cValTN(cmb_CompanyID.SelectedRow.Cells("CompanyID").Value)
        End If
        If myUtils.IsInList(myUtils.cStrTN(Level), "GSTReg") Then
            cmb_GSTRegID.Visible = True
            UltraLabelGSTReg.Visible = True
            cmb_CompanyID.Visible = False
            UltraLabelCompany.Visible = False
            If Me.cmb_GSTRegID.SelectedRow IsNot Nothing Then myView.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(cmb_GSTRegID.SelectedRow.Cells("GSTRegID").Value)
        End If

        btnNewSeries.Visible = True
        UltraGridLevel.Visible = True

    End Function

    Private Sub btnNewSeries_Click(sender As Object, e As EventArgs) Handles btnNewSeries.Click

        Dim gr As UltraGridRow
        gr = myView.mainGrid.ButtonAction("add")
        If myUtils.cValTN(cmb_CompanyID.Value) > 0 Then gr.Cells("CompanyID").Value = myUtils.cValTN(cmb_CompanyID.Value)
        If myUtils.cValTN(cmb_GSTRegID.Value) > 0 Then gr.Cells("GstRegID").Value = myUtils.cValTN(cmb_GSTRegID.Value)
        myView.mainGrid.UpdateData()

    End Sub

End Class

