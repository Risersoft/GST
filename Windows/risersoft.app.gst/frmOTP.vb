Imports risersoft.API.GSTN
Imports System.Text
Imports risersoft.app.mxform.gst
Imports uwg = Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinGrid
Imports Newtonsoft.Json

Public Class frmOTP
    Inherits frmMax

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.InitForm()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridGSTReg)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmOTPModel = Me.InitData("frmOTPModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            UltraGridGSTReg.Text = myUtils.cStrTN(myRow("CompName"))

            btnSubmit.Visible = False
            btnCancel.Visible = False

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("GSTReg"))
            For Each gRow As uwg.UltraGridRow In myView.mainGrid.myGrid.Rows
                gRow.Cells("OTP").Activation = uwg.Activation.NoEdit
            Next

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

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            myView.mainGrid.myGrid.ActiveRow.Cells("OTP").Activation = uwg.Activation.AllowEdit

            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Dim oRet = Me.GenerateParamsOutput("otp", Params)

            Me.HandleActiveRow()
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@otp", "'" & myUtils.cStrTN(myView.mainGrid.myGrid.ActiveRow.Cells("otp").Value) & "'", GetType(String), False))
            Dim oRet = Me.GenerateParamsOutput("token", Params)
            If oRet.Success Then
                Dim obj = oRet.JsonData
                myView.mainGrid.myGrid.ActiveRow.Cells("ExpiryMins").Value = obj.ExpiryMins * 60
                myView.mainGrid.myGrid.UpdateData()
            End If
            Me.HandleActiveRow()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        myView.mainGrid.myGrid.ActiveRow.Cells("OTP").Activation = uwg.Activation.NoEdit
        Me.HandleActiveRow()
    End Sub

    Private Sub HandleActiveRow()
        Dim gRow = Me.UltraGridGSTReg.ActiveRow
        Dim r1 As DataRow = myWinUtils.DataRowFromGridRow(gRow)
        If myUtils.cValTN(r1("expirymins")) > 0 Then
            btnCancel.Visible = False
            btnSubmit.Visible = False
            btnConnect.Visible = False
        ElseIf gRow.Cells("OTP").Activation = uwg.Activation.AllowEdit Then
            btnCancel.Visible = True
            btnSubmit.Visible = True
            btnConnect.Visible = False
        Else
            btnCancel.Visible = False
            btnSubmit.Visible = False
            btnConnect.Visible = True
        End If
    End Sub
    Private Sub UltraGridGSTReg_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridGSTReg.AfterRowActivate
        Me.HandleActiveRow()
    End Sub
End Class

