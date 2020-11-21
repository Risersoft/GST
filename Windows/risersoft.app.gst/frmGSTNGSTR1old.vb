Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports risersoft.API.GSTN.GSTR1
Imports Newtonsoft.Json.Serialization
Imports risersoft.app.mxform
Imports risersoft.API.GSTN
Imports System.Text
Imports risersoft.app.mxform.gst

Public Class frmGSTNGSTR1old
    Inherits frmMax
    Dim myViewGSTReg As New clsWinView

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        myViewGSTReg.SetGrid(Me.UltraGrid2)

        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        OpenFileDialog1.FileName = ""
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGSTNGSTR1Model = Me.InitData("frmGstnGstr1Model", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            UltraGrid2.Text = myUtils.cStrTN(myRow("CompName"))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Invoice"))
            myViewGSTReg.PrepEdit(Me.Model.GridViews("GSTReg"))
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

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            OpenFileDialog1.Title = "Open File"
            OpenFileDialog1.DefaultExt = "json"
            OpenFileDialog1.Filter = "JSON Files|*.json"
            OpenFileDialog1.FilterIndex = 2
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim str1 As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)

                Dim Model = JsonConvert.DeserializeObject(Of GSTR1Total)(str1,
                                                                           New JsonSerializerSettings With {.Error = AddressOf HandleDeserializationError})
                Dim oProc As New clsGSTNReturnGSTR1(Me.Controller)
                Dim dic = oProc.PrepareGSTRAPayloadSQL(0, 0)
                Dim ds2 = SQLHelper.ExecuteDataset(CommandType.Text, dic)
                oProc.PopulateDataset(Model, ds2)
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("ReturnPeriodID").Value), GetType(Integer), False))
                Dim oRet = Me.GenerateDataParamsOutput("import", ds2, Params)
                MsgBox(oRet.Message)
            End If
        End If
    End Sub

    Public Sub HandleDeserializationError(sender As Object, args As ErrorEventArgs)
        Dim currentError = args.ErrorContext.Error.Message
        args.ErrorContext.Handled = True
    End Sub

    Private Sub btnGeneratePayload_Click(sender As Object, e As EventArgs) Handles btnGeneratePayload.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("ReturnPeriodID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@uploadtype", "'UM'", GetType(String), False))

            Dim oRet = Me.GenerateParamsOutput("payload", Params)
            MsgBox(oRet.Message, MsgBoxStyle.Information, Me.Controller.Vars("appname"))
        End If
    End Sub

    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("ReturnPeriodID").Value), GetType(Integer), False))

            Dim oRet = Me.GenerateParamsOutput("signdata", Params)
            Dim cert = DSCUtils.getCertificate()
            Dim toBeSigned As String = oRet.Description
            Dim byt = Encoding.UTF8.GetBytes(toBeSigned)
            Dim sign = Convert.ToBase64String(DSCUtils.SignCms(byt, cert))
            Params.Add(New clsSQLParam("@sign", sign, GetType(String), False))
            oRet = Me.GenerateParamsOutput("filesign", Params)
            MsgBox(oRet.Message)
        End If
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("ReturnPeriodID").Value), GetType(Integer), False))
            Dim oRet1 = Me.GenerateParamsOutput("otp", Params)
            Params.Add(New clsSQLParam("@otp", "'575757'", GetType(String), False))
            Dim oRet2 = Me.GenerateParamsOutput("token", Params)
        End If
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("ReturnPeriodID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@uploadtype", "'UM'", GetType(String), False))

            Dim oRet = Me.GenerateParamsOutput("upload", Params)
            MsgBox(oRet.Message)
        End If
    End Sub

    Private Sub btnGetAll_Click(sender As Object, e As EventArgs) Handles btnGetAll.Click
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ReturnPeriodID", myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("ReturnPeriodID").Value), GetType(Integer), False))

            Dim oRet = Me.GenerateParamsOutput("downloadall", Params)
            MsgBox(oRet.Message)
        End If
    End Sub

    Private Sub UltraGrid2_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGrid2.AfterRowActivate
        If Not IsNothing(myViewGSTReg.mainGrid.myGrid.ActiveRow) Then
            myView.mainGrid.myDv.RowFilter = "GstRegID = " & myUtils.cValTN(myViewGSTReg.mainGrid.myGrid.ActiveRow.Cells("GstRegID").Value)
        End If
    End Sub
End Class